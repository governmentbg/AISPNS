<template>
  <v-navigation-drawer
    id="core-navigation-drawer"
    v-model="drawer"
    :dark="barColor !== 'rgba(228, 226, 226, 1), rgba(255, 255, 255, 0.7)'"
    :expand-on-hover="expandOnHover"
    :right="$vuetify.rtl"
    :src="barImage"
    color="secondary"
    mobile-breakpoint="960"
    app
    mini-variant-width="0"
    width="240"
    height="100%"
    v-bind="$attrs"
  >
    <template v-slot:img="props">
      <v-img
        :gradient="`to bottom, ${barColor}`"
        v-bind="props"
      />
    </template>

    <v-list-item class="px-1 white" style="height: 60px;">
      <v-list-item-content>
        <v-list-item-title class="text-uppercase font-weight-regular display-2 my-0">
          <span class="logo-normal">
            <v-img 
              contain
              height="90"
              style="image-rendering: -webkit-optimize-contrast;background-position: initial;"
              :src="logo"
            ></v-img>
          </span>
        </v-list-item-title>
      </v-list-item-content>
    </v-list-item>

    <v-divider class="mb-2" />

    <v-list
      expand
      nav
    >
      <div />

      <template v-for="(item, i) in permissionRoutes">
        <template v-if="!item.hidden">

          <v-divider class="my-5" :key="`group1-${i}`" v-if="item.meta && item.meta.divider" />          
          <base-item-group
            v-if="item.children"
            :key="`group-${i}`"
            :item="item"
          >
          </base-item-group>
          <base-item
            v-else
            :key="`item-${i}`"
            :item="item"
          />
        </template>
        <template v-else>
          <base-item
            :key="`item-${i}`"
            :item="item"
            :shownBadgesPaths="['/messages']"
            :badges="badges"
          />
        </template>
      </template>
      <div />
    </v-list>
    <v-divider class="my-5" />
    <div :class="`version${isProduction ? '' : ' dev'}`">
      Версия: {{(!isProduction ? 'ТЕСТОВА СРЕДА ' : '') + version}}
    </div>
  </v-navigation-drawer>
</template>

<script>
import {
  mapState,
  mapGetters
} from 'vuex'
import config from "@/config";
import logo from "@/assets/logo_ispn.png";
import { getToken } from '@/utils/auth';
import { apiGetUnreadMessages } from '@/api/messages';

export default {
  name: "CoreAppDrawer",
  props: {
    expandOnHover: {
      type: Boolean,
      default: false,
    },
  },
  data(){
    return {
      version: config.version,
      logo: logo,
      unreadMessagesInterval: null,
      badges: {
        unreadMessagesCount: 0,
      }
    }
  },
  computed: {
    ...mapState(['barColor', 'barImage']),
    ...mapGetters([
      'permissionRoutes',
      'isProduction',
      'token'
    ]),
    drawer: {
      get () {
        return this.$store.state.app.drawer
      },
      set (val) {
        this.$store.commit('app/SET_DRAWER', val)
      },
    }
  },
  watch: {
    // '$vuetify.breakpoint.smAndDown' (val) {
    //   this.$emit('update:expandOnHover', !val)
    // }
  },
  mounted(){
    this.getUserUnreadMessages();
    this.unreadMessagesInterval = setInterval(this.getUserUnreadMessages, 60000);
  },
  methods: {
    getUserUnreadMessages(){
      if (this.token || getToken()) {
        apiGetUnreadMessages().then(result => {
          if(typeof result === "number")
            this.$set(this.badges, "unreadMessagesCount", result);
        })
      } else {
        clearInterval(this.unreadMessagesInterval);
      }
    }
  }
}
</script>

<style lang="sass">
.version
  &.dev
    background: #efd629    
    color: $secondary

#core-navigation-drawer
  &.v-navigation-drawer--mini-variant
    .v-list-item
      justify-content: flex-start !important

    .v-list-group--sub-group
      display: block !important

  
    // .logo-normal
    //   .v-image__image
    //     height: 97% !important
    //     background-position: auto

  
  // .logo-normal
  //   .v-image__image
  //     background-size: auto !important

  .v-list-group__header.v-list-item--active:before
    opacity: .24

  .v-list-item
    min-height: 40px

    &__icon--text,
    &__icon:first-child
      justify-content: center
      text-align: center
      width: 20px

      // +ltr()
      //   margin-right: 24px
      //   margin-left: 12px !important

      // +rtl()
      //   margin-left: 24px
      //   margin-right: 12px !important

  .v-list--dense
    .v-list-item
      &__icon--text,
      &__icon:first-child
        margin-top: 10px

      min-height: 33px

  .v-list-group--sub-group
    .v-list-item
      // +ltr()
      //   padding-left: 8px

      // +rtl()
      //  padding-right: 8px

    .v-list-group__header
      // +ltr()
      //   padding-right: 0

      // +rtl()
      //   padding-right: 0

      .v-list-item__icon--text
        margin-top: 19px
        order: 0

      .v-list-group__header__prepend-icon
        order: 2


        // +ltr()
        //   margin-right: 8px

        // +rtl()
        //   margin-left: 8px
</style>
