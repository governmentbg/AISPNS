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
          Ново Обявяване
        </v-btn>
      </v-col>
    </v-row>

    

    <base-material-card
      color="primary"
      icon="mdi-card-account-details-star-outline"
      inline
      class="px-5 py-5 mt-10"
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
              
              <entrepreneurs-stabilization-acts-filter :ref="tab.key + 'entrepreneursActsListFilter'" :courts="nomenclatures.courts" @doFilter="getTablesData" />
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

import { EntrepreneursStabilizationActsFilter } from "@/components";
import { apiSearchActAnnouncement } from "@/api/actAnnouncements"
import { apiMetaDataGetCourts } from "@/api/metaData";
import { eActStatuses } from "@/utils/enums/enumerators";
export default {
  name: "entrepreneursActsList",
  components: {
    EntrepreneursStabilizationActsFilter
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
          { title: "Обработен", field: "announcementStatus", sortable: false },
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
          { title: "Обработен", field: "announcementStatus", sortable: false },
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
          { title: "Обработен", field: "announcementStatus", sortable: false },
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

        query.filter = Object.assign({isStabilization: true}, this.$refs[this.currentTab+'entrepreneursActsListFilter'].filters);

        switch(this.currentTab){
          case "notProcessed":
            query.filter.announcementStatusId = eActStatuses.NOT_PROCESSED;
          break;
          case "processed":
            query.filter.announcementStatusId = eActStatuses.PROCESSED;
          break;
          case "notSubjectToAnnouncement":
            query.filter.announcementStatusId = eActStatuses.NOT_SUBJECT_TO_ANNOUCEMENT;
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
    preview(item) {
      this.$router.push({ path: `/entrepreneurs-acts/stabilization/${item.id}` });
    },
    getCourts(){
      apiMetaDataGetCourts().then((result) => {
        this.$set(this.nomenclatures, "courts", result);
      });
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
    addNew(){
      this.$router.push({path: `/entrepreneurs-acts/stabilization/create`})
    },
  },
};
</script>

