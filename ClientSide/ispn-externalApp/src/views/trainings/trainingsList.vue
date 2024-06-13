<template>
  <v-container fluid tag="section" class="full-height" v-if="isSyndic">
    <base-v-component :heading="$route.name" />


    <base-material-card
      color="primary"
      icon="mdi-school-outline"
      inline
      class="px-5 py-5 mt-10"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $route.meta.title }}</div>
      </template>

      <base-material-tabs 
        v-model="currentTab"
        color="primary" 
        fixed-tabs 
        slider-size="3" 
        @change="tabChange" 
      >
        <v-tabs-slider color="secondary" />

        <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key">
          {{ tab.name }}
        </v-tab>

        <v-tabs-items v-model="currentTab">
          <v-tab-item v-for="tab in tabs" :key="tab.key" :value="tab.key" class="mt-10">
            <v-row v-if="tab.key === 'courseApplications'">
              <v-col cols="12" class="text-right">
                <v-btn
                  color="primary"
                  @click="newCourseApplication"
                  class="d-inline-block"
                >
                  <v-icon left dark>
                    mdi-plus
                  </v-icon>
                  Нова заявка
                </v-btn>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12">
                <base-kendo-grid
                  :columns="tables[tab.key].headers"
                  :items="tables[tab.key].data"
                  :pagination="tables[tab.key].pagination"
                  sortable
                  :sort.sync="tables[tab.key].sort"
                  @getData="getTablesData"
                  @click="tableActions"
                />
              </v-col>
            </v-row>
          </v-tab-item>
        </v-tabs-items>
      </base-material-tabs>
    </base-material-card>
    <course-application-modal ref="courseApplicationModal" @update="getTablesData"/>
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import { apiGetPrograms } from "@/api/programs";
import { apiGetSyndicCourseApplications, apiGetSyndicCourseResults, apiGetUserCourseParticipations } from "@/api/courses";
import { CourseApplicationModal } from "@/components";
export default {
  name: "trainingsList",
  components: {
    CourseApplicationModal
  },
  data: () => ({
    currentTab: 'trainingPrograms',
    tabs: [
      { name: 'Програми', key: 'trainingPrograms' },
      { name: 'Заявки', key: 'courseApplications' },
      { name: 'Резултати', key: 'courseResults' },
      { name: 'Текущи обучения', key: 'currentTrainings' },
    ],
    tables: {
      trainingPrograms: {
        headers: [
          { title: "От дата", field: "fromDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: true, width: '110px' },
          { title: "До дата", field: "toDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: true, width: '110px' },
          { title: "Обяснения", field: "description", sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline fs18", color: "white", class: "transparent primary--text", click: "previewProgram" },
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
      courseApplications: {
        headers: [
          { title: "Програма", field: "programDescription", sortable: true },
          { title: "Основно обучение", field: "mainCourseName", sortable: true },
          { title: "Алтернативно обучение", field: "alternateCourseName", sortable: true },
          { title: "Актуален e-mail", field: "actualEmail", sortable: true },
          { title: "Актуален телефон", field: "actualPhone", sortable: false },
          { title: "Дата на записване", field: "dateCreated", type: 'date', format: '{0:dd.MM.yyyy HH:mm}', sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline fs18", color: "white", class: "transparent primary--text", click: "previewCourseApplication" },
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
      courseResults: {
        headers: [
          { title: "Вид", field: "courseKindName", sortable: true },
          { title: "Тема", field: "courseTopic", sortable: true },
          { title: "Основно обучение", field: "mainCourseName", sortable: true },
          { title: "Лектор осн. об.", field: "lecturerDate1", cell: 'trueOrFalse', sortable: true, className: 'text-center'},
          { title: "Алтернативно обучение", field: "alternateCourseName", sortable: true },
          { title: "Лектор алт. об.", field: "lecturerDate2", cell: 'trueOrFalse', sortable: true, className: 'text-center' },
          { title: "Преминал", field: "passedTheCourse", cell: 'status', sortable: true, className: 'text-center', headerClassName: 'text-center'},
          // {
          //   title: "",
          //   cell: "actions",
          //   filterable: false,
          //   width: "50px",
          //   sortable: false,
          //   buttons: [
          //     { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewResult" },
          //   ],
          // },
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
      currentTrainings: {
        headers: [
          { title: "Вид обучение", field: "courseKindName", sortable: false },
          { title: "Тема на курса", field: 'topic', sortable: false },
          { title: "Програма", field: "programDescription", sortable: false },
          { title: "Дата на провеждане", field: "", sortable: false },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewCourse" },
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
    }
  }),
  computed: {
    ...mapGetters(["isSyndic"]),
  },
  mounted() {
    this.getTablesData();
  },
  watch: {
  },
  methods: {
    getTablesData(){
      switch(this.currentTab){
        case 'trainingPrograms':
          this.getTrainingProgramsData()
        break;
        case 'courseApplications':
          this.getCourseApplicationsData()
        break;
        case 'courseResults':
          this.getCourseResultsData()
        break;
        case 'currentTrainings':
          this.getCurrentTrainingsData()
        break;
      }
    },
    getTrainingProgramsData() {
      let query = Object.assign({}, {});
      query.pageNumber = this.tables['trainingPrograms'].pagination.skip / this.tables['trainingPrograms'].pagination.take + 1;
      query.pageSize = this.tables['trainingPrograms'].pagination.take;
      
      apiGetPrograms(query).then((result) => {
        this.tables['trainingPrograms'].data = result.items;
        this.tables['trainingPrograms'].pagination.total = result.totalCount;
        this.tables['trainingPrograms'].pagination.page = result.currentPage;
      });
    },
    getCourseApplicationsData() {
      let query = Object.assign({}, {});

      query.pageNumber = this.tables['courseApplications'].pagination.skip / this.tables['courseApplications'].pagination.take + 1;
      query.pageSize = this.tables['courseApplications'].pagination.take;
      
      apiGetSyndicCourseApplications(query).then((result) => {
        this.tables['courseApplications'].data = result.items;
        this.tables['courseApplications'].pagination.total = result.totalCount;
        this.tables['courseApplications'].pagination.page = result.currentPage;
      });
    },
    getCourseResultsData(){
      let query = Object.assign({}, {});
      query.pageNumber = this.tables['courseResults'].pagination.skip / this.tables['courseResults'].pagination.take + 1;
      query.pageSize = this.tables['courseResults'].pagination.take;
      
      apiGetSyndicCourseResults(query).then((result) => {
        this.tables['courseResults'].data = result.items;
        this.tables['courseResults'].pagination.total = result.totalCount;
        this.tables['courseResults'].pagination.page = result.currentPage;
      });
    },
    getCurrentTrainingsData(){
      let query = Object.assign({}, {});
      query.pageNumber = this.tables['currentTrainings'].pagination.skip / this.tables['currentTrainings'].pagination.take + 1;
      query.pageSize = this.tables['currentTrainings'].pagination.take;
      
      apiGetUserCourseParticipations(query).then((result) => {
        this.tables['currentTrainings'].data = result.items;
        this.tables['currentTrainings'].pagination.total = result.totalCount;
        this.tables['currentTrainings'].pagination.page = result.currentPage;
      });
    },
    previewProgram(item) {
      this.$router.push({ path: `/trainings/programs/${item.id}` });
    },
    previewCourseApplication(item) {
      this.$refs.courseApplicationModal.openModal(item.id)
    },
    previewCourse(item) {
      this.$router.push({ path: `/trainings/${item.id}` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "previewProgram":
          this.previewProgram(rowData);
        break;
        case "previewCourseApplication":
          this.previewCourseApplication(rowData);
        break;
        case "previewCourse":
          this.previewCourse(rowData);
        break;
      }
    },
    tabChange(tabKey, force = false){      
      if((this.tables[tabKey] && !this.tables[tabKey].data.length) || force)
        switch (tabKey) {
          case "trainingPrograms":
            this.getTrainingProgramsData();
          break;
          case "courseApplications":
            this.getCourseApplicationsData();
          break;
          case "courseResults":
            this.getCourseResultsData();
          break;
          case "currentTrainings":
            this.getCurrentTrainingsData();
          break;
        }
    },
    newCourseApplication(){
      this.$refs.courseApplicationModal.openModal();
    },
  },
};
</script>
