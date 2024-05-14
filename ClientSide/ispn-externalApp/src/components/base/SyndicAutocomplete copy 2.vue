<template> 
  <v-autocomplete
    refs="component"
    append-icon=""
    :label="label"
    v-model="internalValue"
    :items="filteredSyndics"
    :search-input.sync="search"
    :color="color"
    item-text="fullName"
    :item-value="textValue"
    :rules="rules"
    :type="type"
    ref="autocomplete"
    :return-object="returnObject"
    dense
    hide-no-data
  >
    <template v-slot:append-item>
      <div v-intersect="endIntersect" /> 
    </template>
    <template v-slot:selection="{ item }">
      {{ item.fullName }}
    </template>
    <template v-slot:item="{ item }">
      <v-list-item-content>
        <v-list-item-title>
          <strong>{{ item.fullName }}</strong>
        </v-list-item-title>
        <v-list-item-subtitle v-html="getSyndicGeneralInfo(item)">
        </v-list-item-subtitle>
      </v-list-item-content>
    </template>
  </v-autocomplete>
</template>


<script>
import Proxyable from 'vuetify/lib/mixins/proxyable';
import { getSyndicFullName, getSyndicGeneralInfo } from '@/utils';
import { apiMetaDataGetSyndics } from '@/api/metaData';
export default {
  components: {  },
  name: 'base-correspondent-autocomplete',
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
    }
  },
  data(){
    return {
      search: null,
      filteredSyndics: [],
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
    items(val){
      for(let i = 0; i < val.length; i++){
        val[i].fullName = getSyndicFullName(val[i]);
      }
      this.$set(this, 'filteredSyndics', val)
    },
    search(val){
      clearTimeout(this.timer)

      let vue = this;
      this.timer = setTimeout(() => {
        vue.searchSyndics(val)
      }, 500)
    }
  },
  methods: {
    searchSyndics(search = ""){
      this.search = search;
      this.paging.page = 1;
      this.loading = true;

      let query = {
        pageNumber: this.paging.page,
        pageSize: this.paging.size,
        searchCriteria: this.search
      }

      apiMetaDataGetSyndics(query).then(result => {
        this.$set(this, "filteredSyndics", result.items.map(item => {
          item.fullName = getSyndicFullName(item);
          return item;
        }))
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
      apiMetaDataGetSyndics(query).then(result => {
        this.$set(this, "filteredSyndics", [...this.filteredSyndics, ...result.items.map(item => {
          item.fullName = getSyndicFullName(item);
          return item;
        })]);
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
    getSyndicFullName: getSyndicFullName,
    getSyndicGeneralInfo: getSyndicGeneralInfo,
    // onInput (evt) {
    //   if (!evt) {
    //     this.model = ''
    //   } else if (typeof evt === 'string') {
    //     this.model = evt
    //   } else if(evt instanceof InputEvent){
    //     this.model = evt.srcElement.value
    //   } else if (typeof e === 'object') {
    //     this.model = evt
    //   }
    // },

  }
}
</script>
