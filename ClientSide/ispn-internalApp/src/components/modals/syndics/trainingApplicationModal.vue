<template>
  <v-dialog v-model="open" width="70%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc">Добавяне на заявка за обучение</template>
        <template v-else>Преглед на заявка за обучение</template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12">
              <base-autocomplete
                label="Програма"
                v-model="data.programId"
                :items="nomenclatures.programs"
                :rules=[rules.required]
                item-value="id"
                @change="getProgramCourses"
                :readonly="isReadonly"
              >
                <template v-slot:selection="{ item }">
                  {{ `${item.description} (${ISODateString(item.fromDate)} - ${ISODateString(item.toDate)})` }}
                </template>
                <template v-slot:item="{ item }">
                  <v-list-item-content>
                    <v-list-item-title>
                      <strong>{{ `${item.description} (${ISODateString(item.fromDate)} - ${ISODateString(item.toDate)})` }}</strong>
                    </v-list-item-title>
                    <v-list-item-subtitle>
                    </v-list-item-subtitle>
                  </v-list-item-content>
                </template>
              </base-autocomplete>
            </v-col>
            <v-col cols="12" lg="6">
              <base-input
                label="E-mail"
                v-model="data.actualEmail"
                :readonly="isReadonly"
              />
            </v-col>
            <v-col cols="12" lg="6">
              <base-input
                label="Телефон"
                v-model="data.actualPhone"
                :readonly="isReadonly"
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <h5>Основно обучение</h5>
            </v-col>
            <v-col cols="12">
              <base-autocomplete
                label="Обучение"
                :items="nomenclatures.courses"
                v-model="data.courseId"
                :rules=[rules.required]
                item-value="id"
                @change="setCourseNomDates"
                :readonly="isReadonly"
              >
                <template v-slot:selection="{ item }">
                  {{ `${item.topic} (Дата 1: ${ISODateString(item.fromDate1)} - ${ISODateString(item.toDate1)} || Дата 2: ${ISODateString(item.fromDate2)} - ${ISODateString(item.toDate2)})` }}
                </template>
                <template v-slot:item="{ item }">
                  <v-list-item-content>
                    <v-list-item-title>
                      <strong>{{ `${item.topic} (Дата 1: ${ISODateString(item.fromDate1)} - ${ISODateString(item.toDate1)} || Дата 2: ${ISODateString(item.fromDate2)} - ${ISODateString(item.toDate2)})` }}</strong>
                    </v-list-item-title>
                    <v-list-item-subtitle>
                      {{ `${item.subTopics}` }}
                    </v-list-item-subtitle>
                  </v-list-item-content>
                </template>
              </base-autocomplete>
            </v-col>
            <v-col cols="12" lg="3">
              <v-checkbox
                label="Хотелска резервация"
                v-model="data.hotelReservationDate1"
                dense
                :readonly="isReadonly"
              />
            </v-col>
            <v-col cols="12" lg="2">
              <v-checkbox
                label="Лектор"
                v-model="data.lecturerDate1"
                dense
                :readonly="isReadonly"
              />
            </v-col>
            <v-col cols="12" lg="6">
              <base-autocomplete
                label="Избор на дата"
                :items="nomenclatures.courseDates"
                v-model="data.courseDate1"
                :rules=[rules.trueOrFalse]
                item-text="label"
                item-value="value"
                @change="setCourseDates"
                :readonly="isReadonly"
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <h5>Алтернативно обучение</h5>
            </v-col>
            <v-col cols="12">
              <base-autocomplete
                label="Обучение"
                :items="nomenclatures.courses"
                v-model="data.alternateCourseId"
                :rules=[rules.required]
                item-value="id"
                @change="setAlternateCourseNomDates"
                :readonly="isReadonly"
              >
                <template v-slot:selection="{ item }">
                  {{ `${item.topic} (Дата 1: ${ISODateString(item.fromDate1)} - ${ISODateString(item.toDate1)} || Дата 2: ${ISODateString(item.fromDate2)} - ${ISODateString(item.toDate2)})` }}
                </template>
                <template v-slot:item="{ item }">
                  <v-list-item-content>
                    <v-list-item-title>
                      <strong>{{ `${item.topic} (Дата 1: ${ISODateString(item.fromDate1)} - ${ISODateString(item.toDate1)} || Дата 2: ${ISODateString(item.fromDate2)} - ${ISODateString(item.toDate2)})` }}</strong>
                    </v-list-item-title>
                    <v-list-item-subtitle>
                      {{ `${item.subTopics}` }}
                    </v-list-item-subtitle>
                  </v-list-item-content>
                </template>
              </base-autocomplete>
            </v-col>
            <v-col cols="12" lg="3">
              <v-checkbox
                label="Хотелска резервация"
                v-model="data.hotelReservationDate2"
                dense
                :readonly="isReadonly"
              />
            </v-col>
            <v-col cols="12" lg="2">
              <v-checkbox
                label="Лектор"
                v-model="data.lecturerDate2"
                dense
                :readonly="isReadonly"
              />
            </v-col>
            <v-col cols="12" lg="6">
              <base-autocomplete
                label="Избор на дата"
                :items="nomenclatures.alternateCourseDates"
                v-model="data.alternateCourseDate2"
                :rules=[rules.trueOrFalseAlternateDate2]
                item-text="label"
                item-value="value"
                @change="setAlternateCourseDates"
                :readonly="isReadonly"
              />
            </v-col>
          </v-row>
        </v-form>
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
</template>

<script>

