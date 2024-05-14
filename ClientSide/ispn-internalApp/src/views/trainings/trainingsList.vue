<template>
  <v-container fluid tag="section" class="full-height">
    <base-v-component :heading="$route.name" />

    <v-row class="my-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          class="primary"
          @click="addNew"
        >
          <v-icon left dark>mdi-plus</v-icon>
          Нова програма
        </v-btn>
      </v-col>
    </v-row>

    <base-material-card
      color="primary"
      icon="mdi-school-outline"
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
import { apiGetPrograms } from "@/api/programs";
export default {
  name: "trainingsList",
  components: {},
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
      { title: "От дата", field: "fromDate", type: "date", format: "{0:dd.MM.yyyy}", sortable: false, width: '90px' },
      { title: "До дата", field: "toDate", type: "date", format: "{0:dd.MM.yyyy}", sortable: false, width: '90px' },
      { title: "Заповед № МП", field: "mojorderNumber", sortable: false, width: '90px' },
      { title: "Заповед дата МП", field: "mojorderDate", type: "date", format: "{0:dd.MM.yyyy}", sortable: false, width: '90px' },
      { title: "Заповед № МИЕТ", field: "moeorderNumber", sortable: false, width: '90px' },
      { title: "Заповед дата МИЕТ", field: "moeorderDate", type: "date", format: "{0:dd.MM.yyyy}", sortable: false, width: '100px' },
      { title: "Обяснения", field: "additionalDescription", cell: this.renderAdditionalDescription, sortable: false },
      { title: "Забележки", field: "notes", cell: this.renderNotes, sortable: false },
      { title: "Описание", field: "description", cell: this.renderDescription, sortable: false },
      {
        title: "",
        cell: "actions",
        filterable: false,
        width: "45px",
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
    ];
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
      
      query.filter = {};
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
      query.pageSize = this.table.pagination.take;

      apiGetPrograms(query).then((result) => {
        this.table.data = result.items;
        this.table.pagination.total = result.totalCount;
        this.table.pagination.page = result.currentPage;
      });
    },
    preview(item) {
      this.$router.push({ path: `/trainings/${item.id}` });
    },
    renderDescription(h, tdElement, props) {
      let item = props.dataItem;
      return h("td", item.description && item.description.length > 100 ? item.description.slice(0, 100)+'...' : item.description);
    },
    renderAdditionalDescription(h, tdElement, props) {
      let item = props.dataItem;
      return h("td", item.additionalDescription && item.additionalDescription.length > 100 ? item.additionalDescription.slice(0, 100)+'...' : item.additionalDescription);
    },
    renderNotes(h, tdElement, props) {
      let item = props.dataItem;
      return h("td", item.notes && item.notes.length > 100 ? item.notes.slice(0, 100)+'...' : item.notes);
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
    addNew(){
      this.$router.push({path: `/trainings/create`})
    },
  },
};
</script>
