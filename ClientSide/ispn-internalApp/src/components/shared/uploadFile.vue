<template>
  <base-material-card
    color="primary"
    :icon="icon ? 'mdi-text-box-plus-outline' : ''"
    :inline="icon"
    :class="`${icon ? 'px-5 mt-10' : 'px-0 elevation-0'}`"
    style="padding-bottom: 43px !important"
  >
    <template v-slot:after-heading v-if="icon || !icon && noIconWithTitle">
      <div :class="`display-2 ${!icon && noIconWithTitle ? 'font-weight-normal' : 'font-weight-light'}`">Прикачени файлове</div>
    </template>

    <v-form v-if="hasAccess">
      <v-row>
        <v-col cols="12" xl="3" order-xl="1" lg="4" order-lg="1" :md="showDocumentTypes ? '12' : '6'" order-md="1">
          <v-file-input
            @click:clear="disableUpload"
            v-model="currentFile"
            show-size
            label="Избери файл"
            color="secondary"
            dense
            :disabled="disabled"
          >
            <v-tooltip slot="append" top>
              <template v-slot:activator="{ on, attrs }">
                <v-icon color="success" v-bind="attrs" v-on="on">
                  mdi-information-outline
                </v-icon>
              </template>
              <span>Моля прикачете файл</span>
            </v-tooltip>
          </v-file-input>
        </v-col>
        <v-col cols="12" xl="3" order-xl="2" lg="4" order-lg="2" md="6" order-md="2" v-if="showDocumentTypes">
          <base-select
            v-model="attachedDocumentTypeId"
            label="Тип на документа"
            class="pb-0"
            :items="nomenclatures.documentTypes"
            item-text="name"
            item-value="id"
            color="secondary"
            :disabled="disabled"
          />
        </v-col>
        <v-col cols="12" xl="3" order-xl="3" lg="4" order-lg="3" md="6" order-md="3">
          <base-select
            v-model="attachedDocumentKindId"
            label="Видове документи"
            class="pb-0"
            :items="nomenclatures.documentKinds"
            item-text="name"
            item-value="id"
            color="secondary"
            :disabled="disabled"
          />
        </v-col>
        <v-col 
          cols="12" 
          xl="3" 
          order-xl="4" 
          offset-xl="0" 
          lg="4" 
          :order-lg="showDocumentTypes ? 5 : 3" 
          :offset-lg="showDocumentTypes ? 4 : 0" 
          md="4" 
          order-md="5" 
          offset-md="4"
        >
          <v-btn
            color="primary"
            :disabled="btnDisabled || disabled"
            block
            @click="uploadCurrentFile"
          >
            Прикачи файл
          </v-btn>
        </v-col>
        <v-col cols="12" order-lg="4" order-md="4">
          <base-textarea
            label="Описание"
            v-model="fileDescription"
            :disabled="disabled"
          />
        </v-col>
      </v-row>
    </v-form>

    <base-kendo-grid 
      :columns="table.headers"
      :items="uploadedDocuments"
      :pagination="table.pagination"
      @click="tableActions"
      @dblclick="downloadAttachedFile"
      :styleClass="hasAccess ? 'mt-5' : ''"
      :disabled="disabled"
    />

    <base-confirm-modal ref="confirmModal" />
  </base-material-card>
</template>

<script>
import {
  uploadFile,
  downloadAttachedFile,
  deleteDocument,
} from "@/api/file-upload";
import { formatDateTime, downloadFileFromResponse } from "@/utils";
import { getAttachedDocumentKind } from "@/api/metaData";

