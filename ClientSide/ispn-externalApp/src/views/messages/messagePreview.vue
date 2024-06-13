<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
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
          color="secondary"
          @click="send"
          v-if="!isMessageSent"
        >
          <v-icon left dark>mdi-email-arrow-right-outline</v-icon>
          Изпрати
        </v-btn>
        <v-btn
          color="primary"
          @click="$router.push({path: `/messages/${data.id}/reply`})"
          v-if="!isNewDoc && data.replyId === '00000000-0000-0000-0000-000000000000'"
        >
          <v-icon left dark>mdi-reply</v-icon>
          Отговори
        </v-btn>
      </v-col>
    </v-row>
    <v-row v-if="!isNewDoc">
      <v-col cols="12">
        <v-expansion-panels v-model="panels">
          <v-expansion-panel v-for="message in messageReplies" :key="message.id">
            <v-expansion-panel-header @click="clickedOnExpansionPanel(message)" :color="message.id === data.id ? 'primary lighten-5' : ''">
              <template v-if="message.id === data.id">
                <strong>Текущо съобщение :: </strong>
              </template>
              Номер: <strong> {{ message.number }} </strong>
              Дата на изпращане: <strong> {{ formatDateTime(message.sendDate) }} </strong>
              Дата на прочитане: <strong> {{ formatDateTime(message.seenDate) }} </strong>
              Подател: <strong> {{ message.sender?.fullName }} </strong>
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
                        label="message"
                        v-model="data.number"
                        readonly
                      />
                    </v-col>
                    <v-col cols="12" xl="2" lg="4" md="6">
                      <base-date-time-picker
                        label="Дата на изпращане"
                        v-model="message.sendDate"
                        readonly
                      />
                    </v-col>
                    <v-col cols="12" xl="2" lg="4" md="6">
                      <base-date-time-picker
                        label="Дата на прочитане"
                        v-model="message.seenDate"
                        readonly
                      />
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12" xl="6" lg="4" md="6">
                      <base-input
                        ref="syndicAutocomplete"
                        label="До"
                        :value="message.messageReceiverNames.join(', ')"
                        readonly
                      />
                    </v-col>
                    <v-col cols="12" xl="6" lg="4" md="6">
                      <base-input
                        label="Email"
                        v-model="message.syndicElectronicAddress"
                        :rules="[rules.required]"
                        readonly
                      />
                    </v-col>
                    <v-col cols="12" xl="3" lg="4" md="6">
                      <base-input
                        label="Подател"
                        :value="message.sender?.fullName"
                        :rules="[rules.required]"
                        readonly
                      />
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12">
                      <base-input
                        label="Относно"
                        v-model="message.subject"
                        :rules="[rules.required]"
                        readonly
                      />
                    </v-col>
                    <v-col cols="12">
                      <v-textarea
                        label="Съобщение"
                        v-model="message.body"
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
                      :columns="messageRepliesTables[message.id].headers"
                      :items="messageRepliesTables[message.id].data"
                      :pagination="messageRepliesTables[message.id].pagination"
                      sortable
                      :sort.sync="messageRepliesTables[message.id].sort"
                      @getData="getDocumentFilesData(message)"
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
    <v-row class="my-10">
      <v-col cols="12">
        <base-material-card 
          icon="mdi-message-text-outline"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Съобщение
          </template>
          
          <template v-slot:after-heading-button v-if="isMEIEmployee">
            <v-checkbox
              label="До всички синдици"
              v-model="data.sendToAll"
              :true-value="true"
              :false-value="false"
              dense
              class="d-inline-block mr-10"
              @change="setSendToAll"
              :readonly="isMessageSent"
            />
          </template>

          <v-form
            ref="form"
            lazy-validation
            class="pa-3"
          >
            <v-row v-if="!isNewDoc">
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Номер"
                  v-model="data.number"
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
                <template v-if="isNewDoc">
                  <base-syndic-static-autocomplete
                    ref="syndicAutocomplete"
                    :label="recieversLabel"
                    v-model="recievers"
                    :items="nomenclatures.recievers"
                    text-value="userId"
                    :rules="[rules.required]"
                    multiple
                    do-not-search
                    :clearable="!data.sendDate"
                    :readonly="isMessageSent"
                    :disabled="disabledSyndicsField"
                    v-if="isMEIEmployee"
                  />
                  <base-employee-static-autocomplete
                    ref="employeeAutocomplete"
                    :label="recieversLabel"
                    v-model="recievers"
                    :items="nomenclatures.recievers"
                    text-value="userId"
                    :rules="[rules.required]"
                    multiple
                    do-not-search
                    :clearable="!data.sendDate"
                    :readonly="isMessageSent"
                    :disabled="disabledSyndicsField"
                    v-else
                  />
                </template>
                <base-input
                  label="До"
                  :value="data?.messageReceiverNames?.join(', ')"
                  v-else
                />
              </v-col>
              <v-col cols="12" xl="6" lg="4" md="6">
                <base-input
                  label="Email"
                  v-model="data.syndicElectronicAddress"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Подател"
                  v-model="data.sender.fullName"
                  :rules="[rules.required]"
                  multiple
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
                  :readonly="isMessageSent"
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Съобщение"
                  v-model="data.body"
                  rows="4"
                  auto-grow
                  :rules="[rules.required]"
                  :readonly="isMessageSent"
                />
              </v-col>
            </v-row>
          </v-form>
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
              v-if="!isMessageSent"
            >
              <v-icon left dark>mdi-plus</v-icon>
              Нов файл
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="table.headers"
            :items="isNewDoc ? data.messageAttachments : table.data"
            :pagination="table.pagination"
            sortable
            :sort.sync="table.sort"
            @getData="getDocumentFilesData"
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
import { ISODateString, formatDateTime, bytesToSize, downloadFileFromResponse } from '@/utils';

