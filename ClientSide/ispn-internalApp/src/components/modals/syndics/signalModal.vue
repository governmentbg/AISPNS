<template>
  <div>
    <v-dialog v-model="open" width="90%" scrollable>
      <v-card>
        <v-card-title class="headline">
          <template v-if="isNewDoc">Нов сигнал</template>
          <template v-else>Преглед на сигнал</template>
        </v-card-title>

        <v-card-text>
          <v-form ref="form">
            <v-row>
              <v-col cols="12" lg="6">
                <base-material-card 
                  icon="mdi-bell-ring"
                  color="primary"
                  class="elevation-3 mt-5"
                >
                  <template v-slot:after-heading>
                    Сигнал
                  </template>
                  
                  <v-container class="py-3">
                    <v-row>
                      <v-col cols="12" lg="8">
                        <base-input
                          label="Номер"
                          v-model="data.number"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12" lg="4">
                        <base-material-date-picker
                          label="Дата"
                          v-model="data.date"
                        />
                      </v-col>
                      <v-col cols="12">
                        <base-input
                          label="Описание"
                          v-model="data.description"
                        />
                      </v-col>
                      <v-col cols="12">
                        <base-textarea
                          label="Забележки"
                          v-model="data.notes"
                        />
                      </v-col>
                    </v-row>
                  </v-container>
                </base-material-card>
              </v-col>
              <v-col cols="12" lg="6">
                <base-material-card 
                  icon="mdi-gavel"
                  color="primary"
                  class="elevation-3 mt-5"
                >
                  <template v-slot:after-heading>
                    Дело
                  </template>

                  <template v-slot:after-heading-button>
                    <v-btn small class="primary mr-2" @click="openCasesModal">
                      <v-icon left dark> mdi-plus </v-icon>
                      Избери дело
                    </v-btn>
                  </template>
                  
                  <v-container class="py-3">
                    <v-row>
                      <v-col cols="12" lg="6">
                        <base-input
                          label="Номер"
                          v-model="caseData.number"
                          :rules=[rules.required]
                          readonly
                        />
                      </v-col>
                      <v-col cols="12" lg="6">
                        <base-input
                          label="Година"
                          v-model="caseData.year"
                          type="number"
                          :rules=[rules.required]
                          readonly
                        />
                      </v-col>
                      <v-col cols="12">
                        <base-autocomplete
                          label="Съд"
                          :value="caseData.court?.number"
                          :items="nomenclatures.courts"
                          item-text="name"
                          item-value="number"
                          readonly
                        />
                      </v-col>
                      <v-col cols="12">
                        <base-input
                          label="Длъжник"
                          :value="caseData.side?.entity?.name"
                          readonly
                        />
                      </v-col>
                    </v-row>
                  </v-container>
                </base-material-card>
              </v-col>
              <v-col cols="12">
                <base-material-card 
                  icon="mdi-account"
                  color="primary"
                  class="elevation-3 mt-10"
                >
                  <template v-slot:after-heading>
                    Подател
                  </template>
                  
                  <v-container class="py-3">
                    <v-row>
                      <v-col cols="12" lg="4">
                        <base-autocomplete
                          label="Тип"
                          v-model="data.sender.signalSenderTypeId"
                          :items="nomenclatures.senderTypes"
                          item-text="name"
                          item-value="id"
                        />
                      </v-col>
                    </v-row>
                    <v-row>
                      <v-col cols="12" lg="4">
                        <base-input
                          label="Наименование"
                          v-model="data.sender.name"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12" lg="4">
                        <base-input
                          label="ЕГН/ЕИК"
                          v-model="data.sender.citizenshipNumber"
                          type="number"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12" lg="4">
                        <base-input
                          label="E-mail"
                          v-model="data.sender.email"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12" lg="4">
                        <base-input
                          label="Телефон"
                          v-model="data.sender.phone"
                          :rules=[rules.required]
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
                          v-model="data.sender.address.regionId"
                          :items="nomenclatures.districts"
                          item-text="name"
                          item-value="id"
                          @change="getMunicipalities"
                          clearable
                        />
                      </v-col>
                      <v-col cols="12" xl="3" lg="4" md="4">
                        <base-autocomplete
                          v-model="data.sender.address.municipalityId"
                          label="Община"
                          :items="nomenclatures.municipalities"
                          item-text="name"
                          item-value="id"
                          class="pb-0"
                          @change="getSettlements"
                          clearable
                        />
                      </v-col>
                      <v-col cols="12" xl="3" lg="4" md="4">
                        <base-autocomplete
                          label="Населено място"
                          class="pb-0"
                          v-model="data.sender.address.settlementId"
                          :items="nomenclatures.settlements"
                          item-text="name"
                          item-value="id"
                          clearable
                        />
                      </v-col>
                      <v-col cols="12" xl="1" lg="2" md="2">
                        <v-text-field
                          v-model="data.sender.address.postCode"
                          label="ПК"
                          color="secondary"
                          dense
                        />
                      </v-col>
                      <v-col cols="12" xl="5" lg="6" md="4">
                        <v-text-field
                          v-model="data.sender.address.streetName"
                          label="Улица"
                          color="secondary"
                          dense
                        />
                      </v-col>
                      <v-col cols="12" xl="1" lg="2" md="2">
                        <v-text-field
                          v-model="data.sender.address.streetNumber"
                          label="Улица №"
                          color="secondary"
                          dense
                        />
                      </v-col>
                      <v-col cols="12" xl="1" lg="2" md="2">
                        <v-text-field
                          v-model="data.sender.address.buildingNumber"
                          label="Сграда №"
                          color="secondary"
                          dense
                        />
                      </v-col>
                      <v-col cols="12" xl="1" lg="2" md="2">
                        <v-text-field
                          v-model="data.sender.address.entranceNumber"
                          label="Вход"
                          color="secondary"
                          dense
                        />
                      </v-col>
                      <v-col cols="12" xl="1" lg="2" md="2">
                        <v-text-field
                          v-model="data.sender.address.floorNumber"
                          label="Етаж"
                          color="secondary"
                          dense
                        />
                      </v-col>
                      <v-col cols="12" xl="1" lg="2" md="2">
                        <v-text-field
                          v-model="data.sender.address.apartmentNumber"
                          label="Ап. №"
                          color="secondary"
                          dense
                        />
                      </v-col>
                      <v-col cols="12" xl="2" lg="3" md="4">
                        <v-text-field
                          v-model="data.sender.address.ekkate"
                          readonly
                          label="ЕКАТТЕ"
                          color="secondary"
                          dense
                        />
                      </v-col>
                    </v-row>
                  </v-container>
                </base-material-card>
              </v-col>
              <v-col cols="12" v-if="!isNewDoc">
                <base-material-card 
                  icon="mdi-file-multiple-outline"
                  color="primary"
                  class="elevation-3 my-10"
                >
                  <template v-slot:after-heading>
                    Документи
                  </template>

                  <template v-slot:after-heading-button>
                    <v-btn 
                      small 
                      class="primary mr-2" 
                      @click="addNewDocument" 
                    >
                      <v-icon left dark> mdi-plus </v-icon>
                      Нов документ
                    </v-btn>
                  </template>
                  
                  <v-container class="py-3">
                    <v-row>
                      <v-col cols="12">
                        <base-kendo-grid
                          :columns="table.headers"
                          :items="table.data"
                          :pagination="table.pagination"
                          @getData="getDocumentFilesData"
                          @click="tableActions"
                        />
                      </v-col>
                    </v-row>
                  </v-container>
                </base-material-card>
              </v-col>
            </v-row>
          </v-form>
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="save(false)"> Запази </v-btn>
          <v-btn color="secondary" @click="save(true)"> Запази и затвори </v-btn>
          <v-btn color="primary" outlined @click="open = false">
            Затвори
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    
    <syndic-signal-document-modal ref="syndicSignalDocumentModal" :parent-data="data" @reloadData="getDocumentFilesData" @setDocumentCollectionId="setDocumentCollectionId" />
    <cases-list-selection-modal ref="casesListSelectionModal" @selected="selectedCase" :courts="nomenclatures.courts" />
  </div>
