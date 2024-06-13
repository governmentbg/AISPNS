<template>
  <base-material-filter
    title="Търсене на инспекции"
    :hasFilters="hasActiveFilters"
    v-on:do-filter="doFilter"
    v-on:remove-filter="clearFilters"
    class="ma-1"
  >
    <v-row @keypress.enter="doFilter" class="mx-1">
      <v-col cols="12" xl="4" lg="4" md="6">
        <base-syndic-autocomplete
          label="Синдик"
          v-model="filters.syndicId"
          :items="nomSyndics"
          clearable
        />
      </v-col>
      <v-col cols="12" xl="4" lg="4" md="6">
        <base-autocomplete
          label="Тип на инспекцията"
          v-model="filters.inspectionTypeID"
          :items="nomInspectionTypes"
          item-text="description"
          item-value="id"
          clearable
        />
      </v-col>
      <v-col cols="12" xl="4" lg="4" md="6">
        <base-input
          label="Инспектор"
          v-model="filters.inspectorName"
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
import { apiMetaDataGetInspections, apiMetaDataGetSyndics } from "@/api/metaData";

const filtersModel = Object.freeze({
  syndicId: null,
  inspectionTypeID: null,
  inspectorName: null,
  fromDate: null,
  toDate: null
});

export default {
  name: "inspectionsFilter",
  props: {
    inspectionTypes: {
      type: Array,
      default: () => null
    },
    syndics: {
      type: Array,
      default: () => null
    }
  },
  data() {
    return {
      nomenclatures: {
        inspectionTypes: [],
        syndics: []
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
    };
  },
  computed: {
    nomSyndics() {
      if(this.syndics === null)
        return this.nomenclatures.syndics;

      return this.syndics;
    },
    nomInspectionTypes() {
      if(this.inspectionTypes === null)
        return this.nomenclatures.inspectionTypes;

      return this.inspectionTypes;
    },
  },
  mounted() {
    if(this.inspectionTypes === null) this.getInspectionTypes();
    if(this.syndics === null) this.getSyndics();
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
    getInspectionTypes(){
      apiMetaDataGetInspections().then(result => {
        this.$set(this.nomenclatures, "inspectionTypes", result);
      });
    },
    getSyndics(){
      apiMetaDataGetSyndics().then(result => {
        this.$set(this.nomenclatures, "syndics", result);
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


