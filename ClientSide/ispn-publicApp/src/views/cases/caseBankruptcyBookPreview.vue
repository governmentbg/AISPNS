<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <v-row class="mt-5 d-print-none">
      <v-col cols="12" lg="8" offset-lg="2" class="text-right mx-auto">
        <v-btn
          class="py-5 mb-5"
          color="primary"
          @click="$router.push({path: `/cases/bankruptcy/${id}`})"
        >
          <v-icon left dark>mdi-gavel</v-icon>
          {{ $t('go_to_case') }}
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="primary lighten-1"
          @click="$router.push({path: `/cases/bankruptcy/${id}/acts`})"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          {{ $t('acts_subject_to_appeal') }}
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="d-print-none mt-10">
      <v-col cols="12" lg="8" offset-lg="2" class="mx-auto">
        <base-case-card-data :data="data" />
      </v-col>

      <v-col cols="12" class="mt-5">
        <base-material-card 
          icon="mdi-book-open-variant"
          color="primary"
          inline
        >
          <template v-slot:after-heading>
            <div>{{$t('book_634_v')}}</div>
          </template>

          <v-container class="py-0">
            <v-simple-table class="bordered">
              <template v-slot:default>
                <thead>
                  <tr>
                    <th rowspan="2">№</th>
                    <th rowspan="2">{{$t('date_of_receipt')}}</th>
                    <th rowspan="2">{{$t('action_request_complaint_other')}}</th>
                    <th rowspan="2" class="text-center">{{$t('participant_in_the_proceedings')}}</th>
                    <th class="text-center">{{$t('judicial_act')}}</th>
                  </tr>
                  <tr>
                    <th class="text-center">{{ data?.court?.name }}</th>
                  </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, idx) of rows" :key="`row-${idx}`">
                      <td>{{ item.rowNumber }}</td>
                      <td>{{ ISODateString(item.date) }}</td>
                      <td>{{ item.actvity }}</td>
                      <td>{{ item.participantName }}</td>
                      <td class="text-center">
                        <template v-if="item.actvity">
                          <h6>{{ item.actvity }}</h6>
                          <v-divider />
                        </template>
                        
                        <span v-if="item.sessionResult">{{ item.sessionResult }}</span>
                        
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
import { ISODateString, isValidGUID } from '@/utils';
import { apiGetCaseById, apiGetCaseBook } from "@/api/cases";
export default {
  name: "previewCaseBook",
  components: {
  },
  data(){
    return {
      id: null,
      data: {},
      rows: [],
    }
  },
  mounted(){
    if(!isValidGUID(this.$route.params.id)){
      this.$router.push('/cases/bankruptcy/by-case')
    } else {
      this.id = this.$route.params.id;
      this.getCaseData();
    }
  },
  computed: {},
  methods: {
    getCaseBookData(){
      apiGetCaseBook(this.id).then(data => {
        this.rows = data;
      })
    },
    getCaseData(){
      apiGetCaseById(this.id).then(data => {
        this.data = data;
        this.getCaseBookData();
      })
    },
    ISODateString: ISODateString
  }
}
</script>