<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <v-row class="mt-5 d-print-none">
      <v-col lg="8" offset-lg="2"  class="text-right mx-auto">
        <v-btn
          class="py-5 mb-5"
          color="primary"
          @click="$router.push({path: `/cases/stabilization/${id}/acts`})"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          {{ $t('acts_subject_to_appeal') }}
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
                    <td>{{ $t('court') }}:</td>
                    <td>Окръжен съд – Благоевград</td>
                  </tr>
                  <tr>
                    <td>{{ $t('number') }}:</td>
                    <td>189/2023</td>
                  </tr>
                  <tr>
                    <td>{{ $t('date') }}:</td>
                    <td>2023-10-18</td>
                  </tr>
                  <tr>
                    <td>{{ $t('kind') }}:</td>
                    <td>Търговско дело</td>
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
          icon="mdi-account-supervisor"
          inline
          class="px-5 py-5 mt-5"
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">{{ $t('case_sides') }}</div>
          </template>

          <base-data-table
            :headers="caseSidesTable.headers"
            :hasPaging="false"
            :data="data.sides"
            class="mb-5"
          />
        </base-material-card>
      </v-col>

      <v-col cols="12">
        <base-material-card
          color="primary"
          icon="mdi-file-arrow-left-right"
          inline
          class="px-5 py-5 mt-5"
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">Движения по делото</div>
          </template>

          <v-expansion-panels
            v-model="movementPanels"
            multiple
          >
            <v-expansion-panel
              v-for="(item,i) in movementsData"
              :key="i"
            >
              <v-expansion-panel-header>{{ `${ISODateString(item.date)} ${item.actvity ? ' - '+ item.actvity : ''}` }}</v-expansion-panel-header>
              <v-expansion-panel-content>
                {{ item.sessionResult }}
              </v-expansion-panel-content>
            </v-expansion-panel>
          </v-expansion-panels>
        </base-material-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, isValidGUID } from '@/utils';
import { apiGetCaseById, apiGetCaseBook } from '@/api/cases';
export default {
  name: "previewStabilizationCase",
  components: {
  },
  data(){
    return {
      id: null,
      data: {},
      caseSidesTable: {
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
      },
      movementPanels: [],
      movementsData: [],
    }
  },
  mounted(){
    if(!isValidGUID(this.$route.params.id)){
      this.$router.push('/cases/stabilization/by-case')
    } else {
      this.caseSidesTable.headers = [
        { text: this.$t('name'), value: "sideName", sortable: false, valueType: "text" },
        { text: this.$t('kind'), value: "involvementKindName", sortable: false, valueType: "text" },
      ];

      this.id = this.$route.params.id;

      this.getData();
    }
  },
  computed: {
    ...mapGetters([
      'isAdministrator'
    ]),
  },
  methods: {
    getData(){
      apiGetCaseById(this.id).then(data => {
        this.data = data;
        this.getCaseBookData();
      })
    },
    getCaseBookData(){
      apiGetCaseBook(this.id).then(data => {
        this.movementsData = data;
      })
    },
    ISODateString: ISODateString
  }
}
</script>