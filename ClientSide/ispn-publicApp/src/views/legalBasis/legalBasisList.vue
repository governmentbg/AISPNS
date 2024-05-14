<template>
  <v-container fluid tag="section" class="full-height">

    <base-material-card
      color="primary"
      icon="mdi-file-document-edit-outline"
      inline
      class="px-5 py-5 mt-5"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $t('legal_basis') }}</div>
      </template>

      <base-data-table 
        :headers="table.headers" 
        :pagination="table.pagination" 
        :data="table.data" 
        @getData="getData" 
        @doAction="tableActions"
      />
    </base-material-card>
  </v-container>
</template>

<script>
import { apiGetAllDocumentLegalBasis } from "@/api/syndics";
import { apiDownloadDocument } from "@/api/documents";
import { downloadFileFromResponse } from "@/utils";

export default {
  name: "legalBasisList",
  components: {
  },
  data: () => ({
    showAlert: false,
    table: {
      headers: [],
      data: [],
      sort: {},
      pagination: {
        itemsPerPage: 15,
        page: 1,
        perPageOptions: [5, 15, 25, 50, 100],
        total: 0
      },
    },
  }),
  computed: {
  },
  mounted() {
    this.table.headers = [
      { text: this.$t('description'), value: "description", valueType: 'text',  sortable: false },
      { text: this.$t('date'), value: "date", valueType: 'date',  sortable: false },
      {
        sortable: false,
        text: '',
        value: '',
        valueType: 'button',
        style: "width: 50px;",
        class: 'text-right',
        buttons: [
          { label: '', title: this.$t('download'), icon: 'mdi-download', size: 'small', class: 'transparent primary--text', click: 'download', if: {type: 'notEmpty', item: 'documentContentId'} }
        ],
      }
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
      query.filter = {};
      query.pageNumber = this.table.pagination.page;
      query.pageSize = this.table.pagination.itemsPerPage;

      apiGetAllDocumentLegalBasis(query).then((result) => {
        this.table.data = result.items;
        this.table.pagination.total = result.totalCount;
        this.table.pagination.page = result.currentPage;
        this.showAlert = true;
      });
    },
    download(item) {
      apiDownloadDocument(item.documentContentId).then(result => {
        downloadFileFromResponse(result);
      });
    },
    tableActions({ action, item }) {
      switch (action) {
        case "download":
          this.download(item);
        break;
      }
    },
  },
};
</script>
