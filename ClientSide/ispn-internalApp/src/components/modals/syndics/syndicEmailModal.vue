<template>
  <div>
    <v-dialog v-model="open" width="60%" scrollable>
      <v-card>
        <v-card-title class="headline">
          Изпращане на мейл до {{ getSyndicFullName(syndicData) }}
        </v-card-title>

        <v-card-text>
          <base-material-card 
            icon="mdi-email-arrow-right-outline"
            color="primary"
            class="elevation-3 mt-5"
          >
            <template v-slot:after-heading>
              Съобщение
            </template>
            
            <v-form ref="form">
              <v-row>
                <v-col cols="12">
                  <base-input
                    label="Относно"
                    v-model="data.subject"
                    :rules=[rules.required]
                  />
                </v-col>
                <v-col cols="12">
                  <v-textarea
                    label="Съобщение"
                    v-model="data.body"
                    rows="2"
                    auto-grow
                    :rules=[rules.required]
                  />
                </v-col>
              </v-row>
            </v-form>
          </base-material-card>
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="send(false)"> Изпрати </v-btn>
          <v-btn color="primary" outlined @click="open = false">
            Затвори
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { getSyndicFullName } from "@/utils";
import { apiSendCustomEmail } from "@/api/emails";

export default {
  name: "syndicEmailModal",
  components: {
  },
  props: {
    syndicData: {
      type: Object,
      default: () => {
        return null;
      },
    }
  },
  data() {
    return {
      open: false,
      
      data: {
        subject: null,
        body: null
      },
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  mounted() {},
  watch: {
    open(isOpen) {
      if (!isOpen)
        this.$set(this, 'data', {
          subject: null,
          body: null
        });
    },
  },
  computed: {},
  methods: {
    send(){
      if(this.$refs.form.validate){
        let data = Object.assign({}, this.data);
        data.syndicId = this.syndicData.id;

        apiSendCustomEmail(data).then(result => {
          if(result)
            this.closeModal();
        })
      }
    },
    openModal() {
      this.open = true;

      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    closeModal(){
      this.open = false;
    },
    getSyndicFullName
  },
};
</script>

<style></style>
