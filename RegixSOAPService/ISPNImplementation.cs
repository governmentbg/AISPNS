
using AISTN.Data.DataModel;
using MathNet.Numerics;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using RegixSOAPService;
using System.Linq;
using System.Xml.Serialization;


public class ISPNImplementation : ispn
{
    private readonly IDbContextFactory<AistnContext> _dbFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ISPNImplementation(IDbContextFactory<AistnContext> dbFactory, IHttpContextAccessor httpContextAccessor)
    {
        _dbFactory = dbFactory;
        _httpContextAccessor = httpContextAccessor;

    }

    public async Task<LOGINResponse> LOGINAsync(LOGINRequest request)
    {
        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var callerIpAddress = httpContext.Connection.RemoteIpAddress.ToString();

            var response = new RESPONSE { TYPE = LotusHelper.RT_Session };

            AistnContext db = await _dbFactory.CreateDbContextAsync();

            var userRecord = db.RegixSoapusers.SingleOrDefault(x => x.Username == request.USER && x.IsActive == true);
            if (userRecord == null)
            {

                db.RegixFailedLogins.Add(new RegixFailedLogin { ProvidedUserName = request.USER, ProvidedPassword = request.PASS, Ipaddress = callerIpAddress  });
                await db.SaveChangesAsync();
                throw new InvalidParameterException();
            }

            if (PasswordHelper.GenerateSaltedHash(request.PASS, userRecord.Salt) != userRecord.HashedPassword)
            {

                db.RegixFailedLogins.Add(new RegixFailedLogin { ProvidedUserName = request.USER, ProvidedPassword = request.PASS, Ipaddress = callerIpAddress });
                await db.SaveChangesAsync();
                throw new InvalidParameterException();

            }

            userRecord.RegixSoapsessions.Add(new RegixSoapsession { CreationDate = DateTime.Now, ExpirationDate = DateTime.Now.AddSeconds(LotusHelper.TTL_DEFAULT), Ipaddress = callerIpAddress  });
            await db.SaveChangesAsync();

            var session = userRecord.RegixSoapsessions.First();
            var result = new LOGINResponse { LOGINReturn = new RESPONSE { TYPE = LotusHelper.RT_Session, SESSION = new SESSION { ID = session.Id.ToString(), TTL = LotusHelper.TTL_DEFAULT, USERNAME = request.USER } } };


            return result;

        }
        catch (InvalidParameterException ex)

