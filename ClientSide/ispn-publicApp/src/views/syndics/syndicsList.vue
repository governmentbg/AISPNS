<template>
  <v-container fluid tag="section" class="full-height">

    <syndics-list-filter ref="syndicsListFilter" @doFilter="getData" class="mt-5" />

    <base-material-card
      color="primary"
      icon="mdi-account-tie"
      inline
      class="px-5 py-5 mt-15"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $t('syndics') }}</div>
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
import { apiGetSyndics } from "@/api/syndics";
import { SyndicsListFilter } from "@/components";

export default {
  name: "syndicsList",
  components: {
    SyndicsListFilter,
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
      { text: this.$t('name'), value: "fullName", valueType: 'text', sortable: false },
      { text: this.$t('speciality'), value: "syndicSpecialty", valueType: 'text',  sortable: false },
      { text: this.$t('phone'), value: "phone", valueType: 'text',  sortable: false },
      { text: this.$t('address'), value: 'fullAddress', valueType: 'text',  sortable: false },
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
      query.filter = Object.assign({}, this.$refs.syndicsListFilter.filters);
      query.pageNumber = this.table.pagination.page;
      query.pageSize = this.table.pagination.itemsPerPage;

      apiGetSyndics(query).then((result) => {
        this.table.data = result.items;
        this.table.pagination.total = result.totalCount;
        this.table.pagination.page = result.currentPage;
      });
    },
    preview(item) {
      this.$router.push({ path: `/syndics/${item.id}` });
    },
    renderAddresses(h, tdElement , props ) {
      let item = props.dataItem;
      let result = '';
      if(item.addresses){
        for(let addr of item.addresses){
          console.log(addr);
          result += addr.address + '<br />';
        }
      }

      if(!result.length)
        result = ' - '

      return h('td', null, [
        result
      ]);
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
