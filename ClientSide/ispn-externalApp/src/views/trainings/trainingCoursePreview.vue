<template>
  <v-container fluid tag="section" class="full-height" v-if="isSyndic">
    <base-v-component :heading="$route.name" />

    <v-row class="my-10">
      <v-col cols="12">

        <base-material-card 
          icon="mdi-school-outline"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Обучение (курс)
          </template>


          <v-form ref="form" class="pa-3">
            <v-row>
              <v-col cols="12">
                <base-autocomplete
                  label="Вид обучение"
                  v-model="data.courseKindId"
                  :items="nomenclatures.courseKinds"
                  :rules=[rules.required]
                  item-text="name"
                  item-value="id"
                  readonly
                />
              </v-col>
              <v-col cols="12">
                <base-input
                  label="Тема"
                  v-model="data.topic"
                  :rules=[rules.required]
                  readonly
                />
              </v-col>
              
              <v-col cols="12">
                <v-textarea
                  rows="2"
                  auto-grow
                  label="Лектори"
                  v-model="data.lecturers"
                  :rules=[rules.required]
                  readonly
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  rows="2"
                  auto-grow
                  label="Подтеми"
                  v-model="data.subTopics"
                  :rules=[rules.required]
                  readonly
                />
              </v-col>
              <v-col cols="12" lg="2">
                <base-material-date-picker
                  label="Първа дата на изпита"
                  v-model="data.examDate1"
                  readonly
                />
              </v-col>
              <v-col cols="12" lg="2">
                <base-material-date-picker
                  label="Втора дата на изпита"
                  v-model="data.examDate2"
                  readonly
                />
              </v-col>
              <v-col cols="12" lg="8">
                <base-input
                  label="Максимален брой участници"
                  v-model="data.maximumParticipants"
                  type="number"
                  :rules=[rules.required]
                  readonly
                />
              </v-col>
              
            </v-row>
            <v-row>
              <v-col cols="12">
                <h4>Провеждане на обучение дата 1</h4>
                <v-divider class="my-3" />
              </v-col>
              <v-col cols="12" lg="2" md="6">
                <base-material-date-picker
                  label="От дата"
                  v-model="data.fromDate1"
                  readonly
                />
              </v-col>
              <v-col cols="12" lg="2" md="6">
                <base-material-date-picker
                  label="До дата"
                  v-model="data.toDate1"
                  readonly
                />
              </v-col>
              <v-col cols="12" lg="2">
                <base-input
                  label="Продължителност"
                  v-model="data.lengthDate1"
                  :rules=[rules.required]
                  readonly
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  label="Област"
                  class="pb-0"
                  v-model="data.address1Navigation.regionId"
                  :items="nomenclatures.address1Navigation.districts"
                  item-text="name"
                  item-value="id"
                  @change="getMunicipalities('address1Navigation')"
                  clearable
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  v-model="data.address1Navigation.municipalityId"
                  label="Община"
                  :items="nomenclatures.address1Navigation.municipalities"
                  item-text="name"
                  item-value="id"
                  class="pb-0"
                  @change="getSettlements('address1Navigation')"
                  clearable
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  label="Населено място"
                  class="pb-0"
                  v-model="data.address1Navigation.settlementId"
                  :items="nomenclatures.address1Navigation.settlements"
                  item-text="name"
                  item-value="id"
                  clearable
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address1Navigation.postCode"
                  label="ПК"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="5" lg="6" md="4">
                <base-input
                  v-model="data.address1Navigation.streetName"
                  label="Улица"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address1Navigation.streetNumber"
                  label="Улица №"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address1Navigation.buildingNumber"
                  label="Сграда №"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address1Navigation.entranceNumber"
                  label="Вход"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address1Navigation.floorNumber"
                  label="Етаж"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address1Navigation.apartmentNumber"
                  label="Ап. №"
                  readonly
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12">
                <h4>Провеждане на обучение дата 2</h4>
                <v-divider class="my-3" />
              </v-col>
              <v-col cols="12" lg="2" md="6">
                <base-material-date-picker
                  label="От дата"
                  v-model="data.fromDate2"
                  readonly
                />
              </v-col>
              <v-col cols="12" lg="2" md="6">
                <base-material-date-picker
                  label="До дата"
                  v-model="data.toDate2"
                  readonly
                />
              </v-col>
              <v-col cols="12" lg="8">
                <base-input
                  label="Продължителност"
                  v-model="data.lengthDate2"
                  :rules=[rules.required]
                  readonly
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  label="Област"
                  class="pb-0"
                  v-model="data.address2Navigation.regionId"
                  :items="nomenclatures.address2Navigation.districts"
                  item-text="name"
                  item-value="id"
                  @change="getMunicipalities('address2Navigation')"
                  clearable
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  v-model="data.address2Navigation.municipalityId"
                  label="Община"
                  :items="nomenclatures.address2Navigation.municipalities"
                  item-text="name"
                  item-value="id"
                  class="pb-0"
                  @change="getSettlements('address2Navigation')"
                  clearable
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  label="Населено място"
                  class="pb-0"
                  v-model="data.address2Navigation.settlementId"
                  :items="nomenclatures.address2Navigation.settlements"
                  item-text="name"
                  item-value="id"
                  clearable
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address2Navigation.postCode"
                  label="ПК"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="5" lg="6" md="4">
                <base-input
                  v-model="data.address2Navigation.streetName"
                  label="Улица"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address2Navigation.streetNumber"
                  label="Улица №"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address2Navigation.buildingNumber"
                  label="Сграда №"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address2Navigation.entranceNumber"
                  label="Вход"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address2Navigation.floorNumber"
                  label="Етаж"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address2Navigation.apartmentNumber"
                  label="Ап. №"
                  readonly
                />
              </v-col>
            </v-row>
          </v-form>
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

          <base-kendo-grid
            :columns="tables.files.headers"
            :items="tables.files.data"
            :hasPaging="false"
            @click="tableActions"
          />
        </base-material-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import TrainingModel from "@/models/trainings/TrainingModel";