</template>

<script>
import { SyndicSignalDocumentModal } from "@/components";
import CasesListSelectionModal from "@/components/modals/cases/casesListSelectionModal.vue";
import { apiGetSignalById, apiCreateSignal, apiUpdateSignal } from "@/api/signals"
import { 
  apiMetaDataGetCourts, 
  apiGetSignalSenderTypes,
  apiMetaDataGetDistricts, 
  apiMetaDataGetMunicipalities, 
  apiMetaDataGetSettlements 
} from "@/api/metaData";
import { apiGetCaseById } from "@/api/cases";
import { apiDeleteDocument, apiDownloadDocument, apiGetAllDocumentFiles } from "@/api/documents";
import { downloadFileFromResponse } from "@/utils";
export default {
  name: "syndicSignalModal",
  components: {
    SyndicSignalDocumentModal,
    CasesListSelectionModal
  },
  props: {
    syndicData: {
      type: Object,
      default: () => {
        return null;
      },
    }
  },
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {sender: {address: {}}},
      caseData: {},
      nomenclatures: {
        courts: [],
        senderTypes: [],
        districts: [],
        municipalities: [],
        settlements: []
      },
      table: {
        headers: [
          { title: "Дата", field: "documentDate", type: 'date', format: "{0:dd.MM.yyyy}", sortable: true },
          { title: "Вид документ", field: "signalDocumentKind.name", sortable: true },
          { title: "Описание", field: "description", sortable: true },
          { title: "Бележки", field: "notes", sortable: true },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "120px",
            sortable: false,
            buttons: [
              {
                title: "Преглед",
                icon: "mdi-text-box-search-outline",
                color: "white",
                class: "primary mr-1",
                click: "preview",
              },
              {
                title: "Свали",
                icon: "mdi-download",
                color: "white",
                class: "secondary mr-1",
                click: "download",
              },
              {
                title: "Изтрий",
                icon: "mdi-trash-can-outline",
                color: "white",
                class: "error",
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
  mounted() {},
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.$set(this, 'data', {sender: {address: {}}});
        this.$set(this.table, 'data', []);
        this.$set(this, 'caseData', {});
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
      apiGetSignalById(this.data.id).then((result) => {
        this.$set(this, "data", result);
        
        this.getAddressNomenclatures();

        if(this.data.caseId)
          this.getCaseData();

        this.getDocumentFilesData();
        this.open = true;
        this.isNewDoc = false;
      });
    },
    getSenderTypes(){
      apiGetSignalSenderTypes().then(result => {
        this.$set(this.nomenclatures, 'senderTypes', result);
      })
    },
    getCourts(){
      apiMetaDataGetCourts().then(result => {
        this.$set(this.nomenclatures, 'courts', result);
      })
    },
    getCaseData(){
      apiGetCaseById(this.data.caseId).then(result => {
        this.$set(this, 'caseData', result);
      })
    },
    save(closeModal = false) {
      if(this.$refs.form.validate()){

        if (this.isNewDoc) {
          const data = Object.assign({syndicId: this.syndicData.id}, this.data);
          apiCreateSignal(data).then((result) => {
            if (result) {
              this.$emit("update");
              this.data.id = result;
              if(closeModal){
                this.closeModal();
              } else {
                this.getData();
              }
            }
          });
        } else {
          apiUpdateSignal(this.data).then((result) => {
            if (result) {
              this.$emit("update");
              if(closeModal)
                this.closeModal();
            }
          });
        }
      }
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.previewDocument(rowData);
        break;
        case "download":
          this.documentDownload(rowData);
        break;
        case "delete":
          this.deleteFileAsk(rowData)
        break;
      }
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
    documentDownload(data){
      apiDownloadDocument(data.id).then(result => {
        downloadFileFromResponse(result);
      })
    },
    deleteFileAsk(item){
      let confirmData = {
        title: "Изтриване на файл",
        body: `Сигурни ли сте, че искате да изтриете избрания файл?`,
        callback: this.deleteDocumentFile,
        parameter: item.id,
      };
      this.$emit('confirm', confirmData);
    },
    deleteDocumentFile(fileId) {
      apiDeleteDocument(fileId).then((result) => {
        if (result) {
          this.getDocumentFilesData();
          this.$emit('reloadData');
          this.$emit('closeConfirm');
        }
      });
    },
    previewDocument(data){
      this.$refs.syndicSignalDocumentModal.openModal(data.id)
    },
    addNewDocument(){
      this.$refs.syndicSignalDocumentModal.openModal()
    },
    setDocumentCollectionId(documentCollectionId){
      if(documentCollectionId)
        this.$set(this.data, 'documentCollectionId', documentCollectionId);
    },
    selectedCase(caseData){
      this.$set(this, 'caseData', caseData);
      this.$set(this.data, 'caseId', caseData.id);
    },
    getAddressNomenclatures(){
      if(this.data.sender.address.regionId)
        apiMetaDataGetMunicipalities(this.data.sender.address.regionId).then((result) => {
          this.$set(this.nomenclatures, "municipalities", result);

          if(this.data.sender.address.municipalityId)
            apiMetaDataGetSettlements(this.data.sender.address.municipalityId).then((result) => {
              this.$set(this.nomenclatures, "settlements", result);
            });
        });
    },
    getDistricts(){
      apiMetaDataGetDistricts().then((result) => {
        this.$set(this.nomenclatures, "districts", result);
      });
    },
    getMunicipalities() {
      apiMetaDataGetMunicipalities(this.data.sender.address.regionId).then((result) => {
        this.$set(this.nomenclatures, "municipalities", result);
        this.$set(this.data.sender.address, "settlementId", null);
        this.$set(this.data.sender.address, "municipalityId", null);
      });
    },
    getSettlements() {
      apiMetaDataGetSettlements(this.data.sender.address.municipalityId).then((result) => {
        this.$set(this.nomenclatures, "settlements", result);
        this.$set(this.data.sender.address, "settlementId", null);
      });
    },
    openCasesModal(){
      this.$refs.casesListSelectionModal.openModal()
    },
    openModal(id = null) {
      if(!this.nomenclatures.senderTypes.length)
        this.getSenderTypes();

      if(!this.nomenclatures.courts.length)
        this.getCourts();

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
    },
    closeModal(){
      this.open = false;
    }
  },
};
</script>

<style></style>
