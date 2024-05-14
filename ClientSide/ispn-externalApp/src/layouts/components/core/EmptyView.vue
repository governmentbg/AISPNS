<template>
  <v-main style="padding:0">
    <router-view :key="$route.fullPath" />
  </v-main>
</template>

<script>

export default {
  name: 'CoreAppEmptyView',
  data(){
    return {
    }
  },
  components: {
  },
}
</script>
