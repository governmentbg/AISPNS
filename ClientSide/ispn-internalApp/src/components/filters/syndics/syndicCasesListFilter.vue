<template>
  <div>
    <base-material-filter
      title="Търсене на дела"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters"
    >
      <v-container @keypress.enter="doFilter" class="mx-1 py-2">
        <v-row>
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
          <v-col xl="2" lg="2" md="3" cols="12">
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
          <v-col xl="4" lg="4" md="5" cols="12">
            <base-autocomplete
              label="Съд"
              v-model="filters.courtNumber"
              :items="nomCourts"
              item-text="name"
              item-value="number"
            />
          </v-col>
        </v-row>
      </v-container>
      
    </base-material-filter>
  </div>
</template>

<script>
import { isEmptyObject } from "@/utils";
import { apiMetaDataGetCourts } from "@/api/metaData";
const filtersModel = Object.freeze({
  caseNumber: null,
  caseYear: null,
  courtNumber: null,
});

export default {
  name: "syndicCasesListFilter",
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
      },
      personType: 0,
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
        this.$set(this.nomenclatures, "courts", result);
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


