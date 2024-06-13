<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc">Добавяне на документ към сигнал</template>
        <template v-else>Преглед на документ към сигнал</template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12" lg="6">
              <base-autocomplete
                label="Вид документ"
                v-model="data.signalDocumentKindId"
                :items="nomenclatures.documentTypes"
                :rules=[rules.required]
                item-text="name"
                item-value="id"
              />
            </v-col>
            <v-col cols="12" lg="3">
              <base-material-date-picker
                label="Дата"
                v-model="data.documentDate"
              />
            </v-col>
            <v-col cols="12">
              <base-input
                label="Описание"
                v-model="data.description"
              />
            </v-col>
            <v-col cols="12">
              <base-textarea
                label="Бележки"
                v-model="data.notes"
              />
            </v-col>
            <v-col cols="12" v-if="isNewDoc">
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
        <v-btn color="primary" @click="save(true)">Запази и затвори</v-btn>
        <v-btn color="primary" outlined @click="open = false">Затвори</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { apiGetDocumentContentById, apiSubmitDocument, apiUpdateDocumentContent } from "@/api/documents";
import { apiGetAttachedDocumentSignalKinds } from "@/api/metaData";
import { eDocumentContentTypes } from "@/utils/enums/enumerators";
export default {
  name: "signalDocumentModal",
  props: {
    parentData: {
      type: Object,
      default: () => ({})
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      nomenclatures: {
        documentTypes: []
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
      apiGetDocumentContentById(this.data.id).then((result) => {
        this.data = result;
        this.isNewDoc = false;
        this.open = true;
      });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate()){
        if(this.isNewDoc){
          let formData = new FormData();
          formData.append("id", this.parentData.id)
          formData.append("file", this.data.file);
          formData.append("signalDocumentKindId", this.data.signalDocumentKindId);
          formData.append("documentDate", this.data.documentDate);
          formData.append("description", this.data.description);
          formData.append("notes", this.data.notes);
          formData.append("documentContentType", eDocumentContentTypes.SIGNAL);

          if(this.parentData?.documentCollectionId)
            formData.append("attachedDocumentCollectionId", this.parentData.documentCollectionId);

          apiSubmitDocument(formData).then((result) => {
            if (result && result.id) {
              if(!this.parentData?.documentCollectionId)
                this.$emit('setDocumentCollectionId', result.documentCollectionId)

              this.$nextTick(() => {
                this.$emit("reloadData");
                if(closeModal)
                  this.closeModal();
              });
            }
          });
        } else {
          apiUpdateDocumentContent(this.data).then((result) => {
            if (result && result.id) {
              this.$emit("reloadData");
              if(closeModal)
                this.closeModal();
            }
          });
        }
      }
    },
    getSignalDocumentKinds(){
      apiGetAttachedDocumentSignalKinds().then((result) => {
        this.nomenclatures.documentTypes = result;
      });
    },
    openModal(id = null) {
      if(!this.nomenclatures.documentTypes.length)
        this.getSignalDocumentKinds();

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