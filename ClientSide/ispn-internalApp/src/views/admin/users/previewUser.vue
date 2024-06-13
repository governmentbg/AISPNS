<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      :heading="this.isNewDoc ? 'Добавяне на потребител' : 'Преглед на потребител'"
      goBackText="Обратно към потребители"
      goBackTo="/admin/users"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4" v-if="false">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4">
          Създаден на: <strong> {{ ISODateString(data.dateCreated) }} </strong> <br />
        </v-sheet>
      </v-col>
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
          icon="mdi-account"
          color="primary"
        >
          <template v-slot:after-heading>
            <div class="font-weight-light card-title mt-2">
              Потребител
            </div>
          </template>

          <template v-slot:after-heading-button>
            <v-row class="px-3">
              <v-col cols="12">
                <v-checkbox
                  v-model="data.isActive"
                  label="Активен"
                  color="primary"
                  :true-value="true"
                  :false-value="false"
                  dense
                  hide-details
                  class="d-inline-block"
                />
              </v-col>
            </v-row>
          </template>

          <v-form
            ref="form"
            v-model="validForm"
            lazy-validation
            class="px-3 pt-3"
          >
            <v-row>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Име"
                  :rules="[rules.required, rules.min]"
                  v-model="data.firstName"
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Презиме"
                  v-model="data.secondName"
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Фамилия"
                  :rules="[rules.required, rules.min]"
                  v-model="data.lastName"
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="ЕГН"
                  v-model="data.egn"
                  :rules="isEGNRequired ? [rules.required] : []"
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="E-mail"
                  :rules="[rules.required, rules.email]"
                  v-model="data.email"
                />
              </v-col>
              <v-col cols="12" xl="3" lg="4" md="6">
                <base-input
                  label="Потребителско име"
                  v-model="data.userName"
                  :rules="isUserNameRequired ? [rules.required] : []"
                />
              </v-col>
              <v-col cols="12" xl="6">
                <base-select
                  :items="nomenclatures.roles"
                  v-model="data.roleIds"
                  item-text="translatedName"
                  item-value="id"
                  multiple
                  :rules="[rules.requiredSelectReturnObjectMultiple]"
                  label="Права"
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
import { apiCreateUser, apiGetUserById, apiUpdateUser } from '@/api/account';
import { apiMetaDataGetUserRoles } from '@/api/metaData'
import UserModel from '@/models/userModel';
import { eUserRoleNames, eUserRoleGUIDS } from '@/utils/enums/enumerators';
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
    isEGNRequired(){
      return this.data.roleIds.some(id => [eUserRoleGUIDS.Syndic, eUserRoleGUIDS.MEIEmployee].includes(id))
    },
    isUserNameRequired(){
      return this.data.roleIds.some(id => [eUserRoleGUIDS.Employee, eUserRoleGUIDS.Inspector, eUserRoleGUIDS.Administrator].includes(id))
    }
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetUserById(this.data.id).then(result => {
          if(result){
            this.$set(this, "data", result);
            this.isNewDoc = false;
          }
        })
    },
    getUserRoles(){
      apiMetaDataGetUserRoles().then(result => {
        this.$set(this.nomenclatures, 'roles', result.map(role => {
          role.translatedName = eUserRoleNames[role.name];
          return role;
        }));
      })
    },
    save(){
      if(this.$refs.form.validate()){
        if(this.isNewDoc){
          apiCreateUser(this.data).then(result => {
            if(result && result.length)
              this.$router.push({path: `/admin/users/${result}`})
          })
        } else {
          apiUpdateUser(this.data)
        }
      }
    },
    ISODateString: ISODateString
  }
}
</script>