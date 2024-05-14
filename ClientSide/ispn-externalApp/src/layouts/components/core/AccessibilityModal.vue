<template>
  <v-dialog
    v-model="accessibility.modal.open"
    scrollable
    :max-width="this.$vuetify.breakpoint.sm || this.$vuetify.breakpoint.xs ? '100%' : '40%'"
    style="margin-left:0;margin-right:0"
  >
    <v-card id="accessibilityModal" ref="accessibilityModal">
      <v-card-title class="headline">
        Настройки интерфейс
      </v-card-title>
      <v-card-text class="pt-10">
        <v-row>
          <v-col cols="12">
            Размер на шрифта:
            <v-btn
              outlined
              small
              class="mx-3"
              @click="biggerFontSize"
              :disabled="biggerFontSizeDisabled"
            >
              <v-icon>mdi-plus</v-icon>
            </v-btn>
            <v-btn
              outlined
              small
              @click="smallerFontSize"
              :disabled="accessibility.font.size <= accessibility.font.minSize"
            >
              <v-icon>mdi-minus</v-icon>
            </v-btn>
            <v-btn
              outlined
              small
              @click="defaultFontSize"
              class="ml-3"
            >
              <v-icon>mdi-refresh</v-icon>
            </v-btn>
          </v-col>
        </v-row>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          :color="$vuetify.theme.isDark ? '' : 'primary'"
          @click="closeModal"
          dark
        >
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>


<script>
import { mapGetters } from 'vuex';


export default {
  name: 'accessibilityModal',
  data(){
    return {
      open: false,
      data: {},
    }
  },
  watch: {
    open: function(val){
      if(!val)
        this.closeModal();
    }
  },
  computed: {
    ...mapGetters(['accessibility']),
    biggerFontSizeDisabled(){
      return this.accessibility.font.size >= this.accessibility.font.maxSize
    },
    smallerFontSizeDisabled(){
      return this.accessibility.font.size <= this.accessibility.font.minSize;
    }
  },
  mounted(){},
  methods: {
    openModal(data){
      this.open = true;
      this.data = data.event;
    },
    closeModal(){
      this.$store.dispatch("app/closeAccessibilityModal")
    },
    biggerFontSize(){
      this.$store.dispatch("app/biggerFontSize")
    },
    smallerFontSize(){
      this.$store.dispatch("app/smallerFontSize")
    },
    defaultFontSize(){
      this.$store.dispatch("app/defaultFontSize")
    },
    setLightTheme(){
      this.$store.dispatch("app/setLightTheme")
    },
    setDarkTheme(){
      this.$store.dispatch("app/setDarkTheme")
    },
    setColorBlindTheme(){
      this.$store.dispatch("app/setColorBlindTheme")
    },
    setFontFamily(fontFamily){
      this.$store.dispatch("app/setFontFamily", fontFamily)
    }
  }
}
</script>