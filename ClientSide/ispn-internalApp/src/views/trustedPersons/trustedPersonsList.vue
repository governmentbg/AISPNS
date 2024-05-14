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
          Ново доверено лице
        </v-btn>
      </v-col>
    </v-row>

    <trusted-persons-filter ref="trustedPersonsFilter" @doFilter="getData" />

    <base-material-card
      color="primary"
      icon="mdi-account-star"
      inline
      class="px-5 py-5 mt-10"
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

    <search-trusted-person-by-e-g-n-modal ref="searchTrustedPersonByEGNModal" />
  </v-container>
</template>

<script>
import { apiSearchSyndics } from "@/api/syndics";
import { TrustedPersonsFilter, SearchTrustedPersonByEGNModal } from "@/components"
export default {
  name: "trustedPersonsList",
  components: {
    TrustedPersonsFilter
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
      { title: "Име", field: "firstName", cell: this.renderSyndicName, sortable: false },
      { title: "ЕГН", field: "egn", sortable: false },
      { title: "Адрес", field: 'fullAddress', sortable: false, width: '250px' },
      { title: "Специалност", field: "syndicSpecialty", sortable: false },
      { title: "Статус на синдика", field: "syndicStatus", sortable: false },
      { title: "Утвърден със заповед", field: "orders", cell: this.renderSyndicOrderDescription, sortable: false },
      { title: "Активен", field: "active", cell: 'status', sortable: false, width: '80px', className: 'text-center', headerClassName: 'text-center' },
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
    },
    '$vuetify.breakpoint': {
      handler(breakpoint){
        if(breakpoint.mdAndDown){
          this.table.headers = [
            { title: "Име", field: "firstName", cell: this.renderSyndicName, sortable: false },
            { title: "ЕГН", field: "egn", sortable: false },
            { title: "Адрес", field: 'fullAddress', sortable: false, width: '250px' },
            { title: "Активен", field: "active", cell: 'status', sortable: false, width: '80px', className: 'text-center', headerClassName: 'text-center' },
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
        } else if(breakpoint.lgAndUp){
          this.table.headers = [
            { title: "Име", field: "firstName", cell: this.renderSyndicName, sortable: false },
            { title: "ЕГН", field: "egn", sortable: false },
            { title: "Адрес", field: 'fullAddress', sortable: false, width: '250px' },
            { title: "Специалност", field: "syndicSpecialty", sortable: false },
            { title: "Статус на синдика", field: "syndicStatus", sortable: false },
            { title: "Заповед с която е утвърден", field: "orders", cell: this.renderSyndicOrderDescription, sortable: false },
            { title: "Активен", field: "active", cell: 'status', sortable: false, width: '80px', className: 'text-center', headerClassName: 'text-center' },
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
        }
      },
      deep: true
    }
  },
  methods: {
    getData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.trustedPersonsFilter.filters);
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
      query.pageSize = this.table.pagination.take;


      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.table.pagination.skip = 0;
      }

      apiSearchSyndics(query).then((result) => {
        this.table.data = result.items;
        this.table.pagination.total = result.totalCount;
        this.table.pagination.page = result.currentPage;
      });
    },
    preview(item) {
      this.$router.push({ path: `/trusted-persons/${item.id}` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
    renderSyndicName(h, tdElement , props ) {
      let item = props.dataItem;
      let result = '';
      if(item.firstName)
        result = item.firstName

      if(item.secondName)
          result += (result.length ? ' ' : '') + item.secondName;

      if(item.lastName)
        result += (result.length ? ' ' : '') + item.lastName;


        return h('td', null, [
          result
        ]);
    },
    renderSyndicAddress(h, tdElement , props ) {
      let item = props.dataItem;
      let result = '';
      if(item.addresses)
        for(let addr of item.addresses){
          result += addr.address;
        }


        return h('td', null, [
          result
        ]);
    },
    renderSyndicOrderDescription(h, tdElement , props ) {
      let item = props.dataItem;
      let result = '';
      if(item.orders?.length)
        result = item.orders[0].description


      return h('td', null, [
        result
      ]);
    },
    renderSyndicOrderNumber(h, tdElement , props ) {
      let item = props.dataItem;
      let result = '';
      if(item.orders?.length)
        result = item.orders[0].number


      return h('td', null, [
        result
      ]);
    },
    renderSyndicOrderStateEdition(h, tdElement , props ) {
      let item = props.dataItem;
      let result = '';
      if(item.orders?.length)
        result = item.orders[0].stateGazetteEdition


      return h('td', null, [
        result
      ]);
    },
    renderSyndicOrderStateYear(h, tdElement , props ) {
      let item = props.dataItem;
      let result = '';
      if(item.orders?.length)
        result = item.orders[0].stateGazetteYear


      return h('td', null, [
        result
      ]);
    },
    addNew(){
      //this.$router.push({path: `/trusted-persons/create`})
      this.$refs.searchTrustedPersonByEGNModal.openModal();
    },
  },
};
</script>
