<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isSyndic"
  >
    <base-v-component
      :heading="isNewDoc ? 'Нов отчет' : 'Преглед на отчет'"
      goBackText="Обратно към отчети"
      goBackTo="/reports"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4" v-if="false">
          Създаденo на: <strong> {{ ISODateString(data.dateCreated) }} </strong> <br />
          Последна промяна: <strong> {{ ISODateString(data.dateModified) }} </strong> <br />
        </v-sheet>
      </v-col>
      <v-col cols="12" lg="7" xl="8" class="text-right">
        <v-btn
          color="primary"
          @click="save"
        >
          <v-icon left dark>mdi-check</v-icon>
          Запази
        </v-btn>
        <v-btn
          color="secondary"
          @click="generateReport"
          v-if="!isNewDoc"
        >
          <v-icon left dark>mdi-progress-download</v-icon>
          Генерирай отчет
        </v-btn>
      </v-col>
    </v-row>
    

    <v-row class="my-10">
      <v-col cols="12">

        <base-material-card 
          icon="mdi-gavel"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Дело
          </template>
          
          <template v-slot:after-heading-button>
            <v-btn
              class="primary"
              @click="openCasesModal"
            >
              <v-icon left dark>mdi-plus</v-icon>
              Избери дело
            </v-btn>
          </template>
          

          <v-form
            ref="form"
            lazy-validation
            class="pa-3"
          >
            <v-row>
              <v-col cols="12" xl="2" lg="4" md="6">
                <base-input
                  label="Дело номер"
                  v-model="caseData.number"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="6">
                <base-input
                  label="Дело година"
                  v-model="caseData.year"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="5" lg="4" md="6">
                <base-autocomplete
                  label="Съд"
                  :value="caseData.court?.id"
                  :items="nomenclatures.courts"
                  item-text="name"
                  item-value="id"
                  readonly
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Синдик"
                  v-model="syndicName"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Длъжник име"
                  v-model="debtorName"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Длъжник ЕИК"
                  v-model="debtorBulstat"
                  readonly
                />
              </v-col>
              <v-col cols="12" v-if="false">
                <base-input
                  label="Длъжник адрес"
                  v-model="caseData.debtorName"
                  :rules="[rules.requiredSelect]"
                />
              </v-col>
            </v-row>
          </v-form>
        </base-material-card>

        <base-material-card 
          icon="mdi-file-chart-outline"
          color="primary"
          class="elevation-3 mt-15"
        >
          <template v-slot:after-heading>
            Отчет
          </template>

          <v-form
            ref="form"
            lazy-validation
            class="pa-3"
          >
            <v-row>
              <v-col cols="12" xl="4" lg="5" md="6">
                <base-autocomplete
                  label="Тип"
                  v-model="data.reportTypeId"
                  :items="nomenclatures.reportTypes"
                  :rules="[rules.required]"
                  item-text="name"
                  item-value="id"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="6">
                <base-input
                  label="Идент №"
                  v-model="data.number"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="6">
                <base-material-date-picker
                  label="Дата"
                  v-model="data.reportDate"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="6">
                <base-material-date-picker
                  label="От дата"
                  v-model="data.fromDate"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="6">
                <base-material-date-picker
                  label="До дата"
                  v-model="data.toDate"
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Бележки"
                  v-model="data.notes"
                  rows="2"
                  auto-grow
                />
              </v-col>
            </v-row>
          </v-form>
        </base-material-card>

        <base-material-card 
          icon="mdi-file-document-edit-outline"
          color="primary"
          class="elevation-3 mt-15"
          v-if="false"
        >
          <template v-slot:after-heading>
            Образци
          </template>
          
          <template v-slot:after-heading-button>
            <v-btn
              class="primary"
              @click="openTemplateModal"
            >
              <v-icon left dark>mdi-plus</v-icon>
              Нов образец
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="tables.templates.headers"
            :items="tables.templates.data"
            :pagination="tables.templates.pagination"
            sortable
            :sort.sync="tables.templates.sort"
            @getData="getTemplatesData"
            @click="tableActions"
            class="mt-5"
          />

        </base-material-card>

        <base-material-card 
          icon="mdi-format-list-bulleted"
          color="primary"
          class="elevation-3 mt-15"
          v-if="showActions"
        >
          <template v-slot:after-heading>
            Действия
          </template>
          
          <template v-slot:after-heading-button>
            <v-btn
              class="primary"
              @click="openActionModal"
            >
              <v-icon left dark>mdi-plus</v-icon>
              Ново действие
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="tables.actions.headers"
            :items="tables.actions.data"
            :pagination="tables.actions.pagination"
            sortable
            :sort.sync="tables.actions.sort"
            @getData="getActionsData"
            @click="tableActions"
            class="mt-5"
          />

        </base-material-card>

        <base-material-card 
          icon="mdi-file-multiple-outline"
          color="primary"
          class="elevation-3 mt-15"
          v-if="!isNewDoc"
        >
          <template v-slot:after-heading>
            Файлове
          </template>
          
          <template v-slot:after-heading-button>
            <v-btn
              class="primary"
              @click="addNewFile"
            >
              <v-icon left dark>mdi-plus</v-icon>
              Нов файл
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="tables.files.headers"
            :items="tables.files.data"
            :pagination="tables.files.pagination"
            @getData="getDocumentsListData"
            @click="tableActions"
          />
        </base-material-card>
      </v-col>
    </v-row>
    <cases-list-selection-modal ref="casesListSelectionModal" @selected="selectedCase" :is-syndic="true"/>
    <journal-action-modal ref="journalActionModal" @reload="getActionsData" v-if="showActions" />
    <report-template-modal ref="reportTemplateModal" @reload="getTemplatesData" />
    <report-upload-file-modal ref="reportUploadFileModal" :parent-data="data" @reloadData="getDocumentsListData" @setDocumentCollectionId="setDocumentCollectionId" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, bytesToSize, downloadFileFromResponse } from '@/utils';
