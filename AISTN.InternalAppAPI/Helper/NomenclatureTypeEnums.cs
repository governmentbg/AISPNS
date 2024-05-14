using AISTN.InternalAppAPI.Models.Save;

namespace AISTN.InternalAppAPI.Helper
{
    public enum NomenclatureEnums
    {
        #region Bankruptcy

        Action = 1,
        ActKind = 2,
        ActReason = 3,
        AttachedDocumentKind = 4,
        CaseCode = 5,
        CaseKind = 6,
        CourtNumber = 7,
        IncomingDocumentKind = 8,
        InvolvementKind = 9,
        ReportSource = 10,
        ReportType = 11,
        SendToKind = 12,
        SessionKind = 13,
        SessionResult = 14,
        DebtorStatus = 15,
        AppellateCourt = 16,
        DirectiveTemplate = 17,
        AppealKind = 18,
        //ActCategory = 19,
        //ActContractor = 20,
        ActAnnouncementCategory = 21,
        RegistrationLegalBasis = 22,
        SyndicKind = 23,
        RegisterField = 24,
        RegisterSyndicKind = 25,

        #endregion

        #region Syndic

        OrderKind = 40,
        Specialty = 41,
        TemplateKind = 42,
        SyndicStatus = 43,
        InstallmentYear = 44,
        SignalSenderType = 45,
        SignalDocumentKind = 46,
        InspectionType = 47,
        ObjectKind = 48,
        ObjectType = 49,
        SyndicCaseReport = 50,
        CourseKind = 51,
        OrderPaymentKind = 52,
        // Note: не се използва в момента, но може да потрябва в бъдеще. Всички методи свързани с него са закоментирани и ДТО-то е изтрито
        //SyndicAction = 53,
        ActivityKind = 54,
        SampleKind = 55,
        SyndicReportType = 56,
        CompanySize = 57,

        #endregion

        #region SellAnnouncement

        LegalBasis = 80,
        OfferingLocationType = 81,
        OfferingProcedure = 82,
        OfferingKind = 83,
        PapersForSale = 84,
        SalesProcedure = 85,
        AnnouncementStatus = 86,
        SellAnnouncementTemplate = 87,

        #endregion
    }

    public class NomenclatureTypeBG
    {
        public static List<NomenclatureTypeDTO> BgNames = new List<NomenclatureTypeDTO>
        {
            #region Bankruptcy
            new NomenclatureTypeDTO() {ID = 1, Name = "Вид действие", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 2, Name = "Вид на съдебни актове", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 3, Name = "Основание по ТЗ", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 4, Name = "Вид прикачен документ", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 5, Name = "Шифър на дело", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 6, Name = "Вид дело", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 7, Name = "Единен информационен код на съдилищата", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 8, Name = "Вид на входящия документ", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 9, Name = "Участие/Страна", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 10, Name = "Източници на справки и статистики", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 11, Name = "Типове справки и статистики", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 12, Name = "Вид на изходящ документ", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 13, Name = "Вид на заседание", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 14, Name = "Резултат от заседание", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 15, Name = "Статус на длъжника", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 16, Name = "Съдилища за обжалване по функционална подсъдност", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 17, Name = "Образци по чл. 28 от Директивата", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 18, Name = "Вид на входящ документ за обжалване", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            //new NomenclatureTypeDTO() {ID = 19, Name = "Категория на актове", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            //new NomenclatureTypeDTO() {ID = 20, Name = "Вписване на данни на предприемачи", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 21, Name = "Категория на обявени актове", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 22, Name = "Правно основание при вписване на данни", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 23, Name = "Вид на синдик", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 24, Name = "Вписване на данни на преприемачи", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            new NomenclatureTypeDTO() {ID = 25, Name = "Вид на синдик за вписване", NomenclatureTypeID = (int)NomenclatureTypeEnums.Bankruptcy},
            #endregion

            #region Syndic
            new NomenclatureTypeDTO() {ID = 40, Name = "Вид заповеди", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 41, Name = "Специалност на синдик", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 42, Name = "Образци на синдици", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 43, Name = "Статус на синдик", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 44, Name = "Година на вноска", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 45, Name = "Вид подател на сигнал", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 46, Name = "Вид на документ към сигнал", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 47, Name = "Тип на инспекция", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 48, Name = "Вид вещ", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 49, Name = "Тип вещ", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 50, Name = "Отчети на синдици към дело", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 51, Name = "Вид на обучение", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 52, Name = "Вид начин на плащане", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            //new NomenclatureTypeDTO() {ID = 53, Name = "Вид действие на синдик", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 54, Name = "Вид дейност", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 55, Name = "Вид пример", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 56, Name = "Вид доклад на синдик", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            new NomenclatureTypeDTO() {ID = 57, Name = "Размер на длъжници", NomenclatureTypeID = (int)NomenclatureTypeEnums.Syndic},
            #endregion

            #region SellAnnouncement
            new NomenclatureTypeDTO() {ID = 80, Name = "Правно основание", NomenclatureTypeID = (int)NomenclatureTypeEnums.SellAnnouncement},
            new NomenclatureTypeDTO() {ID = 81, Name = "Място за продажба", NomenclatureTypeID = (int)NomenclatureTypeEnums.SellAnnouncement},
            new NomenclatureTypeDTO() {ID = 82, Name = "Ред на продажбата", NomenclatureTypeID = (int)NomenclatureTypeEnums.SellAnnouncement},
            new NomenclatureTypeDTO() {ID = 83, Name = "Начин на продажбата", NomenclatureTypeID = (int)NomenclatureTypeEnums.SellAnnouncement},
            new NomenclatureTypeDTO() {ID = 84, Name = "Книжа за продажбата", NomenclatureTypeID = (int)NomenclatureTypeEnums.SellAnnouncement},
            new NomenclatureTypeDTO() {ID = 85, Name = "Описание на процеса на продажба", NomenclatureTypeID = (int)NomenclatureTypeEnums.SellAnnouncement},
            new NomenclatureTypeDTO() {ID = 86, Name = "Статус на обявата", NomenclatureTypeID = (int)NomenclatureTypeEnums.SellAnnouncement},
            new NomenclatureTypeDTO() {ID = 87, Name = "Темплейти на продажби", NomenclatureTypeID = (int)NomenclatureTypeEnums.SellAnnouncement}
            #endregion
        };
        public enum NomenclatureTypeEnums
        {
            Bankruptcy = 1,
            Syndic = 2,
            SellAnnouncement = 3
        }
    }
}
