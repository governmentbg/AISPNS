<template>
  <v-dialog v-model="open" width="70%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc"> Ново действие </template>
        <template v-else> Преглед на двйствие </template>
      </v-card-title>

      <v-card-text ref="cardContent">
        <v-form ref="form">
          <v-row>
            <v-col cols="12" lg="9">
              <base-autocomplete
                label="Вид"
                v-model="data.activityKindId"
                :items="nomenclatures.syndicActions"
                item-text="description"
                item-value="id"
                :rules=[rules.required]
                readonly
              />
            </v-col>
            <v-col cols="12" lg="3">
              <base-material-date-picker
                label="Дата"
                v-model="data.activityDate"
                :rules=[rules.required]
                readonly
              />
            </v-col>
            <v-col cols="12">
              <v-textarea
                label="Описание"
                v-model="data.description"
                rows="2"
                auto-grow
                readonly
              />
            </v-col>
            <v-col cols="12">
              <v-textarea
                label="Бележки"
                v-model="data.notes"
                rows="2"
                auto-grow
                readonly
              />
            </v-col>
            <v-col cols="12" v-if="!isNewDoc">
              <base-input
                label="Синдик"
                v-model="data.syndicName"
                readonly
              />
            </v-col>
          </v-row>
        </v-form>

        <base-material-card 
          icon="mdi-file-multiple-outline"
          color="primary"
          class="elevation-3 mt-15"
        >
          <template v-slot:after-heading>
            Файлове
          </template>

          <base-kendo-grid
            :columns="table.headers"
            :items="table.data"
            :pagination="table.pagination"
            @getData="getDocumentsListData"
            @click="tableActions"
          />
        </base-material-card>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { apiMetaDataGetActivityKinds } from "@/api/metaData";
import { apiGetSyndicActivityById } from "@/api/syndics";
import { apiDownloadDocument, apiGetAllDocumentFiles } from "@/api/documents";
import { bytesToSize, downloadFileFromResponse } from "@/utils";

export default {
  name: "syndicActivityModal",
  components: {
    
  },
  props: {
    caseData: {
      type: Object,
      default: null
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {
        syndicName: "Иван Иванов"
      },
      nomenclatures: {
        syndicActions: []
      },
      table: {
        headers: [
          { title: "Файл", field: "fileName", sortable: false },
          { title: "Описание", field: "description", sortable: false },
          { title: "Размер", cell: this.renderFileSize, sortable: false, width: '100px' },
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
                class: "primary mr-1",
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
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = {};
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
      apiGetSyndicActivityById(this.data.id).then((result) => {
        this.$set(this, "data", result);
        this.isNewDoc = false;
        this.getDocumentsListData();
        this.open = true;

        this.$nextTick(() => {
          this.$refs.cardContent.scrollTop = 0;
        })
      });
    },
    getSyndicActions(){
      apiMetaDataGetActivityKinds().then(result => {
        if(result)
          this.nomenclatures.syndicActions = result;
      });
    },
    openModal(id = null) {
      if(!this.nomenclatures.syndicActions.length)
        this.getSyndicActions();

      this.$set(this, "data", {});
      this.$set(this.table, "data", [])

      if(id){
        this.data.id = id;
        this.getData();
      } else {
        this.$set(this, "data", {
          caseId: this.caseData.id,
        })
        this.open = true;
        this.isNewDoc = true;
      }
    },
    closeModal(){
      this.open = false;
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
      }
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
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
