<template>
  <v-container fluid tag="section" class="full-height">
    <base-v-component :heading="$route.meta.title" />

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
        fixed-tabs 
        slider-size="3" 
        color="primary" 
        @change="tabChange" 
      >
        <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key">
          {{ tab.name }}
        </v-tab>

        <v-tabs-items v-model="currentTab">
          <template v-for="tab in tabs">
            <v-tab-item eager :value="tab.key" class="mt-5">
              <template v-if="tab.key === 'upcoming'">
                <sell-announcements-filter ref="upcomingSellAnnouncementsFilter" @doFilter="getUpcomingSellAnnouncementsData" />

                <base-kendo-grid
                  :columns="tables.upcoming.headers"
                  :items="tables.upcoming.data"
                  :pagination="tables.upcoming.pagination"
                  sortable
                  :sort.sync="tables.upcoming.sort"
                  @getData="getUpcomingSellAnnouncementsData"
                  @click="tableActions"
                  @dblclick="preview"
                  class="mt-15"
                />
              </template>
              <template v-else>
                <sell-announcements-filter ref="finishedSellAnnouncementsFilter" @doFilter="getFinishedSellAnnouncementsData" />

                <base-kendo-grid
                  :columns="tables.finished.headers"
                  :items="tables.finished.data"
                  :pagination="tables.finished.pagination"
                  sortable
                  :sort.sync="tables.finished.sort"
                  @getData="getFinishedSellAnnouncementsData"
                  @click="tableActions"
                  @dblclick="preview"
                  class="mt-15"
                />
              </template>
            </v-tab-item>
          </template>
        </v-tabs-items>
      </base-material-tabs>

      
    </base-material-card>
  </v-container>
</template>

<script>
import { getSyndicFullName } from "@/utils"
import { SellAnnouncementsFilter } from "@/components"
import { apiGetUpcomingAnnouncements, apiGetFinishedAnnouncements } from "@/api/announcements";
export default {
  name: "sellAnnouncementsList",
  components: {
    SellAnnouncementsFilter
  },
  data: () => ({
    currentTab: 0,
    tabs: [
      { name: "Предстоящи продажби", key: "upcoming" },
      { name: "Приключили продажби", key: "finished" },
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
      finished: {
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
    },
  }),
  computed: {
  },
  mounted() {
    const headers = [
      { title: "Дата", field: "offeringDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: true },
      { title: "Длъжник", field: 'case.side.entity.name', sortable: true },
      { title: "Място", field: "fullAddress", sortable: true },
      { title: "Синдик", cell: this.renderSyndic, sortable: true },
      { title: "Дело №", field: "case.number", sortable: false },
      { title: "Година", field: "case.year", sortable: false },
      { title: "Съд", field: "case.court.name", sortable: false },
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
    this.tables.finished.headers = headers;

    this.getUpcomingSellAnnouncementsData();
  },
  watch: {
    'table.sort': function(){
      if(this.tab === 0){
        this.getUpcomingSellAnnouncementsData();
      } else {
        this.getFinishedSellAnnouncementsData();
      }
    }
  },
  methods: {
    getUpcomingSellAnnouncementsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.upcomingSellAnnouncementsFilter.filters);
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
    getFinishedSellAnnouncementsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.finishedSellAnnouncementsFilter.filters);
      query.pageNumber = this.tables.finished.pagination.skip / this.tables.finished.pagination.take + 1;

      query.pageSize = this.tables.finished.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.finished.pagination.skip = 0;
      }

      apiGetFinishedAnnouncements(query).then((result) => {
        this.tables.finished.data = result.items;
        this.tables.finished.pagination.total = result.totalCount;
        this.tables.finished.pagination.page = result.currentPage;
      });
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
    tabChange(tabKey, force = false){      
      if((this.tables[tabKey] && !this.tables[tabKey].data.length) || force)
        switch (tabKey) {
          case "current":
            this.getUpcomingSellAnnouncementsData();
          break;
          case "finished":
            this.getFinishedSellAnnouncementsData();
          break;
        }
    },
    renderSyndic(h, tdElement, props) {
      return h('td', null, [
        getSyndicFullName(props.dataItem.syndic)
      ]);
    },
  },
};
</script>
