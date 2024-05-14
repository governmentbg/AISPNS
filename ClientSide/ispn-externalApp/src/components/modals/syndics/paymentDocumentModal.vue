<template>
  <v-dialog id="paymentDocumentModal" v-model="open" max-width="850px" scrollable>
    <v-card>
      <v-card-text class="pa-2" style="overflow-y: auto">
        <v-form ref="paymentDocumentForm" :action="paymentURL+'/ais/paymentOrder'" method="post" id="platezhnoNarezhdane" target="_blank" class="hidden">
          <v-text-field type="hidden" id="clientId" name="clientId" v-model="paymentOrderData.clientId" />
          <v-text-field type="hidden" id="hmac" name="hmac" v-model="paymentOrderData.hmac" />
          <v-text-field type="hidden" id="data" name="data" v-model="paymentOrderData.data" />
          <button type="submit" ref="paymentDocumentSubmitButton" id="submitPaymentOrderData" value="Платежно нареждане" class="v-btn v-btn--block v-btn--contained theme--light v-size--large primary" style="width: 262px; margin-right: 20px;"></button>
        </v-form>
        <iframe name="paymentOrderIframe" id="paymentOrderIframe" width="100%" height="885px"></iframe>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn class="primary" @click="open = !open">Затвори</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>


<script>
import config from '@/config';
export default {
  name: "paymentDocumentModal",
  props: {
    paymentOrderData: {
      type: Object,
      default: () => {},
    },
  },
  mounted() {},
  data() {
    return {
      open: false,
      paymentURL: "",
      data: {},
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = {};
      }
    },
  },
  computed: {
    
  },
  methods: {
    openModal() {
      this.paymentURL = config.isProduction ? 'https://pay.egov.bg' : 'https://pay-test.egov.bg';
      this.open = true;
      let vue = this;
      this.$nextTick().then(() => {
        //this.$refs.paymentDocumentForm.submit();
        document.querySelector("#submitPaymentOrderData").click();
        vue.closeModal();
      });
    },
    closeModal(){
      this.open = false;
    }
  },
};
</script>