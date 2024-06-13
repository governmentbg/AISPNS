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
          Прикачи образец
        </v-btn>
      </v-col>
    </v-row>

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
        <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key">
          {{ tab.name }}
        </v-tab>

        <v-tabs-items v-model="currentTab">
          <template v-for="tab in tabs">
            <v-tab-item eager :value="tab.key" :key="tab.key">
              <template v-if="tab.key === 'uploaded'">
                <base-kendo-grid
                  :columns="tables[tab.key].headers"
                  :items="tables[tab.key].data"
                  :pagination="tables[tab.key].pagination"
                  sortable
                  :sort.sync="tables[tab.key].sort"
                  @getData="getUploadedTemplatesData"
                  @click="tableActions"
                  @dblclick="preview"
                  class="mt-5"
                />
              </template>

              <template v-else>
                <base-kendo-grid
                  :columns="tables[tab.key].headers"
                  :items="tables[tab.key].data"
                  :pagination="tables[tab.key].pagination"
                  sortable
                  :sort.sync="tables[tab.key].sort"
                  @getData="getPublishedTemplatesData"
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
import { apiGetTemplates, apiGetAdminTemplates, apiGenerateAdminTemplate } from "@/api/templates";
import { apiDownloadDocument } from '@/api/documents';
import { downloadFileFromResponse } from '@/utils';
export default {
  name: "templatesList",
  components: {},
  data: () => ({
    currentTab: 'uploaded',
    tabs: [
      { name: "Прикачени образци", key: "uploaded" },
      { name: "Публикувани образци", key: "published"}
    ],
    tables: {
      uploaded: {
        headers: [
          { title: "Вид", field: "templateKindName", sortable: false },
          { title: "Дата", field: "date", type: 'date', format: "{0:dd.MM.yyyy}", sortable: false },
          { title: "Описание", field: "description", sortable: false },
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
        ],
        data: [
          {
            id: 1,
            type: "Писмо",
            date: "2021-01-01",
            description: "Писмо за образец",
          },
          {
            id: 2,
            type: "Писмо",
            date: "2021-01-01",
            description: "Писмо за образец",
          },
          {
            id: 3,
            type: "Писмо",
            date: "2021-01-01",
            description: "Писмо за образец",
          },
          {
            id: 4,
            type: "Писмо",
            date: "2021-01-01",
            description: "Писмо за образец",
          },
          {
            id: 5,
            type: "Писмо",
            date: "2021-01-01",
            description: "Писмо за образец",
          },
          {
            id: 6,
            type: "Писмо",
            date: "2021-01-01",
            description: "Писмо за образец",
          },
          {
            id: 7,
            type: "Писмо",
            date: "2021-01-01",
            description: "Писмо за образец",
          },
          {
            id: 8,
            type: "Писмо",
            date: "2021-01-01",
            description: "Писмо за образец",
          },
          {
            id: 9,
            type: "Писмо",
            date: "2021-01-01",
            description: "Писмо за образец",
          },
          {
            id: 10,
            type: "Писмо",
            date: "2021-01-01",
            description: "Писмо за образец",
          }
        ],
        sort: {},
        pagination: {
          page: 1,
          skip: 0,
          take: 20,
          perPageOptions: [5, 10, 20, 50, 100],
          total: 0,
        }
      },
      published: {
        headers: [
          { title: "Вид", field: "templateName" },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              {
                title: "Свали",
                icon: "mdi-download fs18",
                color: "white",
                class: "transparent primary--text fs18",
                click: "download",
              }
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
    ...mapGetters(["isSyndic"]),
  },
  mounted() {
    this.getUploadedTemplatesData();
  },
  watch: {
  },
  methods: {
    getUploadedTemplatesData() {
      let query = Object.assign({filters: {}}, {});
      
      query.pageNumber = this.tables.uploaded.pagination.skip / this.tables.uploaded.pagination.take + 1;
      query.pageSize = this.tables.uploaded.pagination.take;

      apiGetTemplates(query).then((result) => {
        this.tables.uploaded.data = result.items;
        this.tables.uploaded.pagination.total = result.totalCount;
        this.tables.uploaded.pagination.page = result.currentPage;
      });
    },
    getPublishedTemplatesData(){
      let query = Object.assign({filters: {}}, {});

      query.pageNumber = this.tables.published.pagination.skip / this.tables.published.pagination.take + 1;
      query.pageSize = this.tables.published.pagination.take;
      
      apiGetAdminTemplates(query).then((result) => {
        this.tables.published.data = result.items;
        this.tables.published.pagination.total = result.totalCount;
        this.tables.published.pagination.page = result.currentPage;
      });
    },
    preview(item) {
      this.$router.push({ path: `/templates/${item.id}` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
        case "download":
          this.download(rowData);
      }
    },
    addNew(){
      this.$router.push({path: `/templates/create`})
    },
    download(fileData){
      // apiDownloadDocument(fileData.documentContentID).then(result => {
      //   downloadFileFromResponse(result)
      // })
      apiGenerateAdminTemplate(fileData.id).then(result => {
        downloadFileFromResponse(result)
      })
    },
    tabChange(tabKey, force = false){      
      if((this.tables[tabKey] && !this.tables[tabKey].data.length) || force)
        switch (tabKey) {
          case "uploaded":
            this.getUploadedTemplatesData();
          break;
          case "published":
            this.getPublishedTemplatesData();
          break;
        }
    }
  },
};
</script>
