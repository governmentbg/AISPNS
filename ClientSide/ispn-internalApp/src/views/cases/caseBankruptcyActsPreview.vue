<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      heading="Преглед на книга по чл.634в"
      goBackText="Обратно към дела по несъстоятелност"
      goBackTo="/cases/bankruptcy"
    />

    <v-row class="mt-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          class="py-5 mb-5"
          color="secondary"
          @click="$router.push({path: `/cases/bankruptcy/${data.id}`})"
        >
          <v-icon left dark>mdi-gavel</v-icon>
          Към делото
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          color="secondary"
          @click="$router.push({path: `/cases/bankruptcy/${data.id}/book`})"
        >
          <v-icon left dark>mdi-book-open-variant</v-icon>
          Книга по чл.634в
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          dark
          color="blue-grey darken-1"
          @click="$router.push({path: `/cases/bankruptcy/${data.id}/registredEntries`})"
          v-if="data.isProprietor"
        >
          <v-icon left dark>mdi-book-edit-outline</v-icon>
          Данни за производството по несъстоятелност
        </v-btn>
        <v-btn
          class="py-5 mb-5"
          dark
          color="blue-grey darken-2 ml-1"
          @click="$router.push({path: `/cases/bankruptcy/${data.id}/actsAnnounced`})"
          v-if="data.isProprietor"
        >
          <v-icon left dark>mdi-message-alert-outline</v-icon>
          Обявени актове по несъстоятелност
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="d-print-none mt-10">
      <v-col cols="12" xl="6" offset-xl="3" lg="8" offset-lg="2" class="mx-auto">
        <base-case-card :data="data" />
      </v-col>

      <v-col cols="12">
        <base-material-card
          color="primary"
          icon="mdi-ticket"
          inline
          class="px-5 py-5 mt-5"
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">Актове подлежащи на обжалване</div>
          </template>

          <base-kendo-grid
            :columns="table.headers"
            :items="table.data"
            :pagination="table.pagination"
            sortable
            :sort.sync="table.sort"
            @getData="getCaseActsData"
          />
        </base-material-card>
      </v-col>

    </v-row>
  </v-container>
</template>

<script>
import { ISODateString, isValidGUID } from '@/utils';
import { apiGetCaseById, apiGetCaseActsByCaseId } from '@/api/cases';
export default {
  name: "previewCaseActs",
  components: {
  },
  data(){
    return {
      data: {},
      table: {
        headers: [
          { title: "Актове подлежащи на обжалване", field: "actType", sortable: true },
          { title: "Дата на обявяване/вписване", field: "fieldActionDate", type: "date", format: "{0:dd.MM.yyyy}", sortable: true },
          { title: "Срок на обжалване", field: "actComplaintTerm", sortable: true },
          { title: "Съд за обжалване", field: "courtName", sortable: true },
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
    }
  },
  mounted(){
    if(!isValidGUID(this.$route.params.id)){
      this.$router.push('/cases/bankruptcy/by-case')
    } else {
      this.data.id = this.$route.params.id;

      this.getData();
    }
  },
  computed: {
  },
  methods: {
    getData(){
      apiGetCaseById(this.data.id).then(result => {
        this.$set(this, "data", result);
      
        this.getCaseActsData();
      })
    },
    getCaseActsData(){
      apiGetCaseActsByCaseId(this.data.id).then(result => {
        this.$set(this.table, "data", result);
      })
    },
    ISODateString: ISODateString
  }
}
</script>