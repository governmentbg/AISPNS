<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      heading="Преглед на дело по несъстоятелност"
      goBackText="Обратно към всички дела"
      goBackTo="/cases"
    />

    <v-row class="mt-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          class="py-5 mb-5"
          color="secondary darken-2"
          @click="$router.push({path: `/cases/bankruptcy/${data.id}/book`})"
        >
          <v-icon left dark>mdi-book-open-variant</v-icon>
          Книга по чл.634в
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="secondary darken-1"
          @click="$router.push({path: `/cases/bankruptcy/${data.id}/acts`})"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          Актове подлежащи на обжалване
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="secondary"
          @click="showSyndicReports"
        >
          <v-icon left dark>mdi-chart-tree</v-icon>
          Отчети на синдика по делото
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="primary"
          @click="showJournalActivities"
        >
          <v-icon left dark>mdi-book-account-outline</v-icon>
          Дневник на синдика
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="primary lighten-1"
          @click="showJournalProperties"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          Маса по несъстоятелност
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          dark
          color="blue-grey darken-1"
          @click="$router.push({path: `/cases/bankruptcy/${data.id}/registredEntries`})"
          v-if="data.isProprietor"
        >
          <v-icon left dark>mdi-book-edit-outline</v-icon>
          Данни за производството по несъстоятелност
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          dark
          color="blue-grey darken-2"
          @click="$router.push({path: `/cases/bankruptcy/${data.id}/actsAnnounced`})"
          v-if="data.isProprietor"
        >
          <v-icon left dark>mdi-message-alert-outline</v-icon>
          Обявени актове по несъстоятелност
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="d-print-none mt-10">
      <v-col cols="12" xl="6" offset-xl="3" lg="8" offset-lg="2" class="mx-auto">
        <base-case-card :data="data" />
      </v-col>
      <v-col cols="12">
        <base-material-card
          color="primary"
          icon="mdi-account-supervisor"
          inline
          class="px-5 py-5 mt-5"
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">Страни по делото</div>
          </template>

          <base-kendo-grid
            :columns="caseSidesTable.headers"
            :items="data.sides"
            :hasPaging="false"
            sortable
            :sort.sync="caseSidesTable.sort"
          />
        </base-material-card>
      </v-col>

      <v-col cols="12">
        <base-material-card
          color="primary"
          icon="mdi-file-arrow-left-right-outline"
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
            <template v-if="movementsData?.length">
              <v-expansion-panel
                v-for="(item,i) in movementsData"
                :key="i"
              >
                <v-expansion-panel-header>{{ `${ISODateString(item.date)} ${item.actvity ? ' - '+ item.actvity : ''}` }}</v-expansion-panel-header>
                <v-expansion-panel-content>
                  {{ item.sessionResult }}
                </v-expansion-panel-content>
              </v-expansion-panel>
            </template>
            <template v-else>
              <v-row>
                <v-col cols="12">
                  <v-alert
                    border="top"
                    color="secondary lighten-1"
                    dark
                    class="text-center"
                  >
                    Няма данни
                  </v-alert>
                </v-col>
              </v-row>
            </template>
          </v-expansion-panels>
        </base-material-card>
      </v-col>
    </v-row>
    <syndic-journal-activity-modal ref="syndicJournalActivityModal" :syndic-data="data" />
    <syndic-journal-property-modal ref="syndicJournalPropertyModal" />
    <case-reports-modal ref="caseReportsModal" @previewReport="previewCaseReport" />
    <syndic-report-modal ref="caseReportModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';  
import {
  SyndicJournalActivityModal,
  SyndicJournalPropertyModal,
  CaseReportsModal,
  SyndicReportModal
} from "@/components";
import { ISODateString } from '@/utils';

import { apiGetCaseById, apiGetCaseBook } from "@/api/cases";
export default {
  name: "previewBankruptcyCase",
  components: {
    SyndicJournalActivityModal,
    SyndicJournalPropertyModal,
    CaseReportsModal,
    SyndicReportModal
  },
  data(){
    return {
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
    this.caseSidesTable.headers = [
      { title: "Име", field: "name", cell: this.renderSideName, sortable: true },
      { title: "Тип", field: "involvementKind.description", sortable: true },
    ]
    this.data.id = this.$route.params.id;

    this.getData();
  },
  computed: {
  },
  methods: {
    getData(){
      apiGetCaseById(this.data.id).then(result => {
        this.$set(this, "data", result);
        this.getCaseBookData();
      })
    },
    getCaseBookData(){
      apiGetCaseBook(this.data.id).then(data => {
        this.movementsData = data;
      })
    },
    renderSideName(h, tdElement , props ) {
      let item = props.dataItem;
      let result = '';
      if(item.entity)
        result = item.entity.name;

      if(item.person)
        result = item.person.firstName + ' ' + item.person.lastName;

      return h('td', null, [
        result
      ]);
    },
    showSyndicReports(){
      this.$refs.caseReportsModal.openModal(this.data.id);
    },
    previewCaseReport(data){
      this.$refs.caseReportModal.openModal(data.id)
    },
    showJournalProperties(){
      this.$refs.syndicJournalPropertyModal.openModal(this.data.id);
    },
    showJournalActivities(){
      this.$refs.syndicJournalActivityModal.openModal(this.data.id);
    },
    ISODateString: ISODateString
  }
}
</script>