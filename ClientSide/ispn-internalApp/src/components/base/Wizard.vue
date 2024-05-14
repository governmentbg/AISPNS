<template>
  <div>
    <v-row>
      <v-col cols="12">
        <v-stepper v-model="internalValue">
          <v-stepper-header>
            <template
              v-for="(item, i) in items"
            >
              <v-stepper-step
                :complete="availableSteps.includes(i+1)"
                :editable="availableSteps.includes(internalValue)"
                :step="i+1"
                :key="`base-stepper-${i+1}`"
                :style="$vuetify.breakpoint.xlOnly ? 'padding: 0 10px' : ''"
                class="display-1"
              >
                {{ item.label }}
              </v-stepper-step>
              <v-divider v-bind:key="i+1" v-if="$vuetify.breakpoint.xlOnly" />
            </template>
          </v-stepper-header>
          <v-stepper-items>
            <slot />
          </v-stepper-items>
        </v-stepper>
      </v-col>
    </v-row>
    <v-row class="mt-10" v-if="showNextPrev">
      <v-col cols="12" md="6">
        <v-btn
          :disabled="internalValue === 1"
          class="white--text"
          color="grey darken-2"
          min-width="200"
          @click="$emit('click:prev')"
        >
          Назад
        </v-btn>
      </v-col>
      <v-col cols="12" md="6" class="text-right">
        <v-btn
          :disabled="!availableSteps.includes(internalValue)"
          :color="$vuetify.theme.isDark ? '' : 'primary'"
          min-width="200"
          @click="$emit('click:next')"
        >
          {{ internalValue === items.length ? "Завърши" : "Напред" }}
        </v-btn>
      </v-col>
    </v-row>
  </div>
</template>
      
<script>
import Proxyable from 'vuetify/lib/mixins/proxyable';

export default {
  name: 'IVSSWizard',

  mixins: [Proxyable],

  props: {
    availableSteps: {
      type: Array,
      default: () => ([]),
    },
    items: {
      type: Array,
      default: () => ([]),
    },
    showNextPrev: {
      type: Boolean,
      default: true
    }
  },
}
</script>

<style lang="sass">
.v-stepper
  .v-stepper__header
    justify-content: space-between
    

    .v-stepper__step
      padding: 0 2px

      &:first-child
        padding: 0 0 0 5px

      &:last-:nth-last-child(1)
        padding: 0 5px 0 0 !important

      &.v-stepper__step--active
        background: #4606132e

      .v-stepper__label
        font-size: 18px

      .v-stepper__step__step
        height: 25px
        min-width: 25px
        width: 25px
        margin-right: 5px
        font-size: 16px
        
        i 
          font-size: 16px

    .v-divider
      margin: 0 0

  &.theme--dark
    .v-stepper__header
      .v-stepper__step
        .v-stepper__step__step
          &.primary
            color: #000
          i 
            color: #000

        &.v-stepper__step--active
          background: #e3e3e32e !important
</style>