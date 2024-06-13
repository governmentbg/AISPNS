<template>
  <base-material-filter
    title="Търсене на статистики/справки"
    :hasFilters="hasActiveFilters"
    v-on:do-filter="doFilter"
    v-on:remove-filter="clearFilters"
    class="ma-1"
  >
    <v-row @keypress.enter="doFilter" class="mx-1">
      <v-col cols="12" xl="4" lg="4" md="6">
        <base-autocomplete
          label="По тип"
          v-model="filters.type"
          :items="nomenclatures.types"
          item-text="name"
          item-value="id"
        />
      </v-col>
      <v-col cols="12" xl="4" lg="4" md="6">
        <base-autocomplete
          label="Източник"
          v-model="filters.source"
          :items="nomenclatures.sources"
          item-text="name"
          item-value="id"
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

import { apiMetaDataGetReportSources, apiMetaDataGetReportTypes } from "@/api/metaData";
const filtersModel = Object.freeze({
  type: null,
  source: null,
  fromDate: null,
  toDate: null
});

export default {
  name: "publishedStatisticsFilter",
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
    this.getReportSources();
    this.getReportTypes();
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
    getReportSources(){
      apiMetaDataGetReportSources().then(data => {
        this.nomenclatures.sources = data;
      })
    },
    getReportTypes(){
      apiMetaDataGetReportTypes().then(data => {
        this.nomenclatures.types = data;
      })
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


