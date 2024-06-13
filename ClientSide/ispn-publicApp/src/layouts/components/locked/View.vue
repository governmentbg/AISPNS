<template>
  <v-main
    id="locked"
    :class="$vuetify.theme.light ? undefined : 'grey lighten-3'"
  >
    <v-img
      :class="{
        'v-image--sm': this.$vuetify.breakpoint.smAndDown,
        'v-image--md': this.$vuetify.breakpoint.mdAndDown
      }"
      :src="require(`@/assets/${src || 'blank.jpg'}`)"
      gradient="to top, rgba(76, 175, 80, .0), rgba(76, 175, 80, .0)"
      min-height="100%"
      position="top"
      style="image-rendering: -webkit-optimize-contrast;"
    >
      <v-responsive
        :style="styles"
        min-height="100vh"
        class="d-flex align-center"
      >
        <router-view />
      </v-responsive>
    </v-img>
  </v-main>
</template>

<script>
  export default {
    name: 'LockedAppView',

    data: () => ({
      srcs: {
        '/lock': 'lock.jpg',
        '/login': 'login-background.png',
      },
    }),

    computed: {
      src () {
        return this.srcs[this.$route.path]
      },
      styles () {
        let paddingTop;
        let paddingBottom;
        if(this.$vuetify.breakpoint.xlOnly) {
          paddingTop = 175;
          paddingBottom = 175;
        } else if(this.$vuetify.breakpoint.lgAndUp){
          paddingTop = 120;
          paddingBottom = 50;
        } else {
          paddingTop = 100;
          paddingBottom = 100;
        }

        return {
          padding: `${paddingTop}px 0 ${paddingBottom}px 0`,
        }
      },
    },
  }
</script>
<style lang="sass">
  #locked
    .v-responsive__sizer
      padding-bottom: 0 !important
</style>