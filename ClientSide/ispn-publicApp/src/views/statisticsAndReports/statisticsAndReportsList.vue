<template>
  <v-container fluid tag="section" class="full-height">

    <statistics-and-reports-list-filter ref="statisticsAndReportsListFilter" :search-by-case="false" @doFilter="getData" class="mt-5" />

    <base-material-card
      color="primary"
      icon="mdi-chart-multiple"
      inline
      class="px-5 py-5 mt-15"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $t('statistics_and_reports') }}</div>
      </template>

      <base-data-table 
        :headers="table.headers" 
        :pagination="table.pagination" 
        :data="table.data" 
        :sortBy.sync="table.sortBy"
        :sortDesc.sync="table.sortDesc" 
        @getData="getData" 
        @doAction="tableActions"
      />
    </base-material-card>
  </v-container>
</template>

<script>
import { StatisticsAndReportsListFilter } from "@/components";
import { apiGetStatisticsAndReports } from "@/api/statisticsAndReports";
export default {
  name: "statisticsAndReportsList",
  components: {
    StatisticsAndReportsListFilter,
  },
  data: () => ({
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
      { text: this.$t('date_published'), value: "publishedDate", sortable: false, valueType: "date" },
      { text: this.$t('type'), value: "type", sortable: false, valueType: "text" },
      { text: this.$t('period'), value: ["fromDate", "toDate"], sortable: false, valueType: "combineValuesObject", valueSeparator: " - " },
      { text: this.$t('source'), value: "reportSource", sortable: false, valueType: "text" },
      {
        sortable: false,
        text: '',
        value: '',
        valueType: 'button',
        style: "width: 50px;",
        class: 'text-right',
        buttons: [
          { label: '', title: this.$t('preview'), icon: 'mdi-text-box-search-outline', size: 'small', class: 'transparent primary--text', click: 'preview' }
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
      query.filter = Object.assign({}, this.$refs.statisticsAndReportsListFilter.filters);
      query.pageNumber = this.table.pagination.page;
      query.pageSize = this.table.pagination.itemsPerPage;

      apiGetStatisticsAndReports(query).then((result) => {
        this.$set(this.table, "data", result.items);
        this.$set(this.table.pagination, "total", result.totalCount);
        this.$set(this.table.pagination, "page", result.currentPage);
      });
    },
    preview(item) {
      this.$router.push({ path: `/statistics-and-reports/${item.id}` });
    },
    tableActions({ action, item }) {
      switch (action) {
        case "preview":
          this.preview(item);
        break;
      }
    },
  },
};
</script>
