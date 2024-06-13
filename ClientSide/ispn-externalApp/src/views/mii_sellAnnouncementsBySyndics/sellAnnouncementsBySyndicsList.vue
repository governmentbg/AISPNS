<template>
  <v-container fluid tag="section" class="full-height" v-if="isMEIEmployee">
    <base-v-component :heading="$route.meta.title" />


    <base-material-card
      color="primary"
      icon="mdi-clipboard-account-outline"
      inline
      class="px-5 py-5 mt-15"
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
              <template v-if="tab.key === 'notProcessed'">
                <sell-announcements-filter ref="notProcessedSellAnnouncementsFilter" @doFilter="getNotProcessedSellAnnouncementsData" />
              </template>
              <template v-else-if="tab.key === 'onHold'">
                <sell-announcements-filter ref="onHoldSellAnnouncementsFilter" @doFilter="getOnHoldSellAnnouncementsData" />
              </template>
              <template v-else>
                <sell-announcements-filter ref="publishedSellAnnouncementsFilter" @doFilter="getPublishedsSellAnnouncementsData" />
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
import { getSyndicFullName } from '@/utils';
import { SellAnnouncementsFilter } from "@/components"
import { apiGetNewAnnouncementsBySyndics, apiGetOnHoldAnnouncementsBySyndics, apiGetPublishedAnnouncementsBySyndics } from "@/api/sellAnnouncements"

export default {
  name: "sellAnnouncementsList",
  components: {
    SellAnnouncementsFilter
  },
  data: () => ({
    currentTab: 'notProcessed',
    tabs: [
      { name: "Необработени", key: "notProcessed" },
      { name: "Чакащи", key: "onHold" },
      { name: "Публикувани", key: "published" },
    ],
    tables: {
      notProcessed: {
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
      onHold: {
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
      published: {
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

    this.tables.notProcessed.headers = headers;
    this.tables.onHold.headers = headers;
    this.tables.published.headers = headers;

    this.getNotProcessedSellAnnouncementsData();
  },
  watch: {},
  methods: {
    getNotProcessedSellAnnouncementsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.notProcessedSellAnnouncementsFilter.filters);
      
      query.pageNumber = this.tables.notProcessed.pagination.skip / this.tables.notProcessed.pagination.take + 1;

      query.pageSize = this.tables.notProcessed.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.notProcessed.pagination.skip = 0;
      }

      // if(this.tables.notProcessed.sort)
      //   query.filter.sort = this.tables.notProcessed.sort

      apiGetNewAnnouncementsBySyndics(query).then((result) => {
        this.tables.notProcessed.data = result.items;
        this.tables.notProcessed.pagination.total = result.totalCount;
        this.tables.notProcessed.pagination.page = result.currentPage;
      });
    },
    getOnHoldSellAnnouncementsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.onHoldSellAnnouncementsFilter.filters);
      query.pageNumber = this.tables.onHold.pagination.skip / this.tables.onHold.pagination.take + 1;

      query.pageSize = this.tables.onHold.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.onHold.pagination.skip = 0;
      }

      // if(this.tables.onHold.sort)
      //   query.filter.sort = this.tables.onHold.sort

      apiGetOnHoldAnnouncementsBySyndics(query).then((result) => {
        this.tables.onHold.data = result.items;
        this.tables.onHold.pagination.total = result.totalCount;
        this.tables.onHold.pagination.page = result.currentPage;
      });
    },
    getPublishedsSellAnnouncementsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.publishedSellAnnouncementsFilter.filters);
      
      query.pageNumber = this.tables.published.pagination.skip / this.tables.published.pagination.take + 1;

      query.pageSize = this.tables.published.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.published.pagination.skip = 0;
      }

      // if(this.tables.published.sort)
      //   query.filter.sort = this.tables.published.sort

      apiGetPublishedAnnouncementsBySyndics(query).then((result) => {
        this.tables.published.data = result.items;
        this.tables.published.pagination.total = result.totalCount;
        this.tables.published.pagination.page = result.currentPage;
      });
    },
    getTablesData(){
      switch(this.currentTab){
        case "notProcessed":
          this.getNotProcessedSellAnnouncementsData();
        break;
        case "onHold":
          this.getOnHoldSellAnnouncementsData();
        break;
        case "published":
          this.getPublishedsSellAnnouncementsData();
        break;
      }
    },
    preview(item) {
      this.$router.push({ path: `/mii-sell-announcements-by-syndics/${item.id}` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
    renderSyndic(h, tdElement, props) {
      return h('td', null, [
        props.dataItem.syndic ? getSyndicFullName(props.dataItem.syndic) : ''
      ]);
    },
    tabChange(tabKey, force = false){      
      if((this.tables[tabKey] && !this.tables[tabKey].data.length) || force)
        switch (tabKey) {
          case "notProcessed":
            this.getNotProcessedSellAnnouncementsData();
          break;
          case "onHold":
            this.getOnHoldSellAnnouncementsData();
          break;
          case "published":
            this.getPublishedsSellAnnouncementsData();
          break;
        }
    }
  },
};
</script>
