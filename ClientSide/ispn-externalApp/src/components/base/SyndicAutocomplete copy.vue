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
      if(val){
        this.filteredSyndics = [];
        
        this.items.filter((item) => {
          if(item.fullName.toLowerCase().includes(val.toLowerCase())){
            this.filteredSyndics.push(item)
          }
        })
      }
    }
  },
  methods: {
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
