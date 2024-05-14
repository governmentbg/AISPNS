<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      :heading="isNewDoc ? 'Нова обява' : 'Преглед на обява'"
      goBackText="Обратно към обяви за продажба"
      goBackTo="/sell-announcements"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4" v-if="false">
          Номер: <strong> {{  }} </strong> <br />
          Публикувана на: <strong> {{ ISODateString(data.publishedDate) }} </strong> <br />
          Създадена на: <strong> {{ ISODateString(data.dateCreated) }} </strong> <br />
          Последна промяна: <strong> {{ ISODateString(data.dateModified) }} </strong> <br />
        </v-sheet>
      </v-col>
    </v-row>
    <v-row class="my-10">
      <v-col cols="12">
        <base-material-card 
          icon="mdi-gavel"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Дело
          </template>
          
          <v-row>
            <v-col cols="12" xl="2" lg="4" md="6">
              <base-input
                label="Дело номер"
                v-model="data.case.number"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="2" lg="4" md="6">
              <base-input
                label="Дело година"
                v-model="data.case.year"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="5" lg="4" md="6">
              <base-input
                label="Съд"
                :value="data.case.court.name"
                readonly
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-input
                label="Синдик"
                :value="syndicName"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-input
                label="Длъжник име"
                v-model="debtorName"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-input
                label="Длъжник ЕИК"
                v-model="debtorBulstat"
                readonly
              />
            </v-col>
            <v-col cols="12" v-if="false">
              <base-input
                label="Адрес на длъжника"
                v-model="data.debtorAddress"
                readonly
              />
            </v-col>
          </v-row>
        </base-material-card>
        <base-material-card 
          icon="mdi-magnify-expand"
          color="primary"
          class="elevation-3 mt-15"
        >
          <template v-slot:after-heading>
            Обява за продажба
          </template>
          
          <v-row>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-date-time-picker
                label="Дата и час продажба"
                v-model="data.offeringDate"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-date-time-picker
                label="Краен срок за приемане на предложение"
                v-model="data.offerDeadline"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="6" lg="4" md="6">
              <base-input
                label="Място на продажбата"
                v-model="data.locationType"
                readonly
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <h5>Място на продажбата адрес</h5>
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="4">
              <base-input
                label="Област"
                :value="data.address?.regionName"
                class="pb-0"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="4">
              <base-input
                label="Община"
                :value="data.address?.municipalityName"
                class="pb-0"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="4">
              <base-input
                  label="Населено място"
                :value="data.address?.settlementName"
                class="pb-0"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <base-input
                :value="data.address?.postCode"
                label="ПК"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="5" lg="6" md="4">
              <base-input
                :value="data.address?.streetName"
                label="Улица"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <base-input
                :value="data.address?.streetNumber"
                label="Улица №"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <base-input
                :value="data.address?.buildingNumber"
                label="Сграда №"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <base-input
                :value="data.address?.entranceNumber"
                label="Вход"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <base-input
                :value="data.address?.floorNumber"
                label="Етаж"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <base-input
                :value="data.address?.apartmentNumber"
                label="Ап. №"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="2" lg="3" md="4">
              <base-input
                label="ЕКАТТЕ"
                :value="data.address?.ekkate"
                readonly
              />
            </v-col>
          </v-row>
        </base-material-card>


        <base-material-card 
          icon="mdi-bullhorn-outline"
          color="primary"
          class="elevation-3 mt-15"
        >
          <template v-slot:after-heading>
            Обява
          </template>
          <v-row>
            <v-col cols="12" xl="4" lg="4" md="6">
              <base-input
                label="Втори синдик"
                :value="secondSyndicName"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="4" lg="4" md="6">
              <base-input
                label="Ред на продажбата"
                v-model="data.offeringKind"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="4" lg="4" md="6">
              <base-input
                label="Начин на продажбата"
                v-model="data.offeringProcedure"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="4" lg="4" md="6">
              <base-input
                label="Начална продажна цена"
                v-model="data.startingPrice"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="4" lg="4" md="6">
              <base-input
                label="Внасяне на зададък"
                v-model="data.retainer"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="4" lg="4" md="6">
              <base-input
                label="Книжа за продажбата"
                v-model="data.papersForSale"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="8" lg="8">
              <base-input
                label="Описание на процеса на продажба"
                v-model="data.salesProcedure"
                readonly
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <h5>Информация</h5>
            </v-col>
            <v-col cols="12" v-html="data.text"></v-col>
          </v-row>
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

        <base-material-card 
          icon="mdi-book-outline"
          color="primary"
          class="elevation-3 mt-15"
        >
          <template v-slot:after-heading>
            Описание на имущество
          </template>

          <base-kendo-grid
            :columns="objectsTable.headers"
            :items="objectsData"
            :hasPaging="false"
          />
        </base-material-card>

        <base-material-card 
          icon="mdi-file-multiple-outline"
          color="primary"
          class="elevation-3 mt-15"
        >
          <template v-slot:after-heading>
            Файлове
          </template>

          <base-kendo-grid
            :columns="filesTable.headers"
            :items="filesData"
            :hasPaging="false"
            @click="tableActions"
          />
        </base-material-card>
      </v-col>
    </v-row>

    <sell-announcement-object-modal ref="sellAnnouncementObjectModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, bytesToSize, downloadFileFromResponse } from '@/utils';
