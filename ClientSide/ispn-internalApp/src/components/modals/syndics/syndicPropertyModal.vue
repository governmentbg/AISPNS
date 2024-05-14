<template>
  <v-dialog v-model="open" width="70%" scrollable>
    <v-card>
      <v-card-title class="headline">
        {{ getModalTitle }}
      </v-card-title>

      <v-card-text ref="cardContent">
        <v-form ref="form">
          <v-row>
            <template v-if="tab === 'things'">
            <v-col cols="12"  lg="6">
              <base-autocomplete
                label="Тип"
                v-model="data.propertyTypeId"
                :items="nomenclatures.types"
                item-text="description"
                item-value="id"
                readonly
              />
            </v-col>
            <v-col cols="12" lg="6">
              <base-autocomplete
                label="Вид"
                v-model="data.propertyKindId"
                :items="nomenclatures.kinds"
                item-text="description"
                item-value="id"
                readonly
              />
            </v-col>
          </template>
          <template v-else>
            <v-col cols="12" lg="6">
              <base-autocomplete
                label="Вид"
                v-model="data.propertyTypeId"
                :items="nomenclatures.types"
                item-text="description"
                item-value="id"
                readonly
              />
            </v-col>
          </template>
            <v-col cols="12" lg="6" v-if="tab === 'shares' || tab === 'receivables'">
              <base-entity-autocomplete
                label="Юридическо лице"
                v-model="data.entityId"
                :items="nomenclatures.entities"
                readonly
              />
            </v-col>
            <v-col cols="12" lg="6" v-if="tab === 'receivables'">
              <base-person-autocomplete
                label="Физическо лице"
                v-model="data.personId"
                :items="nomenclatures.persons"
                readonly
              />
            </v-col>
            <template v-if="tab === 'things'">
              <v-col cols="12" xl="3" lg="4" md="4">
                <base-autocomplete
                  label="Област"
                  class="pb-0"
                  v-model="data.address.regionId"
                  :items="nomenclatures.districts"
                  item-text="name"
                  item-value="id"
                  @change="getMunicipalities"
                  readonly
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
                  readonly
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
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.postCode"
                  label="ПК"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="5" lg="6" md="4">
                <base-input
                  v-model="data.address.streetName"
                  label="Улица"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.streetNumber"
                  label="Улица №"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.buildingNumber"
                  label="Сграда №"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.entranceNumber"
                  label="Вход"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.floorNumber"
                  label="Етаж"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.apartmentNumber"
                  label="Ап. №"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="4">
                <base-input
                  v-model="data.address.ekkate"
                  readonly
                  label="ЕКАТТЕ"
                />
              </v-col>
              <v-col cols="12" v-if="tab === 'things'">
                <v-textarea
                  label="Състояние"
                  v-model="data.state"
                  rows="2"
                  auto-grow
                  readonly
                />
              </v-col>
            </template>
            <v-col cols="12" xl="3" lg="3" md="4">
              <base-input
                label="Стойност"
                v-model="data.value"
                readonly
              />
            </v-col>
            <v-col cols="12" v-if="tab !== 'things'">
              <v-textarea
                label="Описание"
                v-model="data.state"
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

import { 
  apiMetaDataGetObjectKind, 
  apiMetaDataGetObjectType, 
  apiMetaDataGetDistricts, 
  apiMetaDataGetMunicipalities, 
  apiMetaDataGetSettlements ,
  apiMetaDataGetEntities,
  apiMetaDataGetPersons
} from "@/api/metaData";
import { ePropertyClass } from "@/utils/enums/enumerators";
import { apiGetSyndicCasePropertyById } from "@/api/syndics";
import { bytesToSize, downloadFileFromResponse } from "@/utils";
import { apiDownloadDocument, apiGetAllDocumentFiles } from "@/api/documents";

