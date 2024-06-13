<template>
  <div>
    <v-dialog v-model="open" width="85%" scrollable>
      <v-card>
        <v-card-title class="headline">
          Преглед на маса по несъстоятелност
        </v-card-title>

        <v-card-text ref="cardContent">
          <v-row class="d-print-none mt-1">
            <v-col cols="12" lg="6" class="mx-auto">
              <base-material-card 
                icon="mdi-gavel"
                color="primary"
                inline
              >
                <template v-slot:after-heading>
                  <div>Дело</div>
                </template>

                <v-container class="py-0">
                  <v-simple-table class="table-presentation">
                    <template v-slot:default>
                      <tbody>
                        <tr>
                          <td style="width:100px">Съд:</td>
                          <td>{{ getCourtName }}</td>
                        </tr>
                        <tr>
                          <td>Номер:</td>
                          <td>{{ data.number }}</td>
                        </tr>
                        <tr>
                          <td>Дата:</td>
                          <td>{{ ISODateString(data.formationDate) }}</td>
                        </tr>
                        <tr>
                          <td>Длъжник:</td>
                          <td>{{ debtor.name }}</td>
                        </tr>
                        <tr>
                          <td>ЕИК:</td>
                          <td>{{ debtor.bulstat }}</td>
                        </tr>
                      </tbody>
                    </template>
                  </v-simple-table>
                </v-container>
              </base-material-card>
            </v-col>
          </v-row>
          <v-row class="my-5 mt-10 elevation-5">
            <v-col cols="12">
              <v-tabs
                v-model="currentTab"
                fixed-tabs
                slider-color="primary"
                @change="tabChange"
                slider-size="3"
              >
                <v-tabs-slider color="secondary" />
                <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key">
                  {{ tab.name }}
                </v-tab>
              </v-tabs>
              <v-tabs-items v-model="currentTab">
                <v-tab-item v-for="tab in tabs" :key="tab.key" :value="tab.key">
                  <v-card flat>
                    <v-card-text class="mt-5">
                      <v-row class="d-print-none mt-5">
                        <v-col cols="12" class="mx-auto">

                          <base-material-card 
                            :icon="getCardIconByTab(tab.key)"
                            color="primary"
                            class="elevation-3"
                          >
                            <template v-slot:after-heading>
                              {{ tab.name }}
                            </template>
                            
                            <base-kendo-grid
                              :columns="tables[tab.key].headers"
                              :items="tables[tab.key].data"
                              :pagination="tables[tab.key].pagination"
                              sortable
                              :sort.sync="tables[tab.key].sort"
                              @getData="getTablesData"
                              @click="tableActions"
                            />
                          </base-material-card>
                        </v-col>
                      </v-row>
                    </v-card-text>
                  </v-card>
                </v-tab-item>
              </v-tabs-items>
            </v-col>
          </v-row>
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" outlined @click="open = false">
            Затвори
          </v-btn>
        </v-card-actions>
      </v-card>
      <SyndicPropertyModal ref="propertyModal" :caseData="data" :tab="currentTab" />
    </v-dialog>
  </div>
</template>

