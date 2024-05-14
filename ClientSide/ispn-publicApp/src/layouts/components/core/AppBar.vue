<template>
  <div>
    <template v-if="!$vuetify.breakpoint.mobile">
      <div id="desktopHeader">
        <v-system-bar height="30">
          <v-row justify="end">
            <v-btn color="primary" text small to="/sign-in" v-if="false">{{$t('sign_in')}}</v-btn>
            <template v-if="$i18n.availableLocales.length > 2">

            </template>
            <template v-else>
              <template v-for="(lang, idx) in $i18n.availableLocales">
                <v-btn color="primary" :key="idx" text small v-if="lang !== $i18n.locale" @click="changeLanguage(lang)">{{$t(lang)}}</v-btn>
              </template>
            </template>
            <v-btn color="primary" text small @click="openAccessibilityModal">{{$t('font_resizer')}}</v-btn>
          </v-row>
        </v-system-bar>
        <v-row :style="{padding: '0px 25px'}" align="center" justify="start">
          <v-col cols="12" xl="1" lg="2" md="5" class="logo-container">
            <router-link to="/">
              <svg class="logo">
                <use :xlink:href="`${logo}#icon-main-form`"></use>
              </svg>
            </router-link>
          </v-col>
          <v-col cols="12" xl="10" lg="9" md="5">
            <h3 class="mt-1">{{$t('ispn_app_name_short').toUpperCase()}}</h3>
            <h4>{{$t('ministry_of_justice')}}</h4>
          </v-col>
          
        </v-row>
        <v-app-bar
          id="app-bar"
          height="60"
        >
          <template v-for="(menu, idx) in menus">
            <base-main-menu-item :menu="menu" :key="idx" />
          </template>
          <v-btn 
            color="white"
            text
            :ripple="false"
            class="sign-in"
            href="https://external-aistn.mjs.bg/login"
          >
            <v-icon class="mr-2">mdi-login</v-icon>
            {{$t("sign_in")}}
          </v-btn>
        </v-app-bar>
      </div>
    </template>
    <template v-else>
      <div id="mobileHeader">
        <v-system-bar height="30">
          <v-row justify="end">
            <template v-if="$i18n.availableLocales.length > 2">

            </template>
            <template v-else>
              <template v-for="(lang, idx) in $i18n.availableLocales">
                <v-btn :color="$vuetify.theme.isDark ? '' : 'primary'" :key="idx" text small v-if="lang !== $i18n.locale" @click="changeLanguage(lang)">{{$t(lang)}}</v-btn>
              </template>
            </template>
            <v-btn :color="$vuetify.theme.isDark ? '' : 'primary'" text small @click="openAccessibilityModal">{{$t('font_resizer')}}:</v-btn>
          </v-row>
        </v-system-bar>
        <v-row :style="{padding: '0 15px'}" align="center" justify="start">
          <v-col cols="3">
            <svg class="logo">
              <use :xlink:href="`${logo}#icon-main-form`"></use>
            </svg>
          </v-col>
          <v-col cols="7">
            <h3 class="mt-1">{{$t('ispn_app_name_short').toUpperCase()}}</h3>
            <h4>{{$t('ministry_of_justice')}}</h4>
          </v-col>
          <v-col cols="2">
            <v-btn
              small
              :color="$vuetify.theme.isDark ? '' : 'primary'"
              :style="{height: '40px'}"
              @click="drawer = !drawer"
            >
              <v-icon dense>mdi-menu</v-icon>
            </v-btn>

            <v-navigation-drawer
              id="core-navigation-drawer"
              v-model="drawer"
              :dark="barColor !== 'rgba(228, 226, 226, 1), rgba(255, 255, 255, 0.7)'"
              :expand-on-hover="expandOnHover"
              :right="$vuetify.rtl"
              :color="$vuetify.theme.isDark ? '' : 'primary'"
              mobile-breakpoint="1200"
              app
              mini-variant-width="0"
              width="340"
              v-bind="$attrs"
            >
              <template v-slot:img="props">
                <v-img
                  :gradient="`to bottom, ${barColor}`"
                  v-bind="props"
                />
              </template>

              <v-list-item two-line :class="`px-2 ${$vuetify.theme.isDark ? 'grey darken-4' : 'white'}`">
                <v-list-item-content>
                  <v-list-item-title class="font-weight-regular display-2 my-1">
                    <v-row>
                      <v-col md="12" class="py-0 px-4">
                        <span class="logo-normal">
                          <v-row>
                            <v-col cols="4">
                              <svg class="logo">
                                <use :xlink:href="`${logo}#icon-main-form`"></use>
                              </svg>
                            </v-col>
                            <v-col cols="8" align-self="center">
                              <h3 id="mobileMenu-ispn" class="mt-3">{{$t('ispn_app_name_short').toUpperCase()}}</h3>
                              <h4 id="mobileMenu-bulgaria">{{$t('ministry_of_justice')}}</h4>
                            </v-col>
                          </v-row>
                        </span>
                      </v-col>
                    </v-row>
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

              <v-divider class="mb-2" />

              <v-list
                expand
                nav
              >
                <template v-for="(item, i) in menus">
                  <template v-if="!item.hidden">
                    
                    <v-divider class="my-5" :key="`group1-${i}`" v-if="item.meta && item.meta.divider" />

                    <base-item-group
                      v-if="item.children && item.children.length"
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
                  <base-item
                    v-else
                    :key="`item-${i}`"
                    :item="item"
                  />
                </template>
                <div />
              </v-list>
              <v-divider class="my-5" />
            </v-navigation-drawer>
          </v-col>
        </v-row>
      </div>
    </template>
  </div>
