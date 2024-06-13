<template>
  <v-container fluid tag="section" class="full-height">
    <base-v-component :heading="$route.meta.title" />

    <web-service-filter ref="webServiceFilter" @doFilter="getData" />

    <base-material-card
      color="primary"
      icon="mdi-relation-one-or-many-to-zero-or-many"
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
        @dblclick="preview"
      />
    </base-material-card>
    <web-service-exception-modal ref="webServiceExceptionModal" />
  </v-container>
</template>

<script>
import { apiGetApiRequests } from "@/api/administration";
import { WebServiceFilter, WebServiceExceptionModal } from "@/components";
export default {
  name: "webServiceList",
  components: {
    WebServiceFilter,
    WebServiceExceptionModal
  },
  data: () => ({
    table: {
      headers: [
        { title: "Дата на заявка", field: "requestTimestamp", type: 'date', format: '{0:dd.MM.yyyyг. HH:mm:ssч.}', sortable: true, width: '160px' },
        { title: "Дата на отговор", field: "responseTimestamp", type: 'date', format: '{0:dd.MM.yyyyг. HH:mm:ssч.}', sortable: true, width: '160px' },
        { title: "IP Адрес", field: "ipAddress", sortable: false, width: '110px' },
        { title: "Код отговор", field: "responseHttpCode", sortable: false, width: '80px'  },
        { title: "Endpoint", field: "endpoint", sortable: false },
        { title: "requestContent", field: "requestContent", sortable: true },
        { title: "responseContent", field: "responseContent", sortable: true },
        {
          title: "",
          cell: "actions",
          filterable: false,
          width: "80px",
          sortable: false,
          buttons: [
            {
              title: "Преглед",
              icon: "mdi-text-box-search-outline",
              color: "white",
              class: "primary",
              click: "preview",
            },
            {
              title: "Преглед на грешка",
              icon: "mdi-database-eye-outline",
              color: "white",
              class: "error ml-1",
              click: "previewException",
              if: {field: 'exceptionId', value: null}
            }
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
  },
  methods: {
    getData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.webServiceFilter.filters);
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;

      query.pageSize = this.table.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.table.pagination.skip = 0;
      }

      apiGetApiRequests(query).then((result) => {
        this.table.data = result.items;
        this.table.pagination.total = result.totalCount;
        this.table.pagination.page = result.currentPage;
      });
    },
    preview(item) {
      this.$router.push({ path: `/admin/web-service/${item.id}` });
    },
    previewException(item){
      this.$refs.webServiceExceptionModal.openModal(item.exceptionId);
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
        case "previewException":
          this.previewException(rowData);
        break;
      }
    },
  },
};
</script>
