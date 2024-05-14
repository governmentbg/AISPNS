<template>
  <div>
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
                  v-model="data.courseKindId"
                  :items="nomenclatures.courseKinds"
                  :rules=[rules.required]
                  item-text="name"
                  item-value="id"
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
                  v-model="data.lecturers"
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
                  v-model="data.maximumParticipants"
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
                  v-model="data.lengthDate1"
                  :rules=[rules.required]
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  label="Област"
                  class="pb-0"
                  v-model="data.address1Navigation.regionId"
                  :items="nomenclatures.address1Navigation.districts"
                  item-text="name"
                  item-value="id"
                  @change="getMunicipalities('address1Navigation')"
                  clearable
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  v-model="data.address1Navigation.municipalityId"
                  label="Община"
                  :items="nomenclatures.address1Navigation.municipalities"
                  item-text="name"
                  item-value="id"
                  class="pb-0"
                  @change="getSettlements('address1Navigation')"
                  clearable
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  label="Населено място"
                  class="pb-0"
                  v-model="data.address1Navigation.settlementId"
                  :items="nomenclatures.address1Navigation.settlements"
                  item-text="name"
                  item-value="id"
                  clearable
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address1Navigation.postCode"
                  label="ПК"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="5" lg="6" md="4">
                <v-text-field
                  v-model="data.address1Navigation.streetName"
                  label="Улица"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address1Navigation.streetNumber"
                  label="Улица №"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address1Navigation.buildingNumber"
                  label="Сграда №"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address1Navigation.entranceNumber"
                  label="Вход"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address1Navigation.floorNumber"
                  label="Етаж"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address1Navigation.apartmentNumber"
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
                  v-model="data.lengthDate2"
                  :rules=[rules.required]
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  label="Област"
                  class="pb-0"
                  v-model="data.address2Navigation.regionId"
                  :items="nomenclatures.address2Navigation.districts"
                  item-text="name"
                  item-value="id"
                  @change="getMunicipalities('address2Navigation')"
                  clearable
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  v-model="data.address2Navigation.municipalityId"
                  label="Община"
                  :items="nomenclatures.address2Navigation.municipalities"
                  item-text="name"
                  item-value="id"
                  class="pb-0"
                  @change="getSettlements('address2Navigation')"
                  clearable
                />
              </v-col>
              <v-col cols="12" xl="2" lg="4" md="4">
                <base-autocomplete
                  label="Населено място"
                  class="pb-0"
                  v-model="data.address2Navigation.settlementId"
                  :items="nomenclatures.address2Navigation.settlements"
                  item-text="name"
                  item-value="id"
                  clearable
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address2Navigation.postCode"
                  label="ПК"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="5" lg="6" md="4">
                <v-text-field
                  v-model="data.address2Navigation.streetName"
                  label="Улица"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address2Navigation.streetNumber"
                  label="Улица №"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address2Navigation.buildingNumber"
                  label="Сграда №"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address2Navigation.entranceNumber"
                  label="Вход"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address2Navigation.floorNumber"
                  label="Етаж"
                  color="secondary"
                  dense
                />
              </v-col>
              <v-col cols="12" xl="1" lg="2" md="2">
                <v-text-field
                  v-model="data.address2Navigation.apartmentNumber"
                  label="Ап. №"
                  color="secondary"
                  dense
                />
              </v-col>
            </v-row>
          </v-form>


          <base-material-card 
            icon="mdi-file-multiple-outline"
            color="primary"
            class="elevation-3 mt-15"
            v-if="!isNewDoc"
          >
            <template v-slot:after-heading>
              Файлове
            </template>

            <template v-slot:after-heading-button>
              <v-btn
                class="primary"
                @click="addNewFile"
              >
                <v-icon left dark>mdi-plus</v-icon>
                Нов файл
              </v-btn>
            </template>

            <base-kendo-grid
              :columns="tables.files.headers"
              :items="tables.files.data"
              :hasPaging="false"
              @click="tableActions"
            />
          </base-material-card>

          
          <v-tabs
            v-model="currentTab"
            fixed-tabs
            slider-color="primary"
            @change="tabChange"
            slider-size="3"
            class="mt-10"
            v-if="!isNewDoc"
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
                  <template v-if="tab.key === 'trainingResults'">
                  <v-row v-if="false">
                    <v-col cols="12" class="text-right mb-5 mt-2">
                      <v-btn color="primary">Отрязяване на резултати</v-btn>
                    </v-col>
                  </v-row>
                  </template>
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
          <v-btn color="primary" @click="save(false)"> Запази </v-btn>
          <v-btn color="secondary" @click="save(true)"> Запази и затвори </v-btn>
          <v-btn color="primary" outlined @click="open = false">
            Затвори
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <course-file-upload-modal ref="fileUploadModal" :parent-data="data" @reloadData="getDocumentsData" @setDocumentCollectionId="setDocumentCollectionId"/>
  </div>
