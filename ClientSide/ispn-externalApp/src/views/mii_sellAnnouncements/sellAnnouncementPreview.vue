<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isMEIEmployee"
  >
    <base-v-component
      :heading="isNewDoc ? 'Нова обява' : 'Преглед на обява'"
      goBackText="Обратно към обяви за продажба"
      goBackTo="/mii-sell-announcements"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4" v-if="false">
          Номер: <strong> {{  }} </strong> <br />
          <template v-if="data.publishDate">
            Публикувана на: <strong> {{ ISODateString(data.publishDate) }} </strong> <br />
          </template>
          <template v-if="data.receivedDate">
            Дата на получаване: <strong> {{ ISODateString(data.receivedDate) }} </strong> <br />
          </template>
          <template v-if="data.publishedBy">
            Публикувана от: <strong> {{ data.publishedBy }} </strong> <br />
          </template>
        </v-sheet>
      </v-col>
      <v-col cols="12" lg="7" xl="8" class="text-right">
        <v-btn
          color="primary"
          @click="save"
          v-if="isDraft"
        >
          <v-icon left dark>mdi-check</v-icon>
          Запази
        </v-btn>
        <v-btn
          color="secondary"
          @click="publishAsk"
          v-if="showPublishButton"
        >
          <v-icon left dark>mdi-certificate</v-icon>
          Публикуване
        </v-btn>
        <v-btn
          color="error"
          @click="cancelAnnouncementAsk"
          v-if="isPublished"
        >
          <v-icon left dark>mdi-cancel</v-icon>
          Отмяна на обява
        </v-btn>
        <v-btn
          color="secondary"
          @click="sendAnnouncementForCorrectionInDraftAsk"
          v-if="isPublished"
        >
          <v-icon left dark>mdi-circle-edit-outline</v-icon>
          Върни за редакция
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="my-10">
      <v-col cols="12">
        
        <v-form
          ref="form"
          lazy-validation
        >
          <base-material-card 
            icon="mdi-gavel"
            color="primary"
            class="elevation-3"
          >
            <template v-slot:after-heading>
              Дело
            </template>
            
            <template v-slot:after-heading-button>
              <v-btn
                class="primary"
                @click="openCasesModal"
                v-if="!isPublished && !isCanceled"
              >
                <v-icon left dark>mdi-plus</v-icon>
                Избери дело
              </v-btn>
            </template>
            
            <div class="pa-3">
              <v-row>
                <v-col cols="12" xl="2" lg="4" md="6">
                  <base-input
                    label="Дело номер"
                    v-model="data.caseNumber"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="4" md="6">
                  <base-input
                    label="Дело година"
                    v-model="data.caseYear"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="5" lg="4" md="6">
                  <base-autocomplete
                    label="Съд"
                    v-model="data.courtId"
                    :items="nomenclatures.courts"
                    item-text="name"
                    item-value="id"
                    :rules="[rules.required]"
                    :clearable="!isReadonly"
                    :readonly="isReadonly"
                  />
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-syndic-autocomplete
                    label="Синдик"
                    v-model="data.syndicId"
                    :items="nomenclatures.syndics"
                    :rules="[rules.required]"
                    @changed="changedSyndic"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-input
                    label="Длъжник име"
                    v-model="data.debtorName"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-input
                    label="Длъжник ЕИК"
                    v-model="data.debtorEik"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" v-if="false">
                  <base-input
                    label="Длъжник адрес"
                    v-model="data.debtorName"
                    :readonly="isReadonly"
                  />
                </v-col>
              </v-row>
            </div>
          </base-material-card>
          <base-material-card 
            icon="mdi-magnify-expand"
            color="primary"
            class="elevation-3 mt-15"
          >
            <template v-slot:after-heading>
              Обява за продажба
            </template>
            
            <div class="pa-3">
              <v-row>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-date-time-picker
                    label="Дата и час продажба"
                    v-model="data.offeringDate"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                    :clearable="!isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-date-time-picker
                    label="Краен срок за приемане на предложение"
                    v-model="data.offerDeadline"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                    :clearable="!isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="6" lg="4" md="6">
                  <base-autocomplete
                    label="Място на продажбата"
                    v-model="data.locationTypeId"
                    :items="nomenclatures.places"
                    item-text="description"
                    item-value="id"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                    :clearable="!isReadonly"
                    @change="changedLocationType"
                  />
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12">
                  <h5>Място на продажбата адрес</h5>
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
                    :clearable="!isReadonly"
                    :readonly="isReadonly"
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
                    :clearable="!isReadonly"
                    :readonly="isReadonly"
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
                    :clearable="!isReadonly"
                    :readonly="isReadonly"
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
            </div>
          </base-material-card>


          <base-material-card 
            icon="mdi-bullhorn-outline"
            color="primary"
            class="elevation-3 mt-15"
          >
            <template v-slot:after-heading>
              Обява
            </template>

            <div class="pa-3">
              <v-row>
                <v-col cols="12" xl="4" lg="4" md="6">
                  <base-syndic-autocomplete
                    label="Втори синдик"
                    v-model="data.secondSyndicId"
                    :items="nomenclatures.syndics2"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                    @changed="changedSecondSyndic"
                  />
                </v-col>
                <v-col cols="12" xl="4" lg="4" md="6">
                  <base-autocomplete
                    label="Ред на продажбата"
                    v-model="data.offeringKindId"
                    :items="nomenclatures.offeringKinds"
                    item-text="description"
                    item-value="id"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                    :clearable="!isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="4" lg="4" md="6">
                  <base-autocomplete
                    label="Начин на продажбата"
                    v-model="data.offeringProcedureId"
                    :items="nomenclatures.offeringProcedures"
                    item-text="description"
                    item-value="id"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                    :clearable="!isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="4" lg="4" md="6">
                  <base-input
                    label="Начална продажна цена"
                    v-model="data.startingPrice"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="4" lg="4" md="6">
                  <base-input
                    label="Внасяне на зададък"
                    v-model="data.retainer"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="4" lg="4" md="6">
                  <base-autocomplete
                    label="Книжа за продажбата"
                    v-model="data.papersForSaleId"
                    :items="nomenclatures.papersForSale"
                    item-text="description"
                    item-value="id"
                    :rules="[rules.required]"
                    :readonly="isReadonly"
                    :clearable="!isReadonly"
                  />
                </v-col>
                <v-col cols="12" xl="8" lg="8">
                  <base-autocomplete
                    label="Описание на процеса на продажба"
                    v-model="data.salesProcedureId"
                    :items="nomenclatures.salesProcedures"
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
                  <h5>Информация</h5>
                </v-col>
                <v-col cols="12" v-if="!isReadonly">
                  <base-editor
                    v-model="data.text"
                  />
                </v-col>
                <v-col cols="12" v-html="data.text" v-else></v-col>
              </v-row>
            </div>
          </base-material-card>


          <base-material-card 
            color="primary"
            class="elevation-3 mt-15 pt-0"
          >
            <v-simple-table class="bordered">
              <template v-slot:default>
                <tbody>
                  <tr v-for="template in nomenclatures.templates">
                    <td width="30%">{{ template.name }}</td>
                    <td>
                      {{ data[getTemplateFieldByNumber(template.number)] }}
                    </td>
                  </tr>
                </tbody>
              </template>
            </v-simple-table>
          </base-material-card>
        </v-form>
        <base-material-card 
          icon="mdi-book-outline"
          color="primary"
          class="elevation-3 mt-15"
          v-if="!isNewDoc"
        >
          <template v-slot:after-heading>
            Описание на имущество
          </template>
          
          <template v-slot:after-heading-button v-if="!isReadonly">
            <v-btn
              class="primary"
              @click="addNewObject"
            >
              <v-icon left dark>mdi-plus</v-icon>
              Нова вещ
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="objectsTable.headers"
            :items="objectsTable.data"
            :pagination="objectsTable.pagination"
            sortable
            :sort.sync="objectsTable.sort"
            @getData="getObjectsData"
            @click="tableActions"
          />
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
          
          <template v-slot:after-heading-button v-if="!isReadonly">
            <v-btn
              class="primary"
              @click="addNewFile"
            >
              <v-icon left dark>mdi-plus</v-icon>
              Нов файл
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="filesTable.headers"
            :items="filesTable.data"
            :pagination="filesTable.pagination"
            sortable
            :sort.sync="filesTable.sort"
            @getData="getDocumentsListData"
            @click="tableActions"
          />
        </base-material-card>
      </v-col>
    </v-row>

    <sell-announcement-object-modal ref="sellAnnouncementObjectModal" @update="getObjectsData" :announcement-data="data" @confirmAction="confirmAction" @closeConfirm="$refs.confirmModal.closeModal()" />
    <sell-announcement-upload-file-modal ref="sellAnnouncementUploadFileModal" :parent-data="data" @reloadData="getDocumentsListData" @setDocumentCollectionId="setDocumentCollectionId" />
    <cases-list-selection-modal ref="casesListSelectionModal" @selected="selectedCase" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, bytesToSize, downloadFileFromResponse } from '@/utils';
