<template>
  <v-container fluid tag="section" class="full-height">
    <base-v-component :heading="$route.meta.title" />

    <v-row class="my-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          class="primary"
          @click="addNew"
        >
          <v-icon left dark>mdi-plus</v-icon>
          Нов документ
        </v-btn>
      </v-col>
    </v-row>
    
    <base-material-card
      color="primary"
      icon="mdi-file-document-multiple-outline"
      inline
      class="px-5 py-5 mt-15"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $route.name }}</div>
      </template>

      <base-kendo-grid
        :columns="table.headers"
        :items="table.data"
        :pagination="table.pagination"
        sortable
        :sort.sync="table.sort"
        @getData="getData"
        @click="tableActions"
      />
    </base-material-card>
    <document-upload-modal ref="documentUploadModal" @reloadData="getData" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { DocumentUploadModal } from "@/components";
import { ISODateString, bytesToSize, downloadFileFromResponse } from "@/utils";
import { apiGetAllDocumentFiles, apiDeleteDocument, apiDownloadDocument } from "@/api/documents";
import { eDocumentContentTypes } from "@/utils/enums/enumerators";
export default {
  name: "documentsList",
  components: {
    DocumentUploadModal
  },
  data: () => ({
    table: {
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
  }),
  computed: {
  },
  mounted() {
    this.table.headers = [
      { title: "Описание", field: "description",  sortable: false },
      { title: "Файл", field: "fileName",  sortable: false },
      { title: "Размер", field: 'fileSize', cell: this.renderFileSize },
      { title: "Качен на", field: "blobDateCreated", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false, width: '100px' },
      {
        title: "",
        cell: "actions",
        filterable: false,
        width: "120px",
        sortable: false,
        buttons: [
          {
            title: "Преглед",
            icon: "mdi-pencil",
            color: "white",
            class: "primary mr-1",
            click: "preview",
          },
          {
            title: "Свали",
            icon: "mdi-download",
            color: "white",
            class: "primary lighten-1 mr-1",
            click: "download",
          },
          {
            title: "Изтрий",
            icon: "mdi-trash-can-outline",
            color: "white",
            class: "secondary",
            click: "delete",
          },
        ],
      },
    ]
    this.getData();
  },
  watch: {
    'table.sort': function(){
      this.getData();
    }
  },
  methods: {
    getData() {
      let query = Object.assign({}, {});
      
      query.filter = [eDocumentContentTypes.WEBSITE_DOCUMENT];
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;

      query.pageSize = this.table.pagination.take;

      apiGetAllDocumentFiles(query).then((result) => {
        this.$set(this.table, "data", result.items);
        this.$set(this.table.pagination, "total", result.totalCount);
        this.$set(this.table.pagination, "page", result.currentPage);
      });
    },
    preview(fileData){
      this.$refs.documentUploadModal.openModal(fileData.id);
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
    deleteAsk(fileData){
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
          this.getData();
          this.$refs.confirmModal.closeModal();
        }
      })
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
        case "download":
          this.download(rowData);
        break;
        case "delete":
          this.deleteAsk(rowData);
        break;
      }
    },
    addNew(){
      this.$refs.documentUploadModal.openModal()
    },
  },
};
</script>
