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
      goBackTo="/journals"
    />

    <v-row class="mt-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          color="primary"
          @click="save"
        >
          <v-icon left dark>mdi-check</v-icon>
          Запази
        </v-btn>
        <v-btn
          color="secondary"
          @click="print"
        >
          <v-icon left dark>mdi-printer</v-icon>
          Разпечатай
        </v-btn>
        <v-btn
          color="secondary"
          @click="sendByEmail"
        >
          <v-icon left dark>mdi-email-arrow-right-outline</v-icon>
          Изпрати по мейл
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

          <template v-slot:after-heading-button>
            <v-btn small class="primary mr-2"  @click="openCasesModal">
              <v-icon left dark> mdi-book-search-outline </v-icon>
              Избери дело
            </v-btn>
          </template>

          <v-container class="py-0">
            <v-simple-table class="table-presentation">
              <template v-slot:default>
                <tbody>
                  <tr>
                    <td style="width:100px">Съд:</td>
                    <td>Окръжен съд – Благоевград</td>
                  </tr>
                  <tr>
                    <td>Номер:</td>
                    <td>189/2023</td>
                  </tr>
                  <tr>
                    <td>Дата:</td>
                    <td>2023-10-18</td>
                  </tr>
                  <tr>
                    <td>Длъжник:</td>
                    <td>Иван Иванов ООД</td>
                  </tr>
                  <tr>
                    <td>ЕИК:</td>
                    <td>143242344</td>
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
    <cases-list-selection-modal ref="casesListSelectionModal" @selected="selectedCase" />
    <journal-bankruptcy-modal ref="journalBankruptcyModal" @reload="getActionsData" :tab="currentTab" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString } from '@/utils';
import { CasesListSelectionModal, JournalActionModal, JournalBankruptcyModal } from '@/components';
export default {
  name: "previewJournal",
  components: {
    CasesListSelectionModal,
    JournalBankruptcyModal
  },
  data(){
    return {
      isNewDoc: true,
      currentTab: 'things',
      tabs: [
        { name: "Вещи", key: "things" },
        { name: "Патенти", key: "patents" },
        { name: "Дялове", key: "shares" },
        { name: "Взимания", key: "receipts" },
      ],
      data: {},
      tables: {
        things: {
          headers: [
            { title: "Тип", field: "", sortable: true },
            { title: "Вид", field: "", sortable: true },
            { title: "Адрес", field: "", sortable: true },
            { title: "Стойност", field: "", sortable: false },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewJournal" },
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
            { title: "Вид", field: "", sortable: false },
            { title: "Стойност", field: "", sortable: false },
            { title: "Описание", field: "", sortable: false },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewJournal" },
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
            { title: "Вид", field: "", sortable: false },
            { title: "Юридическо лице", field: "", sortable: false },
            { title: "Стойност", field: "", sortable: false },
            { title: "Описание", field: "", sortable: false },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewJournal" },
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
        receipts: {
          headers: [
            { title: "Вид", field: "", sortable: true },
            { title: "Юридическо лице", field: "", sortable: false },
            { title: "Физическо лице", field: "", sortable: false },
            { title: "Стойност", field: "", sortable: false },
            { title: "Описание", field: "", sortable: true },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewJournal" },
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
    if(this.$route.params.id === 'create'){
      this.isNewDoc = true;
    } else {
      this.data.id = this.$route.params.id;
      this.getData();
    }
  },
  computed: {
    ...mapGetters([
      'isSyndic'
    ]),
  },
  methods: {
    getData(){
      // apiGetCaseById(this.data.id).then(data => {
      //   this.data = data;
      //   this.isNewDoc = false;
      // })
    },
    getTablesData(){
      switch(this.tab){
        case 0:
          this.getThingsData();
        break;
        case 1:
          this.getPatentsData();
        break;
        case 2:
          this.getSharesData();
        break;
        case 3:
          this.getReceiptsData();
        break;
      }
    },
    getThingsData(){

    },
    getPatentsData(){

    },
    getSharesData(){

    },
    getReceiptsData(){

    },
    tabChange(tabKey, force = false){      
      if((this.tables[tabKey] && !this.tables[tabKey].data.length) || force)
        switch (tabKey) {
          case "things":
            this.getThingsData();
          break;
          case "patents":
            this.getPatentsData();
          break;
          case "shares":
            this.getSharesData();
          break;
          case "receipts":
            this.getReceiptsData();
          break;
        }
    },
    getCardIconByTab(tabKey){
      switch(tabKey){
        case "things":
          return "mdi-home-city-outline";
        case "patents":
          return "mdi-file-sign";
        case "shares":
          return "mdi-book-multiple-outline";
        case "receipts":
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
        case "receipts":
          return "Ново взимане";
      }
    },
    addNewByTab(tabKey){
      console.log("journalBankruptcyModal = ")
      this.$refs.journalBankruptcyModal.openModal()
    },
    save(){

    },
    print(){

    },
    sendByEmail(){

    },
    selectedCase(){

    },
    getActionsData(){
      let query = Object.assign({}, {});
      query.pageNumber = this.actionTable.pagination.skip / this.actionTable.pagination.take + 1;

      query.pageSize = this.actionTable.pagination.take;
      if(query.filters.page === 1){
        query.pageNumber = 1;
        this.actionTable.pagination.skip = 0;
      }

      // apiGetCaseSides(query).then(result => {
      //   this.actionTable.data = result.items;
      //   this.actionTable.pagination.total = result.totalCount;
      //   this.actionTable.pagination.page = result.currentPage;
      // })
    },
    preview(){

    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
    addAction(){

    },
    openCasesModal(){
      this.$refs.casesListSelectionModal.openModal();
    },
    openJournalActionModal(){
      this.$refs.journalActionModal.openModal();
    },
    ISODateString: ISODateString
  }
}
</script>