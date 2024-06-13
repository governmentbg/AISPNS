<template>
  <v-dialog v-model="open" width="80%" scrollable>
    <v-card>
      <v-card-title class="headline">
        Избор на дело
      </v-card-title>

      <v-card-text>
        <v-row>
          <v-col cols="12">
            <case-reports-filter ref="caseReportsFilter" eager @doFilter="getData" />
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12">
            <base-kendo-grid
              :columns="table.headers"
              :items="table.data"
              :pagination="table.pagination"
              sortable
              :sort.sync="table.sort"
              @getData="getData"
              @click="tableActions"
              class="mt-15"
            />
          </v-col>
        </v-row>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { apiGetCaseReports } from "@/api/cases";
import { CaseReportsFilter } from "@/components";
export default {
  name: "casesReportsModal",
  components: {
    CaseReportsFilter
  },
  props: {
    courts: {
      type: Array,
      default: () => null
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      caseId: null,
      table: {
        headers: [
          { title: "Дело Номер", field: "caseNumber", sortable: false },
          { title: "Дело Година", field: "caseYear", sortable: false },
          { title: "Дело Съд", field: "courtName", sortable: false },
          { title: "Синдик", field: "syndicName", sortable: false },
          { title: "Вид образец", field: "reportTypeName", sortable: false },
          { title: "Дата", field: "reportDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewReport" },
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
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
  },
  computed: {
  },
  methods: {
    getData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.caseReportsFilter.filters);
      query.id = this.caseId;
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
      query.pageSize = this.table.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.table.pagination.skip = 0;
      }
      
      apiGetCaseReports(query).then((result) => {
        this.table.data = result.items;
        this.table.pagination.total = result.totalCount;
        this.table.pagination.page = result.currentPage;
      });
    },
    select(item) {
      this.$emit("selected", item);
      this.open = false;
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "previewReport":
          this.previewReport(rowData);
        break;
      }
    },
    previewReport(rowData){
      this.$emit("previewReport", rowData);
    },
    openModal(id) {
      this.caseId = id;
      this.table.pagination.page = 1;
      this.table.pagination.skip = 0;
      this.table.data = [];
      this.open = true;

      setTimeout(() =>{
        if(this.open && !this.table.data.length)
          this.getData(id);
      }, 50)
    },
    closeModal(){
      this.open = false;
    },
  },
};
</script>