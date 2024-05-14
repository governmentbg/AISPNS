<template>
  <base-material-filter
    :title="$t('statistics_and_reports_filter')"
    :hasFilters="hasActiveFilters"
    v-on:do-filter="doFilter"
    v-on:remove-filter="clearFilters"
    class="ma-1"
  >
    <v-row @keypress.enter="doFilter" class="mx-1">
      <v-col md="6" cols="12">
        <base-autocomplete
          :label="$t('by_type')"
          v-model="filters.type"
          :items="nomenclatures.types"
          item-text="name"
          item-value="id"
          clearable
        />
      </v-col>
      <v-col md="6" cols="12">
        <base-autocomplete
          :label="$t('source')"
          v-model="filters.source"
          :items="nomenclatures.sources"
          item-text="name"
          item-value="id"
          clearable
        />
      </v-col>
      <v-col cols="12" xl="2" lg="2" md="6">
        <base-material-date-picker
          :label="$t('from_date')"
          v-model="filters.fromDate"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="2" md="6">
        <base-material-date-picker
          :label="$t('to_date')"
          v-model="filters.toDate"
        />
      </v-col>
    </v-row>
  </base-material-filter>
</template>

<script>
import { isEmptyObject } from "@/utils";
import { apiMetaDataGetReportSources, apiMetaDataGetReportTypes } from "@/api/metaData"
const filtersModel = Object.freeze({
  type: null,
  source: null,
  fromDate: null,
  toDate: null,
});

export default {
  name: "statisticsAndReportsFilter",
  props: {},
  data() {
    return {
      nomenclatures: {
        types: [],
        sources: []
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
    };
  },
  mounted() {
    this.getTypes();
    this.getSources();
  },
  methods: {
    getTypes(){
      apiMetaDataGetReportTypes().then(result => {
        this.nomenclatures.types = result;
      })
    },
    getSources(){
      apiMetaDataGetReportSources().then(result => {
        this.nomenclatures.sources = result;
      })
    },
    doFilter() {
      for(let key in this.filters){
        if(this.filters[key] === "")
          this.filters[key] = null;
      }

      if (!isEmptyObject(this.filters)) {
        this.filters.page = 1;
        this.hasActiveFilters = true;
        this.$emit("doFilter");
        delete this.filters.page;
      } else {
        if (this.hasActiveFilters === true) this.$emit("doFilter");

        this.hasActiveFilters = false;
      }
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


