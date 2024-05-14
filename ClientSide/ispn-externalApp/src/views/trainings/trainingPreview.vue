<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isSyndic"
  >
    <base-v-component
      :heading="$route.meta.title"
    />

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

          <v-container class="py-3">
            <v-form ref="form">
              <v-row>
                <v-col cols="12">
                  <base-autocomplete
                    label="Вид обучение"
                    v-model="data.type"
                    :items="nomenclatures.trainingTypes"
                    readonly
                  />
                </v-col>
                <v-col cols="12">
                  <base-input
                    label="Тема"
                    v-model="data.topic"
                    readonly
                  />
                </v-col>
                
                <v-col cols="12">
                  <v-textarea
                    rows="2"
                    auto-grow
                    label="Лектори"
                    v-model="data.lectors"
                    readonly
                  />
                </v-col>
                <v-col cols="12">
                  <v-textarea
                    rows="2"
                    auto-grow
                    label="Подтеми"
                    v-model="data.subTopics"
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
                    v-model="data.maxNumberParticipants"
                    type="number"
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
                    v-model="data.duration1"
                    readonly
                  />
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" xl="2" lg="4" md="4">
                  <base-autocomplete
                    label="Област"
                    class="pb-0"
                    v-model="data.address1.regionId"
                    :items="nomenclatures.districts"
                    item-text="name"
                    item-value="id"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="4" md="4">
                  <base-autocomplete
                    v-model="data.address1.municipalityId"
                    label="Община"
                    :items="nomenclatures.municipalities"
                    item-text="name"
                    item-value="id"
                    class="pb-0"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="4" md="4">
                  <base-autocomplete
                    label="Населено място"
                    class="pb-0"
                    v-model="data.address1.settlementId"
                    :items="nomenclatures.settlements"
                    item-text="name"
                    item-value="id"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address1.postCode"
                    label="ПК"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="5" lg="6" md="4">
                  <base-input
                    v-model="data.address1.streetName"
                    label="Улица"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address1.streetNumber"
                    label="Улица №"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address1.buildingNumber"
                    label="Сграда №"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address1.entranceNumber"
                    label="Вход"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address1.floorNumber"
                    label="Етаж"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address1.apartmentNumber"
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
                    v-model="data.duration2"
                    readonly
                  />
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" xl="2" lg="4" md="4">
                  <base-autocomplete
                    label="Област"
                    class="pb-0"
                    v-model="data.address2.regionId"
                    :items="nomenclatures.districts"
                    item-text="name"
                    item-value="id"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="4" md="4">
                  <base-autocomplete
                    v-model="data.address2.municipalityId"
                    label="Община"
                    :items="nomenclatures.municipalities"
                    item-text="name"
                    item-value="id"
                    class="pb-0"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="4" md="4">
                  <base-autocomplete
                    label="Населено място"
                    class="pb-0"
                    v-model="data.address2.settlementId"
                    :items="nomenclatures.settlements"
                    item-text="name"
                    item-value="id"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address2.postCode"
                    label="ПК"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="5" lg="6" md="4">
                  <base-input
                    v-model="data.address2.streetName"
                    label="Улица"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address2.streetNumber"
                    label="Улица №"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address2.buildingNumber"
                    label="Сграда №"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address2.entranceNumber"
                    label="Вход"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address2.floorNumber"
                    label="Етаж"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="1" lg="2" md="2">
                  <base-input
                    v-model="data.address2.apartmentNumber"
                    label="Ап. №"
                    readonly
                  />
                </v-col>
              </v-row>
            </v-form>
          </v-container>
        </base-material-card>


        <base-material-card 
          icon="mdi-school-outline"
          color="primary"
          class="elevation-3 mt-10"
        >
          <template v-slot:after-heading>
            Програма
          </template>

          <v-container class="py-3">
            <v-form
              ref="form"
              lazy-validation
            >
              <v-row>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <base-material-date-picker
                    label="От дата"
                    v-model="programData.fromDate"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <base-material-date-picker
                    label="До дата"
                    v-model="programData.toDate"
                    readonly
                  />
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <base-input
                    label="Заповед номер МП"
                    v-model="programData.orderNumberMP"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <base-material-date-picker
                    label="Заповед дата МП"
                    v-model="programData.orderDateMP"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <base-input
                    label="Заповед номер МИИ"
                    v-model="programData.orderNumberMII"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <base-material-date-picker
                    label="Заповед дата МИИ"
                    v-model="programData.orderDateMII"
                    readonly
                  />
                </v-col>
                <v-col cols="12">
                  <v-textarea
                    label="Обяснения"
                    v-model="programData.description"
                    rows="2"
                    auto-grow
                    readonly
                  />
                </v-col>
                <v-col cols="12">
                  <v-textarea
                    label="Допълнителни обяснения"
                    v-model="programData.additionalDescription"
                    rows="2"
                    auto-grow
                    readonly
                  />
                </v-col>
                <v-col cols="12">
                  <base-input
                    label="Бележки"
                    v-model="programData.notes"
                    readonly
                  />
                </v-col>
              </v-row>
            </v-form>
          </v-container>
        </base-material-card>


        <base-material-card 
          icon="mdi-book-open-variant"
          color="primary"
          class="elevation-3 mt-10"
        >
          <template v-slot:after-heading>
            Материали
          </template>

          <v-container class="py-3">
            <base-kendo-grid
              :columns="table.headers"
              :items="table.data"
              :pagination="table.pagination"
              sortable
              :sort.sync="table.sort"
              @getData="getMaterialsData"
              @click="tableActions"
            />
          </v-container>
        </base-material-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString } from '@/utils';
