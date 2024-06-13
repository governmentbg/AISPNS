<template> 
  <v-autocomplete
    refs="component"
    append-icon=""
    :label="label"
    v-model="internalValue"
    :items="employeeList"
    :filter="filter"
    :color="color"
    item-text="fullName"
    :item-value="textValue"
    :rules="rules"
    :type="type"
    :multiple="multiple"
    ref="autocomplete"
    return-object
    dense
    hide-no-data
    :readonly="readonly"
    :disabled="disabled"
    :clearable="clearable"
    :placeholder="placeholder"
    @change="change"
    class="staticSyndicAutocomplete"
  >
    <template v-slot:selection="{ item }">
      <template v-if="!multiple">
        {{ item.fullName + getEmployeePosition(item) }}
      </template>
      <template v-else>
        <v-chip small :key="item.fullName" :close="!readonly" @click:close="remove(item)">
          {{ item.fullName + getEmployeePosition(item) }}
        </v-chip>
      </template>
    </template>
    <template v-slot:item="{ item }">
      <v-list-item-content>
        <v-list-item-title>
          <strong>{{ item.fullName + getEmployeePosition(item) }}</strong>
        </v-list-item-title>
        <v-list-item-subtitle v-html="getEmployeeShortGeneralInfo(item)">
        </v-list-item-subtitle>
      </v-list-item-content>
    </template>
    <template v-slot:label>
      <span class="syndicAutocompleteLabel ">{{ label }}</span>
    </template>
  </v-autocomplete>
</template>


<script>
import Proxyable from 'vuetify/lib/mixins/proxyable';
import { eUserRoles } from '@/utils/enums/enumerators';
import { getEmployeeShortGeneralInfo } from '@/utils';
export default {
  components: {  },
  name: 'base-syndic-autocomplete',
  mixins: [Proxyable],
  props: {
    label: {
      type: String,
      default: ''
    },
    rules: {
      type: Array,
      default: () => {
        return []
      }
    },
    disabled: {
      type: Boolean,
      default: false
    },
    type: {
      type: String,
      default: 'text'
    },
    outlined: {
      type: Boolean,
      default: true
    },
    clearable: {
      type: Boolean,
      default: false
    },
    color: {
      type: String,
      default: 'secondary'
    },
    searchInField: {
      type: String,
      default: 'fullName'
    },
    textValue: {
      type: String,
      default: 'id'
    },
    returnObject: {
      type: Boolean,
      default: false
    },
    items: {
      type: Array,
      default: () => {
        return []
      }
    },
    multiple: {
      type: Boolean,
      default: false
    },
    readonly: {
      type: Boolean,
      default: false
    },
    disabled: {
      type: Boolean,
      default: false
    },
    placeholder: {
      type: String,
      default: null
    },
  },
  data(){
    return {
      search: null,
      loading: false,
      timer: null,
      paging: {
        page: 1,
        size: 10,
      },
    }
  },
  created(){
  },
  watch: {
    multiple(val){
      if(!val){
        this.internalValue = null;
      } else {
        this.internalValue = [];
      }
    }
  },
  computed: {
    employeeList(){
      if(this.items && this.items.length){
        let rawItems = this.items.map(item => {
          item.userId = item.id;
          item.role = this.translateRole(item.roleNames);
          return item;
        })
        
        return rawItems.filter(item => item.roleNames.some(role => [eUserRoles.EMPLOYEE, eUserRoles.MEI_EMPLOYEE].includes(role)));
      }

      return [];
    },
  },
  methods: {
    filter(item, queryText, itemText){
      return item.fullName.toLocaleLowerCase().indexOf(queryText.toLocaleLowerCase()) > -1;
    },
    translateRole(roles){
      if(roles.includes(eUserRoles.MEI_EMPLOYEE)){
        return 'Служител МП';
      } else if(roles.includes(eUserRoles.EMPLOYEE)){
        return 'Служител МИИ';
      }
    },
    getEmployeePosition(employee){
      if(employee.role)
        return ` (${employee.role})`;

      return '';
    },
    getEmployeeShortGeneralInfo,
    remove(item){
      let index = this.internalValue.findIndex(i => i[this.textValue] === item[this.textValue]);
      this.internalValue.splice(index, 1);
    },
    onInput() {
      if(this.multiple){
        this.internalValue = this.internalValue.map(syndic => syndic[this.textValue]);
      } else {
        this.internalValue = this.internalValue[this.textValue]
      }
    },
    change(){
      this.$emit('changed', this.internalValue);
    }

  }
}
</script>



