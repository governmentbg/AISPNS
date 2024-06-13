<script>
import { VSelect } from 'vuetify/lib'

export default {
  name: "BaseSelect",
  extends: VSelect,
  props: {
    menuProps: {
      type: [String, Array, Object],
      default: () => {
        return { offsetY: true }
      }
    }
  },
  methods: {
    change(){
      this.$emit("change");
    }
  }
}
</script>
