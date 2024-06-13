<!-- eslint-disable vue/valid-v-for -->
<template>
  <v-container 
    id="nomenclatureTabs" 
    fluid 
    tag="section" 
    class="full-height"
    v-if="isAdministrator"
  >
    <base-v-component :heading="$route.meta.title" />

    <v-tabs
      v-model="currentMainTab"
      slider-color="primary"
      color="secondary"
      fixed-tabs
      @change="mainTabChange"
      class="my-10"
    >
      <v-tab v-for="mainTab in mainTabs" :key="`${mainTab.key}`" active-class="secondary">
        {{ mainTab.name }}
      </v-tab>

      <v-tabs-items v-model="currentMainTab"  class="py-5 px-3 pb-0">
        <v-tab-item v-for="mainTab in mainTabs" :key="'mainTab_'+mainTab.key">
          <v-tabs
            v-model="currentTab[mainTab.key]"
            vertical
            slider-color="primary"
            color="secondary"
            @change="tabChange"
            class="innerTabs"
          >
            <v-tab v-for="tab in innerTabs[mainTab.key]" :key="`${tab.key}`" active-class="secondary">
              {{ tab.name }}
            </v-tab>

            <v-tabs-items v-model="currentTab[mainTab.key]" class="pt-4">
              <v-tab-item v-for="tab in innerTabs[mainTab.key]" :key="tab.key" class="px-3">
                <v-card flat class="">
                  <v-card-title class="headline print d-print-flex mb-0">
                    <v-row>
                      <v-col cols="12" md="6">
                        {{ tab.name }}
                      </v-col>
                      <v-col cols="12" md="6" class="text-right" v-if="showNewNomenclatureButton">
                        <v-btn color="primary" small @click="newNomenclature">
                          <v-icon left> mdi-plus </v-icon>
                          Добави номенклатура
                        </v-btn>
                      </v-col>
                    </v-row>
                  </v-card-title>
                  <v-card-text class="pa-0">
                    <Grid
                      :columns="tables[tab.key].headers"
                      :data-items="tables[tab.key].data"
                      scrollable="none"
                      resizable
                      :row-render="rowRender"
                    >
                        
                      <template v-slot:status="{ props }">
                        <td :class="`k-table-td${props.className ? ' '+props.className : ''}`">
                          <v-icon v-if="props.dataItem[props.field]" color="success">mdi-check</v-icon>
                          <v-icon v-else color="error">mdi-close</v-icon>
                        </td>
                      </template>
                      <template v-slot:actions="{ props }">
                        <td :class="props.className ? props.className : ''">
                          <v-btn
                            title="Редакция"
                            color="white"
                            icon
                            outlined
                            rounded
                            small
                            @click="editNomenclature(props.dataItem)"
                            class="primary mr-1"
                          >
                            <v-icon class="fs18">mdi-pencil</v-icon>
                          </v-btn>
                          <v-btn
                            title="Изтрий"
                            color="grey--text"
                            icon
                            outlined
                            rounded
                            small
                            @click="deleteNomenclatureAsk(props.dataItem)"
                            v-if="showNewNomenclatureButton"
                          >
                            <v-icon class="fs18">mdi-trash-can-outline</v-icon>
                          </v-btn>
                        </td>
                      </template>
                    </Grid>
                  </v-card-text>
                </v-card>
              </v-tab-item>
            </v-tabs-items>
          </v-tabs>
        </v-tab-item>
      </v-tabs-items>
    </v-tabs>

    <nomenclature-modal
      ref="nomenclatureModal"
      :type="getCurrentTabDetails ? getCurrentTabDetails.id : -1"
      :nomenclature-enums="innerTabs[this.mainTabs[this.currentMainTab].key]"
      @update="getTablesData"
    />

    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
// import na api request ot @/api/nomenclatures
import {
  apiGetNomenclatures,
  apiGetNomenclatureEnums,
  apiDeleteNomenclature,
} from "@/api/nomenclatures";

import { NomenclatureModal } from "@/components";
import { Grid } from "@progress/kendo-vue-grid";
import { eNomenclatureMainField } from "@/utils/enums/enumerators"

