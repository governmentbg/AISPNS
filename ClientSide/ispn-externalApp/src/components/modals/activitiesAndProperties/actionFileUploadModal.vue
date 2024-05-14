<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        Нов файл
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12">
              <v-textarea
                label="Описание"
                v-model="data.description"
                auto-grow
                rows="2"
              />
            </v-col>
            <v-col cols="12">
              <v-file-input
                label="Файл"
                v-model="data.file"
                dense
                :rules=[rules.required]
              />
            </v-col>
          </v-row>
        </v-form>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="save"> Прикачи </v-btn>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { apiGetAttachedDocumentKinds } from "@/api/metaData";
import { apiSubmitDocument } from "@/api/documents";
import { eDocumentContentTypes } from "@/utils/enums/enumerators";
export default {
  name: "actionFileUploadModal",
  props: {
    parentData: {
      type: Object,
      default: () => ({})
    },
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      nomenclatures: {
        types: []
      },
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
    getData() {
    },
    getAttachmentKinds(){
      apiGetAttachedDocumentKinds().then(result => {
        if(result)
          this.nomenclatures.types = result;
      })
    },
    save(closeModal = false) {
      if(this.$refs.form.validate()){
        let formData = new FormData();
        formData.append("id", this.parentData.id)
        formData.append("file", this.data.file);
        formData.append("description", this.data.description)
        
        formData.append("documentContentType", eDocumentContentTypes.ACTIVITY);

        if(this.parentData?.documentCollectionId)
          formData.append("attachedDocumentCollectionId", this.parentData?.documentCollectionId);

        apiSubmitDocument(formData).then((result) => {
          if (result && result.id) {
            if(!this.parentData?.documentCollectionId)
              this.$emit('setDocumentCollectionId', result.documentCollectionId)

            this.$emit("reloadData");
            if(closeModal)
              this.closeModal();
          }
        });
      }
    },
    openModal(id = null) {
      if(!this.nomenclatures.types.length)
        this.getAttachmentKinds();

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