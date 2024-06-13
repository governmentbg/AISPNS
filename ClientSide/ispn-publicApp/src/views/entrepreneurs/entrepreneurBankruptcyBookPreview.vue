<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <v-row class="mt-5 d-print-none">
      <v-col cols="12" class="text-right mx-auto">
        <v-btn
          class="py-5 mb-5"
          color="primary"
          @click="$router.push({path: `/entrepreneurs/bankruptcy/${data.id}`})"
        >
          <v-icon left dark>mdi-gavel</v-icon>
          {{ $t('go_to_case') }}
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="primary lighten-1"
          @click="$router.push({path: `/entrepreneurs/bankruptcy/${data.id}/acts`})"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          {{ $t('acts_subject_to_appeal') }}
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="primary lighten-1"
          @click="$router.push({path: `/entrepreneurs/bankruptcy/${data.id}/proceedings-data`})"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          {{ $t('bankruptcy_proceedings_data') }}
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="primary lighten-1"
          @click="$router.push({path: `/entrepreneurs/bankruptcy/${data.id}/declared-acts`})"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          {{ $t('declared_acts_of_bankruptcy') }}
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
            <small>{{ $t('in_bankruptcy_proceedings') }}</small>
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
                    <th class="text-center">Окръжен съд – Благоевград</th>
                  </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, idx) of rows" :key="`row-${idx}`">
                      <td>{{ idx+1 }}</td>
                      <td>{{ item.date }}</td>
                      <td>{{ item.action }}</td>
                      <td>{{ item.participant }}</td>
                      <td class="text-center">
                        <template v-if="item.act">
                          <h6>{{ item.act }}</h6>
                          <v-divider />
                        </template>
                        
                        <span>{{  item.title }}</span>
                        <span v-if="item.body">{{ item.body }}</span>
                        
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
import { ISODateString } from '@/utils';
export default {
  name: "previewCaseBook",
  components: {
  },
  data(){
    return {
      data: {},
      rows: [
        {
          date: '18.10.2023',
          action: 'Молба за обявяване в несъстоятелност',
          participant: '',
          act: '',
          title: '',
          body: ``
        },
        {
          date: '18.10.2023	',
          action: 'Закрито/разпоредително',
          participant: '',
          act: 'Закрито/разпоредително заседание',
          title: 'Определение от 18.10.2023',
          body: `Постановява на основание чл. 629, ал. 1, изр. 2 от ТЗ молбата на длъжника „ГЕО ФИН ПРИМ“ ЕООД, ЕИК 201326442 за откриване на производство по несъстоятелност да се обяви в Търговския регистър. Приема представените с молбата доказателства.  Указва на молителя, че не сочи доказателства за обстоятелствата и твърденията, в молбата с които обосновава за начална дата на неплатежоспособността си  1.1.2023 г., поради което му дава възможност в 3 - дневен срок с молба до БлОС да ангажира такива.  Насрочва с.з. за 09.11.2023 г. от 13:15 ч.  `
        },
        {
          date: '19.10.2023',
          action: 'Моля, да ми бъде предоставен електронен достъп до делото, като адвокат чрез страна Цветелина Радославова Василева в качеството на Адвокат.',
          participant: 'Вносител - Цветелина Р. Василева	',
          act: '',
          title: '',
          body: ``
        },
        {
          date: '20.10.2023',
          action: 'Молба',
          participant: 'Адвокат - Цветелина Р. Василева, Молител - Гео Фин Прим ЕООД',
          act: '',
          title: '',
          body: ``
        },
        {
          date: '06.11.2023',
          action: 'дали има образувани граждански дела за откриване на производство по несъстоятелност на " Гео Фин Прим" ЕООД',
          participant: 'Друго - Одмвр - Благоевград',
          act: '',
          title: '',
          body: ``
        },
        {
          date: '09.11.2023',
          action: 'Първо',
          participant: '',
          act: 'Първо заседание',
          title: '',
          body: ``
        },
        {
          date: '14.11.2023',
          action: 'молба с приложено преводно за депозит за ССчЕ',
          participant: 'Вносител - Цветелина Р. Василева',
          act: '',
          title: '',
          body: ``
        },
        {
          date: '01.12.2023',
          action: 'делото е върнато от в.л.Росица Велинова с изготвено заключение Опис: т.д.№189/23 г. на БОС',
          participant: 'Вещо лице - Росица К. Велинова',
          act: '',
          title: '',
          body: ``
        },
        {
          date: '14.12.2023',
          action: 'Открито',
          participant: '',
          act: 'Открито заседание',
          title: '',
          body: ``
        },
      ],
    }
  },
  mounted(){
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
      // apiGetCaseBookById(this.data.id).then(data => {
      //   this.data = data;
      // })
    },
    ISODateString: ISODateString
  }
}
</script>