<template>
  <v-card
    v-bind="$attrs"
    :class="classes"
    class="v-card--material pa-3"
    :min-height="minHeight"
    :dark="$vuetify.theme.isDark"
  >
    <div class="d-flex grow flex-wrap">
      <v-avatar
        v-if="avatar"
        :size="avatarSize"
        class="mx-auto v-card--material__avatar elevation-6"
        color="white"

      >
        <v-img :src="avatar" :position="avatarPosition"  />
      </v-avatar>

      <v-sheet
        v-else-if="icon || $slots.heading"
        :class="{
          'cardIconHolder': icon && icon.length ? true : false,
          'px-4 py-2 mb-n3': smIcon,
          'pa-4': !smIcon,
          'white-header': !$vuetify.theme.isDark ? whiteHeader : false
          //'pa-7': !$slots.image
        }"
        :color="$vuetify.theme.isDark ? '' : color"
        outlined
        :max-height="icon ? 90 : undefined"
        :width="inline || icon ? 'auto' : '100%'"
        elevation="6"
        class="text-start v-card--material__heading text--white d-print-none"
      >
        <slot
          v-if="$slots.heading"
          name="heading"
        />

        <slot
          v-else-if="$slots.image"
          name="image"
        />

        <slot
          v-else-if="$slots.svg"
          name="svg"
        />

        <h4
          v-else-if="title && !icon"
          class="display-1 font-weight-light text-uppercase"
          v-text="title"
        />

        <template v-else-if="icon">
          <v-icon
            v-if="!icon.includes('svg')"
            size="32"
          >
            {{ icon }}
          </v-icon>
          <v-img v-else width="40" :src="require(`@/assets/${icon}`)" class="mt-2" :style="(icon.includes('.svg') ? 'height: 15px;' : '')" />
        </template>

        <div
          v-if="text"
          class="headline font-weight-thin"
          v-text="text"
        />
      </v-sheet>



      <v-row v-if="($slots['after-heading'] || $slots['after-heading-button']) && !print" :class="!fullTitle ? 'ml-3' : ''">
        <template v-if="fullTitle">
          <v-col cols="12" class="cardTitle display-2 font-weight-light">
            <slot name="after-heading" />
          </v-col>
        </template>
        <template v-else>
          <v-col cols="12" :lg="expandable || $slots['after-heading-button'] ? 6 : 12" :md="expandable || $slots['after-heading-button'] ? 7 : 12" class="cardTitle display-2 font-weight-light">
            <slot name="after-heading" />
          </v-col>
          <v-col cols="12" lg="6" md="5" class="buttonContainer text-right" v-if="expandable || $slots['after-heading-button']">
            <template v-if="expandable">
              <template v-if="areAllCardsExpanded != null">
                <v-tooltip top v-if="areAllCardsExpanded" color="primary">
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn 
                      icon
                      v-bind="attrs"
                      v-on="on"
                      small
                      style="min-width:auto!important"
                      @click="collapseAllCards"
                      class="primary mr-2"
                    >
                      <v-icon>mdi-eye-off-outline</v-icon>
                    </v-btn>
                  </template>
                  <span>Свий всички карти</span>
                </v-tooltip>
                <v-tooltip top v-else color="secondary">
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn 
                      icon
                      v-bind="attrs"
                      v-on="on"
                      small
                      style="min-width:auto!important"
                      @click="expandAllCards"
                      class="secondary mr-2"
                    >
                      <v-icon>mdi-eye-outline</v-icon>
                    </v-btn>
                  </template>
                  <span>Покажи всички карти</span>
                </v-tooltip>
              </template>
              <v-tooltip top v-if="expanded" color="primary">
                <template v-slot:activator="{ on, attrs }">
                  <v-btn 
                    icon
                    v-bind="attrs"
                    v-on="on"
                    small
                    style="min-width:auto!important"
                    @click="toggleExpand"
                    class="primary mx-2"
                  >
                    <v-icon>mdi-chevron-up</v-icon>
                  </v-btn>
                </template>
                <span>Свий <strong>{{expandableTitle}}</strong></span>
              </v-tooltip>
              <v-tooltip top v-else color="secondary">
                <template v-slot:activator="{ on, attrs }">
                  <v-btn 
                    icon
                    v-bind="attrs"
                    v-on="on"
                    small
                    style="min-width:auto!important"
                    @click="toggleExpand"
                    class="secondary mx-2"
                  >
                    <v-icon>mdi-chevron-down</v-icon>
                  </v-btn>
                </template>
                <span>Покажи <strong>{{expandableTitle}}</strong></span>
              </v-tooltip>
            </template>
            <template v-else>
              <slot name="after-heading-button" />
            </template>
          </v-col>
        </template>
      </v-row>

      <v-col cols="12" class="cardTitle grey lighten-3 fs14 fw500 text-uppercase pb-3" v-else-if="print">
        <slot name="after-heading" />
      </v-col>
      
      <div
        v-if="$slots['after-heading'] && false"
        class="ml-5"
      >
        <slot name="after-heading" />
      </div>

      <v-col
        v-if="hoverReveal"
        cols="12"
        class="hoverableIcon text-center py-0 mt-n12"
      >
        <slot name="reveal-actions" />
      </v-col>

      <div
        v-else-if="icon && title"
        class="ml-5"
      >
        <h4
          :class="`text-uppercase ${$vuetify.theme.isDark ? 'white--text' : ''}`"
          v-text="title"
        />
      </div>
    </div>

    <template v-if="expandable">
      
      <slot v-if="!expanded" name="show-when-collapsed" />
      
      <v-expand-transition v-if="expanded">
        <div>
          <slot />
        </div>
      </v-expand-transition>
    </template>
    <template v-else>
      <slot />
    </template>

    <template v-if="$slots.actions">
      <v-divider class="mt-2" />

      <v-card-actions class="pb-0">
        <div class="text-right" style="width:100%;">
          <slot name="actions" />
        </div>
      </v-card-actions>
    </template>
  </v-card>
