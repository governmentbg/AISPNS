<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <v-row class="mt-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          class="py-5 mb-5"
          color="primary"
          @click="$router.push({path: `/entrepreneurs/stabilization/${data.id}`})"
        >
          <v-icon left dark>mdi-gavel</v-icon>
          {{ $t('go_to_case') }}
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="primary lighten-1"
          @click="$router.push({path: `/entrepreneurs/stabilization/${data.id}/acts`})"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          {{ $t('acts_subject_to_appeal') }}
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="primary lighten-1"
          @click="$router.push({path: `/entrepreneurs/stabilization/${data.id}/proceedings-data`})"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          {{ $t('stabilization_proceedings_data') }}
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="d-print-none mt-10">
      <v-col cols="12" lg="8" offset-lg="2" class="mx-auto">
        <base-material-card 
          icon="mdi-gavel"
          color="primary"
          inline
        >
          <template v-slot:after-heading>
            <div>{{ $t('_case')}}</div>
            <small>{{ $t('in_stabilization_proceedings') }}</small>
          </template>

          <v-container class="py-7">
            <v-simple-table class="table-presentation">
              <template v-slot:default>
                <tbody>
                  <tr>
                    <td>{{$t('court')}}:</td>
                    <td>Окръжен съд – Благоевград</td>
                  </tr>
                  <tr>
                    <td>{{$t('number')}}:</td>
                    <td>189/2023</td>
                  </tr>
                  <tr>
                    <td>{{$t('date')}}:</td>
                    <td>2023-10-18</td>
                  </tr>
                  <tr>
                    <td>{{$t('entrepreneur')}}:</td>
                    <td>Иван Иванов</td>
                  </tr>
                </tbody>
              </template>
            </v-simple-table>
          </v-container>
        </base-material-card>
      </v-col>

      <v-col cols="12">
        <base-material-card
          color="primary"
          icon="mdi-ticket"
          inline
          class="px-5 py-5 mt-5"
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">{{$t('declared_acts_of_bankruptcy')}}</div>
          </template>

          
          <base-data-table 
            :headers="table.headers" 
            :pagination="table.pagination" 
            :data="table.data" 
            @getData="getCaseActsData" 
          />
        </base-material-card>
      </v-col>

    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString } from '@/utils';
export default {
  name: "previewEntrepreneurDeclaredActsOfBankruptcy",
  components: {
  },
  data(){
    return {
      data: {},
      table: {
        headers: [],
        data: [],
        sort: {},
        pagination: {
          page: 1,
          skip: 0,
          take: 20,
          perPageOptions: [5, 10, 20, 50, 100],
          total: 0,
        },
      }
    }
  },
  mounted(){
    this.table.headers = [
      { text: this.$t('act_category'), value: "act", valueType: 'text', sortable: false },
      { text: this.$t('act_description'), valueType: 'date', value: "date", sortable: false },
      { text: this.$t('act_announcement_date'), value: "deadline", valueType: 'text', sortable: false },
      { text: this.$t('file'), value: "court", valueType: 'text', sortable: false },
    ]
    this.data.id = this.$route.params.id;

    this.getData();
  },
  computed: {
    ...mapGetters([
      'isAdministrator'
    ]),
  },
  methods: {
    getData(){
      // apiGetCaseById(this.data.id).then(data => {
      //   this.data = data;
      
      //   this.getCaseActsData();
      // })
    },
    getCaseActsData(){
      // apiGetCaseActsByCaseId(this.data.id).then(data => {
      //   this.table.data = result.items;
      //   this.table.pagination.total = result.totalCount;
      //   this.table.pagination.page = result.currentPage;
      // })
    },
    ISODateString: ISODateString
  }
}
</script>