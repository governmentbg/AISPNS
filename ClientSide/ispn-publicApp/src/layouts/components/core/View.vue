<template>
  <v-main>
    <keep-alive>
      <router-view :key="$route.fullPath" />
    </keep-alive>
  </v-main>
</template>

<script>

export default {
  name: 'CoreAppView',

  components: {
  },
}
</script>
