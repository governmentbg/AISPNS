<template>
  <v-dialog v-model="open" width="50%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc"> Добавяне на номенклатура </template>
        <template v-else> Редакция на номенклатура </template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row class="mt-2">
            <v-col cols="12" v-if="showYear">
              <base-input
                label="Година"
                v-model="data.year"
                type="number"
              />
            </v-col>
            <v-col cols="12" v-if="showAmount">
              <base-input
                label="Вноска (лв.)"
                v-model="data.amount"
                type="number"
              />
            </v-col>
            <v-col cols="12" v-if="showCode">
              <base-input
                label="Код"
                v-model="data.code"
                :type="isCodeString ? 'text' : 'number'"
              />
            </v-col>
            <v-col cols="12" v-if="showNumber">
              <base-input
                label="Номер"
                v-model="data.number"
                type="number"
              />
            </v-col>
            <v-col cols="12" v-if="showName">
              <base-input
                label="Наименование"
                v-model="data.name"
                :readonly="isNameReadonly"
              />
            </v-col>
            <v-col cols="12" v-if="showDescription">
              <base-input
                label="Описание"
                v-model="data.description"
              />
            </v-col>
            <v-col cols="12" v-if="showIsStabilization">
              <base-autocomplete
                label="Стабилизация/Несъстоятелност"
                v-model="data.isStabilization"
                :items="nomenclatures.stabilizationOrBankruptcy"
                item-text="label"
                item-value="value"
                :rules="[rules.requiredTrueFalse]"
              />
            </v-col>
          </v-row>
        </v-form>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="save"> Запази </v-btn>
        <v-btn color="primary" outlined class="ml-3" @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
// import AddNewFuelExpenseParamModel from '@/models/addNewFuelExpenseParamModel.js';
import {
  apiGetNomenclatureById,
  apiUpdateNomenclature,
  apiInsertNomenclature,
} from "@/api/nomenclatures";

export default {
  name: "nomenclatureModal",
  props: {
    type: {
      type: Number,
      default: null,
    },
    nomenclatureEnums: {
      type: Array,
      default: null
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {},
      nomenclatures: {
        stabilizationOrBankruptcy: [
          { label: "Стабилизация", value: true },
          { label: "Несъстоятелност", value: false },
        ],
      },
      rules: {
        required: [(v) => !!v || "Полето е задължително"],
        requiredTrueFalse: (v) => v === true || v === false || "Полето е задължително",
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
    nomenclatureKey(){
      let key = null;
      for(let nomenclatureEnum of this.nomenclatureEnums){
        if(nomenclatureEnum.id === this.type){
          key = nomenclatureEnum.key;
          break;
        }
      }
      return key;
    },
    isNameReadonly(){
      return [87].includes(this.type);
    },
    showName(){
      return [4, 7, 10, 11, 15, 24, 42, 45, 46, 50, 51, 52, 87].includes(this.type);
    },
    showCode(){
      return [1, 2, 3, 5, 6, 8, 9, 12, 13, 14, 18, 24].includes(this.type);
    },
    showDescription(){
      return [1, 2, 3, 5, 6, 8, 9, 12, 13, 14, 17, 18, 19, 20, 21, 22, 23, 24, 25, 40, 41, 43, 47, 48, 49, 53, 54, 57, 80, 81, 82, 83, 84, 85, 11, 86, 87].includes(this.type);
    },
    showNumber(){
      return [7, 16].includes(this.type);
    },
    showAmount(){
      return [44].includes(this.type);
    },
    showYear(){
      return [44].includes(this.type);
    },
    isCodeString(){
      return [5].includes(this.type);
    },
    showIsStabilization(){
      return [19, 20, 21, 24].includes(this.type)
    }
  },
  methods: {
    getData(id, type) {
      id && 
        apiGetNomenclatureById(id, type).then((result) => {
          this.$set(this, "data", result);
          this.open = true;
        });
    },
    save() {
      if(this.$refs.form.validate()){
        if (this.isNewDoc) {
          apiInsertNomenclature(this.data, this.type).then((result) => {
            if (result && result.id) {
              this.$emit("update");
              this.closeModal();
            }
          });
        } else {
          apiUpdateNomenclature(this.data, this.type).then((result) => {
            if (result && result.id) {
              this.$emit("update");
              this.closeModal();
            }
          });
        }
      } else {
        this.$snotify.warning("Моля попълнете всички задължителни полета.")
      }
    },
    openModal(data = null) {
      if(data)
        this.data = data;

      if(this.$refs.form)
        this.$refs.form.resetValidation();
      
      this.open = true;
    },
    closeModal(){
      this.open = false;
    }
  },
};
</script>

<style></style>
