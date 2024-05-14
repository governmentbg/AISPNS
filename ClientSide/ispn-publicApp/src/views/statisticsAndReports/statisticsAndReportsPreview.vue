<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <v-row class="my-5">
      <v-col cols="12" lg="8" offset-lg="2" class="mx-auto">
        <base-material-card 
          icon="mdi-chart-multiple"
          color="primary"
          class="elevation-3"
        >
          <template v-slot:after-heading>
            {{ $t('statistics_and_reports') }}
          </template>


          <v-container class="py-3">
            <v-simple-table class="table-presentation" v-if="!noResultsFound">
              <template v-slot:default>
                <tbody>
                  <tr>
                    <td width="300">{{ $t('ident_no') }}:</td>
                    <td>{{ data.identificationNumbr }}</td>
                  </tr>
                  <tr>
                    <td>{{ $t('type') }}:</td>
                    <td>{{ data.type }}</td>
                  </tr>
                  <tr>
                    <td>{{ $t('report_date') }}:</td>
                    <td>{{ ISODateString(data.date) }}</td>
                  </tr>
                  <tr>
                    <td>{{ $t('from_date') }}:</td>
                    <td>{{ ISODateString(data.fromDate) }}</td>
                  </tr>
                  <tr>
                    <td>{{ $t('to_date') }}:</td>
                    <td>{{ ISODateString(data.toDate) }}</td>
                  </tr>
                  <tr>
                    <td>{{ $t('source_of_information') }}:</td>
                    <td>{{ data.source }}</td>
                  </tr>
                  <tr>
                    <td>{{ $t('published_on_f') }}:</td>
                    <td>{{ ISODateString(data.published) }}</td>
                  </tr>
                  <tr>
                    <td>{{ $t('uploaded_documents') }}:</td>
                    <td>
                      <template v-for="file in data?.documentCollection?.documentContents">
                        <v-btn small text @click="download(file.id)">
                          <v-icon left>mdi-download</v-icon>
                          {{ file.fileName }}
                        </v-btn>
                        <br />
                      </template>
                    </td>
                  </tr>
                </tbody>
              </template>
            </v-simple-table>

            

            <v-alert
              v-else
              border="left"
              color="error darken-2"
              dark
              transition="slide-x-transition"
              class="py-2 mt-3"
            >
              <template v-slot:prepend>
                <v-icon left>mdi-alert-circle-outline</v-icon>
              </template>
              <p v-html="$t('the_report_was_not_found')" class="mb-0" />
            </v-alert>
          </v-container>
        </base-material-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { ISODateString, downloadFileFromResponse, isValidGUID } from '@/utils';
import { apiGetStatisticsAndReportById } from "@/api/statisticsAndReports";
import { apiDownloadDocument } from "@/api/documents";

export default {
  name: "statisticsAndReportsPreview",
  components: {
  },
  data(){
    return {
      noResultsFound: false,
      data: {},
      rules: {
      },
    }
  },
  mounted(){
    if(isValidGUID(this.$route.params.id) === false){
      this.$router.push('/statistics-and-reports');
    } else {
      this.data.id = this.$route.params.id;
      this.noResultsFound = false;
      this.getData();
    }
  },
  computed: {
  },
  methods: {
    getData(){
      apiGetStatisticsAndReportById(this.data.id).then(result => {
        if(result !== null){
          this.$set(this, "data", result);
        } else {
          this.noResultsFound = true;
        }
      })
    },
    download(id){
      apiDownloadDocument(id).then(result => {
        downloadFileFromResponse(result);
      });
    },
    ISODateString: ISODateString
  }
}
</script>