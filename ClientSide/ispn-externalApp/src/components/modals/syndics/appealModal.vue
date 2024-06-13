<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc">Ново обжалване</template>
        <template v-else>Преглед на обжалване</template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12" xl="4" lg="6">
              <base-input
                label="Заповед номер"
                v-model="data.number"
                :rules="[rules.required]"
              />
            </v-col>
            <v-col cols="12" xl="4" lg="6">
              <base-material-date-picker
                label="Заповед дата"
                v-model="data.date"
                :rules="[rules.required]"
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" xl="4" lg="6">
              <base-input
                label="Жалба номер"
                v-model="data.number"
                :rules="[rules.required]"
              />
            </v-col>
            <v-col cols="12" xl="4" lg="6">
              <base-material-date-picker
                label="Жалба дата"
                v-model="data.date"
                :rules="[rules.required]"
              />
            </v-col>
            <v-col cols="12">
              <base-textarea
                label="Решение на ВАС"
                v-model="data.resolution"
              />
            </v-col>
            <v-col cols="12">
              <v-file-input
                label="Файл"
                v-model="data.file"
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

export default {
  name: "syndicAppealModal",
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {},
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
      // apiGetSyndicAppeal(this.data.id).then((result) => {
      //   this.$set(this, "data", result);
      //   this.open = true;
      // });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertSyndicAppeal(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateSyndicSignalAppeal(this.data).then((result) => {
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