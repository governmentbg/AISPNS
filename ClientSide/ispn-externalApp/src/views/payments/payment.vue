<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isSyndic"
  >
    <base-v-component
      :heading="$route.meta.title"
    />

    <v-row class="my-5 d-print-none">
      <v-col cols="12" lg="5" xl="4">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4" v-if="!isNewDoc && data.paymentStatus">
          Статус на плащането: <strong> {{ paymentStatus }} </strong> <br />
        </v-sheet>
      </v-col>
      <v-col cols="12" lg="7" xl="8" class="text-right">
        <v-btn
          color="primary"
          @click="save"
          v-if="!isPaymentGenerated"
        >
          <v-icon left dark>
            mdi-check
          </v-icon>
          Запази
        </v-btn>
        <v-btn
          color="secondary"
          @click="generatePayment"
          v-if="showGeneratePayment"
        >
          <v-icon left dark>
            mdi-reload
          </v-icon>
          Генерирай плащане
        </v-btn>
        <v-btn
          color="secondary"
          @click="goToEPayment"
          v-if="isPaymentGenerated"
        >
          <v-icon left dark>
            mdi-credit-card-outline
          </v-icon>
          Към ПОС терминал
        </v-btn>

        <v-btn
          color="primary"
          @click="showPaymentDocument"
          v-if="isPaymentGenerated"
        >
          <v-icon left dark>
            mdi-text-box-outline
          </v-icon>
          Платежно нареждане
        </v-btn>

        
      </v-col>
    </v-row>

    <v-row class="my-5">
      <v-col cols="12">
        <base-material-card 
          icon="mdi-cash-sync"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            {{ $route.meta.title }}
          </template>

          <v-form
            ref="form"
            lazy-validation
          >
            <v-row>
              <v-col cols="12" xl="6" lg="10">
                <base-autocomplete
                  label="Вид плащане"
                  v-model="data.installmentKindId"
                  :items="nomenclatures.paymentTypes"
                  item-text="description"
                  item-value="id"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" xl="2" lg="2">
                <base-select
                  label="За година"
                  v-model="data.installmentYearId"
                  :items="nomenclatures.years"
                  item-text="year"
                  item-value="id"
                  :rules=[rules.required]
                  @change="setPaymentAmount"
                />
              </v-col>
              <v-col cols="12" xl="4" lg="5">
                <base-material-date-picker
                  label="Дата на плащане/на платежен документ"
                  v-model="data.paymentDate"
                />
              </v-col>
              <v-col cols="12" xl="4" lg="7">
                <base-input
                  label="Банка"
                  v-model="data.bank"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4">
                <base-input
                  label="Сума"
                  v-model="data.amount"
                  type="number"
                  :rules=[rules.required]
                />
              </v-col>
            </v-row>
          </v-form>
        </base-material-card>

        <base-material-card 
          icon="mdi-file-multiple-outline"
          color="primary"
          class="elevation-3 mt-15"
          v-if="!isNewDoc"
        >
          <template v-slot:after-heading>
            Файлове
          </template>
      
          <template v-slot:after-heading-button>
            <v-btn
              class="primary"
              @click="addNewFile"
            >
              <v-icon left dark>mdi-plus</v-icon>
              Нов файл
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="table.headers"
            :items="table.data"
            :hasPaging="false"
            @click="tableActions"
          />
        </base-material-card>
      </v-col>
    </v-row>
    <payment-file-upload-modal ref="paymentFileUploadModal" :parent-data="data" @reloadData="getDocumentsData" @setDocumentCollectionId="setDocumentCollectionId" />
    <payment-document-modal ref="paymentDocumentModal" :payment-order-data="paymentVPOSData" />
    
    <base-confirm-modal ref="confirmModal" />

    <v-form ref="paymentVPOSForm" :action="paymentURL+'/vpos/payment'" method="post" id="VPOSForm" class="hidden">
      <v-text-field type="hidden" id="clientId" name="clientId" v-model="paymentVPOSData.clientId" />
      <v-text-field type="hidden" id="hmac" name="hmac" v-model="paymentVPOSData.hmac" />
      <v-text-field type="hidden" id="data" name="data" v-model="paymentVPOSData.data" />
      <button type="submit" ref="paymentVPOSSubmitButton" id="submitPaymentVPOSData"></button>
    </v-form>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, bytesToSize, downloadFileFromResponse } from '@/utils';
