<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      heading="Обявяване на актове по стабилизация на предприемач"
      goBackText="Обратно към обявяване на актове предприемачи стабилизация"
      goBackTo="/entrepreneurs-data/stabilization"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
      </v-col>
      <v-col cols="12" lg="7" xl="8" class="text-right">
        <v-btn
          color="primary"
          @click="announceActAsk"
          v-if="!isReadonly"
        >
          <v-icon left dark>mdi-check</v-icon>
          Обяви
        </v-btn>
        <v-btn
          color="primary darken-2"
          @click="setNoSubjectToAnnouncementAsk"
          v-if="!isReadonly"
        >
          <v-icon left dark>mdi-close</v-icon>
          Неподлежи на обявяване
        </v-btn>
        <v-btn
          color="secondary"
          @click="$router.push(`/entrepreneurs-data/bankruptcy/${actData.id}`)"
          v-if="!isReadonly"
        >
          <v-icon left dark>mdi-badge-account-horizontal-outline</v-icon>
          Вписване
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="my-10">
      <v-col cols="12">
        <base-material-card 
          icon="mdi-magnify-expand"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Обявяване на актове по стабилизация предприемач
          </template>
          
          <v-row class="pa-3">
            <v-col cols="12" xl="2" lg="6" md="6">
              <base-input
                label="Дело номер"
                v-model="actData.caseNumber"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="2" lg="6" md="6">
              <base-input
                label="Дело година"
                v-model="actData.caseYear"
                type="number"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="4">
              <base-input
                label="Съд"
                v-model="actData.courtName"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="5" lg="8">
              <base-input
                label="Предприемач"
                v-model="actData.proprietorName"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="3" lg="6">
              <base-input
                label="Вид акт"
                v-model="actData.actKind"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="2" lg="3" md="6">
              <base-input
                label="Свали Акт"
                v-model="actData.actFile"
                append-icon="mdi-download"
                @click:append="downloadAct"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="2" lg="3" md="6">
              <base-input
                label="Свали Писмо"
                v-model="actData.letterFile"
                append-icon="mdi-download"
                @click:append="downloadLetter"
                readonly
              />
            </v-col>
            <v-col cols="12" xl="2" lg="4" md="6">
              <base-input
                label="Статус"
                v-model="actData.announcementStatus"
              />
            </v-col>
          </v-row>
        </base-material-card>

        <base-material-card 
          icon="mdi-file-multiple-outline"
          color="primary"
          class="elevation-3 mt-15"
        >
          <template v-slot:after-heading>
            Обявен акт
          </template>
          
          <v-form ref="form" class="pa-3" v-if="!actIsNotSubjectToAnnouncement">
            <v-row>
              <v-col cols="12">
                <base-autocomplete
                  label="Категория акт"
                  v-model="data.actCategoryId"
                  :items="nomenclatures.actCategories"
                  item-text="description"
                  item-value="id"
                  :rules="[rules.required]"
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Описание"
                  v-model="data.description"
                  rows="2"
                  auto-grow
                  :readonly="isReadonly"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="6">
                <base-material-date-picker
                  label="Дата"
                  v-model="data.announcedDate"
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
            </v-row>
          </v-form>

          <template v-else>
            <v-alert
              type="info"
              color="primary"
              icon="mdi-information"
              class="fs18 py-2"
            >
              Акта неподлежи на обявяване. 
              <br /> 
              {{ announcedBy }}
            </v-alert>
          </template>
        </base-material-card>
      </v-col>
    </v-row>
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, downloadFileFromResponse, getFullName } from '@/utils';
import { ActAnnouncementFileModal } from "@/components";
import ActAnnouncementModel from '@/models/actAnnouncement/ActAnnouncementModel';
import { apiGetActById, apiGetActAnnouncementById, apiAnnounceAct, apiNoSubjectToAnnouncement, apiDownloadActImage, apiDownloadActLetterImage } from "@/api/actAnnouncements";
import { apiMetaDataGetActAnnouncementCategories } from "@/api/metaData";
import { eActStatuses } from "@/utils/enums/enumerators";
export default {
  name: "entrepreneurActsStabilizationPreview",
  components: {
    ActAnnouncementFileModal
  },
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {
        actCategories: [],
      },
      actData: Object.assign({}, {}),
      data: Object.assign({}, new ActAnnouncementModel()),
      rules: {
        required: v => !!v || 'Полето е задължително.',
        requiredSelect: v => (typeof v === 'string' && v.length) || typeof v === 'boolean' || !isNaN(parseFloat(v)) && v >= 0 && v <= 999 || "Полето е задължително",
        requiredSelectReturnObjectMultiple: v => v.length > 0 || "Полето е задължително",
        min: v => (v && v.length >= 3) || 'Полето трябва да съдържа поне 3 символа',
        email: v => !v || /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) || 'E-mail-a трябва да бъде валиден адрес.'
      },
    }
  },
  mounted(){
    this.getActCategories();
    this.isNewDoc = true;
    if(this.$route.params.id != 'create'){
      this.actData.id = this.$route.params.id;
      this.isNewDoc = false;
    } else {
      this.data.inspector = this.currentUser.firstAndLastName;
    }

    this.getData();
  },
  computed: {
    ...mapGetters([
      'currentUser'
    ]),
    isReadonly(){
      return this.data.announcementStatusId?.toLowerCase() !== eActStatuses.NOT_PROCESSED.toLowerCase();
    },
    actIsNotSubjectToAnnouncement(){
      return this.data.announcementStatusId?.toLowerCase() === eActStatuses.NOT_SUBJECT_TO_ANNOUCEMENT.toLowerCase();
    },
    announcedBy(){
      if(this.data.announcedByUser)
        return `Отбелязано от: ${getFullName(this.data.announcedByUser)} на ${ISODateString(this.data.announcedDate)}`; ;
    }
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetActById(this.actData.id).then(result => {
          this.$set(this, 'actData', result);
          this.getAnnouncementData();
        })
    },
    getAnnouncementData(){
      if(this.actData.actAnnouncementId)
        apiGetActAnnouncementById(this.actData.actAnnouncementId).then(result => {
          this.$set(this, 'data', result);
          
          this.$refs.form.resetValidation();
        })
    },
    getActCategories(){
      apiMetaDataGetActAnnouncementCategories(true).then(result => {
        this.nomenclatures.actCategories = result;
      })
    },
    announceActAsk(){
      if(this.$refs.form.validate()){
        let confirmData = {
          title: "Обявяване на акт",
          body: "Сигурни ли сте, че искате да обявите акта?",
          callback: this.announceAct
        }

        this.$refs.confirmModal.openModal(confirmData);
      } else {
        this.$snotify.warning("Моля попълнете всички задължителни полета, отбелязани в червен цвят.")
      }
    },
    announceAct(){
      apiAnnounceAct(this.data).then(result => {
        if(result && result.length){
          this.$refs.confirmModal.closeModal();
          this.getData();
        }
      })
    },
    setNoSubjectToAnnouncementAsk(){
      let confirmData = {
        title: "Неподлежи на обявяване",
        body: "Сигурни ли сте, че искате да обявите акта като неподлежащ на обявяване?",
        callback: this.setNoSubjectToAnnouncement
      }

      this.$refs.confirmModal.openModal(confirmData);
    },
    setNoSubjectToAnnouncement(){
      apiNoSubjectToAnnouncement(this.data).then(result => {
        if(result && result.length){
          this.$refs.confirmModal.closeModal();
          this.getData();
        }
      })
    },
    downloadAct(){
      apiDownloadActImage(this.actData.id).then(result => {
        downloadFileFromResponse(result);
      })
    },
    downloadLetter(){
      apiDownloadActLetterImage(this.actData.id).then(result => {
        downloadFileFromResponse(result);
      })
    },

    ISODateString
  }
}
</script>