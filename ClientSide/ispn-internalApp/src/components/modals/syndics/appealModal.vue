<template>
  <div>
    <v-dialog v-model="open" width="70%" scrollable>
      <v-card>
        <v-card-title class="headline">
          <template v-if="isNewDoc">Ново обжалване</template>
          <template v-else>Преглед на обжалване</template>
        </v-card-title>

        <v-card-text>
          <v-form ref="form">
            <v-row>
              <v-col cols="12" xl="4" lg="6">
                <base-input
                  label="Заповед номер"
                  v-model="data.orderNumber"
                  :rules="[rules.required]"
                />
              </v-col>
              <v-col cols="12" xl="4" lg="6">
                <base-material-date-picker
                  label="Заповед дата"
                  v-model="data.orderDate"
                  :rules="[rules.required]"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" xl="4" lg="6">
                <base-input
                  label="Жалба номер"
                  v-model="data.pleaNumber"
                  :rules="[rules.required]"
                />
              </v-col>
              <v-col cols="12" xl="4" lg="6">
                <base-material-date-picker
                  label="Жалба дата"
                  v-model="data.pleaDate"
                  :rules="[rules.required]"
                />
              </v-col>
              <v-col cols="12">
                <base-textarea
                  label="Решение на ВАС"
                  v-model="data.courtDecision"
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

    <syndic-appeal-file-upload-modal ref="uploadFileModal" :parent-data="data" @reloadData="getDocumentsData" @setDocumentCollectionId="setDocumentCollectionId" />
  </div>
</template>

<script>
//import { apiGetAppealById, apiCreateAppeal, apiUpdateAppeal } from "@/api/appeals"
import { apiDeleteDocument, apiDownloadDocument, apiGetAllDocumentFiles } from "@/api/documents";
import { apiGetSyndicPleaById, apiCreatePlea, apiUpdatePlea } from "@/api/syndics"
import SyndicAppealFileUploadModal from "@/components/modals/syndics/syndicAppealFileUploadModal.vue";
import { bytesToSize, downloadFileFromResponse } from "@/utils";
export default {
  name: "syndicAppealModal",
  components: {
    SyndicAppealFileUploadModal
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
      isNewDoc: true,
      data: {},
      rules: {
        required: (v) => !!v || "Полето е задължително",
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
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.$set(this, 'data', {});
        this.$set(this.table, 'data', []);
      } else {
        if (this.data.id) {
          this.isNewDoc = false;
        } else {
          this.isNewDoc = true;
        }
      }
    },
  },
  computed: {
    
  },
  methods: {
    getData() {
      apiGetSyndicPleaById(this.data.id).then((result) => {
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

          apiCreatePlea(data).then((result) => {
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
          apiUpdatePlea(this.data).then((result) => {
            if (result && result.id) {
              this.$set(this, "data", result);
              this.$emit("update");
              if(closeModal)
                this.closeModal();
            }
          });
        }
    },
    openModal(id = null) {
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
          this.$emit('update');
        
        apiGetAllDocumentFiles(query).then((result) => {
          this.$set(this.table, 'data', result.items);
          this.$set(this.table.pagination, 'total', result.totalCount);
          this.$set(this.table.pagination, 'page', result.currentPage);
        });
      }
    },
    addNewFile(){
      this.$refs.uploadFileModal.openModal()
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
          this.$emit('update');
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