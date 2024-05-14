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
          v-model="filters.caseNumber"
          type="number"
          clearable
          autocomplete="off"
        />
      </v-col>
      <v-col xl="2" lg="2" md="3" cols="12">
        <base-input
          label="Дело година"
          type="number"
          v-model="filters.caseYear"
          clearable
          autocomplete="off"
        />
      </v-col>
      <v-col xl="5" lg="4" md="5" cols="12">
        <base-autocomplete
          label="Съд"
          v-model="filters.courtNumber"
          :items="nomenclatures.courts"
          item-text="name"
          item-value="id"
        />
      </v-col>
      <v-col cols="12" xl="3" lg="4" md="6">
        <base-input
          label="ЕГН/ЕИК/Булстат на длъжника"
          v-model="filters.debtorIdentifier"
        />
      </v-col>
      <v-col cols="12" xl="3" lg="4" md="6">
        <base-input
          label="Име на длъжника"
          v-model="filters.debtorName"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="4" md="6">
        <base-input
          label="Име синдик"
          v-model="filters.syndicFirstName"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="4" md="6">
        <base-input
          label="Фамилия синдик"
          v-model="filters.syndicLastName"
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

const filtersModel = Object.freeze({
  caseNumber: null,
  caseYear: null,
  courtNumber: null,
  debtorIdentifier: null,
  debtorName: null,
  syndicFirstName: null,
  syndicLastName: null,
  fromDate: null,
  toDate: null
});

export default {
  name: "sellAnnouncementsFilter",
  props: {},
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