import { CasesListSelectionModal, JournalActionModal, ReportTemplateModal, ReportUploadFileModal } from '@/components';
import { apiMetaDataGetSyndicReportTypes, apiMetaDataGetCourts } from "@/api/metaData";
import { apiGetReportById, apiCreateReport, apiUpdateReport, apiGenerateReportTemplate } from "@/api/reports";
import { apiGetAnnouncementCaseById } from '@/api/sellAnnouncements';
import { apiDeleteDocument, apiDownloadDocument, apiGetAllDocumentFiles } from '@/api/documents';
export default {
  name: "templatePreview",
  components: {
    CasesListSelectionModal,
    JournalActionModal,
    ReportTemplateModal,
    ReportUploadFileModal
  },
  data(){
    return {
      isNewDoc: true,
      isReply: false,
      caseData: {},
      nomenclatures: {
        reportTypes: [],
        courts: [],
        types: [
          {label: "Месечен отчет", value: 0},
          {label: "6 месечен отчет", value: 1},
          {label: "Годишен отчет", value: 2},
          {label: "Отчет за разходите", value: 3},
          {label: "Степента на удовлетворяване на кредиторите", value: 4},
          {label: "Отговор по конкретно зададени въпроси", value: 5},
          {label: "Образец \"Сметка за разпределение\"", value: 6},
          {label: "Образец \"План за осребряване\"", value: 7},
        ],
      },
      tables: {
        templates: {
          headers: [
            { title: "Вид", field: "type", sortable: true },
            { title: "Дата", field: "date", sortable: true },
            { title: "Описание", field: "description", sortable: true },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                {
                  title: "Преглед",
                  icon: "mdi-text-box-search-outline fs18",
                  color: "white",
                  class: "transparent primary--text mr-1",
                  click: "previewTemplate",
                },
              ],
            },
          ],
          data: [
            {
              id: 1,
              type: "Вид 1",
              date: "2021-01-01",
              description: "Описание 1",
            },
            {
              id: 2,
              type: "Вид 2",
              date: "2021-01-02",
              description: "Описание 2",
            },
          ],
          sort: {},
          pagination: {
            page: 1,
            skip: 0,
            take: 20,
            perPageOptions: [5, 10, 20, 50, 100],
            total: 0,
          }
        },
        actions: {
          headers: [
            { title: "Вид", field: "", sortable: true },
            { title: "Дата", field: "", sortable: true },
            { title: "Пореден номер", field: "", sortable: true },
            { title: "Описание", field: "", sortable: false },
            { title: "Синдик", field: "", sortable: true },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewJournal" },
              ],
            },
          ],
          data: [],
          sort: {
            field: 'date',
            dir: 'desc'
          },
          pagination: {
            skip: 0,
            take: 10,
            total: 0,
            page: 1
          }
        },
        files: {
          headers: [
            { title: "Файл", field: "fileName", sortable: false },
            { title: "Описание", field: "description", sortable: false },
            { title: "Размер", cell: this.renderFileSize, sortable: false, width: '100px' },
            { title: "Качен на", field: "blobDateCreated", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false, width: '100px' },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "80px",
              sortable: false,
              buttons: [
                {
                  title: "Свали",
                  icon: "mdi-download",
                  color: "white",
                  class: "primary mr-1",
                  click: "download",
                },
                {
                  title: "Изтрий",
                  icon: "mdi-trash-can-outline",
                  color: "white",
                  class: "secondary",
                  click: "deleteFileAsk",
                },
              ],
            },
          ],
          data: [],
          sort: {},
          pagination: {
            page: 1,
            skip: 0,
            take: 20,
            perPageOptions: [5, 10, 20, 50, 100],
            total: 0,
          }
        }
      },
      data: {},
      rules: {
        required: v => !!v || 'Полето е задължително.',
        requiredSelect: v => (typeof v === 'string' && v.length) || typeof v === 'boolean' || !isNaN(parseFloat(v)) && v >= 0 && v <= 999 || "Полето е задължително",
        requiredSelectReturnObjectMultiple: v => v.length > 0 || "Полето е задължително",
        min: v => (v && v.length >= 3) || 'Полето трябва да съдържа поне 3 символа',
        email: v => !v || /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) || 'E-mail-a трябва да бъде валиден адрес.'
      },
    }
  },
  mounted(){
    this.isNewDoc = true;
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;

      this.getData();
    } else {
      this.data = {};
    }

    this.getReportTypes();
    this.getCourts();
  },
  computed: {
    ...mapGetters([
      'isSyndic',
      'currentUser'
    ]),
    showActions(){
      return [0,1,2].includes(this.data.type)
    },
    syndicName(){
      if(this.isNewDoc){
        return this.currentUser.fullName
      } else {
        return this.data.syndicName
      }
    },
    debtorName(){
      return this.caseData?.side?.entity?.name || '';
    },
    debtorBulstat(){
      return this.caseData?.side?.entity?.bulstat || '';
    },
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetReportById(this.data.id).then(result => {
          this.$set(this, 'data', result);
          this.getDocumentsListData();

          if(this.data.caseId)
          apiGetAnnouncementCaseById(this.data.caseId).then(result => {
            this.$set(this, 'caseData', result);
          })
        })
    },
    getReportTypes(){
      apiMetaDataGetSyndicReportTypes().then(result => {
        this.nomenclatures.reportTypes = result;
      })
    },
    getCourts(){
      apiMetaDataGetCourts().then(result => {
        this.nomenclatures.courts = result;
      })
    },
    getTemplatesData(){

    },
    getActionsData(){
      // apiGetReportActions().then(result => {
      //   this.nomenclatures.syndics = result;
      // })
    },
    save(){
      if(this.$refs.form.validate()){
        if(this.isNewDoc){
          apiCreateReport(this.data).then(result => {
            if(result && result !== '00000000-0000-0000-0000-000000000000')
              this.$router.push({path: `/reports/${result}`})
          })
        } else {
          apiUpdateReport(this.data)
        }
      }
    },
    openTemplateModal(){
      this.$refs.reportTemplateModal.openModal();
    },
    previewTemplate(item){
      this.$refs.reportTemplateModal.openModal(item.id);
    },
    openActionModal(){
      this.$refs.journalActionModal.openModal();
    },
    previewAction(item){
      this.$refs.journalActionModal.openModal(item.id);
    },
    openCasesModal(){
      this.$refs.casesListSelectionModal.openModal();
    },
    selectedCase(item){
      apiGetAnnouncementCaseById(item.id).then(result => {
        this.$set(this.data, "caseId", result.id)
        this.$set(this, 'caseData', result);
      })
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "previewTemplate":
          this.previewTemplate(rowData);
        break;
        case "previewAction":
          this.previewAction(rowData);
        break;
        case "download":
          this.download(rowData);
        break;
        case "deleteFileAsk":
          this.deleteFileAsk(rowData);
        break;
      }
    },
    
    addNewFile(){
      this.$refs.reportUploadFileModal.openModal();
    },
    deleteFileAsk(fileData){
      let confirmData = {
        title: "Изтриване на файл",
        body: `Сигурни ли сте, че искате да изтриете избрания файл?`,
        callback: this.deleteFile,
        parameter: fileData.id
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteFile(fileId){
      apiDeleteDocument(fileId).then(result => {
        if(result){
          this.getDocumentsListData();
          this.$refs.confirmModal.closeModal();
        }
      })
    },
    getDocumentsListData(){
      if(this.data.documentCollectionId){
        let query = Object.assign({}, {});
        
        query.pageNumber = this.tables.files.pagination.skip / this.tables.files.pagination.take + 1;
        query.pageSize = this.tables.files.pagination.take;
        query.filter = [this.data.documentCollectionId]
        
        apiGetAllDocumentFiles(query).then((result) => {
          this.tables.files.data = result.items;
          this.tables.files.pagination.total = result.totalCount;
          this.tables.files.pagination.page = result.currentPage;
        });
      }
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
    setDocumentCollectionId(documentCollectionId){
      this.data.documentCollectionId = documentCollectionId
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
    generateReport(){
      apiGenerateReportTemplate(this.data.reportTypeId).then(result => {
        downloadFileFromResponse(result)
      })
    },
    ISODateString: ISODateString
  }
}
</script>