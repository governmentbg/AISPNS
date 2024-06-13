
export const eUserRoles = {
  ADMINISTRATOR: "96746c7a-ed69-4f04-adfd-8687dd898b1a",      //  Администратор
  DOCUMENTS_FLOW: "1a79edc0-33b3-42e6-a0fe-7e0deba0d82d",     //  Документооборот
  DOCUMENTS_EAU: "4d7f096c-5523-4a5f-893e-3f0077f17148",      //  Административни услуги
  INSPECTIONS_READ: "552ffcfd-5ee9-41a0-a4af-466a146a2ab7",   //  Проверки читател
  INSPECTIONS_MANAGER: "7f5bdd98-1cbc-428e-9abd-35f544baa97f",//  Проверки - всичко
  RSI_READ: "e5454dd2-8b75-44b3-9998-6ce3fbca4201",           //  РСИ Читател
  RSI_MANAGER: "be46005c-3eaa-4876-b987-e37311cb8150",        //  РСИ - всичко
  REPORTS: "49c8809a-fa41-42ec-a533-dd45e0ede71e",            //  Справки
  REPORTS_EXTERNAL: "866cd60b-96ff-4c3d-a4ce-e520ef6738e8",   //  Разширени справки
  UA_READ: "d8117b95-6781-43ad-9833-69e2506f4493",            //  Управление на автопарка - управляващ само своите коли
  UA_MANAGER: "071c4489-45db-4a1d-8970-b8d364ac88ee",         //  Управление на автопарка - всичко
  UP_MANAGER: "3e1df31d-66a3-42ce-88c7-7766fd550888",         //  Управление на персонала Мениджър
  UP_BUSINESS_TRIPS: "cec67b4a-9891-4fff-a112-9bbb897e4a06",  //  Управление на персонала само Командировки
  UP_VACATIONS: "36c4c8dd-515a-4551-8182-aa0f91093025",       //  Управление на персонала само Отпуски
  UP_EXPENSES: "9f9b5e1c-a87d-43da-84dd-b7d91b05fbf6"         //  Управление на персонала само Разходи
}


export const eDocumentContentTypes = {
  INSTALLMENT: 'FBD0FF9F-0AF2-4653-AA15-A88BFEF4B915',
  ORDER: 'c5c6a1a8-6209-4171-b160-0d16041d44e1',
  SYNDIC: 'ceaa2be9-94b7-4fcf-9a9f-0efb9608caa9',
  TEMPLATE: '1cd6e880-cbb4-4943-922b-e2e4f88d4b12',
  STATISTICAL_REPORT: '14796653-fd75-4f14-a8bf-21c45f42894b',
  MESSAGE: '3f5618ea-c50a-48f9-a6f4-ae96849a6be1',
  ANNOUNCEMENT: '024973F4-17BE-4B10-8FF3-850C5AE5FE82',
  ANNOUNCEMENT_OBJECT: 'BAC0CF2F-2E53-4A6E-AB1B-14591C07D6A7',
  SIGNAL: '2a8fe6ae-96b5-4908-900f-b4b9cf17b02b',
  WEBSITE_DOCUMENT: '78b01353-0cfe-43df-81a0-f4e50c92934e',
  TEMPLATE_DIRECTIVE28: '7D26F968-17A3-4BD7-95C7-3CCB80CF0633',
  TEMPLATE_SYNDIC: 'fd1550ee-87f7-4ec3-b7dd-85e49724bc30',
  TEMPLATE_REPORT_SYNDIC: '71673dfd-0fe7-4d2d-85ae-8f5fe1d7d9d3',
  INSPECTION: 'C848DD37-31B1-48F5-BCFF-114CB1DC53D9',
  COURSE: 'C0EE381D-B639-4DE8-BC41-4707B363FBEA',
  SYNDIC_APPEAL: '9FD4423D-2621-4EA5-8062-C9F0D9FA2CF3',
  SYNDIC_TEMPLATE: '354e4098-a500-408f-984b-b08cdae20d44',
  SYNDIC_REPORT_TEMPLATE: '1cbad5ed-5b9f-4f8f-89b7-66904e69e6c6',
  ACTIVITY: '06F95A0A-A29C-4306-B68A-573AD45FFF78',
  PROPERTY: '311d646e-50c4-4ec6-8a6c-69d08f358eb4',
  TEMPLATE_DOCUMENT: 'e89276ac-3a22-48c0-b438-a72ef60a61ea',
  LEGAL_BASIS_DOCUMENT: '3794E9AB-F18B-438F-8EBA-398AFD366388',
  ACT_ANNOUNCEMENT: '27065C4B-E821-4ED1-A881-E10EF84F615D',
  REGISTER_ENTRY: 'DD879FC2-3F33-42C4-B81F-DD27F966AC30'
}


