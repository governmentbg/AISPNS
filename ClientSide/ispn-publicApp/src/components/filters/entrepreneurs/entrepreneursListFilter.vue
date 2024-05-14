<template>
  <div>
    <base-material-filter
      :title="$t('entrepreneurs_filter')"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters"
    >
      <v-row @keypress.enter="doFilter" class="mx-1">
        <v-col xl="2" lg="3" md="4" cols="12">
          <v-text-field
            :label="$t('case_number')"
            v-model="filters.number"
            type="number"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col xl="2" lg="2" md="3" cols="12">
          <v-text-field
            :label="$t('case_year')"
            type="number"
            v-model="filters.year"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col xl="5" lg="4" md="5" cols="12">
          <base-autocomplete
            :label="$t('court')"
            v-model="filters.court"
            :items="nomenclatures.courts"
            item-text="name"
            item-value="id"
          />
        </v-col>
        <v-col lg="3" md="4" cols="12">
          <v-text-field
            :label="$t('name')"
            v-model="filters.name"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col lg="3" md="4" cols="12">
          <v-text-field
            :label="$t('egn_eik_bulstat')"
            v-model="filters.uin"
            clearable
            autocomplete="off"
            color="secondary"
            dense
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
  </div>
</template>

<script>
import { isEmptyObject } from "@/utils";

const filtersModel = Object.freeze({
  name: null,
  city: null,
  egn: null,
  status: 0
});

export default {
  name: "entrepreneursListFilter",
  data() {
    return {
      nomenclatures: {
        courts: []
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
    };
  },
  mounted() {
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


