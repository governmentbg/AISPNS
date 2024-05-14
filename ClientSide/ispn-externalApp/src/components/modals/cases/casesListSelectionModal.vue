<template>
  <v-dialog v-model="open" width="80%" scrollable>
    <v-card>
      <v-card-title class="headline">
        Избор на дело
      </v-card-title>

      <v-card-text>
        <v-row>
          <v-col cols="12">
            <cases-list-filter ref="casesListFilter" eager @doFilter="getData" />
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
              @dblclick="select"
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
import { CasesListFilter } from "@/components";
import { apiGetCases, apiGetCasesBySyndic } from "@/api/cases";
export default {
  name: "casesListSelectionModal",
  components: {
    CasesListFilter,
  },
  props: {
    isSyndic: {
      type: Boolean,
      default: false,
    },
  },
  mounted() {},
  data() {
    return {
      open: false,
      table: {
        headers: [
          { title: "№", field: "number", sortable: false, width: '80px' },
          { title: "Година", field: "year", sortable: false, width: '80px' },
          { title: "Дата", field: "formationDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false, width: '100px' },
          { title: "Съд", field: "court.name", sortable: true },
          { title: "Вид", field: "caseKindDescription", sortable: true },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              {
                title: "Избери",
                icon: "mdi-check",
                color: "white",
                class: "primary",
                click: "select",
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
      
      query.filter = Object.assign({}, this.$refs.casesListFilter.filters);
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
      query.pageSize = this.table.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.table.pagination.skip = 0;
      }

      if(this.isSyndic){
        apiGetCasesBySyndic(query).then((result) => {
          this.table.data = result.items;
          this.table.pagination.total = result.totalCount;
          this.table.pagination.page = result.currentPage;
        });
      } else {
        apiGetCases(query).then((result) => {
          this.table.data = result.items;
          this.table.pagination.total = result.totalCount;
          this.table.pagination.page = result.currentPage;
        });
      }
    },
    openModal() {
      this.open = true;

      setTimeout(() =>{
        if(this.open && !this.table.data.length)
          this.getData();
      }, 50)
    },
    select(item) {
      this.$emit("selected", item);
      this.open = false;
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "select":
          this.select(rowData);
        break;
      }
    },
    closeModal(){
      this.open = false;
    },
  },
};
</script>