<template>
  <v-container fluid tag="section" class="full-height" v-if="isSyndic">
    <base-v-component :heading="$route.meta.title" />

    <v-row class="my-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          class="primary"
          @click="addNew"
        >
          <v-icon left dark>mdi-plus</v-icon>
          Нов отчет
        </v-btn>
      </v-col>
    </v-row>

    <reports-filter ref="reportsFilter" @doFilter="getReportsData" />

    <base-material-card
      color="primary"
      icon="mdi-file-chart-outline"
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
        @getData="getReportsData"
        @click="tableActions"
        @dblclick="preview"
      />

      
    </base-material-card>
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import { ReportsFilter } from "@/components";
import { apiSearchReports } from "@/api/reports";
export default {
  name: "reportsList",
  components: {
    ReportsFilter
  },
  data: () => ({
    table: {
      headers: [
        { title: "Дело Номер", field: "caseNumber", sortable: true },
        { title: "Дело Година", field: "caseYear", sortable: true },
        { title: "Съд", field: "courtName", sortable: true },
        { title: "Вид образец", field: "reportTypeName", sortable: true },
        { title: "Дата", field: "reportDate", type: 'date', format: "{0:dd.MM.yyyy}", sortable: true },
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
              click: "preview",
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
  }),
  computed: {
    ...mapGetters(["isSyndic"]),
  },
  mounted() {
    this.getReportsData();
  },
  watch: {
    'table.sort': function(){
      this.getReportsData();
    },
  },
  methods: {
    getReportsData() {
      let query = {};
      query.filter = Object.assign({}, this.$refs.reportsFilter.filters);
      
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
      query.pageSize = this.table.pagination.take;

      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.table.pagination.skip = 0;
      }

      apiSearchReports(query).then((result) => {
        this.table.data = result.items;
        this.table.pagination.total = result.totalCount;
        this.table.pagination.page = result.currentPage;
      });
    },
    preview(item) {
      this.$router.push({ path: `/reports/${item.id}` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
    addNew(){
      this.$router.push({path: `/reports/create`})
    },
  },
};
</script>
