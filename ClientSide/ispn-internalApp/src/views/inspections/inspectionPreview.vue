<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isInspector"
  >
    <base-v-component
      :heading="isNewDoc ? 'Добавяне на инспекция' : 'Преглед на инспекция'"
      goBackText="Обратно към инспекции"
      goBackTo="/inspections"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4">
          Създаден на: <strong> {{ ISODateString(data.dateCreated) }} </strong> <br />
          Последна промяна: <strong> {{ ISODateString(data.dateModified) }} </strong> <br />
        </v-sheet>
      </v-col>
      <v-col cols="12" lg="7" xl="8" class="text-right">
        <v-btn
          color="primary"
          @click="save"
        >
          <v-icon left dark>mdi-check</v-icon>
          Запази
        </v-btn>
        <v-btn
          color="secondary"
          @click="generateReportTemplate"
          v-if="!isNewDoc"
        >
          <v-icon left dark>mdi-file</v-icon>
          Генериране на доклад по образец
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
            Инспекция
          </template>
          

          <v-container class="py-3">
            <v-form
              ref="form"
              lazy-validation
            >
              <v-row>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-material-date-picker
                    label="Дата"
                    v-model="data.inspectionDate"
                    :rules="[rules.required]"
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-syndic-autocomplete
                    label="Синдик"
                    v-model="data.syndicId"
                    :items="nomenclatures.syndics"
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-autocomplete
                    label="Тип инспекция"
                    v-model="data.inspectionTypeId"
                    :items="nomenclatures.inspectionTypes"
                    item-text="description"
                    item-value="id"
                    :rules="[rules.requiredSelect]"
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-input
                    label="Инспектор"
                    v-model="data.inspectorName"
                    :rules="[rules.required]"
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-material-date-picker
                    label="Заповед за проверка дата"
                    v-model="data.inspectionOrderDate"
                    :rules="[rules.required]"
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-input
                    label="Заповед за проверка номер"
                    v-model="data.inspectionOrderNumber"
                    :rules="[rules.required]"
                  />
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <v-checkbox 
                    label="Приключена проверка"
                    v-model="data.isCompleted"
                    :true-value="true"
                    :false-value="false"
                    dense
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <base-material-date-picker 
                    label="Дата на приключване"
                    v-model="data.completionTime"
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
            >
              <v-icon left dark>mdi-plus</v-icon>
              Нов файл
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="table.headers"
            :items="table.data"
            :pagination="table.pagination"
            sortable
            :sort.sync="table.sort"
            @getData="getDocumentsData"
            @click="tableActions"
          />
        </base-material-card>
      </v-col>
    </v-row>

    <inspection-file-upload-modal ref="inspectionFileUploadModal" :parent-data="data" @update="getDocumentsData" @reloadData="getDocumentsData" @setDocumentCollectionId="setDocumentCollectionId" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, bytesToSize, downloadFileFromResponse } from '@/utils';
import InspectionModel from '@/models/inspections/InspectionModel';
import { InspectionFileUploadModal } from "@/components";
import { apiCreateInspection, apiGenerateSampleReportForInspection, apiGetInspectionById, apiUpdateInspection } from "@/api/inspections";
import { apiMetaDataGetInspections, apiMetaDataGetSyndics } from "@/api/metaData";
import { apiDeleteDocument, apiDownloadDocument, apiGetAllDocumentFiles } from '@/api/documents';
export default {
  name: "inspectionPreview",
  components: {
    InspectionFileUploadModal
  },
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {
        inspectionTypes: [],
        syndics: [],
      },
      data: Object.assign({}, new InspectionModel()),
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
        required: v => !!v || 'Полето е задължително.',
        requiredSelect: v => !!v || (typeof v === 'string' && v.length) || typeof v === 'boolean' || !isNaN(parseFloat(v)) && v >= 0 && v <= 999 || "Полето е задължително",
      },
    }
  },
  mounted(){
    this.isNewDoc = true;
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
      this.getData();
    } else {
      this.data.inspector = this.currentUser.firstAndLastName;
    }

    this.getSyndics();
    this.getInspectionTypes();

  },
  computed: {
    ...mapGetters([
      'isInspector',
      'currentUser'
    ]),
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetInspectionById(this.data.id).then(result => {
          this.$set(this, 'data', result);
          this.getSyndics();
          this.getDocumentsData();
          this.isNewDoc = false;
          this.$refs.form.resetValidation();
        })
    },
    save(){
      if(this.$refs.form.validate()){
        if(this.isNewDoc){
          apiCreateInspection(this.data).then(result => {
            if(result && result !== '00000000-0000-0000-0000-000000000000')
              this.$router.push({path: `/inspections/${result}`})
          })
        } else {
          apiUpdateInspection(this.data)
        }
      }
    },
    getInspectionTypes(){
      apiMetaDataGetInspections().then(result => {
        this.$set(this.nomenclatures, "inspectionTypes", result);
      });
    },
    getSyndics(){
      if(!this.isNewDoc){
        let query = {pageSize: 10, pageNumber: 1, searchCriteria: null, syndicId: this.data.syndicId}
        apiMetaDataGetSyndics(query).then((result) => {
          this.$nextTick(() => {
            this.$set(this.nomenclatures, "syndics", result.items);
          })
        });
      } else {
        apiMetaDataGetSyndics().then(result => {
          this.$set(this.nomenclatures, "syndics", result.items);
        
        })
      }
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "delete":
          this.deleteDocumentFileAsk(rowData);
        break;
        case "download":
          this.download(rowData);
        break;
      }
    },
    generateReportTemplate(){
      apiGenerateSampleReportForInspection(this.data.id).then(result => {
        downloadFileFromResponse(result);
      })
    },

    

    setDocumentCollectionId(documentCollectionId){
      if(documentCollectionId)
        this.$set(this.data, 'documentCollectionId', documentCollectionId);
    },
    getDocumentsData(){
      if(this.data.documentCollectionId){
        let query = Object.assign({}, {});
        
        query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
        query.pageSize = this.table.pagination.take;
        query.filter = [this.data.documentCollectionId]
        
        apiGetAllDocumentFiles(query).then((result) => {
          this.table.data = result.items;
          this.table.pagination.total = result.totalCount;
          this.table.pagination.page = result.currentPage;
        });
      }
    },
    addNewFile(){
      this.$refs.inspectionFileUploadModal.openModal()
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
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

    ISODateString: ISODateString
  }
}
</script>