<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isMEIEmployee"
  >
    <base-v-component
      :heading="'Преглед на контактна информация'"
    />

    <v-row class="mt-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          color="primary"
          @click="save"
        >
          <v-icon left dark>mdi-check</v-icon>
          Запази
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="my-10">
      <v-col cols="12">
        <base-material-card 
          icon="mdi-book-open-blank-variant"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Контактна информация
          </template>

          <v-form
            ref="form"
            lazy-validation
            class="pa-3"
          >
            <v-row>
              <v-col cols="12">
                <base-editor
                  label="Съдържание на страница 'Контакти'"
                  v-model="data.rawHtml"
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

import { apiGetContactForm, apiCreateContactForm, apiUpdateContactForm } from "@/api/mii_contacts"
export default {
  name: "contactPreview",
  components: {
  },
  data(){
    return {
      data: {},
    }
  },
  mounted(){
    this.getData();
  },
  computed: {
    ...mapGetters([
      'isMEIEmployee',
      'currentUser'
    ]),
  },
  methods: {
    getData(){
      apiGetContactForm(this.data.id).then(result => {
        this.isNewDoc = true;
        if(result){
          this.$set(this, 'data', result);
          this.isNewDoc = false;
        }
        
      })
    },
    save(){
      if(this.$refs.form.validate()){
        if(this.isNewDoc){
          apiCreateContactForm(this.data).then(result => {
            if(result)
              this.getData();
          })
        } else {
          apiUpdateContactForm(this.data)
        }
      }
    },
    ISODateString: ISODateString
  }
}
</script>