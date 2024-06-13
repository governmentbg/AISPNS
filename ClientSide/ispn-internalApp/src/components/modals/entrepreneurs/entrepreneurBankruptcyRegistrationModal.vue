<template>
  <v-dialog v-model="open" width="80%" scrollable>
    <v-card>
      <v-card-title class="headline">
        <template v-if="isNewDoc">Ново вписване на данни на предприемач по несъстоятелност</template>
        <template v-else>Преглед на вписани данни на предприемач по несъстоятелност</template>
      </v-card-title>

      <v-card-text>
        <v-form ref="form">
          <v-row>
            <v-col cols="12">
              <base-autocomplete
                label="Избор на поле за вписване"
                v-model="data.fieldId"
                :items="nomenclatures.actFields"
                item-text="description"
                item-value="id"
                :rules="[rules.required]"
                :readonly="isReadonly"
              />
            </v-col>
            <template v-if="!showSyndic">
              <v-col cols="12">
                <v-textarea
                  rows="2"
                  auto-grow
                  label="Описание"
                  v-model="data.description"
                  :rules="[rules.required]"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="6">
                <base-input
                  label="Номер"
                  v-model="data.number"
                  disabled
                >
                  <template v-slot:append>
                    <v-tooltip 
                      top
                      color="primary"
                    >
                      <template v-slot:activator="{ on, attrs }">
                        <v-btn
                          icon
                          small
                          v-bind="attrs"
                          v-on="on"
                        >
                          <v-icon color="secondary">mdi-information-slab-circle-outline</v-icon>
                        </v-btn>
                      </template>
                      <span>Номера на вписване се генерира автоматично.</span>
                    </v-tooltip>
                  </template>
                </base-input>
              </v-col>
              <v-col cols="12" xl="2" lg="6">
                <base-material-date-picker
                  label="Дата"
                  v-model="data.date"
                  :rules="[rules.required]"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="4" lg="6">
                <base-autocomplete
                  label="Правно основание"
                  v-model="data.legalBasisId"
                  :items="nomenclatures.legalBasis"
                  item-text="description"
                  item-value="id"
                  :rules="[rules.required]"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="6">
                <base-input
                  label="Срок на обжалване (дни)"
                  v-model="data.appealTerm"
                  type="number"
                  :rules="[rules.required]"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col xl="4" lg="3" md="4" cols="12">
                <v-checkbox
                  v-model="data.isEnforcedImmediately"
                  label="Подлежи на незабавно изпълнение"
                  dense
                  :readonly="isReadonly"
                />
              </v-col>
            </template>
          </v-row>
          <v-row v-if="showSyndic">
            <template>
              <v-col cols="12" lg="6">
                <base-syndic-static-autocomplete
                  label="Синдик"
                  v-model="data.syndicId"
                  :items="nomenclatures.syndics"
                  :rules="[rules.required]"
                  v-if="false"
                />
                
                <base-syndic-static-autocomplete
                  ref="syndicAutocomplete"
                  label="Синдик"
                  v-model="data.syndicId"
                  :items="nomenclatures.syndics"
                  text-value="id"
                  :rules="[rules.required]"
                  do-not-search
                  :return-object="false"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" lg="6">
                <base-autocomplete
                  label="Вид на синдика"
                  v-model="data.registerSyndicKindId"
                  :items="nomenclatures.syndicKinds"
                  item-text="description"
                  item-value="id"
                  :rules="[rules.required]"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12">
                <h5>Адрес синдик</h5>
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
                  :clearable="!isReadonly"
                  :readonly="isReadonly"
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
                  :clearable="!isReadonly"
                  :readonly="isReadonly"
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
                  :clearable="!isReadonly"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.postCode"
                  label="ПК"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="5" lg="6" md="4">
                <base-input
                  v-model="data.address.streetName"
                  label="Улица"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.streetNumber"
                  label="Улица №"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.buildingNumber"
                  label="Сграда №"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.entranceNumber"
                  label="Вход"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.floorNumber"
                  label="Етаж"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <base-input
                  v-model="data.address.apartmentNumber"
                  label="Ап. №"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="4">
                <base-input
                  v-model="data.address.ekkate"
                  readonly
                  label="ЕКАТТЕ"
                />
              </v-col>
              <v-col cols="12" md="6">
                <base-input
                  v-model="data.phoneNumber"
                  label="Телефон"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" md="6">
                <base-input
                  v-model="data.email"
                  label="E-mail"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" lg="6">
                <base-autocomplete
                  label="Правно основание"
                  v-model="data.legalBasisId"
                  :items="nomenclatures.legalBasis"
                  item-text="description"
                  item-value="id"
                  :rules="[rules.required]"
                  :readonly="isReadonly"
                />
              </v-col>
            </template>
          </v-row>
        </v-form>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="save(false)" v-if="!isReadonly"> Запази </v-btn>
        <v-btn color="secondary" @click="save(true)" v-if="!isReadonly"> Запази и затвори </v-btn>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { eActDataRegistration, eActStatuses } from "@/utils/enums/enumerators";
