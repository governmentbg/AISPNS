<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isSyndic"
  >
    <base-v-component
      :heading="isNewDoc ? 'Ново съобщение' : 'Преглед на съобщение'"
      goBackText="Обратно към съобщения"
      goBackTo="/messages"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4" v-if="false">
          Създаденo на: <strong> {{ ISODateString(data.dateCreated) }} </strong> <br />
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
      </v-col>
    </v-row>
    <v-row class="my-10">
      <v-col cols="12">
        <base-material-card 
          icon="mdi-message-text-outline"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Образец
          </template>

          <v-container class="py-3">
            <v-form
              ref="form"
              lazy-validation
            >
              <v-row>
                <v-col cols="12" xl="4" lg="5" md="6">
                  <base-autocomplete
                    label="Вид"
                    v-model="data.templateKindId"
                    :items="nomenclatures.templateKinds"
                    item-text="name"
                    item-value="id"
                    :rules="[rules.required]"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="4" md="6">
                  <base-material-date-picker
                    label="Дата"
                    v-model="data.date"
                  />
                </v-col>
                <v-col cols="12">
                  <v-textarea
                    label="Описание"
                    v-model="data.description"
                    rows="2"
                    auto-grow
                    :rules="[rules.required]"
                  />
                </v-col>
              </v-row>
            </v-form>
          </v-container>
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
            :pagination="table.pagination"
            @getData="getDocumentsListData"
            @click="tableActions"
          />
        </base-material-card>
      </v-col>
    </v-row>
    <template-upload-file-modal ref="templateUploadFileModal" :parent-data="data" @reloadData="getDocumentsListData" @setDocumentCollectionId="setDocumentCollectionId" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { TemplateUploadFileModal } from "@/components";
import { ISODateString, bytesToSize, downloadFileFromResponse } from '@/utils';
import { apiMetaDataGetSyndicTemplateKinds } from "@/api/metaData";
import { apiGetTemplateById, apiCreateTemplate, apiUpdateTemplate } from "@/api/templates";
import { apiDeleteDocument, apiDownloadDocument, apiGetAllDocumentFiles } from '@/api/documents';
export default {
  name: "templatePreview",
  components: {
    TemplateUploadFileModal
  },
  data(){
    return {
      isNewDoc: true,
      isReply: false,
      nomenclatures: {
        templateKinds: [],
      },
      table: {
        headers: [
          { title: "Файл", field: "fileName", sortable: false },
          { title: "Тип", field: "description", sortable: false },
          { title: "Размер", cell: this.renderFileSize, sortable: false, width: '100px' },
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
                click: "deleteFileAsk",
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
      data: {},
      rules: {
        required: v => !!v || 'Полето е задължително.',
        requiredSelect: v => (typeof v === 'string' && v.length) || typeof v === 'boolean' || !isNaN(parseFloat(v)) && v >= 0 && v <= 999 || "Полето е задължително",
        requiredSelectReturnObjectMultiple: v => v.length > 0 || "Полето е задължително",
        min: v => (v && v.length >= 3) || 'Полето трябва да съдържа поне 3 символа',
        email: v => !v || /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) || 'E-mail-a трябва да бъде валиден адрес.'
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
      this.data = {};
    }

    this.getTemplateKinds();
  },
  computed: {
    ...mapGetters([
      'isSyndic',
      'currentUser'
    ]),
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetTemplateById(this.data.id).then(result => {
          this.$set(this, 'data', result);
          this.getDocumentsListData();
        })
    },
    getTemplateKinds(){
      apiMetaDataGetSyndicTemplateKinds().then(result => {
        this.nomenclatures.templateKinds = result;
      })
    },
    save(){
      if(this.$refs.form.validate()){
        if(this.isNewDoc){
          apiCreateTemplate(this.data).then(result => {
            if(result && result !== '00000000-0000-0000-0000-000000000000')
              this.$router.push({path: `/templates/${result}`})
          })
        } else {
          apiUpdateTemplate(this.data)
        }
      }
    },
    addNewFile(){
      this.$refs.templateUploadFileModal.openModal();
    },
    deleteFileAsk(fileData){
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
          this.getDocumentsListData();
          this.$refs.confirmModal.closeModal();
        }
      })
    },
    getDocumentsListData(){
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
    tableActions({ action, rowData }) {
      switch (action) {
        case "download":
          this.download(rowData);
        break;
        case "deleteFileAsk":
          this.deleteFileAsk(rowData);
        break;
      }
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
    setDocumentCollectionId(documentCollectionId){
      this.data.documentCollectionId = documentCollectionId
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
    ISODateString: ISODateString
  }
}
</script>