import SyndicTrainingModel from "@/models/syndics/SyndicTrainingModel";
import { apiCreateSyndicCourseApplication, apiGetSyndicApplicationById, apiUpdateSyndicCourseApplication } from "@/api/syndics";
import { apiGetProgramCourses, apiGetPrograms } from "@/api/programs";
import { ISODateString } from "@/utils";
export default {
  name: "syndicTrainingModal",
  props: {
    syndicData: {
      type: Object,
      default: () => null,
    },
    isReadonly: {
      type: Boolean,
      default: false,
    },
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: Object.assign({}, new SyndicTrainingModel()),
      nomenclatures: {
        programs: [],
        courses: [],
        courseDates: [],
        alternateCourseDates: []
      },
      rules: {
        required: (v) => !!v || "Полето е задължително",
        trueOrFalse: (v) => v === true || v === false || "Полето е задължително",
        trueOrFalseAlternateDate2: (v) => this.data.alternateCourseId && (v === true || v === false) || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = Object.assign({}, new SyndicTrainingModel());
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
      apiGetSyndicApplicationById(this.data.id).then((result) => {
        this.$set(this, "data", result);

        if(!this.data.alternateCourseId)
          this.data.alternateCourseDate2 = null;

        if(this.data.programId)
          this.getProgramCourses(true);

        this.isNewDoc = false;
        this.open = true;
      });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          let data = Object.assign({}, this.data);
          data.syndicId = this.syndicData.id;
          apiCreateSyndicCourseApplication(data).then((result) => {
            if (result && result.length && result !== '00000000-0000-0000-0000-000000000000') {
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
          apiUpdateSyndicCourseApplication(this.data).then((result) => {
            if (result && result.id) {
              this.$emit("update");
              if(closeModal)
                this.closeModal();
            }
          });
        }
    },
    getPrograms(){
      apiGetPrograms({pageSize: 9999, pageNumber: 1}).then(result => {
        this.$set(this.nomenclatures, "programs", result.items);
      });
    },
    getProgramCourses(generateNomenclatures = false){
      if(this.data.programId){
        let query = {
          pageSize: 9999,
          pageNumber: 1,
          id: this.data.programId
        }
        apiGetProgramCourses(query).then(result => {
          this.$set(this.nomenclatures, "courses", result.items);

          if(generateNomenclatures)
            this.generateCousesNomDates();
        });
      }
    },
    generateCousesNomDates(){
      let courseData = this.nomenclatures.courses.find(item => item.id === this.data.courseId);
      this.nomenclatures.courseDates = [
        { label: `Дата 1: ${ISODateString(courseData.fromDate1)} - ${ISODateString(courseData.toDate1)}`, value: true },
        { label: `Дата 2: ${ISODateString(courseData.fromDate2)} - ${ISODateString(courseData.toDate2)}`, value: false }
      ]

      let alternateCourseData = this.nomenclatures.courses.find(item => item.id === this.data.alternateCourseId);
      if(alternateCourseData)
        this.nomenclatures.alternateCourseDates = [
          { label: `Дата 1: ${ISODateString(alternateCourseData.fromDate1)} - ${ISODateString(alternateCourseData.toDate1)}`, value: true },
          { label: `Дата 2: ${ISODateString(alternateCourseData.fromDate2)} - ${ISODateString(alternateCourseData.toDate2)}`, value: false }
        ]
    },
    setCourseNomDates(courseId){
      let courseData = this.nomenclatures.courses.find(item => item.id === courseId);
      this.$set(this.nomenclatures, 'courseDates', [
        { label: `Дата 1: ${ISODateString(courseData.fromDate1)} - ${ISODateString(courseData.toDate1)}`, value: true },
        { label: `Дата 2: ${ISODateString(courseData.fromDate2)} - ${ISODateString(courseData.toDate2)}`, value: false }
      ])
    },
    setCourseDates(chosenDate){
      let courseData = this.nomenclatures.courses.find(item => item.id === this.data.courseId);
      if(chosenDate){
        this.data.fromDate1 = courseData.fromDate1;
        this.data.toDate1 = courseData.toDate1;
      } else {
        this.data.fromDate1 = courseData.fromDate2;
        this.data.toDate1 = courseData.toDate2;
      }
    },
    setAlternateCourseNomDates(courseId){
      let alternateCourseData = this.nomenclatures.courses.find(item => item.id === courseId);
      this.$set(this.nomenclatures, 'alternateCourseDates', [
        { label: `Дата 1: ${ISODateString(alternateCourseData.fromDate1)} - ${ISODateString(alternateCourseData.toDate1)}`, value: true },
        { label: `Дата 2: ${ISODateString(alternateCourseData.fromDate2)} - ${ISODateString(alternateCourseData.toDate2)}`, value: false }
      ])
    },
    setAlternateCourseDates(chosenDate){
      let alternateCourseData = this.nomenclatures.courses.find(item => item.id === this.data.alternateCourseId);
      if(chosenDate){
        this.data.fromDate2 = alternateCourseData.fromDate1;
        this.data.toDate2 = alternateCourseData.toDate1;
      } else {
        this.data.fromDate2 = alternateCourseData.fromDate2;
        this.data.toDate2 = alternateCourseData.toDate2;
      }
    },
    openModal(id = null) {
      this.$set(this, 'data', Object.assign({}, new SyndicTrainingModel()));
      this.$set(this.nomenclatures, 'courseDates', []);

      if(!this.nomenclatures.programs.length)
        this.getPrograms();


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
    },
    ISODateString
  },
};
</script>

<style></style>
