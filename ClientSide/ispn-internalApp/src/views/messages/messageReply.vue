<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      heading="Отговор на съобщение"
      goBackText="Обратно към съобщения"
      goBackTo="/messages"
    />

    <v-row class="mt-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          color="primary"
          @click="sendReply"
        >
          <v-icon left dark>mdi-email-arrow-right-outline</v-icon>
          Изпрати
        </v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <v-expansion-panels>
          <v-expansion-panel>
            <v-expansion-panel-header>
              Номер: <strong> {{ data.number }} </strong>
              Дата на изпращане: <strong> {{ formatDateTime(data.sendDate) }} </strong>
              Дата на прочитане: <strong> {{ formatDateTime(data.seenDate) }} </strong>
              Подател: <strong> {{ data.sender.fullName }} </strong>
            </v-expansion-panel-header>
            <v-expansion-panel-content>
              <base-material-card 
                icon="mdi-message-text-outline"
                color="primary"
                class="elevation-3 mt-10"
              >
                <template v-slot:after-heading>
                  Съобщение
                </template>

                <div class="pa-3">
                  <v-row>
                    <v-col cols="12" xl="3" lg="4" md="6">
                      <base-input
                        label="Номер"
                        v-model="data.number"
                        :rules="[rules.required]"
                        readonly
                      />
                    </v-col>
                    <v-col cols="12" xl="2" lg="4" md="6">
                      <base-date-time-picker
                        label="Дата на изпращане"
                        v-model="data.sendDate"
                        readonly
                      />
                    </v-col>
                    <v-col cols="12" xl="2" lg="4" md="6">
                      <base-date-time-picker
                        label="Дата на получаване"
                        v-model="data.seenDate"
                        readonly
                      />
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12" xl="6" lg="4" md="6">
                      <base-syndic-static-autocomplete
                        ref="syndicAutocomplete"
                        :label="syndicsLabel"
                        v-model="syndics"
                        :items="nomenclatures.syndics"
                        text-value="userId"
                        :rules="[rules.required]"
                        multiple
                        do-not-search
                        :clearable="!data.sendDate"
                        readonly
                      />
                    </v-col>
                    <v-col cols="12" xl="6" lg="4" md="6">
                      <base-input
                        label="Email"
                        v-model="data.syndicElectronicAddress"
                        :rules="[rules.required]"
                        readonly
                      />
                    </v-col>
                    <v-col cols="12" xl="3" lg="4" md="6">
                      <base-input
                        label="Подател"
                        v-model="data.sender.fullName"
                        :rules="[rules.required]"
                        readonly
                      />
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12">
                      <base-input
                        label="Относно"
                        v-model="data.subject"
                        :rules="[rules.required]"
                        readonly
                      />
                    </v-col>
                    <v-col cols="12">
                      <v-textarea
                        label="Съобщение"
                        v-model="data.body"
                        rows="4"
                        auto-grow
                        :rules="[rules.required]"
                        readonly
                      />
                    </v-col>
                  </v-row>

                  <base-material-card 
                    icon="mdi-file-multiple-outline"
                    color="primary"
                    class="elevation-3 mt-10"
                  >
                    <template v-slot:after-heading>
                      Файлове
                    </template>

                    <base-kendo-grid
                      :columns="table.headers"
                      :items="table.data"
                      :pagination="table.pagination"
                      sortable
                      :sort.sync="table.sort"
                      @getData="getDocumentFilesData"
                      @click="tableActions"
                    />
                  </base-material-card>
                </div>
              </base-material-card>
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <base-material-card 
          icon="mdi-message-arrow-right-outline"
          color="primary"
          class="elevation-3 mt-10"
        >
          <template v-slot:after-heading>
            Отговор на съобщение
          </template>

          <v-container class="py-3">
            <v-form
              ref="form"
              lazy-validation
            >
              <v-row>
                <v-col cols="12">
                  <base-input
                    label="Относно"
                    v-model="replyData.subject"
                    :rules="[rules.required]"
                  />
                </v-col>
                <v-col cols="12">
                  <v-textarea
                    label="Съобщение"
                    v-model="replyData.body"
                    rows="4"
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
          class="elevation-3 mt-10"
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
            :columns="replyTable.headers"
            :items="replyData.messageAttachments"
            :pagination="replyTable.pagination"
            sortable
            :sort.sync="replyTable.sort"
            @click="tableActions"
          />
        </base-material-card>
      </v-col>
    </v-row>
    <message-file-upload-modal ref="messageFileUploadModal" @reloadData="getDocumentFilesData" @saveFile="saveFile" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, bytesToSize, downloadFileFromResponse, formatDateTime } from '@/utils';

import { MessageFileUploadModal } from "@/components";