export const eNomenclatureFieldsByType = {
  CODE: [1, 2, 3, 5, 6, 8, 9, 12, 13, 14],
  NAME: [4, 7, 10, 11, 15, 42, 45, 46, 50, 51, 52, 87],
  DESCRIPTION: [1, 2, 3, 5, 6, 8, 9, 12, 13, 14, 17, 18, 19, 20, 40, 41, 43, 47, 48, 49, 53, 80, 81, 82, 83, 84, 85, 11, 42, 86, 87],
  NUMBER: [7, 16]
}

export const eNomenclatureMainField = {
  DESCRIPTION: [1, 2, 3, 5, 6, 8, 9, 12, 13, 14, 17, 18, 19, 20, 40, 41, 43, 47, 48, 49, 53, 80, 81, 82, 83, 84, 85, 11, 42, 86, 87],
  NAME: [4, 7, 10, 11, 15, 42, 45, 46, 50, 51, 52, 87]
}


export const eUserRoleNames = {
  Employee: "Служител МП",
  MEIEmployee: "Служител на МИИ",
  Inspector: "Инспектор",
  Syndic: "Синдик",
  Administrator: "Администратор",
}

export const eUserRoleGUIDS = {
  Employee: "f1d6833e-38e2-4e8e-85d6-4a37c668aaa6",
  MEIEmployee: "cdf589fd-5c2c-45e2-b446-fafe46db91a4",
  Inspector: "89cc9684-f34d-4251-8cf8-f78915d319a5",
  Syndic: "b0aa570b-e3e8-4fbc-8fb5-025c6919c264",
  Administrator: "5f6ed29b-3ef1-4ad4-a478-31912214eaf9",
}

export const UserPermissions = {
  EMPLOYEE: "Employee",
  INSPECTOR: "Inspector",
  ADMIN: "Administrator"
};

export const ePropertyClassKinds = {
  THINGS: 1,
  PATENTS: 2,
  SHARES: 3,
  RECEIVABLES: 4
}

export const ePropertyClass = {
  THINGS: "865BB922-8913-486B-94DC-D1FB42AF68C3",
  PATENTS: "5D6334A3-02E7-4968-AD39-F89B64A025F7",
  SHARES: "C5BABCD7-B5B2-4FA5-8D7E-05FE2340CFD2",
  RECEIVABLES: "8830DC5B-1A9B-4F47-9844-0039D4EE4C2D"
}

export const eTemplateTypes = {
  INSPECTION: "Inspection",
  CERTIFICATE: "Certificate",
  JOURNAL: "Journal",
  BANKRUPTCY: "Bankruptcy"
}

export const eTemplateTypeNames = {
  INSPECTION: "Шаблон за инспекция",
  CERTIFICATE: "Шаблон за удостоверение",
  JOURNAL: "Шаблон за Дневник на синдика",
  BANKRUPTCY: "Шаблон за Маса по несъстоятелност"
}

export const eTemplateDropDownTypes = [
  {label: eTemplateTypeNames.INSPECTION, value: eTemplateTypes.INSPECTION},
  {label: eTemplateTypeNames.CERTIFICATE, value: eTemplateTypes.CERTIFICATE},
  {label: eTemplateTypeNames.JOURNAL, value: eTemplateTypes.JOURNAL},
  {label: eTemplateTypeNames.BANKRUPTCY, value: eTemplateTypes.BANKRUPTCY}
]

export const eActStatuses = {
  NOT_PROCESSED: "a3c76af3-e415-48c0-b54c-a1c0f411182d",
  PROCESSED: "bf6462de-8baf-4d50-9de3-c466da230852",
  NOT_SUBJECT_TO_ANNOUCEMENT: "fabc4947-4cec-4bc1-8f76-0f5f338ca45b",
	NOT_SUBJECT_TO_REGISTER: "42b07fef-e6e1-4e2b-9daf-cfe6d235c2cb",
}

export const eActDataRegistration = {
  SYNDIC: "ec1e4da9-d9cf-4e06-acb6-91456edc7afe",
  TRUSTED_PERSON: "0642c478-c7dd-4f57-ac0a-6e4911a32740"
}