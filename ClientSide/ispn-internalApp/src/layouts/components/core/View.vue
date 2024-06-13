<template>
  <v-main>
    <keep-alive :include="persistentComponents">
      <localization-provider :language="'bg-BG'">
        <router-view :key="$route.fullPath" />
      </localization-provider>
    </keep-alive>
    <div id="footerContainer">
      <core-app-footer />
    </div>
  </v-main>
</template>

<script>
import CoreAppFooter from "./Footer.vue";
import { LocalizationProvider, loadMessages, load } from "@progress/kendo-vue-intl";

import likelySubtags from 'cldr-core/supplemental/likelySubtags.json';
import currencyData from 'cldr-core/supplemental/currencyData.json';
import weekData from 'cldr-core/supplemental/weekData.json';

import numbers from 'cldr-numbers-full/main/bg/numbers.json';
import currencies from 'cldr-numbers-full/main/bg/currencies.json';
import caGregorian from 'cldr-dates-full/main/bg/ca-gregorian.json';
import dateFields from 'cldr-dates-full/main/bg/dateFields.json';
import timeZoneNames from 'cldr-dates-full/main/bg/timeZoneNames.json';

load(
  likelySubtags,
  currencyData,
  weekData,
  numbers,
  currencies,
  caGregorian,
  dateFields,
  timeZoneNames
);

const bgMessages = {
  datepicker: {
    toggleCalendar: 'Превключване на календара',
  },
  calendar: {
    today: 'Днес',
  },
  dateinput: {
    increment: 'Увеличаване на стойността',
    decrement: 'Намаляване на стойността',
  },
  numerictextbox: {
    increment: 'Увеличаване на стойността',
    decrement: 'Намаляване на стойността',
  },
  grid: {
    groupPanelEmpty: 'Дръпни колона и я пусни тук, за да групираш',
    noRecords: 'Няма налични записи.',
    pagerFirstPage: 'Към първата страница',
    pagerPreviousPage: 'Към предишната страница',
    pagerNextPage: 'Към следващата страница',
    pagerLastPage: 'Към последната страница',
    pagerPage: 'Страница',
    pagerOf: 'от',
    pagerItems: 'записи',
    pagerInfo: '{0} - {1} от {2} записи',
    pagerItemsPerPage: 'записи на страница',
    filterEqOperator: 'Е равно на',
    filterNotEqOperator: 'Не е равно на',
    filterIsNullOperator: 'Е null',
    filterIsNotNullOperator: 'Не е null',
    filterIsEmptyOperator: 'Е празно',
    filterIsNotEmptyOperator: 'Не е празно',
    filterStartsWithOperator: 'Започва с',
    filterContainsOperator: 'Съдържа',
    filterNotContainsOperator: 'Не съдържа',
    filterEndsWithOperator: 'Завършва на',
    filterGteOperator: 'Е по-голяма или равно на',
    filterGtOperator: 'Е по-голямо от',
    filterLteOperator: 'Е по-малко или равно на',
    filterLtOperator: 'Е по-малко от',
    filterIsTrue: 'Да',
    filterIsFalse: 'Не',
    filterBooleanAll: '(Всички)',
    filterAfterOrEqualOperator: 'Е след или равно на',
    filterAfterOperator: 'След',
    filterBeforeOperator: 'Преди',
    filterBeforeOrEqualOperator: 'Е преди или равно на',
    filterFilterButton: 'Филтрирай',
    filterClearButton: 'Изчисти',
    filterAndLogic: 'И',
    filterOrLogic: 'Или',
  },
};
loadMessages(bgMessages, 'bg-BG');
export default {
  name: 'CoreAppView',
  data(){
    return {
      persistentComponents: [
        'businessProcessesList',
        'documentsReport'
      ]
    }
  },
  components: {
    CoreAppFooter,
    LocalizationProvider
  },
}
</script>

<style lang="sass" scoped>
#footerContainer
  margin-top: 100px
</style>