export default {
  components: {
  },
  props: {
    typeOfFile: {
      type: String,
      default: "",
    },
    documentId: {
      type: String,
      default: "",
    },
    dataId: {
      type: String,
      default: "",
    },
    documentContentType: {
      type: String,
      default: "",
    },
    getDataFunction: {
      type: Function,
      default: null,
    },
    downloadFileFunction: {
      type: Function,
      default: null,
    },
    documentTypes: {
      type: Array,
      default: null,
    },
    tableDocuments: {
      type: Array,
      default: () => [],
    },
    disabled: {
      type: Boolean,
      default: false
    },
    documentKinds: {
      type: Array,
      default: () => {
        return null
      }
    },
    icon: {
      type: Boolean,
      default: true
    },
    noIconWithTitle: {
      type: Boolean,
      default: false
    },
    hasAccess: {
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      attachedDocumentKindId: null,
      attachedDocumentTypeId: null,
      attachedId: null,
      attachedDocumentId: null,
      btnDisabled: true,
      currentFile: null,
      fileDescription: "",
      nomenclatures: {
        documentKinds: [],
        documentTypes: []
      },
      documentTypeName: null,
      table: {
        headers: [],
        pagination: {
          page: 1,
          skip: 0,
          take: 10,
          perPageOptions: [5, 10, 25, 50, 100],
        }
      }
    };
  },
  mounted() {
    this.nomenclatures.documentTypes = this.documentTypes
    this.table.headers = [
      { title: "Описание", field: "description" },
      { title: "Име на файл", field: "fileName" },
      { title: "Тип на документа", field: "attachedDocumentType", cell: this.getDocumentTypeName },
      { title: "Вид на документа", field: "attachedDocumentKindId", cell: this.getAttachedDocumentKindName },
      { title: "Дата", field: "documentDate", type: "date", format: "{0:dd.MM.yyyyг. HH:mmч.}" },
      { title: "", cell: "actions", filterable: false, width: '75px', hidden: !this.hasAccess, buttons: [
        { title: 'Свали файла', icon: 'mdi-download', color: 'white', class: 'primary mr-1', click: 'download' },
        { title: 'Изтрий', icon: 'mdi-trash-can-outline', color: 'grey', click: 'delete', disabled: this.disabled }
      ]},
    ]
    this.getDocumentKind();
  },
  computed: {
    uploadedDocuments(){
      return this.tableDocuments.slice(this.table.pagination.skip, this.table.pagination.take + this.table.pagination.skip);
    },
    showDocumentTypes(){
      return this.nomenclatures.documentTypes.length > 0;
    }
  },
  watch: {
    currentFile() {
      if (this.currentFile != null && this.attachedDocumentTypeId != null) {
        this.btnDisabled = false;
      }
    },
    attachedDocumentTypeId() {
      if (this.attachedDocumentTypeId != null) {
        this.attachedId = this.$props.documentTypes.find(
          (x) => x.id == this.attachedDocumentTypeId
        ).pk;
      }

      if (
        this.currentFile != null &&
        this.attachedDocumentTypeId != null &&
        this.attachedDocumentKindId != null
      ) {
        this.btnDisabled = false;
      }
    },
    attachedDocumentKindId() {
      if (
        this.currentFile != null &&
        this.attachedDocumentTypeId != null &&
        this.attachedDocumentKindId != null
      ) {
        this.btnDisabled = false;
      }

      if (this.nomenclatures.documentTypes.length == 1) {
        this.btnDisabled = false;
      }
    },
    tableDocuments(items){
      this.table.pagination.total = items.length
    }
  },
  methods: {
    formatDateTime,
    disableUpload() {
      this.btnDisabled = true;
    },
    getDocumentKind() {
      if(!this.documentKinds){
        if(!this.nomenclatures.documentKinds.length)
          getAttachedDocumentKind().then((response) => {
            this.nomenclatures.documentKinds = response;
          });
      } else {
        this.nomenclatures.documentKinds = this.documentKinds;
      }
    },
    downloadAttachedFile(rowData) {
      downloadAttachedFile(rowData.id).then((response) => {
        downloadFileFromResponse(response);
      });
    },
    confirmDeleteFile(fileId) {
      let confirmData = {
        title: "Изтриване на файл",
        body: `Сигурни ли сте, че искате да изтриете този файл?`,
        callback: this.deleteDocumentFile,
        parameter: fileId
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteDocumentFile(fileId) {
      deleteDocument(fileId).then(() => {
        this.$refs.confirmModal.closeModal();

        this.getDataFunction();
      });
    },
    uploadCurrentFile() {
      if (this.currentFile != null) {
        uploadFile(
          this.currentFile,
          this.documentContentType,
          this.dataId,
          this.typeOfFile,
          this.attachedId,
          this.fileDescription,
          this.attachedDocumentKindId,
          this.attachedDocumentTypeId
        ).then((response) => {
          this.tableDocuments.unshift(response);
          // let selectedDocType = this.documentTypes.find(
          //   (dt) => dt.id == this.attachedDocumentTypeId
          // );

          // // if we have new document collection and dont have pk's in doc types we will refresh doc list
          // if (selectedDocType.pk == null) {
          //   this.getDataFunction();
          // }

          this.getDataFunction();

          this.currentFile = null;
          this.attachedDocumentTypeId = null;
          this.attachedDocumentKindId = null;
          this.fileDescription = "";
        });
      }
    },
    getDocumentTypeName(h, tdElement , props ) {
      return h('td', null, [
        this.getTypeNameByRow(props.dataItem)
      ]);
    },
    getTypeNameByRow(item) {
      const docType = this.documentTypes.find((x) => x.pk == item.attachedDocumentId);
      return docType != undefined ? docType.name : null;
    },
    getAttachedDocumentKindName(h, tdElement , props ) {
      return h('td', null, [
        this.getDocumentKingByRow(props.dataItem)
      ]);
    },
    getDocumentKingByRow(item){
      const attachedDocumentKind = this.nomenclatures.documentKinds.find(
        (x) => x.id == item.attachedDocumentKindId
      );
      return attachedDocumentKind != undefined
        ? attachedDocumentKind.name
        : null;
    },
    tableActions({action, rowData}){
      switch(action){
        case "download":
          this.downloadAttachedFile(rowData)
        break;
        case "delete":
          this.confirmDeleteFile(rowData.id)
        break;
      }
    }
  },
};
</script>

<style></style>
