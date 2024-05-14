<template>
  <v-app>
    <pages-core-app-bar />

    <pages-core-view />
    
    <vue-snotify v-if="false"></vue-snotify>

    <pages-core-footer />
  </v-app>
</template>

<script>
  export default {
    name: 'LockedLayout',

    components: {
      PagesCoreAppBar: () => import('./components/locked/AppBar'),
      PagesCoreFooter: () => import('./components/locked/Footer'),
      PagesCoreView: () => import('./components/locked/View'),
    },

    created () {
      this.$vuetify.theme.dark = true
    },

    beforeDestroy () {
      this.$vuetify.theme.dark = false
    },
  }
</script>
