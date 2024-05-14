<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isSyndic"
  >
    <base-v-component
      :heading="$route.meta.title"
    />

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

          <v-form
            ref="form"
            lazy-validation
            class="pa-2"
          >
            <v-row>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-material-date-picker
                  label="От дата"
                  v-model="data.fromDate"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-material-date-picker
                  label="До дата"
                  v-model="data.toDate"
                  readonly
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-input
                  label="Заповед номер МП"
                  v-model="data.mojorderNumber"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-material-date-picker
                  label="Заповед дата МП"
                  v-model="data.mojorderDate"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-input
                  label="Заповед номер МИИ"
                  v-model="data.moeorderNumber"
                  readonly
                />
              </v-col>
              <v-col cols="12" xl="2" lg="3" md="6">
                <base-material-date-picker
                  label="Заповед дата МИИ"
                  v-model="data.moeorderDate"
                  readonly
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Обяснения"
                  v-model="data.description"
                  rows="2"
                  auto-grow
                  readonly
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Допълнителни обяснения"
                  v-model="data.additionalDescription"
                  rows="2"
                  auto-grow
                  readonly
                />
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Бележки"
                  v-model="data.notes"
                  rows="2"
                  auto-grow
                  readonly
                />
              </v-col>
            </v-row>
          </v-form>
        </base-material-card>

        <base-material-card 
          icon="mdi-school-outline"
          color="primary"
          class="elevation-3 mt-10"
        >
          <template v-slot:after-heading>
            Обучения (курсове)
          </template>

          <v-container class="py-3">
            <base-kendo-grid
              :columns="table.headers"
              :items="table.data"
              :pagination="table.pagination"
              sortable
              :sort.sync="table.sort"
              @getData="getProgramCoursesData"
              @click="tableActions"
            />
          </v-container>
        </base-material-card>
      </v-col>
    </v-row>

    <training-modal ref="trainingModal" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString } from '@/utils';
import { TrainingModal } from "@/components";
import { apiGetProgramById, apiGetProgramCourses } from '@/api/programs';
export default {
  name: "trainingPreview",
  components: {
    TrainingModal
  },
  data(){
    return {
      isNewDoc: true,
      data: Object.assign({}, {}),
      table: {
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
          // {
          //   title: "",
          //   cell: "actions",
          //   filterable: false,
          //   width: "50px",
          //   sortable: false,
          //   buttons: [
          //     { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewTraining" },
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
      rules: {
        required: v => !!v || 'Полето е задължително.',
        requiredSelect: v => (typeof v === 'string' && v.length) || typeof v === 'boolean' || !isNaN(parseFloat(v)) && v >= 0 && v <= 999 || "Полето е задължително",
      },
    }
  },
  mounted(){
    // TODO: this.isNewDoc = true;
    this.isNewDoc = false;
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
    }

    this.getData();
  },
  computed: {
    ...mapGetters([
      'isSyndic',
      'currentUser'
    ]),
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetProgramById(this.data.id).then(result => {
          if(result){
            this.$set(this, 'data', result);
            this.getProgramCoursesData();
          }
        })
    },
    getProgramCoursesData(){
      let query = Object.assign({}, {});
      
      query.id = this.data.id;
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
      query.pageSize = this.table.pagination.take;

      apiGetProgramCourses(query).then((result) => {
        this.table.data = result.items;
        this.table.pagination.total = result.totalCount;
        this.table.pagination.page = result.currentPage;
      });
    },
    previewTraining(){

    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "previewTraining":
          this.previewTraining(rowData);
        break;
      }
    },
    ISODateString: ISODateString
  }
}
</script>