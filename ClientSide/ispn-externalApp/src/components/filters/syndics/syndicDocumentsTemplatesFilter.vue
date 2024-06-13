<template>
  <div>
    <base-material-filter
      title="Търсене на образци"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters"
    >
      <v-row @keypress.enter="doFilter" class="mx-1">
        <v-col cols="12" xl="5" lg="6" md="4">
          <base-autocomplete
            label="Вид образец"
            v-model="filters.type"
            :items="nomenclatures.templateTypes"
            clearable
            autocomplete="off"
            color="secondary"
            dense
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
  type: null,
  fromDate: null,
  toDate: null
});

export default {
  name: "syndicDocumentsTemplatesFilter",
  data() {
    return {
      nomenclatures: {
        templateTypes: []
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


