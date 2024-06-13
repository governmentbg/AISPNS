<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc"> Добавяне на образец </template>
        <template v-else> Редакция на образец </template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row class="mt-2">
            <v-col cols="12">
              <base-autocomplete
                label="Вид образец"
                v-model="data.type"
                :items="nomenclatures.types"
                item-text="label"
                :rules=[rules.required]
                clearable
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
            <v-col cols="12">
              <base-material-date-picker
                label="Дата"
                v-model="data.date"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12">
              <v-textarea
                label="Описание"
                v-model="data.description"
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
        <v-btn color="primary" @click="save"> Запази </v-btn>
        <v-btn color="info" @click="save(true)"> Запази и затвори </v-btn>
        <v-btn color="primary" outlined class="ml-3" @click="open = false">Затвори</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import DocumentTemplateModel from "@/models/settings/DocumentTemplateModel"

export default {
  name: "documentTemplateModal",
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: Object.assign({}, new DocumentTemplateModel()),
      nomenclatures: {
        types: [
          {label: "Образец на съгласие"},
          {label: "Образец на декларация"},
          {label: "Образец на акт за встъпване в длъжност"},
          {label: "Образец на на месечен отчет"},
          {label: "Образец на 6 месечен отчет"},
          {label: "Образец на на годишен отчет"},
          {label: "Образец на отчет за разходите"},
          {label: "Образец на степента на удовлетворяване на кредиторите"},
          {label: "Образец на отговор по конкретно зададени въпроси"},
          {label: "Образец 'Сметка за разпределение'"},
          {label: "Образец 'План за Осребряване'"},
        ]
      },
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = Object.assign({}, new DocumentTemplateModel());
      } else {
        if (this.data.id) {
          this.isNewDoc = false;
        } else {
          this.isNewDoc = true;
        }
      }
    },
  },
  computed: {},
  methods: {
    getData(id) {
      // id && 
        // apiGetDocumentTemplateById(id).then((result) => {
        //   this.$set(this, "data", result);
        //   this.open = true;
        // });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertDocumentTemplate(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("reloadData");
          //     if(closeModal)    
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateDocumentTemplate(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("reloadData");
          //     if(closeModal)    
          //       this.closeModal();
          //   }
          // });
        }
    },
    getTemplateTypes(){
      // apiGetDocumentTemplateTypes().then(result => {
      //   this.$set(this.nomenclatures, "types", result)
      // })
    },
    openModal(id = null) {
      if(id){
        this.data.id = id;
        this.getData();
      }
      
      this.open = true;
    },
    closeModal(){
      this.open = false;
    }
  },
};
</script>