<template>
  <v-container fluid tag="section" class="full-height" v-if="isMEIEmployee">
    <base-v-component :heading="$route.meta.title" />


    <base-material-card
      color="primary"
      icon="mdi-message-text-outline"
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
              <template v-if="tab.key === 'future'">
                <base-kendo-grid
                  :columns="tables.future.headers"
                  :items="tables.future.data"
                  :pagination="tables.future.pagination"
                  sortable
                  :sort.sync="tables.future.sort"
                  @getData="getFutureCertificatesData"
                  @click="tableActions"
                  @dblclick="preview"
                  class="mt-5"
                />
              </template>

              <template v-else>
                <base-kendo-grid
                  :columns="tables.ended.headers"
                  :items="tables.ended.data"
                  :pagination="tables.ended.pagination"
                  sortable
                  :sort.sync="tables.ended.sort"
                  @getData="getEndedCertificatesData"
                  @click="tableActions"
                  @dblclick="preview"
                  class="mt-5"
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

import { mapGetters } from 'vuex';
import { apiGetAnnouncementCertificates, apiGetFinishedAnnouncementCertificates } from "@/api/mii_certificates";
import { apiGenerateCertificateForAnnouncement } from '@/api/mii_certificates';
import { downloadFileFromResponse } from '@/utils';
export default {
  name: "certificatesList",
  components: {},
  data: () => ({
    currentTab: 0,
    tabs: [
      { name: "Удостоверения предстоящи продажби", key: "future" },
      { name: "Удостоверения приключили продажби", key: "ended"}
    ],
    tables: {
      future: {
        headers: [
          { title: "Обява номер", field: "announcementNumber", sortable: true },
          { title: "Дата на публикуване", field: "publishDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: true },
          { title: "Длъжник", field: "debtorName", sortable: true },
          { title: "Синдик", field: "syndicName", sortable: true },
          { title: "Дело №", field: "caseNumber", sortable: true },
          { title: "Година", field: "caseYear", sortable: true },
          { title: "Съд", field: "courtName", sortable: true },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "80px",
            sortable: false,
            buttons: [
              {
                title: "Преглед",
                icon: "mdi-text-box-search-outline",
                color: "white",
                class: "primary mr-1",
                click: "preview",
              },
              {
                title: "Свали удостоверение",
                icon: "mdi-download",
                color: "white",
                class: "secondary",
                click: "downloadCertificate",
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
        }
      },
      ended: {
        headers: [
          { title: "Обява номер", field: "announcementNumber", sortable: false },
          { title: "Дата на публикуване", field: "publishDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false },
          { title: "Длъжник", field: "debtorName", sortable: false },
          { title: "Синдик", field: "syndicName", sortable: false },
          { title: "Дело №", field: "caseNumber", sortable: false },
          { title: "Година", field: "caseYear", sortable: false },
          { title: "Съд", field: "courtName", sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "80px",
            sortable: false,
            buttons: [
              {
                title: "Преглед",
                icon: "mdi-text-box-search-outline",
                color: "white",
                class: "primary mr-1",
                click: "preview",
              },
              {
                title: "Свали удостоверение",
                icon: "mdi-download",
                color: "white",
                class: "secondary",
                click: "downloadCertificate",
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
    }
  }),
  computed: {
    ...mapGetters([
      "isMEIEmployee",
    ]),
  },
  mounted() {
    this.getFutureCertificatesData();
  },
  watch: {
  },
  methods: {
    getFutureCertificatesData() {
      let query = Object.assign({filters: {}}, {});
      
      query.pageNumber = this.tables.future.pagination.skip / this.tables.future.pagination.take + 1;

      query.pageSize = this.tables.future.pagination.take;
      if(query.filters.page === 1){
        query.pageNumber = 1;
        this.tables.future.pagination.skip = 0;
      }

      if(this.tables.future.sort)
        query.filters.sort = this.tables.future.sort

      apiGetAnnouncementCertificates(query).then((result) => {
        this.tables.future.data = result.items;
        this.tables.future.pagination.total = result.totalCount;
        this.tables.future.pagination.page = result.currentPage;
      });
    },
    getEndedCertificatesData(){
      let query = Object.assign({filters: {}}, {});

      query.pageNumber = this.tables.ended.pagination.skip / this.tables.ended.pagination.take + 1;

      query.pageSize = this.tables.ended.pagination.take;
      if(query.filters.page === 1){
        query.pageNumber = 1;
        this.tables.ended.pagination.skip = 0;
      }

      if(this.tables.ended.sort)
        query.filters.sort = this.tables.ended.sort

      apiGetFinishedAnnouncementCertificates(query).then((result) => {
        this.tables.ended.data = result.items;
        this.tables.ended.pagination.total = result.totalCount;
        this.tables.ended.pagination.page = result.currentPage;
      });
    },
    preview(item) {
      this.$router.push({ path: `/mii-sell-announcements/${item.announcementId}` });
    },
    downloadCertificate(data){
      apiGenerateCertificateForAnnouncement(data.announcementId).then(result => {
        downloadFileFromResponse(result);
      })
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
        case "downloadCertificate":
          this.downloadCertificate(rowData);
        break;
      }
    },
    tabChange(tabKey, force = false){      
      if((this.tables[tabKey] && !this.tables[tabKey].data.length) || force)
        switch (tabKey) {
          case "future":
            this.getFutureCertificatesData();
          break;
          case "ended":
            this.getEndedCertificatesData();
          break;
        }
    }
  },
};
</script>