import { SellAnnouncementObjectModal } from "@/components";
import { apiGetAnnouncementById } from '@/api/announcements';
import { 
  apiMetaDataGetCourts, 
  apiMetaDataGetSyndics, 
  apiMetaDataGetOfferingLocationType, 
  apiMetaDataGetOfferingProcedure, 
  apiMetaDataGetOfferingKind, 
  apiMetaDataGetPapersForSale, 
  apiMetaDataGetSalesProcedure, 
  apiMetaDataGetSellAnnouncementTemplates 
} from "@/api/metaData";
import { apiDownloadDocument } from '@/api/documents';


export default {
  name: "sellAnnouncementPreview",
  components: {
    SellAnnouncementObjectModal
  },
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {
        courts: [],
        syndics: [],
        places: [],
        offeringKinds: [],
        offeringProcedures: [],
        papersForSale: [],
        salesProcedures: []
      },
      data: Object.assign({}, {address: {}, case: {court: {}}}),
      objectsTable: {
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
      filesTable: {
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
  mounted(){
    this.isNewDoc = true;

    this.objectsTable.headers = [
      { title: "Тип", field: "objectType", sortable: false },
      { title: "Вид", field: "objectKind", sortable: false },
      { title: "Адрес", cell: this.renderAddress, sortable: false },
      { title: "Състояние", field: "condition", sortable: false },
      { title: "Стойност", field: "value", sortable: false }
    ]
    this.filesTable.headers = [
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
          },
        ],
      },
    ]
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
    } else {
      this.data.inspector = this.currentUser.firstAndLastName;
    }

    this.getSellAnnouncementTemplates();
    this.getCourts();
    this.getSyndics();
    this.getOfferingLocationTypes();
    this.getOfferingKinds();
    this.getOfferingProcedures();
    this.getPapersForSale();
    this.getSalesProcedures();
    this.getData();
  },
  computed: {
    ...mapGetters([
      'currentUser'
    ]),
    syndicName(){
      return this.data.syndic ? this.data.syndic.firstName + ' ' +this.data.syndic.lastName : '';
    },
    secondSyndicName(){
      return this.data.secondSyndic ? this.data.secondSyndic.firstName + ' ' +this.data.secondSyndic.lastName : '';
    },
    debtorName(){
      return this.data.case?.sides?.length ? this.data.case.sides[0].entity.name : '';
    },
    debtorBulstat(){
      return this.data.case?.sides?.length ? this.data.case.sides[0].entity.bulstat : '';
    },
    objectsData(){
      if(this.data.objects?.length)
        return this.data.objects;
      
      return []
    },
    filesData(){
      if(this.data.documentCollection?.documentContents?.length)
        return this.data.documentCollection.documentContents;
      
      return []
    }
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetAnnouncementById(this.data.id).then(result => {
          this.$set(this, 'data', Object.assign({address: {}}, result));
        })
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "download":
          this.download(rowData);
        break;
      }
    },
    getCourts(){
      apiMetaDataGetCourts().then((result) => {
        this.$set(this.nomenclatures, "courts", result);
      });
    },
    getSyndics(){
      apiMetaDataGetSyndics().then((result) => {
        this.$set(this.nomenclatures, "syndics", result.items);
      });
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
          return 'asd';
        case 6:
          return 'coownershipSale'
        case 7:
          return 'mortgagedPropertySale'
        default: 
          return null;
      }
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
    renderAddress(h, tdElement, props) {
      let result = '';
      let item = props.dataItem;

      if(item.address){
        result += item.address.regionName + ', ' + item.address.municipalityName + ', ' + item.address.settlementName;
        if(item.address.streetName)
          result += ', ' + item.address.streetName + ' ' + item.address.streetNumber;
        if(item.address.buildingNumber)
          result += ', № ' + item.address.buildingNumber;
        if(item.address.entranceNumber)
          result += ', вх. ' + item.address.entranceNumber;
        if(item.address.floorNumber)
          result += ', ет. ' + item.address.floorNumber;
        if(item.address.apartmentNumber)
          result += ', ап. ' + item.address.apartmentNumber;
      }

      return h('td', null, [
        result
      ]);
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