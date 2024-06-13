<template>
  <v-footer
    id="ispn-core-footer"
    class="pt-0"
    style="min-height:100px;"
  >
    <v-container class="pb-0">
      <v-row
        align="center"
        no-gutters
      >
        <v-col
          v-for="(link, i) in links"
          :key="i"
          class="text-center"
          cols="auto"
        >
          <v-btn
            :to="link.path"
            link
            text
            plain
            :class="`mr-0 white--text px-5 ${$vuetify.theme.isDark ? '' : 'text--darken-3'}`"
            rel="noopener"
          >
            {{link.name}}
          </v-btn>
        </v-col>

        <v-spacer class="hidden-sm-and-down" />

        <v-col
          id="ispn-core-organization"
          cols="12"
          class="mt-10"
        >
          <div class="body-1 font-weight-light pt-6 pt-md-0 text-center">
            &copy; {{$t('ispn_to_ministry_of_justice')}} {{year}}
          </div>
        </v-col>
      </v-row>
    </v-container>
  </v-footer>
</template>

<script>
export default {
  name: 'CoreAppFooter',
  data: () => ({
    links: []
  }),
  mounted(){
    this.links.push(
      
    )
  },
  computed: {
    year(){
      let year = (new Date).getFullYear();
      
      return +year;
    }
  }
}
</script>

<style lang="sass">
#ispn-core-footer
  a
    font-size: .825rem
    font-weight: 500
    text-decoration: none
    text-transform: uppercase
    color: white !important

  #ispn-core-organization
    .body-1
      color: white !important
</style>
