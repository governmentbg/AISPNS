<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      :heading="isNewDoc ? 'Добавяне на инспекция' : 'Преглед на инспекция'"
      goBackText="Обратно към инспекции"
      goBackTo="/inspections"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4" v-if="!isNewDoc && (data.dateCreated || data.dateModified)">
          <template v-if="data.dateCreated">
            Създадена на: <strong> {{ formatDateTime(data.dateCreated) }} </strong> <br />
          </template>
          <template v-if="data.dateModified">
            Последна промяна: <strong> {{ formatDateTime(data.dateModified) }} </strong> <br />
          </template>
        </v-sheet>
      </v-col>
      <v-col cols="12" lg="7" xl="8" class="text-right">
        <v-btn
          color="success"
          @click="publishProgramAsk"
          v-if="showPublishButton"
        >
          <v-icon left dark>mdi-publish</v-icon>
          Публикуване
        </v-btn>
        <v-btn
          color="primary"
          @click="enrollProgramAsk"
          v-if="showEnrollmentButton"
        >
          <v-icon left dark>mdi-format-list-numbered</v-icon>
          Класиране на участници
        </v-btn>
        
        <v-btn
          color="error"
          @click="discardEnrollmentProgramAsk"
          v-if="showDiscardEnrollmentButton"
        >
          <v-icon left dark>mdi-close</v-icon>
          Изчисти класиране
        </v-btn>
        
        <v-menu offset-y v-if="showSendMessageButton">
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              color="secondary"
              dark
              v-bind="attrs"
              v-on="on"
              class="mr-1"
            >
              <template slot:append-icon>
                <v-icon left>mdi-email-arrow-right-outline</v-icon>
              </template>
              Изпращане
              <template slot:prepend-icon>
                <v-icon right>mdi-menu-down</v-icon>
              </template>
            </v-btn>
          </template>
          <v-list>
            <v-list-item
              link
              @click="sendEnrollMessageToSyndicsAsk"
              v-if="data.isEnrolled"
            >
              <v-list-item-title>На съобщение на синдиците по електронна поща и в профила на синдика за класиране на обучение</v-list-item-title>
            </v-list-item>
            <v-list-item
              link
              @click="sendProgramPublishedMessageToSyndicsAsk"
              v-if="data.published"
            >
              <v-list-item-title>На съобщение на синдиците по електронна поща и в профила на синдика за публуквана програма</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>

        <v-btn
          color="secondary"
          outlined
        >
          <v-icon left dark>mdi-printer</v-icon>
          Печат на програма
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="my-10">
      <v-col cols="12">
        <base-material-card 
          icon="mdi-school-outline"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            Програма
          </template>

          <template v-slot:after-heading-button>
            <v-checkbox
              label="Публикувана програма"
              v-model="data.published"
              :true-value="true"
              :false-value="false"
              dense
              readonly
              class="d-inline-block mr-10"
            />
        
            <v-btn
              color="primary"
              @click="save"
            >
              <v-icon left dark>mdi-check</v-icon>
              Запази
            </v-btn>
          </template>

          <v-form
            ref="form"
            lazy-validation
            class="pa-3"
          >
            <v-row>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-material-date-picker
                  label="От дата"
                  v-model="data.fromDate"
                  :rules="[rules.required]"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-material-date-picker
                  label="До дата"
                  v-model="data.toDate"
                  :rules="[rules.required]"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-input
                  label="Заповед номер МП"
                  v-model="data.mojorderNumber"
                  :rules="[rules.required]"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-material-date-picker
                  label="Заповед дата МП"
                  v-model="data.mojorderDate"
                  :rules="[rules.required]"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-input
                  label="Заповед номер МИИ"
                  v-model="data.moeorderNumber"
                  :rules="[rules.required]"
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-material-date-picker
                  label="Заповед дата МИИ"
                  v-model="data.moeorderDate"
                  :rules="[rules.required]"
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Обяснения"
                  v-model="data.description"
                  :rules="[rules.required]"
                  dense
                  rows="2"
                  auto-grow
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Допълнителни обяснения"
                  v-model="data.additionalDescription"
                  :rules="[rules.required]"
                  dense
                  rows="2"
                  auto-grow
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Бележки"
                  v-model="data.notes"
                  :rules="[rules.required]"
                  dense
                  rows="2"
                  auto-grow
                />
              </v-col>
            </v-row>
          </v-form>
        </base-material-card>

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
                  <v-col cols="12" md="6" class="text-right" v-if="tab.key === 'courses'">
                    <v-btn color="primary" small @click="newCourse">
                      <v-icon left> mdi-plus </v-icon>
                      Добави обучение
                    </v-btn>
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
      </v-col>
    </v-row>

    <course-modal ref="courseModal" :program-data="data" @reloadData="getProgramCoursesData" @previewApplication="previewApplication" />
    <syndic-training-modal ref="syndicTrainingModal" :syndic-data="data" :is-readonly="true" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, formatDateTime } from '@/utils';
