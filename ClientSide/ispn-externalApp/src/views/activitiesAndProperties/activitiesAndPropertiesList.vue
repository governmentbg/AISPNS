<template>
  <v-container fluid tag="section" class="full-height" v-if="isSyndic">
    <base-v-component :heading="$route.meta.title" />

    

    <base-material-card 
      icon="mdi-notebook"
      color="primary"
      class="elevation-3 mt-10"
    >
      <template v-slot:after-heading>
        Дневници
      </template>

      <base-material-tabs 
        v-model="currentTab"
        color="primary" 
        fixed-tabs 
        slider-size="3" 
        @change="tabChange" 
      >
        <v-tab v-for="tab in tabs" :key="`tab_${tab.key}`" :href="'#'+tab.key">
          {{ tab.name }}
        </v-tab>

        <v-tabs-items v-model="currentTab">
          <v-tab-item v-for="tab in tabs" :key="tab.key" :value="tab.key" eager class="pa-1 mt-5">
            <template v-if="tab.key === 'activities'">
                <activities-and-property-filter ref="activitiesFilter" :courts="nomenclatures.courts" @doFilter="getActivitiesData" />

                <base-kendo-grid
                  :columns="tables.activities.headers"
                  :items="tables.activities.data"
                  :pagination="tables.activities.pagination"
                  sortable
                  :sort.sync="tables.activities.sort"
                  @getData="getActivitiesData"
                  @click="tableActions"
                  @dblclick="previewJournal"
                  class="mt-15"
                />
            </template>
            <template v-else>
              <activities-and-property-filter ref="propertyFilter" @doFilter="getPropertyData" :courts="nomenclatures.courts" is-property />
              
              <base-kendo-grid
                :columns="tables.property.headers"
                :items="tables.property.data"
                :pagination="tables.property.pagination"
                sortable
                :sort.sync="tables.property.sort"
                @getData="getPropertyData"
                @click="tableActions"
                class="mt-15"
              />
            </template>
          </v-tab-item>
        </v-tabs-items>
      </base-material-tabs>
    </base-material-card>
  </v-container>
</template>

<script>

import { mapGetters } from 'vuex';
import { ActivitiesAndPropertyFilter } from "@/components"
import { apiMetaDataGetCourts } from '@/api/metaData';
import { apiGetCasesBySyndic } from "@/api/cases";
export default {
  name: "sellAnnouncementsList",
  components: {
    ActivitiesAndPropertyFilter
  },
  data: () => ({
    currentTab: 'activities',
    tabs: [
      { name: "Дневници", key: "activities" },
      { name: "Маси по несъстоятелност", key: "property" },
    ],
    nomenclatures: {
      courts: [],
    },
    tables: {
      activities: {
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
              { title: "Преглед", icon: "mdi-text-box-search-outline fs18", color: "white", class: "transparent primary--text mr-1", click: "previewActivity" },
            ],
          },
        ],
        data: null,
        sort: {},
        pagination: {
          page: 1,
          skip: 0,
          take: 20,
          perPageOptions: [5, 10, 20, 50, 100],
          total: 0,
        },
      },
      property: {
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
              { title: "Преглед", icon: "mdi-text-box-search-outline fs18", color: "white", class: "transparent primary--text mr-1", click: "previewProperty" },
            ],
          },
        ],
        data: null,
        sort: {},
        pagination: {
          page: 1,
          skip: 0,
          take: 20,
          perPageOptions: [5, 10, 20, 50, 100],
          total: 0,
        },
      },
    }
  }),
  computed: {
    ...mapGetters([
      'isSyndic'
    ]),
  },
  mounted() {
    this.getCourts();
    this.getActivitiesData();
  },
  watch: {
  },
  methods: {
    getTablesData(){
      switch(this.tab){
        case 'activities':
          this.getActivitiesData();
        break;
        case 'property':
          this.getPropertyData();
        break;
      }
    },
    getCourts(){
      apiMetaDataGetCourts().then((response) => {
        this.nomenclatures.courts = response;
      });
    },
    previewActivity(item) {
      this.$router.push({ path: `/activities/${item.id}` });
    },
    previewProperty(item) {
      this.$router.push({ path: `/activities/${item.id}/property` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "previewActivity":
          this.previewActivity(rowData);
        break;
        case "previewProperty":
          this.previewProperty(rowData);
        break;
      }
    },
    tabChange(tabKey, force = false){
      if((this.tables[tabKey] && this.tables[tabKey].data === null) || force)
        switch (tabKey) {
          case "activities":
            this.getActivitiesData();
          break;
          case "property":
            this.getPropertyData();
          break;
        }
    },

    // ACTIVITIES
    getActivitiesData(){
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.activitiesFilter[0].filters);
      query.pageNumber = this.tables.activities.pagination.skip / this.tables.activities.pagination.take + 1;
      query.pageSize = this.tables.activities.pagination.take;

      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.activities.pagination.skip = 0;
      }

      apiGetCasesBySyndic(query).then((result) => {
        this.tables.activities.data = result.items;
        this.tables.activities.pagination.total = result.totalCount;
        this.tables.activities.pagination.page = result.currentPage;
      });
    },
    previewJournal(data){
      this.$refs.syndicTrainingModal.openModal(data.id)
    },


    // PROPERTIES
    getPropertyData(){
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.propertyFilter[0].filters);
      query.pageNumber = this.tables.property.pagination.skip / this.tables.property.pagination.take + 1;
      query.pageSize = this.tables.property.pagination.take;

      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.property.pagination.skip = 0;
      }

      apiGetCasesBySyndic(query).then((result) => {
        this.tables.property.data = result.items;
        this.tables.property.pagination.total = result.totalCount;
        this.tables.property.pagination.page = result.currentPage;
      });
    },
  },
};
</script>
