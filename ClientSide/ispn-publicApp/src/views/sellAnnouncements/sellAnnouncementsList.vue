<template>
  <v-container fluid tag="section" class="full-height">
    <v-row class="my-5">
      <v-col cols="12" class="mx-auto text-right">
        <v-btn
          color="primary"
          @click="goToContacts"
        >
          {{ $t('contacts') }}
        </v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <base-material-card
          color="primary"
          icon="mdi-bullhorn-outline"
          inline
          class="px-5 py-5 "
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">{{ $t('sell_announcements') }}</div>
          </template>

          <v-tabs 
            v-model="currentTab"
            fixed-tabs
            slider-size="3"
            @change="tabChange"
          >
            <v-tabs-slider color="secondary" />
            <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key">
              {{ tab.name }}
            </v-tab>

            <v-tabs-items v-model="currentTab">
              <v-tab-item eager value="upcoming">
                <sell-announcements-list-filter ref="upcomingSellAnnouncementsFilter" :courts="nomenclatures.courts" :object-kinds="nomenclatures.objectKinds" @doFilter="getUpcomingSellAnnouncementsData" />

                <base-data-table
                  :headers="tables.upcoming.headers"
                  :pagination="tables.upcoming.pagination"
                  :data="tables.upcoming.data"
                  @getData="getUpcomingSellAnnouncementsData"
                  @doAction="tableActions"
                  class="mt-15 px-1"
                />
              </v-tab-item>
              <v-tab-item eager value="ended">
                <sell-announcements-list-filter ref="endedSellAnnouncementsFilter" :courts="nomenclatures.courts" :object-kinds="nomenclatures.objectKinds" @doFilter="getEndedSellAnnouncementsData" />

                <base-data-table
                  :headers="tables.ended.headers"
                  :pagination="tables.ended.pagination"
                  :data="tables.ended.data"
                  @getData="getEndedSellAnnouncementsData"
                  @doAction="tableActions"
                  class="mt-15 px-1"
                />
              </v-tab-item>
            </v-tabs-items>
          </v-tabs>

          
        </base-material-card>
        
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { getSyndicFullName } from "@/utils"
import { SellAnnouncementsListFilter } from "@/components"
import { apiGetCurrentAnnouncementsList, apiGetFinishedAnnouncementsList } from "@/api/announcements"
import { apiMetaDataGetCourtsList, apiMetaDataGetObjectKind } from "@/api/metaData";

export default {
  name: "sellAnnouncementsList",
  components: {
    SellAnnouncementsListFilter
  },
  data: () => ({
    nomenclatures: {
      courts: [],
      objectKinds: []
    },
    currentTab: 'upcoming',
    tabs: [],
    tables: {
      upcoming: {
        headers: [],
        data: [],
        sort: {},
        pagination: {
          itemsPerPage: 15,
          page: 1,
          perPageOptions: [5, 15, 25, 50, 100],
          total: 0
        },
      },
      ended: {
        headers: [],
        data: [],
        sort: {},
        pagination: {
          itemsPerPage: 15,
          page: 1,
          perPageOptions: [5, 15, 25, 50, 100],
          total: 0
        },
      }
    },
  }),
  computed: {
  },
  mounted() {
    this.tabs = [
      { name: this.$t('upcoming_sales'), key: "upcoming" },
      { name: this.$t('ended_sales'), key: "ended" },
    ]
    const headers = [
      { text: this.$t('date'), value: "offeringDate", valueType: 'date', sortable: false },
      { text: this.$t('debtor'), value: "debtorName", valueType: 'text', sortable: false },
      { text: this.$t('place'), value: "fullAddress", valueType: 'text', sortable: false },
      { text: this.$t('syndic'), value: "syndicName", valueType: 'text', sortable: false },
      { text: this.$t('case_number'), value: "caseNumber", valueType: 'text', sortable: false },
      { text: this.$t('year'), value: "caseYear", valueType: 'text', sortable: false },
      { text: this.$t('court'), value: "courtName", valueType: 'text', sortable: false },
      { text: this.$t('date_published'), value: 'publishDate', valueType: 'date', sortable: false},
      { text: this.$t('status'), value: 'status', valueType: 'text', sortable: false},
      {
        sortable: false,
        text: '',
        value: '',
        valueType: 'button',
        style: "width: 50px;",
        class: 'text-right',
        buttons: [
          { label: '', title: this.$t('preview'), icon: 'mdi-text-box-search-outline', size: 'small', class: 'transparent primary--text', click: 'preview' }
        ],
      },
    ];

    this.tables.upcoming.headers = headers
    this.tables.ended.headers = headers
    this.getCourts();
    this.getObjectKinds();
    this.getUpcomingSellAnnouncementsData();
  },
  watch: {},
  methods: {
    getUpcomingSellAnnouncementsData() {
      let query = Object.assign({}, {});
      query.filter = Object.assign({}, this.$refs.upcomingSellAnnouncementsFilter.filters);
      query.filter.isActive = false;
      query.pageNumber = this.tables.upcoming.pagination.page;
      query.pageSize = this.tables.upcoming.pagination.itemsPerPage;

      apiGetCurrentAnnouncementsList(query).then((result) => {
        this.tables.upcoming.data = result.items;
        this.tables.upcoming.pagination.total = result.totalCount;
        this.tables.upcoming.pagination.page = result.currentPage;
      });
    },
    getEndedSellAnnouncementsData() {
      let query = Object.assign({}, {});
      query.filter = Object.assign({}, this.$refs.endedSellAnnouncementsFilter.filters);
      query.filter.isActive = false;
      query.pageNumber = this.tables.ended.pagination.page;
      query.pageSize = this.tables.ended.pagination.itemsPerPage;

      apiGetFinishedAnnouncementsList(query).then((result) => {
        this.tables.ended.data = result.items;
        this.tables.ended.pagination.total = result.totalCount;
        this.tables.ended.pagination.page = result.currentPage;
      });
    },
    getObjectKinds(){
      apiMetaDataGetObjectKind().then((result) => {
        this.$set(this.nomenclatures, "objectKinds", result);
      })
    },
    getCourts() {
      apiMetaDataGetCourtsList().then((result) => {
        this.$set(this.nomenclatures, "courts", result);
      });
    },
    preview(item) {
      this.$router.push({ path: `/sell-announcements/${item.id}` });
    },
    tableActions({ action, item }) {
      switch (action) {
        case "preview":
          this.preview(item);
        break;
      }
    },
    goToContacts(){
      this.$router.push({path: `/sell-announcements/contacts`})
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
        }
    },
    renderSyndic(h, tdElement, props) {
      return h('td', null, [
        props.dataItem.syndic ? getSyndicFullName(props.dataItem.syndic) : ''
      ]);
    },
  },
};
</script>
