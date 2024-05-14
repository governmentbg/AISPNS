<template>
  <v-container
    class="error-page full-height pt-10"
    tag="section"
  >
    <v-row
      class="text-center my-5"
    >
      <v-col cols="12" lg="4" offset-lg="4">
        <svg class="logo">
          <use :xlink:href="`${logo}#icon-main-form`"></use>
        </svg>
      </v-col>
      <v-col cols="12">
        <h1>
          {{ $t('error_404') }}
        </h1>

        <h2 class="my-5">
          {{ $t('the_page_was_not_found') }}
        </h2>

        <v-btn
          :color="$vuetify.theme.isDark ? '' : 'primary'"
          @click="goToAvailablePath"
        >
          {{ $t('go_to_home') }}
        </v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import logoSVG from "@/assets/symbol-defs.svg";
export default {
  name: 'PagesError',
  data(){
    return {
      logo: logoSVG,
      routeToGo: {
        name: '',
        path: ''
      }
    }
  },
  mounted(){
    this.getFirstAvailableRoute();
  },
  methods: {
    goToAvailablePath(){
      this.$router.push({ path: '/' })
    },
    getFirstAvailableRoute(){
      let route;
      for(let r of this.$router.options.routes){
        if(r.path !== '/' && r.path !== '/authentication' && r.path != '/*'){
          route = r;
          break;
        }
      }

      if(route.hidden){
        if(route.children){
          for(let r of route.children){
            if(!r.hidden){
              this.routeToGo = r;
            }
          }
        }
      } else {
        this.routeToGo = route;
      }
    },
  }
}
</script>

<style lang="sass">

.error-page
  // h1
  //   font-size: 8rem

.theme--dark
  .logo
    fill: #fff
</style>
