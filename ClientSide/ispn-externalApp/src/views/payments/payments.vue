<template>
  <v-container fluid tag="section" class="full-height" v-if="isSyndic">

    <base-v-component :heading="$route.meta.title" />

    <v-row class="my-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          class="primary"
          @click="newPayment"
        >
          <v-icon left dark>mdi-plus</v-icon>
          Нова вноска
        </v-btn>
      </v-col>
    </v-row>

    <base-material-card
      color="primary"
      icon="mdi-cash-multiple"
      inline
      class="px-5 py-5 mt-10"
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
import { mapGetters } from 'vuex';
import { apiGetSyndicInstallments } from "@/api/installments"
export default {
  name: "paymentsList",
  components: {
  },
  data: () => ({
    table: {
      headers: [
        { title: "За година", field: "installmentYear", sortable: false },
        { title: "Номер на пл. документ", field: "number", sortable: false },
        { title: "Дата на пл. документ", field: "paymentDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false },
        { title: "Банка", field: "bank", sortable: false },
        { title: "Проверено плащане", field: "verified", cell: 'status', className: 'text-center', sortable: false },
        { title: "Сума", field: "amount", cell: 'amount', sortable: false },
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
      },
    },
  }),
  computed: {
    ...mapGetters(["isSyndic"]),
  },
  mounted() {
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
      
      query.filters = {};
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
      query.pageSize = this.table.pagination.take;
      
      apiGetSyndicInstallments(query).then((result) => {
        this.table.data = result.items;
        this.table.pagination.total = result.totalCount;
        this.table.pagination.page = result.currentPage;
      });
    },
    newPayment(){
      this.$router.push({path: '/payments/create'})
    },
    preview(item) {
      this.$router.push({ path: `/payments/${item.id}` });
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