import { apiGetCourseById } from "@/api/courses"
import { apiMetaDataGetCourseKinds, apiGetDistricts, apiGetMunicipalities, apiGetSettlements } from "@/api/metaData";
import { apiDownloadDocument, apiGetAllDocumentFiles } from "@/api/documents";
import { bytesToSize, downloadFileFromResponse } from "@/utils";
export default {
  name: "trainingModal",
  components: {
  },
  props: {
    programData: {
      type: Object,
      default: () => {
        return {}
      }
    }
  },
  mounted() {
    if(!this.nomenclatures.courseKinds.length)
      this.getCourseKinds();

    if(!this.nomenclatures.address1Navigation.districts.length)
      this.getDistricts();
    // TODO: this.isNewDoc = true;
    this.isNewDoc = false;
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
      this.getData();
    }
  },
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: Object.assign({}, new TrainingModel()),
      nomenclatures: {
        address1Navigation: {
          districts: [],
          municipalities: [],
          settlements: []
        },
        address2Navigation: {
          districts: [],
          municipalities: [],
          settlements: []
        },
        courseKinds: [],
      },
      tables: {
        files: {
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
                  click: "downloadFile",
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
        }
      },
      rules: {
        required: (v) => !!v || v === 0 || "Полето е задължително",
      },
    };
  },
  watch: {
  },
  computed: {
    ...mapGetters([
      'isSyndic',
      'currentUser'
    ]),
  },
  methods: {
    getData() {
      apiGetCourseById(this.data.id).then((result) => {
        if(!result.address1Navigation)
          result.address1Navigation = {};

        if(!result.address2Navigation)
          result.address2Navigation = {};


        this.$set(this, "data", result);

        if(this.data.address1Navigation.regionId)
          this.getAddressNomenclatures('address1Navigation');

          
        if(this.data.address2Navigation?.regionId)
          this.getAddressNomenclatures('address2Navigation');
        
        this.getDocumentsData();
        
        this.isNewDoc = false;
        this.open = true;
      });
    },
    getCourseKinds(){
      apiMetaDataGetCourseKinds().then(result => {
        this.$set(this.nomenclatures, "courseKinds", result);
      });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "downloadFile":
          this.download(rowData);
        break;
      }
    },
    getAddressNomenclatures(addressNum){
      if(this.data[addressNum].regionId)
        apiGetMunicipalities(this.data[addressNum].regionId).then((result) => {
          this.$set(this.nomenclatures[addressNum], "municipalities", result);

          if(this.data[addressNum].municipalityId)
            apiGetSettlements(this.data[addressNum].municipalityId).then((result) => {
              this.$set(this.nomenclatures[addressNum], "settlements", result);
            });
        });
    },
    getDistricts(addressNum){
      apiGetDistricts().then((result) => {
        if(addressNum){
          this.$set(this.nomenclatures[addressNum], "districts", result);
        } else {
          this.$set(this.nomenclatures.address1Navigation, "districts", result);
          this.$set(this.nomenclatures.address2Navigation, "districts", result);
        }
      });
    },
    getMunicipalities(addressNum) {
      apiGetMunicipalities(this.data[addressNum].regionId).then((result) => {
        
        this.$set(this.nomenclatures[addressNum], "municipalities", result);

        this.$set(this.data[addressNum], "settlementId", null);
        this.$set(this.data[addressNum], "municipalityId", null);
      });
    },
    getSettlements(addressNum) {
      apiGetSettlements(this.data[addressNum].municipalityId).then((result) => {
        this.$set(this.nomenclatures[addressNum], "settlements", result);
        this.$set(this.data[addressNum], "settlementId", null);
      });
    },
    getDocumentsData(reloadParentData = false){
      if(this.data.documentCollectionId){
        let query = Object.assign({}, {});
        
        query.pageNumber = this.tables.files.pagination.skip / this.tables.files.pagination.take + 1;
        query.pageSize = this.tables.files.pagination.take;
        query.filter = [this.data.documentCollectionId]

        if(reloadParentData)
          this.$emit('reloadData');
        
        apiGetAllDocumentFiles(query).then((result) => {
          this.$set(this.tables.files, 'data', result.items);
          this.$set(this.tables.files.pagination, 'total', result.totalCount);
          this.$set(this.tables.files.pagination, 'page', result.currentPage);
        });
      }
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
  },
};
</script>

<style></style>
