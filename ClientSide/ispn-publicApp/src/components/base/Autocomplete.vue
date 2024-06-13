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
    }
  },
  methods: {
    change(){
      this.$emit("change");
    },
    setLabelWidth () {
      if (!this.outlined || !this.$refs.label) return

      this.labelWidth = this.$refs.label.offsetWidth * 0.75 + 12
    }
  }
}
</script>