import { MessageFileUploadModal } from "@/components";

import MessageModel from '@/models/messages/MessageModel';
import { apiGetMessageById, apiSendMessage, apiGetAllMessageReplies } from '@/api/messages';
import { apiMetaDataGetSyndicsShortInfo, apiMetaDataGetUserShortInfo } from "@/api/metaData";
import { apiDownloadDocument, apiGetAllDocumentFiles } from '@/api/documents';
import { eDocumentContentTypes } from '@/utils/enums/enumerators';
export default {
  name: "messagePreview",
  components: {
    MessageFileUploadModal
  },
  data(){
    return {
      isNewDoc: true,
      isReply: false,
      messageReplies: [],
      messageRepliesTables: {},
      panels: [],
      nomenclatures: {
        recievers: [],
      },
      recieversLabel: 'До',
      backupReceivers: [],
      recievers: [],
      data: Object.assign({}, new MessageModel()),
      table: {
        headers: [],
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
  watch: {
    recievers: {
      handler: function(val){
        if(this.isNewDoc){
          if(val){
            let receiverIDs = val.map(item => item.userId);
            this.$set(this.data, 'messageReceiverIDs', receiverIDs.filter(item => item != null));
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
        }
      },
      deep: true
    }
  },
  mounted(){
    this.isNewDoc = true;

    const headers = [
      { title: "Файл", field: "fileName", sortable: false },
      { title: "Описание", field: "description", sortable: false },
      { title: "Размер", cell: this.renderFileSize, sortable: false },
      { title: "Качен на", field: "blobDateCreated", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false, width: '100px' },
      {
        title: "",
        cell: "actions",
        filterable: false,
        width: "80px",
        sortable: false,
        buttons: [],
      },
    ]
    
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
      headers[headers.length - 1].width = "50px";
      
      headers[headers.length - 1].buttons = [
        {
          title: "Свали",
          icon: "mdi-download",
          color: "white",
          class: "primary",
          click: "download",
        }
      ]
      this.getData();
    } else {
      if(this.isSyndic){
        this.getEmployees();
      } else {
        this.getSyndics();
      }
      headers[headers.length - 1].width = "50px";
      headers[headers.length - 1].buttons = [
        {
          title: "Изтрий",
          icon: "mdi-trash-can-outline",
          color: "white",
          class: "error",
          click: "deleteFileAsk",
        }
      ]
    }

    this.table.headers = headers;

    if(this.isNewDoc)
      this.generateSender();
  },
  computed: {
    ...mapGetters([
      'currentUser',
      'isMEIEmployee',
      'isSyndic'
    ]),
    isMessageSent(){
      return this.data.sendDate != null
    },
    disabledSyndicsField(){
      return this.data.sendToAll
    }
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        if(this.isMEIEmployee){
          apiMetaDataGetSyndicsShortInfo().then(recievers => {
            this.$set(this.nomenclatures, 'recievers', recievers);

            apiGetMessageById(this.data.id).then(result => {
              this.$set(this, 'data', result);

              this.recievers = this.nomenclatures.recievers.filter(receiver => {
                return this.data.messageReceiverIDs.includes(receiver.userId);
              });

              this.getDocumentFilesData();

              this.getAllMessages();
            })
          })
        } else {
          apiMetaDataGetUserShortInfo().then(recievers => {
            this.$set(this.nomenclatures, 'recievers', recievers);

            apiGetMessageById(this.data.id).then(result => {
              this.$set(this, 'data', result);

              this.recievers = this.nomenclatures.recievers.filter(receiver => {
                return this.data.messageReceiverIDs.includes(receiver.userId);
              });

              this.getDocumentFilesData();

              this.getAllMessages();
            })
          })
        }
    },
    getSyndics(){
      apiMetaDataGetSyndicsShortInfo().then(result => {
        this.nomenclatures.recievers = result;
      })
    },
    getEmployees(){
      apiMetaDataGetUserShortInfo().then(result => {
        this.nomenclatures.recievers = result;
      })
    },
    getAllMessages(){
      apiGetAllMessageReplies(this.data.id).then(result => {
        this.$set(this, "messageReplies", result);

        this.messageReplies.map(message => {
          this.messageRepliesTables[message.id] = {
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
                    title: "Свали",
                    icon: "mdi-download",
                    color: "white",
                    class: "primary",
                    click: "download",
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
          }
        })
      })
    },
    send(){
      if(this.$refs.form.validate()){
        if(this.isNewDoc){
          let formData = new FormData();
          formData.append("syndicElectronicAddress", this.data.syndicElectronicAddress);
          formData.append("subject", this.data.subject);
          formData.append("body", this.data.body);
          formData.append("sendToAll", this.data.sendToAll);
          for(let receiverId of this.data.messageReceiverIDs){
            formData.append("messageReceiverIDs", receiverId);
          }
          formData.append("sender", this.data.sender);
          
          for(let i=0;i<this.data.messageAttachments.length; i++){
            let fileToUpload = {
              file: this.data.messageAttachments[i].file,
              description: this.data.messageAttachments[i].description,
              documentContentType: eDocumentContentTypes.MESSAGE
            };
            for(let key in fileToUpload){
              formData.append(`files[${i}].${key}`, fileToUpload[key]);
            }
          }
          
        
          apiSendMessage(formData).then(result => {
            if(result && result.id.length)
              this.$router.push({path: `/messages/${result.id}`})
          })
        }
      }
    },
    getDocumentFilesData(message = null){
      if(!message){
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
      } else {
        if(message.documentCollectionId){
          let query = Object.assign({}, {});
          query.pageNumber = this.messageRepliesTables[message.id].pagination.skip / this.messageRepliesTables[message.id].pagination.take + 1;

          query.pageSize = this.messageRepliesTables[message.id].pagination.take;
          query.filter = [message.documentCollectionId]
          apiGetAllDocumentFiles(query).then(result => {
            this.$set(this.messageRepliesTables[message.id], "data", result.items);
            this.$set(this.messageRepliesTables[message.id].pagination, "total", result.totalCount);
            this.$set(this.messageRepliesTables[message.id].pagination, "page", result.currentPage);

            this.$forceUpdate();
          })
        }
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
        let index = this.data.messageAttachments.findIndex(i => i.tId === fileData.tId);
        this.data.messageAttachments.splice(index, 1);
        this.table.pagination.total = this.data.messageAttachments.length;
        this.$refs.confirmModal.closeModal();
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
    saveFile(fileData){
      fileData.tId = this.data.messageAttachments.length + 1;
      this.data.messageAttachments.push(fileData)
      this.table.pagination.total = this.data.messageAttachments.length;
    },
    generateSender(){
      this.data.sender.fullName = this.currentUser.fullName
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
    setSendToAll(val){
      if(val){
        this.backupReceivers = [...this.syndics];

        this.$set(this, 'syndics', []);
        this.data.messageReceiverIDs = [];
        this.syndicsLabel = "До Синдик/ци - всички"
      } else {
        if(this.backupReceivers)
          this.$set(this, "syndics", [...this.backupReceivers]);

        this.backupReceivers = [];
        this.syndicsLabel = "До Синдик/ци"
      }
    },
    clickedOnExpansionPanel(message){
      if(this.messageRepliesTables[message.id].data.length === 0)
        this.getDocumentFilesData(message);
    },
    ISODateString,
    formatDateTime
  }
}
</script>