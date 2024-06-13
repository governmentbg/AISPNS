<template>
  <div>
    <v-dialog v-model="open" width="70%" scrollable>
      <v-card>
        <v-card-title class="headline">
          Преглед на образец
        </v-card-title>

        <v-card-text>
          <v-form
            ref="form"
            lazy-validation
          >
            <v-row>
              <v-col cols="12" xl="5" lg="6" md="9">
                <base-input
                  label="Вид"
                  v-model="data.templateKindName"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="3">
                <base-material-date-picker
                  label="Дата"
                  v-model="data.date"
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
          <v-btn color="primary" outlined @click="open = false">
            Затвори
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { apiDownloadDocument, apiGetAllDocumentFiles } from "@/api/documents";
import { apiGetSyndicTemplateById } from "@/api/syndics"
import { bytesToSize, downloadFileFromResponse } from "@/utils";
export default {
  name: "syndicTemplateModal",
  components: {
  },
  props: {
    syndicData: {
      type: Object,
      default: () => {},
    },
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {},
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
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
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.$set(this, 'data', {});
        this.$set(this.table, 'data', []);
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
      apiGetSyndicTemplateById(this.data.id).then((result) => {
        this.$set(this, "data", result);
        
        this.getDocumentsData();
        this.isNewDoc = false;
        this.open = true;
      });
    },
    openModal(id = null) {
      if(id){
        this.data.id = id;
        this.getData();
      } else {
        this.open = true;
        this.isNewDoc = true;
      }

      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    closeModal(){
      this.open = false;
    },
    getDocumentsData(reloadParentData = false){
      if(this.data.documentCollectionId){
        let query = Object.assign({}, {});
        
        query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
        query.pageSize = this.table.pagination.take;
        query.filter = [this.data.documentCollectionId]

        if(reloadParentData)
          this.$emit('update');
        
        apiGetAllDocumentFiles(query).then((result) => {
          this.$set(this.table, 'data', result.items);
          this.$set(this.table.pagination, 'total', result.totalCount);
          this.$set(this.table.pagination, 'page', result.currentPage);
        });
      }
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
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
  },
};
</script>