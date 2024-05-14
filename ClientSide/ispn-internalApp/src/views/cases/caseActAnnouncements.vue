<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      heading="Преглед на обявени актове"
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
          color="secondary lighten-1"
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
          v-if="data.id"
        >
          <v-icon left dark>mdi-book-edit-outline</v-icon>
          Данни за производството по {{data.isStabilization ? 'стабилизация' : 'несъстоятелност' }}
        </v-btn>
      </v-col>
    </v-row>
    <v-row class="d-print-none mt-10">
      <v-col cols="12" xl="6" offset-xl="3" lg="8" offset-lg="2" class="mx-auto">
        <base-case-card :data="data" />
      </v-col>

      <v-col cols="12" xl="6" lg="8" offset-xl="3" offset-lg="2">
        <base-material-card
          color="primary"
          icon="mdi-ticket"
          inline
          class="px-5 py-5 mt-5"
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">Обявени актове</div>
          </template>

          
          <template v-for="entry in entries">
            <v-card hover class="mb-7">
              <v-card-text>
                <v-row class="mt-1">
                  <v-col cols="12">
                    <v-row>
                      <v-col cols="12">
                        <v-row>
                          <v-col cols="12" md="8">
                            <h6>1001. Описание на обявения акт</h6>
                          </v-col>
                          <v-col cols="12" md="4" class="text-right">
                            <h6>№ {{ entry.number }}</h6>
                          </v-col>
                        </v-row>
                        <v-row>
                          <v-col cols="12">
                            <template v-for="(actAnnouncement, idx) in entry.actAnnouncements">
                              <v-divider class="my-3 primary" v-if="idx > 0" />
                              <v-simple-table class="registryEntry">
                                <template v-slot:default>
                                  <tbody>
                                    <tr>
                                      <td class="font-weight-medium">Категория акт:</td>
                                      <td>{{ actAnnouncement.actCategoryname }} <v-icon right @click="downloadAct(actAnnouncement.act.id)">mdi-download</v-icon></td>
                                    </tr>
                                    <tr>
                                      <td width="150" class="font-weight-medium">Вид акт:</td>
                                      <td>{{ actAnnouncement.act?.actKind?.description }}</td>
                                    </tr>
                                    <tr v-if="actAnnouncement.date">
                                      <td class="font-weight-medium">Дата:</td>
                                      <td>{{ ISODateString(actAnnouncement.date) }}</td>
                                    </tr>
                                    <tr>
                                      <td class="font-weight-medium">Дата на обявяване:</td>
                                      <td>{{ formatDateTime(actAnnouncement.announcedDate) }}</td>
                                    </tr>
                                  </tbody>
                                </template>
                              </v-simple-table>
                            </template>
                          </v-col>
                        </v-row>
                      </v-col>
                    </v-row>
                    <v-row class="mb-1 mt-5">
                      <v-col cols="12" md="6">
                        <v-menu offset-y>
                          <template v-slot:activator="{ on, attrs }">
                            <v-btn
                              color="primary"
                              small
                              block
                              v-bind="attrs"
                              v-on="on"
                              :disabled="!entry?.files?.length"
                            >
                              {{ !entry?.files?.length ? "Няма налични файлове" : "Файлове" }}
                              <v-icon right v-if="entry?.documentCollection !== null">mdi-menu-down</v-icon>
                            </v-btn>
                          </template>
                          <v-list>
                            <v-list-item
                              v-for="(file, index) in entry?.files"
                              :key="index"
                              style="min-height: 30px"
                              @click="downloadFile(file.id)"
                            >
                              <v-list-item-avatar>
                                <v-icon>mdi-download</v-icon>
                              </v-list-item-avatar>
                              <v-list-item-content>
                                <v-list-item-title>{{ file.fileName }}</v-list-item-title>
                                <v-list-item-subtitle v-if="file.description">{{ file.description }}</v-list-item-subtitle>
                              </v-list-item-content>
                              <v-list-item-action class="fs12">
                                {{ bytesToSize(file.fileSize) }}
                              </v-list-item-action>
                            </v-list-item>
                          </v-list>
                        </v-menu>
                      </v-col>
                      <v-col cols="12" md="6">
                        <v-btn
                          color="secondary"
                          small
                          block
                          @click="previewRegisterEntryHistory(entry.id)"
                        >
                          Преглед на история
                          <v-icon right>mdi-refresh</v-icon>
                        </v-btn>
                      </v-col>
                    </v-row>
                  </v-col>
                </v-row>
              </v-card-text>
            </v-card>
          </template>
        </base-material-card>
      </v-col>

    </v-row>
  </v-container>
</template>

<script>
import { ISODateString, formatDateTime, isValidGUID, bytesToSize } from '@/utils';
import { apiGetCaseById, apiGetCaseActsByCaseId } from '@/api/cases';
import { apiDownloadActImage, apiGetActAnnouncementsByCaseId } from '@/api/actAnnouncements';
export default {
  name: "previewCaseAnnouncedActs",
  components: {
  },
  data(){
    return {
      data: {},
      entries: []
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
      
        this.getAnnouncedActsData();
      })
    },
    getAnnouncedActsData(){
      apiGetActAnnouncementsByCaseId(this.data.id).then(result => {
        if(result){
          let entries = result.map(entry => {
            entry.actAnnouncementCategoryName = entry.actAnnouncements[0].actCategoryname;
            entry.files = [];
            entry.number = entry.actAnnouncements[0].number;
            entry.actAnnouncements.forEach(act => {
              if(act.documentCollection?.documentContents.length)
                entry.files = entry.files.concat(act.documentCollection.documentContents);
            });
            return entry;
          });
          this.$set(this, "entries", entries);
        }
      })
    },
    downloadAct(actId){
      apiDownloadActImage(actId).then(result => {
        downloadFileFromResponse(result);
      })
    },
    ISODateString,
    formatDateTime,
    bytesToSize
  }
}
</script>