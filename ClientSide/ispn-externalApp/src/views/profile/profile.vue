<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isSyndic"
  >
    <base-v-component
      heading="Моят профил"
    />

    <v-row class="my-10">
      <v-col cols="12">
        <base-material-card 
          icon="mdi-account-tie"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Моят профил
          </template>
          
          <template v-slot:after-heading-button>
            <v-switch
              v-model="data.isActive"
              label="Активност"
              class="d-inline-block"
              readonly
            />
          </template>

          <v-form
            ref="form"
            lazy-validation
            class="pa-3"
          >
            <v-row>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Име"
                  v-model="data.firstName"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Презиме"
                  v-model="data.secondName"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Фамилия"
                  v-model="data.lastName"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Втора фамилия"
                  v-model="data.secondLastName"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="ЕГН"
                  v-model="data.egn"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="E-mail"
                  v-model="data.email"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Телефон"
                  v-model="data.phone"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Специалност"
                  v-model="data.syndicSpecialty"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Статус"
                  v-model="data.syndicStatus"
                  readonly
                />
              </v-col>
            </v-row>    
            <v-row>
              <v-col cols="12" xl="3" lg="4" md="4">
                <base-autocomplete
                  label="Област"
                  class="pb-0"
                  v-model="data.address.regionId"
                  :items="nomenclatures.districts"
                  item-text="name"
                  item-value="id"
                  readonly
                  :clearable="false"
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
                  readonly
                  :clearable="false"
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
                  :clearable="false"
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
                  label="ЕКАТТЕ"
                  readonly
                />
              </v-col>
            </v-row>
          </v-form>
        </base-material-card>

        <base-material-card 
          icon="mdi-stamper"
          color="primary"
          class="elevation-3 mt-10"
        >
          <template v-slot:after-heading>
            Заповед
          </template>
          
          <v-form
            ref="orderForm"
            lazy-validation
            class="pa-3"
          >
            <v-row>
              <v-col cols="12">
                <base-input
                  label="Утвърден/а със заповед след изпит за придобиване на квалификация синдик"
                  :value="data.order?.number"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Номер заповед"
                  :value="data.order?.number"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-material-date-picker
                  label="Дата заповед"
                  :value="data.order?.date"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Държавен вестник брой"
                  :value="data.order?.stateGazetteEdition"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Държавен вестник година"
                  :value="data.order?.stateGazetteYear"
                  type="number"
                  readonly
                />
              </v-col>
            </v-row>
          </v-form>
        </base-material-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString } from '@/utils';
import { apiGetDistricts, apiGetMunicipalities, apiGetSettlements } from "@/api/metaData";
import SyndicModel from '@/models/syndics/SyndicModel';
import { apiGetProfileInformation } from '@/api/metaData';
export default {
  name: "profile",
  components: {
  },
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {
        statuses: [],
        specialities: [],
        districts: [],
        municipalities: [],
        settlements: []
      },
      data: Object.assign({}, new SyndicModel()),
    }
  },
  mounted(){
    // TODO - isNewDoc = true;
    this.isNewDoc = false;
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
    }

    this.getDistricts();
    this.getData();
  },
  computed: {
    ...mapGetters([
      'isSyndic'
    ]),
  },
  methods: {
    getData(){
      apiGetProfileInformation().then(result => {
        this.$set(this, "data", result);
        this.getAddressNomenclatures();
        // TODO ...getting address nomenclatures
      })
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
    
    ISODateString: ISODateString
  }
}
</script>