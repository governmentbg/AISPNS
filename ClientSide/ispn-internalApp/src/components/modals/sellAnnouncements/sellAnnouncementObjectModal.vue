<template>
  <v-dialog v-model="open" width="80%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc">Нова вещ към обява за продажба</template>
        <template v-else>Преглед вещ към обява за продажба</template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12" lg="6">
              <base-autocomplete
                label="Тип"
                v-model="data.kind"
                :items="nomenclatures.kinds"
                :rules="[rules.required]"
              />
            </v-col>
            <v-col cols="12" lg="6">
              <base-autocomplete
                label="Вид"
                v-model="data.kind"
                :items="nomenclatures.kinds"
                :rules="[rules.required]"
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <h5>Адрес</h5>
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="4">
              <base-autocomplete
                label="Област"
                class="pb-0"
                v-model="data.address.regionId"
                :items="nomenclatures.districts"
                item-text="name"
                item-value="id"
                @change="getMunicipalities"
                clearable
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="4">
              <base-autocomplete
                v-model="data.address.municipalityId"
                label="Община"
                :items="nomenclatures.municipalities"
                item-text="name"
                item-value="id"
                class="pb-0"
                @change="getSettlements"
                clearable
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4" md="4">
              <base-autocomplete
                label="Населено място"
                class="pb-0"
                v-model="data.address.settlementId"
                :items="nomenclatures.settlements"
                item-text="name"
                item-value="id"
                clearable
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address.postCode"
                label="ПК"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="5" lg="6" md="4">
              <v-text-field
                v-model="data.address.streetName"
                label="Улица"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address.streetNumber"
                label="Улица №"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address.buildingNumber"
                label="Сграда №"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address.entranceNumber"
                label="Вход"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address.floorNumber"
                label="Етаж"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address.apartmentNumber"
                label="Ап. №"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="2" lg="3" md="4">
              <v-text-field
                v-model="data.address.ekkate"
                readonly
                label="ЕКАТТЕ"
                color="secondary"
                dense
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" xl="2" lg="6">
              <base-input
                label="Състояние"
                v-model="data.condition"
                :rules="[rules.required]"
              />
            </v-col>
            <v-col cols="12" xl="2" lg="6">
              <base-input
                label="Стойност"
                v-model="data.amount"
                :rules="[rules.required]"
              />
            </v-col>
            <v-col cols="12">
              <base-input
                label="Бележки"
                v-model="data.notes"
                :rules="[rules.required]"
              />
            </v-col>
            <v-col cols="12">
              <v-file-input
                label="Файл"
                v-model="data.file"
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
        <v-btn color="secondary" @click="save(true)"> Запази и затвори </v-btn>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>

export default {
  name: "sellAnnouncementObjectModal",
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      nomenclatures: {
        types: [],
        kinds: []
      },
      data: {
        address: {}
      },
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = {
          address: {}
        };
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
    getData(id) {
      // apiGetSellAnnouncementObject(this.data.id).then((result) => {
      //   this.$set(this, "data", result);
      //   this.open = true;
      // });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertSellAnnouncementObject(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateSellAnnouncementObject(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
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
    },
    closeModal(){
      this.open = false;
    },
    getMunicipalities() {
      this.data[typeOfAddress].hasChanges = true;
      if(this.data.address.regionId) {
        // apiGetMunicipalities(this.data.address.regionId).then((result) => {
        //   this.$set(this.nomenclatures.municipalities, "municipalities", result);
        //   this.$set(this.data.address, "settlementId", null);
        //   this.$set(this.data.address, "municipalityId", null);
        // });
      } else {
        this.$set(this.data.address, "settlementId", null);
        this.$set(this.data.address, "municipalityId", null);
        this.$set(this.nomenclatures, "municipalities", []);
        this.$set(this.nomenclatures, "settlements", []);
      }

      //Set ekatte to null
      this.data.address.ekkate = null;
    },
    getSettlements(typeOfAddress) {
      if (this.data.address.municipalityId) {
        // apiGetSettlements(this.data.address.municipalityId).then((result) => {
        //   this.$set(this.nomenclatures, "settlements", result);
        //   this.$set(this.data.address, "settlementId", null);
        // });
      } else {
        this.$set(this.nomenclatures, "settlements", []);
        this.$set(this.data.address, "settlementId", null);
      }

      //Set ekatte to null
      this.data.address.ekkate = null;
    },
  },
};
</script>