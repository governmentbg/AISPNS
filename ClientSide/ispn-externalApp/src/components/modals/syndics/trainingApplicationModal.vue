<template>
  <v-dialog v-model="open" width="50%" scrollable>
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
                v-model="data.program"
                :items="nomenclatures.programs"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="6">
              <base-input
                label="E-mail"
                v-model="data.email"
              />
            </v-col>
            <v-col cols="12" lg="6">
              <base-input
                label="Телефон"
                v-model="data.phone"
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <h5>Основно обучение</h5>
            </v-col>
            <v-col cols="12">
              <base-autocomplete
                label="Курс"
                :items="nomenclatures.courses"
                v-model="data.mainTraining.course"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="3">
              <v-checkbox
                label="Хотелска резервация"
                v-model="data.mainTraining.hotelReservation"
                dense
              />
            </v-col>
            <v-col cols="12" lg="2">
              <v-checkbox
                label="Лектор"
                v-model="data.mainTraining.isLector"
                dense
              />
            </v-col>
            <v-col cols="12" lg="3">
              <base-material-date-picker
                label="От дата"
                v-model="data.mainTraining.fromDate"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="3">
              <base-material-date-picker
                label="До дата"
                v-model="data.mainTraining.toDate"
                :rules=[rules.required]
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <h5>Алтернативно обучение</h5>
            </v-col>
            <v-col cols="12">
              <base-autocomplete
                label="Курс"
                :items="nomenclatures.courses"
                v-model="data.alternateTraining.course"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="3">
              <v-checkbox
                label="Хотелска резервация"
                v-model="data.alternateTraining.hotelReservation"
                dense
              />
            </v-col>
            <v-col cols="12" lg="2">
              <v-checkbox
                label="Лектор"
                v-model="data.alternateTraining.isLector"
                dense
              />
            </v-col>
            <v-col cols="12" lg="3">
              <base-material-date-picker
                label="От дата"
                v-model="data.alternateTraining.fromDate"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="3">
              <base-material-date-picker
                label="До дата"
                v-model="data.alternateTraining.toDate"
                :rules=[rules.required]
              />
            </v-col>
          </v-row>
        </v-form>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="save"> Запази </v-btn>
        <v-btn color="secondary" @click="save(true)"> Запази и затвори </v-btn>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>

import SyndicTrainingModel from "@/models/syndics/SyndicTrainingModel";

export default {
  name: "syndicTrainingModal",
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: Object.assign({}, new SyndicTrainingModel()),
      nomenclatures: {
        programs: [],
        courses: []
      },
      rules: {
        required: (v) => !!v || "Полето е задължително",
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
      // apiGetSyndicTrainingById(this.data.id).then((result) => {
      //   this.$set(this, "data", result);
      //   this.open = true;
      // });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertSyndicTraining(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateSyndicTraining(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        }
    },
    openModal(id = null) {
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
    }
  },
};
</script>

<style></style>
