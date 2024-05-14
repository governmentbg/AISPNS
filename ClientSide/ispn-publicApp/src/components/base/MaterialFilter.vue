<template>
  <v-card 
    class="px-5 v-card--filter"
    :flat="flat"
    :class="[{
      'v-card--has-filter': hasFilters,
      'mb-15': marginBottom
    }, cardClass]"
  >
    <div class="pt-6" v-if="header">
      <v-icon
        class="d-inline-block"
        color="primary"
        x-large
      >
        mdi-filter-variant
      </v-icon>
      <h2
        class="filter-title fw400"
        v-text="title"
      />
    </div>

    <v-row class='mt-5'>
      <slot />
    </v-row>
      <v-btn
        :title="$t('search')" 
        :fab="fab"
        :class="`${$vuetify.theme.isDark ? '' : 'primary'} search elevation-12`"
        @click="doFilter()"
      >
        <v-icon size="30">mdi-magnify</v-icon>
      </v-btn>

      <v-btn
        :title="$t('remove_filters')" 
        :fab="fab"
        :class="`grey darken-2 cancel_search elevation-12`"
        @click="$emit('remove-filter')"
        v-if="hasFilters"
      >
        <v-icon size="30">mdi-close</v-icon>
      </v-btn>

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
      },
      fab: {
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
    padding-bottom: 90px
    margin-bottom: 64px

    h2
      display: inline-block
      margin-left: 10px

    .v-btn.search
      position: absolute
      left: calc(50% - 50px)
      top: calc(100% - 54px)
  

  .v-card--filter.v-card--has-filter
    .v-btn.search
      left: calc(50% - 125px)

    .v-btn.cancel_search
      position: absolute
      left: calc(50% - -30px)
      top: calc(100% - 54px)
      color: white
</style>
