<template>
  <v-dialog v-model="open" width="60%" scrollable>
    <v-card>
      <v-card-title class="headline">
        Добавяне на физическо лице
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12" lg="4">
              <base-input
                label="Име"
                v-model="data.firstName"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="4">
              <base-input
                label="Презиме"
                v-model="data.middleName"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="4">
              <base-input
                label="Фамилия"
                v-model="data.lastName"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="4">
              <base-input
                label="ЕГН"
                v-model="data.egn"
                :rules=[rules.required]
              />
            </v-col>
          </v-row>
        </v-form>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="save"> Запази </v-btn>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { apiMetaDataCreatePerson } from "@/api/metaData";
export default {
  name: "personModal",
  props: {
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {},
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = {};
      } else {
        if (this.data.id) {
          this.isNewDoc = false;
        } else {
          this.isNewDoc = true;
        }
      }
    },
  },
  computed: {
    
  },
  methods: {
    save() {
      if(this.$refs.form.validate()){
        apiMetaDataCreatePerson(this.data).then((result) => {
          if (result && result.id) {
            this.$emit("personSaved", result)
          }
        });
      }
    },
    openModal() {
      this.data = {};
      this.open = true;
      this.isNewDoc = true;
      
      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    closeModal(){
      this.open = false;
    }
  },
};
</script>