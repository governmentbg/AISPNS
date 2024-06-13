<template>
  <div>
    <base-material-filter
      title="Търсене на отчети"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters"
    >
      <v-row @keypress.enter="doFilter" class="mx-1">
        <v-col xl="2" lg="3" md="4" cols="12">
          <v-text-field
            label="Номер"
            v-model="filters.caseNumber"
            type="number"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col xl="2" lg="3" md="4" cols="12">
          <v-text-field
            label="Година"
            type="number"
            v-model="filters.caseYear"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col xl="4" lg="3" md="4" cols="12">
          <base-autocomplete
            label="Съд"
            v-model="filters.courtNumber"
            :items="nomCourts"
            item-text="name"
            item-value="number"
          />
        </v-col>
        <v-col xl="4" lg="3" md="4" cols="12">
          <base-autocomplete
            label="Вид образец"
            v-model="filters.reportTypeId"
            :items="nomenclatures.activityKinds"
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
import { apiMetaDataGetCourts, apiMetaDataGetSyndicReportTypes } from "@/api/metaData";
import { isEmptyObject } from "@/utils";

const filtersModel = Object.freeze({
  caseNumber: null,
  caseYear: null,
  courtNumber: null,
  reportTypeId: null,
  fromDate: null,
  toDate: null
});

export default {
  name: "syndicDocumentsReportsFilter",
  props: {
    courts: {
      type: Array,
      default: () => null,
    },
  },
  data() {
    return {
      nomenclatures: {
        courts: [],
        activityKinds: []
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
    this.getReportTypes();

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
    getCourts() {
      apiMetaDataGetCourts().then((result) => {
        this.$set(this.nomenclatures, "vehicleTypes", result);
      });
    },
    getReportTypes(){
      apiMetaDataGetSyndicReportTypes().then((result) => {
        this.$set(this.nomenclatures, "activityKinds", result);
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


