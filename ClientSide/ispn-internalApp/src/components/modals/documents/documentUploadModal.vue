<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc">Нов файл</template>
        <template v-else>Редакция на файл</template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12">
              <base-input
                label="Описание"
                v-model="data.description"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" v-if="isNewDoc">
              <v-file-input
                label="Файл"
                v-model="data.file"
                :rules=[rules.required]
                dense
              />
            </v-col>
          </v-row>
        </v-form>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="save"> Запази </v-btn>
        <v-btn color="secondary" @click="save(true)"> Запази и затвори </v-btn>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { apiSubmitDocument, apiGetDocumentContentById } from "@/api/documents";
import { eDocumentContentTypes } from "@/utils/enums/enumerators";

export default {
  name: "documentUploadModal",
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {
      },
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
    getData(){
      apiGetDocumentContentById(this.data.id).then(result => {
        this.data = result;
        this.isNewDoc = false;
        this.open = true;
      })
    },
    save(closeModal = false) {
      if(this.$refs.form.validate()){
        let formData = new FormData();
        formData.append("file", this.data.file);
        formData.append("DocumentContentType", eDocumentContentTypes.WEBSITE_DOCUMENT);
        formData.append("description", this.data.description);

        apiSubmitDocument(formData).then((result) => {
          if (result && result.id) {
            this.$emit("reloadData");
            if(closeModal)
              this.closeModal();
          }
        });
      }
    },
    openModal(id = null) {
      if(this.$refs.form)
        this.$refs.form.resetValidation();

      if(id){
        this.data.id = id;
        this.getData();
      } else {
        this.open = true;
        this.isNewDoc = true;
      }
    },
    closeModal(){
      this.open = false;
    }
  },
};
</script>