export default {
  name: "syndicPropertyModal",
  components: {
  },
  props: {
    tab: {
      type: String,
      default: null
    },
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
        address: {}
      },
      nomenclatures: {
        kinds: [],
        types: [],
        districts: [],
        municipalities: [],
        settlements: [],
        entities: [],
        persons: []
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
        this.data = {
          address: {}
        };
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
    getModalTitle(){
      if(this.isNewDoc){
        switch(this.tab){
          case "things":
            return "Нова вещ";
          case "patents":
            return "Нов патент";
          case "shares":
            return "Нов дял";
          case "receivables":
            return "Ново взимане";
        }
      } else {
        switch(this.tab){
          case "things":
            return "Преглед на вещ";
          case "patents":
            return "Преглед на патент";
          case "shares":
            return "Преглед на дял";
          case "receivables":
            return "Преглед на взимане";
        }
      }
    }
  },
  methods: {
    getData() {
      apiGetSyndicCasePropertyById(this.data.id).then((result) => {
        if(result){
          this.$set(this, "data", result);
          if(this.tab === 'things' && this.data.regionId)
            this.getAddressNomenclatures();

          this.getDocumentsListData();

          this.isNewDoc = false;

          if(this.data.entityId)
            this.getEntities();

          if(this.data.personId)
           this.getPersons();

          this.open = true;

          this.$nextTick(() => {
            this.$refs.cardContent.scrollTop = 0;
          })
        }
      });
    },
    getObjectKinds(){
      apiMetaDataGetObjectKind().then((result) => {
        this.nomenclatures.kinds = result;
      });
    },
    getObjectTypes(){
      apiMetaDataGetObjectType().then((result) => {
        this.nomenclatures.types = result;
      });
    },
    getAddressNomenclatures(){
      if(this.data.address.regionId)
        apiMetaDataGetMunicipalities(this.data.address.regionId).then((result) => {
          this.$set(this.nomenclatures, "municipalities", result);

          if(this.data.address.municipalityId)
            apiMetaDataGetSettlements(this.data.address.municipalityId).then((result) => {
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
      apiMetaDataGetMunicipalities(this.data.address.regionId).then((result) => {
        this.$set(this.nomenclatures, "municipalities", result);
        this.$set(this.data.address, "settlementId", null);
        this.$set(this.data.address, "municipalityId", null);
      });
    },
    getSettlements() {
      apiMetaDataGetSettlements(this.data.address.municipalityId).then((result) => {
        this.$set(this.nomenclatures, "settlements", result);
        this.$set(this.data.address, "settlementId", null);
      });
    },
    getEntities(){
      if(!this.isNewDoc){
        let query = {pageSize: 10, pageNumber: 1, searchCriteria: null, entityId: this.data.entityId}
        apiMetaDataGetEntities(query).then((result) => {
          this.$nextTick(() => {
            this.$set(this.nomenclatures, "entities", result.items);
          })
        });
      } else {
        apiMetaDataGetEntities().then((result) => {
          this.$set(this.nomenclatures, "entities", result.items);
        });
      }
    },
    getPersons(){
      if(!this.isNewDoc){
        apiMetaDataGetPersons({pageSize: 10, pageNumber: 1, searchCriteria: null, personId: this.data.personId}).then((result) => {
          this.$nextTick(() => {
            this.$set(this.nomenclatures, "persons", result.items);
          })
        });
      } else {
        apiMetaDataGetPersons().then((result) => {
          this.$set(this.nomenclatures, "persons", result.items);
        });
      }
    },
    openModal(id = null) {
      this.$set(this, 'data', {});
      this.$set(this.table, 'data', []); 
      if(!this.nomenclatures.types.length)
        this.getObjectTypes();

      if(this.$refs.form)
        this.$refs.form.resetValidation();

      switch(this.tab){
        case "things":
          this.$set(this, "data", {
            address: {},
            caseId: this.caseData.id,
            propertyClassId: ePropertyClass.THINGS
          })

          if(!this.nomenclatures.kinds.length)
            this.getObjectKinds();

          if(!this.nomenclatures.districts.length)
            this.getDistricts();
        break;
        case "patents":
          this.$set(this, "data", {
            caseId: this.caseData.id,
            propertyClassId: ePropertyClass.PATENTS
          })
          this.getObjectTypes();
        break;
        case "shares":
          if(!this.nomenclatures.entities.length)
            this.getEntities();

          this.$set(this, "data", {
            caseId: this.caseData.id,
            propertyClassId: ePropertyClass.SHARES
          })
        break;
        case "receivables":
          if(!this.nomenclatures.entities.length)
            this.getEntities();
          if(!this.nomenclatures.persons.length)
            this.getPersons();

          this.$set(this, "data", {
            caseId: this.caseData.id,
            propertyClassId: ePropertyClass.RECEIVABLES
          })
        break;
      }

      if(id){
        this.data.id = id;
        this.getData();
      } else {
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
