<template>
  <v-app>
    <pages-core-view />
    
    <vue-snotify></vue-snotify>

    <pages-core-footer />
  </v-app>
</template>

<script>
export default {
  name: 'LockedLayout',

  components: {
    PagesCoreAppBar: () => import('./components/locked/AppBar.vue'),
    PagesCoreFooter: () => import('./components/locked/Footer.vue'),
    PagesCoreView: () => import('./components/locked/View.vue'),
  },

  created () {
    this.$vuetify.theme.dark = true
  },

  beforeDestroy () {
    this.$vuetify.theme.dark = false
  },
}
</script>
