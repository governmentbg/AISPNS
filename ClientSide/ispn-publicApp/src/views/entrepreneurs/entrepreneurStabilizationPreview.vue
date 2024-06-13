<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >

    <v-row class="mt-5 d-print-none">
      <v-col cols="12"  class="text-right mx-auto">
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
        <v-btn
          class="py-5 mb-5"
          color="primary lighten-1"
          @click="$router.push({path: `/entrepreneurs/stabilization/${data.id}/declared-acts`})"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          {{ $t('declared_acts_of_stabilization') }}
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
            :pagination="caseSidesTable.pagination"
            :data="caseSidesTable.data"
            @getData="getCaseSidesData"
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
            <div class="display-2 font-weight-light">{{$t('case_movements')}}</div>
          </template>

          <v-expansion-panels
            v-model="movementPanels"
            multiple
          >
            <v-expansion-panel
              v-for="(item,i) in movementsData"
              :key="i"
            >
              <v-expansion-panel-header>{{ `${item.date} ${item.title ? ' - '+ item.title : ''}` }}</v-expansion-panel-header>
              <v-expansion-panel-content>
                {{ item.body }}
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
export default {
  name: "previewEntrepreneurBankruptcy",
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
      movementsData: [
        // {
        //   date: '18.10.2023',
        //   title: 'Закрито/разпоредително заседание',
        //   body: `Определение от 18.10.2023    Постановява на основание чл. 629, ал. 1, изр. 2 от ТЗ молбата на длъжника „ГЕО ФИН ПРИМ“ ЕООД, ЕИК 201326442 за откриване на производство по несъстоятелност да се обяви в Търговския регистър. Приема представените с молбата доказателства.  Указва на молителя, че не сочи доказателства за обстоятелствата и твърденията, в молбата с които обосновава за начална дата на неплатежоспособността си  1.1.2023 г., поради което му дава възможност в 3 - дневен срок с молба до БлОС да ангажира такива.  Насрочва с.з. за 09.11.2023 г. от 13:15 ч.`
        // },
        // {
        //   date: '19.10.2023',
        //   title: '',
        //   body: 'Вносител - Цветелина Р. Василева'
        // },
        // {
        //   date: '20.10.2023',
        //   title: '',
        //   body: 'Адвокат - Цветелина Р. ВасилеваМолител - Гео Фин Прим ЕООД'
        // },
        // {
        //   date: '06.11.2023',
        //   title: '',
        //   body: 'Друго - Одмвр - Благоевград'
        // },
        // {
        //   date: '09.11.2023',
        //   title: 'Първо заседание',
        //   body: ''
        // },
        // {
        //   date: '14.11.2023',
        //   title: '',
        //   body: 'Вносител - Цветелина Р. Василева'
        // },
        // {
        //   date: '01.12.2023',
        //   title: '',
        //   body: 'Вещо лице - Росица К. Велинова'
        // },
        // {
        //   date: '14.12.2023',
        //   title: 'Открито заседание',
        //   body: ''
        // }
      ],
    }
  },
  mounted(){
    this.caseSidesTable.headers = [
      { text: this.$t('name'), value: "name", sortable: false, valueType: "text" },
      { text: this.$t('kind'), value: "type", sortable: false, valueType: "text" },
    ];
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
      // })
    },
    getCaseSidesData(){
      let query = Object.assign({}, {});
      query.pageNumber = this.caseSidesTable.pagination.skip / this.caseSidesTable.pagination.take + 1;

      query.pageSize = this.caseSidesTable.pagination.take;
      if(query.filters.page === 1){
        query.pageNumber = 1;
        this.caseSidesTable.pagination.skip = 0;
      }

      // apiGetCaseSides(query).then(result => {
      //   this.caseSidesTable.data = result.items;
      //   this.caseSidesTable.pagination.total = result.totalCount;
      //   this.caseSidesTable.pagination.page = result.currentPage;
      // })
    },
    ISODateString: ISODateString
  }
}
</script>