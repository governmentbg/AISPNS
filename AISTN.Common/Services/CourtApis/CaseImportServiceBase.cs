using AISTN.Common.Helper;
using AISTN.Common.Helper.SystemEnums;
using AISTN.Common.Models.CourtApis.ExtendedRequest;
using AISTN.Data.DataModel;
using AISTN.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AISTN.Common.Services.CourtApis
{
    public abstract class CaseImportServiceBase
    {
        protected readonly ILogger<CaseImportServiceBase> _logger;
        protected readonly IDbContextFactory<AistnContextLoggable> _dbFactory;
        private readonly CacheManager _cache;

        public CaseImportServiceBase(IDbContextFactory<AistnContextLoggable> dbFactory, ILogger<CaseImportServiceBase> logger, CacheManager cache)
        {
            _dbFactory = dbFactory;
            _logger = logger;
            _cache = cache;
        }

        protected abstract bool _isExtendedTransfer { get; }

        public async Task<bool> ImportTransfer(XElement xml)
        {
            XDocument xmlDoc = await ValidateXmlDocument(xml);

            Transfer transfer = await DeserializeTransfer(xmlDoc);

            await validateTransferContents(transfer);

            bool result = await ExecuteImport(transfer);

            return result;
        }

        private List<NomCourt> nomCourts
        {
            get
            {
                var result = _cache.GetOrSet(nameof(NomCourt), () =>
                {
                    AistnContextLoggable _db = _dbFactory.CreateDbContextAsync().Result;
                    return _db.NomCourts.AsNoTracking().ToList();
                });

                return result;
            }
        }
        private List<NomCaseCode> nomCaseCodes
        {
            get
            {
                var result = _cache.GetOrSet(nameof(NomCaseCode), () =>
                {
                    AistnContextLoggable _db = _dbFactory.CreateDbContextAsync().Result;
                    return _db.NomCaseCodes.AsNoTracking().ToList();
                });

                return result;
            }
        }

        private List<NomCaseKind> nomCaseKinds
        {
            get
            {
                var result = _cache.GetOrSet(nameof(NomCaseKind), () =>
                {
                    AistnContextLoggable _db = _dbFactory.CreateDbContextAsync().Result;
                    return _db.NomCaseKinds.AsNoTracking().ToList();
                });

                return result;
            }
        }

        private List<NomSendToKind> nomSendToKind
        {
            get
            {
                var result = _cache.GetOrSet(nameof(NomSendToKind), () =>
                {
                    AistnContextLoggable _db = _dbFactory.CreateDbContextAsync().Result;
                    return _db.NomSendToKinds.AsNoTracking().ToList();
                });

                return result;
            }
        }

        private List<NomActKind> nomActKinds
        {
            get
            {
                var result = _cache.GetOrSet(nameof(NomActKind), () =>
                {
                    AistnContextLoggable _db = _dbFactory.CreateDbContextAsync().Result;
                    return _db.NomActKinds.AsNoTracking().ToList();
                });

                return result;
            }
        }

        private List<NomAppealKind> nomAppealKinds
        {
            get
            {
                var result = _cache.GetOrSet(nameof(NomAppealKind), () =>
                {
                    AistnContextLoggable _db = _dbFactory.CreateDbContextAsync().Result;
                    return _db.NomAppealKinds.AsNoTracking().ToList();
                });

                return result;
            }
        }


        private List<NomIncomingDocumentKind> nomIncomingDocumentKinds
        {
            get
            {
                var result = _cache.GetOrSet(nameof(NomIncomingDocumentKind), () =>
                {
                    AistnContextLoggable _db = _dbFactory.CreateDbContextAsync().Result;
                    return _db.NomIncomingDocumentKinds.AsNoTracking().ToList();
                });

                return result;
            }
        }

        private List<NomInvolvementKind> nomInvolvementKinds
        {
            get
            {
                var result = _cache.GetOrSet(nameof(NomInvolvementKind), () =>
                {
                    AistnContextLoggable _db = _dbFactory.CreateDbContextAsync().Result;
                    return _db.NomInvolvementKinds.AsNoTracking().ToList();
                });

                return result;
            }
        }

        private List<NomSendToKind> nomSendToKinds
        {
            get
            {
                var result = _cache.GetOrSet(nameof(NomSendToKind), () =>
                {
                    AistnContextLoggable _db = _dbFactory.CreateDbContextAsync().Result;
                    return _db.NomSendToKinds.AsNoTracking().ToList();
                });

                return result;
            }
        }

        private List<NomSessionResult> nomSessionResults
        {
            get
            {
                var result = _cache.GetOrSet(nameof(NomSessionResult), () =>
                {
                    AistnContextLoggable _db = _dbFactory.CreateDbContextAsync().Result;
                    return _db.NomSessionResults.AsNoTracking().ToList();
                });

                return result;
            }
        }

        private List<NomSessionKind> nomSessionKinds
        {
            get
            {
                var result = _cache.GetOrSet(nameof(NomSessionKind), () =>
                {
                    AistnContextLoggable _db = _dbFactory.CreateDbContextAsync().Result;
                    return _db.NomSessionKinds.AsNoTracking().ToList();
                });

                return result;
            }
        }


        private async Task<XDocument> ValidateXmlDocument(XElement xml)
        {
            XDocument xmlDoc;

            try
            {
                xmlDoc = new XDocument(xml);
            }
            catch (XmlException)
            {
                throw new BusinessException("Not a valid XML");
            }

            string XsdString = await GetXSD();

            XmlSchema schema = XmlSchema.Read(new StringReader(XsdString), validationCallback)!;

            var schemas = new XmlSchemaSet();
            schemas.Add(schema!);

            try
            {
                xmlDoc.Validate(schemas, null);
            }
            catch (XmlSchemaValidationException ex)
            {
                throw new BusinessException($"XML not valid against XSD. Reason:{Environment.NewLine}{ex.Message}");
            }

            return xmlDoc;
        }

        private async Task<Transfer> DeserializeTransfer(XDocument xmlDoc)
        {
            var transferElement = xmlDoc.Element("Transfer") ?? xmlDoc.Root!.Element("Transfer");

            XDocument? transferXml = new XDocument(transferElement);

            XmlSerializer s = new XmlSerializer(typeof(Transfer));

            Transfer? transfer = deserializeWithBinaryData<Transfer>(transferXml);

            if (transfer == null) throw new BusinessException("Unable to deserialize import transfer xml");

            return transfer;
        }

        private async Task<bool> ExecuteImport(Transfer transfer)
        {
            try
            {
                AistnContextLoggable _db = await _dbFactory.CreateDbContextAsync();

                Guid caseId = Guid.Parse(transfer.Case.case_id);

                Case? dbCase = LoadCase(caseId, _db);

                var now = DateTime.Now;

                eUserActionType actionType = 0;

                if (dbCase == null)
                {
                    actionType = eUserActionType.CreateCaseFile;

                    InsertCase(transfer, _db, now);
                }
                else
                {
                    actionType = UpdateCase(transfer, _db, dbCase, now);
                }

                _db.SaveChanges(UserActivity.NewActivity(new CurrentUser
                {
                    PersonId = 1,
                    IpAddress = "127.0.0.1",
                    IsAuthenticated = true,
                    UserId = Guid.NewGuid(),
                    UserName = "Test",
                }, actionType));

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in {MethodBase.GetCurrentMethod()?.Name}");
                throw;
            }

            Case? LoadCase(Guid caseId, AistnContextLoggable _db)
            {
                return _db.Cases
                                .Include(x => x.SurroundDocuments)
                                    .ThenInclude(x => x.Sides)
                                        .ThenInclude(x => x.Person)
                                .Include(x => x.SurroundDocuments)
                                    .ThenInclude(x => x.Sides)
                                        .ThenInclude(x => x.Entity)
                                .Include(x => x.IncomingDocuments)
                                .Include(x => x.Sessions)
                                    .ThenInclude(x => x.Acts)
                                        .ThenInclude(x => x.Appeals)
                                            .ThenInclude(x => x.Sides)
                                                .ThenInclude(x => x.Person)
                                .Include(x => x.Sessions)
                                    .ThenInclude(x => x.Acts)
                                        .ThenInclude(x => x.Appeals)
                                            .ThenInclude(x => x.Sides)
                                                .ThenInclude(x => x.Entity)
                                .Include(x => x.Sessions)
                                    .ThenInclude(x => x.Acts)
                                        .ThenInclude(x => x.Appeals)
                                            .ThenInclude(x => x.OutgoingDocuments)
                                .Include(x => x.Sessions)
                                    .ThenInclude(x => x.Judges)
                                .Include(x => x.Sides)
                                    .ThenInclude(x => x.Person)
                                .Include(x => x.Sides)
                                    .ThenInclude(x => x.Entity)
                                .Include(x => x.Judges)
                                .AsSplitQuery()
                                .SingleOrDefault(x => x.Id == caseId);
            }

            void InsertCase(Transfer transfer, AistnContextLoggable _db, DateTime now)
            {
                switch (transfer.Case.case_action)
                {
                    case (int)eAction.UPDATE_13002:
                        _logger.LogWarning($"Case ({transfer.Case.case_id}) Action is Update, but Case does not exist in the DB. Threating it as Insert");
                        goto case (int)eAction.INSERT_13001;

                    case (int)eAction.INSERT_13001:

                        #region Insert Case

                        Case newCase = new Case();

                        #region Case
                        newCase.Id = Guid.Parse(transfer.Case.case_id);
                        newCase.CourtId = nomCourts.SingleOrDefault(x => x.Number == transfer.Case.case_court)?.Id ?? throw new BusinessException($"Unknown court ({transfer.Case.case_court})");
                        newCase.DateCreated = now;
                        newCase.Year = parseYear(transfer.Case.case_year);
                        newCase.CaseKindId = nomCaseKinds.SingleOrDefault(x => x.Code == transfer.Case.case_kind)?.Id ?? throw new BusinessException($"Unknown case kind ({transfer.Case.case_kind})");
                        newCase.Number = transfer.Case.case_no.ToString();
                        newCase.FormationDate = transfer.Case.case_date;

                        if (_isExtendedTransfer)
                        {
                            newCase.StatCode = transfer.Case.case_code;

                            var code = nomCaseCodes.SingleOrDefault(x => x.Code == transfer.Case.case_code) ?? throw new BusinessException($"Unknown case code ({transfer.Case.case_code})");

                            newCase.CaseCodeId = code.Id;
                            newCase.IsStabilization = code?.SetIsStabilizationFlag;
                            newCase.IsProprietor = code?.SetIsProprietorFlag;
                        }
                        #endregion

                        #region Surround
                        if (transfer.Case.Surround == null) transfer.Case.Surround = Array.Empty<SurroundType>();

                        foreach (var surround in transfer.Case.Surround)
                        {
                            switch (surround.surround_action)
                            {
                                case (int)eAction.UPDATE_13002:
                                    _logger.LogWarning($"Surround ({surround.surround_id}) Action is Update, but Surround does not exist in the DB. Threating it as Insert");
                                    goto case (int)eAction.INSERT_13001;

                                case (int)eAction.INSERT_13001:
                                    {
                                        InsertSurround(_db, now, newCase, surround);
                                    }
                                    break;
                                case (int)eAction.DELETE_13003:
                                case (int)eAction.IGNORE_13004:
                                    throw new EntityNotFoundException<string>(typeof(Case), transfer.Case.case_id);
                                    break;
                                default: throw new BusinessException("Unknown action");
                            }
                        }
                        #endregion

                        #region Sessions
                        if (transfer.Case.Session == null) transfer.Case.Session = Array.Empty<SessionType>();

                        foreach (var session in transfer.Case.Session)
                        {
                            switch (session.session_action)
                            {
                                case (int)eAction.UPDATE_13002:
                                    _logger.LogWarning($"Session ({session.session_id}) Action is Update, but Session does not exist in the DB. Threating it as Insert");
                                    goto case (int)eAction.INSERT_13001;

                                case (int)eAction.INSERT_13001:
                                    {
                                        InsertSession(_db, now, newCase, session);
                                    }
                                    break;
                                default: throw new BusinessException("Session action should only be insert");
                            }
                        }
                        #endregion

                        #region Incoming Documents
                        if (transfer.Case.InDoc != null)
                        {
                            newCase.IncomingDocuments.Add(new IncomingDocument
                            {
                                Id = Guid.Parse(transfer.Case.InDoc.indoc_id),
                                KindId = nomIncomingDocumentKinds.SingleOrDefault(x => x.Code == transfer.Case.InDoc.indoc_kind)?.Id ?? throw new BusinessException($"Unknown incoming document kind ({transfer.Case.InDoc.indoc_kind})"),
                                Number = (short)transfer.Case.InDoc.indoc_no,
                                Year = (short)transfer.Case.InDoc.indoc_year,
                                Date = transfer.Case.InDoc.indoc_date,
                                DateCreated = now,
                            });
                        }
                        #endregion

                        #region Sides
                        if (transfer.Case.Side == null) transfer.Case.Side = Array.Empty<SideType>();

                        foreach (var side in transfer.Case.Side)
                        {
                            InsertSide<Case>(newCase, _db, now, newCase, side);
                        }
                        #endregion

                        #region Judge
                        if (transfer.Case.Judge != null)
                        {
                            newCase.Judges.Add(new Judge()
                            {

                                Id = Guid.Parse(transfer.Case.Judge.judge_id),
                                Name = transfer.Case.Judge.judge_name_1,
                                Rename = transfer.Case.Judge.judge_rename,
                                Family = transfer.Case.Judge.judge_family_1,
                                DateCreated = now,
                                Case = newCase
                            });
                        }
                        #endregion

                        _db.Cases.Add(newCase);

                        #endregion

                        break;

                    case (int)eAction.DELETE_13003:
                    case (int)eAction.IGNORE_13004:
                        _logger.LogError($"Case ({transfer.Case.case_id}) Action is DELETE/IGNORE, but Case does not exist in the DB.");
                        throw new EntityNotFoundException<string>(typeof(Case), transfer.Case.case_id);
                    default: throw new BusinessException("Unknown action");
                }

                return;
            }

            eUserActionType UpdateCase(Transfer transfer, AistnContextLoggable _db, Case dbCase, DateTime now)
            {
                eUserActionType actionType = 0;

                switch (transfer.Case.case_action)
                {
                    case (int)eAction.INSERT_13001:
                        throw new BusinessException($"Case ({transfer.Case.case_id}) Action is INSERT But Case is already inserted");

                    case (int)eAction.UPDATE_13002:
                        actionType = eUserActionType.EditCaseFile;

                        #region Update Case

                        #region Case
                        dbCase.CourtId = nomCourts.SingleOrDefault(x => x.Number == transfer.Case.case_court)?.Id ?? throw new BusinessException($"Unknown court ({transfer.Case.case_court})");
                        dbCase.DateModified = now;
                        dbCase.Year = parseYear(transfer.Case.case_year);
                        dbCase.CaseKindId = nomCaseKinds.SingleOrDefault(x => x.Code == transfer.Case.case_kind)?.Id ?? throw new BusinessException($"Unknown case kind ({transfer.Case.case_kind})");
                        dbCase.Number = transfer.Case.case_no.ToString();
                        dbCase.FormationDate = transfer.Case.case_date;

                        if (_isExtendedTransfer)
                        {
                            dbCase.StatCode = transfer.Case.case_code;

                            var code = nomCaseCodes.SingleOrDefault(x => x.Code == transfer.Case.case_code) ?? throw new BusinessException($"Unknown case code ({transfer.Case.case_code})");

                            dbCase.CaseCodeId = code.Id;
                            dbCase.IsStabilization = code?.SetIsStabilizationFlag;
                            dbCase.IsProprietor = code?.SetIsProprietorFlag;
                        }
                        #endregion

                        #region Surround
                        //Add/Update - Surround
                        if (transfer.Case.Surround == null) transfer.Case.Surround = Array.Empty<SurroundType>();

                        foreach (var surround in transfer.Case.Surround)
                        {
                            var dbSurround = dbCase.SurroundDocuments.SingleOrDefault(x => x.Id == Guid.Parse(surround.surround_id));

                            switch (surround.surround_action)
                            {
                                case (int)eAction.INSERT_13001:
                                    if (dbSurround != null) _logger.LogWarning($"Surround ({surround.surround_id}) Action is INSERT, but Surround exist in the DB. Threating it as UPDATE");
                                    goto case (int)eAction.UPDATE_13002;

                                case (int)eAction.UPDATE_13002:
                                    if (dbSurround == null && surround.surround_action == (int)eAction.UPDATE_13002) _logger.LogWarning($"Surround ({surround.surround_id}) Action is UPDATE, but Surround does not exist in the DB. Threating it as INSERT");

                                    if (dbSurround == null)
                                    {
                                        InsertSurround(_db, now, dbCase, surround);
                                    }
                                    else
                                    {
                                        UpdateSurround(_db, dbCase, now, surround, dbSurround);
                                    }
                                    break;

                                case (int)eAction.IGNORE_13004:
                                    //Do nothing on Ignore...
                                    break;

                                case (int)eAction.DELETE_13003:
                                    if (dbSurround != null)
                                    {
                                        dbSurround.Sides.Where(s => s.Person != null).Select(x => x.Person!).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                                        dbSurround.Sides.Where(s => s.Entity != null).Select(x => x.Entity!).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                                        dbSurround.Sides.ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                                        _db.Entry(dbSurround).State = EntityState.Deleted;
                                    }
                                    break;
                                default: throw new BusinessException("Unknown action");
                            }
                        }

                        //Delete surrounds that exist in the DB but are no longer in the request
                        DeleteSurrounds(transfer, _db, dbCase);
                        #endregion

                        #region Sessions
                        if (transfer.Case.Session == null) transfer.Case.Session = Array.Empty<SessionType>();

                        foreach (var session in transfer.Case.Session)
                        {
                            var dbSession = dbCase.Sessions.SingleOrDefault(x => x.Id == Guid.Parse(session.session_id));

                            switch (session.session_action)
                            {
                                case (int)eAction.INSERT_13001:
                                    if (dbSession != null) _logger.LogWarning($"Session ({session.session_id}) Action is INSERT, but Session exist in the DB. Threating it as UPDATE");
                                    goto case (int)eAction.UPDATE_13002;

                                case (int)eAction.UPDATE_13002:
                                    if (dbSession == null && session.session_action == (int)eAction.UPDATE_13002) _logger.LogWarning($"Session ({session.session_id}) Action is UPDATE, but Session does not exist in the DB. Threating it as INSERT");

                                    if (dbSession == null)
                                    {
                                        InsertSession(_db, now, dbCase, session);
                                    }
                                    else
                                    {
                                        UpdateSession(_db, dbCase, now, session, dbSession);
                                    }
                                    break;
                                case (int)eAction.IGNORE_13004:
                                    //Do nothing on Ignore...
                                    break;

                                case (int)eAction.DELETE_13003:
                                    if (dbSession != null)
                                    {
                                        dbSession.Acts.SelectMany(x => x.Appeals).SelectMany(x => x.Sides).Where(x => x.Person != null).Select(x => x.Person!).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                                        dbSession.Acts.SelectMany(x => x.Appeals).SelectMany(x => x.Sides).Where(x => x.Entity != null).Select(x => x.Entity!).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                                        dbSession.Acts.SelectMany(x => x.Appeals).Select(x => x.Sides).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                                        dbSession.Acts.SelectMany(x => x.Appeals).Select(x => x.OutgoingDocuments).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                                        dbSession.Acts.SelectMany(x => x.Appeals).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                                        dbSession.Acts.ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                                        dbSession.Judges.ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                                        _db.Entry(dbSession).State = EntityState.Deleted;
                                    }
                                    break;
                                default: throw new BusinessException("Session action should only be insert"); break;
                            }
                        }

                        //Delete session that exist in the DB but are no longer in the request
                        DeleteDefuncSessions(transfer, _db, dbCase);
                        #endregion

                        #region Incoming Documents
                        if (transfer.Case.InDoc != null)
                        {
                            var dbIncDoc = dbCase.IncomingDocuments.SingleOrDefault(x => x.Id == Guid.Parse(transfer.Case.InDoc.indoc_id));

                            if (dbIncDoc == null)
                            {
                                //Add IncDoc
                                dbCase.IncomingDocuments.Add(new IncomingDocument
                                {
                                    Id = Guid.Parse(transfer.Case.InDoc.indoc_id),
                                    KindId = nomIncomingDocumentKinds.SingleOrDefault(x => x.Code == transfer.Case.InDoc.indoc_kind)?.Id ?? throw new BusinessException($"Unknown incoming document kind ({transfer.Case.InDoc.indoc_kind})"),
                                    Number = (short)transfer.Case.InDoc.indoc_no,
                                    Year = (short)transfer.Case.InDoc.indoc_year,
                                    Date = transfer.Case.InDoc.indoc_date,
                                    DateCreated = now,
                                });
                            }
                            else
                            {
                                //Update IncDoc
                                dbIncDoc.KindId = nomIncomingDocumentKinds.SingleOrDefault(x => x.Code == transfer.Case.InDoc.indoc_kind)?.Id ?? throw new BusinessException($"Unknown incoming document kind ({transfer.Case.InDoc.indoc_kind})");
                                dbIncDoc.Number = (short)transfer.Case.InDoc.indoc_no;
                                dbIncDoc.Year = (short)transfer.Case.InDoc.indoc_year;
                                dbIncDoc.Date = transfer.Case.InDoc.indoc_date;
                                dbIncDoc.DateModified = now;
                            }
                        }
                        else
                        {
                            //Delete Incoming Doc that exist in the DB but are no longer in the request                                
                            dbCase.IncomingDocuments
                                    .ToList()
                                    .ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                        }
                        #endregion

                        #region Sides
                        if (transfer.Case.Side == null) transfer.Case.Side = Array.Empty<SideType>();

                        foreach (var side in transfer.Case.Side)
                        {
                            Side? dbSide = dbCase.Sides.SingleOrDefault(x => x.Id == Guid.Parse(side.side_id));

                            if (dbSide == null)
                            {
                                InsertSide<Case>(dbCase, _db, now, dbCase, side);
                            }
                            else
                            {
                                UpdateSide(_db, now, side, dbSide);
                            }
                        }

                        //Delete Sides that exist in the DB but are no longer in the request
                        DeleteDefuncCaseSides(transfer, _db, dbCase);
                        #endregion

                        #region Judge
                        if (transfer.Case.Judge != null)
                        {
                            var dbJudge = dbCase.Judges.SingleOrDefault(x => x.Id == Guid.Parse(transfer.Case.Judge.judge_id));

                            if (dbJudge == null)
                            {
                                //Add Judge
                                dbCase.Judges.Add(new Judge()
                                {
                                    Id = Guid.Parse(transfer.Case.Judge.judge_id),
                                    Name = transfer.Case.Judge.judge_name_1,
                                    Rename = transfer.Case.Judge.judge_rename,
                                    Family = transfer.Case.Judge.judge_family_1,
                                    DateCreated = now,
                                    Case = dbCase
                                });
                            }
                            else
                            {
                                //Update Judge
                                dbJudge.Name = transfer.Case.Judge.judge_name_1;
                                dbJudge.Family = transfer.Case.Judge.judge_family_1;
                                dbJudge.DateModified = now;
                                dbJudge.Case = dbCase;
                            }
                        }

                        //Delete case judges that exist in the DB but are no longer in the request
                        dbCase.Judges.Where(x => x.SessionId is null && (transfer.Case.Judge is null || x.Id != Guid.Parse(transfer.Case.Judge.judge_id)))
                                    .ToList()
                                    .ForEach(j => _db.Entry(j).State = EntityState.Deleted);

                        #endregion

                        #endregion

                        break;

                    case (int)eAction.DELETE_13003:
                        actionType = eUserActionType.DeleteCaseFile;
                        //TODO check if we can delete this Case somehow?

                        DeleteCase(_db, dbCase);
                        break;

                    case (int)eAction.IGNORE_13004:
                        //Do nothing on Ignore...
                        actionType = eUserActionType.IgnoreCaseFile;
                        break;
                }

                return actionType;
            }

            void DeleteDefuncCaseSides(Transfer transfer, AistnContextLoggable _db, Case dbCase)
            {
                foreach (var dbSide in dbCase.Sides.Where(x => x.SurroundDocumentId is null)
                                                    .Where(x => x.AppealId is null)
                                                    .Where(dbS => !transfer.Case.Side
                                                                                        .Select(s => Guid.Parse(s.side_id))
                                                                                        .Contains(dbS.Id))
                                                    .ToList())
                {
                    if (dbSide.Person != null)
                    {
                        _db.Entry(dbSide.Person).State = EntityState.Deleted;
                    }

                    if (dbSide.Entity != null)
                    {
                        _db.Entry(dbSide.Entity).State = EntityState.Deleted;
                    }

                    _db.Entry(dbSide).State = EntityState.Deleted;
                }
            }

            void DeleteCase(AistnContextLoggable _db, Case dbCase)
            {
                _db.RemoveRange(dbCase.SurroundDocuments.SelectMany(x => x.Sides.Where(s => s.Person != null)).Select(x => x.Person!));
                _db.RemoveRange(dbCase.SurroundDocuments.SelectMany(x => x.Sides.Where(s => s.Entity != null)).Select(x => x.Entity!));
                _db.RemoveRange(dbCase.SurroundDocuments.SelectMany(x => x.Sides));
                _db.RemoveRange(dbCase.SurroundDocuments);
                _db.RemoveRange(dbCase.IncomingDocuments);
                _db.RemoveRange(dbCase.Sessions.SelectMany(x => x.Acts).SelectMany(x => x.Appeals).SelectMany(x => x.Sides).Where(x => x.Person != null).Select(x => x.Person!));
                _db.RemoveRange(dbCase.Sessions.SelectMany(x => x.Acts).SelectMany(x => x.Appeals).SelectMany(x => x.Sides).Where(x => x.Entity != null).Select(x => x.Entity!));
                _db.RemoveRange(dbCase.Sessions.SelectMany(x => x.Acts).SelectMany(x => x.Appeals).SelectMany(x => x.Sides));
                _db.RemoveRange(dbCase.Sessions.SelectMany(x => x.Acts).SelectMany(x => x.Appeals).SelectMany(x => x.OutgoingDocuments));
                _db.RemoveRange(dbCase.Sessions.SelectMany(x => x.Acts).SelectMany(x => x.Appeals));
                _db.RemoveRange(dbCase.Sessions.SelectMany(x => x.Acts));
                _db.RemoveRange(dbCase.Sessions.SelectMany(x => x.Judges));
                _db.RemoveRange(dbCase.Sessions);
                _db.RemoveRange(dbCase.Sides.Where(x => x.Person != null).Select(x => x.Person!));
                _db.RemoveRange(dbCase.Sides.Where(x => x.Entity != null).Select(x => x.Entity!));
                _db.RemoveRange(dbCase.Sides);
                _db.RemoveRange(dbCase.Judges);
                _db.Remove(dbCase);
            }


            void InsertSurround(AistnContextLoggable _db, DateTime now, Case newCase, SurroundType surround)
            {
                //Check if this entity already exists but for another case
                if (_db.SurroundDocuments.Any(x => x.Id == Guid.Parse(surround.surround_id)))
                    throw new BusinessException($"Surround Document ({surround.surround_id}) already exists for different case");

                var newSurround = new SurroundDocument()
                {

                    Id = Guid.Parse(surround.surround_id),
                    Number = surround.surround_no,
                    Year = parseYear(surround.surround_year),
                    Date = surround.surround_date,
                    Text = surround.surround_text,
                    DateCreated = now,
                };

                foreach (var side in surround.Side)
                {
                    InsertSide<SurroundDocument>(newSurround, _db, now, newCase, side);
                }

                newCase.SurroundDocuments.Add(newSurround);

            }

            void UpdateSurround(AistnContextLoggable _db, Case dbCase, DateTime now, SurroundType surround, SurroundDocument dbSurround)
            {
                dbSurround.Number = surround.surround_no;
                dbSurround.Year = parseYear(surround.surround_year);
                dbSurround.Date = surround.surround_date;
                dbSurround.Text = surround.surround_text;
                dbSurround.DateModified = now;

                foreach (var side in surround.Side)
                {
                    Side? dbSide = dbCase.SurroundDocuments.SelectMany(x => x.Sides)
                                                            .SingleOrDefault(x => x.Id == Guid.Parse(side.side_id));

                    if (dbSide == null)
                    {
                        InsertSide<SurroundDocument>(dbSurround, _db, now, dbCase, side);
                    }
                    else
                    {
                        UpdateSide(_db, now, side, dbSide);
                    }
                }

                //Delete surround sides that exist in the DB but are no longer in the request
                DeleteDefuncSurroundSides(_db, surround, dbSurround);
            }

            void DeleteSurrounds(Transfer transfer, AistnContextLoggable _db, Case dbCase)
            {
                foreach (var dbSur in dbCase.SurroundDocuments
                                            .Where(dbS => !transfer.Case.Surround
                                                                                .Select(s => Guid.Parse(s.surround_id))
                                                                                .Contains(dbS.Id))
                                            .ToList())
                {
                    dbSur.Sides.Where(x => x.Person is not null)
                                .Select(x => x.Person)
                                .ToList()
                                .ForEach(x => _db.Entry(x!).State = EntityState.Deleted);

                    dbSur.Sides.Where(x => x.Entity is not null)
                                .Select(x => x.Entity)
                                .ToList()
                                .ForEach(x => _db.Entry(x!).State = EntityState.Deleted);

                    dbSur.Sides.ToList().ForEach(x => _db.Entry(x!).State = EntityState.Deleted);

                    _db.Entry(dbSur).State = EntityState.Deleted;
                }
            }

            void DeleteDefuncSurroundSides(AistnContextLoggable _db, SurroundType surround, SurroundDocument dbSurround)
            {
                foreach (var dbSurSide in dbSurround.Sides.Where(dbSS => !surround.Side
                                                                                            .Select(s => Guid.Parse(s.side_id))
                                                                                            .Contains(dbSS.Id))
                                                            .ToList())
                {
                    if (dbSurSide.Person != null)
                    {
                        _db.Entry(dbSurSide.Person).State = EntityState.Deleted;
                    }

                    if (dbSurSide.Entity != null)
                    {
                        _db.Entry(dbSurSide.Entity).State = EntityState.Deleted;
                    }

                    _db.Entry(dbSurSide).State = EntityState.Deleted;
                }
            }


            void InsertSession(AistnContextLoggable _db, DateTime now, Case newCase, SessionType session)
            {
                //Check if this entity already exists but for another case
                if (_db.Sessions.Any(x => x.Id == Guid.Parse(session.session_id)))
                    throw new BusinessException($"Session ({session.session_id}) already exists for different case");

                var newSession = new Session();

                newSession.Id = Guid.Parse(session.session_id);
                newSession.SessionKindId = nomSessionKinds.SingleOrDefault(x => x.Code == session.session_kind)?.Id ?? throw new BusinessException($"Unknown session kind ({session.session_kind})");
                newSession.Date = session.session_date;
                newSession.ResultId = nomSessionResults.SingleOrDefault(x => x.Code == session.session_result)?.Id ?? throw new BusinessException($"Unknown session result ({session.session_result})");
                newSession.Text = session.session_text;
                newSession.DateCreated = now;

                if (session.Judge == null) session.Judge = Array.Empty<JudgeType>();

                foreach (var judge in session.Judge)
                {
                    newSession.Judges.Add(new Judge()
                    {
                        Id = Guid.Parse(judge.judge_id),
                        Name = judge.judge_name_1,
                        Rename = judge.judge_rename,
                        Family = judge.judge_family_1,
                        DateCreated = now,
                        Case = newCase
                    });
                }

                if (session.Act == null) session.Act = Array.Empty<ActType>();

                foreach (var act in session.Act)
                {
                    InsertSessionAct(_db, now, newSession, act, newCase);
                }


                newCase.Sessions.Add(newSession);
            }

            void UpdateSession(AistnContextLoggable _db, Case dbCase, DateTime now, SessionType session, Session dbSession)
            {
                dbSession.SessionKindId = nomSessionKinds.SingleOrDefault(x => x.Code == session.session_kind)?.Id ?? throw new BusinessException($"Unknown session kind ({session.session_kind})");
                dbSession.Date = session.session_date;
                dbSession.ResultId = nomSessionResults.SingleOrDefault(x => x.Code == session.session_result)?.Id ?? throw new BusinessException($"Unknown session result ({session.session_result})");
                dbSession.Text = session.session_text;
                dbSession.DateModified = now;

                if (session.Judge == null) session.Judge = Array.Empty<JudgeType>();

                foreach (var judge in session.Judge)
                {
                    var dbSessionJudge = dbSession.Judges.SingleOrDefault(x => x.Id == Guid.Parse(judge.judge_id));

                    if (dbSessionJudge == null)
                    {
                        //Add Session Judge
                        dbSession.Judges.Add(new Judge()
                        {
                            Id = Guid.Parse(judge.judge_id),
                            Name = judge.judge_name_1,
                            Rename = judge.judge_rename,
                            Family = judge.judge_family_1,
                            DateCreated = now,
                            Case = dbCase
                        });
                    }
                    else
                    {
                        //Update Session Judge
                        dbSessionJudge.Name = judge.judge_name_1;
                        dbSessionJudge.Family = judge.judge_family_1;
                        dbSessionJudge.DateModified = now;
                        dbSessionJudge.Case = dbCase;
                    }
                }

                //Delete Session Judges that exist in the DB but are no longer in the request
                dbSession.Judges.Where(dbSJ => !session.Judge
                                                                .Select(j => Guid.Parse(j.judge_id))
                                                                .Contains(dbSJ.Id))
                                .ToList()
                                .ForEach(x => _db.Entry(x).State = EntityState.Deleted);



                if (session.Act == null) session.Act = Array.Empty<ActType>();

                foreach (var act in session.Act)
                {
                    var dbAct = dbSession.Acts.SingleOrDefault(x => x.Id == Guid.Parse(act.act_id));

                    if (dbAct == null)
                    {
                        InsertSessionAct(_db, now, dbSession, act, dbCase);
                    }
                    else
                    {
                        UpdateSessionAct(_db, now, act, dbAct, dbCase);
                    }
                }

                //Delete Session Acts that exist in the DB but are no longer in the request
                DeleteDefuncSessionActs(_db, session, dbSession);
            }

            void DeleteDefuncSessions(Transfer transfer, AistnContextLoggable _db, Case dbCase)
            {
                foreach (var dbSession in dbCase.Sessions.Where(dbSS => !transfer.Case.Session
                                                                                                .Select(s => Guid.Parse(s.session_id))
                                                                                                .Contains(dbSS.Id))
                                                            .ToList())
                {
                    dbSession.Judges.ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);

                    dbSession.Acts.ToList().ForEach(act =>
                    {
                        act.Appeals.ToList().ForEach(appeal =>
                        {
                            appeal.Sides.Where(x => x.Person is not null)
                                        .Select(x => x.Person)
                                        .ToList()
                                        .ForEach(x => _db.Entry(x!).State = EntityState.Deleted);

                            appeal.Sides.Where(x => x.Entity is not null)
                                        .Select(x => x.Entity)
                                        .ToList()
                                        .ForEach(x => _db.Entry(x!).State = EntityState.Deleted);

                            appeal.Sides.ToList().ForEach(x => _db.Entry(x!).State = EntityState.Deleted);
                            appeal.OutgoingDocuments.ToList().ForEach(x => _db.Entry(x!).State = EntityState.Deleted);

                            _db.Entry(appeal!).State = EntityState.Deleted;
                        });
                        _db.Entry(act).State = EntityState.Deleted;
                    });

                    _db.Entry(dbSession).State = EntityState.Deleted;
                }
            }


            void InsertSessionAct(AistnContextLoggable _db, DateTime now, Session session, ActType act, Case newCase)
            {
                switch (act.act_action)
                {
                    case (int)eAction.UPDATE_13002:
                        _logger.LogWarning($"Act ({act.act_id}) Action is Update, but Act does not exist in the DB. Threating it as Insert");
                        goto case (int)eAction.INSERT_13001;

                    case (int)eAction.INSERT_13001:

                        //Check if this entity already exists but for another case
                        if (_db.Acts.Any(x => x.Id == Guid.Parse(act.act_id)))
                            throw new BusinessException($"Act ({act.act_id}) already exists for different case");

                        var newAct = new Act()
                        {
                            Id = Guid.Parse(act.act_id),
                            ActKindId = nomActKinds.SingleOrDefault(x => x.Code == act.act_kind)?.Id ?? throw new BusinessException($"Unknown act kind ({act.act_kind})"),
                            Number = act.act_no.ToString(),
                            ActReason = act.act_reason == null ? null : _db.NomActReasons.SingleOrDefault(x => x.Code == act.act_reason[0]),// ?? throw new BusinessException($"Unknown act reason ({act.act_reason[0]})"),
                            Date = act.act_date,
                            DateCreated = now,
                            Text = act.act_text
                        };

                        if (act.Appeal == null) act.Appeal = Array.Empty<AppealType>();

                        foreach (var appeal in act.Appeal)
                        {
                            InsertActAppeal(_db, now, newAct, appeal, newCase);
                        }

                        if (_isExtendedTransfer)
                        {
                            if (act.act_image_ind)
                            {
                                newAct.RedactedLetterImage = act.act_letter_image_;
                            }
                            else
                            {
                                newAct.OriginalLetterImage = act.act_letter_image_;
                            }
                        }

                        session.Acts.Add(newAct);
                        break;

                    case (int)eAction.DELETE_13003:
                    case (int)eAction.IGNORE_13004:
                        throw new EntityNotFoundException<string>(typeof(Act), act.act_id);

                    default: throw new BusinessException("Unknown action");
                }
            }

            void UpdateSessionAct(AistnContextLoggable _db, DateTime now, ActType act, Act dbAct, Case dbCase)
            {
                switch (act.act_action)
                {
                    case (int)eAction.INSERT_13001:
                        _logger.LogWarning($"Act ({act.act_id}) Action is Insert, but Act does exist in the DB. Threating it as Update");
                        goto case (int)eAction.UPDATE_13002;

                    case (int)eAction.UPDATE_13002:
                        dbAct.ActKindId = nomActKinds.SingleOrDefault(x => x.Code == act.act_kind)?.Id ?? throw new BusinessException($"Unknown act kind ({act.act_kind})");
                        dbAct.Number = act.act_no.ToString();
                        dbAct.ActReasonId = act.act_reason == null ? null : _db.NomActReasons.SingleOrDefault(x => x.Code == act.act_reason[0])?.Id;// ?? throw new BusinessException($"Unknown act reason ({act.act_reason[0]})");
                        dbAct.Date = act.act_date;
                        dbAct.DateModified = now;
                        dbAct.Text = act.act_text;

                        if (act.Appeal == null) act.Appeal = Array.Empty<AppealType>();

                        foreach (var appeal in act.Appeal)
                        {
                            var dbAppeal = dbAct!.Appeals.SingleOrDefault(x => x.Id == Guid.Parse(appeal.appeal_id));

                            if (dbAppeal == null)
                            {
                                InsertActAppeal(_db, now, dbAct, appeal, dbCase);
                            }
                            else
                            {
                                UpdateActAppeal(_db, now, dbCase, appeal, dbAppeal);
                            }
                        }

                        DeleteDefuncActAppeal(_db, act, dbAct);

                        if (_isExtendedTransfer)
                        {
                            if (act.act_image_ind)
                            {
                                dbAct.RedactedLetterImage = act.act_letter_image_;
                            }
                            else
                            {
                                dbAct.OriginalLetterImage = act.act_letter_image_;
                            }
                        }
                        break;

                    case (int)eAction.DELETE_13003:
                        dbAct.Appeals.SelectMany(x => x.Sides).Where(x => x.Person != null).Select(x => x.Person!).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                        dbAct.Appeals.SelectMany(x => x.Sides).Where(x => x.Entity != null).Select(x => x.Entity!).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                        dbAct.Appeals.SelectMany(x => x.Sides).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                        dbAct.Appeals.SelectMany(x => x.OutgoingDocuments).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                        dbAct.Appeals.ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                        _db.Entry(dbAct).State = EntityState.Deleted;
                        break;

                    case (int)eAction.IGNORE_13004:
                        //Do nothing on Ignore...
                        break;

                    default: throw new BusinessException("Unknown action");
                }


            }

            void DeleteDefuncSessionActs(AistnContextLoggable _db, SessionType session, Session dbSession)
            {
                dbSession.Acts.Where(dbSA => !session.Act
                                                                            .Select(a => Guid.Parse(a.act_id))
                                                                            .Contains(dbSA.Id))
                                                .ToList()
                                                .ForEach(x =>
                                                {
                                                    x.Appeals.ToList().ForEach(a =>
                                                    {
                                                        a.Sides.Where(x => x.Person is not null)
                                                                    .Select(x => x.Person)
                                                                    .ToList()
                                                                    .ForEach(x => _db.Entry(x!).State = EntityState.Deleted);

                                                        a.Sides.Where(x => x.Entity is not null)
                                                                    .Select(x => x.Entity)
                                                                    .ToList()
                                                                    .ForEach(x => _db.Entry(x!).State = EntityState.Deleted);

                                                        a.Sides.ToList().ForEach(x => _db.Entry(x!).State = EntityState.Deleted);
                                                        a.OutgoingDocuments.ToList().ForEach(x => _db.Entry(x!).State = EntityState.Deleted);
                                                        _db.Entry(a!).State = EntityState.Deleted;
                                                    });


                                                    _db.Entry(x).State = EntityState.Deleted;

                                                });
            }


            void InsertActAppeal(AistnContextLoggable _db, DateTime now, Act act, AppealType appeal, Case dbCase)
            {
                switch (appeal.appeal_action)
                {
                    case (int)eAction.UPDATE_13002:
                        _logger.LogWarning($"Act Appeal ({appeal.appeal_id}) Action is Update, but SurrAct Appealound does not exist in the DB. Threating it as Insert");
                        goto case (int)eAction.INSERT_13001;

                    case (int)eAction.INSERT_13001:

                        //Check if this entity already exists but for another case
                        if (_db.Appeals.Any(x => x.Id == Guid.Parse(appeal.appeal_id)))
                            throw new BusinessException($"Appeal ({appeal.appeal_id}) already exists for different case");

                        var newAppeal = (new Appeal
                        {
                            Id = Guid.Parse(appeal.appeal_id),
                            AppealKindId = nomAppealKinds.SingleOrDefault(x => x.Code == appeal.appeal_kind)?.Id ?? throw new BusinessException($"Unknown appeal kind ({appeal.appeal_kind})"),
                            Number = appeal.appeal_no.ToString(),
                            Year = parseYear(appeal.appeal_year),
                            Date = appeal.appeal_date,
                            DateCreated = now
                        });

                        //Appeal Sides
                        if (appeal.Side == null) appeal.Side = Array.Empty<SideType>();

                        foreach (var side in appeal.Side)
                        {
                            InsertSide<Appeal>(newAppeal, _db, now, dbCase, side);
                        }

                        //Appeal Outgoing documents
                        if (appeal.SendTo == null) appeal.SendTo = Array.Empty<SendToType>();

                        foreach (var sendTo in appeal.SendTo)
                        {
                            InsertAppealOutgoingDocument(_db, now, newAppeal, sendTo);
                        }

                        act.Appeals.Add(newAppeal);
                        break;

                    case (int)eAction.DELETE_13003:
                    case (int)eAction.IGNORE_13004:
                        throw new EntityNotFoundException<string>(typeof(Appeal), appeal.appeal_id);
                    default: throw new BusinessException("Unknown action");
                }

            }

            void UpdateActAppeal(AistnContextLoggable _db, DateTime now, Case dbCase, AppealType appeal, Appeal dbAppeal)
            {
                switch (appeal.appeal_action)
                {
                    case (int)eAction.INSERT_13001:
                        _logger.LogWarning($"Appeal({appeal.appeal_id}) Action is Insert, but Appeal does exist in the DB. Threating it as Update");
                        goto case (int)eAction.UPDATE_13002;

                    case (int)eAction.UPDATE_13002:
                        dbAppeal.AppealKindId = nomAppealKinds.SingleOrDefault(x => x.Code == appeal.appeal_kind)?.Id ?? throw new BusinessException($"Unknown appeal kind ({appeal.appeal_kind})");
                        dbAppeal.Number = appeal.appeal_no.ToString();
                        dbAppeal.Year = parseYear(appeal.appeal_year);
                        dbAppeal.Date = appeal.appeal_date;
                        dbAppeal.DateModified = now;

                        //Appeal sides
                        if (appeal.Side == null) appeal.Side = Array.Empty<SideType>();

                        foreach (var side in appeal.Side)
                        {
                            var dbSide = dbAppeal!.Sides.SingleOrDefault(x => x.Id == Guid.Parse(side.side_id));

                            if (dbSide == null)
                            {
                                InsertSide<Appeal>(dbAppeal, _db, now, dbCase, side);
                            }
                            else
                            {
                                UpdateSide(_db, now, side, dbSide);
                            }
                        }

                        DeleteDefuncAppealSides(_db, appeal, dbAppeal);

                        //Appeal Outgoing documents
                        if (appeal.SendTo == null) appeal.SendTo = Array.Empty<SendToType>();

                        foreach (var sendTo in appeal.SendTo)
                        {
                            var dbOutDoc = dbAppeal!.OutgoingDocuments.SingleOrDefault(x => x.Id == Guid.Parse(sendTo.sendto_id));

                            if (dbOutDoc == null)
                            {
                                InsertAppealOutgoingDocument(_db, now, dbAppeal, sendTo);
                            }
                            else
                            {
                                UpdateAppealOutgoingDocument(_db, now, sendTo, dbOutDoc);
                            }
                        }

                        DeleteDefuncAppealOutgoingDocument(_db, appeal, dbAppeal);
                        break;

                    case (int)eAction.DELETE_13003:
                        dbAppeal.Sides.Where(x => x.Person != null).Select(x => x.Person!).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                        dbAppeal.Sides.Where(x => x.Entity != null).Select(x => x.Entity!).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                        dbAppeal.Sides.ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                        dbAppeal.OutgoingDocuments.ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                        _db.Entry(dbAppeal).State = EntityState.Deleted;
                        break;

                    case (int)eAction.IGNORE_13004:
                        //Do nothing on Ignore...
                        break;

                    default: throw new BusinessException("Unknown action");
                }

            }

            void DeleteDefuncAppealSides(AistnContextLoggable _db, AppealType appeal, Appeal dbAppeal)
            {
                foreach (var dbAppSide in dbAppeal.Sides.Where(dbSS => !appeal.Side
                                                                                    .Select(s => Guid.Parse(s.side_id))
                                                                                    .Contains(dbSS.Id))
                                                            .ToList())
                {
                    if (dbAppSide.Person != null)
                    {
                        _db.Entry(dbAppSide.Person).State = EntityState.Deleted;
                    }

                    if (dbAppSide.Entity != null)
                    {
                        _db.Entry(dbAppSide.Entity).State = EntityState.Deleted;
                    }

                    _db.Entry(dbAppSide).State = EntityState.Deleted;
                }
            }

            void DeleteDefuncActAppeal(AistnContextLoggable _db, ActType act, Act dbAct)
            {
                foreach (var dbApp in dbAct.Appeals.Where(dbApp => !act.Appeal
                                                                                                    .Select(s => Guid.Parse(s.appeal_id))
                                                                                                    .Contains(dbApp.Id))
                                                                            .ToList())
                {
                    dbApp.Sides.Where(x => x.Person != null).Select(x => x.Person!).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                    dbApp.Sides.Where(x => x.Entity != null).Select(x => x.Entity!).ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                    dbApp.Sides.ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                    dbApp.OutgoingDocuments.ToList().ForEach(x => _db.Entry(x).State = EntityState.Deleted);
                    _db.Entry(dbApp).State = EntityState.Deleted;
                }
            }

            void InsertAppealOutgoingDocument(AistnContextLoggable _db, DateTime now, Appeal dbAppeal, SendToType sendTo)
            {
                switch (sendTo.sendto_action)
                {
                    case (int)eAction.UPDATE_13002:
                        _logger.LogWarning($"Outgoing Document ({sendTo.sendto_action}) Action is Update, but Outgoing Document does not exist in the DB. Threating it as Insert");
                        goto case (int)eAction.INSERT_13001;

                    case (int)eAction.INSERT_13001:
                        //Check if this entity already exists but for another case
                        if (_db.OutgoingDocuments.Any(x => x.Id == Guid.Parse(sendTo.sendto_id)))
                            throw new BusinessException($"Outgoing Document ({sendTo.sendto_id}) already exists for different case");

                        dbAppeal.OutgoingDocuments.Add(new OutgoingDocument
                        {
                            Id = Guid.Parse(sendTo.sendto_id),
                            SendKindId = nomSendToKinds.SingleOrDefault(x => x.Code == sendTo.sendto_kind)?.Id ?? throw new BusinessException($"Unknown send to kind ({sendTo.sendto_kind})"),
                            CourtId = nomCourts.SingleOrDefault(x => x.Number == sendTo.sendto_court)?.Id ?? throw new BusinessException($"Unknown court ({sendTo.sendto_court})"),
                            Number = sendTo.sendto_no.ToString(),
                            Year = parseYear(sendTo.sendto_year),
                            Date = sendTo.sendto_date,
                            ReturnResult1 = sendTo.@return?.return_result,
                            ReturnResult2 = sendTo.@return?.return_result2,
                            DateCreated = now
                        });
                        break;

                    case (int)eAction.DELETE_13003:
                    case (int)eAction.IGNORE_13004:
                        throw new EntityNotFoundException<string>(typeof(Act), sendTo.sendto_id);

                    default: throw new BusinessException("Unknown action");
                }
            }

            void UpdateAppealOutgoingDocument(AistnContextLoggable _db, DateTime now, SendToType sendTo, OutgoingDocument dbOutDoc)
            {
                switch (sendTo.sendto_action)
                {
                    case (int)eAction.INSERT_13001:
                        _logger.LogWarning($"Outgoing Document({sendTo.sendto_id}) Action is Insert, but Outgoing Document does exist in the DB. Threating it as Update");
                        goto case (int)eAction.UPDATE_13002;

                    case (int)eAction.UPDATE_13002:
                        dbOutDoc.SendKindId = nomSendToKinds.SingleOrDefault(x => x.Code == sendTo.sendto_kind)?.Id ?? throw new BusinessException($"Unknown send to kind ({sendTo.sendto_kind})");
                        dbOutDoc.CourtId = nomCourts.SingleOrDefault(x => x.Number == sendTo.sendto_court)?.Id ?? throw new BusinessException($"Unknown court ({sendTo.sendto_court})");
                        dbOutDoc.Number = sendTo.sendto_no.ToString();
                        dbOutDoc.Year = parseYear(sendTo.sendto_year);
                        dbOutDoc.Date = sendTo.sendto_date;
                        dbOutDoc.ReturnResult1 = sendTo.@return?.return_result;
                        dbOutDoc.ReturnResult2 = sendTo.@return?.return_result2;
                        dbOutDoc.DateModified = now;
                        break;

                    case (int)eAction.DELETE_13003:
                        _db.Entry(dbOutDoc).State = EntityState.Deleted;
                        break;

                    case (int)eAction.IGNORE_13004:
                        //Do nothing on Ignore...
                        break;

                    default: throw new BusinessException("Unknown action");
                }
            }

            void DeleteDefuncAppealOutgoingDocument(AistnContextLoggable _db, AppealType appeal, Appeal dbAppeal)
            {
                foreach (var dbOutDoc in dbAppeal.OutgoingDocuments.Where(dbOD => !appeal.SendTo
                                                                                    .Select(s => Guid.Parse(s.sendto_id))
                                                                                    .Contains(dbOD.Id))
                                                            .ToList())
                {
                    _db.Entry(dbOutDoc).State = EntityState.Deleted;
                }
            }


            void InsertSide<T>(T parent, AistnContextLoggable _db, DateTime now, Case dbCase, SideType side)
            {
                Side? newSide = dbCase.GetCaseSide(Guid.Parse(side.side_id));

                if (newSide == null)
                {
                    //Check if this entity already exists but for another case
                    if (_db.Sides.Any(x => x.Id == Guid.Parse(side.side_id)))
                        throw new BusinessException($"Side ({side.side_id}) already exists for different case");

                    newSide = new Side
                    {
                        Id = Guid.Parse(side.side_id),
                        InvolvementKindId = nomInvolvementKinds.SingleOrDefault(x => x.Code == side.side_involvement)?.Id ?? throw new BusinessException($"Unknown side involvment kind ({side.side_involvement})"),
                        DateCreated = now,
                        Case = dbCase,
                        IsDebtor = false, //Put false as default
                    };


                    if (side.Item is SideTypeSide_citizen citizen)
                    {

                        newSide.Person = new Person
                        {
                            Id = Guid.NewGuid(),
                            FirstName = citizen.side_name_1,
                            MiddleName = citizen.side_rename,
                            LastName = citizen.side_family_1,
                            //TODO do something with citizen.side_family_2 ?
                            DateCreated = now,
                        };
                    }
                    else if (side.Item is SideTypeSide_firm firm)
                    {
                        newSide.Entity = new Entity
                        {
                            Id = Guid.NewGuid(),
                            Name = firm.side_name,
                            Bulstat = firm.side_bulstat,
                            DateCreated = now,
                        };
                    }
                    else
                    {
                        throw new BusinessException("Unknown SideType");
                    }
                }

                if (parent is Case _case)
                {
                    _case.Sides.Add(newSide);
                }
                else if (parent is SurroundDocument surround)
                {
                    surround.Sides.Add(newSide);
                }
                else if (parent is Appeal appeal)
                {
                    appeal.Sides.Add(newSide);
                }
            }

            void UpdateSide(AistnContextLoggable _db, DateTime now, SideType side, Side dbSide)
            {
                dbSide.InvolvementKindId = nomInvolvementKinds.SingleOrDefault(x => x.Code == side.side_involvement)?.Id ?? throw new BusinessException($"Unknown side involvment kind ({side.side_involvement})");
                dbSide.DateModified = now;

                if (side.Item is SideTypeSide_citizen citizen)
                {
                    if (dbSide.Person == null)
                    {
                        //Add - Side Person
                        dbSide.Person = new Person
                        {
                            Id = Guid.NewGuid(),
                            FirstName = citizen.side_name_1,
                            MiddleName = citizen.side_rename,
                            LastName = citizen.side_family_1,
                            //TODO do something with citizen.side_family_2 ?
                            DateCreated = now,
                        };
                    }
                    else
                    {
                        //Update - Side Person
                        dbSide.Person.FirstName = citizen.side_name_1;
                        dbSide.Person.MiddleName = citizen.side_rename;
                        dbSide.Person.LastName = citizen.side_family_1;
                        //TODO do something with citizen.side_family_2 ?
                        dbSide.Person.DateModified = now;
                    }

                    //Delete - Side Person if it's now a Person
                    if (dbSide.Entity != null)
                    {
                        _db.Entry(dbSide.Entity).State = EntityState.Deleted;
                    }
                }
                else if (side.Item is SideTypeSide_firm firm)
                {
                    if (dbSide.Entity == null)
                    {
                        //Add - Side Entity
                        dbSide.Entity = new Entity
                        {
                            Id = Guid.NewGuid(),
                            Name = firm.side_name,
                            Bulstat = firm.side_bulstat,
                            DateCreated = now,
                        };
                    }
                    else
                    {
                        //Update - Side Entity
                        dbSide.Entity.Name = firm.side_name;
                        dbSide.Entity.Bulstat = firm.side_bulstat;
                        dbSide.Entity.DateModified = now;
                    }

                    //Delete - Side Person if it's now a Entity
                    if (dbSide.Person != null)
                    {
                        _db.Entry(dbSide.Person).State = EntityState.Deleted;
                    }
                }
                else
                {
                    throw new BusinessException("Unknown SideType");
                }
            }
        }

        protected short parseYear(string year)
        {
            if (year == null)
                throw new BusinessException("Year not provided");
            short yearInt = 0;

            if (!short.TryParse(year, out yearInt))
                throw new BusinessException($"{year} not a valid year");

            if (yearInt < DateTime.Now.AddYears(-100).Year || yearInt > DateTime.Now.AddYears(1).Year)
                throw new BusinessException($"{year} is out of proper range");

            return yearInt;
        }

        protected void validationCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.Write("WARNING: ");
            else if (args.Severity == XmlSeverityType.Error)
                throw new BusinessException("Failed loading XSD schema");

            Console.WriteLine(args.Message);
        }

        private T? deserializeWithBinaryData<T>(XDocument el)
        {
            using (var ms = new MemoryStream())
            {
                el.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var serializer = new XmlSerializer(typeof(T));
                return (T?)serializer.Deserialize(ms);
            }
        }

        protected abstract Task<string> GetXSD();
        protected virtual async Task validateTransferContents(Transfer transfer)
        {
            AistnContextLoggable _db = await _dbFactory.CreateDbContextAsync();

            List<string> errors = new List<string>();

            //Check for case duplication
            if (_db.Cases.Where(x => x.Id != Guid.Parse(transfer.Case.case_id))
                        .Where(x => x.Year.ToString() == transfer.Case.case_year)
                        .Where(x => x.Court.Number == transfer.Case.case_court)
                        .Where(x => x.Number == transfer.Case.case_no.ToString())
                        .Any())
                errors.Add("Case with the same Year/Court/Number but different ID already exists");



            if (errors.Any())
            {
                throw new ValidationErrorsException("Validation errors", errors);
            }

            return;
        }

    }
}