using AISTN.Common.Models;
using AISTN.InternalAppAPI.Models.Index;

namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveCaseDTO
    {
        public Guid? Id { get; set; }

        public int Year { get; set; }

        public NomCourtDTO Court { get; set; }

        public SideIndexDTO Debtor { get; set; }
    }
}
