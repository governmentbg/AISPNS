<template>
  <v-dialog v-model="open" width="60%" scrollable>
    <v-card>
      <v-card-title class="headline">
        Добавяне на юридическо лице
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12" lg="6">
              <base-input
                label="Юридическо лице"
                v-model="data.name"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="6">
              <base-input
                label="Булстат"
                v-model="data.bulstat"
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
import { apiMetaDataCreateEntity } from "@/api/metaData";
export default {
  name: "entityName",
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
        apiMetaDataCreateEntity(this.data).then((result) => {
          if (result && result.id) {
            this.$emit("entitySaved", result)
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