</template>

<script>
  export default {
    name: 'MaterialCard',

    props: {
      avatarSize: {
        type: String,
        default: '128'
      },
      avatarPosition: {
        type: String,
        default: 'center center'
      },
      avatar: {
        type: String,
        default: '',
      },
      color: {
        type: String,
        default: '',
      },
      hoverReveal: {
        type: Boolean,
        default: false,
      },
      icon: {
        type: String,
        default: undefined,
      },
      smIcon: {
        type: Boolean,
        default: false
      },
      image: {
        type: Boolean,
        default: false,
      },
      inline: {
        type: Boolean,
        default: false,
      },
      text: {
        type: String,
        default: '',
      },
      title: {
        type: String,
        default: '',
      },
      fullTitle: {
        type: Boolean,
        default: false
      },
      expandable: {
        type: Boolean,
        default: false
      },
      expandableTitle: {
        type: String,
        default: ''
      },
      expanded: {
        type: Boolean,
        default: true
      },
      areAllCardsExpanded: {
        type: Boolean,
        default: null
      },
      minHeight: {
        type: String,
        default: null
      },
      whiteHeader: {
        type: Boolean,
        default: false
      },
      print: {
        type: Boolean,
        default: false
      }
    },

    computed: {
      classes () {
        return {
          'v-card--material--has-heading': this.hasHeading,
          'v-card--material--hover-reveal': this.hoverReveal && !this.smIcon,
          'v-card--material--hover-reveal-mini': this.hoverReveal && this.smIcon
        }
      },
      hasHeading () {
        return Boolean(this.$slots.heading || this.title || this.icon)
      },
      hasAltHeading () {
        return Boolean(this.$slots.heading || (this.title && this.icon))
      },
    },
    methods: {
      collapseAllCards(){
        this.$emit("collapseAllCards");
      },
      expandAllCards(){
        this.$emit("expandAllCards");
      },
      toggleExpand(){
        this.$emit('update:expanded', !this.expanded);
      }
    }
  }
</script>

<style lang="sass">
  .v-card--material
    &__avatar
      position: relative
      top: -64px
      margin-bottom: -32px

    &__heading
      position: relative
      top: -40px
      transition: .3s ease
      z-index: 1

    &.v-card--material--hover-reveal:hover
      .v-card--material__heading
        transform: translateY(-30px)

    &.v-card--material--hover-reveal-mini:hover
      .v-card--material__heading
        transform: translateY(-15px)
    
    .grow
      &.d-flex
        .white-header
          width: 100%
          background: #ffffff !important
          border-color: rgba(0, 0, 0, 0.12) !important
          box-shadow: 0px 3px 5px -28px rgba(0, 0, 0, 0.2), 0px 6px 16px 0px rgba(0, 0, 0, 0.14), 0px 1px 18px 0px rgba(0, 0, 0, 0.12) !important
</style>
