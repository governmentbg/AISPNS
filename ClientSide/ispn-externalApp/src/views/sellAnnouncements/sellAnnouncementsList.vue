<template>
  <v-container fluid tag="section" class="full-height" v-if="isSyndic">
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
        color="primary" 
        fixed-tabs slider-size="3" 
        @change="tabChange" 
        v-model="currentTab"
      >
        <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key">
          {{ tab.name }}
        </v-tab>

        <v-tabs-items v-model="currentTab">
          <template v-for="tab in tabs" >
            <v-tab-item eager :value="tab.key" class="mt-5">
              <template v-if="tab.key === 'published'">
                <sell-announcements-filter ref="publishedSellAnnouncementsFilter" @doFilter="getPublishedSellAnnouncementsData" />
              </template>
              <template v-else-if="tab.key === 'sent'">
                <sell-announcements-filter ref="sentSellAnnouncementsFilter" @doFilter="getSentSellAnnouncementsData" />
              </template>
              <template v-else>
                <sell-announcements-filter ref="draftsSellAnnouncementsFilter" @doFilter="getDraftsSellAnnouncementsData" />
              </template>

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
import { mapGetters } from "vuex";
import { getSyndicFullName } from "@/utils"
import { SellAnnouncementsFilter } from "@/components"
import { apiSearchAnnouncementForCorrection, apiGetPublishedAnnouncementsBySyndics, apiGetDraftsAnnouncements } from "@/api/sellAnnouncements";
export default {
  name: "sellAnnouncementsList",
  components: {
    SellAnnouncementsFilter
  },
  data: () => ({
    currentTab: 'published',
    tabs: [
      { name: "Публикувани продажби", key: "published" },
      { name: "Изпратени за публикуване/върнати за корекция", key: "sent" },
      { name: "Чернови", key: "drafts" },
    ],
    tables: {
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
      },
      sent: {
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
    ...mapGetters(["isSyndic"]),
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
            icon: "mdi-text-box-search-outline fs18",
            color: "white",
            class: "transparent primary--text mr-1",
            click: "preview",
          },
        ],
      },
    ]

    this.tables.published.headers = headers;
    this.tables.sent.headers = headers;
    this.tables.drafts.headers = headers;

    this.getPublishedSellAnnouncementsData();
  },
  watch: {
  },
  methods: {
    getPublishedSellAnnouncementsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.publishedSellAnnouncementsFilter.filters);
      query.pageNumber = this.tables.published.pagination.skip / this.tables.published.pagination.take + 1;

      query.pageSize = this.tables.published.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.published.pagination.skip = 0;
      }

      apiGetPublishedAnnouncementsBySyndics(query).then((result) => {
        this.tables.published.data = result.items;
        this.tables.published.pagination.total = result.totalCount;
        this.tables.published.pagination.page = result.currentPage;
      });
    },
    getSentSellAnnouncementsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.sentSellAnnouncementsFilter.filters);
      query.pageNumber = this.tables.sent.pagination.skip / this.tables.sent.pagination.take + 1;

      query.pageSize = this.tables.sent.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.sent.pagination.skip = 0;
      }

      apiSearchAnnouncementForCorrection(query).then((result) => {
        this.tables.sent.data = result.items;
        this.tables.sent.pagination.total = result.totalCount;
        this.tables.sent.pagination.page = result.currentPage;
      });
    },
    getDraftsSellAnnouncementsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.draftsSellAnnouncementsFilter.filters);
      query.pageNumber = this.tables.drafts.pagination.skip / this.tables.drafts.pagination.take + 1;

      query.pageSize = this.tables.drafts.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.drafts.pagination.skip = 0;
      }

      apiGetDraftsAnnouncements(query).then((result) => {
        this.tables.drafts.data = result.items;
        this.tables.drafts.pagination.total = result.totalCount;
        this.tables.drafts.pagination.page = result.currentPage;
      });
    },
    getTablesData(){
      switch(this.currentTab){
        case "published":
          this.getPublishedSellAnnouncementsData();
        break;
        case "sent":
          this.getSentSellAnnouncementsData();
        break;
        case "drafts":
          this.getDraftsSellAnnouncementsData();
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
    renderSyndic(h, tdElement, props) {
      return h('td', null, [
        props.dataItem.syndic ? getSyndicFullName(props.dataItem.syndic) : ''
      ]);
    },
    tabChange(tabKey, force = false){      
      if((this.tables[tabKey] && !this.tables[tabKey].data.length) || force)
        switch (tabKey) {
          case "current":
            this.getPublishedSellAnnouncementsData();
          break;
          case "sent":
            this.getSentSellAnnouncementsData();
          break;
          case "drafts":
            this.getDraftsSellAnnouncementsData();
          break;
        }
    }
  },
};
</script>