<script>
import { ISODateString } from "@/utils";
import { apiGetSyndicCaseProperties } from "@/api/syndics";
import { ePropertyClassKinds } from "@/utils/enums/enumerators";
import { apiGetCaseById } from "@/api/cases";
import SyndicPropertyModal from "@/components/modals/syndics/syndicPropertyModal.vue";
export default {
  name: "syndicJournalActivityModal",
  components: {
    SyndicPropertyModal
  },
  props: {
    syndicData: {
      type: Object,
      default: () => {},
    },
  },
  mounted() {},
  data() {
    return {
      open: false,
      currentTab: 'things',
      tabs: [
        { name: "Вещи", key: "things" },
        { name: "Патенти", key: "patents" },
        { name: "Дялове", key: "shares" },
        { name: "Взимания", key: "receivables" },
      ],
      data: {},
      tables: {
        things: {
          headers: [
            { title: "Тип", field: "propertyTypeName", sortable: false },
            { title: "Вид", field: "propertyKindName", sortable: false },
            { title: "Адрес", field: "fullAddress", sortable: false },
            { title: "Стойност", field: "value", cell: 'amount', sortable: false },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "previewJournal" },
              ],
            },
          ],
          data: [],
          sort: {
            field: 'date',
            dir: 'desc'
          },
          pagination: {
            skip: 0,
            take: 10,
            total: 0,
            page: 1
          }
        },
        patents: {
          headers: [
            { title: "Вид", field: "propertyTypeName", sortable: false },
            { title: "Описание", field: "state", sortable: false },
            { title: "Стойност", field: "value", cell: 'amount', sortable: false },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "previewJournal" },
              ],
            },
          ],
          data: [],
          sort: {
            field: 'date',
            dir: 'desc'
          },
          pagination: {
            skip: 0,
            take: 10,
            total: 0,
            page: 1
          }
        },
        shares: {
          headers: [
            { title: "Вид", field: "propertyTypeName", sortable: false },
            { title: "Юридическо лице", field: "entityName", sortable: false },
            { title: "Описание", field: "state", sortable: false },
            { title: "Стойност", field: "value", cell: 'amount', sortable: false },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "previewJournal" },
              ],
            },
          ],
          data: [],
          sort: {
            field: 'date',
            dir: 'desc'
          },
          pagination: {
            skip: 0,
            take: 10,
            total: 0,
            page: 1
          }
        },
        receivables: {
          headers: [
            { title: "Вид", field: "propertyTypeName", sortable: true },
            { title: "Юридическо лице", field: "entityName", sortable: false },
            { title: "Физическо лице", field: "personName", sortable: false },
            { title: "Описание", field: "state", sortable: true },
            { title: "Стойност", field: "value", cell: 'amount', sortable: false },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "previewJournal" },
              ],
            },
          ],
          data: [],
          sort: {
            field: 'date',
            dir: 'desc'
          },
          pagination: {
            skip: 0,
            take: 10,
            total: 0,
            page: 1
          }
        }
      }
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.$set(this, 'data', {});
        this.currentTab = 'things';
        for(let key in this.tables){
          this.$set(this.tables[key], 'data', []);
        }
      }
    },
  },
  computed: {
    getCourtName(){
      return this.data.court ? this.data.court.name : '';
    },
    debtor(){
      let debtor = {};
      if(this.data.sides)
        debtor = this.data.sides.map(side => {
          if(side.isDebtor)
            return side.entity;
        })

      return debtor[0] || {name: ' — ', bulstat: ' — '};
    }
  },
  methods: {
    getData() {
      apiGetCaseById(this.data.id).then((result) => {
        this.$set(this, "data", result);
        this.getTablesData();
        this.open = true;

        this.$nextTick(() => {
          this.$refs.cardContent.scrollTop = 0;
        })
      });
    },
    openModal(id = null) {
      if(id){
        this.data.id = id;
        this.getData();
      } else {
        this.open = true;
      }

      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    closeModal(){
      this.open = false;
    },
    getTablesData(){
      let query = Object.assign({}, {});
      
      query.pageNumber = this.tables[this.currentTab].pagination.skip / this.tables[this.currentTab].pagination.take + 1;
      query.pageSize = this.tables[this.currentTab].pagination.take;
      query.filter = {
        caseId: this.data.id,
        kind: ePropertyClassKinds[this.currentTab.toUpperCase()]
      }
      
      apiGetSyndicCaseProperties(query).then((result) => {
        this.tables[this.currentTab].data = result.items;
        this.tables[this.currentTab].pagination.total = result.totalCount;
        this.tables[this.currentTab].pagination.page = result.currentPage;
      });
    },
    tabChange(tabKey, force = false){      
      if((this.tables[tabKey] && !this.tables[tabKey].data.length) || force)
        this.getTablesData();
    },
    getCardIconByTab(tabKey){
      switch(tabKey){
        case "things":
          return "mdi-home-city-outline";
        case "patents":
          return "mdi-file-sign";
        case "shares":
          return "mdi-book-multiple-outline";
        case "receivables":
          return "mdi-receipt-text-outline";
      }
    },
    getButtonTextByTab(tabKey){
      switch(tabKey){
        case "things":
          return "Нова вещ";
        case "patents":
          return "Нов патент";
        case "shares":
          return "Нов дял";
        case "receivables":
          return "Ново взимане";
      }
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "previewJournal":
          this.previewJournal(rowData);
        break;
      }
    },
    previewJournal(data){
      this.$refs.propertyModal.openModal(data.id)
    },
    ISODateString
  },
};
</script>