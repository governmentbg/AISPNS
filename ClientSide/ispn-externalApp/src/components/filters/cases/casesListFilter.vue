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
              v-model="filters.caseNumber"
              type="number"
              clearable
              autocomplete="off"
            />
          </v-col>
          <v-col xl="2" lg="2" md="3" cols="12">
            <base-input
              label="Година"
              type="number"
              v-model="filters.caseYear"
              clearable
              autocomplete="off"
            />
          </v-col>
          <v-col xl="4" lg="4" md="5" cols="12">
            <base-autocomplete
              label="Съд"
              v-model="filters.courtNumber"
              :items="nomenclatures.courts"
              item-text="name"
              item-value="number"
              clearable
            />
          </v-col>
          <v-col xl="4" lg="3" md="4" cols="12">
            <v-checkbox
              v-model="filters.onlyEntrepreneurs"
              label="Само предприемачи"
              dense
            />
          </v-col>
        </v-row>
        <v-row>
          <v-col xl="3" lg="5" md="7" cols="12">
            <v-radio-group
              v-model="personType"
              dense
              row
            >
              <v-radio
                label="Физическо лице"
                :value="0"
              ></v-radio>
              <v-radio
                label="Юридическо лице"
                :value="1"
              ></v-radio>
            </v-radio-group>
          </v-col>
          <template  v-if="personType === 0">
            <v-col xl="3" lg="3" md="6" cols="12">
              <base-input
                label="Име"
                v-model="filters.firstName"
                clearable
                dense
              />
            </v-col>
            <v-col xl="3" lg="3" md="6" cols="12">
              <base-input
                label="Фамилия"
                v-model="filters.lastName"
                clearable
                dense
              />
            </v-col>
            <v-col xl="2" lg="3" md="6" cols="12">
              <base-input
                label="ЕГН/ЛНЧ/Дата на раждане"
                v-model="filters.egn"
                clearable
                dense
              />
            </v-col>
          </template>
          <template v-else>
            <v-col xl="3" lg="3" md="6" cols="12">
              <base-input
                label="Фирма/Наименование"
                v-model="filters.entityName"
                clearable
                dense
              />
            </v-col>
            <v-col xl="2" lg="3" md="6" cols="12">
              <base-input
                label="ЕИК/ПИК/Булстат"
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
import { apiMetaDataGetCourts } from "@/api/metaData"
const filtersModel = Object.freeze({
  caseNumber: null,
  caseYear: null,
  courtNumber: null,
  entityName: null,
  bulstat: null,
  firstName: null,
  lastName: null,
  egn: null
});

export default {
  name: "casesListFilter",
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
      apiMetaDataGetCourts().then((result) => {
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


