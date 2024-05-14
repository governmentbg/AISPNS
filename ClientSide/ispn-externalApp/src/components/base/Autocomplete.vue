<script>
import { VAutocomplete } from 'vuetify/lib'

export default {
  name: "BaseAutocomplete",
  extends: VAutocomplete,
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
    readonly: {
      type: Boolean,
      default: false
    },
    clearable: {
      type: Boolean,
      default: true
    }
  },
  watch: {
  },
  methods: {
    change(){
      this.$emit("change");
    }
  }
}
</script>
