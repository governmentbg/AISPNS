<template>
  <v-app>
    <core-app-empty-view />
  </v-app>
</template>

<script>
  import CoreAppEmptyView from "./components/core/EmptyView.vue";

  export default {
    name: "PP_ERP_BaseCoreEmptyLayout",
    components: {
      CoreAppEmptyView
    },
    data: () => ({
      expandOnHover: false
    }),
    computed: {}
  }
</script>

