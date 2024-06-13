<template>
  <base-material-filter
    title="Търсене на потребители"
    :hasFilters="hasActiveFilters"
    v-on:do-filter="doFilter"
    v-on:remove-filter="clearFilters" 
  >
    <v-row @keypress.enter="doFilter" class="mx-1">
      <v-col lg="3" md="4" cols="12">
        <v-text-field 
          label="Име"
          v-model="filters.firstName"
          clearable
          autocomplete="off"
          color="secondary"
          dense
        />
      </v-col>
      <v-col lg="3" md="4" cols="12">
        <v-text-field 
          label="Презиме"
          v-model="filters.middleName"
          clearable
          autocomplete="off"
          color="secondary"
          dense
        />
      </v-col>
      <v-col lg="3" md="4" cols="12">
        <v-text-field 
          label="Фамилия"
          v-model="filters.lastName"
          clearable
          autocomplete="off"
          color="secondary"
          dense
        />
      </v-col>
      <v-col lg="3" md="4" cols="12">
        <v-text-field 
          label="Потребителско име" 
          v-model="filters.username"
          clearable
          autocomplete="off"
          color="secondary"
          dense
        />
      </v-col>
      <v-col lg="3" md="4" cols="12">
        <v-text-field 
          label="ЕГН" 
          v-model="filters.egn"
          type="number"
          clearable
          autocomplete="off"
          color="secondary"
          dense
        />
      </v-col>
      <v-col lg="3" md="4" cols="12">
        <v-text-field 
          label="E-mail" 
          v-model="filters.email"
          clearable
          autocomplete="off"
          color="secondary"
          dense
        />
      </v-col>
      <v-col lg="3" md="4" cols="12">
        <base-select
          :items="properties.statuses"
          item-text="name"
          item-value="value"
          v-model="filters.isActive"
          label="Активност"
          clearable
          autocomplete="off"
        />
      </v-col>
      <v-col lg="3" md="4" cols="12">
        <base-select
          :items="properties.roles"
          item-text="translatedName"
          item-value="id"
          v-model="filters.roleId"
          label="Роли"
          clearable
          autocomplete="off"
        />
      </v-col>
    </v-row>
  </base-material-filter>
</template>

<script>

import { isEmptyObject } from "@/utils";
import { apiMetaDataGetUserRoles } from '@/api/metaData';
import { eUserRoleNames } from "@/utils/enums/enumerators";

const filtersModel = Object.freeze({
  firstName: null,
  middleName: null,
  lastName: null,
  username: null,
  egn: null,
  email: null,
  status: null,
  roleId: null,
});

export default {
  name: "usersFilter",
  data(){
    return {
      properties: {
        statuses: [
          {name: "Активен", value: true},
          {name: "Неактивен", value: false}
        ],
        roles: []
      },
      filters: Object.assign({}, filtersModel),
      hasActiveFilters: false
    }
  },
  mounted(){
    this.getUserRoles();
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
    doFilter(){
      for (const key in this.filters) {
        if(this.filters[key] === "")
          this.filters[key] = null;
      }
      if(!isEmptyObject(this.filters)){
        this.filters.page = 1;
        this.hasActiveFilters = true;
        this.$emit("doFilter");
        delete this.filters.page;
      } else {          
        this.hasActiveFilters = false;
      }
      this.$emit("doFilter");
    },
    getUserRoles(){
      apiMetaDataGetUserRoles().then(result => {
        this.$set(this.properties, 'roles', result.map(role => {
          role.translatedName = eUserRoleNames[role.name];
          return role;
        }));
      })
    },
    clearFilters(){
      this.filters = Object.assign({}, filtersModel);

      this.hasActiveFilters = false;

      this.filters.page = 1;
      this.$emit("doFilter", true);
      delete this.filters.page;
    },
  }
}
</script>