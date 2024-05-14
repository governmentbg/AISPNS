<template>
  <div>
    <v-dialog v-model="open" width="80%" scrollable>
      <v-card>
        <v-card-title class="headline">
          <template v-if="isNewDoc">Нова вещ към обява за продажба</template>
          <template v-else>Преглед вещ към обява за продажба</template>
        </v-card-title>

        <v-card-text>
          <v-form ref="form">
            <v-row>
              <v-col cols="12" lg="6">
                <base-autocomplete
                  label="Тип"
                  v-model="data.objectTypeId"
                  :items="nomenclatures.types"
                  item-text="description"
                  item-value="id"
                  :rules="[rules.required]"
                  :readonly="isReadonly"
                  :clearable="!isReadonly"
                />
              </v-col>
              <v-col cols="12" lg="6">
                <base-autocomplete
                  label="Вид"
                  v-model="data.objectKindId"
                  :items="nomenclatures.kinds"
                  item-text="description"
                  item-value="id"
                  :rules="[rules.required]"
                  :readonly="isReadonly"
                  :clearable="!isReadonly"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12">
                <h5>Адрес</h5>
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="4">
                <base-autocomplete
                  label="Област"
                  class="pb-0"
                  v-model="data.address.regionId"
                  :items="nomenclatures.districts"
                  item-text="name"
                  item-value="id"
                  @change="getMunicipalities"
                  :readonly="isReadonly"
                  :clearable="!isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="4">
                <base-autocomplete
                  v-model="data.address.municipalityId"
                  label="Община"
                  :items="nomenclatures.municipalities"
                  item-text="name"
                  item-value="id"
                  class="pb-0"
                  @change="getSettlements"
                  :readonly="isReadonly"
                  :clearable="!isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="4">
                <base-autocomplete
                  label="Населено място"
                  class="pb-0"
                  v-model="data.address.settlementId"
                  :items="nomenclatures.settlements"
                  item-text="name"
                  item-value="id"
                  clearable
                  :readonly="isReadonly"
                  :clearable="!isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.postCode"
                  label="ПК"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="5" lg="6" md="4">
                <base-input
                  v-model="data.address.streetName"
                  label="Улица"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.streetNumber"
                  label="Улица №"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.buildingNumber"
                  label="Сграда №"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.entranceNumber"
                  label="Вход"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.floorNumber"
                  label="Етаж"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.apartmentNumber"
                  label="Ап. №"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="4">
                <base-input
                  v-model="data.address.ekkate"
                  label="ЕКАТТЕ"
                  readonly
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" xl="2" lg="6">
                <base-input
                  label="Стойност"
                  v-model="data.value"
                  type="number"
                  :rules="[rules.required]"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Състояние"
                  v-model="data.condition"
                  rows="2"
                  auto-grow
                  :rules="[rules.required]"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Бележки"
                  v-model="data.notes"
                  rows="2"
                  auto-grow
                  :readonly="isReadonly"
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
                v-if="!isReadonly"
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
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="save(false)" v-if="!isReadonly"> Запази </v-btn>
          <v-btn color="secondary" @click="save(true)" v-if="!isReadonly"> Запази и затвори </v-btn>
          <v-btn color="primary" outlined @click="open = false">
            Затвори
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    
    
    <sell-announcement-upload-file-modal ref="sellAnnouncementUploadFileModal" :parent-data="data" @reloadData="getDocumentsListData" @setDocumentCollectionId="setDocumentCollectionId" :is-announcement-object="true" />
  </div>
</template>

<script>
import { bytesToSize, downloadFileFromResponse } from "@/utils";
import { apiGetAnnouncementObjectById, apiCreateAnnouncementObject, apiUpdateSellAnnouncementObject } from "@/api/sellAnnouncements"
import { 
  apiMetaDataGetObjectKind, 
  apiMetaDataGetObjectType,
  apiGetDistricts, 
  apiGetMunicipalities, 
  apiGetSettlements
} from "@/api/metaData";
import SellAnnouncementUploadFileModal from "@/components/modals/sellAnnouncements/sellAnnouncementFileUpload.vue";

