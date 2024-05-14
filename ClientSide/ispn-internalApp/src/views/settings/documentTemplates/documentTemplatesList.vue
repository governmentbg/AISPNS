<!-- eslint-disable vue/valid-v-for -->
<template>
  <v-container 
    id="nomenclatureTabs" 
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
      <v-tab v-for="tab in tabs" :key="`${tab.key}`" active-class="secondary">
        {{ tab.name }}
      </v-tab>

      <v-tabs-items v-model="currentTab"  class="py-5 px-3 pb-0">
        <v-tab-item v-for="tab in tabs" :key="tab.key">
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
                :data-items="tables[tab.key].data"
                :pagination="tables[tab.key].pagination"
                sortable
                :sort.sync="tables[tab.key].sort"
                @getData="getTablesData"
                @click="tableActions"
                @dblclick="preview"
              />
            </v-card-text>
          </v-card>
        </v-tab-item>
      </v-tabs-items>
    </v-tabs>


    <base-confirm-modal ref="confirmModal" />
    <document-template-modal ref="documentTemplateModal" @reloadData="getTablesData" />
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import { DocumentTemplateModal } from "@/components";
import { Grid } from "@progress/kendo-vue-grid";

export default {
  name: "documentTemplatesTabs",
  components: {
    Grid,
    DocumentTemplateModal
  },
  data: () => ({
    tabs: [
      { key: 'syndics', name: 'Синдици' },
      { key: 'directive28', name: 'по чл. 28 от Директивата' },
      { key: 'others', name: 'Други' }
    ],
    tables: {
      syndics: {
        headers: [
          { title: "Образец", field: "", sortable: true },
          { title: "Качен на", field: "", sortable: false },
          { title: "Качен от", field: "", sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "preview", },
              { title: 'Свали образеца', icon: 'mdi-download', color: 'white', class: 'primary mr-1', click: 'download' },
              { title: 'Изтрий', icon: 'mdi-trash-can-outline', color: 'grey', click: 'delete' }
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
        },
      },
      directive28: {
        headers: [
          { title: "Образец", field: "", sortable: true },
          { title: "Качен на", field: "", sortable: false },
          { title: "Качен от", field: "", sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "preview", },
              { title: 'Свали образеца', icon: 'mdi-download', color: 'white', class: 'primary mr-1', click: 'download' },
              { title: 'Изтрий', icon: 'mdi-trash-can-outline', color: 'grey', click: 'delete' }
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
        },
      },
      others: {
        headers: [
          { title: "Образец", field: "", sortable: true },
          { title: "Качен на", field: "", sortable: false },
          { title: "Качен от", field: "", sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "preview", },
              { title: 'Свали образеца', icon: 'mdi-download', color: 'white', class: 'primary mr-1', click: 'download' },
              { title: 'Изтрий', icon: 'mdi-trash-can-outline', color: 'grey', click: 'delete' }
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
        },
      }
    },
    currentTab: 0,
  }),
  computed: {
    ...mapGetters([
      'isAdministrator'
    ]),
    getTableTitle(){
      switch(this.currentTab){
        case 0:
          return "Образци синдици"
        case 1:
          return "Образци по чл.28 от Директивата"
        case 2:
          return "Други образци"
        default:
          return "";
      }
    }
  },
  mounted() {},
  methods: {
    getTablesData(){
      switch(this.currentTab){
        case 0:
          this.getSyndicsData()
        break;
        case 1:
          this.getDirective28Data()
        break;
        case 2:
          this.getOthersData()
        break;
      }
    },
    getSyndicsData(){
      let query = Object.assign({filters: {}}, {});
      query.pageNumber = this.tables['syndics'].pagination.skip / this.tables['syndics'].pagination.take + 1;

      query.pageSize = this.tables['syndics'].pagination.take;
      if(query.filters.page === 1){
        query.pageNumber = 1;
        this.tables['syndics'].pagination.skip = 0;
      }

      if(this.tables['syndics'].sort)
        query.filters.sort = this.tables['syndics'].sort

      // apiGetSyndicsDocumentTemplate().then(result => {
      //   this.tables['syndics'].data = result.items;
      //   this.tables['syndics'].pagination.total = result.totalCount;
      //   this.tables['syndics'].pagination.page = result.currentPage;
      // })
    },
    getDirective28Data(){
      let query = Object.assign({filters: {}}, {});
      query.pageNumber = this.tables['directive28'].pagination.skip / this.tables['directive28'].pagination.take + 1;

      query.pageSize = this.tables['directive28'].pagination.take;
      if(query.filters.page === 1){
        query.pageNumber = 1;
        this.tables['directive28'].pagination.skip = 0;
      }

      if(this.tables['directive28'].sort)
        query.filters.sort = this.tables['directive28'].sort

      // apiGetDirective28DocumentTemplate().then(result => {
      //   this.tables['directive28'].data = result.items;
      //   this.tables['directive28'].pagination.total = result.totalCount;
      //   this.tables['directive28'].pagination.page = result.currentPage;
      // })
    },
    getOthersData(){
      let query = Object.assign({filters: {}}, {});
      query.pageNumber = this.tables['others'].pagination.skip / this.tables['others'].pagination.take + 1;

      query.pageSize = this.tables['others'].pagination.take;
      if(query.filters.page === 1){
        query.pageNumber = 1;
        this.tables['others'].pagination.skip = 0;
      }

      if(this.tables['others'].sort)
        query.filters.sort = this.tables['others'].sort

      // apiGetOthersDocumentTemplate().then(result => {
      //   this.tables['others'].data = result.items;
      //   this.tables['others'].pagination.total = result.totalCount;
      //   this.tables['others'].pagination.page = result.currentPage;
      // })
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
        case "download":
          this.downloadFile()
        break;
        case "delete":
          this.deleteDocumentTemplateAsk()
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
        callback: this.deleteDocumentTemplate,
        parameter: item.id,
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteDocumentTemplate(nomenclatureId) {
      // apiDeleteDocumentTemplate(docTemplateId).then((result) => {
      //     if (result) this.getTablesData();
      //     this.$refs.confirmModal.closeModal();
      // });
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
    }
  },
};
</script>

<style lang="sass">

</style>
