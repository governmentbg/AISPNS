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

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4">
          Създадена на: <strong> {{ ISODateString(data.dateCreated) }} </strong> <br />
          Дата на заявка: <strong> {{ ISODateString(data.requestDate) }} </strong> <br />
        </v-sheet>
      </v-col>
      <v-col cols="12" lg="7" xl="8" class="text-right">
        <v-btn
          color="primary"
        >
          <v-icon left dark>mdi-check</v-icon>
          Запази
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="my-10">
      <v-col cols="12">
        <base-material-card 
          icon="mdi-school-outline"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Заявка за обучение
          </template>

          <v-container class="py-3">
            <v-form
              ref="form"
              lazy-validation
            >
              <v-row>
                <v-col cols="12" xl="6">
                  <base-autocomplete
                    label="Програма"
                    v-model="data.program"
                    :rules="[rules.required]"
                  />
                </v-col>
                
                <v-col cols="12" xl="3" lg="6">
                  <base-input
                    label="Актуален E-Mail"
                    v-model="data.email"
                    :rules="[rules.required]"
                  />
                </v-col>
                
                <v-col cols="12" xl="3" lg="6">
                  <base-input
                    label="Актуален телефон"
                    v-model="data.phone"
                    :rules="[rules.required]"
                  />
                </v-col>
                
                <v-col cols="12">
                  <h5>Основно обучение</h5>
                </v-col>
                <v-col cols="12" xl="4" lg="6">
                  <base-autocomplete
                    label="Обучение (курс)"
                    v-model="data.training1"
                    :rules="[rules.required]"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <base-material-date-picker
                    label="От дата"
                    v-model="data.fromDate1"
                    :rules="[rules.required]"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <base-material-date-picker
                    label="До дата"
                    v-model="data.toDate1"
                    :rules="[rules.required]"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <v-checkbox
                    v-model="data.isLector1"
                    label="Лектор"
                    dense
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <v-checkbox
                    v-model="data.hotelReservation1"
                    label="Хотелска резервация"
                    dense
                  />
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12">
                  <h5>Алтернативно обучение</h5>
                </v-col>
                <v-col cols="12" xl="4" lg="6">
                  <base-autocomplete
                    label="Обучение (курс)"
                    v-model="data.training2"
                    :rules="[rules.required]"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <base-material-date-picker
                    label="От дата"
                    v-model="data.fromDate2"
                    :rules="[rules.required]"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <base-material-date-picker
                    label="До дата"
                    v-model="data.toDate2"
                    :rules="[rules.required]"
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <v-checkbox
                    v-model="data.isLector2"
                    label="Лектор"
                    dense
                  />
                </v-col>
                <v-col cols="12" xl="2" lg="3" md="6">
                  <v-checkbox
                    v-model="data.hotelReservation2"
                    label="Хотелска резервация"
                    dense
                  />
                </v-col>
              </v-row>
            </v-form>
          </v-container>
        </base-material-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString } from '@/utils';
import InspectionModel from '@/models/inspections/InspectionModel';
export default {
  name: "trainingRequestPreview",
  components: {
  },
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {
        programs: [],
      },
      data: Object.assign({}, new InspectionModel()),
      
      rules: {
        required: v => !!v || 'Полето е задължително.',
        requiredSelect: v => (typeof v === 'string' && v.length) || typeof v === 'boolean' || !isNaN(parseFloat(v)) && v >= 0 && v <= 999 || "Полето е задължително",
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

    this.getAvailableProgramsData();
  },
  computed: {
    ...mapGetters([
      'isSyndic',
      'currentUser'
    ]),
    getCurrentTabDetails(){
      if(this.tabs.length && this.tabs[this.currentTab]) 
        return this.tabs[this.currentTab];

      return null;
    },
  },
  methods: {
    getData(){
      // if(!this.isNewDoc)
      //   apiGetTrainingRequestById(this.data.id).then(data => {
      //     this.data = data;
      //   })
    },
    save(){
      if(this.$refs.form.validate()){
        if(this.isNewDoc){
          // apiCreateTrainingRequest(this.data).then(result => {
          //   if(result && result.length)
          //     this.$router.push({path: `/trainings/requests/${result}`})
          // })
        } else {
          //apiUpdateTrainingRequest(this.data)
        }
      }
    },
    getAvailableProgramsData(){
      // apiGetAvailablePrograms().then(result => {
      //   this.nomenclatures.programs = result;
      // })
    },
    getTrainingsBySelectedProgram(){
      // apiGetAvailableTrainingsByProgramId(this.data.program).then(result => {
      //   this.nomenclatures.trainings = result;
      // })
    },
    ISODateString: ISODateString
  }
}
</script>