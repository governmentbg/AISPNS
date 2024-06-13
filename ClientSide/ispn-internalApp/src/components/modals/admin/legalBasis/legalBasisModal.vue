<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc"> Добавяне на нормативна уредба </template>
        <template v-else> Редакция на нормативна уредба </template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row class="mt-2">
            <v-col cols="12">
              <base-input
                v-model="data.description"
                label="Наименование"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="3" md="6">
              <base-material-date-picker
                label="Дата"
                v-model="data.date"
                :rules=[rules.required]
                clearable
              />
            </v-col>
            <v-col cols="12" lg="3" md="6">
              <v-checkbox
                v-model="data.isPublished"
                label="Публикувана"
                :true-value="true"
                :false-value="false"
                dense
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
              v-if="!table.data.length"
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
        <v-btn color="primary" outlined class="ml-3" @click="open = false">Затвори</v-btn>
      </v-card-actions>
    </v-card>

    <legal-basis-upload-modal ref="legalBasisUploadModal" :parent-data="data" @reloadData="getDocumentsData" @setDocumentCollectionId="setDocumentCollectionId" />
  </v-dialog>
</template>

<script>
import { apiGetDocumentLegalBasisById, apiCreateDocumentLegalBasis, apiUpdateDocumentLegalBasis } from "@/api/administration";
import { apiDeleteDocument, apiDownloadDocument, apiGetAllDocumentFiles } from "@/api/documents";
import TemplateModel from "@/models/settings/TemplateModel"
import { bytesToSize, downloadFileFromResponse } from "@/utils";
import { eTemplateDropDownTypes } from "@/utils/enums/enumerators";
import LegalBasisUploadModal from "./legalBasisUploadModal.vue";

export default {
  name: "legalBasisModal",
  components: {
    LegalBasisUploadModal
  },
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: Object.assign({}, new TemplateModel()),
      nomenclatures: {
        types: [
          {label: "Молба до ДВ"},
          {label: "Списък на синдици"},
          {label: "Образец/молба за заплщане"},
          {label: "Шаблон на програма за обучение"},
          {label: "Шаблон уведомление на синдик за класиране за обучение"},
          {label: "Шаблон за уведомпление на синдиците за публикуване на програма"},
          {label: "Образец на доклад от проверка"},
        ],
        templateTypes: eTemplateDropDownTypes
      },
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
      table: {
        headers: [
          { title: "Файл", field: "fileName",  sortable: false },
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
        this.data = Object.assign({}, new TemplateModel());
      } else {
        if (this.data.id) {
          this.isNewDoc = false;
        } else {
          this.isNewDoc = true;
        }
      }
    },
  },
  computed: {},
  methods: {
    getData() {
      this.data.id && 
        apiGetDocumentLegalBasisById(this.data.id).then((result) => {
          this.$set(this, "data", result);

          if(this.data.documentCollectionId)
            this.getDocumentsData();

          this.open = true;
        });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          apiCreateDocumentLegalBasis(this.data).then((result) => {
            if(result && result !== '00000000-0000-0000-0000-000000000000') {
              this.$emit("reloadData");
              this.data.id = result;
              this.getData();
              this.isNewDoc = false;
              if(closeModal)    
                this.closeModal();
            }
          });
        } else {
          apiUpdateDocumentLegalBasis(this.data).then((result) => {
            if(result && result !== '00000000-0000-0000-0000-000000000000') {
              this.$emit("reloadData");
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
      }
      this.table.data = [];
      
      this.open = true;

      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "download":
          this.download(rowData);
        break;
        case "delete":
          this.deleteDocumentTemplateFileAsk(rowData);
        break;
      }
    },
    
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
    deleteDocumentTemplateFileAsk(item) {
      let confirmData = {
        title: "Изтриване на файл",
        body: `Сигурни ли сте, че искате да изтриете избрания файл към нормативната уредба?`,
        callback: this.deleteDocumentTemplateFile,
        parameter: item.id,
      };
      this.$emit('confirm', confirmData);
    },
    deleteDocumentTemplateFile(fileId) {
      apiDeleteDocument(fileId).then((result) => {
        if (result) {
          this.getDocumentsData();
          this.$emit('reloadData');
          this.$emit('closeConfirm');
        }
      });
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
    closeModal(){
      this.open = false;
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
    setDocumentCollectionId(documentCollectionId){
      if(documentCollectionId)
        this.$set(this.data, 'documentCollectionId', documentCollectionId);
    },
    addNewFile(){
      this.$refs.legalBasisUploadModal.openModal();
    }

  },
};
</script>