<template>
  <v-container
    id="previewUser"
    fluid
    tag="section"
    class="full-height"
    v-if="isAdministrator"
  >
    <base-v-component
      :heading="this.isNewDoc ? 'Добавяне на потребител' : 'Преглед на потребител'"
      goBackText="Обратно към потребители"
      goBackTo="/settings/users"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4">
          Създаден на: <strong> {{ ISODateString(data.dateCreated) }} </strong> <br />
          Видим в системата: <strong> {{ data.person.isActive ? "ДА" : "НЕ" }} </strong> <br />
        </v-sheet>
      </v-col>
      <v-col cols="12" lg="7" xl="8" class="text-right">
        <v-btn
          class="py-5 mb-5"
          color="secondary"
          @click="setPersonVisible"
          v-if="!isNewDoc && !data.person.isActive"
        >
          <v-icon left dark>mdi-eye</v-icon>
          Направи видим
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="secondary"
          @click="setPersonNotVisible"
          v-if="!isNewDoc && data.person.isActive"
        >
          <v-icon left dark>mdi-eye-off</v-icon>
          Направи невидим
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="primary"
          @click="saveData"
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
          icon="mdi-account"
          color="primary"
        >
          <template v-slot:after-heading>
            <div class="font-weight-light card-title mt-2">
              Потребител
            </div>
          </template>

          <v-container class="py-7">
            <v-form
              ref="form"
              v-model="validForm"
              lazy-validation
            >
              <v-row>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <v-text-field
                    label="Име"
                    :rules="[rules.required, rules.min]"
                    v-model="data.person.firstName"
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <v-text-field
                    label="Презиме"
                    :rules="[rules.required, rules.min]"
                    v-model="data.person.middleName"
                  ></v-text-field>
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <v-text-field
                    label="Фамилия"
                    :rules="[rules.required, rules.min]"
                    v-model="data.person.lastName"
                  ></v-text-field>
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <v-text-field
                    label="ЕГН"
                    v-model="data.person.egn"
                  ></v-text-field>
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <v-text-field
                    label="E-mail"
                    :rules="[rules.required, rules.email]"
                    v-model="data.person.email"
                  ></v-text-field>
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <v-text-field
                    label="Потребителско име"
                    :rules="[rules.required]"
                    v-model="data.userName"
                  ></v-text-field>
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-select
                    :items="nomenclatures.statuses"
                    item-text="label"
                    item-value="value"
                    v-model="data.isActive"
                    :rules="[rules.requiredSelect]"
                    label="Статус"
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-select
                    :items="nomenclatures.roles"
                    v-model="data.roles"
                    item-text="name"
                    item-value="id"
                    multiple
                    :rules="[rules.requiredSelectReturnObjectMultiple]"
                    label="Права"
                    return-object
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
// import { apiCreateUser, apiGetUserById, apiUpdateUser } from '@/api/task-shreder/account';
// import { apiGetUserRoles, apiSetPersonIsActive } from '@/api/administrative-services/index'
import UserModel from '@/models/userModel';
export default {
  name: "previewUser",
  components: {
  },
  data(){
    return {
      nomenclatures: {
        statuses: [
          {label: "Активен", value: true},
          {label: "Неактивен", value: false}
        ],
        roles: []
      },
      data: Object.assign({}, new UserModel()),
      isNewDoc: true,
      validForm: true,
      rules: {
        required: v => !!v || 'Полето е задължително.',
        requiredSelect: v => (typeof v === 'string' && v.length) || typeof v === 'boolean' || !isNaN(parseFloat(v)) && v >= 0 && v <= 999 || "Полето е задължително",
        requiredSelectReturnObjectMultiple: v => v.length > 0 || "Полето е задължително",
        min: v => (v && v.length >= 3) || 'Полето трябва да съдържа поне 3 символа',
        email: v => !v || /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) || 'E-mail-a трябва да бъде валиден адрес.'
      },
    }
  },
  mounted(){
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
    }

    this.getUserRoles()

    this.getData();
  },
  computed: {
    ...mapGetters([
      'isAdministrator'
    ]),
  },
  methods: {
    getData(){
      // if(!this.isNewDoc)
      //   apiGetUserById(this.data.id).then(data => {
      //     this.data = data;
      //   })
    },
    getUserRoles(){
      // apiGetUserRoles().then(result => {
      //   this.nomenclatures.roles = result;
      // })
    },
    saveData(){
      if(this.$refs.form.validate()){
        if(this.isNewDoc){
          // apiCreateUser(this.data).then(result => {
          //   if(result && result.length)
          //     this.$router.push({path: `/settings/users/${result}`})
          // })
        } else {
          //apiUpdateUser(this.data)
        }
      }
    },
    setPersonVisible(){
      // apiSetPersonIsActive(this.data.person.id, true).then(result => {
      //   if(result) this.getData();
      // })
    },
    setPersonNotVisible(){
      // apiSetPersonIsActive(this.data.person.id, false).then(result => {
      //   if(result) this.getData();
      // })
    },
    ISODateString: ISODateString
  }
}
</script>