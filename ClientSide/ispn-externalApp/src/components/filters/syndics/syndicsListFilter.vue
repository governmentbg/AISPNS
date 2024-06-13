<template>
  <div>
    <base-material-filter
      title="Търсене на синдици"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters"
    >
      <v-row @keypress.enter="doFilter" class="mx-1">
        <v-col xl="3" lg="4" md="4" cols="12">
          <base-input
            label="Име"
            v-model="filters.name"
            clearable
            autocomplete="off"
          />
        </v-col>
        <v-col xl="2" lg="3" md="4" cols="12">
          <base-input
            label="Град"
            v-model="filters.city"
            clearable
            autocomplete="off"
          />
        </v-col>
        <v-col xl="2" lg="3" md="4" cols="12">
          <base-input
            label="ЕГН"
            v-model="filters.egn"
            clearable
            autocomplete="off"
          />
        </v-col>
        <v-col xl="2" lg="3" md="4" cols="12">
          <base-select
            label="Статус"
            v-model="filters.status"
            clearable
            autocomplete="off"
            :items="nomenclautres.statuses"
            item-text="label"
            item-value="value"
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

const filtersModel = Object.freeze({
  name: null,
  city: null,
  egn: null,
  status: 0
});

export default {
  name: "syndicsListFilter",
  data() {
    return {
      nomenclautres: {
        statuses: [
          {
            label: "Всички",
            value: 0
          },
          {
            label: "Активни",
            value: 1
          },
          {
            label: "Временно отстранени",
            value: 2
          },
          {
            label: "Изключени",
            value: 3
          },
          {
            label: "Неактивни",
            value: 4
          }
        ]
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


