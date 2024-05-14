<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isSyndic"
  >
    <base-v-component
      :heading="isNewDoc ? 'Създаване на дневник' : 'Преглед на дневник'"
      :goBackText="$route.meta.goBackTitle"
      goBackTo="/activities"
    />

    <v-row class="mt-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          color="primary"
          @click="saveEntityStatisticalData"
          v-if="showSaveEntityStatisticalDataButton"
        >
          <v-icon left dark>mdi-check</v-icon>
          Запази
        </v-btn>
        <v-btn
          color="secondary"
          @click="generateDocument"
        >
          <v-icon left dark>mdi-download</v-icon>
          Генерирай Дневник
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
      <v-col cols="12" lg="6" class="mx-auto">
        <base-material-card
          color="primary"
          icon="mdi-account"
          inline
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">Длъжник</div>
          </template>

          <v-simple-table class="table-presentation">
            <template v-slot:default>
              <tbody>
                <tr>
                  <td style="width:50%">Броя на съкратените работни места, свързани с ПНС:</td>
                  <td>
                    <base-input
                      v-model="entityStatisticalData.numberJobCuts"
                      type="number"
                      hide-details
                      :disabled="!showSaveEntityStatisticalDataButton"
                    />
                  </td>
                </tr>
                <tr>
                  <td>Бил ли е обект на план за преструктуриране:</td>
                  <td>
                    <v-checkbox 
                      v-model="entityStatisticalData.wasRestructured"
                      dense
                      hide-details
                      :true-value="true"
                      :false-value="false"
                      :disabled="!showSaveEntityStatisticalDataButton"
                    />
                  </td>
                </tr>
                <tr>
                  <td>Размер на длъжника:</td>
                  <td>
                    <base-autocomplete
                      v-model="entityStatisticalData.companySizeId"
                      hide-details
                      :items="nomenclatures.companySizes"
                      :disabled="!showSaveEntityStatisticalDataButton"
                    />
                  </td>
                </tr>
              </tbody>
            </template>
          </v-simple-table>
        </base-material-card>
      </v-col>

      <v-col cols="12">
        <base-material-card
          color="primary"
          icon="mdi-format-list-bulleted"
          inline
          class="px-5 py-5 mt-5"
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">Действия</div>
          </template>

          <template v-slot:after-heading-button>
            <v-btn small class="primary mr-2"  @click="openActionModal">
              <v-icon left dark> mdi-plus </v-icon>
              Ново действие
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="actionTable.headers"
            :items="actionTable.data"
            :pagination="actionTable.pagination"
            sortable
            :sort.sync="actionTable.sort"
            @getData="getActivitiesData"
            @click="tableActions"
            class="mt-5"
          />
        </base-material-card>
      </v-col>
    </v-row>
    <journal-action-modal ref="actionModal" @update="getActivitiesData" :case-data="data" @confirm="getConfirm" @closeConfirm="$refs.confirmModal.closeModal()" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, downloadFileFromResponse } from '@/utils';
import { JournalActionModal } from '@/components';
import { apiGetCaseById, apiGetEntityStatisticalDataByEntityId, apiSaveEntityStatisticalData } from '@/api/cases';
import { apiMetaDataGetCompanySizes } from "@/api/metaData";
import { apiSearchActivities, apiDeleteActivity, apiGenerateActivityDocument } from "@/api/activities";
export default {
  name: "previewJournal",
  components: {
    JournalActionModal
  },
  data(){
    return {
      isNewDoc: true,
      data: {},
      entityStatisticalData: {},
      nomenclatures: {
        companySizes: []
      },
      actionTable: {
        headers: [
          { title: "Вид", field: "activityKindName", sortable: true },
          { title: "Дата", field: "activityDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: true },
          { title: "Описание", field: "description", sortable: false },
          { title: "Синдик", field: "syndicName", sortable: true },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "80px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "previewActivity" },
              { title: "Изтрий", icon: "mdi-trash-can-outline", color: "white", class: "secondary", click: "deleteActivityAsk" },
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
    }
  },
  mounted(){
    if(this.$route.params.id === 'create'){
      this.isNewDoc = true;
    } else {
      this.data.id = this.$route.params.id;
      this.getData();
    }

    this.getCompanySizes();
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
    showSaveEntityStatisticalDataButton(){
      if(this.data?.side?.id)
        return true;

      return false;
    }
  },
  methods: {
    getData(){
      apiGetCaseById(this.data.id).then(result => {
        this.$set(this, 'data', result);
        this.getActivitiesData()
        this.isNewDoc = false;
        this.getEntityStatisticalData();
      })
    },
    getEntityStatisticalData(){
      if(this.data?.side?.id){
        apiGetEntityStatisticalDataByEntityId(this.data?.side?.id).then(result => {
          if(result){
            this.$set(this, 'entityStatisticalData', result);
          } else {
            this.$set(this, 'entityStatisticalData', {
              entityId: this.data?.side?.id
            })
          }
        })
      }
    },
    saveEntityStatisticalData(){
      apiSaveEntityStatisticalData(this.entityStatisticalData).then(result => {
        if(result && result.id)
          this.getEntityStatisticalData();
      })
    },
    getCompanySizes(){
      apiMetaDataGetCompanySizes().then(result => {
        this.$set(this.nomenclatures, 'companySizes', result);
      })
    },
    print(){

    },
    getActivitiesData(){
      let query = Object.assign({}, {});
      query.filter = {
        caseId: this.data.id
      }
      query.pageNumber = this.actionTable.pagination.skip / this.actionTable.pagination.take + 1;
      query.pageSize = this.actionTable.pagination.take;
      

      apiSearchActivities(query).then(result => {
        this.actionTable.data = result.items;
        this.actionTable.pagination.total = result.totalCount;
        this.actionTable.pagination.page = result.currentPage;
      })
    },
    previewActivity(data){
      this.$refs.actionModal.openModal(data.id);
    },
    deleteActivityAsk(activityData){
      let confirmData = {
        title: "Изтриване на действие",
        body: `Сигурни ли сте, че искате да изтриете избраното действие?`,
        callback: this.deleteActivity,
        parameter: activityData.id
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteActivity(id){
      apiDeleteActivity(id).then(result => {
        if(result){
          this.getActivitiesData();
          this.$refs.confirmModal.closeModal();
        }
      })
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "previewActivity":
          this.previewActivity(rowData);
        break;
        case "deleteActivityAsk":
          this.deleteActivityAsk(rowData);
        break;
      }
    },
    openActionModal(){
      this.$refs.actionModal.openModal();
    },
    getConfirm(confirmData){
      this.$refs.confirmModal.openModal(confirmData);
    },
    generateDocument(){
      apiGenerateActivityDocument(this.data.id).then(result => {
        downloadFileFromResponse(result);
      })
    },
    ISODateString: ISODateString
  }
}
</script>