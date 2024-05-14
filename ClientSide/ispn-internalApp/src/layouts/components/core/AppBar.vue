<template>
  <v-app-bar
    id="app-bar"
    absolute
    app
    color="primary"
    flat
    height="60"
  >
    <v-btn
      class="mr-3 white--text"
      text
      small
      @click="$vuetify.breakpoint.smAndDown ? setDrawer(!drawer) : setDrawer(!drawer)"
    >
      <v-icon v-if="drawer">
        mdi-arrow-collapse-left
      </v-icon>

      <v-icon v-else>
        mdi-arrow-collapse-right
      </v-icon>
    </v-btn>

    <v-toolbar-title
      v-if="drawer"
      class="hidden-sm-and-down font-weight-light white--text pl-5"
    >
      {{$route.meta.title}}
    </v-toolbar-title>

    <v-spacer />

    <div class="mx-3" />

    <v-btn 
      class="ml-2 white--text"
      min-width="0"
      text @click="openAccessibilityModal"
    >
      <v-icon>mdi-format-size</v-icon>
    </v-btn>

    <v-menu
      bottom
      left
      min-width="200"
      offset-y
      origin="top right"
      transition="scale-transition"
      :close-on-click="false"
    >
      <template v-slot:activator="{ attrs, on }">
        <v-btn
          class="ml-2 white--text"
          min-width="0"
          text
          v-bind="attrs"
          v-on="on"
        >
          <v-icon>mdi-account</v-icon>
          <span class="ml-2">{{ user.name }}</span>
        </v-btn>
      </template>

      <v-list 
        dense
      >
        <template v-for="(p, i) in profileMenu">
          <v-divider
            v-if="p.divider"
            :key="`divider-${i}`"
            class="mb-2 mt-2"
          />

          <v-list-item
            v-else
            :key="`item-${i}`"
            @click="p.method"
            link
          >
            <v-list-item-title>{{p.title}}</v-list-item-title>
          </v-list-item>
        </template>
      </v-list>
    </v-menu>
  </v-app-bar>
</template>

<script>
// Utilities
import { mapGetters } from 'vuex'
import { formatDate } from '@progress/kendo-intl';
import { getToken } from '@/utils/auth';


export default {
  name: "CoreAppBar",
  props: {
    value: {
      type: Boolean,
      default: false
    }
  },
  data(){
    return {
      profileMenu: [
        { title: 'Изход', method: this.signOut },
      ],
      isExpanded: false,
      notificationsInterval: null,
      notifications: {
        data: [],
        pagination: {
          itemsPerPage: 10,
          page: 1,
          total: 0
        }
      },
      defaultFontSize: 17
    }
  },
  computed: {
    ...mapGetters(['user', 'drawer', 'permissionRoutes', 'token', 'accessibility']),
    fontSize(){
      return this.accessibility.font.size;
    },
  },
  watch:{
    fontSize(val){
      this.changeFontSize(val * this.defaultFontSize)
    },
  },
  mounted(){
    this.changeFontSize(this.accessibility.font.size * this.defaultFontSize);
    // if(!this.$vuetify.breakpoint.smAndDown){
    //   const vue = this;
    //   setTimeout(() => {
    //     vue.$store.state.app.drawer = true;
    //     vue.$forceUpdate();
    //   }, 0)
    // } else {
    //   const vue = this;
    //   setTimeout(() => {
    //     vue.$store.state.app.drawer = false;
    //     vue.$forceUpdate();
    //   }, 0)
    // }
  },
  methods: {
    setDrawer(val){
      this.$store.commit('app/SET_DRAWER', val)
    },
    redirectUser(routeModule){
      this.$router.push({path: routeModule.children[0].path})
    },
    signOut(){
      this.$store.dispatch('user/signout').then((result) => {
        if(result)
          this.$router.push({path: '/login'})
      });
    },
    getActivePath(checkPath){
      let result = false;
      for(let matchedPath of this.$route.matched){
        if(matchedPath.path.indexOf(checkPath) > -1)
          result = true;
      }

      return result;
    },
    openAccessibilityModal(){
      this.$store.dispatch("app/toggleAccessibilityModal")
    },
    changeFontSize(new_size){
      document.documentElement.style=`font-size: ${new_size}px`;
    },
    formatDate: formatDate
  }
}
</script>

<style lang="sass">
.loadMoreNotifications
  min-height: 39px !important
  height: 39px !important
</style>