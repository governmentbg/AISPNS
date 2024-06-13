<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc"> Нова заповед </template>
        <template v-else> Преглед на заповед </template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12">
              <v-checkbox
                v-model="data.isExclusion"
                label="Заповед за отстраняване/изключване"
                :true-value="true"
                :false-value="false"
                dense
              />
            </v-col>
            <template v-if="!data.isExclusion">
              <v-col cols="12">
                <base-autocomplete
                  label="Вид заповед"
                  v-model="data.type"
                  :items="nomenclatures.orderTypes"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" lg="4">
                <base-input
                  label="Номер"
                  v-model="data.number"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" lg="4">
                <base-input
                  label="Държавен вестник брой"
                  v-model="data.dvBroi"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" lg="4">
                <base-input
                  label="Държавен вестник година"
                  v-model="data.dvYear"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12">
                <base-input
                  label="Описание"
                  v-model="data.description"
                />
              </v-col>
              <v-col cols="12" v-if="isNewDoc">
                <v-file-input
                  label="Прикачи файл"
                  v-model="data.file"
                  :rules=[rules.required]
                  dense
                />
              </v-col>
            </template>

            <template v-else>
              <v-col cols="12">
                <base-material-date-picker
                  label="Дата на изключване"
                  v-model="data.exclusionDate"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12">
                <base-input
                  label="Основание"
                  v-model="data.reason"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" lg="6">
                <base-material-date-picker
                  label="Дата на временно отстраняване"
                  v-model="data.date"
                />
              </v-col>
              <v-col cols="12" lg="6">
                <base-material-date-picker
                  label="Срок на временно отстраняване"
                  v-model="data.termDate"
                />
              </v-col>
              <v-col cols="12">
                <base-input
                  label="Заповед"
                  v-model="data.order"
                  :rules=[rules.required]
                />
              </v-col>
              <v-col cols="12" v-if="isNewDoc">
                <v-file-input
                  label="Прикачи файл"
                  v-model="data.file"
                  :rules=[rules.required]
                />
              </v-col>
            </template>
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
  name: "syndicOrderModal",
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {},
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

<style></style>
