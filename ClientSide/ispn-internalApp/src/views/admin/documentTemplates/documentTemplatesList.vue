<!-- eslint-disable vue/valid-v-for -->
<template>
  <v-container
    fluid 
    tag="section" 
    class="full-height"
    v-if="isAdministrator"
  >
    <base-v-component :heading="$route.meta.title" />

    <v-tabs
      v-model="currentTab"
      slider-color="primary"
      color="secondary"
      fixed-tabs
      @change="tabChange"
      class="my-10"
    >
      <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key" active-class="secondary">
        {{ tab.name }}
      </v-tab>

      <v-tabs-items v-model="currentTab"  class="py-5 px-3 pb-0">
        <v-tab-item v-for="tab in tabs" :key="tab.key" :value="tab.key">
          <v-card flat class="">
            <v-card-title class="headline print d-print-flex mb-0">
              <v-row>
                  <v-col cols="12" md="6">
                    {{ getTableTitle }}
                  </v-col>
                  <v-col cols="12" md="6" class="text-right">
                  <v-btn color="primary" small @click="newDocumentTemplate">
                    <v-icon left> mdi-plus </v-icon>
                    Добави образец
                  </v-btn>
                </v-col>
              </v-row>
            </v-card-title>
            <v-card-text class="pa-0">
              <base-kendo-grid
                :columns="tables[tab.key].headers"
                :items="tables[tab.key].data"
                :pagination="tables[tab.key].pagination"
                @getData="getTablesData"
                @click="tableActions"
                @dblclick="preview"
              />
            </v-card-text>
          </v-card>
        </v-tab-item>
      </v-tabs-items>
    </v-tabs>


    <document-template-modal ref="documentTemplateModal" @reloadData="getTablesData" :type="currentTab" @confirm="getConfirm" @closeConfirm="$refs.confirmModal.closeModal()" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import { DocumentTemplateModal } from "@/components";
