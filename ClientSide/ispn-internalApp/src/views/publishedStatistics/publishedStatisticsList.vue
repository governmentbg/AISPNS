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
          Ново публикуване на статистика
        </v-btn>
      </v-col>
    </v-row>

    <published-statistics-filter ref="publishedStatisticsFilter" @doFilter="getData" />
    
    <base-material-card
      color="primary"
      icon="mdi-chart-line"
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
  </v-container>
</template>

<script>
import { PublishedStatisticsFilter } from "@/components";
import { ISODateString } from "@/utils";
import { apiGetStatisticsAndReports } from "@/api/statisticsAndReports"
export default {
  name: "publishedStatisticsList",
  components: {
    PublishedStatisticsFilter
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
      { title: "Дата на публикуване", field: "published", type: "date", format: "{0:dd.MM.yyyy}",  sortable: true },
      { title: "Тип", field: "typeNavigation.name", sortable: true },
      { title: "Период", cell: this.renderPeriod, sortable: true },
      { title: "Източник", field: "reportSource.name", sortable: true },
      {
        title: "",
        cell: "actions",
        filterable: false,
        width: "50px",
        sortable: false,
        buttons: [
          {
            title: "Преглед",
            icon: "mdi-text-box-search-outline",
            color: "white",
            class: "primary",
            click: "preview",
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
      
      query.filter = Object.assign({}, this.$refs.publishedStatisticsFilter.filters);
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;

      query.pageSize = this.table.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.table.pagination.skip = 0;
      }

      if(this.table.sort)
        query.filter.sort = this.table.sort

      apiGetStatisticsAndReports(query).then((result) => {
        this.$set(this.table, "data", result.items);
        this.$set(this.table.pagination, "total", result.totalCount);
        this.$set(this.table.pagination, "page", result.currentPage);
      });
    },
    preview(item) {
      this.$router.push({ path: `/published-statistics/${item.id}` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
    renderPeriod(h, tdElement , props ) {
      let item = props.dataItem;
      let result = '';
      if(item.fromDate)
        result = ISODateString(item.fromDate)

      if(item.toDate)
          result += (result.length ? ' - ' : '') + ISODateString(item.toDate);

        return h('td', null, [
          result
        ]);
    },
    addNew(){
      this.$router.push({path: `/published-statistics/create`})
    },
  },
};
</script>
