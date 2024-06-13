/* eslint-disable vue/valid-template-root */
<template>
  <v-list-item
    v-if="!moduleData.hidden"
    href="javascript:void(0)"
    :class="active ? `primary ${!isDark ? 'black' : 'white'}--text` : `${!isDark ? 'white' : 'black'}--text`"
    @click="setAppModule(moduleData)"
  >
    <v-list-item-content v-if="moduleData.name">
      <v-list-item-title class="pt-1 text-uppercase">
        {{moduleData.name}}
      </v-list-item-title>
    </v-list-item-content>
  </v-list-item>
</template>

<script>
  import Themeable from 'vuetify/lib/mixins/themeable'

  export default {
    name: 'ModuleItem',

    mixins: [Themeable],

    props: {
      moduleData: {
        type: Object,
        default: () => ({}),
      },
      active: {
        type: Boolean,
        default: false
      }
    },

    computed: {
    },
    methods: {
      setAppModule(_module){
        this.$emit('appModuleChanged', _module);
      }
    }
  }
</script>
