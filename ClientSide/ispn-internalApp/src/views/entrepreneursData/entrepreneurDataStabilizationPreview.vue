<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      heading="Вписване на данни по стабилизация на предприемач"
      goBackText="Обратно към вписване на данни предприемачи стабилизация"
      goBackTo="/entrepreneurs-data/b"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
      </v-col>
      <v-col cols="12" lg="7" xl="8" class="text-right">
        <v-btn
          color="primary"
          @click="registerActAsk"
          v-if="!isReadonly"
        >
          <v-icon left dark>mdi-check</v-icon>
          Впиши
        </v-btn>
        <v-btn
          color="primary darken-2"
          @click="setNoSubjectToRegistrationAsk"
          v-if="!isReadonly"
        >
          <v-icon left dark>mdi-close</v-icon>
          Неподлежи на вписване
        </v-btn>
        <v-btn
          color="secondary"
          @click="$router.push(`/entrepreneurs-acts/stabilization/${actData.id}`)"
          v-if="!isReadonly"
        >
          <v-icon left dark>mdi-card-account-details-star-outline</v-icon>
          Обявяване
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="my-10">
      <v-col cols="12">
        <base-material-card 
          icon="mdi-magnify-expand"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Вписване на данни по стабилизация предприемач
          </template>

          <v-row class="pa-3">
            <v-col cols="12" xl="2" lg="6" md="6">
              <base-input
                label="Дело номер"
                v-model="actData.caseNumber"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="2" lg="6" md="6">
              <base-input
                label="Дело година"
                v-model="actData.caseYear"
                type="number"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4">
              <base-input
                label="Съд"
                v-model="actData.courtName"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="5" lg="8">
              <base-input
                label="Предприемач"
                v-model="actData.proprietorName"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="6">
              <base-input
                label="Вид акт"
                v-model="actData.actKind"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="2" lg="3" md="6">
              <base-input
                label="Свали Акт"
                v-model="actData.actFile"
                append-icon="mdi-download"
                @click:append="downloadAct"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="2" lg="3" md="6">
              <base-input
                label="Свали Писмо"
                v-model="actData.letterFile"
                append-icon="mdi-download"
                @click:append="downloadLetter"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="2" lg="4" md="6">
              <base-input
                label="Статус"
                v-model="actData.registrationStatus"
              />
            </v-col>
          </v-row>
        </base-material-card>

        <base-material-card 
          icon="mdi-file-multiple-outline"
          color="primary"
          class="elevation-3 mt-15"
        >
          <template v-slot:after-heading>
            Вписване
          </template>
          
          <template v-slot:after-heading-button>
            <v-btn
              class="primary"
              @click="addNew"
              v-if="!isReadonly"
            >
              <v-icon left dark>mdi-plus</v-icon>
              Ново вписване
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="table.headers"
            :items="table.data"
            :pagination="table.pagination"
            sortable
            :sort.sync="table.sort"
            @getData="getEntrepreneurRegistersData"
            @click="tableActions"
          />
        </base-material-card>
      </v-col>
    </v-row>
    <entrepreneur-stabilization-register-modal ref="entrepreneurStabilizationRegisterModal" @update="getEntrepreneurRegistersData" :act-data="actData" @confirm="getConfirm" @closeConfirm="$refs.confirmModal.closeModal()" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { downloadFileFromResponse } from '@/utils';
