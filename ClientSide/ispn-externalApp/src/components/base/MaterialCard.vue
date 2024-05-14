<template>
  <v-card
    v-bind="$attrs"
    :class="classes"
    class="v-card--material px-3 py-5"
  >
    <div class="d-flex grow flex-wrap">
      <v-avatar
        v-if="avatar"
        size="128"
        class="mx-auto v-card--material__avatar elevation-6"
        color="white"
      >
        <v-img :src="avatar" />
      </v-avatar>

      <v-sheet
        v-else-if="icon || $slots.heading"
        :class="{
          'cardIconHolder': icon && icon.length ? true : false,
          'px-4 py-2 mb-n3': smIcon,
          'pa-4 mb-n6': !smIcon,
          //'pa-7': !$slots.image
        }"
        :color="color"
        :max-height="icon ? 59 : undefined"
        :width="inline || icon ? 'auto' : '100%'"
        elevation="6"
        class="text-start v-card--material__heading"
        dark
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

        <template v-else-if="title && !icon">
          <div
            v-if="!removable"
            class="display-1 font-weight-light"
            v-text="title"
          />

          <template v-else>
            <v-row class="py-2">
              <v-col cols="11">
                <div
                  class="display-1 font-weight-light"
                  v-text="title"
                />
              </v-col>
              <v-col cols="1" md="1" class="text-right">
                <v-tooltip 
                  left
                  top
                  color="secondary"
                >
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn
                      icon
                      small
                      v-bind="attrs"
                      v-on="on"
                      @click="$emit('remove')"
                    >
                      <v-icon>mdi-close</v-icon>
                    </v-btn>
                  </template>
                  <span>Изтрий</span>
                </v-tooltip>
              </v-col>
            </v-row>
          </template>

        </template>

        <template v-else-if="icon">
          <v-icon
            v-if="!icon.includes('svg')"
            size="27"
          >
            {{icon}}
          </v-icon>
          <v-img v-else width="40" :src="require(`@/assets/${icon}`)" class="mt-2" :style="(icon.includes('.svg') ? 'height: 15px;' : '')" />
        </template>

        <div
          v-if="text"
          class="headline font-weight-thin"
          v-text="text"
        />
      </v-sheet>



      <v-row v-if="$slots['after-heading'] || $slots['after-heading-button']" class="ml-3">
        <template v-if="fullTitle">
          <v-col cols="12" class="cardTitle display-2 font-weight-light">
            <slot name="after-heading" />
          </v-col>
        </template>
        <template v-else>
          <v-col cols="12" :lg="$slots['after-heading-button'] ? 6 : 12" :md="$slots['after-heading-button'] ? 7 : 12" class="cardTitle display-2 font-weight-light">
            <slot name="after-heading" />
          </v-col>
          <v-col cols="12" :lg="$slots['after-heading-button'] ? 6 : 12" class="buttonContainer text-right">
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
                      <v-icon color="white">mdi-eye-off-outline</v-icon>
                    </v-btn>
                  </template>
                  <span>Свий всички карти</span>
                </v-tooltip>
                <v-tooltip top v-else color="primary">
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn 
                      icon
                      v-bind="attrs"
                      v-on="on"
                      small
                      style="min-width:auto!important"
                      @click="expandAllCards"
                      class="primary mr-2"
                    >
                      <v-icon color="white">mdi-eye-outline</v-icon>
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
                    <v-icon color="white">mdi-chevron-up</v-icon>
                  </v-btn>
                </template>
                <span>Свий <strong>{{expandableTitle}}</strong></span>
              </v-tooltip>
              <v-tooltip top v-else color="primary">
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
                    <v-icon color="white">mdi-chevron-down</v-icon>
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


      <div
        v-if="$slots['after-heading'] && false"
        class="ml-6"
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
        class="ml-4"
      >
        <div

          class="card-title font-weight-light"
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
      <div :class="icon ? 'pt-5' : 'pt-5'">
        <slot />
      </div>
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
      avatar: {
        type: String,
        default: '',
      },
      color: {
        type: String,
        default: 'success',
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
      removable: {
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
        transform: translateY(-15px)

    &.v-card--material--hover-reveal-mini:hover
      .v-card--material__heading
        transform: translateY(-15px)
</style>