</template>

<script>
import TrainingModel from "@/models/trainings/TrainingModel";
import { apiGetCourseById, apiCreateCourse, apiUpdateCourse, apiGetEnrolledDate1, apiGetEnrolledDate2, apiGetEnrolledParticipants, apiMarkCourseParticipantCourseAsPassed, apiMarkCourseParticipantCourseAsNotPassed } from "@/api/courses"
import { apiMetaDataGetCourseKinds, apiMetaDataGetDistricts, apiMetaDataGetMunicipalities, apiMetaDataGetSettlements } from "@/api/metaData";
import { apiGetCourseApplications } from "@/api/courses";
import { apiDeleteDocument, apiDownloadDocument, apiGetAllDocumentFiles } from "@/api/documents";
import { bytesToSize, downloadFileFromResponse } from "@/utils";
import CourseFileUploadModal from "@/components/modals/trainings/courseFileUploadModal.vue";
export default {
  name: "trainingModal",
  components: {
    CourseFileUploadModal
  },
  props: {
    programData: {
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
        address1Navigation: {
          districts: [],
          municipalities: [],
          settlements: []
        },
        address2Navigation: {
          districts: [],
          municipalities: [],
          settlements: []
        },
        courseKinds: [],
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
            { title: "Име", field: "syndicName", sortable: true },
            { title: "Регистрирация на", field: "dateCreated", type: "date", format: '{0:dd.MM.yyyy}', sortable: true },
            { title: "Основно обучение", field: "mainCourseName", sortable: true },
            //{ title: "Резерв. осн. об.", field: "", sortable: true },
            { title: "Лектор осн. об.", field: "lecturerDate1", cell: 'trueOrFalse', sortable: true, className: 'text-center'},
            { title: "Алтернативно обучение", field: "alternateCourseName", sortable: true },
            { title: "Лектор алт. об.", field: "lecturerDate2", cell: 'trueOrFalse', sortable: true, className: 'text-center' },
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
                  click: "previewApplication",
                },
              ],
            },
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
        rankedParticipantsFirstDate: {
          headers: [
            { title: "Име", field: "syndicName", sortable: true },
            { title: "Регистрирация на", field: "dateCreated", type: "date", format: '{0:dd.MM.yyyy}', sortable: true },
            { title: "Основно обучение", field: "mainCourseName", sortable: true },
            //{ title: "Резерв. осн. об.", field: "", sortable: true },
            { title: "Лектор осн. об.", field: "lecturerDate1", cell: 'trueOrFalse', sortable: true, className: 'text-center'},
            { title: "Алтернативно обучение", field: "alternateCourseName", sortable: true },
            { title: "Лектор алт. об.", field: "lecturerDate2", cell: 'trueOrFalse', sortable: true, className: 'text-center' },
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
        rankedParticipantsSecondDate: {
          headers: [
            { title: "Име", field: "syndicName", sortable: true },
            { title: "Регистрирация на", field: "dateCreated", type: "date", format: '{0:dd.MM.yyyy}', sortable: true },
            { title: "Основно обучение", field: "mainCourseName", sortable: true },
            //{ title: "Резерв. осн. об.", field: "", sortable: true },
            { title: "Лектор осн. об.", field: "lecturerDate1", cell: 'trueOrFalse', sortable: true, className: 'text-center'},
            { title: "Алтернативно обучение", field: "alternateCourseName", sortable: true },
            { title: "Лектор алт. об.", field: "lecturerDate2", cell: 'trueOrFalse', sortable: true, className: 'text-center' },
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
        trainingResults: {
          headers: [
            { title: "Име", field: "syndicName", sortable: true },
            { title: "Регистрирация на", field: "dateCreated", type: "date", format: '{0:dd.MM.yyyy}', sortable: true },
            { title: "Основно обучение", field: "mainCourseName", sortable: true },
            //{ title: "Резерв. осн. об.", field: "", sortable: true },
            { title: "Лектор осн. об.", field: "lecturerDate1", cell: 'trueOrFalse', sortable: true, className: 'text-center'},
            { title: "Алтернативно обучение", field: "alternateCourseName", sortable: true },
            { title: "Лектор алт. об.", field: "lecturerDate2", cell: 'trueOrFalse', sortable: true, className: 'text-center' },
            { title: "Преминал", field: "passedTheCourse", cell: 'status', sortable: true, className: 'text-center', headerClassName: 'text-center'},
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "80px",
              sortable: false,
              buttons: [
                {
                  title: "Преминал",
                  icon: "mdi-check",
                  color: "white",
                  class: "primary mr-1",
                  click: "participantPassed",
                },
                {
                  title: "Непреминал",
                  icon: "mdi-close",
                  color: "white",
                  class: "error",
                  click: "participantNotPassed",
                },
              ],
            }
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
        files: {
          headers: [
            { title: "Файл", field: "fileName",  sortable: false },
            { title: "Описание", field: "description",  sortable: false },
            { title: "Размер", field: 'fileSize', cell: this.renderFileSize },
            { title: "Качен на", field: "blobDateCreated", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false, width: '100px' },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "80px",
              sortable: false,
              buttons: [
                {
                  title: "Свали",
                  icon: "mdi-download",
                  color: "white",
                  class: "primary mr-1",
                  click: "downloadFile",
                },
                {
                  title: "Изтрий",
                  icon: "mdi-trash-can-outline",
                  color: "white",
                  class: "secondary",
                  click: "deleteFile",
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
        }
      },
      rules: {
        required: (v) => !!v || v === 0 || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.$set(this, 'data', Object.assign({}, new TrainingModel()));
        this.$set(this.tables.rankedParticipantsFirstDate, 'data', null);
        this.$set(this.tables.rankedParticipantsSecondDate, 'data', null);
        this.$set(this.tables.trainingResults, 'data', null);
        this.$set(this.tables.trainingRequests, 'data', null);
        this.$set(this, 'currentTab', 'trainingRequests');
      } else {
        if(this.data.id) {
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
      apiGetCourseById(this.data.id).then((result) => {
        if(!result.address1Navigation)
          result.address1Navigation = {};

        if(!result.address2Navigation)
          result.address2Navigation = {};


        this.$set(this, "data", result);

        if(this.data.address1Navigation.regionId)
          this.getAddressNomenclatures('address1Navigation');

          
        if(this.data.address2Navigation?.regionId)
          this.getAddressNomenclatures('address2Navigation');
        
        this.getDocumentsData();
        
        this.isNewDoc = false;
        this.open = true;
      });
    },
    getCourseKinds(){
      apiMetaDataGetCourseKinds().then(result => {
        this.$set(this.nomenclatures, "courseKinds", result);
      });
    },
    getTrainingRequestsData(){
      let query = Object.assign({}, {});
      query.id = this.data.id;
      query.pageNumber = this.tables.trainingRequests.pagination.skip / this.tables.trainingRequests.pagination.take + 1;
      query.pageSize = this.tables.trainingRequests.pagination.take;

      apiGetCourseApplications(query).then((result) => {
        this.$set(this.tables.trainingRequests, 'data', result.items);
        this.$set(this.tables.trainingRequests.pagination, 'total', result.totalCount);
        this.$set(this.tables.trainingRequests.pagination, 'page', result.currentPage);
      });
    },
    getRankedParticipantsFirstDate(){
      let query = Object.assign({}, {});
      query.id = this.data.id;
      query.pageNumber = this.tables.rankedParticipantsFirstDate.pagination.skip / this.tables.rankedParticipantsFirstDate.pagination.take + 1;
      query.pageSize = this.tables.rankedParticipantsFirstDate.pagination.take;

      apiGetEnrolledDate1(query).then((result) => {
        this.$set(this.tables.rankedParticipantsFirstDate, 'data', result.items);
        this.$set(this.tables.rankedParticipantsFirstDate.pagination, 'total', result.totalCount);
        this.$set(this.tables.rankedParticipantsFirstDate.pagination, 'page', result.currentPage);
      });
    },
    getRankedParticipantsSecondDate(){
      let query = Object.assign({}, {});
      query.id = this.data.id;
      query.pageNumber = this.tables.rankedParticipantsSecondDate.pagination.skip / this.tables.rankedParticipantsSecondDate.pagination.take + 1;
      query.pageSize = this.tables.rankedParticipantsSecondDate.pagination.take;

      apiGetEnrolledDate2(query).then((result) => {
        this.$set(this.tables.rankedParticipantsSecondDate, 'data', result.items);
        this.$set(this.tables.rankedParticipantsSecondDate.pagination, 'total', result.totalCount);
        this.$set(this.tables.rankedParticipantsSecondDate.pagination, 'page', result.currentPage);
      });
    },
    getTrainingResultsData(){
      let query = Object.assign({}, {});
      query.id = this.data.id;
      query.pageNumber = this.tables.trainingResults.pagination.skip / this.tables.trainingResults.pagination.take + 1;
      query.pageSize = this.tables.trainingResults.pagination.take;

      apiGetEnrolledParticipants(query).then((result) => {
        this.$set(this.tables.trainingResults, 'data', result.items);
        this.$set(this.tables.trainingResults.pagination, 'total', result.totalCount);
        this.$set(this.tables.trainingResults.pagination, 'page', result.currentPage);
      });
      
    },
    getTablesData(){
      if(!this.isNewDoc)
        switch(this.currentTab){
          case 'trainingRequests':
            this.getTrainingRequestsData()
          break;
          case 'rankedParticipantsFirstDate':
            this.getRankedParticipantsFirstDate()
          break;
          case 'rankedParticipantsSecondDate':
            this.getRankedParticipantsSecondDate()
          break;
          case 'trainingResults':
            this.getTrainingResultsData()
          break;
        }
    },
    tabChange(tabKey, force = false) {
      if(!this.isNewDoc && tabKey && this.tables[tabKey] && this.tables[tabKey].data === null || force)
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
        case "participantPassed":
          this.markCourseParticipantAsPassed(rowData);
        break;
        case "participantNotPassed":
          this.markCourseParticipantAsNotPassed(rowData);
        break;
        case "downloadFile":
          this.download(rowData);
        break;
        case "deleteFile":
          this.deleteDocumentFileAsk(rowData);
        break;
        case "previewApplication":
          this.$emit('previewApplication', rowData.id);
        break;
      }
    },
    markCourseParticipantAsPassed(rowData){
      apiMarkCourseParticipantCourseAsPassed(rowData.id).then((result) => {
        if(result)
          this.getTrainingResultsData();
      });
    },
    markCourseParticipantAsNotPassed(rowData){
      apiMarkCourseParticipantCourseAsNotPassed(rowData.id).then((result) => {
        if(result)
          this.getTrainingResultsData();
      });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          let data = Object.assign({}, this.data);
          data.programId = this.programData.id
          apiCreateCourse(data).then((result) => {
            if (result && result !== '00000000-0000-0000-0000-000000000000') {
              this.data.id = result;
              this.getData();
              this.$emit("reloadData");
              if(closeModal)
                this.closeModal();
            }
          });
        } else {
          apiUpdateCourse(this.data).then((result) => {
            if (result && result.id) {
              this.$emit("reloadData");
              if(closeModal)
                this.closeModal();
            }
          });
        }
    },
    getAddressNomenclatures(addressNum){
      if(this.data[addressNum].regionId)
        apiMetaDataGetMunicipalities(this.data[addressNum].regionId).then((result) => {
          this.$set(this.nomenclatures[addressNum], "municipalities", result);

          if(this.data[addressNum].municipalityId)
            apiMetaDataGetSettlements(this.data[addressNum].municipalityId).then((result) => {
              this.$set(this.nomenclatures[addressNum], "settlements", result);
            });
        });
    },
    getDistricts(addressNum){
      apiMetaDataGetDistricts().then((result) => {
        if(addressNum){
          this.$set(this.nomenclatures[addressNum], "districts", result);
        } else {
          this.$set(this.nomenclatures.address1Navigation, "districts", result);
          this.$set(this.nomenclatures.address2Navigation, "districts", result);
        }
      });
    },
    getMunicipalities(addressNum) {
      apiMetaDataGetMunicipalities(this.data[addressNum].regionId).then((result) => {
        
        this.$set(this.nomenclatures[addressNum], "municipalities", result);

        this.$set(this.data[addressNum], "settlementId", null);
        this.$set(this.data[addressNum], "municipalityId", null);
      });
    },
    getSettlements(addressNum) {
      apiMetaDataGetSettlements(this.data[addressNum].municipalityId).then((result) => {
        this.$set(this.nomenclatures[addressNum], "settlements", result);
        this.$set(this.data[addressNum], "settlementId", null);
      });
    },
    openModal(id = null) {
      if(!this.nomenclatures.courseKinds.length)
        this.getCourseKinds();

      if(!this.nomenclatures.address1Navigation.districts.length)
        this.getDistricts();

      this.$set(this.tables.files, "data", []);
      
      if(id){
        this.data.id = id;
        this.getData();
        this.getTrainingRequestsData();
      } else {
        this.data = Object.assign({}, new TrainingModel());
        this.isNewDoc = true;
        this.open = true;
      }

      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    closeModal(){
      this.open = false;
    },

    setDocumentCollectionId(documentCollectionId){
      if(documentCollectionId)
        this.$set(this.data, 'documentCollectionId', documentCollectionId);
    },
    getDocumentsData(reloadParentData = false){
      if(this.data.documentCollectionId){
        let query = Object.assign({}, {});
        
        query.pageNumber = this.tables.files.pagination.skip / this.tables.files.pagination.take + 1;
        query.pageSize = this.tables.files.pagination.take;
        query.filter = [this.data.documentCollectionId]

        if(reloadParentData)
          this.$emit('reloadData');
        
        apiGetAllDocumentFiles(query).then((result) => {
          this.$set(this.tables.files, 'data', result.items);
          this.$set(this.tables.files.pagination, 'total', result.totalCount);
          this.$set(this.tables.files.pagination, 'page', result.currentPage);
        });
      }
    },
    addNewFile(){
      this.$refs.fileUploadModal.openModal()
    },
    renderFileSize(h, tdElement, props) {
      let item = props.dataItem;
      return h('td', null, [
        item.fileSize ? bytesToSize(item.fileSize) : ''
      ]);
    },
    deleteDocumentFileAsk(item) {
      let confirmData = {
        title: "Изтриване на файл",
        body: `Сигурни ли сте, че искате да изтриете избрания файл?`,
        callback: this.deleteDocumentFile,
        parameter: item.id,
      };
      this.$emit('confirm', confirmData);
    },
    deleteDocumentFile(fileId) {
      apiDeleteDocument(fileId).then((result) => {
        if (result) {
          this.getDocumentsData();
          this.$emit('reloadData');
          this.$emit('closeConfirm');
        }
      });
    },
    download(fileData){
      apiDownloadDocument(fileData.id).then(result => {
        downloadFileFromResponse(result);
      });
    },
  },
};
</script>

<style></style>