export default {
  name: "nomenclatureTabs",
  components: {
    NomenclatureModal,
    Grid,
  },
  data: () => ({
    data: {},
    mainTabs: [
      { key: 'bankruptcy', name: 'Несъстоятелност' },
      { key: 'syndics', name: 'Синдици' },
      { key: 'salesAnnouncements', name: 'Обяви продажби' }
    ],
    currentMainTab: 0,
    innerTabs: {
      bankruptcy: [],
      syndics: [],
      salesAnnouncements: []
    },
    currentTab: {
      bankruptcy: 0,
      syndics: 0,
      salesAnnouncements: 0
    },
    tables: {},
  }),
  computed: {
    ...mapGetters([
      'isAdministrator'
    ]),
    getCurrentTabDetails(){
      if(this.mainTabs.length && this.innerTabs[this.mainTabs[this.currentMainTab].key].length)
        return this.innerTabs[this.mainTabs[this.currentMainTab].key][this.currentTab[this.mainTabs[this.currentMainTab].key]];

      return null;
    },
    showNewNomenclatureButton(){
      return ![87].includes(this.getCurrentTabDetails.id);
    }
  },
  beforeMount() {
    this.getTabsEnums();
  },
  mounted() {},
  methods: {
    getTabsEnums() {
      apiGetNomenclatureEnums().then((result) => {
        // LegalBasus and SignalDocumentKind are not used anymore
        result = result.filter((nom) => !['LegalBasis', 'SignalDocumentKind'].includes(nom.key))
        for(let tab of result){
          this.innerTabs[this.mainTabs[tab.nomenclatureTypeID - 1].key].push(tab)
          this.tables[tab.key] = {
            data: [],
            headers: [
              { title: "Код", field: "code" },
              { title: "Наименование", field: "description" },
              { title: "", cell: "actions", class: "text-right", width: "80px" },
            ],
            pagination: {
              page: 1,
              perPageOptions: [5, 10, 20, 50, 100],
              total: 0,
            },
          };

          if(this.mainTabs[tab.nomenclatureTypeID - 1].key != 'salesAnnouncements'){

            switch(tab.key){
              case 'CourtNumber':
                this.tables[tab.key].headers = [
                  { title: "Номер", field: "number" },
                  { title: "Съд", field: "name" },
                ]
              break;
              case 'ReportSource':
                this.tables[tab.key].headers = [
                  { title: "Източник", field: "name" },
                  { title: "", cell: "actions", class: "text-right", width: "80px" },
                ]
              break;
              case 'ReportType':
                this.tables[tab.key].headers = [
                  { title: "Наименование", field: "name" },
                  { title: "Описание", field: "description" },
                  { title: "", cell: "actions", class: "text-right", width: "80px" },
                ]
              break;
              case 'AppellateCourt':
                this.tables[tab.key].headers = [
                  { title: "Номер", field: "number" },
                  { title: "Апелативен съд", field: "appellateCourtName" },
                  { title: "Съд", field: "courtName" },
                ]
              break;
              case 'OrderKind':
              case 'Specialty':
              case 'DirectiveTemplate':
              case 'SyndicStatus':
              case 'InspectionType':
              case 'ObjectKind':
              case 'ObjectType':
              case 'SyndicAction':
              case 'CompanySize':
              case 'RegistrationLegalBasis':
              case 'SyndicKind':
              case 'RegisterSyndicKind':
                this.tables[tab.key].headers = [
                  { title: "Наименование", field: "description" },
                  { title: "", cell: "actions", class: "text-right", width: "80px" },
                ]
              break;
              case 'SignalSenderType':
              case 'AttachedDocumentKind':
              case 'DebtorStatus':
              case 'SignalSenderType':
              case 'SignalDocumentKind':
              case 'SyndicCaseReport':
              case 'CourseKind':
              case 'OrderPaymentKind':
              case 'TemplateKind':
                this.tables[tab.key].headers = [
                  { title: "Наименование", field: "name" },
                  { title: "", cell: "actions", class: "text-right", width: "80px" },
                ]
              break;
              case 'InstallmentYear':
                this.tables[tab.key].headers = [
                  { title: "Година", field: "year" },
                  { title: "Вноска", field: "amount" },
                  { title: "", cell: "actions", class: "text-right", width: "80px" },
                ]
              break;
              case 'ActCategory':
              case 'ActAnnouncementCategory':
              case 'ActContractor':
              case 'RegisterField':
                this.tables[tab.key].headers = [
                  { title: "Наименование", field: "description" },
                  { title: "Несъстоятелност/стабилизация", cell: this.renderIsStabilization },
                  { title: "", cell: "actions", class: "text-right", width: "80px" },
                ]
              break;
            }
          } else {
            switch(tab.key){
              case "SalesProcedure":
                this.tables[tab.key].headers = [
                  { title: "Описание", field: "description" },
                  { title: "", cell: "actions", class: "text-right", width: "80px" },
                ]
              break;
              case "SellAnnouncementTemplate":
                this.tables[tab.key].headers = [
                  { title: "Наименование", field: "name" },
                  { title: "Описание", field: "description" },
                  { title: "", cell: "actions", class: "text-right", width: "45px", className: 'text-center' },
                ]
              break;
              default: 
                this.tables[tab.key].headers = [
                  { title: "Наименование", field: "description" },
                  { title: "", cell: "actions", class: "text-right", width: "80px" },
                ]
              break;
            }
          }
        }
        this.getTablesData();
      });
    },
    getTablesData() {
      apiGetNomenclatures(this.getCurrentTabDetails.id).then((result) => {
        this.$set(this.tables[this.getCurrentTabDetails.key], "data", result);
        this.$forceUpdate();
      });
    },
    expandFirstLastFields(items) {
      return items.map((nom, idx) => {
        nom.isFirst = false;
        nom.isLast = false;

        if (idx === 0) nom.isFirst = true;

        if (idx === items.length - 1) nom.isLast = true;

        return nom;
      });
    },
    rowRender(h, trElement, defaultSlots, props) {
      return h("tr", {
          on: {
            dblclick: () => {
              this.editNomenclature(props.dataItem);
            },
          },
          staticClass: trElement.data.class,
        },
        defaultSlots
      );
    },
    editNomenclature(item) {
      this.$refs.nomenclatureModal.getData(item.id, this.getCurrentTabDetails.id);
    },
    deleteNomenclatureAsk(item) {
      let confirmData = {
        title: "Изтриване на номенклатура",
        body: `Сигурни ли сте, че искате да изтриете "${eNomenclatureMainField.NAME.includes(this.getCurrentTabDetails.id) ? item.name : item.description}" номенклатура?`,
        callback: this.deleteNomenclature,
        parameter: item.id,
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteNomenclature(nomenclatureId) {
      apiDeleteNomenclature(nomenclatureId, this.getCurrentTabDetails.id).then((result) => {
          if (result) this.getTablesData();
          this.$refs.confirmModal.closeModal();
      });
    },
    save(){

    },
    renderIsStabilization(h, tdElement, props) {
      return h("td", null, [
        props.dataItem.isStabilization ? "Стабилизация" : "Несъстоятелност",
      ]);
    },
    pageChangeHandler(e) {
      this.tables[this.getCurrentTabDetails.key].pagination.take = e.page.take;
      this.tables[this.getCurrentTabDetails.key].pagination.skip = e.page.skip;
      this.getData();
    },
    tabChange() {
      this.getTablesData();
    },
    mainTabChange() {
      this.getTablesData();
    },
    newNomenclature() {
      if(this.getCurrentTabDetails.key === "ValidityPeriod"){
        this.$refs.nomenclatureModal.openModal({name: '', periodInMonths: null, periodInYears: null});
      } else {
        this.$refs.nomenclatureModal.openModal();
      }
    },
  },
};
</script>

<style lang="sass">
#nomenclatureTabs
  .v-tabs
    &.innerTabs
      .v-item-group
        border-bottom: 1px solid #eeeeee

        .v-slide-group__wrapper
          .v-slide-group__content
            &:first-child
              margin-top: 15px

            .v-tab
              height: auto
              min-height: 40px
              font-size: 14px !important
              padding: 8px 0 8px 12px !important
              text-wrap: initial
              text-align: left

</style>
