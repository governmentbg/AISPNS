<template>
  <v-app>
    <core-app-bar v-model="expandOnHover" class="d-print-none" />
    <core-app-drawer
      :expand-on-hover.sync="expandOnHover"
      class="d-print-none"
    />

    <core-app-view />

    <vue-snotify></vue-snotify>
    <core-app-accessibility-modal ref="accessibilityModal" />
  </v-app>
</template>

<script>
import CoreAppBar from "./components/core/AppBar.vue";
import CoreAppDrawer from "./components/core/Drawer.vue";
import CoreAppView from "./components/core/View.vue";
import CoreAppAccessibilityModal from "./components/core/AccessibilityModal.vue";
import { mapGetters } from "vuex";

export default {
  name: "PP_ERP_BaseCoreLayout",
  components: {
    CoreAppBar,
    CoreAppDrawer,
    CoreAppView,
    CoreAppAccessibilityModal
  },
  data: () => ({
    expandOnHover: false,
  }),
  computed: {
    ...mapGetters(['accessibility'])
  },
};
</script>