import { apiGetAllDocumentFiles, apiDeleteDocument, apiDownloadDocument } from "@/api/documents";
import { eSellAnnouncementStatus } from "@/utils/enums/enumerators";
export default {
  name: "sellAnnouncementObjectModal",
  components: {
    SellAnnouncementUploadFileModal,
  },
  props: {
    announcementData: {
      type: Object,
      default: () => {
        return null;
      }
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      nomenclatures: {
        types: [],
        kinds: [],
        districts: [],
        municipalities: [],
        settlements: []
      },
      data: {
        address: {}
      },
      table: {
        headers: [
          { title: "Файл", field: "fileName", sortable: false },
          //{ title: "Тип", field: "attachedDocumentKind", sortable: true },
          { title: "Описание", field: "description", sortable: false },
          { title: "Размер", cell: this.renderFileSize, sortable: false, width: '100px' },
          { title: "Качен на", field: "blobDateCreated", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false, width: '100px' },
          //{ title: "Качен от", field: "", sortable: true },
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
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = {
          address: {}
        };
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
    isReadonly(){
      return this.announcementData?.statusCode !== eSellAnnouncementStatus.DRAFT;
    }
  },
  methods: {
    getData(id) {
      apiGetAnnouncementObjectById(this.data.id).then((result) => {
        this.$set(this, "data", result);

        if(this.data.documentCollectionId)
          this.getDocumentsListData();

        this.isNewDoc = false;
        this.open = true;
      });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          let data = Object.assign({}, this.data);
          data.announcementId = this.announcementData.id;
          apiCreateAnnouncementObject(data).then((result) => {
            if (result) {
              this.data.id = result;
              this.$emit("update");
              if(closeModal){
                this.closeModal();
              } else {
                this.getData();
              }
            }
          });
        } else {
          apiUpdateSellAnnouncementObject(this.data).then((result) => {
            if (result) {
              this.$emit("update");
              if(closeModal)
                this.closeModal();
            }
          });
        }
    },
    openModal(id = null) {
      if(!this.nomenclatures.types.length)
        this.getObjectTypes();
      if(!this.nomenclatures.kinds.length)
        this.getObjectKinds();

      if(!this.nomenclatures.districts.length)
        this.getDistricts();

      if(id){
        this.data.id = id;
        this.getData();
      } else {
        this.open = true;
        this.isNewDoc = true;
      }
      
      if(this.$refs.form)
        this.$refs.form.resetValidation();

      if(this.isReadonly){
        this.table.headers[this.table.headers.length -1].buttons.pop();
        this.table.headers[this.table.headers.length -1].buttons[0].class = "primary"
        this.table.headers[this.table.headers.length -1].width = "50px"
      } else {
        if(this.table.headers[this.table.headers.length -1].buttons.length === 1){
          this.table.headers[this.table.headers.length -1].width = "80px"
          this.table.headers[this.table.headers.length -1].buttons[0].class = "primary mr-1"
          this.table.headers[this.table.headers.length -1].buttons.push({
            title: "Изтрий",
            icon: "mdi-trash-can-outline",
            color: "white",
            class: "secondary",
            click: "deleteFileAsk",
          });
        }
      }
    },
    closeModal(){
      this.open = false;
    },
    getDistricts(){
      apiGetDistricts().then((result) => {
        this.$set(this.nomenclatures, "districts", result);
      });
    },
    getMunicipalities() {
      apiGetMunicipalities(this.data.address.regionId).then((result) => {
        this.$set(this.nomenclatures, "municipalities", result);
        this.$set(this.data.address, "settlementId", null);
        this.$set(this.data.address, "municipalityId", null);
      });
    },
    getSettlements() {
      apiGetSettlements(this.data.address.municipalityId).then((result) => {
        this.$set(this.nomenclatures, "settlements", result);
        this.$set(this.data.address, "settlementId", null);
      });
    },
    getObjectTypes(){
      apiMetaDataGetObjectType().then(result => {
        this.$set(this.nomenclatures, "types", result);
      })
    },
    getObjectKinds(){
      apiMetaDataGetObjectKind().then(result => {
        this.$set(this.nomenclatures, "kinds", result);
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
    deleteFileAsk(fileData){
      let confirmData = {
        title: "Изтриване на файл",
        body: `Сигурни ли сте, че искате да изтриете избрания файл?`,
        callback: this.deleteFile,
        parameter: fileData.id
      };
      this.$emit("confirmAction", confirmData);
    },
    deleteFile(fileId){
      apiDeleteDocument(fileId).then(result => {
        if(result){
          this.getDocumentsListData();
          this.$emit("closeConfirm");
        }
      })
    },
    addNewFile(){
      this.$refs.sellAnnouncementUploadFileModal.openModal();
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
  },
};
</script>