import { apiGetTemplateArticles28, apiDeleteTemplateArticle28, apiGetAdminTemplate, apiDeleteAdminTemplate, apiGetReportTemplate, apiDeleteReportTemplate } from "@/api/administration"
import { apiDownloadDocument } from "@/api/documents";
import { downloadFileFromResponse } from "@/utils";
export default {
  name: "documentTemplatesTabs",
  components: {
    DocumentTemplateModal
  },
  data: () => ({
    tabs: [
      { key: 'syndicTemplates', name: 'Образци синдици' },
      { key: 'syndicReports', name: 'Отчети синдици' },
      { key: 'directive28', name: 'по чл. 28 от Директивата' }
    ],
    tables: {
      syndicTemplates: {
        headers: [],
        data: [],
        sort: {},
        pagination: {
          page: 1,
          skip: 0,
          take: 20,
          perPageOptions: [5, 10, 20, 50, 100],
          total: 0,
        },
      },
      syndicReports: {
        headers: [],
        data: [],
        sort: {},
        pagination: {
          page: 1,
          skip: 0,
          take: 20,
          perPageOptions: [5, 10, 20, 50, 100],
          total: 0,
        },
      },
      directive28: {
        headers: [],
        data: [],
        sort: {},
        pagination: {
          page: 1,
          skip: 0,
          take: 20,
          perPageOptions: [5, 10, 20, 50, 100],
          total: 0,
        },
      },
    },
    currentTab: 'syndicTemplates',
  }),
  computed: {
    ...mapGetters([
      'isAdministrator'
    ]),
    getTableTitle(){
      switch(this.currentTab){
        case 'syndicTemplates':
          return "Образци синдици"
        case 'syndicReports':
          return "Отчети синдици"
        case 'directive28':
          return "Образци по чл.28 от Директивата"
        default:
          return "";
      }
    }
  },
  mounted() {
    this.tables.syndicTemplates.headers = [
      { title: "Вид", field: "templateKind", sortable: true },
      { title: "Описание", field: "templateName", sortable: false },
      { title: "Дата", field: "date", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false },
      {
        title: "",
        cell: "actions",
        filterable: false,
        width: "120px",
        sortable: false,
        buttons: [
          { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "preview", },
          { title: 'Свали образеца', icon: 'mdi-download', color: 'white', class: 'secondary mr-1', click: 'download', if: {field: 'documentContentID', value: null} },
          { title: 'Изтрий', icon: 'mdi-trash-can-outline', color: 'grey', click: 'delete' }
        ],
      }
    ];

    this.tables.syndicReports.headers = [
      { title: "Вид", field: "reportType", sortable: true },
      { title: "Описание", field: "templateName", sortable: false },
      { title: "Дата", field: "date", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false },
      {
        title: "",
        cell: "actions",
        filterable: false,
        width: "120px",
        sortable: false,
        buttons: [
          { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "preview", },
          { title: 'Свали образеца', icon: 'mdi-download', color: 'white', class: 'secondary mr-1', click: 'download', if: {field: 'documentContentID', value: null} },
          { title: 'Изтрий', icon: 'mdi-trash-can-outline', color: 'grey', click: 'delete' }
        ],
      }
    ];

    this.tables.directive28.headers = [
      { title: "Вид", field: "directiveTemplateKindName", sortable: true },
      { title: "Описание", field: "templateName", sortable: false },
      { title: "Дата", field: "date", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false },
      { title: "Публикуван", field: "isPublished", cell: 'status', sortable: false, width: '120px', className: 'text-center', headerClassName: 'text-center' },
      {
        title: "",
        cell: "actions",
        filterable: false,
        width: "120px",
        sortable: false,
        buttons: [
          { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "preview", },
          { title: 'Свали образеца', icon: 'mdi-download', color: 'white', class: 'secondary mr-1', click: 'download', if: {field: 'documentContentId', value: null} },
          { title: 'Изтрий', icon: 'mdi-trash-can-outline', color: 'grey', click: 'delete' }
        ],
      }
    ];

    this.getSyndicTemplatesData();
  },
  methods: {
    getTablesData(){
      switch(this.currentTab){
        case 'syndicTemplates':
          this.getSyndicTemplatesData()
        break;
        case 'syndicReports':
          this.getSyndicReportsData()
        break;
        case 'directive28':
          this.getDirective28Data()
        break;
      }
    },
    getSyndicTemplatesData(){
      let query = Object.assign({filters: {}}, {});
      query.pageNumber = this.tables['syndicTemplates'].pagination.skip / this.tables['syndicTemplates'].pagination.take + 1;
      query.pageSize = this.tables['syndicTemplates'].pagination.take;
      

      apiGetAdminTemplate(query).then(result => {
        this.tables['syndicTemplates'].data = result.items;
        this.tables['syndicTemplates'].pagination.total = result.totalCount;
        this.tables['syndicTemplates'].pagination.page = result.currentPage;
      })
    },
    getSyndicReportsData(){
      let query = Object.assign({filters: {}}, {});
      query.pageNumber = this.tables['syndicReports'].pagination.skip / this.tables['syndicReports'].pagination.take + 1;
      query.pageSize = this.tables['syndicReports'].pagination.take;

      apiGetReportTemplate(query).then(result => {
        this.tables['syndicReports'].data = result.items;
        this.tables['syndicReports'].pagination.total = result.totalCount;
        this.tables['syndicReports'].pagination.page = result.currentPage;
      })
    },
    getDirective28Data(){
      let query = Object.assign({filters: {}}, {});
      query.pageNumber = this.tables['directive28'].pagination.skip / this.tables['directive28'].pagination.take + 1;
      query.pageSize = this.tables['directive28'].pagination.take;

      apiGetTemplateArticles28(query).then(result => {
        this.$set(this.tables['directive28'], 'data', result.items);
        this.$set(this.tables['directive28'].pagination, "total", result.totalCount);
        this.$set(this.tables['directive28'].pagination, "page", result.currentPage);
      })
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
        case "download":
          this.download(rowData)
        break;
        case "delete":
          this.deleteDocumentTemplateAsk(rowData)
        break;
      }
    },
    preview(item){
      this.$refs.documentTemplateModal.openModal(item.id)
    },
    deleteDocumentTemplateAsk(item) {
      let confirmData = {
        title: "Изтриване на образец",
        body: `Сигурни ли сте, че искате да изтриете избрания образец?`,
        callback: this.type === 'directive28' ? this.deleteDocumentTemplate : this.type === 'syndicTemplates' ? this.deleteSyndicTemplate : this.deleteSyndicReportTemplate,
        parameter: item.id,
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteDocumentTemplate(id) {
      apiDeleteTemplateArticle28(id).then((result) => {
        if (result) this.getTablesData();
        this.$refs.confirmModal.closeModal();
      });
    },
    deleteSyndicTemplate(id){
      apiDeleteAdminTemplate(id).then(result => {
        if (result) this.getTablesData();
        this.$refs.confirmModal.closeModal();
      })
    },
    deleteSyndicReportTemplate(id){
      apiDeleteReportTemplate(id).then(result => {
        if (result) this.getTablesData();
        this.$refs.confirmModal.closeModal();
      })
    },
    download(fileData){
      apiDownloadDocument(this.type === 'directive28' ? fileData.documentContentId : fileData.documentContentID).then(result => {
        downloadFileFromResponse(result);
      });
    },
    pageChangeHandler(e) {
      this.tables[this.getCurrentTabDetails.key].pagination.take = e.page.take;
      this.tables[this.getCurrentTabDetails.key].pagination.skip = e.page.skip;
      this.getData();
    },
    tabChange() {
      this.getTablesData();
    },
    newDocumentTemplate(){
      this.$refs.documentTemplateModal.openModal()
    },
    getConfirm(confirmData){
      this.$refs.confirmModal.openModal(confirmData);
    }
  },
};
</script>

<style lang="sass">

</style>
