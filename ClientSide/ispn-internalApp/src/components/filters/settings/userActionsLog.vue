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
              v-model="filters.userName"
              :items="nomenclatures.users"
              item-text="person.fullName"
              item-value="userName"
              clearable
              :rules="[rules.required]"
            />
          </v-col>
          <v-col xl="3" lg="4" md="6" cols="12">
            <base-autocomplete
              label="Действие"
              v-model="filters.userActionType"
              :items="nomenclatures.userActionTypes"
              item-text="name"
              item-value="value"
              clearable
            />
          </v-col>
          <v-col xl="2" lg="2" md="6" cols="12">
            <base-material-date-picker
              v-model="filters.fromDate"
              label="От дата"
            />
          </v-col>
          <v-col xl="2" lg="2" md="6" cols="12">
            <base-material-date-picker
              v-model="filters.toDate"
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
// import { 
//   apiGetUsers
// } from '@/api/task-shreder/account'

const filtersModel = Object.freeze({
  userName: null,
  userActionType: null,
  fromDate: null,
  toDate: null,
  phone: null,
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
    this.nomenclatures.userActionTypes = this.userActionTypes;
  },
  methods: {
    getUsers(){
      // apiGetUsers({skip: 0, take: 99999}, {userName: null}).then(result => {
      //   this.$set(this.nomenclatures, "users", result.items)
      // })
    },
    getUserActions(){

    },
    doFilter(){
      if(this.$refs.form.validate())
        if(!isEmptyObject(this.filters)){
          this.filters.page = 1;
          this.hasActiveFilters = true;
          this.$emit("doFilter");
          delete this.filters.page;
        } else {
          if(this.hasActiveFilters === true)
            this.$emit("doFilter");
          
          this.hasActiveFilters = false;
        }
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

