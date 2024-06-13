<template>
  <div>
    <base-material-filter
      :title="!isBankruptcy ? 'Търсене на дневници' : 'Търсене на маси по несъстоятелност'"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters"
    >
      <v-row @keypress.enter="doFilter" class="mx-1">
        <v-col xl="2" lg="3" md="4" cols="12">
          <base-input
            label="Номер"
            v-model="filters.number"
            type="number"
            clearable
            autocomplete="off"
          />
        </v-col>
        <v-col xl="2" lg="3" md="4" cols="12">
          <base-input
            label="Година"
            type="number"
            v-model="filters.year"
            clearable
            autocomplete="off"
          />
        </v-col>
        <v-col xl="4" lg="3" md="4" cols="12">
          <base-autocomplete
            label="Съд"
            v-model="filters.court"
            :items="nomenclatures.courts"
            item-text="name"
            item-value="id"
          />
        </v-col>
        <v-col xl="4" lg="3" md="4" cols="12">
          <base-autocomplete
            label="Длъжник"
            v-model="filters.debtor"
            :items="nomenclatures.debtors"
            item-text="name"
            item-value="id"
          />
        </v-col>
        <v-col cols="12" xl="2" lg="3" md="4">
          <base-material-date-picker
            label="От дата"
            v-model="filters.fromDate"
          />
        </v-col>
        <v-col cols="12" xl="2" lg="3" md="4">
          <base-material-date-picker
            label="До дата"
            v-model="filters.toDate"
          />
        </v-col>
      </v-row>
    </base-material-filter>
  </div>
</template>

<script>
import { isEmptyObject } from "@/utils";

const filtersModel = Object.freeze({
  number: null,
  year: null,
  court: null,
  debtor: null,
  fromDate: null,
  toDate: null
});

export default {
  name: "journalsAndBankruptcyFilter",
  props: {
    isBankruptcy: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      nomenclatures: {
        debtors: []
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
    };
  },
  mounted() {
  },
  methods: {
    doFilter() {
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


