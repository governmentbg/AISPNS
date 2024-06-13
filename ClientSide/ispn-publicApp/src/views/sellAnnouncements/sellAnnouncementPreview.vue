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

    <v-row class="mt-5 d-print-none" v-if="false">
      <v-col cols="12" lg="4" offset-lg="8" md="6" offset-md="6">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4">
          {{ $t('number') }}: <strong> {{  }} </strong> <br />
          {{ $t('published_on_f') }}: <strong> {{ ISODateString(data.publishedDate) }} </strong> <br />
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
            {{ $t('case') }}
          </template>

          <v-container class="py-3">
            <v-row>
              <v-col cols="12" xl="2" lg="4" md="6">
                <base-input
                  :label="$t('case_number')"
                  v-model="data.caseNumber"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="6">
                <base-input
                  :label="$t('case_year')"
                  v-model="data.caseYear"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="5" lg="4" md="6">
                <base-input
                  :label="$t('court')"
                  :value="data.courtName"
                  readonly
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  :label="$t('syndic')"
                  :value="syndicName"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  :label="$t('debtor_name')"
                  v-model="debtorName"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  :label="$t('debtor_egn')"
                  v-model="debtorBulstat"
                  readonly
                />
              </v-col>
              <v-col cols="12" v-if="false">
                <base-input
                  :label="$t('debtor_address')"
                  v-model="data.debtorAddress"
                  readonly
                />
              </v-col>
            </v-row>
          </v-container>
        </base-material-card>
        <base-material-card 
          icon="mdi-magnify-expand"
          color="primary"
          class="elevation-3 mt-15"
        >
          <template v-slot:after-heading>
            {{ $t('sell_announcement') }}
          </template>


          <template v-slot:after-heading-button v-if="data.publishDate">
            <div class="pr-3 pt-3">
              <strong>{{ $t('published_on_f') }}:</strong>
              {{ data.publishDate ? ISODateString(data.publishDate) : ' - ' }} <br />
              <strong>{{ $t('status') }}:</strong>
              {{ data.status ? data.status : ' - ' }}
            </div>
          </template>

          <v-container class="py-3">
            <v-row>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-date-time-picker
                  :label="$t('sell_announcement_date_hour')"
                  v-model="data.offeringDate"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="4" lg="4" md="6">
                <base-date-time-picker
                  :label="$t('sell_announcement_end')"
                  v-model="data.offerDeadline"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="5" lg="4" md="6">
                <base-input
                  :label="$t('sell_announcement_place')"
                  v-model="data.locationType"
                  readonly
                />
              </v-col>
              <v-col cols="12">
                <h5>{{ $t('sell_announcement_place_address') }}</h5>
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="4">
                <base-input
                  :label="$t('region')"
                  :value="data.address?.regionName"
                  class="pb-0"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="4">
                <base-input
                  :label="$t('municipality')"
                  :value="data.address?.municipalityName"
                  class="pb-0"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="4">
                <base-input
                  :label="$t('settlement')"
                  :value="data.address?.settlementName"
                  class="pb-0"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="2" md="2">
                <base-input
                  :value="data.address?.postCode"
                  :label="$t('postal_code')"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="5" lg="6" md="4">
                <base-input
                  :value="data.address?.streetName"
                  :label="$t('address_street')"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  :value="data.address?.streetNumber"
                  :label="$t('address_street_number')"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  :value="data.address?.buildingNumber"
                  :label="$t('address_building_number')"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  :value="data.address?.entranceNumber"
                  :label="$t('address_entrance')"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  :value="data.address?.floorNumber"
                  :label="$t('address_floor')"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  :value="data.address?.apartmentNumber"
                  :label="$t('address_apartment')"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="4">
                <base-input
                  :label="$t('address_ekatte')"
                  :value="data.address?.ekkate"
                  readonly
                />
              </v-col>
            </v-row>
          </v-container>
        </base-material-card>


        <base-material-card 
          icon="mdi-bullhorn-outline"
          color="primary"
          class="elevation-3 mt-15"
        >
          <template v-slot:after-heading>
            {{ $t('announcement') }}
          </template>

          <v-container class="py-3">
            <v-row>
              <v-col cols="12" xl="4" lg="4" md="6">
                <base-input
                  :label="$t('second_syndic')"
                  :value="secondSyndicName"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="4" lg="4" md="6">
                <base-input
                  :label="$t('sale_accordance')"
                  v-model="data.offeringKind"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="4" lg="4" md="6">
                <base-input
                  :label="$t('sale_method')"
                  v-model="data.offeringProcedure"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="4" lg="4" md="6">
                <base-input
                  :label="$t('sale_initial_price')"
                  v-model="data.startingPrice"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="4" lg="4" md="6">
                <base-input
                  :label="$t('sale_deposit')"
                  v-model="data.retainer"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="4" lg="4" md="6">
                <base-input
                  :label="$t('sale_papers')"
                  v-model="data.papersForSale"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="4" lg="4" md="6">
                <base-input
                  :label="$t('sale_process_description')"
                  v-model="data.salesProcedure"
                  readonly
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12">
                <h5>{{ $t('information') }}</h5>
              </v-col>
              <v-col cols="12" v-html="data.text" />
            </v-row>
          </v-container>
        </base-material-card>


        <base-material-card 
          color="primary"
          class="elevation-3 mt-10"
          v-if="showTableTemplateDetails"
        >
          <v-container class="py-1">
            <v-simple-table class="bordered">
              <template v-slot:default>
                <tbody>
                  <tr>
                    <td width="30%">{{ $t('restriction_on_participation_in_the_sale') }}:</td>
                    <td>
                      {{ data.participationLimitations }}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      {{ $t('term_for_payment_of_price') }}:
                    </td>
                    <td>
                      {{ data.pricePaymentTerms }}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      {{ $t('designation_of_subsequent_purchasers') }}:
                    </td>
                    <td>
                      {{ data.determinationFollowupBuyers }}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      {{ $t('assignment') }}:
                    </td>
                    <td>
                      {{ data.awarded }}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      {{ $t('introduction_and_passing_of_risk') }}:
                    </td>
                    <td>
                      {{ data.riskTransfer }}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      {{ $t('sale_under_shared_ownership') }}:
                    </td>
                    <td>
                      {{ data.coownershipSale }}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      {{ $t('sale_of_mortgaged_or_pledged_property') }}:
                    </td>
                    <td>
                      {{ data.mortgagedPropertySale }}
                    </td>
                  </tr>
                </tbody>
              </template>
            </v-simple-table>
          </v-container>
        </base-material-card>

        <base-material-card 
          icon="mdi-book-outline"
          color="primary"
          class="elevation-3 mt-15"
          v-if="objectsData && objectsData.length"
        >
          <template v-slot:after-heading>
            {{ $t('property_description') }}
          </template>

          <v-container class="py-3">
            <v-row>
              <v-col cols="12">
                <base-data-table
                  :headers="objectsTable.headers"
                  :data="objectsData"
                  :hasPaging="false"
                  @doAction="tableActions"
                  class="mb-7"
                />
              </v-col>
            </v-row>
          </v-container>
        </base-material-card>

        <base-material-card 
          icon="mdi-file-multiple-outline"
          color="primary"
          class="elevation-3 mt-15"
          v-if="filesData && filesData.length"
        >
          <template v-slot:after-heading>
            {{ $t('files') }}
          </template>

          <base-data-table
            :headers="filesTable.headers"
            :data="filesData"
            :hasPaging="false"
            @doAction="tableActions"
            class="mb-7"
          />
        </base-material-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { ISODateString, downloadFileFromResponse } from '@/utils';
