namespace AISTN.ExternalAppAPI.Models.ePayment
{
    public class ElectronicServiceRecipientDTO
    {
        public int ServiceRecipientTypeId { get; set; }

        public string CitizenUin { get; set; }

        public string FirstName { get; set; }

        public string FirstNameLatin { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string LastNameLatin { get; set; }

        public string OtherName { get; set; }

        public string OtherNameLatin { get; set; }

        public string Pseudonim { get; set; }

        public string PseudonimLatin { get; set; }

        public string SettlementCountryName { get; set; }

        public string SettlementCountryCode { get; set; }

        public string BirthCountryName { get; set; }

        public string BirthCountryCode { get; set; }

        public string BirthSettlement { get; set; }

        public DateTime? BirthDate { get; set; }

        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }

        public string LegalEntityName { get; set; }

        public string LegalEntityIdentifier { get; set; }

        public string LegalEntityRegisterName { get; set; }

        public string LegalEntityOtherData { get; set; }

        public string LegalEntityCountryName { get; set; }

        public string LegalEntityCountryCode { get; set; }

        public ElectronicServiceRecipientDTO()
        {
        }

        //public ElectronicServiceRecipientDTO(DocCorrespondent dc)
        //{
        //    var c = dc.Correspondent;
        //    var cc = dc.CorrespondentContact;

        //    if (c.CorrespondentType == CorrespondentType.BulgarianCitizen)
        //    {
        //        ServiceRecipientTypeId = (int)CorrespondentType.BulgarianCitizen;
        //        FirstName = c.FirstName;
        //        MiddleName = c.MiddleName;
        //        LastName = c.LastName;
        //        CitizenUin = c.Uin;
        //    }
        //    else if (c.CorrespondentType == CorrespondentType.Foreigner)
        //    {
        //        ServiceRecipientTypeId = (int)CorrespondentType.Foreigner;
        //        FirstName = c.FirstName;
        //        LastName = c.LastName;
        //        SettlementCountryCode = (cc != null && cc.Country != null) ? cc.Country.Code : null;
        //        SettlementCountryName = (cc != null && cc.Country != null) ? cc.Country.Name : null;
        //        BirthSettlement = cc?.ForeignerSettlement;
        //        BirthDate = c.BirthDate;
        //    }
        //    else if (c.CorrespondentType == CorrespondentType.LegalEntity)
        //    {
        //        ServiceRecipientTypeId = (int)CorrespondentType.LegalEntity;
        //        LegalEntityName = c.Name;
        //        LegalEntityIdentifier = c.Uin;
        //    }
        //    else if (c.CorrespondentType == CorrespondentType.ForeignLegalEntity)
        //    {
        //        ServiceRecipientTypeId = (int)CorrespondentType.ForeignLegalEntity;
        //        LegalEntityName = c.Name;
        //        LegalEntityIdentifier = c.Uin;
        //        LegalEntityRegisterName = c.LegalEntityRegisterName;
        //        LegalEntityOtherData = c.LegalEntityOtherData;
        //        LegalEntityCountryCode = (cc != null && cc.Country != null) ? cc.Country.Code : null;
        //        LegalEntityCountryName = (cc != null && cc.Country != null) ? cc.Country.Name : null;
        //    }
        //}
    }
}