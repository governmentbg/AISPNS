<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc">Отбелязване на плащане</template>
        <template v-else>Преглед на плащане</template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12" lg="10">
              <base-autocomplete
                label="Вид плащане"
                v-model="data.type"
                :items="nomenclatures.paymentTypes"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="2">
              <base-select
                label="За година"
                v-model="data.year"
                :items="nomenclatures.years"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="5">
              <base-material-date-picker
                label="Дата на плащане/на платежен документ"
                v-model="data.paymentDate"
              />
            </v-col>
            <v-col cols="12" lg="7">
              <base-input
                label="Банка"
                v-model="data.bank"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="4">
              <base-input
                label="Сума"
                v-model="data.amount"
                type="number"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="3">
              <v-checkbox
                label="Проверено плащане"
                dense
                v-model="data.approvedPayment"
              />
            </v-col>
            <v-col cols="12" lg="5">
              <base-input
                label="Проверил плащането"
                v-model="data.approvedPaymentBy"
                readonly
              />
            </v-col>
            <v-col cols="12" v-if="isNewDoc">
              <v-file-input
                label="Платежен документ"
                v-model="data.paymentFile"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" v-if="isNewDoc">
              <v-file-input
                label="Образец/молба за заплащане"
                v-model="data.paymentTemplateFile"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12">
              <v-checkbox
                label="Заключен профил"
                dense
                v-model="data.lockedAccount"
              />
            </v-col>
            <v-col cols="12">
              <base-textarea
                label="Описание"
                v-model="data.description"
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
  name: "syndicPaymentModal",
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
      // apiGetSyndicPaymentById(this.data.id).then((result) => {
      //   this.$set(this, "data", result);
      //   this.open = true;
      // });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertSyndicPayment(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateSyndicPayment(this.data).then((result) => {
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
