<template>
  <v-container
    class="error-page full-height pt-10"
    tag="section"
  >
    <v-row
      class="text-center my-5"
    >
      <v-col cols="12" lg="4" offset-lg="4">
        <v-img 
          contain
          width="100%"
          :src="logo"
        ></v-img>
      </v-col>
      <v-col cols="12" class="mt-5">
        <h1 class="error--text">
          Грешка 404
        </h1>

        <h2 class="my-5 error--text">
          Страницата не е намерена
        </h2>

        <v-btn
          :color="$vuetify.theme.isDark ? '' : 'secondary'"
          @click="goToAvailablePath"
        >
          Към Начало
        </v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import logo from "@/assets/logo_ispn_vertical.png"

export default {
  name: 'PagesError',
  data(){
    return {
      routeToGo: {
        name: '',
        path: ''
      },
      logo: logo
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
        if(r.path !== '/' && r.path !== '/login' && r.path != '/*'){
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
  .v-image
    image-rendering: -webkit-optimize-contrast
    max-width: 300px
    display: inline-block
</style>
