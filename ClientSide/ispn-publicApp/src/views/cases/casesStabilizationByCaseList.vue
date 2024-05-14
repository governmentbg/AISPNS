<template>
  <v-container fluid tag="section" class="full-height">

    <cases-list-filter ref="casesStabilizationByCaseListFilter" @doFilter="getData" class="mt-5" />

    <base-material-card
      color="primary"
      icon="mdi-gavel"
      inline
      class="px-5 py-5 mt-15"
      v-if="showTable"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $t('stabilization_cases') }}</div>
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
import { CasesListFilter } from "@/components";
import { apiGetCases } from "@/api/cases";

export default {
  name: "casesStabilizationList",
  components: {
    CasesListFilter,
  },
  data: () => ({
    showTable: false,
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
      { text: this.$t('number'), value: "number", sortable: false, valueType: "text" },
      { text: this.$t('year'), value: "year", sortable: false, valueType: "text" },
      { text: this.$t('date'), value: "formationDate", sortable: false, valueType: "date" },
      { text: this.$t('court'), value: "courtName", sortable: false, valueType: "text" },
      { text: this.$t('acts_subject_to_appeal'), value: "customLink", sortable: false, valueType: "customLink", linkName: this.$t('acts'), path: '/cases/stabilization/$$$/acts', width: '15%' },
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
      },
    ]
  },
  watch: {
    // 'table.sort': function(){
    //   this.getData();
    // }
  },
  methods: {
    getData(clear = false) {
      this.showTable = false;
      if(!clear){
        let query = Object.assign({}, {});
        query.filter = Object.assign({}, this.$refs.casesStabilizationByCaseListFilter.filters);
        query.filter.isStabilization = true;
        query.pageNumber = this.table.pagination.page;
        query.pageSize = this.table.pagination.itemsPerPage;

        if(query.filter.page === 1)
          query.pageNumber = 1;

        delete query.filter.page;
        delete query.filter.firstName;
        delete query.filter.lastName;
        delete query.filter.EGN;
        delete query.filter.entityName;
        delete query.filter.bulstat;

        apiGetCases(query).then((result) => {
          this.table.data = result.items;
          this.table.pagination.total = result.totalCount;
          this.table.pagination.page = result.currentPage;
          this.showTable = true;
        });
      }
    },
    preview(item) {
      this.$router.push({ path: `/cases/stabilization/${item.id}` });
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
