<script>
import { VTextField } from 'vuetify/lib'

export default {
  name: "BaseInput",
  extends: VTextField,
  props: {
    dense: {
      type: Boolean,
      default: true
    }
  },
}
</script>
