<template>
  <div>
    <base-material-filter
      :title="$t('syndics_filter')"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters"
    >
      <v-row @keypress.enter="doFilter" class="mx-1">
        <v-col cols="12">
          <v-checkbox
            v-model="filters.isCustodian"
            :label="$t('trusted_persons')"
            :true-value="true"
            :false-value="null"
            color="secondary"
            dense
          />
        </v-col>
        <v-col lg="3" md="4" cols="12">
          <v-text-field
            :label="$t('first_name')"
            v-model="filters.firstName"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col lg="3" md="4" cols="12">
          <v-text-field
            :label="$t('last_name')"
            v-model="filters.lastName"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col lg="6" md="4" cols="12">
          <base-select
            :label="$t('speciality')"
            v-model="filters.speciality"
            clearable
            autocomplete="off"
            :items="nomenclautres.specialties"
            item-text="description"
            item-value="id"
            color="secondary"
            dense
          />
        </v-col>
        <v-col lg="3" md="4" cols="12">
          <v-text-field
            :label="$t('city')"
            v-model="filters.city"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
      </v-row>
    </base-material-filter>
  </div>
</template>

<script>
import { isEmptyObject } from "@/utils";
import { apiMetaDataGetSyndicSpecialities } from "@/api/metaData";

const filtersModel = Object.freeze({
  firstName: null,
  lastName: null,
  speciality: null,
  city: null,
  isCustodian: null
});

export default {
  name: "syndicsListFilter",
  data() {
    return {
      nomenclautres: {
        specialties: []
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
    };
  },
  mounted() {
    this.getSpecialities();
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
    getSpecialities(){
      apiMetaDataGetSyndicSpecialities().then(result => {
        this.$set(this.nomenclautres, "specialties", result);
      })
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


