using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AISTN.Repository
{
    public enum eUserActionType
    {
        #region CaseFile

        [Description("Създаване на преписка")]
        CreateCaseFile = 1,
        [Description("Редактиране на преписка")]
        EditCaseFile = 2,
        [Description("Изтриване на преписка")]
        DeleteCaseFile = 3,
        [Description("Игнориране на преписка")]
        IgnoreCaseFile = 4,
        #endregion

        #region User

        [Description("Създаване на потребител")]
        CreateUser = 5,
        [Description("Редактиране на потребител")]
        UpdateUser = 6,
        [Description("Изтриване на потребител")]
        DeleteUser = 7,

        #endregion

        #region Course

        [Description("Създаване на курс")]
        CreateCourse = 8,
        [Description("Редактиране на курс")]
        UpdateCourse = 9,
        [Description("Изтриване на курс")]
        DeleteCourse = 10,

        #endregion

        #region Installment

        [Description("Създаване на вноска")]
        CreateInstallment = 11,
        [Description("Редактиране на вноска")]
        UpdateInstallment = 12,
        [Description("Изтриване на вноска")]
        DeleteInstallment = 13,

        #endregion

        #region Order

        [Description("Създаване на заповед")]
        CreateOrder = 14,
        [Description("Редактиране на заповед")]
        UpdateOrder = 15,
        [Description("Изтриване на заповед")]
        DeleteOrder = 16,

        #endregion

        #region Program

        [Description("Създаване на програма")]
        CreateProgram = 17,
        [Description("Редактиране на програма")]
        UpdateProgram = 18,
        [Description("Изтриване на програма")]
        DeleteProgram = 19,

        #endregion

        #region Syndic

        [Description("Създаване на синдик")]
        CreateSyndic = 20,
        [Description("Редактиране на синдик")]
        UpdateSyndic = 21,
        [Description("Изтриване на синдик")]
        DeleteSyndic = 22,

        #endregion

        #region StatisticalReport

        [Description("Създаване на статистически отчет")]
        CreateStatisticalReport = 23,
        [Description("Редактиране на статистически отчет")]
        UpdateStatisticalReport = 24,
        [Description("Изтриване на статистически отчет")]
        DeleteStatisticalReport = 25,
        [Description("Публикуване на статистически отчет")]
        PublishStatisticalReport = 61,
        [Description("Скриване на статистически отчет")]
        UnpublishStatisticalReport = 62,

        #endregion

        #region Announcement

        [Description("Създаване на обява за продажба")]
        CreateAnnouncement = 26,
        [Description("Създаване на имущество към обява за продажба")]
        CreateObject = 27,
        [Description("Прикачване на файл към обява за продажба")]
        AttachFileToAnnouncement = 28,
        [Description("Редактиране на обява за продажба")]
        UpdateAnnouncement = 29,
        [Description("Редактиране на имущество към обява за продажба")]
        UpdateObject = 30,
        [Description("Изтриване на имущество към обява за продажба")]
        DeleteObject = 31,
        [Description("Публикуване на обява")]
        PublishAnnouncement = 32,
        [Description("Отмяна на обява")]
        CancelAnnouncement = 33,
        [Description("Изпращане на обява за публикуване")]
        SendAnnouncementForPublish = 34,
        [Description("Връщане на обява за корекция")]
        SendAnnouncementForCorrection = 35,
        [Description("Връщане на обява в чернови за редакция")]
        SendAnnouncementForCorrectionInDraft = 36,

        #endregion

        #region Signal

        [Description("Създаване на сигнал")]
        CreateSignal = 37,
        [Description("Редактиране на сигнал")]
        UpdateSignal = 38,
        [Description("Изтриване на сигнал")]
        DeleteSignal = 39,

        #endregion

        #region Appeal

        [Description("Създаване на жалба")]
        CreateAppeal = 40,
        [Description("Редактиране на жалба")]
        UpdateAppeal = 41,
        [Description("Изтриване на жалба")]
        DeleteAppeal = 42,

        #endregion

        [Description("Редактиране на данни за документ")]
        UpdateDocumentContent = 43,

        #region Inspection
        [Description("Създаване на инспекция")]
        CreateInspection = 44,
        [Description("Редактиране на инспекция")]
        UpdateInspection = 45,

        #endregion

        #region Plea
        [Description("Създаване на молба")]
        CreatePlea = 46,
        [Description("Редактиране на молба")]
        UpdatePlea = 47,

        #endregion

        #region CourseApplication
        [Description("Създаване на заявка за курс")]
        CreateCourseApplication = 48,
        #endregion

        #region SyndicTemplate
        [Description("Създаване на образец от синдик")]
        CreateSyndicTemplate = 49,
        [Description("Редактиране на образец от синдик")]
        UpdateSyndicTemplate = 50,
        #endregion

        #region SyndicReport
        [Description("Създаване на отчет от синдик")]
        CreateSyndicReport = 51,
        [Description("Редактиране на отчет от синдик")]
        UpdateSyndicReport = 52,
        #endregion

        #region Property
        [Description("Създаване на маса по несъстоятелност")]
        CreateProperty = 53,
        [Description("Редактиране на маса по несъстоятелност")]
        UpdateProperty = 54,
        [Description("Изтриване на маса по несъстоятелност")]
        DeleteProperty = 55,
        #endregion

        #region Activity
        [Description("Създаване на дневник")]
        CreateActivity = 56,
        [Description("Редактиране на дневник")]
        UpdateActivity = 57,
        [Description("Изтриване на дневник")]
        DeleteActivity = 58,
        #endregion


        [Description("Създаване на статистически данни за предприятие")]
        CreateEntityStatisticalData = 59,
        [Description("Редактиране на статистически данни за предприятие")]
        UpdateEntityStatisticalData = 60,
    }
}