import { apiGetAnnouncementById } from "@/api/announcements";
import { apiDownloadDocument } from "@/api/documents";
export default {
  name: "sellAnnouncementPreview",
  components: {
  },
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {},
      data: Object.assign({}, {
        address: {}, 
        case: {},
        objects: []
      }),
      objectsTable: {
        headers: [
          { text: this.$t('type'), value: "objectType", valueType: 'text', sortable: false },
          { text: this.$t('kind'), value: "objectKind", valueType: 'text', sortable: false },
          { text: this.$t('address'), value: "address", valueType: 'address', sortable: false },
          { text: this.$t('condition'), value: "condition", valueType: 'text', sortable: false },
          { text: this.$t('cost'), value: "value", valueType: 'text', sortable: false },
        ],
        data: [],
        sort: {},
        pagination: {
          itemsPerPage: 15,
          page: 1,
          perPageOptions: [5, 15, 25, 50, 100],
          total: 0
        },
      },
      filesTable: {
        headers: [
          { text: this.$t('file'), value: "fileName", valueType: 'text', sortable: false },
          { text: this.$t('description'), value: "description", valueType: 'text', sortable: false },
          { text: this.$t('file_size'), value: "fileSize", valueType: 'fileSize', sortable: false },
          {
            sortable: false,
            text: '',
            value: '',
            valueType: 'button',
            style: "width: 50px;",
            class: 'text-right',
            buttons: [
              { label: '', title: this.$t('download'), icon: 'mdi-download', size: 'small', class: 'transparent primary--text', click: 'download' }
            ],
          }
        ],
        data: [],
        sort: {},
        pagination: {
          itemsPerPage: 15,
          page: 1,
          perPageOptions: [5, 15, 25, 50, 100],
          total: 0
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
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
    } else {
      this.data.inspector = this.currentUser.firstAndLastName;
    }

    this.getData();
  },
  computed: {
    syndicName(){
      return this.data.syndic ? this.data.syndic.fullName : '';
    },
    secondSyndicName(){
      return this.data.secondSyndic ? this.data.secondSyndic.fullName : '';
    },
    debtorName(){
      return this.data?.debtorName?.length ? this.data.debtorName : '';
    },
    debtorBulstat(){
      return this.data?.debtorEik?.length ? this.data.debtorEik : '';
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
    },
    showTableTemplateDetails(){
      return this.data.participationLimitations || this.data.pricePaymentTerms || this.data.determinationFollowupBuyers || this.data.awarded || this.data.riskTransfer || this.data.coownershipSale || this.data.mortgagedPropertySale;
    }
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetAnnouncementById(this.data.id).then(result => {
          if(result){
            this.$set(this, 'data', result);
            this.$forceUpdate();
          } else {
            this.$router.push('/sell-announcements');
          }
        })
    },
    getUploadedFilesData(){

    },
    getObjectsData(){

    },
    tableActions({ action, item }) {
      switch (action) {
        case "download":
          this.download(item.id);
        break;
      }
    },
    download(id){
      apiDownloadDocument(id).then(result => {
        downloadFileFromResponse(result);
      });
    },
    ISODateString: ISODateString
  }
}
</script>