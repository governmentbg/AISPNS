<template>
  <div>
    <v-dialog v-model="open" width="70%" scrollable>
      <v-card>
        <v-card-title class="headline">
          <template v-if="isNewDoc">Отбелязване на плащане</template>
          <template v-else>Преглед на плащане</template>
        </v-card-title>

        <v-card-text>
          <v-form ref="form">
            <v-row>
              <v-col cols="12" xl="10" lg="9" md="9">
                <base-autocomplete
                  label="Вид плащане"
                  v-model="data.installmentKindId"
                  :items="nomenclatures.paymentKinds"
                  item-text="description"
                  item-value="id"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="3">
                <base-select
                  label="За година"
                  v-model="data.installmentYearId"
                  :items="nomenclatures.years"
                  item-text="year"
                  item-value="id"
                />
              </v-col>
              <v-col cols="12" xl="4" lg="5">
                <base-material-date-picker
                  label="Дата на плащане/на платежен документ"
                  v-model="data.paymentDate"
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4">
                <base-input
                  label="Платежен документ №"
                  v-model="data.number"
                />
              </v-col>
              <v-col cols="12" xl="5" lg="5">
                <base-input
                  label="Банка"
                  v-model="data.bank"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3">
                <base-input
                  label="Сума"
                  v-model.number="data.amount"
                  type="number"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4">
                <v-checkbox
                  label="Проверено плащане"
                  v-model="data.verified"
                  dense
                />
              </v-col>
              <v-col cols="12" lg="5">
                <base-input
                  label="Проверил плащането"
                  v-model="data.verifiedByName"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="5" md="6">
                <base-material-date-picker
                  label="Срок за автоматично прекратяване"
                  v-model="data.terminationDeadline"
                />
              </v-col>
              <v-col cols="12">
                <base-textarea
                  label="Бележки"
                  v-model="data.note"
                />
              </v-col>
            </v-row>
          </v-form>

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
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="save(false)"> Запази </v-btn>
          <v-btn color="secondary" @click="save(true)"> Запази и затвори </v-btn>
          <v-btn color="primary" outlined @click="open = false">
            Затвори
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <syndic-installment-file-upload-modal ref="installmentFileUploadModal" :parent-data="data" @reloadData="getDocumentsData" @setDocumentCollectionId="setDocumentCollectionId" />
  </div>
</template>

<script>
import { apiGetInstallmentById, apiCreateInstallment, apiUpdateInstallment } from "@/api/installments";
import { apiMetaDataGetInstallmentKinds, apiMetaDataGetInstallmentYears } from "@/api/metaData";
import SyndicInstallmentModel from "@/models/syndics/SyndicInstallmentModel";
import { apiDeleteDocument, apiDownloadDocument, apiGetAllDocumentFiles } from "@/api/documents";
import { bytesToSize, downloadFileFromResponse } from "@/utils";
import SyndicInstallmentFileUploadModal from "@/components/modals/syndics/paymentFileUploadModal.vue";
export default {
  name: "syndicPaymentModal",
  components: {
    SyndicInstallmentFileUploadModal,
  },
  props: {
    syndicData: {
      type: Object,
      default: () => {
        return {};
      },
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: Object.assign({}, new SyndicInstallmentModel()),
      nomenclatures: {
        paymentKinds: [],
        years: []
      },
      table: {
        headers: [
          { title: "Файл", field: "fileName",  sortable: false },
          { title: "Описание", field: "description",  sortable: false },
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
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = Object.assign({}, new SyndicInstallmentModel());
        this.table.data = [];
      } else {
        if (this.data.id) {
          this.isNewDoc = false;
        } else {
          this.isNewDoc = true;
          if(this.$refs.form)
            this.$refs.form.resetValidation();
        }
      }
    },
  },
  computed: {
    
  },
  methods: {
    getData() {
      apiGetInstallmentById(this.data.id).then((result) => {
        this.$set(this, "data", result);
        this.getDocumentsData();
        this.isNewDoc = false;
        this.open = true;
      });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          let data = structuredClone(this.data);
          data.syndicId = this.syndicData.id;
          apiCreateInstallment(data).then((result) => {
            if(result && result !== '00000000-0000-0000-0000-000000000000') {
              this.data.id = result;
              this.$emit("update");
              if(closeModal){
                this.closeModal();
              } else {
                this.getData();
              }
            }
          });
        } else {
          apiUpdateInstallment(this.data).then((result) => {
            if (result && result.id) {
              this.$set(this, "data", result);
              this.$emit("update");
              if(closeModal)
                this.closeModal();
            }
          });
        }
    },
    getPaymentKinds(){
      apiMetaDataGetInstallmentKinds().then((result) => {
        this.$set(this.nomenclatures, 'paymentKinds', result);
      });
    },
    getPaymentYears(){
      apiMetaDataGetInstallmentYears().then((result) => {
        this.$set(this.nomenclatures, 'years', result);
      });
    },
    openModal(id = null) {
      if(this.nomenclatures.paymentKinds.length === 0)
        this.getPaymentKinds();

      if(this.nomenclatures.years.length === 0)
        this.getPaymentYears();

      if(id){
        this.data.id = id;
        this.getData();
      } else {
        this.open = true;
        this.isNewDoc = true;
      }

      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    closeModal(){
      this.open = false;
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
        query.filter = [this.data.documentCollectionId]

        if(reloadParentData)
          this.$emit('reloadData');
        
        apiGetAllDocumentFiles(query).then((result) => {
          this.$set(this.table, 'data', result.items);
          this.$set(this.table.pagination, 'total', result.totalCount);
          this.$set(this.table.pagination, 'page', result.currentPage);
        });
      }
    },
    addNewFile(){
      this.$refs.installmentFileUploadModal.openModal()
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
      this.$emit('confirm', confirmData);
    },
    deleteDocumentFile(fileId) {
      apiDeleteDocument(fileId).then((result) => {
        if (result) {
          this.getDocumentsData();
          this.$emit('reloadData');
          this.$emit('closeConfirm');
        }
      });
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
  },
};
</script>

<style></style>
