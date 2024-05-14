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
          @click="$router.push({path: `/cases/bankruptcy/${data.id}/acts`})"
        >
          <v-icon left dark>mdi-ticket</v-icon>
          Актове подлежащи на обжалване
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

      <v-col cols="12" class="mt-5">
        <base-material-card 
          icon="mdi-book-open-variant"
          color="primary"
          inline
        >
          <template v-slot:after-heading>
            <div>Книга по чл.634в</div>
          </template>

          <v-container class="py-0">
            <v-simple-table class="bordered">
              <template v-slot:default>
                <thead>
                  <tr>
                    <th rowspan="2">№ по ред</th>
                    <th rowspan="2">Дата на постъпване</th>
                    <th rowspan="2">Действие (молба, жалба, друго)</th>
                    <th rowspan="2" class="text-center">Участник в производството</th>
                    <th class="text-center">Съдебен акт</th>
                  </tr>
                  <tr>
                    <th class="text-center">Окръжен съд – Благоевград</th>
                  </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, idx) of rows" :key="`row-${idx}`">
                      <td>{{ item.rowNumber }}</td>
                      <td>{{ ISODateString(item.date) }}</td>
                      <td>{{ item.actvity }}</td>
                      <td>{{ item.participantName }}</td>
                      <td class="text-center">
                        <template v-if="item.actvity">
                          <h6>{{ item.actvity }}</h6>
                          <v-divider />
                        </template>
                        
                        <span v-if="item.sessionResult">{{ item.sessionResult }}</span>
                        
                      </td>
                    </tr>
                </tbody>
              </template>
            </v-simple-table>
          </v-container>
        </base-material-card>
      </v-col>

    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, isValidGUID } from '@/utils';
import { apiGetCaseById, apiGetCaseBook } from "@/api/cases";
export default {
  name: "previewCaseBook",
  components: {
  },
  data(){
    return {
      data: {},
      rows: [],
    }
  },
  mounted(){
    if(!isValidGUID(this.$route.params.id)){
      this.$router.push('/cases/bankruptcy')
    } else {
      this.data.id = this.$route.params.id;
      this.getCaseData();
    }
  },
  computed: {
  },
  methods: {
    getCaseBookData(){
      apiGetCaseBook(this.data.id).then(data => {
        this.rows = data;
      })
    },
    getCaseData(){
      apiGetCaseById(this.data.id).then(data => {
        this.data = data;
        this.getCaseBookData();
      })
    },
    ISODateString: ISODateString
  }
}
</script>