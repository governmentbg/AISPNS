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
          color="secondary darken-1"
          @click="$router.push({path: `/cases/stabilization/${data.id}/acts`})"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          Актове подлежащи на обжалване
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="secondary"
        >
          <v-icon left dark>mdi-chart-tree</v-icon>
          Отчети на Довереното лице по делото
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          dark
          color="blue-grey darken-1 mr-1"
          @click="$router.push({path: `/cases/bankruptcy/${data.id}/registredEntries`})"
          v-if="data.isProprietor"
        >
          <v-icon left dark>mdi-book-edit-outline</v-icon>
          Данни за производството по стабилизация
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          dark
          color="blue-grey darken-2"
          @click="$router.push({path: `/cases/bankruptcy/${data.id}/actsAnnounced`})"
          v-if="data.isProprietor"
        >
          <v-icon left dark>mdi-message-alert-outline</v-icon>
          Обявени актове по стабилизация
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="d-print-none mt-10">
      <v-col cols="12" xl="6" offset-xl="3" lg="8" offset-lg="2" class="mx-auto">
        <base-material-card 
          icon="mdi-gavel"
          color="primary"
          inline
        >
          <template v-slot:after-heading>
            <div>Дело</div>
            <small>по производство по несъстоятелност</small>
          </template>

          <v-container class="py-7">
            <v-simple-table class="table-presentation">
              <template v-slot:default>
                <tbody>
                  <tr>
                    <td>Съд:</td>
                    <td>Окръжен съд – Благоевград</td>
                  </tr>
                  <tr>
                    <td>Номер:</td>
                    <td>189/2023</td>
                  </tr>
                  <tr>
                    <td>Дата:</td>
                    <td>2023-10-18</td>
                  </tr>
                  <tr>
                    <td>Вид дело:</td>
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
import { ISODateString } from '@/utils';

import { apiGetCaseById, apiGetCaseBook } from "@/api/cases";
export default {
  name: "previewStabilizationCase",
  components: {
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
    ISODateString: ISODateString
  }
}
</script>