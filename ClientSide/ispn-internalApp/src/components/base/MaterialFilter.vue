<template>
  <v-card 
    class="px-5 v-card--filter"
    :flat="flat"
    :class="[{
      'v-card--has-filter': hasFilters,
      'mb-15': marginBottom
    }, cardClass]"
  >
    <div class="pt-2" v-if="header">
      <v-icon
        color="primary"
        large
      >
        mdi-filter-variant
      </v-icon>
      <h2
        class="display-2 font-weight-light"
        v-text="title"
      />
    </div>

    <v-row class='mt-5'>
      <slot />
    </v-row>

    <v-tooltip left>
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          v-bind="attrs"
          v-on="on"
          title="Приложи филтрите" 
          fab 
          class="primary elevation-12 doFilter"
          @click="doFilter()"
        >
          <v-icon size="30">mdi-magnify</v-icon>
        </v-btn>
      </template>
      <span>Приложи филтрите</span>
    </v-tooltip>

    <v-tooltip right v-if="hasFilters">
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          v-bind="attrs"
          v-on="on"
          title="Премахни филтрите" 
          fab 
          class="secondary elevation-12 clearFilters"
          @click="$emit('remove-filter')"
        >
          <v-icon size="30">mdi-close</v-icon>
        </v-btn>
      </template>
      <span>Изчисти филтрите</span>
    </v-tooltip>

  </v-card>
</template>

<script>
  export default {
    name: 'BaseMaterialFilter',

    props: {
      title: {
        type: String,
        default: ''
      },
      hasFilters: {
        type: Boolean,
        default: false
      },
      cardClass: {
        type: String,
        default: ''
      },
      flat: {
        type: Boolean,
        default: false
      },
      header: {
        type: Boolean,
        default: true
      },
      doFilterWithEmptyFields: {
        type: Boolean,
        default: false
      },
      marginBottom: {
        type: Boolean,
        default: false
      }
    },
    methods: {
      doFilter(){
        this.$emit('do-filter', this.doFilterWithEmptyFields)
      }
    }
  }
</script>

<style lang="sass">
  .v-card--filter
    padding-bottom: 70px
    margin-bottom: 64px

    h2
      display: inline-block
      margin-left: 10px

    .v-btn.primary
      position: absolute
      left: calc(50% - 50px)
      top: calc(104% - 54px)
  

  .v-card--filter.v-card--has-filter
    .v-btn.primary
      left: calc(50% - 125px)

    .v-btn.secondary
      position: absolute
      left: calc(50% - -25px)
      top: calc(104% - 54px)
</style>
