<template>
  <section class="d-print-none">
    <v-row>
      <v-col md="6" class="py-0">
        <v-btn text @click="goBackToPath" class="no-hover hover-primary px-2" v-if="(goBackTo && goBackText) || previousRouteTitle">
          <v-icon class="mr-3" :ripple="false">mdi-arrow-left-thick</v-icon>
          {{previousRouteTitle}}
        </v-btn>
      </v-col>
      <v-col md="6" class="py-0 text-right">
        <h1
          class="font-weight-light mb-2 headline"
          v-text="heading"
        />
      </v-col>
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
        if(this.lastRoute && this.lastRoute.path && this.lastRoute.meta && this.lastRoute.meta.title){
          return `Обратно към ${this.lastRoute.meta.title}`;
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
