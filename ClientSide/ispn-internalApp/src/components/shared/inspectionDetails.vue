<template>
  <v-form>
    <v-row>
      <v-col cols="12" xl="3" lg="4" md="3">
        <v-text-field
          v-model="data.inspectionEntryNumber"
          :readonly="true"
          label="Номер на вписване"
          color="secondary"
          dense
        />
      </v-col>
      <v-col cols="12" xl="4" lg="6">
        <div class="v-input v-input--is-label-active v-input--is-dirty v-input--is-readonly v-input--dense theme--light v-text-field v-text-field--is-booted">
          <div class="v-input__control">
            <div class="v-input__slot">
              <div class="v-text-field__slot">
                <label class="v-label v-label--active theme--light" style="left: 0px; top: -3px; right: auto; position: absolute;">Преписка</label>
                <v-chip v-if="getCaseFileRegisterNumber" small @click="goToCaseFile" color="primary">{{  getCaseFileRegisterNumber  }}</v-chip>
              </div>
              <div class="v-input__append-inner">
                <i aria-hidden="true" class="v-icon notranslate theme--light secondary--text" style="height: 27px"></i>
              </div>
            </div>
          </div>
        </div>
      </v-col>
    </v-row>

    <v-row class="mt-2">
      <v-col cols="12" xl="3" md="4">
        <v-text-field
          v-model="data.invoiceNumber"
          label="Номер на фактура"
          :readonly="isFieldReadOnly"
          color="secondary"
          dense
        />
      </v-col>
      <v-col cols="12" xl="3" md="4">
        <v-text-field
          v-model="data.amount"
          label="Дължима сума"
          type="number"
          color="secondary"
          dense
          :readonly="isFieldReadOnly"
        />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <v-text-field
          :readonly="isFieldReadOnly"
          v-model="data.fullAddress"
          label="Адрес"
          color="secondary"
          dense
        />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" xl="4" lg="6" md="12">
        <base-select
          :readonly="isFieldReadOnly"
          label="Вид на проверката"
          v-model="data.inspectionTypeId"
          :items="inspectionTypes"
          item-text="name"
          item-value="id"
        />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" xl="2" lg="3" md="4">
        <base-material-date-picker
          v-model="data.inspectionDate"
          label="Дата на проверката"
          :readonly="isFieldReadOnly"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="3" md="4">
        <base-select
          v-model="data.validityPeriodId"
          label="Период на проверка"
          class="pb-0"
          hide-details
          :readonly="isFieldReadOnly"
          :items="validityPeriods"
          item-text="name"
          item-value="id"
        />
      </v-col>
      <v-col cols="12" xl="3" lg="4" md="4">
        <base-material-date-picker
          :readonly="isFieldReadOnly"
          label="Дата на следваща проверка"
          v-model="data.nextInspectionDate"
        />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" xl="6" lg="9">
        <base-org-structure-select-field 
          label="Проверител"
          v-model="data.inspectorPersonId"
          :readonly="isFieldReadOnly"
          :persons="users"
          :loadPersons="false"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="3" md="4">
        <base-select
          :readonly="isFieldReadOnly"
          label="Знак – вид"
          v-model="data.signTypeId"
          :items="signTypes"
          item-text="name"
          item-value="id"
        />
      </v-col>
      <v-col cols="12" xl="2" lg="3" md="4">
        <v-text-field
          v-model="data.signNumber"
          label="Знак - номер"
          :readonly="isFieldReadOnly"
          color="secondary"
          dense
        />
      </v-col>
      <v-col cols="12" xl="2" lg="3" md="4">
        <v-text-field
          v-model="data.signCount"
          label="Брой знаци"
          :readonly="isFieldReadOnly"
          color="secondary"
          dense
        />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" xl="3" lg="4" md="5">
        <v-text-field
          v-model="data.protocolNumber"
          label="Номер на протокол"
          :readonly="isFieldReadOnly"
          color="secondary"
          dense
        />
      </v-col>

      <v-col cols="12" xl="3" lg="4" md="5" v-if="false">
        <v-text-field
          v-model="data.linkToFile"
          label="Връзка към преписка"
          :readonly="isFieldReadOnly"
          color="secondary"
          dense
        />
      </v-col>
    </v-row>

    <v-row class="mt-4">
      <v-col cols="12" xl="3" lg="4" md="5">
        <base-material-date-picker
          :readonly="isFieldReadOnly"
          label="Дата на Удостоверението"
          dense
          v-model="data.inspectionDate"
          color="secondary"
        />
      </v-col>

      <v-col cols="12" md="2">
        <v-checkbox
          v-model="data.isPaid"
          label="Платена"
          :readonly="isFieldReadOnly"
          color="secondary"
          dense
        />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" xl="3" lg="4" md="6">
        <base-select
          :readonly="isFieldReadOnly"
          label="QR код вид размер"
          v-model="data.qrcodeTypeId"
          :items="qrcodeTypes"
          item-text="name"
          item-value="id"
        />
      </v-col>
      <v-col cols="12" xl="3" lg="4" md="6">
        <v-text-field
          v-model="data.qrcode"
          label="QR код номер залепен на СИ"
          :readonly="isFieldReadOnly"
          color="secondary"
          dense
        />
      </v-col>
      <v-col cols="12">
        <v-text-field
          v-model="data.notes"
          label="Забележка"
          :readonly="isFieldReadOnly"
          color="secondary"
          dense
        />
      </v-col>
    </v-row>
  </v-form>
</template>

<script>
import InspectionViewModel from "@/models/inspections/inspectionViewModel.js";
import { openInNewTab } from "@/utils";
import { formatDate } from "@progress/kendo-intl"

export default {
  props: {
    isFieldReadOnly: {
      type: Boolean,
      default: false,
    },
    dataFromParent: {
      type: Object,
      default: null,
    },
    inspectionTypes: {
      type: Array,
      default() {
        return [];
      },
    },
    validityPeriods: {
      type: Array,
      default() {
        return [];
      },
    },
    users: {
      type: Array,
      default() {
        return [];
      },
    },
    signTypes: {
      type: Array,
      default() {
        return [];
      },
    },
    qrcodeTypes: {
      type: Array,
      default() {
        return [];
      },
    },
  },
  components: {},
  mounted() {
    if (this.dataFromParent != null) {
      this.data = this.dataFromParent;
    } else {
      this.data = new InspectionViewModel();
    }
  },
  watch: {},
  data() {
    return {
      data: new InspectionViewModel(),
    };
  },
  computed: {
    getCaseFileRegisterNumber(){
      if(this.data.document && this.data.document.documentNumber && this.data.document.dateRegistered)
        return this.data.document.documentNumber + '-' + formatDate(new Date(this.data.document.dateRegistered), 'dd.MM.yyyy');
      
      return null;
    },
    caseFileGuid: {
      get(){
        if(this.data && this.data.document && this.data.document.guid)
          return this.data.document.guid

        return null;
      },
      set(caseFileData){
        this.linkCaseFile(caseFileData.guid)
      }
    },
  },
  methods: {
    save() {},
    openModal() {
      this.open = true;
    },
    goToCaseFile(){
      if(this.data && this.data.document && this.data.document.guid)
        openInNewTab(this.$router, `/administrative-services/documents/${this.data.document.guid}`);
    },
  },
};
</script>

<style></style>
