<template> 
  <v-autocomplete
    refs="component"
    append-icon=""
    :label="label"
    v-model="internalValue"
    :items="syndicsList"
    :filter="filter"
    :color="color"
    :item-value="textValue"
    :rules="rules"
    :type="type"
    :multiple="multiple"
    ref="autocomplete"
    :return-object="returnObject"
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
        {{ item.fullName }}
      </template>
      <template v-else>
        <v-chip small :close="!readonly" @click:close="remove(item)">
          {{ item.fullName }}
        </v-chip>
      </template>
    </template>
    <template v-slot:item="{ item }">
      <v-list-item-content>
        <v-list-item-title>
          <strong>{{ item.fullName }}</strong>
        </v-list-item-title>
        <v-list-item-subtitle v-html="getSyndicShortGeneralInfo(item)">
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
import { getSyndicShortGeneralInfo } from '@/utils';
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
      default: 'userId'
    },
    returnObject: {
      type: Boolean,
      default: true
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
  computed: {
    syndicsList(){
      if(this.items && this.items.length)
        return this.items.filter(item => item.userId != null);

      return [];
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
  methods: {
    filter(item, queryText, itemText){
      return item.fullName.toLocaleLowerCase().indexOf(queryText.toLocaleLowerCase()) > -1;
    },
    remove(item){
      let index = this.internalValue.findIndex(i => i[this.textValue] === item[this.textValue]);
      this.internalValue.splice(index, 1);
    },
    change(){
      this.$emit('changed', this.internalValue);
    },
    getSyndicShortGeneralInfo,
  }
}
</script>



