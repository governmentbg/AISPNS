<template>
  <div>
    <base-material-filter
      :title="!isBankruptcy ? 'Търсене на дневници' : 'Търсене на маси по несъстоятелност'"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters"
    >
      <v-row @keypress.enter="doFilter" class="mx-1">
        <v-col lg="2" md="4" cols="12">
          <base-input
            label="Номер"
            v-model="filters.caseNumber"
            type="number"
            clearable
            autocomplete="off"
          />
        </v-col>
        <v-col lg="2" md="4" cols="12">
          <base-input
            label="Година"
            type="number"
            v-model="filters.caseYear"
            clearable
            autocomplete="off"
          />
        </v-col>
        <v-col xl="3" lg="5" md="4" cols="12">
          <base-autocomplete
            label="Съд"
            v-model="filters.courtNumber"
            :items="nomCourts"
            item-text="name"
            item-value="number"
          />
        </v-col>
        <v-col xl="3" lg="3" md="4" cols="12">
          <base-input
            label="Длъжник име"
            v-model="filters.entityName"
          />
        </v-col>
        <v-col xl="2" lg="3" md="4" cols="12">
          <base-input
            label="Длъжник булстат"
            v-model="filters.bulstat"
          />
        </v-col>
        <template v-if="false">
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
        </template>
      </v-row>
    </base-material-filter>
  </div>
</template>

<script>
import { isEmptyObject } from "@/utils";
import { apiMetaDataGetCourts } from "@/api/metaData";
const filtersModel = Object.freeze({
  number: null,
  year: null,
  court: null,
  entityName: null,
  fromDate: null,
  toDate: null
});

export default {
  name: "syndicJournalsFilter",
  props: {
    isBankruptcy: {
      type: Boolean,
      default: false
    },
    courts: {
      type: Array,
      default: () => null,
    }
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
    getCourts() {
      apiMetaDataGetCourts().then((result) => {
        this.$set(this.nomenclatures, "vehicleTypes", result);
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


