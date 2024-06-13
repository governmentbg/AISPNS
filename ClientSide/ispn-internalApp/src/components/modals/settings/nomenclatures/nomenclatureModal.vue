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
            <v-col cols="12">
              <base-textarea
                label="Номенклатура"
                v-model="data.name"
              />
            </v-col>
            <template v-if="Object.prototype.hasOwnProperty.call(data, 'periodInMonths')">
              <v-col cols="12" lg="6">
                <v-text-field 
                  label="Години"
                  v-model="data.periodInYears"
                  dense
                />
              </v-col>
              <v-col cols="12" lg="6">
                <v-text-field 
                  label="Месеци"
                  v-model="data.periodInMonths"
                  dense
                />
              </v-col>
            </template>
            <template v-if="nomenclatureKey === 'GroundOfEntry' || nomenclatureKey === 'ForeignGroundOfEntry'">
              <v-col cols="12" lg="4">
                <v-checkbox
                  v-model="data.isInitialEntry"
                  label="Първоначален"
                />
              </v-col>
              <v-col cols="12" lg="4">
                <v-checkbox
                  v-model="data.canBeCanceled"
                  label="Отменим"
                />
              </v-col>
              <v-col cols="12" lg="4" v-if="nomenclatureKey === 'GroundOfEntry'">
                <v-checkbox
                  v-model="data.isComposite"
                  label="Съставен"
                />
              </v-col>
            </template>
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
      rules: {
        required: [(v) => !!v || "Полето е задължително"],
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
    },
    openModal(data = null) {
      if(data)
        this.data = data;
      
      this.open = true;
    },
    closeModal(){
      this.open = false;
    }
  },
};
</script>

<style></style>
