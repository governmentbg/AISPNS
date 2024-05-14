<template>
  <div>
    <v-dialog v-model="open" width="70%" scrollable>
      <v-card>
        <v-card-title class="headline">
          {{ modalTitle }}
        </v-card-title>

        <v-card-text>
          <v-form ref="form">
            <v-row>
              <v-col cols="12" class="text-right" v-if="type === 'directive28'">
                <v-checkbox
                  label="Публикуван"
                  v-model="data.isPublished"
                  :true-value="true"
                  :false-value="false"
                  class="my-0 py-0 d-inline-block"
                  dense
                />
              </v-col>
              <v-col cols="12" lg="8">
                <base-autocomplete
                  label="Вид образец"
                  v-model="data.directiveTemplateKindId"
                  :items="nomenclatures.kinds"
                  item-text="description"
                  item-value="id"
                  :rules=[rules.required]
                  clearable
                  v-if="type === 'directive28'"
                />
                <base-autocomplete
                  label="Вид образец"
                  v-model="data.templateKindId"
                  :items="nomenclatures.kinds"
                  item-text="description"
                  item-value="id"
                  :rules=[rules.required]
                  clearable
                  v-else-if="type === 'syndicTemplates'"
                />
                <base-autocomplete
                  label="Вид образец"
                  v-model="data.reportTypeId"
                  :items="nomenclatures.reportTypes"
                  item-text="name"
                  item-value="id"
                  :rules=[rules.required]
                  clearable
                  v-else-if="type === 'syndicReports'"
                />
              </v-col>
              <v-col cols="12" lg="4">
                <base-material-date-picker
                  label="Дата"
                  v-model="data.date"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Описание"
                  v-model="data.templateName"
                  rows="2"
                  auto-grow
                  :rules=[rules.required]
                />
              </v-col>
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
            </v-row>
          </v-form>
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="save(false)"> Запази </v-btn>
          <v-btn color="secondary" @click="save(true)"> Запази и затвори </v-btn>
          <v-btn color="primary" outlined class="ml-3" @click="open = false">Затвори</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <document-template-file-upload-modal ref="documentTemplateFileUploadModal" :parent-data="data" :type="type" @reloadData="getDocumentsData(true)" @setDocumentCollectionId="setDocumentCollectionId" /> 
  </div>
</template>

<script>
import { downloadFileFromResponse, bytesToSize } from "@/utils"
import DocumentTemplateModel from "@/models/settings/DocumentTemplateModel";
import { apiMetaDataGetDirectiveTemplateKinds, apiMetaDataGetNomCaseReports, apiMetaDataGetSyndicTemplateKinds } from "@/api/metaData";
import { apiGetAllDocumentFiles, apiDownloadDocument, apiDeleteDocument } from "@/api/documents";
import { 
  apiGetTemplateArticles28ById, 
  apiCreateTemplateArticle28, 
  apiUpdateTemplateArticle28, 
  apiGetAdminTemplateById, 
  apiCreateAdminTemplate, 
  apiUpdateAdminTemplate,
  apiGetReportTemplateById,
  apiCreateReportTemplate,
  apiUpdateReportTemplate
} from "@/api/administration";
import DocumentTemplateFileUploadModal from "@/components/modals/admin/documentTemplates/documentTemplateFileUploadModal.vue";

