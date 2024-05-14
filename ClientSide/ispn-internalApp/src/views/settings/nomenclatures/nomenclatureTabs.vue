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
        <v-tab-item v-for="mainTab in mainTabs" :key="mainTab.key">
          <v-tabs
            v-model="currentTab"
            vertical
            slider-color="primary"
            color="secondary"
            @change="tabChange"
            class="innerTabs"
          >
            <v-tab v-for="tab in innerTabs[mainTab.key]" :key="`${tab.key}`" active-class="secondary">
              {{ tab.name }}
            </v-tab>

            <v-tabs-items v-model="currentTab" class="pt-4">
              <v-tab-item v-for="tab in innerTabs[mainTab.key]" :key="tab.key" class="px-3">
                <v-card flat class="">
                  <v-card-title class="headline print d-print-flex mb-0">
                    <v-row>
                      <v-col cols="12" md="6">
                        {{ tab.name }}
                      </v-col>
                      <v-col cols="12" md="6" class="text-right">
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
                            title="Премести нагоре"
                            color="white"
                            icon
                            outlined
                            rounded
                            small
                            @click="moveUp(props.dataItem)"
                            class="info mr-1"
                            :disabled="props.dataItem.isFirst"
                          >
                            <v-icon class="fs18">mdi-arrow-up-thick</v-icon>
                          </v-btn>
                          <v-btn
                            title="Премести надолу"
                            color="white"
                            icon
                            outlined
                            rounded
                            small
                            @click="moveDown(props.dataItem)"
                            class="info mr-1"
                            :disabled="props.dataItem.isLast"
                          >
                            <v-icon class="fs18">mdi-arrow-down-thick</v-icon>
                          </v-btn>
                          <v-btn
                            title="Изтрий"
                            color="grey--text"
                            icon
                            outlined
                            rounded
                            small
                            @click="deleteNomenclatureAsk(props.dataItem)"
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
// import {
//   apiGetNomenclatures,
//   apiGetNomenclatureEnums,
//   apiDeleteNomenclature,
//   apiMoveNomenclature,
// } from "@/api/nomenclatures";

import { NomenclatureModal } from "@/components";
import { Grid } from "@progress/kendo-vue-grid";

