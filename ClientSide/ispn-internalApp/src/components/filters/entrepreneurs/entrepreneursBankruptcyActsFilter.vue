<template>
  <base-material-filter
    title="Търсене на актове по несъстоятелност на предприемача"
    :hasFilters="hasActiveFilters"
    v-on:do-filter="doFilter"
    v-on:remove-filter="clearFilters"
    class="ma-1"
  >
    <v-row @keypress.enter="doFilter" class="mx-1">
      <v-col cols="12" xl="3" lg="4" md="6">
        <base-input
          label="Име"
          v-model="filters.proprietorName"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="4" md="6">
        <base-input
          label="ЕГН/ЕИК/Булстат"
          v-model="filters.proprietorIdentifier"
        />
      </v-col>
      <v-col xl="2" lg="3" md="4" cols="12">
        <base-input
          label="Дело номер"
          v-model="filters.caseNumber"
          type="number"
          clearable
        />
      </v-col>
      <v-col xl="1" lg="2" md="3" cols="12">
        <base-input
          label="Дело година"
          type="number"
          v-model="filters.caseYear"
          clearable
        />
      </v-col>
      <v-col xl="4" lg="4" md="5" cols="12">
        <base-autocomplete
          label="Съд"
          v-model="filters.court"
          :items="nomCourts"
          item-text="name"
          item-value="number"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="2" md="6">
        <base-material-date-picker
          label="От дата"
          v-model="filters.fromDate"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="2" md="6">
        <base-material-date-picker
          label="До дата"
          v-model="filters.toDate"
        />
      </v-col>
    </v-row>
  </base-material-filter>
</template>

<script>
import { isEmptyObject } from "@/utils";
import { apiMetaDataGetCourts } from "@/api/metaData";
const filtersModel = Object.freeze({
  proprietorName: null,
  proprietorIdentifier: null,
  caseNumber: null,
  caseYear: null,
  courtNumber: null,
  fromDate: null,
  toDate: null,
});

export default {
  name: "entreneursBankruptcyActsFilter",
  props: {
    courts: {
      type: Array,
      default: () => null,
    },
  },
  data() {
    return {
      nomenclatures: {
        courts: []
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
    };
  },
  computed: {
    nomCourts(){
      if(this.courts === null){
        return this.nomenclatures.courts;
      } else {
        return this.courts;
      }
    }
  },
  mounted() {
    if(this.courts === null)
      this.getCourts();
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
    getCourts(){
      apiMetaDataGetCourts().then((result) => {
        this.$set(this.nomenclatures, "courts", result);
      });
    },
    doFilter() {
      for (const key in this.filters) {
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


