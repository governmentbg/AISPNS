<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isSyndic"
  >
    <base-v-component
      :heading="isNewDoc ? 'Създаване на маса по несъстоятелност' : 'Преглед на маса по несъстоятелност'"
      :goBackText="$route.meta.goBackTitle"
      goBackTo="/activities"
    />

    <v-row class="mt-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          color="primary"
          @click="generateDocument"
        >
          <v-icon left dark>mdi-download</v-icon>
          Генерирай Маса по несъстоятелност
        </v-btn>
      </v-col>
    </v-row>

    <v-row class="d-print-none mt-10">
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
                    <td>{{ getDebtorName }}</td>
                  </tr>
                  <tr>
                    <td>ЕИК:</td>
                    <td>{{ getDebtorBulstat }}</td>
                  </tr>
                </tbody>
              </template>
            </v-simple-table>
          </v-container>
        </base-material-card>
      </v-col>
    </v-row>
    <v-row class="my-5">
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

                      <template v-slot:after-heading-button>
                        <v-btn small class="primary px-5"  @click="addNewByTab(tab.key)">
                          <v-icon left dark> mdi-plus </v-icon>
                          {{ getButtonTextByTab(tab.key) }}
                        </v-btn>
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
    <property-modal ref="propertyModal" @update="getTablesData" :tab="currentTab" :case-data="data" @confirm="getConfirm" @closeConfirm="$refs.confirmModal.closeModal()" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, downloadFileFromResponse } from '@/utils';
import { ePropertyClassKinds } from "@/utils/enums/enumerators";
import { PropertyModal } from '@/components';
import { apiGetCaseById } from '@/api/cases';
import { apiGetPropertiesByClass, apiDeleteProperty, apiGeneratePropertyDocument } from "@/api/properties"
export default {
  name: "previewJournal",
  components: {
    PropertyModal
  },
  data(){
    return {
      isNewDoc: true,
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
              width: "80px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "previewJournal" },
                { title: "Изтрий", icon: "mdi-trash-can-outline", color: "white", class: "secondary", click: "deletePropertyThingAsk" },
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
              width: "80px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "previewJournal" },
                { title: "Изтрий", icon: "mdi-trash-can-outline", color: "white", class: "secondary", click: "deletePropertyPatentAsk" },
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
              width: "80px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "previewJournal" },
                { title: "Изтрий", icon: "mdi-trash-can-outline", color: "white", class: "secondary", click: "deletePropertyShareAsk" },
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
              width: "80px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "previewJournal" },
                { title: "Изтрий", icon: "mdi-trash-can-outline", color: "white", class: "secondary", click: "deletePropertyReceivableAsk" },
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
    }
  },
  mounted(){
    this.data.id = this.$route.params.id;
    this.getData();

    this.getTablesData();
  },
  computed: {
    ...mapGetters([
      'isSyndic'
    ]),
    getCourtName(){
      return this.data.court ? this.data.court.name : '';
    },
    getDebtorName(){
      return this.data?.side?.entity?.name || ' - ';
    },
    getDebtorBulstat(){
      return this.data?.side?.entity?.bulstat || ' - ';
    },
  },
  methods: {
    getData(){
      apiGetCaseById(this.data.id).then((result) => {
        this.$set(this, 'data', result);
        this.isNewDoc = false;
      })
    },
    getTablesData(){
      let query = Object.assign({}, {});
      
      query.pageNumber = this.tables[this.currentTab].pagination.skip / this.tables[this.currentTab].pagination.take + 1;
      query.pageSize = this.tables[this.currentTab].pagination.take;
      query.filter = {
        caseId: this.data.id,
        kind: ePropertyClassKinds[this.currentTab.toUpperCase()]
      }
      
      apiGetPropertiesByClass(query).then((result) => {
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
    addNewByTab(){
      this.$refs.propertyModal.openModal()
    },
    previewJournal(data){
      this.$refs.propertyModal.openModal(data.id)
    },
    deletePropertyAsk(propertyData){
      let title = null;
      let body = null;
      switch(this.currentTab){
        case 'things':
          title = 'Изтриване на вещ';
          body = `Сигурни ли сте, че искате да изтриете избраната вещ?`;
        break;
        case 'patents':
          title = "Изтриване на патент";
          body = `Сигурни ли сте, че искате да изтриете избрания патент?`;
        break;
        case 'shares':
          title = "Изтриване на дял";
          body = `Сигурни ли сте, че искате да изтриете избрания дял?`;
        break;
        case 'receivables':
          title = "Изтриване на взимане";
          body = `Сигурни ли сте, че искате да изтриете избраното взимане?`;
        break;
      }
      
      if(title && body){
        let confirmData = {
          title: title,
          body: body,
          callback: this.deleteProperty,
          parameter: propertyData.id
        };
        this.$refs.confirmModal.openModal(confirmData);
      }
    },
    deleteProperty(id){
      apiDeleteProperty(id).then(result => {
        if(result)
          this.getTablesData();
        
        this.$refs.confirmModal.closeModal();
      })
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "previewJournal":
          this.previewJournal(rowData);
        break;
        case "deletePropertyThingAsk":
        case "deletePropertyPatentAsk":
        case "deletePropertyShareAsk":
        case "deletePropertyReceivableAsk":
          this.deletePropertyAsk(rowData);
        break;
      }
    },
    getConfirm(confirmData){
      this.$refs.confirmModal.openModal(confirmData);
    },
    generateDocument(){
      apiGeneratePropertyDocument(this.data.id).then(result => {
        downloadFileFromResponse(result);
      })
    },
    ISODateString
  }
}
</script>