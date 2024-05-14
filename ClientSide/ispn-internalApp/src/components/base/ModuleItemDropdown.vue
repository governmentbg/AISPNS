<template>
  <v-list
    v-model="isOpen"
    :group="group"
    :sub-group="subGroup"
    :value="currentModule.module"
    append-icon="mdi-menu-down"
    color="white"
    class="pp_erp_dropdown_modules"
  >
    <template v-slot:activator>
      <v-list-item-content>
        <v-list-item-title class="pt-1 text-uppercase">
          {{currentModule.name}}
        </v-list-item-title>
      </v-list-item-content>
    </template>

    <template v-for="(_module, i) in modules">
      <base-module-item-app-bar
        :key="`item-${i}`"
        :moduleData="_module"
        :active="currentModule.module === _module.module"
        @appModuleChanged="setAppModule"
      />
    </template>
  </v-list>
</template>

<script>
  // Utilities
  import { mapState } from 'vuex'

  export default {
    name: 'ModuleItemGroup',

    inheritAttrs: false,
    data(){
      return {
        isOpen: false
      }
    },
    props: {
      modules: {
        type: Array,
        default: () => {
          return [];
        }
      },
      currentModule: {
        type: Object,
        default(){
          return {}
        }
      },
      item: {
        type: Object,
        default: () => ({
          avatar: undefined,
          group: undefined,
          name: undefined,
          children: [],
        }),
      },
      subGroup: {
        type: Boolean,
        default: false,
      },
      text: {
        type: Boolean,
        default: false,
      },
    },

    computed: {
      ...mapState(['barColor']),
      hasActiveChildren(){
        return false
      },
      group () {
        return this.genGroup(this.modules)
      },
    },

    methods: {
      genGroup (modules) {
        return modules
          .map(_module => {
            return `${_module.name}`
          }).join('|')
      },
      setAppModule(_module){
        this.$emit("update:currentModule", _module);
        this.isOpen = false;
      }
    },
  }
</script>

<style lang="sass">

.pp_erp_dropdown_modules
  .theme--dark
    .v-list-item
      &.primary
        .v-list-item__content
          .v-list-item__title
            color: #FFF !important
            font-weight: 700 !important
            text-align: left
        
      .v-list-item__content
        .v-list-item__title
          color: #000 !important
          font-weight: 500 !important
          text-align: left

      .v-list-group__header__append-icon
        i
          &.v-icon
            color: #d44438 !important
            font-size: 24px !important

  .theme-light
    .v-list-group__header
      background: white
      color: #d44438 !important
      
      .v-list-item__content
        .v-list-item__title
          font-weight: 700 !important
          text-align: center

      .v-list-group__header__append-icon
        i
          &.v-icon
            color: #d44438 !important
            font-size: 24px !important

  .v-list-group__items
    a
      &.v-list-item
        padding-left: 12px
        div
          font-size: 13px

.v-list-group__activator p 
  margin-bottom: 0


.v-list-group.v-list-group--active 
  background-color: #ffffff1c
  border-radius: 3px

</style>
