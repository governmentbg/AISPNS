<template>
  <v-container fluid tag="section" class="full-height">
    <base-v-component :heading="$route.meta.title" />

    <v-divider class="mt-5" />

    <cases-list-filter ref="casesStabilizationListFilter" @doFilter="getData" />

    <base-material-card
      color="primary"
      icon="mdi-gavel"
      inline
      class="px-5 py-5 mt-15"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $route.meta.title }}</div>
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
import { CasesListFilter } from "@/components";
import { apiGetCases } from "@/api/cases";

export default {
  name: "casesStabilizationList",
  components: {
    CasesListFilter,
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
      { title: "№", field: "number", sortable: true },
      { title: "Година", field: "year", sortable: true },
      { title: "Дата", field: "formationDate", type: 'date', format: "{0:dd.MM.yyyy}", sortable: true },
      { title: "Съд", field: "court.name", sortable: true },
      { title: "Актове подлежащи на обжалване", field: "acts", cell: this.renderActsLink, sortable: false, width: "300px" },
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
      
      query.filter = Object.assign({}, this.$refs.casesStabilizationListFilter.filters);
      query.filter.isStabilization = true;

      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;

      query.pageSize = this.table.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.table.pagination.skip = 0;
      }

      if(this.table.sort)
        query.filter.sort = this.table.sort

      apiGetCases(query).then((result) => {
        this.table.data = result.items;
        this.table.pagination.total = result.totalCount;
        this.table.pagination.page = result.currentPage;
      });
    },
    preview(item) {
      this.$router.push({ path: `/cases/stabilization/${item.id}` });
    },
    renderActsLink(h, tdElement , props) {
      let item = props.dataItem
      return h('td', {class: 'text-center'}, [
        h('v-btn', {
          props: {
            icon: false,
            text: true,
            small: true,
            color: 'primary',
          },
          on: {
            click: () => {
              this.$router.push(`/cases/stabilization/${item.id}/acts`)
            },
          },
        }, [
          'Актове',
        ]),
      ]);
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
  },
};
</script>
