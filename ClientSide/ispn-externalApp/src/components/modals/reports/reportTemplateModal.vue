<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc"> Нов образец </template>
        <template v-else> Преглед на образец </template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12" lg="8">
              <base-autocomplete
                label="Вид"
                v-model="data.type"
                :items="nomenclatures.types"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="4">
              <base-material-date-picker
                label="Дата"
                v-model="data.date"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12">
              <v-textarea
                label="Описание"
                v-model="data.description"
                rows="2"
                auto-grow
              />
            </v-col>
            <v-col cols="12">
              <v-file-input
                label="Образец"
                v-model="data.template"
                :rules=[rules.required]
                dense
              />
            </v-col>
            <v-col cols="12">
              <v-file-input
                label="Документ"
                v-model="data.file"
                dense
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
  name: "reportTemplateModal",
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {
        syndicName: "Иван Иванов"
      },
      nomenclatures: {
        orderTypes: []
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
    getData() {
      // apiGetSyndicOrderById(this.data.id).then((result) => {
      //   this.$set(this, "data", result);
      //   this.open = true;
      // });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertSyndicOrder(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateSyndicOrder(this.data).then((result) => {
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
