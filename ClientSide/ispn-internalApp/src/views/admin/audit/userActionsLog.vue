<template>
  <v-container id="userActionsLog" fluid tag="section" class="full-height" v-if="isAdministrator">
    <base-v-component :heading="$route.meta.title" />

    <user-actions-log-filter
      ref="userActionsLogFilter"
      @doFilter="getData"
      :userActionTypes="nomenclatures.userActionTypes"
      class="mt-15"
    />

    <base-material-card
      hover-reveal
      color="primary"
      icon="mdi-target-account"
      inline
      class="px-5 py-5 mt-15"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $route.name }}</div>
      </template>

      <base-kendo-grid
        :columns="table.headers"
        :items="table.data"
        :pagination="table.pagination"
        @export="exportToExcel"
        @getData="getData"
      />
    </base-material-card>
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import { UserActionsLogFilter } from "@/components";
import { apiGetUserActions } from "@/api/administration";

export default {
  name: "userActionsLog",
  components: {
    UserActionsLogFilter,
  },
  data: () => ({
    table: {
      headers: [],
      data: [],
      pagination: {
        skip: 0,
        take: 20,
        perPageOptions: [5, 10, 20, 50, 100],
        total: 0,
      },
    },
    nomenclatures: {
      userActionTypes: [],
      userActionTypeByName: {},
    },
  }),
  computed: {
    ...mapGetters([
      'isAdministrator'
    ])
  },
  mounted() {
    this.table.headers = [
      { title: "Дата", field: "timestamp", type: "date", format: "{0:dd.MM.yyyyг. HH:mmч.}", width: "150px" },
      { title: "Действие", field: "userActionType", cell: this.getUserActionType },
      { title: "Съобщение", field: "message" },
      { title: "Потребител", field: "userName" },
      { title: "IP адрес", field: "ipAddress" },
    ];

    for (let userAcitonType of this.nomenclatures.userActionTypes) {
      this.nomenclatures.userActionTypeByName[userAcitonType.value] = userAcitonType;
    }

    this.getData();
  },
  methods: {
    getData() {
      let query = {};
      query.filter = Object.assign({}, this.$refs.userActionsLogFilter.filters);

      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;

      query.pageSize = this.table.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.table.pagination.skip = 0;
      }

      apiGetUserActions(query).then((result) => {
        this.table.data = result.items;
        this.table.pagination.total = result.totalCount;
      });
    },
    getUserActionType(h, tdElement, props) {
      return h("td", null, [
        this.getActionTypeTranslated(props.dataItem.userActionType),
      ]);
    },
    getActionTypeTranslated(actionType) {
      if (Object.prototype.hasOwnProperty.call(this.nomenclatures.userActionTypeByName, actionType))
        return this.nomenclatures.userActionTypeByName[actionType].name;

      return actionType;
    },
    exportToExcel() {
      let query = Object.assign({}, {});
      query = Object.assign({}, this.$refs.userActionsLogFilter.filters);
      query.headerRename = this.table.headers;

      const endpoint = "api/Audit/ExportUserActionLogsToExcel";
      //const response = apiExportDocumentToExcelAu(endpoint, "xlsx", query);
      //return response;

    },
  },
};
</script>