import ActAnnouncementModel from '@/models/actAnnouncement/ActAnnouncementModel';
import { apiGetActById, apiGetActAnnouncementById, apiGetAllRegisterEntries, apiRegisterAct, apiNoSubjectToRegistration, apiDownloadActImage, apiDownloadActLetterImage, apiDeleteRegisterEntry } from "@/api/actAnnouncements";
import { EntrepreneurStabilizationRegisterModal } from "@/components";
import { eActStatuses } from "@/utils/enums/enumerators";
import { isReadonly } from 'vue';
export default {
  name: "entrepreneurDataStabilizationPreview",
  components: {
    EntrepreneurStabilizationRegisterModal
  },
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {
      },
      actData: Object.assign({}, {}),
      data: Object.assign({}, new ActAnnouncementModel()),
      table: {
        headers: [
          { title: "Вид поле", field: "fieldName", sortable: false, width: '500px' },
          { title: "Номер", field: "number", sortable: false, width: '130px' },
          { title: "Дата", field: "date", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false, width: '100px' },
          { title: "Правно основание", field: "legalBasisName", sortable: false, width: '150px' },
          { title: "Подлежи на незабавно изпълнение", field: "isEnforcedImmediately", cell: 'trueOrFalse', sortable: false, width: '150px', className: 'text-center', headerClassName: 'text-center' },
          { title: "Срок на обжалване", field: "appealTerm", sortable: false, width: '100px' },
          { title: "Вписване на", field: "announcedDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false, width: '100px' },
          { title: "Вписан от", field: "announcedByUser", sortable: false, width: '200px' },
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
                class: "primary",
                click: "preview",
              },
              {
                title: "Изтрий",
                icon: "mdi-trash-can-outline",
                color: "white",
                class: "error ml-1",
                click: "deleteRegistryEntry",
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
      },
    }
  },
  mounted(){
    this.isNewDoc = true;
    if(this.$route.params.id != 'create'){
      this.actData.id = this.$route.params.id;
      this.isNewDoc = false;
      this.getData();
    } else {
      this.$router.push('/entrepreneurs-data/stabilization');
    }
  },
  computed: {
    ...mapGetters([
      'currentUser'
    ]),
    isReadonly(){
      return this.data.statusId?.toLowerCase() !== eActStatuses.NOT_PROCESSED.toLowerCase();
    },
  },
  watch: {
    isReadonly: {
      handler(val){
        if(val){
          this.table.headers[this.table.headers.length - 1].width = "50px";
          this.table.headers[this.table.headers.length - 1].buttons = [
            {
              title: "Преглед",
              icon: "mdi-text-box-search-outline",
              color: "white",
              class: "primary",
              click: "preview",
            },
          ];
        }
      },
      deep: true
    }
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetActById(this.actData.id).then(result => {
          this.$set(this, 'actData', result);
          this.getAnnouncementData();
        })
    },
    getAnnouncementData(){
      if(this.actData.actAnnouncementId)
        apiGetActAnnouncementById(this.actData.actAnnouncementId).then(result => {
          if(result) {
            this.$set(this, 'data', result);
            this.getEntrepreneurRegistersData();
          }
        })
    },
    registerActAsk(){
      if(this.table.data.length){
        let confirmData = {
          title: "Вписване на данни на акт",
          body: "Сигурни ли сте, че искате да впишете данните на акта?",
          callback: this.registerAct
        }

        this.$refs.confirmModal.openModal(confirmData);
      } else {
        this.$snotify.warning("Моля въведете данни в таблицата.")
      }
    },
    registerAct(){
      apiRegisterAct(this.data).then(result => {
        if(result && result.length){
          this.$refs.confirmModal.closeModal();
          this.getData();
        }
      })
    },
    setNoSubjectToRegistrationAsk(){
      let confirmData = {
        title: "Неподлежи на вписване",
        body: "Сигурни ли сте, че искате да впишете данните като неподлежащи на вписване?",
        callback: this.doNothingToAct
      }

      this.$refs.confirmModal.openModal(confirmData);
    },
    setNoSubjectToRegistration(){
      apiNoSubjectToRegistration(this.data).then(result => {
        if(result && result.length){
          this.$refs.confirmModal.closeModal();
          this.getData();
        }
      })
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.previewEntryData(rowData);
        break;
        case "deleteRegistryEntry":
          this.deleteRegistryEntryAsk(rowData);
        break;
      }
    },
    previewEntryData(rowData){
      this.$refs.entrepreneurStabilizationRegisterModal.openModal(rowData.id);
    },
    deleteRegistryEntryAsk(rowData){
      let confirmData = {
        title: "Изтриване на вписано данни",
        body: "Сигурни ли сте, че искате да изтриете избраните вписани данни?",
        callback: this.deleteRegistryEntry,
        parameter: rowData
      }

      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteRegistryEntry(rowData){
      if(!this.isReadonly)
        apiDeleteRegisterEntry(rowData.id).then(result => {
          if(result && result.length){
            this.getEntrepreneurRegistersData();
            this.$refs.confirmModal.closeModal();
          }
        })
    },
    getEntrepreneurRegistersData(){
      let query = Object.assign({}, {});
        
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
      query.pageSize = this.table.pagination.take;
      query.actId = this.actData.id;

        
      apiGetAllRegisterEntries(query).then(result => {
        this.$set(this.table, 'data', result.items);
        this.$set(this.table.pagination, 'total', result.totalCount);
        this.$set(this.table.pagination, 'page', result.currentPage);
      })
    },
    downloadAct(){
      apiDownloadActImage(this.actData.id).then(result => {
        downloadFileFromResponse(result);
      })
    },
    downloadLetter(){
      apiDownloadActLetterImage(this.actData.id).then(result => {
        downloadFileFromResponse(result);
      })
    },
    addNew(){
      this.$refs.entrepreneurStabilizationRegisterModal.openModal()
    },
    getConfirm(confirmData){
      this.$refs.confirmModal.openModal(confirmData);
    }
  }
}
</script>