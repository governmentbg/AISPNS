<template>
  <base-material-filter
    title="Търсене на данни по несъстоятелност на предприемача"
    :hasFilters="hasActiveFilters"
    v-on:do-filter="doFilter"
    v-on:remove-filter="clearFilters"
    class="ma-1"
  >
    <v-row @keypress.enter="doFilter" class="mx-1">
      
      <v-col xl="2" lg="3" md="5" cols="12">
        <base-autocomplete
          label="Статус"
          v-model="filters.responseHttpCode"
          :items="nomenclatures.statusCodes"
          item-text="label"
          item-value="value"
        />
      </v-col>
      <v-col cols="12" xl="3" lg="4" md="6">
        <base-input
          label="IP адрес"
          v-model="filters.ipAddress"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="2" md="6">
        <base-date-time-picker
          label="От дата"
          v-model="filters.requestTimestampFrom"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="2" md="6">
        <base-date-time-picker
          label="До дата"
          v-model="filters.requestTimestampTo"
        />
      </v-col>
    </v-row>
  </base-material-filter>
</template>

<script>
import { isEmptyObject } from "@/utils";

const filtersModel = Object.freeze({
  responseHttpCode: null,
  ipAddress: null,
  requestTimestampFrom: null,
  requestTimestampTo: null
});

export default {
  name: "webServiceFilter",
  props: {},
  data() {
    return {
      nomenclatures: {
        debtors: [],
        statusCodes: [
          {label: "200", value: 200},
          {label: "500", value: 500},
        ]
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
    };
  },
  mounted() {
  },
  methods: {
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


