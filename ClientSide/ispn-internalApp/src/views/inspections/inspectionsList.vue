<template>
  <v-container fluid tag="section" class="full-height">
    <base-v-component :heading="$route.meta.title" />


    <v-row>
      <v-col cols="12">

        <base-material-card
          color="primary"
          icon="mdi-magnify-expand"
          inline
          class="px-5 mt-10"
        >
          <template v-slot:after-heading>
            Инспекции
          </template>
          
          <template v-slot:after-heading-button>
            <v-btn
              class="primary"
              @click="addNew"
            >
              <v-icon left dark>mdi-plus</v-icon>
              Нова инспекция
            </v-btn>
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
              <template v-for="tab in tabs" >
                <v-tab-item eager :value="tab.key" class="mt-5">
                  <template v-if="tab.key === 'current'">
                    <inspections-filter ref="currentInspectionsFilter" :syndics="nomenclatures.syndics" :inspection-types="nomenclatures.inspectionTypes" @doFilter="getCurrentInspectionsData" />
                      
                    <base-kendo-grid
                      :columns="tables.current.headers"
                      :items="tables.current.data"
                      :pagination="tables.current.pagination"
                      sortable
                      :sort.sync="tables.current.sort"
                      @getData="getCurrentInspectionsData"
                      @click="tableActions"
                      @dblclick="preview"
                      class="mt-15"
                    />
                  </template>

                  <template v-else>
                    <inspections-filter ref="finishedInspectionsFilter" :syndics="nomenclatures.syndics" :inspection-types="nomenclatures.inspectionTypes" @doFilter="getFinishedInspectionsData" />

                    <base-kendo-grid
                      :columns="tables.finished.headers"
                      :items="tables.finished.data"
                      :pagination="tables.finished.pagination"
                      sortable
                      :sort.sync="tables.finished.sort"
                      @getData="getFinishedInspectionsData"
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
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { getSyndicFullName } from "@/utils";
import { InspectionsFilter } from "@/components";
import { apiGetCurrentInspections, apiGetFinishedInspections } from "@/api/inspections";
import { apiMetaDataGetInspections, apiMetaDataGetSyndics } from "@/api/metaData";
export default {
  name: "inspectionsList",
  components: {
    InspectionsFilter
  },
  data: () => ({
    nomenclatures: {
      inspectionTypes: [],
      syndics: []
    },
    currentTab: 'current',
    tabs: [
      { name: "Текущи", key: "current" },
      { name: "Приключили", key: "finished" },
    ],
    tables: {
      current: {
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
    }
  }),
  computed: {
  },
  mounted() {
    const headers = [
      { title: "Дата", field: "inspectionDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: true },
      { title: "Синдик", field: "syndicName", sortable: true },
      { title: "Тип на инспекцията", field: "inspectionTypeDescription", sortable: true },
      { title: "Инспектор", field: "inspectorName", sortable: true },
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
      }
    ]
    this.tables.current.headers = headers;
    this.tables.finished.headers = headers;


    this.getSyndics();
    this.getInspectionTypes();
    this.getCurrentInspectionsData();
  },
  watch: {
  },
  methods: {
    getCurrentInspectionsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.currentInspectionsFilter[0].filters);
      query.pageNumber = this.tables.current.pagination.skip / this.tables.current.pagination.take + 1;

      query.pageSize = this.tables.current.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.current.pagination.skip = 0;
      }

      // if(this.tables.current.sort)
      //   query.filter.sort = this.tables.current.sort

      apiGetCurrentInspections(query).then((result) => {
        this.tables.current.data = result.items;
        this.tables.current.pagination.total = result.totalCount;
        this.tables.current.pagination.page = result.currentPage;
      });
    },
    getFinishedInspectionsData() {
      let query = Object.assign({}, {});
      
      query.filter = Object.assign({}, this.$refs.finishedInspectionsFilter[0].filters);
      query.pageNumber = this.tables.finished.pagination.skip / this.tables.finished.pagination.take + 1;

      query.pageSize = this.tables.finished.pagination.take;
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.finished.pagination.skip = 0;
      }

      // if(this.tables.finished.sort)
      //   query.filter.sort = this.tables.finished.sort

      apiGetFinishedInspections(query).then((result) => {
        this.tables.finished.data = result.items;
        this.tables.finished.pagination.total = result.totalCount;
        this.tables.finished.pagination.page = result.currentPage;
      });
    },
    getInspectionTypes(){
      apiMetaDataGetInspections().then(result => {
        this.$set(this.nomenclatures, "inspectionTypes", result);
      });
    },
    getSyndics(){
      apiMetaDataGetSyndics().then(result => {
        this.$set(this.nomenclatures, "syndics", result.items);
      }); 
    },
    renderSyndic(h, tdElement, props) {
      return h('td', null, [
      props.dataItem.syndic ? getSyndicFullName(props.dataItem.syndic) : ''
      ]);
    },
    preview(item) {
      this.$router.push({ path: `/inspections/${item.id}` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
    addNew(){
      this.$router.push({path: `/inspections/create`})
    },
    tabChange(tabKey, force = false){      
      if((this.tables[tabKey] && !this.tables[tabKey].data.length) || force)
        switch (tabKey) {
          case "current":
            this.getCurrentInspectionsData();
          break;
          case "finished":
            this.getFinishedInspectionsData();
          break;
        }
    }
  },
};
</script>
