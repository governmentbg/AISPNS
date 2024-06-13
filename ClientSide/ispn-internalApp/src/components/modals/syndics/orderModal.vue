<template>
  <div>
    <v-dialog v-model="open" width="70%" scrollable>
      <v-card>
        <v-card-title class="headline">
          <template v-if="isNewDoc"> Нова заповед </template>
          <template v-else> Преглед на заповед </template>
        </v-card-title>

        <v-card-text>
          <v-form ref="form">
            <v-row>
              <v-col cols="12" xl="5" lg="7">
                <v-checkbox
                  v-model="data.isExclusion"
                  label="Заповед за отстраняване/изключване"
                  :true-value="true"
                  :false-value="false"
                  dense
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" xl="6" lg="9">
                <base-autocomplete
                  label="Вид заповед"
                  v-model="data.orderKindId"
                  :items="nomenclatures.orderKinds"
                  item-text="description"
                  item-value="id"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" xl="3" lg="3">
                <base-input
                  label="Номер"
                  v-model="data.number"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" xl="3" lg="3">
                <base-material-date-picker
                  label="Дата"
                  v-model="data.date"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4">
                <base-input
                  label="Държавен вестник брой"
                  v-model="data.stateGazetteEdition"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" xl="3" lg="5">
                <base-input
                  label="Държавен вестник година"
                  v-model="data.stateGazetteYear"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12">
                <base-input
                  label="Описание"
                  v-model="data.description"
                />
              </v-col>

              <template v-if="data.isExclusion">
                <v-col cols="12" xl="3" lg="5">
                  <base-material-date-picker
                    label="Дата на изключване"
                    v-model="data.exclusionStartDate"
                    :rules=[rules.required]
                  />
                </v-col>
                <v-col cols="12" xl="5" lg="6">
                  <base-material-date-picker
                    label="Дата на временно отстраняване"
                    v-model="data.exclusionEndDate"
                  />
                </v-col>
                <v-col cols="12" xl="4" lg="6">
                  <base-material-date-picker
                    label="Срок на временно отстраняване"
                    v-model="data.exclusionTemporaryDate"
                  />
                </v-col>
                <v-col cols="12">
                  <base-input
                    label="Основание"
                    v-model="data.exclusionGrounds"
                    :rules=[rules.required]
                  />
                </v-col>
              </template>
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
    <syndic-order-file-upload-modal ref="orderFileUploadModal" :parent-data="data" @reloadData="getDocumentsData" @setDocumentCollectionId="setDocumentCollectionId" />

  </div>
</template>

<script>
import SyndicOrderModel from "@/models/syndics/SyndicOrderModel";
import { apiGetOrderById, apiCreateOrder, apiUpdateOrder } from "@/api/orders";
import { apiMetaDataGetOrderKinds } from "@/api/metaData";
import { apiDeleteDocument, apiDownloadDocument, apiGetAllDocumentFiles } from "@/api/documents";
import { bytesToSize, downloadFileFromResponse } from "@/utils";
import SyndicOrderFileUploadModal from "@/components/modals/syndics/orderFileUploadModal.vue";
export default {
  name: "syndicOrderModal",
  components: {
    SyndicOrderFileUploadModal
  },
  props: {
    syndicData: {
      type: Object,
      default: null,
    },
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: Object.assign({}, new SyndicOrderModel()),
      nomenclatures: {
        orderKinds: []
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
        this.data = Object.assign({}, new SyndicOrderModel());
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
      apiGetOrderById(this.data.id).then((result) => {
        this.$set(this, "data", result);
        this.getDocumentsData();
        this.isNewDoc = false;
        this.open = true;
      });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          let data = Object.assign({}, this.data);
          data.syndicId = this.syndicData.id;
          apiCreateOrder(data).then((result) => {
            if(result && result !== '00000000-0000-0000-0000-000000000000'){
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
          apiUpdateOrder(this.data).then((result) => {
            if (result && result.id) {
              this.$emit("update");
              if(closeModal)
                this.closeModal();
            }
          });
        }
    },
    getOrderKinds(){
      apiMetaDataGetOrderKinds().then((result) => {
        this.$set(this.nomenclatures, 'orderKinds', result);
      });
    },
    openModal(id = null) {
      if(this.nomenclatures.orderKinds.length === 0)
        this.getOrderKinds();

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
          this.table.data = result.items;
          this.table.pagination.total = result.totalCount;
          this.table.pagination.page = result.currentPage;
        });
      }
    },
    addNewFile(){
      this.$refs.orderFileUploadModal.openModal()
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
