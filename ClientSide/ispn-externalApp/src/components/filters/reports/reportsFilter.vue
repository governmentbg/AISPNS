<template>
  <base-material-filter
    title="Търсене"
    :hasFilters="hasActiveFilters"
    v-on:do-filter="doFilter"
    v-on:remove-filter="clearFilters"
    class="ma-1"
  >
    <v-row @keypress.enter="doFilter" class="mx-1">
      <v-col xl="2" lg="3" md="4" cols="12">
        <base-input
          label="Дело номер"
          v-model="filters.number"
          type="number"
          clearable
          autocomplete="off"
        />
      </v-col>
      <v-col xl="2" lg="2" md="3" cols="12">
        <base-input
          label="Дело година"
          type="number"
          v-model="filters.year"
          clearable
          autocomplete="off"
        />
      </v-col>
      <v-col xl="5" lg="4" md="5" cols="12">
        <base-autocomplete
          label="Съд"
          v-model="filters.court"
          :items="nomenclatures.courts"
          item-text="name"
          item-value="id"
          clearable
        />
      </v-col>
      <v-col cols="12" xl="3" lg="4" md="6">
        <base-autocomplete
          label="Вид образец"
          v-model="filters.templateType"
          :items="nomenclatures.templateTypes"
          item-text="label"
          item-value="value"
          clearable
        />
      </v-col>
      <v-col cols="12" xl="2" lg="2" md="6">
        <base-material-date-picker
          label="От дата"
          v-model="filters.fromDate"
          clearable
        />
      </v-col>
      <v-col cols="12" xl="2" lg="2" md="6">
        <base-material-date-picker
          label="До дата"
          v-model="filters.toDate"
          clearable
        />
      </v-col>
    </v-row>
  </base-material-filter>
</template>

<script>
import { isEmptyObject } from "@/utils";

const filtersModel = Object.freeze({
  number: null,
  year: null,
  court: null,
  templateType: null,
  fromDate: null,
  toDate: null
});

export default {
  name: "reportsFilter",
  props: {},
  data() {
    return {
      nomenclatures: {
        templateTypes: [
          {label: "Месечен отчет", value: 0},
          {label: "6 месечен отчет", value: 1},
          {label: "Годишен отчет", value: 2},
          {label: "Отчет за разходите", value: 3},
          {label: "Степента на удовлетворяване на кредиторите", value: 4},
          {label: "Отговор по конкретно зададени въпроси", value: 5},
          {label: "Образец \"Сметка за разпределение\"", value: 6},
          {label: "Образец \"План за осребряване\"", value: 7},
        ],
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
    };
  },
  mounted() {
  },
  watch: {
    filters: {
      handler: function() {
        let isEmpty = true;
        for (const key in this.filters) {
          if (this.filters[key] !== null && this.filters[key] !== "") {
            isEmpty = false;
            break;
          }
        }

        if(isEmpty)
          this.hasActiveFilters = false;
      },
      deep: true,
    },
  },
  methods: {
    doFilter() {
      for(let key in this.filters){
        if(this.filters[key] === "")
          this.filters[key] = null;
      }

      if (!isEmptyObject(this.filters)) {
        this.filters.page = 1;
        this.hasActiveFilters = true;
        delete this.filters.page;
      } else {
        this.hasActiveFilters = false;
      }
      this.$emit("doFilter");
    },
    clearFilters() {
      this.filters = Object.assign({}, filtersModel);

      this.hasActiveFilters = false;

      this.filters.page = 1;
      this.$emit("doFilter", true);
      delete this.filters.page;
    },
  },
};
</script>


