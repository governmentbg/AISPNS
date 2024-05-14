<template>
  <v-container fluid tag="section" class="full-height" v-if="isSyndic">
    <base-v-component :heading="$route.meta.title" />
    
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
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import { apiGetAllDocumentFiles, apiDownloadDocument } from "@/api/documents";
import { eDocumentContentTypes } from "@/utils/enums/enumerators";
import { bytesToSize, downloadFileFromResponse } from "@/utils";
export default {
  name: "documentsList",
  components: {
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
    ...mapGetters(["isSyndic"]),
  },
  mounted() {
    this.table.headers = [
      { title: "Описание", field: "description",  sortable: false },
      { title: "Файл", field: "fileName",  sortable: false },
      { title: "Размер", field: 'fileSize', cell: this.renderFileSize, width: '100px',  sortable: false },
      { title: "Качен на", field: 'blobDateCreated', type: "date", format: "{0:dd.MM.yyyy}",  sortable: false, width: '100px' },
      {
        title: "",
        cell: "actions",
        filterable: false,
        width: "50px",
        sortable: false,
        buttons: [
          {
            title: "Свали",
            icon: "mdi-download fs20",
            color: "white",
            class: "transparent primary--text mr-1",
            click: "download",
          },
        ],
      },
    ]
    this.getData();
  },
  watch: {
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
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "download":
          this.download(rowData);
        break;
      }
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
  },
};
</script>
