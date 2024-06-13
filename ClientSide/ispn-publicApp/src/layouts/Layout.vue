<template>
  <v-app :dark="true" :class="accessibility.font.family">
    <core-app-header class="d-print-none" />

    <core-app-view />

    <vue-snotify v-if="false"></vue-snotify>

    <core-app-accessibility-modal ref="accessibilityModal" />
    <core-app-footer />
  </v-app>
</template>

<script>
  import CoreAppHeader from "./components/core/AppBar.vue";
  import CoreAppView from "./components/core/View.vue";
  import CoreAppFooter from "./components/core/Footer.vue";
  import CoreAppAccessibilityModal from "./components/core/AccessibilityModal.vue";
  import { mapGetters } from "vuex";

  export default {
    name: "ISPNCoreLayout",
    components: {
      CoreAppHeader,
      CoreAppView,
      CoreAppFooter,
      CoreAppAccessibilityModal
    },
    data: () => ({
      expandOnHover: false
    }),
    computed: {
      ...mapGetters(['accessibility'])
    }
  }
</script>