import MessageModel from '@/models/messages/MessageModel';
import MessageReplyModel from '@/models/messages/MessageReplyModel';
import { apiGetMessageById, apiReplyToMessage } from '@/api/messages';
import { apiMetaDataGetSyndicsShortInfo } from '@/api/metaData';
import { apiDownloadDocument, apiGetAllDocumentFiles } from '@/api/documents';
import { eDocumentContentTypes } from '@/utils/enums/enumerators';
export default {
  name: "messagePreview",
  components: {
    MessageFileUploadModal
  },
  data(){
    return {
      syndicsLabel: 'До Синдик/ци',
      backupReceivers: [],
      syndics: [],
      nomenclatures: {
        syndics: [],
      },
      data: Object.assign({}, new MessageModel()),
      replyData: Object.assign({}, new MessageReplyModel()),
      
      table: {
        headers: [
          { title: "Файл", field: "fileName", sortable: false },
          { title: "Описание", field: "description", sortable: false },
          { title: "Размер", cell: this.renderFileSize, sortable: false },
          { title: "Качен на", field: "blobDateCreated", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              {
                title: "Свали",
                icon: "mdi-download",
                color: "white",
                class: "secondary",
                click: "download",
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
      replyTable: {
        headers: [
          { title: "Файл", field: "fileName", sortable: false },
          { title: "Описание", field: "description", sortable: false },
          { title: "Размер", cell: this.renderFileSize, sortable: false },
          { title: "Качен на", field: "blobDateCreated", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false, width: '100px' },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              {
                title: "Изтрий",
                icon: "mdi-trash-can-outline",
                color: "white",
                class: "error",
                click: "deleteFileAsk",
              }
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
        requiredSelectReturnObjectMultiple: v => v.length > 0 || "Полето е задължително",
        min: v => (v && v.length >= 3) || 'Полето трябва да съдържа поне 3 символа',
        email: v => !v || /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) || 'E-mail-a трябва да бъде валиден адрес.'
      },
    }
  },
  mounted(){
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;

      this.getData();

      if(this.$route.params.reply === 'reply')
        this.replyData = Object.assign({}, new MessageReplyModel());
    }
  },
  computed: {
    ...mapGetters([
      'isAdministrator',
      'currentUser'
    ]),
  },
  watch: {
    syndics: {
      handler: function(val){
        if(val){
          this.$set(this.data, 'messageReceiverIDs', val.map(item => item.userId));
          let emails = val.map(item => item.email);
          let displayEmails = [];
          for(let email of emails){
            if(email)
              displayEmails.push(email)
          }
          this.$set(this.data, 'syndicElectronicAddress', displayEmails.join(", "));
        } else {
          this.$set(this.data, 'messageReceiverIDs', []);
          this.$set(this.data, 'syndicElectronicAddress', null);
        }
      },
      deep: true
    }
  },
  methods: {
    getData(){
      apiMetaDataGetSyndicsShortInfo().then(syndics => {
        this.$set(this.nomenclatures, 'syndics', syndics);

        apiGetMessageById(this.data.id).then(result => {
          this.$set(this, 'data', result);

          this.$set(this.replyData, "subject", "RE: " + this.data.subject);

          this.syndics = this.nomenclatures.syndics.filter(syndic => {
            return this.data.messageReceiverIDs.includes(syndic.userId);
          });

          this.getDocumentFilesData();
        })
      })
    },
    sendReply(){
      if(this.$refs.form.validate()){
        let formData = new FormData();
        
        formData.append("id", this.data.id);
        formData.append("subject", this.replyData.subject);
        formData.append("body", this.replyData.body);
        
        for(let i=0;i<this.replyData.messageAttachments.length; i++){
          let fileToUpload = {
            file: this.replyData.messageAttachments[i].file,
            description: this.replyData.messageAttachments[i].description,
            documentContentType: eDocumentContentTypes.MESSAGE
          };
          for(let key in fileToUpload){
            formData.append(`files[${i}].${key}`, fileToUpload[key]);
          }
        }

        apiReplyToMessage(formData).then(result => {
          if(result && result.id)
            this.$router.push({path: `/messages/${result.id}`})
        })
      } else {
        this.$snotify.warning("Моля попълнете всички задължителни полета.")
      }
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "download":
          this.download(rowData);
        break;
      }
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      })
    },
    addNewFile(){
      this.$refs.messageFileUploadModal.openModal()
    },
    
    getDocumentFilesData(){
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
        case "download":
          this.download(rowData);
        break;
        case "deleteFileAsk":
          this.deleteFileAsk(rowData);
        break;
      }
    },
    deleteFileAsk(fileData){
      const confirmData = {
        title: "Изтриване на файл",
        body: `Сигурни ли сте, че искате да изтриете избрания файл?`,
        callback: this.deleteDocumentFile,
        parameter: fileData,
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    deleteDocumentFile(fileData){
      if(fileData.id){

      } else {
        let index = this.replyData.messageAttachments.findIndex(i => i.tId === fileData.tId);
        this.replyData.messageAttachments.splice(index, 1);
        this.table.pagination.total = this.replyData.messageAttachments.length;
        this.$refs.confirmModal.closeModal();
      }
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      })
    },
    saveFile(fileData){
      fileData.tId = this.replyData.messageAttachments.length + 1;
      this.replyData.messageAttachments.push(fileData)
      this.replyTable.pagination.total = this.replyData.messageAttachments.length;
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
    ISODateString,
    formatDateTime
  }
}
</script>