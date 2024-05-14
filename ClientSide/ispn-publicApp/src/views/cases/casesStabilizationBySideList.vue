<template>
  <v-container fluid tag="section" class="full-height">

    <cases-list-filter ref="casesStabilizationBySideListFilter" :search-by-case="false" @doFilter="getData" class="mt-5" />

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
        :sortBy.sync="table.sortBy"
        :sortDesc.sync="table.sortDesc" 
        @getData="getData" 
        @doAction="tableActions"
      />
    </base-material-card>
  </v-container>
</template>

<script>
import { CasesListFilter } from "@/components";
import { apiGetCasesByPerson, apiGetCasesByEntity } from "@/api/cases";

export default {
  name: "casesStabilizationBySideList",
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
      { text: this.$t('court'), value: "courtName", sortable: false, valueType: "text" },
      { text: this.$t('case_side'), value: "sideName", sortable: false, valueType: "text" },
      { text: this.$t('bulstat_eik'), value: "bulstat", sortable: false, valueType: "text" },
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
  },
  watch: {
  },
  methods: {
    getData(clear = false) {
      this.showTable = false;
      if(!clear){
        let query = Object.assign({}, {});
        query.filter = Object.assign({}, this.$refs.casesStabilizationBySideListFilter.filters);
        query.filter.isStabilization = true;
        query.pageNumber = this.table.pagination.page;
        query.pageSize = this.table.pagination.itemsPerPage;

        if(query.filter.page === 1)
          query.pageNumber = 1;

        if(this.$refs.casesStabilizationBySideListFilter.personType === 0){
          delete query.filter.page;
          delete query.filter.caseNumber
          delete query.filter.caseYear;
          delete query.filter.courtNumber;
          delete query.filter.entityName;
          delete query.filter.bulstat;

          apiGetCasesByPerson(query).then((result) => {
            this.table.data = result.items;
            this.table.pagination.total = result.totalCount;
            this.table.pagination.page = result.currentPage;
            this.showTable = true;
          });
        } else {
          delete query.filter.page;
          delete query.filter.firstName;
          delete query.filter.lastName;
          delete query.filter.EGN;
          delete query.filter.caseNumber
          delete query.filter.caseYear;
          delete query.filter.courtNumber;

          apiGetCasesByEntity(query).then((result) => {
            this.table.data = result.items;
            this.table.pagination.total = result.totalCount;
            this.table.pagination.page = result.currentPage;
            this.showTable = true;
          });
        }
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
