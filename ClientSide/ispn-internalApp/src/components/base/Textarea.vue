<script>
import { VTextarea } from 'vuetify/lib'

export default {
  name: "BaseTextarea",
  extends: VTextarea,
  props: {
    dense: {
      type: Boolean,
      default: true
    },
    autoGrow: {
      type: Boolean,
      default: true
    },
    rows: {
      type: Number,
      default: 2
    }
  },
}
</script>
