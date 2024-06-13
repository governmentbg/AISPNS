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
    },
    color: {
      type: String,
      default: 'secondary'
    },
    dense: {
      type: Boolean,
      default: true
    },
    errorMessages: {
      type: Array,
      default: null
    }
  },
  methods: {
    change(){
      this.$emit("change");
    }
  }
}
</script>
