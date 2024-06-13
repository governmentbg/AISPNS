<template>
  <v-container fluid tag="section" class="full-height">
    <entrepreneurs-list-filter ref="entrepreneursBankruptcyListFilter" @doFilter="getData" class="mt-5" />

    <base-material-card
      color="primary"
      icon="mdi-badge-account-horizontal-outline"
      inline
      class="px-5 py-5 mt-15"
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
import { EntrepreneursListFilter } from "@/components";

export default {
  name: "entrepreneursStabilizationList",
  components: {
    EntrepreneursListFilter,
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
      { text: this.$t('number'), value: "number", sortable: false, valueType: "text" },
      { text: this.$t('year'), value: "year", sortable: false, valueType: "text" },
      { text: this.$t('date'), value: "date", sortable: false, valueType: "date" },
      { text: this.$t('court'), value: "court", sortable: false, valueType: "text" },
      { text: this.$t('acts_subject_to_appeal'), value: "customLink", sortable: false, valueType: "customLink", linkName: this.$t('acts'), path: '/entrepreneurs/stabilization/$$$/acts', width: '10%' },
      { text: this.$t('declared_acts_of_stabilization'), value: "customLink", sortable: false, valueType: "customLink", linkName: this.$t('acts'), path: '/entrepreneurs/stabilization/$$$/declared-acts', width: '13%' },
      { text: this.$t('stabilization_proceedings_data'), value: "customLink", sortable: false, valueType: "customLink", linkName: this.$t('datas'), path: '/entrepreneurs/stabilization/$$$/proceedings-data', width: '10%' },
      
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
      
      query.filters = Object.assign({}, this.$refs.entrepreneursBankruptcyListFilter.filters);
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;

      query.pageSize = this.table.pagination.take;
      if(query.filters.page === 1){
        query.pageNumber = 1;
        this.table.pagination.skip = 0;
      }

      if(this.table.sort)
        query.filters.sort = this.table.sort

      // apiGetBankruptcyCases(query).then((result) => {
      //   this.table.data = result.items;
      //   this.table.pagination.total = result.totalCount;
      //   this.table.pagination.page = result.currentPage;
      // });
    },
    preview(item) {
      this.$router.push({ path: `/entrepreneurs/stabilization/${item.id}` });
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
