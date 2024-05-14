<template>
  <div>
    <base-material-filter
      :title="$t('cases_filter')"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters"
    >
      <v-container @keypress.enter="doFilter" class="mx-1 py-2">
        <v-row v-if="searchByCase">
          <v-col xl="2" lg="3" md="4" cols="12">
            <v-text-field
              label="Номер"
              :label="$t('case_number')"
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
              :label="$t('case_year')"
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
              :label="$t('court')"
              v-model="filters.courtNumber"
              :items="nomenclatures.courts"
              item-text="name"
              item-value="number"
              clearable
            />
          </v-col>
        </v-row>
        <v-row v-else>
          <v-col xl="4" lg="4" md="7" cols="12">
            <v-radio-group
              v-model="personType"
              dense
              row
              class="mt-0"
            >
              <v-radio
              :label="$t('individual_entity')"
                :value="0"
              />
              <v-radio
                :label="$t('legal_entity')"
                :value="1"
              />
            </v-radio-group>
          </v-col>
          <template  v-if="personType === 0">
            <v-col lg="2" md="6" cols="12">
              <base-input
                :label="$t('name')"
                v-model="filters.firstName"
                clearable
                dense
              />
            </v-col>
            <v-col lg="2" md="6" cols="12">
              <base-input
                :label="$t('last_name')"
                v-model="filters.lastName"
                clearable
                dense
              />
            </v-col>
            <v-col lg="4" md="6" cols="12">
              <base-input
                :label="$t('egn_lnch_birthdate')"
                v-model="filters.EGN"
                clearable
                dense
              />
            </v-col>
          </template>
          <template v-else>
            <v-col lg="4" md="6" cols="12">
              <base-input
                :label="$t('company_name')"
                v-model="filters.entityName"
                clearable
                dense
              />
            </v-col>
            <v-col lg="4" md="6" cols="12">
              <base-input
                :label="$t('egn_pik_bulstat')"
                v-model="filters.bulstat"
                clearable
                dense
              />
            </v-col>
          </template>
        </v-row>
      </v-container>
      
    </base-material-filter>
  </div>
</template>

<script>
import { isEmptyObject } from "@/utils";
import { apiMetaDataGetCourtsList } from "@/api/metaData";
const filtersModel = Object.freeze({
  caseNumber: null,
  caseYear: null,
  courtNumber: null,
  firstName: null,
  lastName: null,
  EGN: null,
  entityName: null,
  bulstat: null,
});

export default {
  name: "casesListFilter",
  props: {
    searchByCase: {
      type: Boolean,
      default: true,
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
  mounted() {
    if(this.searchByCase)
      this.getCourts();
  },
  methods: {
    getCourts() {
      apiMetaDataGetCourtsList().then((result) => {
        this.$set(this.nomenclatures, "courts", result);
      });
    },
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


