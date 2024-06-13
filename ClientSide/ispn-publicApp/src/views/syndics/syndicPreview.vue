<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <v-row class="my-5">
      <v-col cols="12" lg="8" offset-lg="2" class="mx-auto">
        <base-material-card 
          icon="mdi-account-tie"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            {{ $t('syndic') }}
          </template>


          <v-container class="py-3">
            <v-simple-table class="table-presentation">
              <template v-slot:default>
                <tbody>
                  <tr>
                    <td width="300">{{ $t('name') }}:</td>
                    <td>{{ getSyndicName }}</td>
                  </tr>
                  <tr>
                    <td>{{ $t('address') }}:</td>
                    <td>
                      {{ data.fullAddress }}
                    </td>
                  </tr>
                  <tr>
                    <td>{{ $t('phone') }}:</td>
                    <td>{{ data.phone }}</td>
                  </tr>
                  <tr>
                    <td>{{ $t('email') }}:</td>
                    <td>{{ data.email }}</td>
                  </tr>
                  <tr>
                    <td>{{ $t('speciality') }}:</td>
                    <td>{{ data.syndicSpecialty }}</td>
                  </tr>
                  <tr>
                    <td>{{ $t('confirmed_by_order_after_an_exam_to_acquire_the_qualification') }}:</td>
                    <td v-html="getOrder">
                    </td>
                  </tr>
                </tbody>
              </template>
            </v-simple-table>
          </v-container>
        </base-material-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, isValidGUID } from '@/utils';


import { apiGetSyndicById } from "@/api/syndics";

export default {
  name: "syndicPreview",
  components: {
  },
  data(){
    return {
      nomenclatures: {
        statuses: [],
        specialities: [],
        districts: [],
        municipalities: [],
        settlements: []
      },
      data: {},
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
    if(!isValidGUID(this.$route.params.id)){
      this.$router.push('/syndics');
    } else {
      this.data.id = this.$route.params.id;

      this.getData();
    }
  },
  computed: {
    getSyndicName(){
      return `${this.data.fullName}`;
    },
    getOrder(){
      let result = '';
      if(this.data?.orderNumber && this.data?.orderDate)
        result += this.data.orderNumber + ' от ' + ISODateString(this.data.orderDate);
      
      if(this.data?.stateGazetteEdition && this.data?.stateGazetteYear)  
        result += ' г. <br />държавен вестник бр. ' +this.data.stateGazetteEdition + ' от ' + this.data.stateGazetteYear + ' г.';

      return result;
    }
  },
  methods: {
    getData(){
      this.data.id && this.data.id.length === 36 &&
      apiGetSyndicById(this.data.id).then(result => {
        this.$set(this, "data", Object.assign({}, result));
      })
    },
    ISODateString: ISODateString
  }
}
</script>