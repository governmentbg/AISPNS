<template>
  <v-dialog v-model="open" width="60%" scrollable>
    <v-card>
      <v-card-title class="headline">
        Връщане на обява за корекция
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12">
              <v-textarea
                label="Коментар"
                v-model="data.comment"
                rows="3"
                :rules="[rules.required]"
                auto-grow
              />
            </v-col>
          </v-row>
        </v-form>

      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="sendForCorrectionAsk">Върни за корекция </v-btn>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { apiSendAnnouncementForCorrection } from "@/api/sellAnnouncements";
export default {
  name: "sendAnnouncementForCorrectionModal",
  components: {
  },
  props: {
    announcementData: {
      type: Object,
      default: () => {
        return null;
      }
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      data: {
        comment: null
      },
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) 
        this.data = {comment: null}
    },
  },
  computed: {},
  methods: {
    sendForCorrectionAsk(){
      if(this.$refs.form.validate()){
        let confirmData = {
          title: "Връщане на обява за корекция",
          body: `Сигурни ли сте, че искате да върнете обявата за корекция?`,
          callback: this.sendForCorrection
        };
        this.$emit("confirmAction", confirmData);
      }
    },
    sendForCorrection() {
      const data = {
        announcementId: this.announcementData.id,
        correctionComment: this.data.comment
      }
      apiSendAnnouncementForCorrection(data).then((result) => {
        if (result) {
          this.$emit("closeConfirm")
          this.closeModal();
          this.$emit("success");
        }
      });
    },
    openModal() {
      this.open = true;
      this.data = {comment: null}
      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    closeModal(){
      this.open = false;
    },
  },
};
</script>