<template>
  <v-container
    id="home"
    tag="section"
    class="full-height"
    fill-height
  >
    <v-row justify="center" align="center" align-content="center">
      <v-col cols="12" lg="6" class="pa-0">
        <svg id="home-logo" class="icon icon-main-form"><use :xlink:href="`${logo}#icon-main-form`"></use></svg>
      </v-col>
      <v-col cols="12" lg="6" class="text-center">
        <h1>{{ $t('ispn_app_name_long') }}</h1>
        <h2>{{ $t('to_the_ministry_of_justice') }}</h2>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import logo_white from "@/assets/symbol-defs.svg";
export default {
  name: "homePage",
  components: {
  },
  data(){
    return {
      data: {
        name: '',
        date: '',
        createdBy: ''
      },
      logo: logo_white,
    }
  },
  computed: {
    ...mapGetters([
      'lastRoute',
      'user',
    ]),
  },
  watch:{
  },
  mounted(){

        
  },
  methods: {
  }
}
</script>

<style lang="sass">
#home-logo
  height: 250px
  width: 100%

.theme--light
  #home-logo
    fill: black

.theme--dark
  #home-logo
    fill: #fff

  
</style>