<template>
  <v-dialog v-model="open" width="60%" scrollable>
    <v-card>
      <v-card-title class="headline">
        Преглед на история на вписаните данни
      </v-card-title>

      <v-card-text>
        <v-row>
          <v-col cols="12">
            <template v-for="entry in historyData">
              <v-card hover class="mb-7" :color="entry.replacedByEntry ? 'blue-grey lighten-3' : ''">
                <v-card-text>
                  <v-row>
                    <v-col cols="12">
                      <v-row class="mt-1">
                        <v-col cols="12">
                          <v-simple-table :class="`registryEntry${entry.replacedByEntry !== null ? ' replacedEntry blue-grey lighten-3' : ''}`">
                            <template v-slot:default>
                              <tbody>
                                <tr>
                                  <td colspan="2" class="font-weight-medium">{{ entry.fieldName }}</td>
                                </tr>
                                <tr class="">
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
                                    <td>{{ caseData.number }}</td>
                                  </tr>
                                  <tr>
                                    <td class="font-weight-medium">Дело Година:</td>
                                    <td>{{ caseData.year }}</td>
                                  </tr>
                                  <tr>
                                    <td class="font-weight-medium">Съд:</td>
                                    <td>{{ caseData.court?.name }}</td>
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
                                    <td>{{ caseData.number }}</td>
                                  </tr>
                                  <tr>
                                    <td class="font-weight-medium">Дело Година:</td>
                                    <td>{{ caseData.year }}</td>
                                  </tr>
                                  <tr>
                                    <td class="font-weight-medium">Съд:</td>
                                    <td>{{ caseData.court?.name }}</td>
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
                        <v-col cols="12" md="6" class="white--text pl-5 font-weight-bold">
                          <template v-if="entry.replacedByEntry !== null">
                            <v-icon left class="white--text">mdi-close-circle</v-icon>
                            Заличено обстоятелство.
                          </template>
                        </v-col>
                        <v-col cols="12" md="6" class="text-right">
                          <v-menu offset-y>
                            <template v-slot:activator="{ on, attrs }">
                              <v-btn
                                color="primary"
                                small
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
                      </v-row>
                    </v-col>
                  </v-row>
                </v-card-text>
              </v-card>
            </template>
          </v-col>
        </v-row>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" outlined @click="open = false">
          Затвори
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { downloadFileFromResponse, bytesToSize } from '@/utils';
import { apiGetRegisterEntryHistory } from '@/api/actAnnouncements';
import { apiDownloadDocument } from '@/api/documents';
export default {
  name: "casesRegisterEntriesHistoryModal",
  components: {
  },
  props: {
    caseData: {
      type: Object,
      default: () => null
    }
  },
  mounted() {},
  data() {
    return {
      open: false,
      historyData: [],
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  watch: {
    open(isOpen){
      if(!isOpen)
        this.$set(this, "historyData", []);
    }
  },
  computed: {
  },
  methods: {
    getData(id) {
      apiGetRegisterEntryHistory(id).then((result) => {
        if(result){
          this.historyData = result;
          this.open = true;
        }
      });
    },
    openModal(id) {
      this.getData(id);
    },
    closeModal(){
      this.open = false;
    },
    downloadFile(fileId){
      apiDownloadDocument(fileId).then(result => {
        downloadFileFromResponse(result);
      });
    },
    bytesToSize
  },
};
</script>

<style lang="sass">
.replacedEntry
  table 
    tr
      td
        text-decoration: line-through
      
      &:hover
        td
          background-color: #90A4AE !important
</style>