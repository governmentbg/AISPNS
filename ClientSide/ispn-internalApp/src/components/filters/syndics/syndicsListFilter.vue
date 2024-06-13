<template>
  <div>
    <base-material-filter
      title="Търсене на синдици"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters"
    >
      <v-row @keypress.enter="doFilter" class="mx-1">
        <v-col xl="2" lg="3" md="4" cols="12">
          <v-text-field
            label="Име"
            v-model="filters.firstName"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col xl="2" lg="3" md="4" cols="12">
          <v-text-field
            label="Презиме"
            v-model="filters.secondName"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col xl="2" lg="3" md="4" cols="12">
          <v-text-field
            label="Фамилия"
            v-model="filters.lastName"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col xl="2" lg="3" md="4" cols="12">
          <v-text-field
            label="Град"
            v-model="filters.city"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col xl="2" lg="3" md="4" cols="12">
          <v-text-field
            label="ЕГН"
            v-model="filters.egn"
            clearable
            autocomplete="off"
            color="secondary"
            dense
          />
        </v-col>
        <v-col xl="2" lg="3" md="4" cols="12">
          <base-select
            label="Активност"
            v-model="filters.active"
            clearable
            autocomplete="off"
            :items="nomenclautres.activeStatuses"
            item-text="label"
            item-value="value"
            color="secondary"
            dense
          />
        </v-col>
        <v-col xl="5" lg="6" cols="12">
          <base-select
            label="Статус"
            v-model="filters.syndicStatusID"
            clearable
            autocomplete="off"
            :items="nomenclautres.statuses"
            item-text="description"
            item-value="id"
            color="secondary"
            dense
          />
        </v-col>
      </v-row>
    </base-material-filter>
  </div>
</template>

<script>
import { apiMetaDataGetSyndicStatuses } from "@/api/metaData";
import { isEmptyObject } from "@/utils";

const filtersModel = Object.freeze({
  firstName: null,
  secondName: null,
  lastName: null,
  city: null,
  egn: null,
  syndicStatusID: null,
  active: null
});

export default {
  name: "syndicsListFilter",
  data() {
    return {
      nomenclautres: {
        statuses: [],
        activeStatuses: [
          { label: "Активни", value: true },
          { label: "Неактивни", value: false },
          { label: "Всички", value: null }
        ]
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
    };
  },
  mounted() {
    this.getSyndicStatuses();
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
    getSyndicStatuses(){
      apiMetaDataGetSyndicStatuses().then(result => {
        this.nomenclautres.statuses = result;
      })
    }
  },
};
</script>


