namespace AISTN.Common.Helper
{
    /// <summary>
    /// This struct is used to define to which table (parent) the document will be uploaded to
    /// </summary>
    public struct AttachedDocumentParent
    {
        public static Guid Installment = new Guid("FBD0FF9F-0AF2-4653-AA15-A88BFEF4B915");
        public static Guid Order = new Guid("c5c6a1a8-6209-4171-b160-0d16041d44e1");
        public static Guid Syndic = new Guid("ceaa2be9-94b7-4fcf-9a9f-0efb9608caa9");
        public static Guid Template = new Guid("1cd6e880-cbb4-4943-922b-e2e4f88d4b12");
        public static Guid StatisticalReport = new Guid("14796653-fd75-4f14-a8bf-21c45f42894b");
        public static Guid Message = new Guid("3f5618ea-c50a-48f9-a6f4-ae96849a6be1");
        public static Guid Announcement = new Guid("024973F4-17BE-4B10-8FF3-850C5AE5FE82");
        public static Guid Object = new Guid("BAC0CF2F-2E53-4A6E-AB1B-14591C07D6A7");
        public static Guid Signal = new Guid("2a8fe6ae-96b5-4908-900f-b4b9cf17b02b");
        public static Guid TemplateArticle28 = new Guid("7D26F968-17A3-4BD7-95C7-3CCB80CF0633");
        public static Guid Inspection = new Guid("C848DD37-31B1-48F5-BCFF-114CB1DC53D9");
        public static Guid Course = new Guid("C0EE381D-B639-4DE8-BC41-4707B363FBEA");
        public static Guid SyndicPlea = new Guid("9FD4423D-2621-4EA5-8062-C9F0D9FA2CF3");
        public static Guid AdminTemplate = new Guid("fd1550ee-87f7-4ec3-b7dd-85e49724bc30");
        public static Guid ReportTemplate = new Guid("71673dfd-0fe7-4d2d-85ae-8f5fe1d7d9d3");
        public static Guid SyndicTemplate = new Guid("354e4098-a500-408f-984b-b08cdae20d44");
        public static Guid SyndicReportTemplate = new Guid("1cbad5ed-5b9f-4f8f-89b7-66904e69e6c6");
        public static Guid Activity = new Guid("06F95A0A-A29C-4306-B68A-573AD45FFF78");
        public static Guid Property = new Guid("311d646e-50c4-4ec6-8a6c-69d08f358eb4");
        public static Guid TemplateDocument = new Guid("e89276ac-3a22-48c0-b438-a72ef60a61ea");
        public static Guid DocumentLegalBasis = new Guid("3794E9AB-F18B-438F-8EBA-398AFD366388");
        public static Guid ActAnnouncement = new Guid("27065C4B-E821-4ED1-A881-E10EF84F615D");
        public static Guid RegisterEntry = new Guid("DD879FC2-3F33-42C4-B81F-DD27F966AC30");
    }

    public struct DocumentCollectionType
    {
        public static Guid WebsiteDocument = new Guid("78b01353-0cfe-43df-81a0-f4e50c92934e");
    }

    public struct RoleNames
    {
        public const string Administrator = "Administrator";
        public const string Inspector = "Inspector";
        public const string Employee = "Employee";
        public const string MEIEmployee = "MEIEmployee";
        public const string Syndic = "Syndic";
    }

    public struct EmailReason
    {
        public const string PublishedProgram = "Нова публикувана програма";
        public const string EnrolledSyndic = "Успешно класиране";
        public const string UnenrolledSyndic = "Неуспешно класиране";
        public const string InstallmentExpiring = "Изтичащ срок на вноска";
        public const string NewProfileMessage = "Имате ново съобщение в профила си";
    }

    public struct PropertyClassKindIds
    {
        public static Guid Things = new Guid("865BB922-8913-486B-94DC-D1FB42AF68C3");
        public static Guid Patents = new Guid("5D6334A3-02E7-4968-AD39-F89B64A025F7");
        public static Guid Shares = new Guid("C5BABCD7-B5B2-4FA5-8D7E-05FE2340CFD2");
        public static Guid Receivables = new Guid("8830DC5B-1A9B-4F47-9844-0039D4EE4C2D");
    }

    public enum PropertyClassKind
    {
        Things = 1,
        Patents = 2,
        Shares = 3,
        Receivables = 4
    }

    public enum TemplateDocumentType
    {
        Inspection = 1,
        Certificate = 2,
        Journal = 3,
        Bankruptcy = 4
    }

    public struct SyndicStatus
    {
        public static Guid TemporarySuspended = new Guid("85F64A73-FB60-4055-95EA-21FA028B0F1A");
        public static Guid Inactive = new Guid("849C2182-541A-4F4C-9F95-3312B5A08558");
        public static Guid ConfirmedByOrder = new Guid("CCDBF309-DB40-4B69-953C-570C9C4EC2E1");
        public static Guid Unlisted = new Guid("57D0216B-33B2-4721-BBE3-5F5BAD60111C");
        public static Guid SelfUnlisted = new Guid("C2309D06-6EA0-4721-B183-8658DCED1DA3");
        public static Guid Revalidation = new Guid("625C9EC8-E00C-42E4-A4F1-8954F5E27EF9");
        public static Guid DelistingRevoked = new Guid("327B3B5B-7E2A-4F35-8AE3-DDDC5AE808CC");
    }

    public struct ActAnnouncementStatus
    {
        public static Guid NoSubjectToAnnouncement = new Guid("FABC4947-4CEC-4BC1-8F76-0F5F338CA45B");
        public static Guid Unprocessed = new Guid("A3C76AF3-E415-48C0-B54C-A1C0F411182D");
        public static Guid Processed = new Guid("BF6462DE-8BAF-4D50-9DE3-C466DA230852");
        public static Guid NoSubjectToRegistration = new Guid("42b07fef-e6e1-4e2b-9daf-cfe6d235c2cb");
    }
}