import EntrepreneurRegisterEntryModel from "@/models/entrepreneurs/EntrepreneurRegisterEntryModel"
import { apiGetRegisterEntryById, apiCreateRegisterEntry, apiUpdateRegisterEntry } from "@/api/actAnnouncements";
import { 
  apiMetaDataGetDistricts, 
  apiMetaDataGetMunicipalities, 
  apiMetaDataGetSettlements,
  apiMetaDataGetActFields,
  apiMetaDataGetRegistrationLegalBasis,
  apiMetaDataGetRegisterSyndicKinds,
  apiMetaDataGetSyndicsShortInfo
} from "@/api/metaData";
import { apiGetSyndicById } from "@/api/syndics";


export default {
  name: "entrepreneurBankruptcyRegistration",
  components: {
  },
  props: {
    actData: {
      type: Object,
      default: () => {},
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      nomenclatures: {
        districts: [],
        municipalities: [],
        settlements: [],
        actFields: [],
        legalBasis: [],
        syndicKinds: [],
        syndics: []
      },
      syndicData: null,
      data: Object.assign({}, new EntrepreneurRegisterEntryModel()),
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen)
        this.data = {};
    },
    'data.syndicId': {
      handler: function (val) {
        if(val && this.isNewDoc)
          this.getSyndicData()
      },
      deep: true
    },
  },
  computed: {
    showSyndic(){
      if(this.data?.fieldId && (this.data?.fieldId?.toLowerCase() === eActDataRegistration.SYNDIC.toLowerCase()) || (this.data?.fieldId?.toLowerCase() === eActDataRegistration.TRUSTED_PERSON.toLowerCase()))
        return true;

      return false;
    },
    isReadonly(){
      return this.actData.registrationStatusId?.toLowerCase() !== eActStatuses.NOT_PROCESSED.toLowerCase();
    },
  },
  methods: {
    getData() {
      if(this.data.id)
        apiGetRegisterEntryById(this.data.id).then((result) => {
          this.$set(this, "data", result);

          if(this.data.address && this.data.address.regionId)
            this.getAddressNomenclatures();

          this.isNewDoc = false;
          this.open = true;
        });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          this.data.actId = this.actData.id;
          apiCreateRegisterEntry(this.data).then((result) => {
            if(result && result !== '00000000-0000-0000-0000-000000000000') {
              this.$emit("update");
              this.data.id = result;
              if(closeModal){
                this.closeModal();
              } else {
                this.getData();
              }
            }
          });
        } else {
          apiUpdateRegisterEntry(this.data).then((result) => {
            if(result && result !== '00000000-0000-0000-0000-000000000000') {
              this.$emit("update");
              if(closeModal)
                this.closeModal();
            }
          });
        }
    },
    openModal(id = null) {
      if(!this.nomenclatures.actFields.length)
        this.getActFields();

      if(!this.nomenclatures.legalBasis.length)
        this.getRegistrationLegalBasis();

      if(!this.nomenclatures.syndicKinds.length)
        this.getSyndicKinds();

      if(!this.nomenclatures.districts.length)
        this.getDistricts();

      if(id){
        this.data.id = id;
        this.getData();
      } else {
        this.isNewDoc = true;
        this.open = true;
        this.data = Object.assign({}, new EntrepreneurRegisterEntryModel());
      }

      this.getSyndics();

      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    closeModal(){
      this.open = false;
    },
    getActFields(){
      apiMetaDataGetActFields().then(result => {
        this.$set(this.nomenclatures, "actFields", result);
      })
    },
    getRegistrationLegalBasis(){
      apiMetaDataGetRegistrationLegalBasis().then(result => {
        this.$set(this.nomenclatures, "legalBasis", result);
      })
    },
    getSyndicKinds(){
      apiMetaDataGetRegisterSyndicKinds().then(result => {
        this.$set(this.nomenclatures, "syndicKinds", result);
      })
    },
    getSyndics(){
      apiMetaDataGetSyndicsShortInfo().then(result => {
        this.$set(this.nomenclatures, "syndics", result);
      })
    },
    getSyndicData(){
      apiGetSyndicById(this.data.syndicId).then(result => {
        this.$set(this, "syndicData", result);
        this.$set(this.data, "address", Object.assign(result.address, {id: null}));
        this.$set(this.data, "email", result.email);
        this.$set(this.data, "phoneNumber", result.phone);
        this.getAddressNomenclatures();
      })
    },
    getAddressNomenclatures(){
      if(this.data.address.regionId)
        apiMetaDataGetMunicipalities(this.data.address.regionId).then((result) => {
          this.$set(this.nomenclatures, "municipalities", result);

          if(this.data.address.municipalityId)
            apiMetaDataGetSettlements(this.data.address.municipalityId).then((result) => {
              this.$set(this.nomenclatures, "settlements", result);
            });
        });
    },
    getDistricts(){
      apiMetaDataGetDistricts().then((result) => {
        this.$set(this.nomenclatures, "districts", result);
      });
    },
    getMunicipalities() {
      apiMetaDataGetMunicipalities(this.data.address.regionId).then((result) => {
        this.$set(this.nomenclatures, "municipalities", result);
        this.$set(this.data.address, "settlementId", null);
        this.$set(this.data.address, "municipalityId", null);
      });
    },
    getSettlements() {
      apiMetaDataGetSettlements(this.data.address.municipalityId).then((result) => {
        this.$set(this.nomenclatures, "settlements", result);
        this.$set(this.data.address, "settlementId", null);
      });
    },
  },
};
</script>