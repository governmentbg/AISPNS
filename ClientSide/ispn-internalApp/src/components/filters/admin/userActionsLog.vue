<template>
  <div>
    <base-material-filter
      title="Търсене на потребителски действия"
      :hasFilters="hasActiveFilters"
      v-on:do-filter="doFilter"
      v-on:remove-filter="clearFilters" 
    >
      <v-form ref="form" lazy-validation @keypress.enter="doFilter">
        <v-row class="mx-1">
          <v-col xl="3" lg="4" md="6" cols="12">
            <base-autocomplete
              label="Потребител"
              v-model="filters.userId"
              :items="nomenclatures.users"
              item-text="fullName"
              item-value="id"
              clearable
            />
          </v-col>
          <v-col xl="3" lg="4" md="6" cols="12">
            <base-autocomplete
              label="Действие"
              v-model="filters.userAction"
              :items="nomenclatures.userActions"
              clearable
            />
          </v-col>
          <v-col xl="2" lg="2" md="6" cols="12">
            <base-date-time-picker
              v-model="filters.timestampFrom"
              label="От дата"
            />
          </v-col>
          <v-col xl="2" lg="2" md="6" cols="12">
            <base-date-time-picker
              v-model="filters.timestampTo"
              label="До дата"
            />
          </v-col>
          <v-col xl="2" lg="2" md="6" cols="12">
            <v-text-field 
              v-model="filters.ipAddress"
              label="IP адрес"
              color="secondary"
              dense
            />
          </v-col>
        </v-row>
      </v-form>
    </base-material-filter>
  </div>
</template>

<script>

import { isEmptyObject } from "@/utils";
import { 
  apiMetaDataGetUsers,
  apiMetaDataGetLogUserActions
} from '@/api/metaData'

const filtersModel = Object.freeze({
  userId: null,
  userAction: null,
  timestampFrom: null,
  timestampTo: null,
  ipAddress: null,
});

export default {
  name: "userActionsLogFilter",
  props: {
    userActionTypes: {
      type: Array,
      default: () => {
        return [];
      }
    }
  },
  data(){
    return {
      nomenclatures: {
        userActionTypes: [],
        users: [],
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false,
      rules: {
        required: value => (typeof value !== 'number' ? !!value : !!value || value === 0) || "Полето е задължително"
      },
    }
  },
  mounted(){
    this.getUsers();
    this.getUserActions();
    this.nomenclatures.userActionTypes = this.userActionTypes;
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
    getUsers(){
      apiMetaDataGetUsers().then(result => {
        this.$set(this.nomenclatures, "users", result)
      })
    },
    getUserActions(){
      apiMetaDataGetLogUserActions().then(result => {
        this.$set(this.nomenclatures, "userActions", result)
      })
    },
    doFilter(){
      for (const key in this.filters) {
        if(this.filters[key] === "")
          this.filters[key] = null;
      }
      if(this.$refs.form.validate())
        if(!isEmptyObject(this.filters)){
          this.filters.page = 1;
          this.hasActiveFilters = true;
          delete this.filters.page;
        } else {
          this.hasActiveFilters = false;
        }
        this.$emit("doFilter");
    },
    clearFilters(){
      this.filters = Object.assign({}, filtersModel);

      this.filters.page = 1;
      this.$emit("doFilter", true);
      delete this.filters.page;

      this.$emit("doFilter", true);
    },
  }
}
</script>

