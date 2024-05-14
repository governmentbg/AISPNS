<template> 
  <div>
    <v-autocomplete
      refs="component"
      :label="label"
      v-model="selected"
      :items="filteredResults"
      :search-input.sync="search"
      :color="color"
      item-text="name"
      :item-value="textValue"
      :rules="rules"
      :type="type"
      :multiple="multiple"
      ref="autocomplete"
      return-object
      dense
      hide-no-data
      @input="onInput"
      @change="change"
      :readonly="readonly"
    >
      <template v-slot:append-item>
        <div v-intersect="endIntersect" /> 
      </template>
      <template v-slot:selection="{ item }">
        <template v-if="!multiple">
          {{ item.name }}
        </template>
        <template v-else>
          <v-chip small :key="item.name" close @click:close="remove(item)">
            {{ item.name }}
          </v-chip>
        </template>
      </template>
      <template v-slot:item="{ item }">
        <v-list-item-content>
          <v-list-item-title>
            <strong>{{ item.name }}</strong>
          </v-list-item-title>
          <v-list-item-subtitle v-html="'БУЛСТАТ:' + item.bulstat" v-if="item.bulstat">
          </v-list-item-subtitle>
        </v-list-item-content>
      </template>
    </v-autocomplete>
  </div>
</template>


<script>
import Proxyable from 'vuetify/lib/mixins/proxyable';
import { apiMetaDataGetEntities } from '@/api/metaData';

export default {
  components: { 
  },
  name: 'base-entity-autocomplete',
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
    }
  },
  data(){
    return {
      search: null,
      filteredResults: [],
      selected: [],
      loading: false,
      timer: null,
      paging: {
        page: 1,
        size: 10,
      },
    }
  },
  mounted(){
    
  },
  watch: {
    items(val){
      for(let i = 0; i < val.length; i++){
        if(val[i].id === this.internalValue){
          if(this.multiple){
            this.selected.push(val[i][this.textValue]);
          } else {
            this.selected = val[i][this.textValue]
          }
        }
      }
      this.$set(this, 'filteredResults', val)
    },
    search(val){
      clearTimeout(this.timer)

      let vue = this;
      this.timer = setTimeout(() => {
        vue.searchItems(val)
      }, 500)
    },
    multiple(val){
      if(!val){
        this.selected = null;
      } else {
        this.selected = [];
      }
    },
    internalValue(val){
      if('undefined' === typeof val){
        this.selected = null;
        this.internalValue = null;
      }
    }
  },
  methods: {
    searchItems(search = ""){
      this.search = search;
      this.paging.page = 1;
      this.loading = true;

      let query = {
        pageNumber: this.paging.page,
        pageSize: this.paging.size,
        searchCriteria: this.search
      }

      apiMetaDataGetEntities(query).then(result => {
        this.$set(this, "filteredResults", result.items)
        this.loading = false;
        this.$forceUpdate()
      })
    },
    loadMore(){
      this.loading = true;

      let query = {
        pageNumber: this.paging.page,
        pageSize: this.paging.size,
        searchCriteria: this.search
      }
      apiMetaDataGetEntities(query).then(result => {
        this.$set(this, "filteredResults", [...this.filteredResults, ...result.items]);
        this.loading = false;
        this.$forceUpdate()
      })
    },
    endIntersect(entries, observer, isIntersecting) {
      if (isIntersecting) {
        this.paging.page++;
        this.loadMore()
      }
    },
    remove(item){
      let index = this.internalValue.findIndex(i => i.id === item.id);
      this.internalValue.splice(index, 1);
    },
    onInput() {
      if(this.multiple){
        this.internalValue = this.selected.map(syndic => syndic[this.textValue]);
      } else {
        this.internalValue = this.selected[this.textValue]
      }
    },
    change(){
      this.$emit('changed', this.selected);
    },

  }
}
</script>
