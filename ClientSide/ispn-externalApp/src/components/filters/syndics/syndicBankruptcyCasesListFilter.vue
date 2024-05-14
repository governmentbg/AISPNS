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
            <base-input
              label="Номер"
              v-model="filters.number"
              type="number"
              clearable
              autocomplete="off"
            />
          </v-col>
          <v-col xl="2" lg="2" md="3" cols="12">
            <base-input
              label="Година"
              type="number"
              v-model="filters.year"
              clearable
              autocomplete="off"
              color="secondary"
              dense
            />
          </v-col>
          <v-col xl="4" lg="4" md="5" cols="12">
            <base-autocomplete
              label="Съд"
              v-model="filters.court"
              :items="nomenclatures.courts"
              item-text="name"
              item-value="id"
            />
          </v-col>
        </v-row>
      </v-container>
      
    </base-material-filter>
  </div>
</template>

<script>
import { isEmptyObject } from "@/utils";

const filtersModel = Object.freeze({
  number: null,
  year: null,
  court: null,
});

export default {
  name: "syndicBankruptcyCasesListFilter",
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
  mounted() {
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
      // apiGetCourts().then((result) => {
      //   this.$set(this.nomenclatures, "vehicleTypes", result);
      // });
    },
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


