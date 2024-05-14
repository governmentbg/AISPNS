<template>
  <v-card
    class="v-card--wizard px-3 pb-10"
    elevation="12"
  >
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
                :key="`ispn-stepper-${i+1}`"
                :style="$vuetify.breakpoint.xlOnly ? 'padding: 0 10px' : ''"
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
    <v-row class="mt-10">
      <v-col cols="12" md="6">
        <v-btn
          :disabled="internalValue === 1"
          class="white--text"
          color="grey darken-2"
          min-width="200"
          @click="$emit('click:prev')"
        >
          {{$t('previousWizard')}}
        </v-btn>
      </v-col>
      <v-col cols="12" md="6" class="text-right">
        <v-btn
          :disabled="!availableSteps.includes(internalValue)"
          :color="$vuetify.theme.isDark ? '' : 'primary'"
          min-width="200"
          @click="$emit('click:next')"
        >
          {{ internalValue === items.length ? $t('submitDeclaration') : $t('nextWizard') }}
        </v-btn>
      </v-col>
    </v-row>
  </v-card>
</template>
      
<script>
import Proxyable from 'vuetify/lib/mixins/proxyable';

export default {
  name: 'ISPNWizard',

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
        font-size: 12px

      .v-stepper__step__step
        height: 20px
        min-width: 20px
        width: 20px
        margin-right: 5px

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