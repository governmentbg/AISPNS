// Sidebar
import sideBar from "./shared/sidebar.vue";

/**
 *  FILTERS
 *  
 * */
// cases
import CasesListFilter from "./filters/cases/casesListFilter.vue";
// syndic

//sell announcements
import SellAnnouncementsFilter from "./filters/sellAnnouncements/sellAnnouncementsFilter.vue";

// activities and property
import ActivitiesAndPropertyFilter from "./filters/activitiesAndProperties/activitiesAndPropertyFilter.vue";

// reports
import ReportsFilter from "./filters/reports/reportsFilter.vue";

/**
 * MODALS
 */

// syndic
import PaymentFileUploadModal from "./modals/syndics/paymentFileUploadModal.vue";
import PaymentDocumentModal from "./modals/syndics/paymentDocumentModal.vue";

//activities and properties
import JournalActionModal from "./modals/activitiesAndProperties/journalActionModal.vue";
import PropertyModal from "./modals/activitiesAndProperties/propertyModal.vue";
import PropertyFileUploadModal from "./modals/activitiesAndProperties/propertyFileUploadModal.vue"
import ActionFileUploadModal from "./modals/activitiesAndProperties/actionFileUploadModal.vue";

// messages
import MessageFileUploadModal from "./modals/messages/messageFileUploadModal.vue";

// FileUpload
import FileUploadModal from "./modals/fileUpload/fileUploadModal.vue";

// trainings
import TrainingModal from "./modals/trainings/trainingModal.vue";
import CourseApplicationModal from "./modals/trainings/courseApplicationModal.vue";

// sell announcement object modal
import SellAnnouncementObjectModal from "./modals/sellAnnouncements/sellAnnouncementObjectModal.vue";
import SellAnnouncementUploadFileModal from "./modals/sellAnnouncements/sellAnnouncementFileUpload.vue";
import SellAnnouncementSendForCorrectionModal from "./modals/sellAnnouncements/sellAnnouncementSendForCorrectionModal.vue";

// Syndic template
import TemplateUploadFileModal from "./modals/templates/templateUploadFileModal.vue";

// Syndic reports
import ReportUploadFileModal from "./modals/reports/reportUploadFileModal.vue";

// cases
import CasesListSelectionModal from "./modals/cases/casesListSelectionModal.vue";

import ReportTemplateModal from "./modals/reports/reportTemplateModal.vue";

import EntityModal from "./modals/entities/entityModal.vue";
import PersonModal from "./modals/persons/personModal.vue";

// Print

// Other

export {
  sideBar,
  
  // filters
  CasesListFilter,
  SellAnnouncementsFilter,
  ActivitiesAndPropertyFilter,
  ReportsFilter,

  // modals
  PaymentFileUploadModal,
  PaymentDocumentModal,
  JournalActionModal,
  PropertyModal,
  PropertyFileUploadModal,
  ActionFileUploadModal,
  MessageFileUploadModal,
  FileUploadModal,
  TrainingModal,
  CourseApplicationModal,
  SellAnnouncementObjectModal,
  SellAnnouncementUploadFileModal,
  SellAnnouncementSendForCorrectionModal,
  TemplateUploadFileModal,
  ReportUploadFileModal,
  CasesListSelectionModal,
  ReportTemplateModal,
  EntityModal,
  PersonModal

  // print
};