</template>

<script>
// Utilities
import { mapGetters, mapState } from 'vuex'
//import i18n from '@/plugins/i18n';
import { setCookie } from '@/utils/auth';
import logoSVG from "@/assets/symbol-defs.svg";
export default {
  name: "CoreAppBar",
  props: {
    value: {
      type: Boolean,
      default: false
    },
    expandOnHover: {
      type: Boolean,
      default: false,
    },
  },
  data(){
    return {
      logo: logoSVG,
      drawer: false,
      menus: [],
      defaultFontSize: 15
    }
  },
  mounted(){
    this.menus = [
      {
        name: this.$t('bankruptcy'),
        children: [
          {
            name: this.$t('search_by_case'),
            path: '/cases/bankruptcy/by-case'
          },
          {
            name: this.$t('search_by_side'),
            path: "/cases/bankruptcy/by-side",
          },
        ]
      },
      {
        name: this.$t('stabilization'),
        children: [
          {
            name: this.$t('search_by_case'),
            path: '/cases/stabilization/by-case',
          },
          {
            name: this.$t('search_by_side'),
            path: "/cases/stabilization/by-side",
          },
        ]
      },
      {
        name: this.$t('syndics'),
        path: "/syndics"
      },
      {
        name: this.$t('entrepreneurs'),
        children: [
          {
            name: this.$t('bankruptcy'),
            path: '/entrepreneurs/bankruptcy',
          },
          {
            name: this.$t('stabilization'),
            path: "/entrepreneurs/stabilization",
          },
        ]
      },
      {
        name: this.$t('sell_announcements'),
        path: "/sell-announcements",
      },

      {
        name: this.$t('statistics_and_reports'),
        path: "/statistics-and-reports"
      },
      {
        name: this.$t("templates"),
        path: "/templates"
      },
      {
        name: this.$t("legal_basis"),
        path: "/legal-basis"
      },
    ]
    for (const sheet of document.styleSheets) {
      try {
        for (const rule of sheet.cssRules) {
          if (rule.type === CSSRule.STYLE_RULE) {
            // Support for recursing into other rule types, like media queries, left as an exercise for the reader
            let { fontSize } = rule.style;
            // eslint-disable-next-line no-unused-vars
            let [all, number, unit] = fontSize.match(/^([\d.]+)(\D+)$/) || [];
            if (unit === "px") {
              // Other units left as an exercise to the reader
              rule.style.fontSize = `${number / 16}rem`
            }
          }
        }
      // eslint-disable-next-line no-empty
      } catch(e){ }
    }
    
    this.changeFontSize(this.accessibility.font.size * this.defaultFontSize);
    this.changeTheme(this.accessibility.theme.type);
    

    
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
  computed: {
    ...mapState(['barColor', ]),
    //...mapGetters(['user', 'drawer'])
    ...mapGetters(['user', 'accessibility']),
    fontSize(){
      return this.accessibility.font.size;
    },
    theme(){
      return this.accessibility.theme.type;
    }
  },
  watch: {
    fontSize(val){
      this.changeFontSize(val * this.defaultFontSize)
    },
    theme(val){
      this.changeTheme(val);
    }
  },
  methods: {
    changeLanguage(lang){
      if(lang){
        setCookie("_locale", lang);
        // eslint-disable-next-line no-self-assign
        window.location.reload();
      }
    },
    changeFontSize(new_size){
      document.documentElement.style=`font-size: ${new_size}px`;
    },
    changeTheme(theme){
      switch(theme){
        case 'light':
          this.$vuetify.theme.dark = false;
          this.$vuetify.theme.themes.light.primary = '#8a6948'
        break;
        case 'dark':
          this.$vuetify.theme.dark = true;
          this.$vuetify.theme.themes.dark.primary = '#FFFFFF'
        break;
        case 'colorBlind':
          
        break;
      }

      this.$forceUpdate();
    },
    signOut(){
      this.$store.dispatch('user/signout').then(() => {
        this.$router.push({path: '/login'})
      });
    },
    openAccessibilityModal(){
      this.$store.dispatch("app/toggleAccessibilityModal")
    }
  }
}
</script>

<style lang="sass">
#desktopHeader
  .logo-container
    margin-right: 30px
    .logo
      width: 100%
      fill: #000

  h3 
    color: #4c4c4c
    font-weight: 600
    margin-bottom: 0
    font-size: 1.8rem
    //text-transform: uppercase
  h4
    color: #4c4c4c
    //text-transform: uppercase
    font-weight: 500

.theme--dark
  #desktopHeader
    h3 
      color: #FFF
    h4 
      color: #FFF
    .logo
      fill: #000

#mobileHeader
  h2 
    font-size: 18px
    font-weight: 800
  h3 
    font-size: 16px
    
  h4
    font-size: 13px !important
    margin-bottom: 5px
    color: #4c4c4c

  .logo
    width: 100%
    max-height: 80px
    fill: $primary

  .logo-normal
    h3
      color: #4c4c4c
      font-size: 16px !important
      white-space: initial
      line-height: 1.6
    
    h4
      font-size: 13px !important
      margin-bottom: 5px
      color: #4c4c4c

header
  .v-btn
    .v-btn__content
      font-size: 15px

#core-navigation-drawer
  &.v-navigation-drawer--mini-variant
    .v-list-item
      justify-content: flex-start !important

    .v-list-group--sub-group
      display: block !important

  .v-list-group__header.v-list-item--active:before
    opacity: .24

  .v-list-item
    &__icon--text,
    &__icon:first-child
      justify-content: center
      text-align: center
      width: 20px

      @mixin ltr
        margin-right: 24px
        margin-left: 12px !important

      @mixin rtl
        margin-left: 24px
        margin-right: 12px !important

  .v-list--dense
    .v-list-item
      &__icon--text,
      &__icon:first-child
        margin-top: 10px

  .v-list-group--sub-group
    .v-list-item
      @mixin ltr
        padding-left: 8px

      @mixin rtl
        padding-right: 8px

    .v-list-group__header
      @mixin ltr
        padding-right: 0

      @mixin rtl
        padding-right: 0

      .v-list-item__icon--text
        margin-top: 19px
        order: 0

      .v-list-group__header__prepend-icon
        order: 0

        @mixin ltr
          margin-right: 8px

        @mixin rtl
          margin-left: 8px

.sign-in
  position: absolute !important
  right:0 !important
</style>