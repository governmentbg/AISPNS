<template>
  <v-dialog v-model="open" width="70%" scrollable>
    <v-card>
      <v-card-title class="headline">
        {{ getModalTitle }}
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12"  lg="6" v-if="tab === 'things'">
              <base-autocomplete
                label="Тип"
                v-model="data.type"
                :items="nomenclatures.types"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="6">
              <base-autocomplete
                label="Вид"
                v-model="data.kind"
                :items="nomenclatures.kinds"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="6" v-if="tab === 'shares' || tab === 'receipts'">
              <base-autocomplete
                label="Юридическо лице"
                v-model="data.entity"
                :items="nomenclatures.entities"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="6" v-if="tab === 'receipts'">
              <base-autocomplete
                label="Физическо лице"
                v-model="data.entity"
                :items="nomenclatures.entities"
                :rules=[rules.required]
              />
            </v-col>
            <template v-if="tab === 'things'">
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
                <base-input
                  v-model="data.address.postCode"
                  label="ПК"
                />
              </v-col>
              <v-col cols="12" xl="5" lg="6" md="4">
                <base-input
                  v-model="data.address.streetName"
                  label="Улица"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.streetNumber"
                  label="Улица №"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.buildingNumber"
                  label="Сграда №"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.entranceNumber"
                  label="Вход"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.floorNumber"
                  label="Етаж"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.apartmentNumber"
                  label="Ап. №"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="4">
                <base-input
                  v-model="data.address.ekkate"
                  readonly
                  label="ЕКАТТЕ"
                />
              </v-col>
              <v-col cols="12" v-if="tab === 'things'">
                <v-textarea
                  label="Състояние"
                  v-model="data.condition"
                  rows="2"
                  auto-grow
                />
              </v-col>
            </template>
            <v-col cols="12" lg="4">
              <base-input
                label="Стойност"
                v-model="data.number"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" v-if="tab !== 'things'">
              <v-textarea
                label="Описание"
                v-model="data.description"
                rows="2"
                auto-grow
              />
            </v-col>
            <v-col cols="12">
              <v-textarea
                label="Бележки"
                v-model="data.notes"
                rows="2"
                auto-grow
              />
            </v-col>
            <v-col cols="12" v-if="isNewDoc">
              <v-file-input
                label="Прикачи документ"
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

export default {
  name: "journalBankruptcyModal",
  props: {
    tab: {
      type: String,
      default: null
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {
        address: {},
        syndicName: "Иван Иванов"
      },
      nomenclatures: {
        orderTypes: [],
        municipalities: [],
        settlements: [],
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
    getModalTitle(){
      if(this.isNewDoc){
        switch(this.tab){
          case "things":
            return "Нова вещ";
          case "patents":
            return "Нов патент";
          case "shares":
            return "Нов дял";
          case "receipts":
            return "Ново взимане";
        }
      } else {
        switch(this.tab){
          case "things":
            return "Преглед на вещ";
          case "patents":
            return "Преглед на патент";
          case "shares":
            return "Преглед на дял";
          case "receipts":
            return "Преглед на взимане";
        }
      }
    }
  },
  methods: {
    getData() {
      // apiGetSyndicOrderById(this.data.id).then((result) => {
      //   this.$set(this, "data", result);
      //   this.open = true;
      // });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertSyndicOrder(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateSyndicOrder(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        }
    },
    getSettlements(){

    },
    getMunicipalities(){

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
  },
};
</script>
