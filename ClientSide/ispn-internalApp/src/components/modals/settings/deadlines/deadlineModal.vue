<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc"> Добавяне на срок </template>
        <template v-else> Редакция на срок </template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row class="mt-2">
            <v-col cols="12">
              <base-autocomplete
                label="Тип срок"
                v-model="data.type"
                :items="nomenclatures.types"
                item-text="label"
                :rules=[rules.required]
                clearable
              />
            </v-col>
            <v-col cols="12">
              <base-input
                label="Срок"
                v-model="data.deadline"
                type="number"
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
import DeadlineModel from "@/models/settings/DeadlineModel"

export default {
  name: "deadlineModal",
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: Object.assign({}, new DeadlineModel()),
      nomenclatures: {
        types: [
          {label: "Срок за предявяване на вземания"},
          {label: "Срок за предявяване на допълнителни вземания"},
          {label: "Срок за обжалване на решението за откриване на производство по несъстоятелност"},
          {label: "Срок за възражения срещу списъци с приети и неприети вземания"},
          {label: "Срок за обжалване на сметка за разпределение"},
          {label: "Срок за обжалване на решения на събиране на кредиторите"},
          {label: "Срок за обжалване на решението за обявяване на несъстоятелност"},
          {label: "Срок за обжалване на постановлението за възлагане"},
          {label: "Срок, в който може да бъде предложен оздравителен план"},
          {label: "Срок за обжалване на решението за откриване на производство по стабилизация"},
          {label: "Срок за възражения срещу списъка на кредиторите"},
          {label: "Срок за обжалване на определението за ограничаване на дейността на търговеца"},
          {label: "Срок за обжалване на определението за наложени обезпечителни мерки"},
          {label: "Срок за обжалване на определението за прекратяване на ПС"},
          {label: "Срок за временно отстраняване"},
          {label: "Срок за автоматично прекратяване на профила на синдика"},
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
        this.data = Object.assign({}, new DeadlineModel());
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
        // apiGetDeadlineById(id).then((result) => {
        //   this.$set(this, "data", result);
        //   this.open = true;
        // });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertDeadline(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("reloadData");
          //     if(closeModal)    
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateDeadline(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("reloadData");
          //     if(closeModal)    
          //       this.closeModal();
          //   }
          // });
        }
    },
    getDeadlineTypes(){
      // apiGetDeadlineTypes().then(result => {
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