import { eSellAnnouncementStatus, eSellAnnouncementLocationType } from "@/utils/enums/enumerators";
import SellAnnouncementModel from '@/models/sellAnnouncements/SellAnnouncementModel'
import { SellAnnouncementObjectModal, CasesListSelectionModal, SellAnnouncementUploadFileModal } from "@/components";
import { apiGetAnnouncementById, apiGetAnnouncementObjects, apiCreateAnnouncement, apiUpdateAnnouncement, apiPublishAnnouncement, apiGetAnnouncementCaseById, apiDeleteObject, apiCancelAnnouncement, apiSendAnnouncementForCorrectionInDraft } from "@/api/sellAnnouncements";
import { 
  apiMetaDataGetCourts, 
  apiMetaDataGetSyndics, 
  apiMetaDataGetOfferingLocationType, 
  apiMetaDataGetOfferingProcedure, 
  apiMetaDataGetOfferingKind, 
  apiMetaDataGetPapersForSale, 
  apiMetaDataGetSalesProcedure, 
  apiMetaDataGetSellAnnouncementTemplates,
  apiGetDistricts, 
  apiGetMunicipalities, 
  apiGetSettlements 
} from "@/api/metaData";
import { apiGetAllDocumentFiles, apiDeleteDocument, apiDownloadDocument } from "@/api/documents";
import { result } from 'lodash';


