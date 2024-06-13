<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      :heading="isNewDoc ? 'Нова статистика/справка' : 'Преглед на статистика/справка'"
      goBackText="Обратно към статистики и справки"
      goBackTo="/published-statistics"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4">
          Създаденo на: <strong> {{ ISODateString(data.dateCreated) }} </strong> <br />
          Последна промяна: <strong> {{ ISODateString(data.dateModified) }} </strong> <br />
        </v-sheet>
      </v-col>
      <v-col cols="12" lg="7" xl="8" class="text-right">
        <v-btn
          color="primary"
          @click="save"
          v-if="isNewDoc || (!isNewDoc && !data.isPublished)"
        >
          <v-icon left dark>mdi-check</v-icon>
          Запази
        </v-btn>
        <v-btn
          color="secondary"
          @click="publishAsk"
          v-if="!isNewDoc && !data.isPublished"
        >
          <v-icon left dark>mdi-certificate-outline</v-icon>
          Публикувай
        </v-btn>
        <v-btn
          color="primary"
          @click="unpublishAsk"
          v-if="!isNewDoc && data.isPublished"
        >
          <v-icon left dark>mdi-cancel</v-icon>
          Депубликувай
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="my-10">
      <v-col cols="12">
        <base-material-card 
          icon="mdi-chart-line"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Статистика / справка
          </template>
          
          <template v-slot:after-heading-button>
            <v-checkbox
              label="Публикувана"
              v-model="data.isPublished"
              dense
              class="d-inline-block mr-10"
              readonly
            />
          </template>

          <v-container class="py-3">
            <v-form
              ref="form"
              lazy-validation
            >
              <v-row>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-input
                    label="Идент. номер"
                    v-model="data.identificationNumbr"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="7" lg="4" md="6">
                  <base-autocomplete
                    label="Тип"
                    v-model="data.type"
                    :items="nomenclatures.types"
                    :rules="[rules.required]"
                    item-text="name"
                    item-value="id"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="4" md="6">
                  <base-material-date-picker
                    label="Дата на справка"
                    v-model="data.date"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="4" md="6">
                  <base-material-date-picker
                    label="От дата"
                    v-model="data.fromDate"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="4" md="6">
                  <base-material-date-picker
                    label="До дата"
                    v-model="data.toDate"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="6" lg="4" md="6">
                  <base-autocomplete
                    label="Доставчик на информацията"
                    v-model="data.reportSourceId"
                    :items="nomenclatures.sources"
                    item-text="name"
                    item-value="id"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="4" md="6">
                  <base-material-date-picker
                    label="Дата на публикуване"
                    v-model="data.published"
                    readonly
                  />
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12">
                  <v-textarea
                    label="Бележки"
                    v-model="data.note"
                    rows="2"
                    auto-grow
                    :readonly="isReadonly"
                  />
                </v-col>
              </v-row>
            </v-form>
          </v-container>
        </base-material-card>

        <base-material-card 
          icon="mdi-file-multiple-outline"
          color="primary"
          class="elevation-3 mt-10"
          v-if="!isNewDoc"
        >
          <template v-slot:after-heading>
            Файлове
          </template>
          
          <template v-slot:after-heading-button>
            <v-btn
              class="primary"
              @click="addNewFile"
              v-if="table.data.length === 0"
            >
              <v-icon left dark>mdi-plus</v-icon>
              Нов файл
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="table.headers"
            :items="table.data"
            :pagination="table.pagination"
            @getData="getUploadedFilesData"
            @click="tableActions"
          />
        </base-material-card>
      </v-col>
    </v-row>
    <published-statistics-upload-file-modal ref="publishedStatisticsUploadFileModal" :parent-data="data" @reloadData="getUploadedFilesData" @setDocumentCollectionId="setDocumentCollectionId" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, downloadFileFromResponse, bytesToSize } from '@/utils';
import { apiGetStatisticsAndReportById, apiCreateStatisticsAndReport, apiUpdateStatisticsAndReport, apiPublishStatisticalReport, apiUnpublishStatisticalReport } from "@/api/statisticsAndReports";
import { apiMetaDataGetReportSources, apiMetaDataGetReportTypes } from "@/api/metaData";
import { apiDownloadDocument, apiGetAllDocumentFiles, apiDeleteDocument } from "@/api/documents"
import PublishedStatisticsModel from "@/models/publishedStatistics/PublishedStatisticsModel";

import { PublishedStatisticsUploadFileModal } from "@/components";
import { readonly } from 'vue';

