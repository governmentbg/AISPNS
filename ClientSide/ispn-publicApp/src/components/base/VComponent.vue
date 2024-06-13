<template>
  <section class="mb-4 mt-4 d-print-none">
    <v-row>
      
    </v-row>
  </section>
</template>

<script>
import { mapGetters } from 'vuex'
  export default {
    name: 'VComponent',

    props: {
      heading: {
        type: String,
        default: '',
      },
      goBackText: {
        type: String,
        default: '',
      },
      goBackTo: {
        type: String,
        default: '',
      },
    },
    computed: {
      ...mapGetters([
        'lastRoute'
      ]),
      previousRouteTitle() {
        if(this.lastRoute && this.lastRoute.path && this.lastRoute.name){
          return `Обратно към ${this.lastRoute.name}`;
        } else if(this.goBackText){
          return this.goBackText
        }

        return '';
        //return this.lastRoute && this.lastRoute.meta && this.lastRoute.meta.goBackTitle && this.lastRoute.path !== '/' ? this.lastRoute.meta.goBackTitle : this.goBackText;
      },
    },
    methods: {
      goBackToPath(){
        //if(this.lastRoute && this.lastRoute.meta && this.lastRoute.meta.goBackTitle && this.lastRoute.path !== '/'){
        if(this.lastRoute && this.lastRoute.path) {
          this.$store.dispatch("permission/goBack");
        } else if(this.goBackTo){
          this.$router.push({path: this.goBackTo})
        }
      }
    }
  }
</script>
