<template>
  <v-container fluid tag="section" class="full-height">
    <base-v-component :heading="$route.meta.title" />

    <base-material-card
      color="primary"
      icon="mdi-account-tie"
      inline
      class="px-5 py-5 mt-15"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $route.name }}</div>
      </template>

      <template v-slot:after-heading-button>
        <v-btn
          class="primary"
          @click="addNew"
        >
          <v-icon left>mdi-plus</v-icon>
          Нов шаблон
        </v-btn>
      </template>

      <base-kendo-grid
        :columns="table.headers"
        :items="table.data"
        :pagination="table.pagination"
        sortable
        :sort.sync="table.sort"
        @getData="getData"
        @click="tableActions"
        @dblclick="preview"
      />
    </base-material-card>
    <template-modal ref="templateModal" @reloadData="getData" />
  </v-container>
</template>

<script>
import { TemplateModal } from "@/components";

export default {
  name: "templatesList",
  components: {
    TemplateModal
  },
  data: () => ({
    table: {
      headers: [
        { title: "Тип шаблон", field: "", sortable: true },
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
            { title: 'Свали шаблона', icon: 'mdi-download', color: 'white', class: 'primary mr-1', click: 'download' },
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
  }),
  computed: {
  },
  mounted() {
    this.getData();
  },
  watch: {
    'table.sort': function(){
      this.getData();
    }
  },
  methods: {
    getData() {
      let query = Object.assign({filters: {}}, {});
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;

      query.pageSize = this.table.pagination.take;
      if(query.filters.page === 1){
        query.pageNumber = 1;
        this.table.pagination.skip = 0;
      }

      if(this.table.sort)
        query.filters.sort = this.table.sort

      // apiGetTemplates(query).then((result) => {
      //   this.table.data = result.items;
      //   this.table.pagination.total = result.totalCount;
      //   this.table.pagination.page = result.currentPage;
      // });
    },
    preview(item) {
      this.$refs.templateModal.openModal(item.id)
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
          this.confirmDeleteTemplate()
        break;
      }
    },
    addNew(){
      this.$refs.templateModal.openModal()
    },
    downloadFile(rowData) {
      downloadAttachedFile(rowData.id).then((response) => {
        downloadFileFromResponse(response);
      });
    },
    confirmDeleteTemplate(fileId) {
      let confirmData = {
        title: "Изтриване на шаблон",
        body: `Сигурни ли сте, че искате да изтриете избрания шаблон?`,
        callback: this.deleteTemplateFile,
        parameter: fileId
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteTemplateFile(fileId) {
      // deleteDocument(fileId).then(() => {
      //   this.$refs.confirmModal.closeModal();

      //   this.getData()
      // });
    },
  },
};
</script>
