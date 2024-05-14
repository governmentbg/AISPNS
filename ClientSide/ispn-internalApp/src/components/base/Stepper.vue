<template>
  <v-card class="pt-5 pb-3 mt-10">
    <Stepper
      :value="step"
      :items="steps"
    />
  </v-card>
</template>

<script>
import { Stepper } from '@progress/kendo-vue-layout';

export default {
  name: "BaseStepper",
  components: {
    Stepper
  },
  props: {
    step: {
      type: Number,
      default: 0
    },
    steps: {
      type: Array,
      default: () => []
    }
  },
  data() {
    return {}
  }
}
</script>
