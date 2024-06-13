<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component />

    <v-row class="mt-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          class="py-5 mb-5"
          color="primary"
          @click="save"
        >
          <v-icon
            left
            dark
          >
            mdi-check
          </v-icon>
          Запази
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="d-print-none mt-10">
      <v-col cols="12" class="mx-auto">
        <base-material-card 
          icon="mdi-account-group"
          color="primary"
        >
          <template v-slot:after-heading>
            <div class="font-weight-light card-title mt-2">
              Настройка имена на служители
            </div>
          </template>

          <v-form
            ref="form"
            v-model="validForm"
            lazy-validation
          >
            <v-row>
              <v-col cols="12">
                <base-input
                  label="Министър на правосъдието"
                  :rules="[rules.required, rules.min]"
                  v-model="data.ministerName"
                />
              </v-col>
              <v-col cols="12" lg="6">
                <base-input
                  label="Инспектор описание"
                  v-model="data.chiefInspectorDescription"
                />
              </v-col>
              <v-col cols="12" lg="6">
                <base-input
                  label="Инспектор"
                  v-model="data.chiefInspector"
                />
              </v-col>
            </v-row>
          </v-form>
        </base-material-card>

        <base-material-card 
          icon="mdi-bank"
          color="primary"
          class="mt-15"
        >
          <template v-slot:after-heading>
            <div class="font-weight-light card-title mt-2">
              Настройка банкови данни
            </div>
          </template>

          <v-form
            ref="bankForm"
            v-model="validForm"
            lazy-validation
          >
            <v-row>
              <v-col cols="12">
                <base-input
                  label="Описание"
                  v-model="data.paymentInstruction"
                />
              </v-col>
              <v-col cols="12">
                <base-input
                  label="Банка"
                  v-model="data.bankName"
                />
              </v-col>
              <v-col cols="12">
                <base-input
                  label="BIC"
                  v-model="data.bic"
                />
              </v-col>
              <v-col cols="12">
                <base-input
                  label="IBAN"
                  v-model="data.iban"
                />
              </v-col>
            </v-row>
          </v-form>
        </base-material-card>

        <base-material-card 
          icon="mdi-clock-check-outline"
          color="primary"
          class="mt-15"
        >
          <template v-slot:after-heading>
            <div class="font-weight-light card-title mt-2">
              Срокове
            </div>
          </template>

          <v-form
            ref="termsForm"
            lazy-validation
          >
            <v-row>
              <v-col cols="12" xl="4" lg="6">
                <base-input
                  label="Известие на синдик за плащане на вноска (дни преди крайния срок)"
                  v-model.number="data.notificationDeadline"
                  type="number"
                  :rules="[rules.required]"
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
import { apiGetSettingsForm, apiCreateSettingForm, apiUpdateSettingForm } from '@/api/settings';
export default {
  name: "systemSettings",
  components: {
  },
  data(){
    return {
      nomenclatures: {},
      data: Object.assign({}, {}),
      isNewDoc: true,
      validForm: true,
      rules: {
        required: v => !!v || 'Полето е задължително.',
        requiredSelect: v => (typeof v === 'string' && v.length) || typeof v === 'boolean' || !isNaN(parseFloat(v)) && v >= 0 && v <= 999 || "Полето е задължително",
        min: v => (v && v.length >= 3) || 'Полето трябва да съдържа поне 3 символа',
      },
    }
  },
  mounted(){
    this.getData();
  },
  computed: {
    ...mapGetters([
      'isAdministrator'
    ]),
  },
  methods: {
    getData(){
      apiGetSettingsForm(this.data.id).then(result => {
        if(result){
          this.$set(this, "data", result);
          this.isNewDoc = false;
        } else {
          this.isNewDoc = true;
        }
      })
    },
    save(){
      if(this.$refs.form.validate() && this.$refs.form.validate()){
        if(this.isNewDoc){
          apiCreateSettingForm(this.data).then(result => {
            if(result && result.length)
              this.$router.push({path: `/admin/users/${result}`})
          })
        } else {
          apiUpdateSettingForm(this.data)
        }
      }
    },
    ISODateString: ISODateString
  }
}
</script>