<template>
  <v-card
    :color="$vuetify.theme.isDark ? '' : (data.background ? data.background + '' : undefined)"
    :light="data.light ? true : false"
    class="v-card--plan mx-auto pt-3 pb-5 px-2 text-center mb-5"
    max-width="100%"
    :to="data.to"
    hover
  >
    <h4
      :class="`text-uppercase mb-5 ${$vuetify.theme.isDark ? 'white--text' :''} `"
      v-text="data.heading"
    />

    <v-avatar
      size="75"
    >
      <v-icon
        :color="data.color"
        size="40"
      >
        {{ data.icon }}
      </v-icon>
    </v-avatar>

    <h2
      class="display-2 font-weight-light"
      v-text="data.title"
    />

    <v-card-text
      class="body-1 font-weight-light pa-1"
    >
      {{ data.subTitle }}
    </v-card-text>

    <pages-btn :color="$vuetify.theme.isDark ? '' : data.color">
      {{ $t('see_more') }}
    </pages-btn>
  </v-card>
</template>

<script>
  import PagesBtn from "@/components/utils/Btn.vue"
  export default {
    name: 'PagesPlanCard',

    components: {
      PagesBtn
    },

    props: {
      data: {
        type: Object,
        default: () => ({
          light: false,
          background: undefined,
          color: undefined,
          heading: undefined,
          icon: undefined,
          title: undefined,
          subTitle: undefined,
        }),
      },
    },
  }
</script>

<style lang="sass">
  .v-card--plan
    .v-avatar
      border-radius: 50%
      border: 1px solid #E5E5E5
</style>
