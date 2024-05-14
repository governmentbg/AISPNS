<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <v-row class="mt-5 d-print-none">
      <v-col cols="12" lg="8" offset-lg="2" class="text-right">
        <v-btn
          class="py-5 mb-5"
          color="primary"
          @click="$router.push({path: `/cases/stabilization/${id}`})"
        >
          <v-icon left dark>mdi-gavel</v-icon>
          {{$t('go_to_case')}}
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="d-print-none mt-10">
      <v-col cols="12" lg="8" offset-lg="2" class="mx-auto">
        <base-case-card-data :data="data" />
      </v-col>

      <v-col cols="12">
        <base-material-card
          color="primary"
          icon="mdi-ticket"
          inline
          class="px-5 py-5 mt-5"
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">{{$t('acts_subject_to_appeal')}}</div>
          </template>

          <base-data-table 
            :headers="table.headers" 
            :hasPaging="false"
            :data="table.data" 
            @getData="getCaseActsData" 
            class="mb-5"
          />
        </base-material-card>
      </v-col>

    </v-row>
  </v-container>
</template>

<script>
import { ISODateString, isValidGUID } from '@/utils';
import { apiGetCaseById, apiGetCaseActsByCaseId } from '@/api/cases';

export default {
  name: "previewCaseActs",
  components: {
  },
  data(){
    return {
      id: null,
      data: {},
      table: {
        headers: [],
        data: [],
        sort: {},
        pagination: {
          page: 1,
          itemsPerPage: 0,
          perPageOptions: [5, 10, 20, 50, 100],
          total: 0,
        },
      }
    }
  },
  mounted(){
    if(!isValidGUID(this.$route.params.id)){
      this.$router.push('/cases/stabilization/by-case')
    } else {
      this.table.headers = [
        { text: this.$t('acts_subject_to_appeal'), value: 'actType', valueType: 'text', sortable: false },
        { text: this.$t('date_of_announcement_or_entry'), value: 'fieldActionDate', valueType: "date", sortable: false },
        { text: this.$t('appeal_period'), value: "actComplaintTerm", valueType: 'text', sortable: false },
        { text: this.$t('court_of_appeal'), value: "courtOfAppealName", valueType: 'text', sortable: false },
      ]
      this.id = this.$route.params.id;

      this.getData();
    }
  },
  computed: {
  },
  methods: {
    getData(){
      apiGetCaseById(this.id).then(result => {
        this.$set(this, 'data', result);
      
        this.getCaseActsData();
      })
    },
    getCaseActsData(){
      apiGetCaseActsByCaseId(this.id).then(result => {
        this.$set(this.table, 'data', result);
      })
    },
    ISODateString: ISODateString
  }
}
</script>