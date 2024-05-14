<template>
  <base-material-filter
    :title="$t('sell_announcements_filter')"
    :hasFilters="hasActiveFilters"
    v-on:do-filter="doFilter"
    v-on:remove-filter="clearFilters"
    class="ma-1"
  >
    <v-row @keypress.enter="doFilter" class="mx-1">
      <v-col xl="2" lg="3" md="4" cols="12">
        <v-text-field
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
      <v-col xl="5" lg="4" md="5" cols="12">
        <base-autocomplete
          :label="$t('court')"
          v-model="filters.courtNumber"
          :items="nom_courts"
          item-text="name"
          item-value="number"
        />
      </v-col>
      <v-col cols="12" xl="3" lg="3" md="6">
        <base-input
          :label="$t('egn_eik_bulstat_debtor')"
          v-model="filters.debtorIdentifier"
        />
      </v-col>
      <v-col cols="12" xl="3" lg="4" md="6">
        <base-input
          :label="$t('debtor_name')"
          v-model="filters.debtorName"
        />
      </v-col>
      <v-col cols="12" xl="3" lg="4" md="6">
        <base-input
          :label="$t('syndic_first_name')"
          v-model="filters.syndicFirstName"
        />
      </v-col>
      <v-col cols="12" xl="3" lg="4" md="6">
        <base-input
          :label="$t('syndic_last_name')"
          v-model="filters.syndicLastName"
        />
      </v-col>
      <v-col cols="12" xl="3" lg="4" md="6">
        <base-input
          :label="$t('city')"
          v-model="filters.city"
        />
      </v-col>
      <v-col cols="12" xl="3" lg="4" md="6">
        <base-autocomplete
          :label="$t('kind')"
          v-model="filters.objectKindId"
          :items="nom_objectKinds"
          item-text="description"
          item-value="id"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="2" md="6">
        <base-material-date-picker
          :label="$t('from_date')"
          v-model="filters.fromDate"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="2" md="6">
        <base-material-date-picker
          :label="$t('to_date')"
          v-model="filters.toDate"
        />
      </v-col>
    </v-row>
  </base-material-filter>
</template>

<script>
import { isEmptyObject } from "@/utils";
import { apiMetaDataGetCourtsList, apiMetaDataGetObjectKind } from "@/api/metaData";

const filtersModel = Object.freeze({
  number: null,
  year: null,
  court: null,
  uniqueIdentifierDebtor: null,
  debtorName: null,
  syndicFirstName: null,
  syndicLastName: null,
  city: null,
  objectKindId: null,
  fromDate: null,
  toDate: null
});

export default {
  name: "sellAnnouncementsFilter",
  props: {
    courts: {
      type: Array,
      default: () => null
    },
    objectKinds: {
      type: Array,
      default: () => null
    }
  },
  data() {
    return {
      nomenclatures: {
        courts: [],
        objectKinds: []
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
    };
  },
  computed: {
    nom_courts() {
      if(this.courts !== null){
        return this.courts
      } else {
        return this.nomenclatures.courts;
      }
    },
    nom_objectKinds() {
      if(this.objectKinds !== null){
        return this.objectKinds
      } else {
        return this.nomenclatures.objectKinds;
      }
    }
  },
  mounted() {
    if(this.courts === null)
      this.getCourts();
    
    if(this.objectKinds === null)
      this.getObjectKinds();
  },
  methods: {
    getCourts() {
      apiMetaDataGetCourtsList().then((result) => {
        this.$set(this.nomenclatures, "courts", result);
      });
    },
    getObjectKinds(){
      apiMetaDataGetObjectKind().then((result) => {
        this.$set(this.nomenclatures, "objectKinds", result);
      })
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


