<template>
  <v-dialog v-model="open" width="80%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc">Ново обучение</template>
        <template v-else>Преглед на обучение</template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12">
              <base-autocomplete
                label="Вид обучение"
                v-model="data.type"
                :items="nomenclatures.trainingTypes"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12">
              <base-input
                label="Тема"
                v-model="data.topic"
                :rules=[rules.required]
              />
            </v-col>
            
            <v-col cols="12">
              <v-textarea
                rows="2"
                auto-grow
                label="Лектори"
                v-model="data.lectors"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12">
              <v-textarea
                rows="2"
                auto-grow
                label="Подтеми"
                v-model="data.subTopics"
                :rules=[rules.required]
              />
            </v-col>
            <v-col cols="12" lg="2">
              <base-material-date-picker
                label="Първа дата на изпита"
                v-model="data.examDate1"
              />
            </v-col>
            <v-col cols="12" lg="2">
              <base-material-date-picker
                label="Втора дата на изпита"
                v-model="data.examDate2"
              />
            </v-col>
            <v-col cols="12" lg="8">
              <base-input
                label="Максимален брой участници"
                v-model="data.maxNumberParticipants"
                type="number"
                :rules=[rules.required]
              />
            </v-col>
            
          </v-row>
          <v-row>
            <v-col cols="12">
              <h4>Провеждане на обучение дата 1</h4>
              <v-divider class="my-3" />
            </v-col>
            <v-col cols="12" lg="2" md="6">
              <base-material-date-picker
                label="От дата"
                v-model="data.fromDate1"
              />
            </v-col>
            <v-col cols="12" lg="2" md="6">
              <base-material-date-picker
                label="До дата"
                v-model="data.toDate1"
              />
            </v-col>
            <v-col cols="12" lg="2">
              <base-input
                label="Продължителност"
                v-model="data.duration1"
                :rules=[rules.required]
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" xl="2" lg="4" md="4">
              <base-autocomplete
                label="Област"
                class="pb-0"
                v-model="data.address1.regionId"
                :items="nomenclatures.districts"
                item-text="name"
                item-value="id"
                @change="getMunicipalities"
                clearable
              />
            </v-col>
            <v-col cols="12" xl="2" lg="4" md="4">
              <base-autocomplete
                v-model="data.address1.municipalityId"
                label="Община"
                :items="nomenclatures.municipalities"
                item-text="name"
                item-value="id"
                class="pb-0"
                @change="getSettlements"
                clearable
              />
            </v-col>
            <v-col cols="12" xl="2" lg="4" md="4">
              <base-autocomplete
                label="Населено място"
                class="pb-0"
                v-model="data.address1.settlementId"
                :items="nomenclatures.settlements"
                item-text="name"
                item-value="id"
                clearable
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address1.postCode"
                label="ПК"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="5" lg="6" md="4">
              <v-text-field
                v-model="data.address1.streetName"
                label="Улица"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address1.streetNumber"
                label="Улица №"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address1.buildingNumber"
                label="Сграда №"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address1.entranceNumber"
                label="Вход"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address1.floorNumber"
                label="Етаж"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address1.apartmentNumber"
                label="Ап. №"
                color="secondary"
                dense
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <h4>Провеждане на обучение дата 2</h4>
              <v-divider class="my-3" />
            </v-col>
            <v-col cols="12" lg="2" md="6">
              <base-material-date-picker
                label="От дата"
                v-model="data.fromDate2"
              />
            </v-col>
            <v-col cols="12" lg="2" md="6">
              <base-material-date-picker
                label="До дата"
                v-model="data.toDate2"
              />
            </v-col>
            <v-col cols="12" lg="8">
              <base-input
                label="Продължителност"
                v-model="data.duration2"
                :rules=[rules.required]
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" xl="2" lg="4" md="4">
              <base-autocomplete
                label="Област"
                class="pb-0"
                v-model="data.address2.regionId"
                :items="nomenclatures.districts"
                item-text="name"
                item-value="id"
                @change="getMunicipalities"
                clearable
              />
            </v-col>
            <v-col cols="12" xl="2" lg="4" md="4">
              <base-autocomplete
                v-model="data.address2.municipalityId"
                label="Община"
                :items="nomenclatures.municipalities"
                item-text="name"
                item-value="id"
                class="pb-0"
                @change="getSettlements"
                clearable
              />
            </v-col>
            <v-col cols="12" xl="2" lg="4" md="4">
              <base-autocomplete
                label="Населено място"
                class="pb-0"
                v-model="data.address2.settlementId"
                :items="nomenclatures.settlements"
                item-text="name"
                item-value="id"
                clearable
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address2.postCode"
                label="ПК"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="5" lg="6" md="4">
              <v-text-field
                v-model="data.address2.streetName"
                label="Улица"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address2.streetNumber"
                label="Улица №"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address2.buildingNumber"
                label="Сграда №"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address2.entranceNumber"
                label="Вход"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address2.floorNumber"
                label="Етаж"
                color="secondary"
                dense
              />
            </v-col>
            <v-col cols="12" xl="1" lg="2" md="2">
              <v-text-field
                v-model="data.address2.apartmentNumber"
                label="Ап. №"
                color="secondary"
                dense
              />
            </v-col>
          </v-row>
          <v-row>
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


        <v-tabs
          v-model="currentTab"
          fixed-tabs
          slider-color="primary"
          @change="tabChange"
          slider-size="3"
          class="mt-10"
        >
          <v-tabs-slider color="secondary" />
          <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key">
            {{ tab.name }}
          </v-tab>
        </v-tabs>
        <v-tabs-items v-model="currentTab">
          <v-tab-item v-for="tab in tabs" :key="tab.key" :value="tab.key">
            <v-card flat>
              <v-card-title class="headline print d-print-flex mb-0 mt-3 py-4">
                <v-row>
                  <v-col cols="12" md="6">
                    {{tab.name}}
                  </v-col>
                </v-row>
              </v-card-title>
              <v-card-text class="mt-5">
                <base-kendo-grid
                  :columns="tables[tab.key].headers"
                  :items="tables[tab.key].data"
                  :pagination="tables[tab.key].pagination"
                  sortable
                  :sort.sync="tables[tab.key].sort"
                  @getData="getTablesData"
                  @click="tableActions"
                />
              </v-card-text>
            </v-card>
          </v-tab-item>
        </v-tabs-items>
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
import TrainingModel from "@/models/trainings/TrainingModel";
export default {
  name: "trainingModal",
  props: {
    trainingData: {
      type: Object,
      default: () => {
        return {}
      }
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: Object.assign({}, new TrainingModel()),
      nomenclatures: {
        trainingTypes: [],
        districts: [],
        municipalities: [],
        settlements: []
      },
      currentTab: 0,
      tabs: [
        { key: 'trainingRequests', name: 'Заявки за обучение' },
        { key: 'rankedParticipantsFirstDate', name: 'Класирани първа дата' },
        { key: 'rankedParticipantsSecondDate', name: 'Класирани втора дата' },
        { key: 'trainingResults', name: 'Резултат преминали/непреминали' }
      ],
      tables: {
        trainingRequests: {
          headers: [
            { title: "Име", field: "", sortable: true },
            { title: "Регистрирация на", field: "", sortable: true },
            { title: "Основно обучение", field: "", sortable: true },
            { title: "Резерв. осн. об.", field: "", sortable: true },
            { title: "Лектор осн. об.", field: "", sortable: true },
            { title: "Алтернативно обучение", field: "", sortable: true },
            { title: "Резерв. алт. об.", field: "", sortable: true },
            { title: "Лектор алт. об.", field: "", sortable: true },
          ],
          data: [],
          sort: {},
          pagination: {
            page: 1,
            skip: 0,
            take: 20,
            perPageOptions: [5, 10, 20, 50, 100],
            total: 0,
          },
        },
        rankedParticipantsFirstDate: {
          headers: [
            { title: "Име", field: "", sortable: true },
            { title: "Регистрирация на", field: "", sortable: true },
            { title: "Основно обучение", field: "", sortable: true },
            { title: "Резерв. осн. об.", field: "", sortable: true },
            { title: "Лектор осн. об.", field: "", sortable: true },
            { title: "Алтернативно обучение", field: "", sortable: true },
            { title: "Резерв. алт. об.", field: "", sortable: true },
            { title: "Лектор алт. об.", field: "", sortable: true },
          ],
          data: [],
          sort: {},
          pagination: {
            page: 1,
            skip: 0,
            take: 20,
            perPageOptions: [5, 10, 20, 50, 100],
            total: 0,
          },
        },
        rankedParticipantsSecondDate: {
          headers: [
            { title: "Име", field: "", sortable: true },
            { title: "Регистрирация на", field: "", sortable: true },
            { title: "Основно обучение", field: "", sortable: true },
            { title: "Резерв. осн. об.", field: "", sortable: true },
            { title: "Лектор осн. об.", field: "", sortable: true },
            { title: "Алтернативно обучение", field: "", sortable: true },
            { title: "Резерв. алт. об.", field: "", sortable: true },
            { title: "Лектор алт. об.", field: "", sortable: true },
          ],
          data: [],
          sort: {},
          pagination: {
            page: 1,
            skip: 0,
            take: 20,
            perPageOptions: [5, 10, 20, 50, 100],
            total: 0,
          },
        },
        trainingResults: {
          headers: [
            { title: "Име", field: "", sortable: true },
            { title: "Регистрирация на", field: "", sortable: true },
            { title: "Основно обучение", field: "", sortable: true },
            { title: "Резерв. осн. об.", field: "", sortable: true },
            { title: "Лектор осн. об.", field: "", sortable: true },
            { title: "Алтернативно обучение", field: "", sortable: true },
            { title: "Резерв. алт. об.", field: "", sortable: true },
            { title: "Лектор алт. об.", field: "", sortable: true },
          ],
          data: [],
          sort: {},
          pagination: {
            page: 1,
            skip: 0,
            take: 20,
            perPageOptions: [5, 10, 20, 50, 100],
            total: 0,
          },
        },
      },
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = Object.assign({}, new TrainingModel());
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
    getCurrentTabDetails(){
      if(this.tabs.length && this.tabs[this.currentTab]) 
        return this.tabs[this.currentTab];

      return null;
    },
  },
  methods: {
    getData() {
      // apiGetProgramTrainingById(this.data.id).then((result) => {
      //   this.$set(this, "data", result);
      //   this.open = true;
      // });
    },
    getTrainingRequestsData(){

    },
    getRankedParticipantsFirstDate(){

    },
    getRankedParticipantsSecondDate(){

    },
    getTrainingResultsData(){

    },
    getTablesData(){
      switch(this.currentTab){
        case 0:
          this.getTrainingRequestsData()
        break;
        case 1:
          this.getRankedParticipantsFirstDate()
        break;
        case 2:
          this.getRankedParticipantsSecondDate()
        break;
        case 3:
          this.getTrainingResultsData()
        break;
      }
    },
    tabChange(tabKey, force = false) {
      if(tabKey && this.tables[tabKey] && this.tables[tabKey].data === null || force)
        switch (tabKey) {
          case "trainingRequests":
            this.getTrainingRequestsData()
          break;
          case "rankedParticipantsFirstDate":
            this.getRankedParticipantsFirstDate()
          break;
          case "rankedParticipantsSecondDate":
            this.getRankedParticipantsSecondDate()
          break;
          case "trainingResults":
            this.getTrainingResultsData()
          break;
        }
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
        
        break;
      }
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertProgramTraining(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("reloadData");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateProgramTraining(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("reloadData");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        }
    },
    getMunicipalities() {
      if(this.data.address1.regionId) {
        // apiGetMunicipalities(this.data.address.regionId).then((result) => {
        //   this.$set(this.nomenclatures.municipalities, "municipalities", result);
        //   this.$set(this.data.address, "settlementId", null);
        //   this.$set(this.data.address, "municipalityId", null);
        // });
      } else {
        this.$set(this.nomenclatures, "municipalities", []);
        this.$set(this.nomenclatures, "settlements", []);
      }

      //Set ekatte to null
      this.data.address.ekkate = null;
    },
    getSettlements() {
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
    openModal(id = null) {
      if(id){
        this.data.id = id;
        this.getData();
      } else {
        this.data = Object.assign({}, new TrainingModel());
        this.isNewDoc = true;
        this.open = true;
      }
    },
    closeModal(){
      this.open = false;
    }
  },
};
</script>

<style></style>