import TrainingModel from "@/models/trainings/TrainingModel";

export default {
  name: "trainingPreview",
  components: {},
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {
        trainingTypes: [],
        districts: [],
        municipalities: [],
        settlements: []
      },
      programData: {},
      data: Object.assign({}, new TrainingModel()),
      rules: {
        required: v => !!v || 'Полето е задължително.',
      },
      table: {
        headers: [
          { title: "Вид", field: "", sortable: true },
          { title: "Дата", field: "", sortable: true },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "download" },
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
    }
  },
  mounted(){
    // TODO: this.isNewDoc = true;
    this.isNewDoc = false;
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
      this.getData();
    }

    this.getTrainingTypes();
  },
  computed: {
    ...mapGetters([
      'isSyndic',
      'currentUser'
    ]),
  },
  methods: {
    getData(){
      // if(!this.isNewDoc)
      //   apiGetTrainingById(this.data.id).then(data => {
      //     this.data = data;
      //   })
    },
    getTrainingTypes(){
      // apiGetTrainingTypes().then(result => {
      //   this.nomenclatures.trainingTypes = result;
      // })
    },
    getDistricts(){

    },
    getMunicipalities() {
      if(this.data.address1.regionId) {
        // apiGetMunicipalities(this.data.address.regionId).then((result) => {
        //   this.$set(this.nomenclatures.municipalities, "municipalities", result);
        //   this.$set(this.data.address, "settlementId", null);
        //   this.$set(this.data.address, "municipalityId", null);
        // });
      } else {
        this.$set(this.nomenclatures, "municipalities", []);
        this.$set(this.nomenclatures, "settlements", []);
      }

      //Set ekatte to null
      this.data.address.ekkate = null;
    },
    getSettlements() {
      if (this.data.address.municipalityId) {
        // apiGetSettlements(this.data.address.municipalityId).then((result) => {
        //   this.$set(this.nomenclatures, "settlements", result);
        //   this.$set(this.data.address, "settlementId", null);
        // });
      } else {
        this.$set(this.nomenclatures, "settlements", []);
        this.$set(this.data.address, "settlementId", null);
      }

      //Set ekatte to null
      this.data.address.ekkate = null;
    },
    getMaterialsData(){

    },
    download(){

    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "download":
          this.download(rowData);
        break;
      }
    },
    ISODateString: ISODateString
  }
}
</script>