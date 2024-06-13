<template>
  <v-dialog v-model="open" width="70%" scrollable>
    <v-card>
      <v-card-title class="headline">
        Търсене на синдик
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row class="my-2" @keypress.enter="getData">
            <v-col cols="12" md="4" offset-md="2">
              <base-input
                label="ЕГН"
                v-model="egn"
                :rules="[rules.required, rules.min]"
              />
            </v-col>
            <v-col cols="12" md="4">
              <v-btn
                color="primary"
                @click="getData"
                block
              >
                Търси
              </v-btn>
            </v-col>
          </v-row>
        </v-form>

        <base-material-card
          color="primary"
          icon="mdi-account-star"
          inline
          class="px-5 py-5 mt-10"
          elevation="10"
          v-if="foundResult === true && data.id"
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">Синдик</div>
          </template>

          <v-row class="mt-3">
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-input
                label="Име"
                v-model="data.firstName"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-input
                label="Презиме"
                v-model="data.secondName"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-input
                label="Фамилия"
                v-model="data.lastName"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-input
                label="Втора фамилия"
                v-model="data.secondLastName"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-input
                label="ЕГН"
                v-model="data.egn"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-input
                label="E-mail"
                v-model="data.email"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-input
                label="Телефон"
                v-model="data.phone"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="6">
              <base-input
                label="Специалност"
                v-model="data.syndicSpecialty"
                readonly
              />
            </v-col>
            <v-col cols="12">
              <v-textarea
                label="Адрес"
                v-model="data.fullAddress"
                rows="2"
                auto-grow
                readonly
              />
            </v-col>
            <v-col cols="12" lg="6" offset-lg="3" v-if="data.isCustodian" class="text-center error--text">
              Лицето е доверено лице
            </v-col>
            <v-col cols="12" lg="6" offset-lg="3" >
              <v-btn
                class="secondary"
                @click="createCustodian"
                block
                :disabled="data.isCustodian"
              >
                Направи доверено лице
              </v-btn>
            </v-col>
          </v-row>
        </base-material-card>

        <v-row v-if="foundResult === false" class="mt-3">
          <v-col cols="12" lg="8" offset-lg="2">
            <v-alert
              type="error"
              prominent
              icon="mdi-alert"
              color="primary"
            >
            <v-row align="center">
              <v-col class="grow fs18 fw500">
                Няма намерен синдик с ЕГН: {{ searchEGN }}
              </v-col>
              <v-col class="shrink">
                <v-btn @click="$router.push({path: '/trusted-persons/create'})">
                  <v-icon left>mdi-plus</v-icon>
                  Създайте доверено лице
                </v-btn>
              </v-col>
            </v-row>
              
            </v-alert>
          </v-col>
        </v-row>

      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script>

import { apiSearchSyndics, apiCreateCustodian } from "@/api/syndics";

export default {
  name: "searchTrustedPersonByEGNModal",
  props: {
    parentData: {
      type: Object,
      default: () => {},
    },
  },
  mounted() {},
  data() {
    return {
      open: false,
      egn: null,
      searchEGN: null,
      data: {},
      foundResult: null,
      rules: {
        required: (v) => !!v || "Полето е задължително",
        min: (v) => (v && v.length === 10) || "Моля въведете десет цифрено ЕГН",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen){
        this.egn = null;
        this.searchEGN = null;
        this.foundResult = null;
        this.data = {};
      }
    },
  },
  computed: {
    
  },
  methods: {
    getData(e){
      if(e && e.key === 'Enter'){
        e.preventDefault();
        e.stopPropagation();
      }

      if(this.$refs.form.validate()){
        let query = Object.assign({}, {});
        
        query.filter = {
          egn: this.egn
        };
        query.pageNumber = 1;
        query.pageSize = 1;
        
        this.searchEGN = this.egn;
        apiSearchSyndics(query).then(result => {
          if(result.items.length){
            this.foundResult = true;
            this.data = result.items[0];
          } else {
            this.foundResult = false;
          }
        })
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

      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    closeModal(){
      this.open = false;
    },
    createCustodian(){
      apiCreateCustodian(this.data.id).then(result => {
        if(result)
          this.$router.push({ path: `/trusted-persons/${result}` })
      })
    }
  },
};
</script>
