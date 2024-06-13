<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc"> Добавяне на шаблон </template>
        <template v-else> Редакция на шаблон </template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row class="mt-2">
            <v-col cols="12">
              <base-autocomplete
                label="Тип шаблон"
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
import TemplateModel from "@/models/settings/TemplateModel"

export default {
  name: "templateModal",
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: Object.assign({}, new TemplateModel()),
      nomenclatures: {
        types: [
          {label: "Молба до ДВ"},
          {label: "Списък на синдици"},
          {label: "Образец/молба за заплщане"},
          {label: "Шаблон на програма за обучение"},
          {label: "Шаблон уведомление на синдик за класиране за обучение"},
          {label: "Шаблон за уведомпление на синдиците за публикуване на програма"},
          {label: "Образец на доклад от проверка"},
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
        this.data = Object.assign({}, new TemplateModel());
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
        // apiGetTemplateById(id).then((result) => {
        //   this.$set(this, "data", result);
        //   this.open = true;
        // });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertTemplate(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("reloadData");
          //     if(closeModal)    
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateTemplate(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("reloadData");
          //     if(closeModal)    
          //       this.closeModal();
          //   }
          // });
        }
    },
    getTemplateTypes(){
      // apiGetTemplateTypes().then(result => {
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