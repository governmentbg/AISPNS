<template>
  <v-dialog v-model="open" width="70%" scrollable>
    <v-card>
      <v-card-title class="headline">
        Нов файл
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12">
              <base-autocomplete
                label="Тип документ"
                v-model="data.description"
                :items="nomenclatures.documentTypes"
              />
            </v-col>
            <v-col cols="12">
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
        <v-btn color="primary" @click="save(true)"> Запази </v-btn>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { apiSubmitDocument, apiGetFileName } from "@/api/documents";
import { eDocumentContentTypes } from "@/utils/enums/enumerators";

export default {
  name: "templateFileUploadModal",
  props: {
    parentData: {
      type: Object,
      default: () => {},
    },
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      nomenclatures: {
        documentTypes: [
          "Образец",
          "Друг документ"
        ],
      },
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
      apiGetFileName(this.data.id).then(result => {
        this.open = true;
      })
    },
    save(closeModal = false) {
      if(this.$refs.form.validate()){
        let formData = new FormData();
        formData.append("id", this.parentData.id);
        formData.append("file", this.data.file);
        formData.append("DocumentContentType", eDocumentContentTypes.SYNDIC_TEMPLATE);
        formData.append("description", this.data.description);

        if(this.parentData.documentCollectionId)
          formData.append("attachedDocumentCollectionId", this.parentData.documentCollectionId);

        apiSubmitDocument(formData).then((result) => {
          if (result && result.id) {
            this.$emit('setDocumentCollectionId', result.documentCollectionId)

            this.$nextTick(() => {
              this.$emit("reloadData");
              if(closeModal)
                this.closeModal();
            });
          }
        });
      }
    },
    openModal(id = null) {
      if(id){
        this.data.id = id;
        this.getData();
      } else {
        this.open = true;
        this.isNewDoc = true;
      }

      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    closeModal(){
      this.open = false;
    }
  },
};
</script>
