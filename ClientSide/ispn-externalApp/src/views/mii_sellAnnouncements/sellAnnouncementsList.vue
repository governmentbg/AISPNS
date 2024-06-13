<template>
  <v-container fluid tag="section" class="full-height" v-if="isMEIEmployee">
    <base-v-component :heading="$route.meta.title" />

    <v-row class="my-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          class="primary"
          @click="addNew"
        >
          <v-icon left dark>mdi-plus</v-icon>
          Нова обява
        </v-btn>
      </v-col>
    </v-row>

    <base-material-card
      color="primary"
      icon="mdi-bullhorn-outline"
      inline
      class="px-5 py-5 mt-10"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $route.name }}</div>
      </template>

      <base-material-tabs 
        v-model="currentTab"
        color="primary" 
        fixed-tabs 
        slider-size="3" 
        @change="tabChange" 
      >
        <v-tabs-slider color="secondary" />
        <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key">
          {{ tab.name }}
        </v-tab>

        <v-tabs-items v-model="currentTab">
          <template v-for="tab in tabs" >
            <v-tab-item eager :value="tab.key" class="mt-5">
              <template v-if="tab.key === 'upcoming'">
                <sell-announcements-filter ref="upcomingSellAnnouncementsFilter" @doFilter="getUpcomingSellAnnouncementsData" />
              </template>
              <template v-else-if="tab.key === 'drafts'">
                <sell-announcements-filter ref="draftsSellAnnouncementsFilter" @doFilter="getDraftsSellAnnouncementsData" />
              </template>
              <template v-else>
                <sell-announcements-filter ref="endedSellAnnouncementsFilter" @doFilter="getEndedSellAnnouncementsData" />
              </template>

              <base-kendo-grid
                :columns="tables[tab.key].headers"
                :items="tables[tab.key].data"
                :pagination="tables[tab.key].pagination"
                sortable
                :sort.sync="tables[tab.key].sort"
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

import { mapGetters } from 'vuex';
import { SellAnnouncementsFilter } from "@/components"
import { getSyndicFullName } from '@/utils'
import { apiGetUpcomingAnnouncements, apiGetDraftsAnnouncements, apiGetFinishedAnnouncements } from "@/api/sellAnnouncements";
export default {
  name: "sellAnnouncementsList",
  components: {
    SellAnnouncementsFilter
  },
  data: () => ({
    currentTab: 'upcoming',
    tabs: [
      { name: "Предстоящи продажби", key: "upcoming" },
      { name: "Чернови", key: "drafts" },
      { name: "Приключили продажби", key: "ended" },
    ],
    tables: {
      upcoming: {
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
      ended: {
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
      drafts: {
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
      }
    }
  }),
  computed: {
    ...mapGetters([
      'isMEIEmployee',
      'currentUser'
    ]),
  },
  mounted() {
    const headers = [
      { title: "Дата", field: "offeringDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false, width: '100px' },
      { title: "Длъжник", field: 'debtorName', sortable: false },
      { title: "Място", field: "fullAddress", sortable: false },
      { title: "Синдик", field: '', cell: this.renderSyndic, sortable: false },
      { title: "Дело №", field: "caseNumber", sortable: false, width: '100px' },
      { title: "Година", field: "caseYear", sortable: false, width: '80px' },
      { title: "Съд", field: "courtName", sortable: false },
      { title: "Статус", field: "status", sortable: false, width: '110px' },
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
    this.tables.upcoming.headers = headers;
    this.tables.drafts.headers = headers;
    this.tables.ended.headers = headers;
    this.getUpcomingSellAnnouncementsData();
  },
  watch: {},
  methods: {
    getUpcomingSellAnnouncementsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.upcomingSellAnnouncementsFilter[0].filters);
      query.pageNumber = this.tables.upcoming.pagination.skip / this.tables.upcoming.pagination.take + 1;

      query.pageSize = this.tables.upcoming.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.upcoming.pagination.skip = 0;
      }

      // if(this.tables.upcoming.sort)
      //   query.filters.sort = this.tables.upcoming.sort

      apiGetUpcomingAnnouncements(query).then((result) => {
        this.tables.upcoming.data = result.items;
        this.tables.upcoming.pagination.total = result.totalCount;
        this.tables.upcoming.pagination.page = result.currentPage;
      });
    },
    getDraftsSellAnnouncementsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.draftsSellAnnouncementsFilter[0].filters);
      query.pageNumber = this.tables.drafts.pagination.skip / this.tables.drafts.pagination.take + 1;

      query.pageSize = this.tables.drafts.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.drafts.pagination.skip = 0;
      }

      // if(this.tables.drafts.sort)
      //   query.filter.sort = this.tables.drafts.sort

      apiGetDraftsAnnouncements(query).then((result) => {
        this.tables.drafts.data = result.items;
        this.tables.drafts.pagination.total = result.totalCount;
        this.tables.drafts.pagination.page = result.currentPage;
      });
    },
    getEndedSellAnnouncementsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.endedSellAnnouncementsFilter[0].filters);
      
      query.pageNumber = this.tables.ended.pagination.skip / this.tables.ended.pagination.take + 1;

      query.pageSize = this.tables.ended.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.ended.pagination.skip = 0;
      }

      if(this.tables.ended.sort)
        query.filter.sort = this.tables.ended.sort

      apiGetFinishedAnnouncements(query).then((result) => {
        this.tables.ended.data = result.items;
        this.tables.ended.pagination.total = result.totalCount;
        this.tables.ended.pagination.page = result.currentPage;
      });
    },
    getTablesData(){
      switch(this.currentTab){
        case 'upcoming':
          this.getUpcomingSellAnnouncementsData();
        break;
        case 'drafts':
          this.getDraftsSellAnnouncementsData();
        break;
        case 'ended':
          this.getEndedSellAnnouncementsData();
        break;
      }
    },
    renderSyndic(h, tdElement, props) {
      return h('td', null, [
        props.dataItem.syndic ? getSyndicFullName(props.dataItem.syndic) : ''
      ]);
    },
    preview(item) {
      this.$router.push({ path: `/mii-sell-announcements/${item.id}` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
    addNew(){
      this.$router.push({path: `/mii-sell-announcements/create`})
    },
    tabChange(tabKey, force = false){      
      if((this.tables[tabKey] && !this.tables[tabKey].data.length) || force)
        switch (tabKey) {
          case "upcoming":
            this.getUpcomingSellAnnouncementsData();
          break;
          case "ended":
            this.getEndedSellAnnouncementsData();
          break;
          case "drafts":
            this.getDraftsSellAnnouncementsData();
          break;
        }
    }
  },
};
</script>
