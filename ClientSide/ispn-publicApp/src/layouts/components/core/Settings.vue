<template>
  <div id="settings-wrapper">
    <v-tooltip top :color="$vuetify.theme.isDark ? 'grey darken-4' : 'primary'" class="lighten-2 mr-15">
      <template v-slot:activator="{ on, attrs }">
        <v-card
          id="settings"
          class="py-2 px-4"
          :color="$vuetify.theme.isDark ? '' : 'primary lighten-2 white--text'"
          dark
          flat
          link
          v-bind="attrs"
          v-on="on"
          min-width="100"
          style="position: fixed; top: 255px; right: -35px; border-radius: 8px;"
        >
          <v-icon 
            large
          >
            mdi-cogs
          </v-icon>
        </v-card>
      </template>
      <span>НАСТРОЙКИ</span>
    </v-tooltip>

    <v-menu
      v-model="menu"
      :close-on-content-click="false"
      activator="#settings"
      bottom
      content-class="v-settings"
      left
      nudge-left="8"
      offset-x
      origin="top right"
      transition="scale-transition"
    >
      <v-card
        class="text-center mb-0"
        width="300"
      >
        <v-card-text>
          
          <strong class="mb-3 d-inline-block">SIDEBAR КОЛОНИ</strong>
          <v-row
              align="center"
              no-gutters
            >

              <v-spacer />

              <v-col cols="12">
                <v-btn-toggle v-model="sidebarColumnsSize">
                  <v-btn :value="3" :class="sidebarColumnsSize === 3 ? 'primary white--text' : ''">
                    3
                  </v-btn>

                  <v-btn :value="4" :class="sidebarColumnsSize === 4 ? 'primary white--text' : ''">
                    4
                  </v-btn>

                  <v-btn :value="5" :class="sidebarColumnsSize === 5 ? 'primary white--text' : ''">
                    5
                  </v-btn>
                </v-btn-toggle>
              </v-col>
            </v-row>

            <v-divider class="my-4 secondary" />
        </v-card-text>
      </v-card>
    </v-menu>
  </div>
</template>

<script>
  // Mixins
  import Proxyable from 'vuetify/lib/mixins/proxyable'
  import { mapGetters } from 'vuex'

  export default {
    name: 'DashboardCoreSettings',

    mixins: [Proxyable],

    data: () => ({
      color: '#E91E63',
      colors: [
        '#9C27b0',
        '#00CAE3',
        '#4CAF50',
        '#ff9800',
        '#E91E63',
        '#FF5252',
      ],
      menu: false,
      saveImage: '',
      scrim: 'rgba(0, 0, 0, .7), rgba(0, 0, 0, .7)',
      scrims: [
        'rgba(0, 0, 0, .7), rgba(0, 0, 0, .7)',
        'rgba(228, 226, 226, 1), rgba(255, 255, 255, 0.7)',
        'rgba(244, 67, 54, .8), rgba(244, 67, 54, .8)',
      ],
      showImg: true,
    }),

    computed: {
      ...mapGetters(['sidebarColumns']),
      sidebarColumnsSize : {
        get: function(){
          return this.sidebarColumns;
        },
        set: function(val){
          this.$store.dispatch('app/setSidebarColumns', val)
        }
      }
    },

    watch: {
      color (val) {
        this.$vuetify.theme.themes[this.isDark ? 'dark' : 'light'].primary = val
      },
    },

    methods: {
    },
  }
</script>

<style lang="sass">
  .v-settings
    .v-item-group > *
      cursor: pointer

    &__item
      border-width: 3px
      border-style: solid
      border-color: transparent !important

      &--active
        border-color: #00cae3 !important
</style>
