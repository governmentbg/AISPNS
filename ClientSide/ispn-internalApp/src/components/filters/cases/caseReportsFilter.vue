<template>
  <div>
    <base-material-filter
      title="Търсене на отчети"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters"
    >
      <v-container @keypress.enter="doFilter" class="mx-1 py-2">
        <v-row>
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
      </v-container>
      
    </base-material-filter>
  </div>
</template>

<script>
import { isEmptyObject } from "@/utils";
import { apiMetaDataGetSyndicReportTypes } from "@/api/metaData";

const filtersModel = Object.freeze({
  reportTypeId: null,
  fromDate: null,
  toDate: null,
});

export default {
  name: "caseReportsFilter",
  props: {
    courts: {
      type: Array,
      default: () => null,
    },
  },
  data() {
    return {
      nomenclatures: {
        activityKinds: [],
      },
      personType: 0,
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
    };
  },
  computed: {},
  mounted() {
    if(!this.nomenclatures.activityKinds.length)
      this.getActivityKinds();
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
    getActivityKinds() {
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


