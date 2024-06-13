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
      :src="background"
      gradient="to top, rgba(76, 175, 80, .0), rgba(76, 175, 80, .0)"
      min-height="100%"
      position="top"
      style="image-rendering: -webkit-optimize-contrast;"
    >
      <v-responsive
        :style="styles"
        class="d-flex align-center"
      >
        <router-view />
      </v-responsive>
    </v-img>
  </v-main>
</template>

<script>
import backgroundImg from "@/assets/login-background.png";
import blank from "@/assets/blank.jpg";

export default {
  name: 'PP_MP_LockedAppView',

  data: () => ({
    background: backgroundImg,
    blank: blank
  }),

  computed: {
    src () {
      if(this.$route.path === "/lock"){
        return this.backgroundImg;
      } else if(this.$route.path === "/login"){
        return this.backgroundImg;
      }

      return this.blank;
    },
    styles () {
      let paddingTop;
      let paddingBottom;
      let minHeight;
      if(this.$vuetify.breakpoint.xlOnly) {
        minHeight = 80;
        paddingTop = 190;
        paddingBottom = 0;
      } else if(this.$vuetify.breakpoint.lgAndUp){
        minHeight = 40;
        paddingTop = 110;
        paddingBottom = 0;
      } else {
        minHeight = 30;
        paddingTop = 100;
        paddingBottom = 0;
      }

      return {
        minHeight: `${minHeight}vh`,
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