import ProgramModel from '@/models/trainings/ProgramModel';
import { CourseModal, SyndicTrainingModal } from "@/components";
import { 
  apiGetProgramById, 
  apiGetCreateProgram, 
  apiGetUpdateProgram, 
  apiGetProgramCourses, 
  apiGetProgramCourseApplications, 
  apiPublishProgram, 
  apiEnrollCourseParticipants, 
  apiDiscardCourseParticipants, 
  apiGetProgramEnrolledParticipants, 
  apiGetProgramDiscardedParticipants,
  apiSendPublishedProgramEmail,
  apiSendEnrolledSyndicsEmail
} from "@/api/programs";
export default {
  name: "trainingPreview",
  components: {
    CourseModal,
    SyndicTrainingModal
  },
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {
        inspectionTypes: [],
      },
      data: Object.assign({}, new ProgramModel()),
      tables: {
        courses: {
          headers: [
            { title: "Вид", field: "courseKindName", sortable: true },
            { title: "Тема", field: "topic", sortable: true },
            { title: "Лектори", field: "lecturers", sortable: true },
            { title: "От дата 1", field: "fromDate1", type: "date", format: "{0:dd.MM.yyyy}", sortable: true },
            { title: "До дата 1", field: "toDate1", type: "date", format: "{0:dd.MM.yyyy}", sortable: true },
            { title: "От дата 2", field: "fromDate2", type: "date", format: "{0:dd.MM.yyyy}", sortable: true },
            { title: "До дата 2", field: "toDate2", type: "date", format: "{0:dd.MM.yyyy}", sortable: true },
            { title: "Продълж. 1", field: "lengthDate1", sortable: true },
            { title: "Продълж. 2", field: "lengthDate2", sortable: true },
            { title: "Място", field: "", sortable: true },
            { title: "Макс. брой участници", field: "maximumParticipants", sortable: true },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                {
                  title: "Преглед",
                  icon: "mdi-text-box-search-outline",
                  color: "white",
                  class: "primary",
                  click: "previewCourse",
                },
              ],
            },
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
        participants: {
          headers: [
            { title: "Име", field: "syndicName", sortable: true },
            { title: "Регистрирация на", field: "dateCreated", type: 'date', format: '{0:dd.MM.yyyy}', sortable: true },
            { title: "Основно обучение", field: "mainCourseName", sortable: true },
            { title: "Алтернативно обучение", field: "alternateCourseName", sortable: true },
            //{ title: "Преминал разпр.", field: "", sortable: true },
            //{ title: "Класиран", field: "", sortable: true },
            //{ title: "Забел.", field: "", sortable: true },
            // {
            //   title: "",
            //   cell: "actions",
            //   filterable: false,
            //   width: "50px",
            //   sortable: false,
            //   buttons: [
            //     {
            //       title: "Преглед",
            //       icon: "mdi-text-box-search-outline",
            //       color: "white",
            //       class: "primary",
            //       click: "preview",
            //     },
            //   ],
            // },
          ],
          data: null,
          sort: {},
          pagination: {
            page: 1,
            skip: 0,
            take: 20,
            perPageOptions: [5, 10, 20, 50, 100],
            total: 0,
          },
        },
        rankedParticipants: {
          headers: [
            { title: "Име", field: "syndicName", sortable: true },
            { title: "Регистрирация на", field: "dateCreated", type: 'date', format: '{0:dd.MM.yyyy}',sortable: true },
            { title: "Основно обучение", field: "mainCourseName", sortable: true },
            { title: "Алтернативно обучение", field: "alternateCourseName", sortable: true },
            //{ title: "Преминал разпр.", field: "", sortable: true },
            //{ title: "Класиран", field: "", sortable: true },
            { title: "Обуч. за които е класиран", field: "courseName", sortable: true },
            // {
            //   title: "",
            //   cell: "actions",
            //   filterable: false,
            //   width: "50px",
            //   sortable: false,
            //   buttons: [
            //     {
            //       title: "Преглед",
            //       icon: "mdi-text-box-search-outline",
            //       color: "white",
            //       class: "primary",
            //       click: "preview",
            //     },
            //   ],
            // },
          ],
          data: null,
          sort: {},
          pagination: {
            page: 1,
            skip: 0,
            take: 20,
            perPageOptions: [5, 10, 20, 50, 100],
            total: 0,
          },
        },
        unrankedParticipants: {
          headers: [
            { title: "Име", field: "syndicName", sortable: true },
            { title: "Регистрирация на", field: "dateCreated", type: 'date', format: '{0:dd.MM.yyyy}', sortable: true },
            { title: "Основно обучение", field: "mainCourseName", sortable: true },
            { title: "Алтернативно обучение", field: "alternateCourseName", sortable: true },
            //{ title: "Преминал разпр.", field: "", sortable: true },
            //{ title: "Класиран", field: "", sortable: true },
            //{ title: "Обуч. за които е класиран", field: "", sortable: true },
            // {
            //   title: "",
            //   cell: "actions",
            //   filterable: false,
            //   width: "50px",
            //   sortable: false,
            //   buttons: [
            //     {
            //       title: "Преглед",
            //       icon: "mdi-text-box-search-outline",
            //       color: "white",
            //       class: "primary",
            //       click: "preview",
            //     },
            //   ],
            // },
          ],
          data: null,
          sort: {},
          pagination: {
            page: 1,
            skip: 0,
            take: 20,
            perPageOptions: [5, 10, 20, 50, 100],
            total: 0,
          },
        }
      },
      currentTab: 0,
      tabs: [
        { key: 'courses', name: "Обучения" },
        { key: 'participants', name: "Записани участници" },
        { key: 'rankedParticipants', name: "Класирани" },
        { key: 'unrankedParticipants', name: "Некласирани" },
      ],
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
    this.isNewDoc = true;
    this.data = Object.assign({}, new ProgramModel());
    if(this.$route.params.id){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
      this.getData();
    }
  },
  computed: {
    ...mapGetters([
      'currentUser'
    ]),
    getCurrentTabDetails(){
      if(this.tabs.length && this.tabs[this.currentTab]) 
        return this.tabs[this.currentTab];

      return null;
    },
    showEnrollmentButton(){
      return !this.isNewDoc && !this.data.isEnrolled && this.data.published;
    },
    showDiscardEnrollmentButton(){
      return !this.isNewDoc && this.data.isEnrolled;
    },
    showPublishButton(){
      return !this.isNewDoc && !this.data.published;
    },
    showSendMessageButton(){
      return !this.isNewDoc && (this.data.isEnrolled || this.data.published);
    }
  },
  methods: {
    getData(loadProgramCourses = true){
      if(!this.isNewDoc)
        apiGetProgramById(this.data.id).then(data => {
          this.data = data;

          if(loadProgramCourses)
            this.getProgramCoursesData();
        })
    },
    save(){
      if(this.$refs.form.validate()){
        if(this.isNewDoc){
          apiGetCreateProgram(this.data).then(result => {
            if(result && result.length)
              this.$router.push({path: `/trainings/${result}`})
          })
        } else {
          apiGetUpdateProgram(this.data)
        }
      }
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "previewCourse":
          this.$refs.courseModal.openModal(rowData.id);
        break;
      }
    },
    newCourse(){
      this.$refs.courseModal.openModal();
    },
    getProgramCoursesData(){
      let query = Object.assign({}, {});
      
      query.id = this.data.id;
      query.pageNumber = this.tables.courses.pagination.skip / this.tables.courses.pagination.take + 1;
      query.pageSize = this.tables.courses.pagination.take;

      apiGetProgramCourses(query).then((result) => {
        this.tables.courses.data = result.items;
        this.tables.courses.pagination.total = result.totalCount;
        this.tables.courses.pagination.page = result.currentPage;
      });
    },
    getRegisteredParticipants(){
      let query = Object.assign({}, {});
      
      query.id = this.data.id;
      query.pageNumber = this.tables.participants.pagination.skip / this.tables.participants.pagination.take + 1;
      query.pageSize = this.tables.participants.pagination.take;

      apiGetProgramCourseApplications(query).then((result) => {
        this.tables.participants.data = result.items;
        this.tables.participants.pagination.total = result.totalCount;
        this.tables.participants.pagination.page = result.currentPage;
      });
    },
    getRankedParticipants(){
      let query = Object.assign({}, {});
      
      query.id = this.data.id;
      query.pageNumber = this.tables.rankedParticipants.pagination.skip / this.tables.rankedParticipants.pagination.take + 1;
      query.pageSize = this.tables.rankedParticipants.pagination.take;

      apiGetProgramEnrolledParticipants(query).then((result) => {
        this.tables.rankedParticipants.data = result.items;
        this.tables.rankedParticipants.pagination.total = result.totalCount;
        this.tables.rankedParticipants.pagination.page = result.currentPage;
      });
    },
    getUnrankedParticipants(){
      let query = Object.assign({}, {});
      
      query.id = this.data.id;
      query.pageNumber = this.tables.unrankedParticipants.pagination.skip / this.tables.unrankedParticipants.pagination.take + 1;
      query.pageSize = this.tables.unrankedParticipants.pagination.take;

      apiGetProgramDiscardedParticipants(query).then((result) => {
        this.tables.unrankedParticipants.data = result.items;
        this.tables.unrankedParticipants.pagination.total = result.totalCount;
        this.tables.unrankedParticipants.pagination.page = result.currentPage;
      });
      
    },
    getTablesData(){
      switch(this.currentTab){
        case 'courses':
          this.getProgramCoursesData()
        break;
        case 'participants':
          this.getRegisteredParticipants()
        break;
        case 'rankedParticipants':
          this.getRankedParticipants()
        break;
        case 'unrankedParticipants':
          this.getUnrankedParticipants()
        break;
      }
    },
    tabChange(tabKey, force = false) {
      if(tabKey && this.tables[tabKey] && this.tables[tabKey].data === null || force)
        switch (tabKey) {
          case "courses":
            this.getProgramCoursesData()
          break;
          case "participants":
            this.getRegisteredParticipants()
          break;
          case "rankedParticipants":
            this.getRankedParticipants()
          break;
          case "unrankedParticipants":
            this.getUnrankedParticipants()
          break;
        }
    },
    publishProgramAsk(){
      let confirmData = {
        title: "Публикуване на програма",
        body: "Сигурни ли сте, че искате да публикувате програмата?",
        callback: this.publishProgram
      };

      this.$refs.confirmModal.openModal(confirmData);
    },
    publishProgram(){
      apiPublishProgram(this.data.id).then((result) => {
        if(result)
          this.getData();

        this.$refs.confirmModal.closeModal();
      })
    },
    enrollProgramAsk(){
      let confirmData = {
        title: "Класиране на участници",
        body: "Сигурни ли сте, че искате да класирате участниците?",
        callback: this.enrollProgram
      };

      this.$refs.confirmModal.openModal(confirmData);
    },
    enrollProgram(){
      apiEnrollCourseParticipants(this.data.id).then((result) => {
        if(result){
          this.getData(false);
          this.tables.rankedParticipants.data = null;
          this.tables.unrankedParticipants.data = null;
          this.getTablesData();
          this.$refs.confirmModal.closeModal();
        }
      })
    },
    discardEnrollmentProgramAsk(){
      let confirmData = {
        title: "Изчистване на класиране на участници",
        body: "Сигурни ли сте, че искате да изчистите класираните участници?",
        callback: this.discardEnrollment
      };

      this.$refs.confirmModal.openModal(confirmData);
    },
    discardEnrollment(){
      apiDiscardCourseParticipants(this.data.id).then((result) => {
        if(result){
          this.getData(false);
          this.tables.rankedParticipants.data = null;
          this.tables.unrankedParticipants.data = null;
          this.getTablesData();
          this.$refs.confirmModal.closeModal();
        }
      })
    },
    sendEnrollMessageToSyndicsAsk(){
      let confirmData = {
        title: "Изпращане на съобщение до синдиците",
        body: "Сигурни ли сте, че искате да изпратите съобщение до синдиците за тяхното класиране?",
        callback: this.sendEnrollMessageToSyndics
      };

      this.$refs.confirmModal.openModal(confirmData);
    },
    sendEnrollMessageToSyndics(){
      apiSendEnrolledSyndicsEmail(this.data.id).then((result) => {
        if(result)
          this.$refs.confirmModal.closeModal();
      })
    },
    sendProgramPublishedMessageToSyndicsAsk(){
      let confirmData = {
        title: "Изпращане на съобщение до синдиците",
        body: "Сигурни ли сте, че искате да изпратите съобщение до синдиците за публикувана програма?",
        callback: this.sendProgramPublishedMessageToSyndics
      };

      this.$refs.confirmModal.openModal(confirmData);
    },
    sendProgramPublishedMessageToSyndics(){
      apiSendPublishedProgramEmail(this.data.id).then((result) => {
        if(result)
          this.$refs.confirmModal.closeModal();
      })
    },
    previewApplication(id){
      this.$refs.syndicTrainingModal.openModal(id)
    },
    ISODateString: ISODateString,
    formatDateTime: formatDateTime
  }
}
</script>