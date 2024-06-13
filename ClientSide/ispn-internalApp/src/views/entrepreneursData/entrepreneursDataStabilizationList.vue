<template>
  <v-container fluid tag="section" class="full-height">
    <base-v-component :heading="$route.meta.title" />

    <base-material-card
      color="primary"
      icon="mdi-badge-account-horizontal-outline"
      inline
      class="px-5 py-5 mt-15"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $route.name }}</div>
      </template>

      <base-material-tabs 
        v-model="currentTab"
        fixed-tabs 
        slider-size="3" 
        color="primary" 
        @change="tabChange" 
      >
        <v-tabs-slider color="secondary" />

        <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key">
          {{ tab.name }}
        </v-tab>

        <v-tabs-items v-model="currentTab">
          <template v-for="tab in tabs">
            <v-tab-item eager :value="tab.key" :key="tab.key" class="mt-5">

              <entrepreneurs-stabilization-filter :ref="tab.key + 'entrepreneursDataListFilter'" :courts="nomenclatures.courts" @doFilter="getTablesData" />
              <base-kendo-grid
                :columns="tables[tab.key].headers"
                :items="tables[tab.key].data"
                :pagination="tables[tab.key].pagination"
                @getData="getTablesData"
                @click="tableActions"
                @dblclick="preview"
                class="mt-15"
              />
            </v-tab-item>
          </template>
        </v-tabs-items>
      </base-material-tabs>
    </base-material-card>
  </v-container>
</template>

<script>
import { EntrepreneursStabilizationFilter } from "@/components";
import { apiSearchActAnnouncement } from "@/api/actAnnouncements"
import { apiMetaDataGetCourts } from "@/api/metaData";
import { eActStatuses } from "@/utils/enums/enumerators";
export default {
  name: "entrepreneursDataList",
  components: {
    EntrepreneursStabilizationFilter
  },
  data: () => ({
    currentTab: "notProcessed",
    tabs: [
      { name: "Необработени", key: "notProcessed" },
      { name: "Обработени", key: "processed" },
      { name: "Неподлежащи на обявяване", key: "notSubjectToAnnouncement" }
    ],
    nomenclatures: {
      courts: []
    },
    tables: {
      notProcessed: {
        headers: [
          { title: "Дело №", field: "caseNumber", sortable: true },
          { title: "Дело година", field: "caseYear", sortable: true },
          { title: "Съд", field: "courtName", sortable: true },
          { title: "Предприемач", field: "proprietorName", sortable: true },
          { title: "Обработен", field: "registrationStatus", sortable: false },
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
      processed: {
        headers: [
          { title: "Дело №", field: "caseNumber", sortable: true },
          { title: "Дело година", field: "caseYear", sortable: true },
          { title: "Съд", field: "courtName", sortable: true },
          { title: "Предприемач", field: "proprietorName", sortable: true },
          { title: "Обработен", field: "registrationStatus", sortable: false },
          { title: "Дата час на обработка", field: "announcedDate", sortable: false },
          { title: "Служител", field: "announceByUserName", sortable: false },
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
      notSubjectToAnnouncement: {
        headers: [
          { title: "Дело №", field: "caseNumber", sortable: true },
          { title: "Дело година", field: "caseYear", sortable: true },
          { title: "Съд", field: "courtName", sortable: true },
          { title: "Предприемач", field: "proprietorName", sortable: true },
          { title: "Обработен", field: "registrationStatus", sortable: false },
          { title: "Дата час на обработка", field: "announcedDate", sortable: false },
          { title: "Служител", field: "announceByUserName", sortable: false },
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
      }
    },
  }),
  computed: {
  },
  mounted() {
    this.getCourts();
    this.getTablesData();
  },
  watch: {
  },
  methods: {
    getTablesData() {
      this.$nextTick(() => {
        let query = Object.assign({}, {});

        query.filter = Object.assign({isStabilization: true}, this.$refs[this.currentTab+'entrepreneursDataListFilter'].filters);

        switch(this.currentTab){
          case "notProcessed":
            query.filter.registrationStatusId = eActStatuses.NOT_PROCESSED;
          break;
          case "processed":
            query.filter.registrationStatusId = eActStatuses.PROCESSED;
          break;
          case "notSubjectToAnnouncement":
            query.filter.registrationStatusId = eActStatuses.NOT_SUBJECT_TO_REGISTER;
          break;
        }
        
        query.pageNumber = this.tables[this.currentTab].pagination.skip / this.tables[this.currentTab].pagination.take + 1;

        query.pageSize = this.tables[this.currentTab].pagination.take;
        if(query.filter.page === 1){
          query.pageNumber = 1;
          this.tables[this.currentTab].pagination.skip = 0;
        }

        apiSearchActAnnouncement(query).then((result) => {
          this.tables[this.currentTab].data = result.items;
          this.tables[this.currentTab].pagination.total = result.totalCount;
          this.tables[this.currentTab].pagination.page = result.currentPage;
        });
      })
    },
    getCourts(){
      apiMetaDataGetCourts().then((result) => {
        this.$set(this.nomenclatures, "courts", result);
      });
    },
    preview(item) {
      this.$router.push({ path: `/entrepreneurs-data/stabilization/${item.id}` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
    tabChange(tabKey, force = false){      
      this.getTablesData();
    },
  },
};
</script>