export default {
  name: "publishedStatisticsPreview",
  components: {
    PublishedStatisticsUploadFileModal
  },
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {
        types: [],
        sources: [],
      },
      data: Object.assign({}, new PublishedStatisticsModel()),
      table: {
        headers: [
          { title: "Файл", field: "fileName", sortable: false },
          { title: "Размер", field: "fileSize", cell: this.renderFileSize, sortable: false },
          { title: "Качен на", field: "blobDateCreated", type: 'date', format: "{0:dd.MM.yyyy}", sortable: false, width: '100px' },
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
                title: "Изтриване",
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
        required: v => !!v || 'Полето е задължително.',
        requiredSelect: v => (typeof v === 'string' && v.length) || typeof v === 'boolean' || !isNaN(parseFloat(v)) && v >= 0 && v <= 999 || "Полето е задължително",
      },
    }
  },
  mounted(){
    this.getReportSources();
    this.getReportTypes();
    this.isNewDoc = true;
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;

      this.getData();
    }
  },
  computed: {
    ...mapGetters([
      'currentUser'
    ]),
    isReadonly(){
      return !this.isNewDoc && this.data.isPublished;
    }
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetStatisticsAndReportById(this.data.id).then(result => {
          this.$set(this, "data", result);

          if(this.data.isPublished){
            this.table.headers[this.table.headers.length - 1].width = "50px";
            this.table.headers[this.table.headers.length - 1].buttons = [
              {
                title: "Свали",
                icon: "mdi-download",
                color: "white",
                class: "primary",
                click: "download",
              }
            ]
          }

          if(this.data.documentCollectionId)
            this.getUploadedFilesData();
        })
    },
    getReportSources(){
      apiMetaDataGetReportSources().then(data => {
        this.nomenclatures.sources = data;
      })
    },
    getReportTypes(){
      apiMetaDataGetReportTypes().then(data => {
        this.nomenclatures.types = data;
      })
    },
    save(){
      if(this.$refs.form.validate()){
        if(this.isNewDoc){
          apiCreateStatisticsAndReport(this.data).then(result => {
            if(result && result.length && result.indexOf('00000000-0000-') === -1)
              this.$router.push({path: `/published-statistics/${result}`})
          })
        } else {
          apiUpdateStatisticsAndReport(this.data)
        }
      }
    },
    publishAsk(){
      let confirmData = {
        title: "Публикуване на статистика/справка",
        body: `Сигурни ли сте, че искате да публикувате статистиката/справката?`,
        callback: this.publish
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    publish(){
      apiPublishStatisticalReport(this.data.id).then(result => {
        if(result){
          this.getData();
          this.$refs.confirmModal.closeModal();
        }
      })
    },
    unpublishAsk(){
      let confirmData = {
        title: "Депубликуване на статистика/справка",
        body: `Сигурни ли сте, че искате да депубликувате статистиката/справката?`,
        callback: this.unpublish
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    unpublish(){
      apiUnpublishStatisticalReport(this.data.id).then(result => {
        if(result){
          this.getData();
          this.$refs.confirmModal.closeModal();
        }
      })
    },
    setDocumentCollectionId(documentCollectionId){
      this.$set(this.data, "documentCollectionId", documentCollectionId);
    },
    getUploadedFilesData(){
      if(this.data.documentCollectionId){
        let query = Object.assign({}, {});
        query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;

        query.pageSize = this.table.pagination.take;
        query.filter = [this.data.documentCollectionId]
        apiGetAllDocumentFiles(query).then(result => {
          this.$set(this.table, "data", result.items);
          this.$set(this.table.pagination, "total", result.totalCount);
          this.$set(this.table.pagination, "page", result.currentPage);
        })
      }
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "delete":
          this.deleteAsk(rowData);
        break;
        case "download":
          this.download(rowData);
        break;
      }
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
    deleteAsk(fileData){
      let confirmData = {
        title: "Изтриване на файл",
        body: `Сигурни ли сте, че искате да изтриете избрания файл?`,
        callback: this.deleteFile,
        parameter: fileData.id
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteFile(fileId){
      apiDeleteDocument(fileId).then(result => {
        if(result){
          this.getUploadedFilesData();
          this.$refs.confirmModal.closeModal();
        }
      })
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
    addNewFile(){
      this.$refs.publishedStatisticsUploadFileModal.openModal()
    },

    ISODateString: ISODateString
  }
}
</script>