export default {
  name: "documentTemplateModal",
  components: {
    DocumentTemplateFileUploadModal
  },
  props: {
    type: {
      type: String,
      default: 'syndicTemplates'
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: Object.assign({}, new DocumentTemplateModel()),
      nomenclatures: {
        kinds: [],
        reportTypes: []
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
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = Object.assign({}, new DocumentTemplateModel());
        this.table.data = [];
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
    modalTitle(){
      if(this.isNewDoc){
        switch(this.type){
          case 'syndicTemplates':
            return "Нов образец синдик"
          case 'directive28':
            return "Нов образец по чл.28 от Директивата"
          case 'others':
            return "Нов образец"
          default:
            return "Нов образец";
        }
      } else {
        switch(this.type){
          case 'syndicTemplates':
            return "Преглед на образец синдик"
          case 'directive28':
            return "Преглед на образец по чл.28 от Директивата"
          case 'others':
            return "Преглед на образец"
          default:
            return "Преглед на образец";
        }
      }
    }
  },
  methods: {
    getData() {

      if(this.type === "directive28" && this.data.id){
        apiGetTemplateArticles28ById(this.data.id).then((result) => {
          this.$set(this, "data", result);

          if(this.data.documentCollectionId)
            this.getDocumentsData();

          this.open = true;
          this.isNewDoc = false;
        });
      } else if(this.type === "syndicTemplates" && this.data.id){
        apiGetAdminTemplateById(this.data.id).then((result) => {
          this.$set(this, "data", result);

          if(this.data.documentCollectionId)
            this.getDocumentsData();

          this.open = true;
          this.isNewDoc = false;
        });
      } else if(this.type === "syndicReports" && this.data.id){
        apiGetReportTemplateById(this.data.id).then((result) => {
          this.$set(this, "data", result);

          if(this.data.documentCollectionId)
            this.getDocumentsData();

          this.open = true;
          this.isNewDoc = false;
        });
      }
    },
    getDirectiveTemplateKinds(){
      apiMetaDataGetDirectiveTemplateKinds().then(result => {
        this.$set(this.nomenclatures, "kinds", result)
      })
    },
    getSyndicTemplateKinds(){
      apiMetaDataGetSyndicTemplateKinds().then(result => {
        this.$set(this.nomenclatures, "kinds", result)
      })
    },
    getSyndicReportTypes(){
      apiMetaDataGetNomCaseReports().then(result => {
        this.$set(this.nomenclatures, "reportTypes", result)
      })
    },
    getTemplateKinds(){
      switch(this.type){
        case 'syndicTemplates': 
          this.getSyndicTemplateKinds();
        break;
        case 'syndicReports':
          this.getSyndicReportTypes();
        break;
        case 'directive28':
          this.getDirectiveTemplateKinds()
        break;
      }
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        switch(this.type){
          case 'syndicTemplates':
            if (this.isNewDoc) {
              apiCreateAdminTemplate(this.data).then((result) => {
                if (result) {
                  this.$emit("reloadData");
                  if(closeModal){
                    this.closeModal();
                  } else {
                    this.data.id = result;
                    this.getData();
                  }
                }
              });
            } else {
              apiUpdateAdminTemplate(this.data).then((result) => {
                if (result) {
                  this.$emit("reloadData");
                  if(closeModal)    
                    this.closeModal();
                }
              });
            }
          break;
          case 'directive28':
            if (this.isNewDoc) {
              apiCreateTemplateArticle28(this.data).then((result) => {
                if (result) {
                  this.$emit("reloadData");
                  if(closeModal){
                    this.closeModal();
                  } else {
                    this.data.id = result;
                    this.getData();
                  }
                }
              });
            } else {
              apiUpdateTemplateArticle28(this.data).then((result) => {
                if (result) {
                  this.$emit("reloadData");
                  if(closeModal)    
                    this.closeModal();
                }
              });
            }
          break;
          case 'syndicReports':
            if (this.isNewDoc) {
              apiCreateReportTemplate(this.data).then((result) => {
                if (result) {
                  this.$emit("reloadData");
                  if(closeModal){
                    this.closeModal();
                  } else {
                    this.data.id = result;
                    this.getData();
                  }
                }
              });
            } else {
              apiUpdateReportTemplate(this.data).then((result) => {
                if (result) {
                  this.$emit("reloadData");
                  if(closeModal)    
                    this.closeModal();
                }
              });
            }
          break;
        }
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
    deleteDocumentTemplateFileAsk(item) {
      let confirmData = {
        title: "Изтриване на образец",
        body: `Сигурни ли сте, че искате да изтриете избрания образец?`,
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
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
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
    openModal(id = null) {
      this.getTemplateKinds();
      if(id){
        this.data.id = id;
        this.getData();
      }

      if(this.$refs.form)
        this.$refs.form.resetValidation();
      this.open = true;
    },
    closeModal(){
      this.open = false;
    },
    addNewFile(){
      this.$refs.documentTemplateFileUploadModal.openModal()
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
  },
};
</script>