        {
            var response = new RESPONSE { TYPE = LotusHelper.RT_Error, ERROR = new ERRORCLASS { DESCRIPTION = "Wrong username/password.", ID = LotusHelper.ERR_UserPass } };
            return new LOGINResponse { LOGINReturn = response };

        }
        catch (Exception ex)
        {
            throw;

        }




    }

    public async Task<PLUSResponse> PLUSAsync(PLUSRequest request)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PLUSRequest));
        using (StringWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, request);
            string requestXml = writer.ToString(); // This is your XML in a string
        }

        var result = new PLUSResponse();
        result.PLUSReturn = new RESPONSE { TYPE = LotusHelper.RT_Success, NUMBER = 5 };
        return result;
    }


    public async Task<QUERYResponse> QUERYAsync(QUERYRequest request)
    {
      

        if (!await ValidateUserSession(request.SESSIONID))
        {


            var errResult = new RESPONSE();

            errResult.TYPE = LotusHelper.RT_Error;
            errResult.ERROR = new ERRORCLASS { DESCRIPTION = @"Invalid/Expired session.", ID = LotusHelper.ERR_InvalidSession };
            return new QUERYResponse { QUERYReturn = errResult };
        }

        var UIC = request.STATENUMBER;
        var involvment = request.INVOLVEMENT;  //0 if not to be used

        AistnContext db = await _dbFactory.CreateDbContextAsync();

       // var involvementKind = db.NomInvolvementKinds.Where(x => x.Code == involvment).FirstOrDefault();


        var entityQuery = from e in db.Entities
                          where e.Bulstat == UIC
                          join s in db.Sides on e.Id equals s.EntityId
                          join c in db.Cases on s.CaseId equals c.Id
                          join nc in db.NomCourts on c.CourtId equals nc.Id
                          join ik in db.NomInvolvementKinds on 1 equals 1

                          where (involvment == 0 || ik.Code == involvment)
                          select new
                          {
                              c.Year,
                              c.FormationDate,
                              CaseNumber = c.Number,
                              CourtNumber = nc.Number,
                              CourtName = nc.Name,
                              Involvment = ik.Code,
                              InvolvmentText = ik.Description,
                              IsPerson = false,
                              SideName = e.Name,
                              StateNumber = e.Bulstat,
                              DateModified = e.DateModified
                          };


        var personQuery = from p in db.People
                          where p.Egn == UIC
                          join s in db.Sides on p.Id equals s.PersonId 
                          join c in db.Cases on s.CaseId equals c.Id
                          join nc in db.NomCourts on c.CourtId equals nc.Id
                          join ik in db.NomInvolvementKinds on 1 equals 1

                          where (involvment == 0 || ik.Code == involvment)
                          select new
                          {
                              c.Year,
                              c.FormationDate,
                              CaseNumber = c.Number,
                              CourtNumber = nc.Number,
                              CourtName = nc.Name,
                              Involvment = ik.Code,
                              InvolvmentText = ik.Description,
                              IsPerson = true,
                              SideName = p.FirstName + " " + (string.IsNullOrEmpty(p.MiddleName) ? "" : p.MiddleName + " ") + p.LastName,
                              StateNumber = p.Egn,
                              DateModified = s.DateModified
                          };


        var queryResultList = await (entityQuery.Union(personQuery)).Take(100).ToListAsync();
        var reslist = new List<RESULT>();

        foreach (var item in queryResultList)
        {
            reslist.Add(new RESULT
            {
                SIDE = new ISPNSIDE
                {
                    INVOLVEMENT = item.Involvment,
                    INVOLVEMENTTEXT = item.InvolvmentText,
                    ISPERSON = item.IsPerson,
                    NAME = item.SideName,
                    STATENUMBER = item.StateNumber,
                    LASTUPDATE = item.DateModified
                },
                CASE = new ISPNCASE
                {
                    COURT = item.CourtNumber.ToString(),
                    COURTNAME = item.CourtName,
                    NUMBER = int.TryParse(item.CaseNumber, out int parsedValue) ? (int)parsedValue : 0,
                    DATE = item.FormationDate,
                    YEAR = item.Year,
                    LASTUPDATE = item.DateModified
                },
                LASTUPDATE = null
            });
        }


        var userSession = await db.RegixSoapsessions.Where(x => x.Id == new Guid(request.SESSIONID)).SingleOrDefaultAsync();
        userSession.RegixSoaprequests.Add(new RegixSoaprequest { InvolvmentKind = involvment.ToString() , StateNumber = UIC, ResultsCount = reslist.Count , CreationDate = DateTime.Now});
        await db.SaveChangesAsync();


        var result = new RESPONSE();

        result.RESULTSET = new RESULTSET();
        result.RESULTSET.RESULTS = reslist.ToArray();
        result.RESULTSET.COUNT = reslist.Count;
        return new QUERYResponse { QUERYReturn = result };

    }


    private async Task<bool> ValidateUserSession(string sessionID)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var callerIpAddress = httpContext.Connection.RemoteIpAddress.ToString();

        AistnContext db = await _dbFactory.CreateDbContextAsync();

        var now = DateTime.Now;

        Guid testGuid;
        if (!Guid.TryParse(sessionID, out testGuid))
        {
            db.RegixUnrecognizedSessions.Add(new RegixUnrecognizedSession { ProvidedToken = sessionID.ToString(), Ipaddress = callerIpAddress, CreationDate = DateTime.Now });
            await db.SaveChangesAsync();
            return false;
        }

        var userSession = await db.RegixSoapsessions.Where(x => x.Id == testGuid).SingleOrDefaultAsync();

        if (userSession == null)
        {
            db.RegixUnrecognizedSessions.Add(new RegixUnrecognizedSession { ProvidedToken = sessionID.ToString(), Ipaddress = callerIpAddress  , CreationDate = DateTime.Now});
            await db.SaveChangesAsync();
            return false;

        }

        return userSession.ExpirationDate > now;

    }
}
