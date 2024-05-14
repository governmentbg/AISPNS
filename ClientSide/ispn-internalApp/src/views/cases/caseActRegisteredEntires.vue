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
          color="blue-grey darken-2"
          @click="$router.push({path: `/cases/bankruptcy/${data.id}/actsAnnounced`})"
          v-if="data.id"
        >
          <v-icon left dark>mdi-message-alert-outline</v-icon>
          Обявени актове по {{data.isStabilization ? 'стабилизация' : 'несъстоятелност' }}
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
            <div class="display-2 font-weight-light">Вписани данни</div>
          </template>

          <template v-if="!entries || !entries.length">
            
            <base-material-alert
              color="primary"
              class="ma-0"
              dark
            >
              Няма вписани данни
            </base-material-alert>
          </template>
          <template v-for="entry in entries">
            <v-card hover class="mb-7">
              <v-card-text>
                <v-row class="mt-1">
                  <v-col cols="12">
                    <v-row>
                      <v-col cols="12">
                        <v-simple-table class="registryEntry">
                          <template v-slot:default>
                            <tbody>
                              <tr>
                                <td colspan="2" class="font-weight-medium">{{ entry.fieldName }}</td>
                              </tr>
                              <tr>
                                <td class="font-weight-medium">Номер:</td>
                                <td>{{ entry.number }}</td>
                              </tr>
                              <template v-if="!entry.syndicFullName">
                                <tr>
                                  <td width="150" class="font-weight-medium">Вид акт:</td>
                                  <td>{{ entry.act?.actKind?.description }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Дело номер:</td>
                                  <td>{{ data.number }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Дело Година:</td>
                                  <td>{{ data.year }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Съд:</td>
                                  <td>{{ data.court?.name }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Правно основание:</td>
                                  <td>{{ entry.legalBasisName }}</td>
                                </tr>
                                <tr>
                                  <td colspan="2" class="font-weight-medium">
                                    {{ entry.isEnforcedImmediately ? "Подлежи на незабавно изпълнение" : "Неподлежи на незабавно изпълнение" }}
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2" class="font-weight-medium">
                                    Подлежи на обжалване в срок от {{ entry.appealTerm }} дни от датата на вписване в Търговския регистър и регистъра на ЮЛНЦ
                                  </td>
                                </tr>
                              </template>
                              <template v-else>
                                <tr>
                                  <td width="150" class="font-weight-medium">Синдик:</td>
                                  <td>{{ entry.syndicFullName }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Адрес:</td>
                                  <td>{{ entry.fullAddress }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Телефон:</td>
                                  <td>{{ entry.phoneNumber }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">E-mail:</td>
                                  <td>{{ entry.email }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Вид на синдика:</td>
                                  <td>{{ entry.registerSyndicKindName }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Вид акт:</td>
                                  <td>{{ entry.act?.actKind?.description }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Дело номер:</td>
                                  <td>{{ data.number }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Дело Година:</td>
                                  <td>{{ data.year }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Съд:</td>
                                  <td>{{ data.court?.name }}</td>
                                </tr>
                                <tr>
                                  <td class="font-weight-medium">Правно основание:</td>
                                  <td>{{ entry.legalBasisName }}</td>
                                </tr>
                              </template>
                            </tbody>
                          </template>
                        </v-simple-table>
                      </v-col>
                    </v-row>
                    <v-row class="mb-1">
                      <v-col cols="12" md="6">
                        <v-menu offset-y>
                          <template v-slot:activator="{ on, attrs }">
                            <v-btn
                              color="primary"
                              small
                              block
                              v-bind="attrs"
                              v-on="on"
                              :disabled="entry?.documentCollection === null"
                            >
                              {{ entry?.documentCollection === null ? "Няма налични файлове" : "Файлове" }}
                              <v-icon right v-if="entry?.documentCollection !== null">mdi-menu-down</v-icon>
                            </v-btn>
                          </template>
                          <v-list>
                            <v-list-item
                              v-for="(file, index) in entry?.documentCollection?.documentContents"
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
    <case-register-entries-history-modal ref="caseRegisterEntriesHistoryModal" :case-data="data" />
  </v-container>
</template>

<script>
import { downloadFileFromResponse, isValidGUID, bytesToSize } from '@/utils';
import { apiGetCaseById } from '@/api/cases';
import { apiGetRegisterEntriesByCaseId } from '@/api/actAnnouncements';
import { apiDownloadDocument } from '@/api/documents';
import { CaseRegisterEntriesHistoryModal } from "@/components";
export default {
  name: "previewCaseActRegisteredEntires",
  components: {
    CaseRegisterEntriesHistoryModal
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
      
        this.getRegisteredEntriesData();
      })
    },
    getRegisteredEntriesData(){
      apiGetRegisterEntriesByCaseId(this.data.id).then(result => {
        if(result && result.length){
          this.$set(this, "entries", result.sort((a, b) => {
            if (a.fieldName < b.fieldName) 
              return -1;
            if (a.fieldName > b.fieldName)
              return 1;
            return 0;
          }));
        } else {
          this.$set(this, "entries", []);
        }
      })
    },
    downloadFile(fileId){
      apiDownloadDocument(fileId).then(result => {
        downloadFileFromResponse(result);
      });
    },
    previewRegisterEntryHistory(registerEntryId){
      this.$refs.caseRegisterEntriesHistoryModal.openModal(registerEntryId);
    },
    bytesToSize
  }
}
</script>

<style lang="sass">
.v-data-table
  &.registryEntry 
    table
      tr
        td
          height: 25px !important
</style>