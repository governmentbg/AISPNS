<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc"> Добавяне на шаблон </template>
        <template v-else> Редакция на шаблон </template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row class="mt-2">
            <v-col cols="12">
              <base-input
                v-model="data.description"
                label="Шаблон наименование"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12">
              <base-autocomplete
                label="Тип шаблон"
                v-model="data.type"
                :items="nomenclatures.templateTypes"
                item-text="label"
                :rules=[rules.required]
                clearable
              />
            </v-col>
            <v-col cols="12" v-if="false">
              <v-file-input
                label="Файл"
                v-model="data.file"
                dense
                :rules=[rules.required]
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

    <template-file-upload-modal ref="fileUploadModal" :parent-data="data" @reloadData="getDocumentsData" @setDocumentCollectionId="setDocumentCollectionId" />
  </v-dialog>
</template>

<script>
import { apiGetTemplateDocumentById, apiCreateTemplateDocument, apiUpdateTemplateDocument } from "@/api/administration";
import { apiDeleteDocument, apiDownloadDocument, apiGetAllDocumentFiles } from "@/api/documents";
import TemplateModel from "@/models/settings/TemplateModel"
import { bytesToSize, downloadFileFromResponse } from "@/utils";
import { eTemplateDropDownTypes } from "@/utils/enums/enumerators";
import TemplateFileUploadModal from "@/components/modals/admin/templates/templateFileUploadModal.vue";

export default {
  name: "templateModal",
  components: {
    TemplateFileUploadModal
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
        apiGetTemplateDocumentById(this.data.id).then((result) => {
          this.$set(this, "data", result);

          if(this.data.documentCollectionId)
            this.getDocumentsData();

          this.open = true;
        });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          apiCreateTemplateDocument(this.data).then((result) => {
            if(result && result.id) {
              this.$emit("reloadData");
              this.$set(this, "data", result);
              this.isNewDoc = false;
              if(closeModal)    
                this.closeModal();
            }
          });
        } else {
          apiUpdateTemplateDocument(this.data).then((result) => {
            if(result && result !== '00000000-0000-0000-0000-000000000000') {
              this.$emit("reloadData");
              if(closeModal)    
                this.closeModal();
            }
          });
        }
    },
    getTemplateTypes(){
      // apiGetTemplateTypes().then(result => {
      //   this.$set(this.nomenclatures, "types", result)
      // })
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
        title: "Изтриване на файл към образец",
        body: `Сигурни ли сте, че искате да изтриете избрания файл към образец?`,
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
      this.$refs.fileUploadModal.openModal();
    }

  },
};
</script>