export default {
  name: "nomenclatureTabs",
  components: {
    NomenclatureModal,
    Grid,
  },
  data: () => ({
    mainTabs: [
      { key: 'bankruptcy', name: 'Несъстоятелност' },
      { key: 'syndics', name: 'Синдици' },
      { key: 'salesAnnouncements', name: 'Обяви продажби' }
    ],
    currentMainTab: 0,
    innerTabs: {
      bankruptcy: [
        { key: 'codeOfCourts', name: 'Единен информационен код на съдилищата' },
        { key: 'functionalJurisdiction', name: 'Съдилища за обжалване по функционална подсъдност' },
        { key: 'caseTypes', name: 'Вид дело' },
        { key: 'incommingDocTypes', name: 'Вид на входящ документ' },
        { key: 'sides', name: 'Участие/страна' },
        { key: 'meetingTypes', name: 'Вид заседания' },
        { key: 'meetingResults', name: 'Резултати от заседания' },
        { key: 'legalActsTypes', name: 'Вид на съдебни актове' },
        { key: 'TZGrounds', name: 'Основания по ТЗ' },
        { key: 'incommingAppealDocumentTypes', name: 'Вид на входящ документ за обжалване' },
        { key: 'actionTypes', name: 'Вид действия' },
        { key: 'debtorStatuses', name: 'Статус на длъжника' },
        { key: 'outgoingDocumentTypes', name: 'Вид на изходящ документ' },
      ],
      syndics: [
        { key: 'statuses', name: 'Статус на синдика' },
        { key: 'specialities', name: 'Специалност на синдика' },
        { key: 'orderTypes', name: 'Вид заповеди' },
        { key: 'groundTypes', name: 'Описание/Основание на заповедта' },
        { key: 'paymentTypes', name: 'Вид начин на плащане' },
        { key: 'amountsByYear', name: 'Сума на вноската по години' },
        { key: 'trainingTypes', name: 'Вид на обученията' },
        { key: 'syndicsTemplates', name: 'Образци на синдици' },
        { key: 'syndicsCaseReports', name: 'Отчети на синдици към дело' },
        { key: 'expertTypes', name: 'Тип вещ' },
        { key: 'expertKinds', name: 'Вид вещ' },
        { key: 'patentTypes', name: 'Вид патент' },
        { key: 'partitionTypes', name: 'Вид дял' },
        { key: 'receivableTypes', name: 'Вид вземания' },
        { key: 'syndicActionTypes', name: 'Вид действие на динсик' },
        { key: 'senderSignalTypes', name: 'Тип на подателя на сигнала'},
        { key: 'documentSignalTypes', name: 'Вид на документа по сигнал' },
        { key: 'inspectionTypes', name: 'Тип на инспекцията' },
      ],
      salesAnnouncements: []
    },
    currentTab: 0,
    tables: {},
  }),
  computed: {
    ...mapGetters([
      'isAdministrator'
    ]),
    getCurrentTabDetails(){
      if(this.mainTabs.length && this.innerTabs[this.mainTabs[this.currentMainTab].key].length) 
        return this.innerTabs[this.mainTabs[this.currentMainTab].key];

      return null;
    },
  },
  beforeMount() {
    this.getTabsEnums();
  },
  mounted() {},
  methods: {
    getTabsEnums() {
      // TODO - remove rows bellow
      for(let mainTab of this.mainTabs){
        if(this.innerTabs[mainTab.key])
          for(let tab of this.innerTabs[mainTab.key]){
            this.tables[tab.key] = {
              data: [],
              headers: [
                { title: "Наименование", field: "name" },
                { title: "", cell: "actions", class: "text-right", width: "140px" },
              ],
              pagination: {
                page: 1,
                perPageOptions: [5, 10, 20, 50, 100],
                total: 0,
              },
            };
          }
      }

      // apiGetNomenclatureEnums().then((result) => {
      //   this.$set(this, "mainTabs", result);
      //   for(let mainTab of this.mainTabs){
      //     for(let tab of this.innerTabs[mainTab.key]){
      //       this.tables[tab.key] = {
      //         data: [],
      //         headers: [
      //           { title: "Наименование", field: "name" },
      //           { title: "", cell: "actions", class: "text-right", width: "140px" },
      //         ],
      //         pagination: {
      //           page: 1,
      //           perPageOptions: [5, 10, 20, 50, 100],
      //           total: 0,
      //         },
      //       };

      //       if(tab.key === 'GroundOfEntry'){
      //         this.tables[tab.key].headers = [
      //           { title: "Наименование", field: "name" },
      //           { title: "Първоначален", field: "isInitialEntry", cell: "status", width: "150px" },
      //           { title: "Отменим", field: "canBeCanceled", cell: "status", width: "100px" },
      //           { title: "Съставен", field: "isComposite", cell: "status", width: "100px" },
      //           { title: "", cell: "actions", class: "text-right", width: "140px" },
      //         ];
      //       } else if(tab.key === 'ForeignGroundOfEntry'){
      //         this.tables[tab.key].headers = [
      //           { title: "Наименование", field: "name" },
      //           { title: "Първоначален", field: "isInitialEntry", cell: "status", width: "150px" },
      //           { title: "Отменим", field: "canBeCanceled", cell: "status", width: "100px" },
      //           { title: "", cell: "actions", class: "text-right", width: "140px" },
      //         ];
      //       }
      //     }
      //   }

      //   this.getTablesData();
      // });
    },
    getTablesData() {
      // apiGetNomenclatures(this.getCurrentTabDetails.id).then((result) => {
      //   this.$set(this.tables[this.getCurrentTabDetails.key], "data", this.expandFirstLastFields(result));
      //   this.$forceUpdate();
      // });
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
    moveUp(item) {
      // apiMoveNomenclature(item.id, this.getCurrentTabDetails.id, true).then(() => { 
      //   this.getTablesData();
      // });
    },
    moveDown(item) {
      // apiMoveNomenclature(item.id, this.getCurrentTabDetails.id, false).then(() => {
      //   this.getTablesData();
      // });
    },
    deleteNomenclatureAsk(item) {
      let confirmData = {
        title: "Изтриване на номенклатура",
        body: `Сигурни ли сте, че искате да изтриете "${item.name}" номенклатура?`,
        callback: this.deleteNomenclature,
        parameter: item.id,
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteNomenclature(nomenclatureId) {
      // apiDeleteNomenclature(nomenclatureId, this.getCurrentTabDetails.id).then((result) => {
      //     if (result) this.getTablesData();
      //     this.$refs.confirmModal.closeModal();
      // });
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
      this.$forceUpdate();
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
              font-size: 14px !important
              padding: 0 0 0 12px !important
              text-wrap: initial
              text-align: left

</style>
