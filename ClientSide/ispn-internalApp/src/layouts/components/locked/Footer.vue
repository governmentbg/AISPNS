<template>
  <v-footer
    id="pages-core-footer"
    color="#eff2f9"
    dark
    style="z-index: 1 !important;"
  >
    <v-container>
      <v-row
        align="center"
        no-gutters
      >
        <v-col
          v-for="(link, i) in links"
          :key="i"
          class="text-center"
          cols="6"
          md="auto"
        >
          <a
            :href="link.href"
            class="mr-0"
            rel="noopener"
            target="_blank"
            v-text="link.text"
          />
        </v-col>

        <v-spacer class="hidden-sm-and-down" />

        <v-col
          cols="12"
          md="auto"
        >
          <div class="body-1 font-weight-light pt-3 pt-md-0 text-center secondary--text text--darken-2">
            &copy; {{year}}, {{title}}
          </div>
        </v-col>
      </v-row>
    </v-container>
  </v-footer>
</template>

<script>
  import config from "@/config";
  export default {
    name: 'LockedAppFooter',

    data: () => ({
      links: [],
      title: config.title
    }),
    computed: {
      year(){
        let year = (new Date).getFullYear();
        if(year == "2023") return "2023"

        return "2023 - "+year;
      }
    }
  }
</script>

<style lang="sass">
  #pages-core-footer
    padding: 10px 0 10px 4px
    
    a
      color: #FFFFFF
      font-size: .825rem
      font-weight: 500
      text-decoration: none
      text-transform: uppercase
</style>
