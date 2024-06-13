<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc">Добавяне на документ към сигнал</template>
        <template v-else>Преглед на документ към сигнал</template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12" lg="6">
              <base-autocomplete
                label="Вид документ"
                v-model="data.documentType"
                :items="nomenclatures.documentTypes"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="3">
              <base-material-date-picker
                label="Дата"
                v-model="data.date"
              />
            </v-col>
            <v-col cols="12">
              <base-input
                label="Описание"
                v-model="data.description"
              />
            </v-col>
            <v-col cols="12">
              <base-textarea
                label="Бележки"
                v-model="data.notes"
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

export default {
  name: "syndicSignalDocumentModal",
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {},
      nomenclatures: {
        paymentTypes: [],
        years: []
      },
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = {};
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
    getData(id) {
      // apiGetSyndicSignalDocument(this.data.id).then((result) => {
      //   this.$set(this, "data", result);
      //   this.open = true;
      // });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertSyndicSignalDocument(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateSyndicSignalDocument(this.data).then((result) => {
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