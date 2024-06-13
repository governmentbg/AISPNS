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
            <div class="display-2 font-weight-light">{{$t('stabilization_proceedings_data')}}</div>
          </template>

          
          <v-simple-table class="table-presentation">
            <template v-slot:default>
              <tbody>
                <tr v-for="item in tableData" :key="item.order">
                  <td v-html="item.htmlData.replace(`<p class='field-text'>`, `<p class='field-text'>${item.nameCode.split('_')[2]}. `)" class="pt-3"></td>
                  <td>{{ item.fieldEntryNumber }}</td>
                </tr>
              </tbody>
            </template>
          </v-simple-table>
        </base-material-card>
      </v-col>

    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString } from '@/utils';
export default {
  name: "previewEntrepreneurBankruptcyProceedingsData",
  components: {
  },
  data(){
    return {
      data: {},
      tableData: [
        // {
        //   "nameCode": "CR_F_901_L",
        //   "htmlData": "<div class='record-container record-container--preview'><p class='field-text'>Открито производство по несъстоятелност<br/>Вид на акт: Решение, № 31 от 01.03.2023г.<br/>Дело № 268/2022, Окръжен съд - Русе<br/>Правно основание: чл. 630, ал. 2  от ТЗ<br/>Подлежи на незабавно изпълнение<br/>Подлежи на обжалване в срок от 7 дни от датата на вписване в Търговския регистър и регистъра на ЮЛНЦ</p></div>",
        //   "fieldEntryNumber": "20230302153358",
        //   "recordMinActionDate": "2023-03-02T15:33:58",
        //   "fieldEntryDate": "2023-03-02T15:33:58",
        //   "fieldActionDate": "2023-03-02T15:33:58",
        //   "fieldIdent": "09010",
        //   "fieldOperation": 3,
        //   "order": "09010"
        // },
        // {
        //     "nameCode": "CR_F_902_L",
        //     "htmlData": "<div class='record-container record-container--preview'><p class='field-text'>Начална дата на неплатежоспособността/ свръхзадължеността: 4.1.2017 г.<br/>Вид на акт: Решение, № 31 от 01.03.2023г.<br/>Дело № 268/2022, Окръжен съд - Русе<br/>Правно основание: чл. 630, ал. 1  от ТЗ<br/>Подлежи на незабавно изпълнение<br/>Подлежи на обжалване в срок от 7 дни от датата на вписване в Търговския регистър и регистъра на ЮЛНЦ</p></div>",
        //     "fieldEntryNumber": "20230302153358",
        //     "recordMinActionDate": "2023-03-02T15:33:58",
        //     "fieldEntryDate": "2023-03-02T15:33:58",
        //     "fieldActionDate": "2023-03-02T15:33:58",
        //     "fieldIdent": "09021",
        //     "fieldOperation": 3,
        //     "order": "09021"
        // },
        // {
        //     "nameCode": "CR_F_903_L",
        //     "htmlData": "<div class='record-container record-container--preview'><p class='field-text'>Прекратени правомощия на органите на длъжника<br/>Вид на акт: Решение, № 31 от 01.03.2023г.<br/>Дело № 268/2022, Окръжен съд - Русе<br/>Правно основание: чл. 630, ал. 2  от ТЗ<br/>Подлежи на незабавно изпълнение<br/>Подлежи на обжалване в срок от 7 дни от датата на вписване в Търговския регистър и регистъра на ЮЛНЦ</p></div>",
        //     "fieldEntryNumber": "20230302153358",
        //     "recordMinActionDate": "2023-03-02T15:33:58",
        //     "fieldEntryDate": "2023-03-02T15:33:58",
        //     "fieldActionDate": "2023-03-02T15:33:58",
        //     "fieldIdent": "09030",
        //     "fieldOperation": 3,
        //     "order": "09030"
        // },
        // {
        //     "nameCode": "CR_F_907_L",
        //     "htmlData": "<div class='record-container record-container--preview'><p class='field-text'>Длъжникът е лишен от правото да управлява и да се разпорежда с имуществото, включено в масата на несъстоятелността<br/>Вид на акт: Решение, № 31 от 01.03.2023г.<br/>Дело № 268/2022, Окръжен съд - Русе<br/>Правно основание: чл. 630, ал. 2  от ТЗ<br/>Подлежи на незабавно изпълнение<br/>Подлежи на обжалване в срок от 7 дни от датата на вписване в Търговския регистър и регистъра на ЮЛНЦ</p></div>",
        //     "fieldEntryNumber": "20230302153358",
        //     "recordMinActionDate": "2023-03-02T15:33:58",
        //     "fieldEntryDate": "2023-03-02T15:33:58",
        //     "fieldActionDate": "2023-03-02T15:33:58",
        //     "fieldIdent": "09070",
        //     "fieldOperation": 3,
        //     "order": "09070"
        // },
        // {
        //     "nameCode": "CR_F_908_L",
        //     "htmlData": "<div class='record-container record-container--preview'><p class='field-text'>Постановен обща възбрана и запор върху имуществото на длъжника<br/>Вид на акт: Решение, № 31 от 01.03.2023г.<br/>Дело № 268/2022, Окръжен съд - Русе<br/>Правно основание: чл. 630, ал. 2  от ТЗ<br/>Подлежи на незабавно изпълнение<br/>Подлежи на обжалване в срок от 7 дни от датата на вписване в Търговския регистър и регистъра на ЮЛНЦ</p></div>",
        //     "fieldEntryNumber": "20230302153358",
        //     "recordMinActionDate": "2023-03-02T15:33:58",
        //     "fieldEntryDate": "2023-03-02T15:33:58",
        //     "fieldActionDate": "2023-03-02T15:33:58",
        //     "fieldIdent": "09080",
        //     "fieldOperation": 3,
        //     "order": "09080"
        // },
        // {
        //     "nameCode": "CR_F_909_L",
        //     "htmlData": "<div class='record-container record-container--preview'><p class='field-text'>Започнало е осребряване и разпределение<br/>Вид на акт: Решение, № 31 от 01.03.2023г.<br/>Дело № 268/2022, Окръжен съд - Русе<br/>Правно основание: чл. 630, ал. 2  от ТЗ<br/>Подлежи на незабавно изпълнение<br/>Подлежи на обжалване в срок от 7 дни от датата на вписване в Търговския регистър и регистъра на ЮЛНЦ</p></div>",
        //     "fieldEntryNumber": "20230302153358",
        //     "recordMinActionDate": "2023-03-02T15:33:58",
        //     "fieldEntryDate": "2023-03-02T15:33:58",
        //     "fieldActionDate": "2023-03-02T15:33:58",
        //     "fieldIdent": "09090",
        //     "fieldOperation": 3,
        //     "order": "09090"
        // },
        // {
        //     "nameCode": "CR_F_910_L",
        //     "htmlData": "<div class='record-container record-container--preview'><p class='field-text'>Обявен в несъстоятелност<br/>Вид на акт: Решение, № 31 от 01.03.2023г.<br/>Дело № 268/2022, Окръжен съд - Русе<br/>Правно основание: чл. 630, ал. 2  от ТЗ<br/>Подлежи на незабавно изпълнение<br/>Подлежи на обжалване в срок от 7 дни от датата на вписване в Търговския регистър и регистъра на ЮЛНЦ</p></div>",
        //     "fieldEntryNumber": "20230302153358",
        //     "recordMinActionDate": "2023-03-02T15:33:58",
        //     "fieldEntryDate": "2023-03-02T15:33:58",
        //     "fieldActionDate": "2023-03-02T15:33:58",
        //     "fieldIdent": "09100",
        //     "fieldOperation": 3,
        //     "order": "09100"
        // },
        // {
        //   "nameCode": "CR_F_912_L",
        //   "htmlData": "<div class='record-container record-container--preview'><p class='field-text'>Иво Стоилков Траянов<br/>Държава: БЪЛГАРИЯ<br/>Област: Русе, Община: Русе<br />Населено място: гр. Русе, п.к. 7000<br/>бул./ул. бул.Цар Освободител № 33, ет. 1, ап. 4<br/>Телефон: 082584713; 0889977654<br/>Адрес на електронна поща: <a style=\"text-decoration:underline;color:black\" href=\"mailto:itraqnov@hotmail.com\">itraqnov@hotmail.com</a><br/>Вид на синдика: постоянен<br/>Вид на акт: Определение, № 225 от 13.07.2023г.<br/>Дело № 268/2022, Окръжен съд - Русе<br/>Правно основание: Чл. 656 ал. 1 от ТЗ</p></div>",
        //   "fieldEntryNumber": "20230713164522",
        //   "fieldEntryDate": "2023-07-13T16:45:22",
        //   "fieldActionDate": "2023-07-13T16:45:22",
        //   "fieldIdent": "09120",
        //   "fieldOperation": 1,
        //   "order": "09120"
        // }
      ]
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