export default {
  name: "sellAnnouncementPreview",
  components: {
    SellAnnouncementObjectModal,
    SellAnnouncementUploadFileModal,
    CasesListSelectionModal
  },
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {
        courts: [],
        syndics: [],
        syndics2: [],
        places: [],
        offeringKinds: [],
        offeringProcedures: [],
        papersForSale: [],
        salesProcedures: [],
        templates: [],
      },
      syndicData: null,
      secondSyndicData: null,
      data: Object.assign({}, new SellAnnouncementModel()),
      caseData: Object.assign({}, {
        number: null,
        year: null,
        court: {}
      }),
      objectsTable: {
        headers: [
          { title: "Тип", field: "objectType", sortable: false },
          { title: "Вид", field: "objectKind", sortable: false },
          { title: "Адрес", field: "fullAddress", sortable: false },
          { title: "Състояние", field: "condition", sortable: false },
          { title: "Стойност", field: "value", sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              {
                title: "Преглед",
                icon: "mdi-text-box-search-outline",
                color: "white",
                class: "primary mr-1",
                click: "previewObject",
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
      filesTable: {
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
        required: v => !!v || v === 0 || 'Полето е задължително.',
      },
    }
  },
  mounted(){
    this.isNewDoc = true;
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
    } else {
      this.data = Object.assign({}, new SellAnnouncementModel());
      this.getSyndics();
      this.getSyndics2();
    }

    this.getDistricts();
    this.getSellAnnouncementTemplates();
    this.getCourts();
    this.getOfferingLocationTypes();
    this.getOfferingKinds();
    this.getOfferingProcedures();
    this.getPapersForSale();
    this.getSalesProcedures();
    this.getData();
  },
  computed: {
    ...mapGetters([
      'isMEIEmployee',
      'currentUser'
    ]),
    showPublishButton(){
      return !this.isNewDoc && this.isMEIEmployee && !this.isPublished && !this.isCanceled;
    },
    isDraft(){
      return this.isNewDoc || (!this.isNewDoc && this.data.statusCode === eSellAnnouncementStatus.DRAFT)
    },
    isPublished(){
      return !this.isNewDoc && this.data.statusCode === eSellAnnouncementStatus.PUBLISHED
    },
    isCanceled(){
      return !this.isNewDoc && this.data.statusCode === eSellAnnouncementStatus.CANCELED
    },
    isReadonly(){
      return this.isPublished || this.isCanceled;
    }
  },
  watch: {
    isDraft: {
      handler(isDraftValue){
        if(isDraftValue){
          this.filesTable.headers[this.filesTable.headers.length - 1].width = '80px';
          this.filesTable.headers[this.filesTable.headers.length - 1].buttons.push({
            title: "Изтрий",
            icon: "mdi-trash-can-outline",
            color: "white",
            class: "secondary",
            click: "deleteFileAsk",
          })
          this.objectsTable.headers[this.objectsTable.headers.length - 1].width = '80px';
          this.objectsTable.headers[this.objectsTable.headers.length - 1].buttons.push({
            title: "Изтрий",
            icon: "mdi-trash-can-outline",
            color: "white",
            class: "secondary",
            click: "deleteObjectAsk",
          })
        }
      },
      immediate: true
    }
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetAnnouncementById(this.data.id).then(result => {
          if(result){
            this.isNewDoc = false;
            if(!result.address)
              result.address = {};

            this.$set(this, 'data', Object.assign({address: {}}, result));
            
            this.getAddressNomenclatures();

            this.getSyndics()

            this.getSyndics2()

            this.getObjectsData();

            if(this.data.documentCollectionId)
              this.getDocumentsListData();

            
            this.$refs.form.resetValidation();
          }
        })
    },
    save(){
      //if(this.$refs.form.validate()){
        if(this.isNewDoc){
          //if(this.data.caseId){
          if(!this.data.offeringDate)
            this.data.offeringDate = new Date();

          apiCreateAnnouncement(this.data).then(result => {
            if(result && result.length && result.indexOf('00000000-0000-') === -1)
              this.$router.push({path: `/mii-sell-announcements/${result}`})
          })
          // } else {
          //   this.$snotify.warning("Моля изберете дело.")
          // }
        } else {
          apiUpdateAnnouncement(this.data).then(result => {
            if(result)
              this.$set(this, "data", result)
          })
        }
      //}
    },
    publishAsk(){
      let confirmData = {
        title: "Публикуване на обява",
        body: `Сигурни ли сте, че искате да публикувате обявата?`,
        callback: this.publish
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    publish(){
      if(this.$refs.form.validate()){
        apiPublishAnnouncement(this.data.id).then(result => {
          if(result){
            this.getData();
            this.$refs.confirmModal.closeModal();
          }
        })
      }
    },
    getCourts(){
      apiMetaDataGetCourts().then((result) => {
        this.$set(this.nomenclatures, "courts", result);
      });
    },
    getSyndics(){
      if(!this.isNewDoc){
        let query = {pageSize: 10, pageNumber: 1, searchCriteria: null, syndicId: this.data.syndicId}
        apiMetaDataGetSyndics(query).then((result) => {
          this.$nextTick(() => {
            this.$set(this.nomenclatures, "syndics", result.items);
          })
          if(result.items)
            this.$set(this, 'syndicData', result.items[0])
        });
      } else {
        apiMetaDataGetSyndics().then(result => {
          this.$set(this.nomenclatures, "syndics", result.items);
        
        })
      }
    },
    getSyndics2(){
      if(!this.isNewDoc){
        let query = {pageSize: 10, pageNumber: 1, searchCriteria: null, syndicId: this.data.secondSyndicId}
        apiMetaDataGetSyndics(query).then((result) => {
          this.$nextTick(() => {
            this.$set(this.nomenclatures, "syndics2", result.items);
          })
          
          if(result.items)
            
            this.$set(this, 'secondSyndicData', result.items[0])
        });
      } else {
        apiMetaDataGetSyndics().then(result => {
          this.$set(this.nomenclatures, "syndics2", result.items);
        
        })
      }
    },
    setPlaceOfSellingAddresToSyndicAddress(){
      if(this.syndicData){
        this.data.address = Object.assign({}, this.syndicData.address);

        if(this.isNewDoc)
          this.data.address.id = null;

        this.getAddressNomenclatures();
      }
    },
    getOfferingLocationTypes(){
      apiMetaDataGetOfferingLocationType().then(result => {
        this.$set(this.nomenclatures, "places", result);
      })
    },
    getOfferingKinds(){
      apiMetaDataGetOfferingKind().then(result => {
        this.$set(this.nomenclatures, "offeringKinds", result);
      })
    },
    getOfferingProcedures(){
      apiMetaDataGetOfferingProcedure().then(result => {
        this.$set(this.nomenclatures, "offeringProcedures", result);
      })
    },
    getPapersForSale(){
      apiMetaDataGetPapersForSale().then(result => {
        this.$set(this.nomenclatures, "papersForSale", result);
      })
    },
    getSalesProcedures(){
      apiMetaDataGetSalesProcedure().then(result => {
        this.$set(this.nomenclatures, "salesProcedures", result);
      })
    },
    getSellAnnouncementTemplates(){
      apiMetaDataGetSellAnnouncementTemplates().then(result => {
        this.$set(this.nomenclatures, "templates", result);
        if(this.isNewDoc)
          for(let templ of this.nomenclatures.templates){
            if(this.getTemplateFieldByNumber(templ.number))
              this.$set(this.data, this.getTemplateFieldByNumber(templ.number), templ.description);
          }
      })
    },
    setDocumentCollectionId(documentCollectionId){
      if(!this.data.documentCollectionId)
        this.$set(this.data, "documentCollectionId", documentCollectionId)
    },
    getDocumentsListData(){
      if(this.data.documentCollectionId){
        let query = Object.assign({}, {});
        
        query.pageNumber = this.filesTable.pagination.skip / this.filesTable.pagination.take + 1;
        query.pageSize = this.filesTable.pagination.take;
        query.filter = [this.data.documentCollectionId]
        
        apiGetAllDocumentFiles(query).then((result) => {
          this.filesTable.data = result.items;
          this.filesTable.pagination.total = result.totalCount;
          this.filesTable.pagination.page = result.currentPage;
        });
      }
    },
    getObjectsData(){
      let query = {
        pageNumber: this.objectsTable.pagination.page,
        pageSize: this.objectsTable.pagination.take,
        id: this.data.id
      }
      apiGetAnnouncementObjects(query).then(result => {
        this.$set(this.objectsTable, "data", result.items);
        this.$set(this.objectsTable.pagination, "total", result.totalCount);
        this.$set(this.objectsTable.pagination, "page", result.currentPage);
      })
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "download":
          this.download(rowData);
        break;
        case "deleteFileAsk":
          this.deleteFileAsk(rowData);
        break;
        case "previewObject":
          this.$refs.sellAnnouncementObjectModal.openModal(rowData.id);
        break;
        case "deleteObjectAsk":
          this.deleteObjectAsk(rowData);
        break;
      }
    },
    deleteObjectAsk(fileData){
      if(this.isDraft){
        let confirmData = {
          title: "Изтриване на имущество",
          body: `Сигурни ли сте, че искате да изтриете избраното имущество?`,
          callback: this.deleteObject,
          parameter: fileData.id
        };
        this.$refs.confirmModal.openModal(confirmData);
      }
    },
    deleteObject(fileId){
      apiDeleteObject(fileId).then(result => {
        if(result){
          this.getObjectsData();
          this.$refs.confirmModal.closeModal();
        }
      })
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
    confirmAction(confirmData){
      this.$refs.confirmModal.openModal(confirmData);
    },
    getAddressNomenclatures(){
      if(this.data.address.regionId)
        apiGetMunicipalities(this.data.address.regionId).then((result) => {
          this.$set(this.nomenclatures, "municipalities", result);

          if(this.data.address.municipalityId)
            apiGetSettlements(this.data.address.municipalityId).then((result) => {
              this.$set(this.nomenclatures, "settlements", result);
            });
        });
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
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
    addNewFile(){
      this.$refs.sellAnnouncementUploadFileModal.openModal()
    },
    addNewObject(){
      this.$refs.sellAnnouncementObjectModal.openModal()
    },
    selectedCase(item){
      apiGetAnnouncementCaseById(item.id).then(result => {
        this.$set(this, 'caseData', result);
        this.$set(this.data, 'caseId', item.id);
        this.$set(this.data, 'caseNumber', result.number);
        this.$set(this.data, 'caseYear', result.year);
        if(result?.court?.id)
          this.$set(this.data, 'courtId', result?.court?.id);

        if(result?.side?.entity?.name)
          this.$set(this.data, 'debtorName', result?.side?.entity?.name)

        if(result?.side?.entity?.bulstat)
          this.$set(this.data, 'debtorEik', result?.side?.entity?.bulstat)
      })
    },
    getTemplateFieldByNumber(number){
      switch(number){
        case 1:
          return 'participationLimitations';
        case 2:
          return 'pricePaymentTerms';
        case 3:
          return 'determinationFollowupBuyers';
        case 4:
          return 'awarded';
        case 5:
          return 'riskTransfer';
        case 6:
          return 'coownershipSale'
        case 7:
          return 'mortgagedPropertySale'
        default: 
          return null;
      }
    },
    changedSyndic(item){
      this.syndicData = item;
    },
    changedSecondSyndic(item){
      this.secondSyndicData = item;
    },
    openCasesModal(){
      this.$refs.casesListSelectionModal.openModal()
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
    changedLocationType(val){
      if(val === eSellAnnouncementLocationType.SYNDIC){
        this.setPlaceOfSellingAddresToSyndicAddress();
      } else if(val === eSellAnnouncementLocationType.DEBTOR){
        this.$set(this.data, 'address', {});
      }
    },
    cancelAnnouncementAsk(){
      let confirmData = {
        title: "Отмяна на обява",
        body: `Сигурни ли сте, че искате да отмените обявата?`,
        callback: this.cancelAnnouncement
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    cancelAnnouncement(){
      apiCancelAnnouncement(this.data.id).then(result => {
        if(result){
          this.getData();
          this.$refs.confirmModal.closeModal();
        }
      })
    },
    sendAnnouncementForCorrectionInDraftAsk(){
      let confirmData = {
        title: "Връщане в редакция",
        body: `Сигурни ли сте, че искате да върнете обявата за редакция?`,
        callback: this.sendAnnouncementForCorrectionInDraft
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    sendAnnouncementForCorrectionInDraft(){
      apiSendAnnouncementForCorrectionInDraft(this.data.id).then(result => {
        if(result){
          this.getData();
          this.$refs.confirmModal.closeModal();
        }
      })
    },
    ISODateString: ISODateString
  }
}
</script>