import { PaymentFileUploadModal, PaymentDocumentModal } from "@/components";
import { apiMetaDataGetInstallmentKinds, apiMetaDataGetInstallmentYears } from "@/api/metaData";
import { apiGetInstallmentById, apiCreateInstallment, apiUpdateInstallment } from "@/api/installments";
import { apiSendPaymentRequest, apiGetPaymentOrderData, apiGetPaymentVPOSData } from "@/api/payments";
import SyndicInstallmentModel from '@/models/syndics/SyndicInstallmentModel';
import { apiGetAllDocumentFiles, apiDeleteDocument, apiDownloadDocument } from '@/api/documents';
import config from '@/config';
export default {
  name: "paymentPreview",
  components: {
    PaymentFileUploadModal,
    PaymentDocumentModal
  },
  data(){
    return {
      isNewDoc: true,
      open: false,
      nomenclatures: {
        paymentTypes: [],
        years: [],
      },
      data: Object.assign({}, new SyndicInstallmentModel()),
      paymentOrder: {
        clientId: null,
        hmac: null,
        data: null
      },
      paymentVPOSData: {
        clientId: null,
        hmac: null,
        data: null
      },
      paymentURL: '',
      table: {
        headers: [
          { title: "Файл", field: "fileName",  sortable: false },
          { title: "Тип документ", field: "description", sortable: false},
          { title: "Размер", field: 'fileSize', cell: this.renderFileSize },
          { title: "Качен на", field: "blobDateCreated", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false, width: '100px' },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "80px",
            sortable: false,
            buttons: [
              {
                title: "Свали",
                icon: "mdi-download",
                color: "white",
                class: "primary mr-1",
                click: "download",
              },
              {
                title: "Изтрий",
                icon: "mdi-trash-can-outline",
                color: "white",
                class: "secondary",
                click: "delete",
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
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    }
  },
  mounted(){
    this.paymentURL = config.isProduction ? 'https://pay.egov.bg' : 'https://pay-test.egov.bg';
    this.isNewDoc = true;
    if(this.$route.params.id){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
      this.getData();
    }

    this.getPaymentKinds();
    this.getPaymentYears();
  },
  computed: {
    ...mapGetters([
      'isSyndic'
    ]),
    isPaymentGenerated(){
      return !this.isNewDoc && this.data.paymentRequestId !== null;
    },
    showGeneratePayment(){
      return !this.isNewDoc && !this.data.paymentRequestId;
    },
    paymentStatus(){
      switch(this.data.paymentStatus){
        case "pending":
          return "Очаква плащане";
        break;
        case "paid":
          return "Платено";
        break;
        case "inProcess":
          return "В процес на обработка";
        break;
      }
    }
  },
  methods: {
    getData(){
      if(this.data.id)
        apiGetInstallmentById(this.data.id).then(result => {
          this.$set(this, 'data', result);

          this.getDocumentsData();

          this.getPaymentOrderData();
          this.getPaymentVPOSData();
        })
    },
    getPaymentKinds(){
      apiMetaDataGetInstallmentKinds().then(result => {
        this.nomenclatures.paymentTypes = result;
      })
    },
    getPaymentYears(){
      apiMetaDataGetInstallmentYears().then(result => {
        this.nomenclatures.years = result;
      })
    },
    getPaymentOrderData(){
      if(this.data.paymentRequestId)
        apiGetPaymentOrderData(this.data.paymentRequestId).then(result => {
          if(result){
            this.paymentOrder = result;
          }
        })
    },
    getPaymentVPOSData(){
      if(this.data.paymentRequestId)
        apiGetPaymentVPOSData(this.data.paymentRequestId).then(result => {
          if(result){
            this.paymentVPOSData = result;
          }
        })
    },
    save(){
      if(this.isNewDoc){
        apiCreateInstallment(this.data).then(result => {
          if(result && result !== '00000000-0000-0000-0000-000000000000') {
            this.$router.push({path: `/payments/${result}`})
          }
        })
      } else {
        apiUpdateInstallment(this.data).then(result => {
          if(result && result.id){
            this.$set(this, "data", result);
          }
        })
      }
    },
    goToEPayment(){
      if(this.data.paymentAccessCode)
        window.open(`${this.paymentURL}/home/accessByCode?code=${this.data.paymentAccessCode}`, '_blank');
      // if(this.paymentVPOSData.clientId)
      //   this.$nextTick().then(() => {
      //     //this.$refs.paymentDocumentForm.submit();
      //     document.querySelector("#submitPaymentVPOSData").click();
      //   });
    },
    generatePayment(){
      apiSendPaymentRequest(this.data.id).then(result => {
        if(result)
          this.getData();
      })
    },
    setDocumentCollectionId(documentCollectionId){
      if(documentCollectionId)
        this.$set(this.data, 'documentCollectionId', documentCollectionId);
    },
    getDocumentsData(reloadParentData = false){
      if(this.data.documentCollectionId){
        let query = Object.assign({}, {});
        
        query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
        query.pageSize = this.table.pagination.take;
        query.filter = [this.data.documentCollectionId];

        if(reloadParentData)
          this.$emit('update');
        
        apiGetAllDocumentFiles(query).then((result) => {
          this.$set(this.table, 'data', result.items);
          this.$set(this.table.pagination, 'total', result.totalCount);
          this.$set(this.table.pagination, 'page', result.currentPage);
        });
      }
    },
    addNewFile(){
      this.$refs.paymentFileUploadModal.openModal()
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "download":
          this.download(rowData);
        break;
        case "delete":
          this.deleteDocumentFileAsk(rowData);
        break;
      }
    },
    deleteDocumentFileAsk(item) {
      let confirmData = {
        title: "Изтриване на файл",
        body: `Сигурни ли сте, че искате да изтриете избрания файл?`,
        callback: this.deleteDocumentFile,
        parameter: item.id,
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteDocumentFile(fileId) {
      apiDeleteDocument(fileId).then((result) => {
        if (result) {
          this.getDocumentsData();
          this.$refs.confirmModal.closeModal();
        }
      });
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
    showPaymentDocument(e){
      this.$refs.paymentDocumentModal.openModal()
    },
    setPaymentAmount(yearId){
      this.$set(this.data, "amount", this.nomenclatures.years.filter(item => item.id === yearId).map(item => item.amount)[0])
    },

    ISODateString: ISODateString
  }
}
</script>