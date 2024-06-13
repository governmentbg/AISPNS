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
      <v-tabs 
        v-model="currentTab"
        fixed-tabs
        slider-size="3"
      >
        <v-tabs-slider color="secondary" />
        <v-tab v-for="tab in tabs" :key="`tab_${tab.key}`" :href="'#'+tab.key">
          {{ tab.name }}
        </v-tab>

        <v-tabs-items v-model="currentTab">
          <v-tab-item v-for="tab in tabs" :key="tab.key" :value="tab.key" eager class="pa-1">
            <v-card elevation="2" v-if="tab.key === 'journals'">
              <v-card-text>

                <v-row class="my-5 d-print-none">
                  <v-col cols="12" class="text-right">
                    <v-btn
                      class="primary mr-2"
                      @click="addNewJournal"
                    >
                      <v-icon left>mdi-plus</v-icon>
                      Нов дневник
                    </v-btn>
                  </v-col>
                </v-row>
                <journals-and-bankruptcy-filter ref="journalsFilter" @doFilter="getJournalsData" />

                <base-kendo-grid
                  :columns="tables.journals.headers"
                  :items="tables.journals.data"
                  :pagination="tables.journals.pagination"
                  sortable
                  :sort.sync="tables.journals.sort"
                  @getData="getJournalsData"
                  @click="tableActions"
                  @dblclick="previewJournal"
                  class="mt-15"
                />
              </v-card-text>
            </v-card>
            <v-card v-else>
              <v-card-text>
                <v-row class="my-5 d-print-none">
                  <v-col cols="12" class="text-right">
                    <v-btn
                      class="primary mr-2"
                      @click="addNewBankruptcyJournal"
                    >
                      <v-icon left>mdi-plus</v-icon>
                      Нова маса по несъстоятелност
                    </v-btn>
                  </v-col>
                </v-row>

                <journals-and-bankruptcy-filter ref="bankruptcyFilter" @doFilter="getBankruptcyData" is-bankruptcy />
                
                
                <base-kendo-grid
                  :columns="tables.bankruptcy.headers"
                  :items="tables.bankruptcy.data"
                  :pagination="tables.bankruptcy.pagination"
                  sortable
                  :sort.sync="tables.bankruptcy.sort"
                  @getData="getBankruptcyData"
                  @click="tableActions"
                  @dblclick="previewBankruptcy"
                  class="mt-15"
                />
              </v-card-text>
            </v-card>
          </v-tab-item>
        </v-tabs-items>
      </v-tabs>
    </base-material-card>
  </v-container>
</template>

<script>

import { mapGetters } from 'vuex';
import { SellAnnouncementsFilter, JournalsAndBankruptcyFilter } from "@/components"

export default {
  name: "sellAnnouncementsList",
  components: {
    SellAnnouncementsFilter,
    JournalsAndBankruptcyFilter
  },
  data: () => ({
    currentTab: 0,
    tabs: [
      { name: "Дневници", key: "journals" },
      { name: "Маси по несъстоятелност", key: "bankruptcy" },
    ],
    tables: {
      journals: {
        headers: [
          { title: "Дело", field: "", sortable: true },
          { title: "Номер", field: "", sortable: true },
          { title: "Година", field: "", sortable: true },
          { title: "Дата на последна редакция", field: "", sortable: true },
          { title: "Длъжник", field: "", sortable: false },
          { title: "Синдик", field: "", sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewJournal" },
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
      bankruptcy: {
        headers: [
          { title: "Дело", field: "", sortable: true },
          { title: "Номер", field: "", sortable: true },
          { title: "Година", field: "", sortable: true },
          { title: "Дата на последна редакция", field: "", sortable: true },
          { title: "Длъжник", field: "", sortable: false },
          { title: "Синдик", field: "", sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewJournal" },
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
    this.getJournalsData();
  },
  watch: {
    'table.sort': function(){
      this.getTablesData();
    }
  },
  methods: {
    getTablesData(){
      switch(this.tab){
        case 0:
          this.getJournalsData();
        break;
        case 1:
          this.getBankruptcyData();
        break;
      }
    },
    preview(item) {
      this.$router.push({ path: `/sell-announcements/${item.id}` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
    addNew(){
      this.$router.push({path: `/sell-announcements/create`})
    },

    tabChange(tabKey, force = false){      
      if((this.tables[tabKey] && !this.tables[tabKey].data.length) || force)
        switch (tabKey) {
          case "journals":
            this.getJournalsData();
          break;
          case "bankruptcy":
            this.getBankruptcyData();
          break;
        }
    },

    // JOURNALS
    getJournalsData(){
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.journalsFilter.filters);
      query.pageNumber = this.tables.journals.pagination.skip / this.tables.journals.pagination.take + 1;
      query.pageSize = this.tables.journals.pagination.take;

      if(this.tables.journals.sort)
        query.filter.sort = this.tables.journals.sort

      // apiGetSyndicJournals(query).then((result) => {
      //   this.tables.journals.data = result.items;
      //   this.tables.journals.pagination.total = result.totalCount;
      //   this.tables.journals.pagination.page = result.currentPage;
      // });
    },
    addNewJournal(){
      this.$router.push({path: `/journals/create`})
    },
    previewJournal(data){
      this.$refs.syndicTrainingModal.openModal(data.id)
    },


    // JOURNALS
    getBankruptcyData(){
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.bankruptcyFilter.filters);
      query.pageNumber = this.tables.bankruptcy.pagination.skip / this.tables.bankruptcy.pagination.take + 1;
      query.pageSize = this.tables.bankruptcy.pagination.take;

      if(this.tables.bankruptcy.sort)
        query.filter.sort = this.tables.bankruptcy.sort

      // apiGetSyndicBankruptcies(query).then((result) => {
      //   this.tables.bankruptcy.data = result.items;
      //   this.tables.bankruptcy.pagination.total = result.totalCount;
      //   this.tables.bankruptcy.pagination.page = result.currentPage;
      // });
    },
    addNewBankruptcyJournal(){
      this.$router.push({path: `/journals/create/bankruptcy`})
    },
    previewBankruptcy(data){
      this.$refs.syndicTrainingModal.openModal(data.id)
    },
  },
};
</script>
