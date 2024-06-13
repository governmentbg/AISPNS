<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      :heading="'Преглед на заявка на уеб сървиса'"
      goBackText="Обратно към контрол на уеб сървиса"
      goBackTo="/admin/web-service"
    />

    <v-row class="d-print-none mt-10">
      <v-col cols="12" class="mx-auto">
        <base-material-card 
          icon="mdi-relation-one-or-many-to-zero-or-many"
          color="primary"
        >
          <template v-slot:after-heading>
            <div class="font-weight-light card-title mt-2">
              Заявка детайли
            </div>
          </template>

          <v-container class="py-3">
            <v-form
              ref="form"
              lazy-validation
            >
              <v-row>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-date-time-picker
                    label="Заявка дата"
                    v-model="data.requestTimestamp"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-date-time-picker
                    label="Отговор дата"
                    v-model="data.responseTimestamp"
                    readonly
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-input
                    label="IP адрес"
                    v-model="data.ipAddress"
                  />
                </v-col>
                <v-col cols="12" xl="3" lg="4" md="6">
                  <base-input
                    label="Статус код"
                    v-model="data.responseHttpCode"
                  />
                </v-col>
                <v-col cols="12">
                  <base-input
                    label="Endpoint"
                    v-model="data.endpoint"
                  />
                </v-col>
              </v-row>
            </v-form>
          </v-container>
        </base-material-card>
        
        <base-material-card
          icon="mdi-folder-arrow-left-outline"
          color="primary"
          class="mt-10"
        >
          <template v-slot:after-heading>
            <div class="font-weight-light card-title mt-2">
              Съдържание на отговор
            </div>
          </template>

          <v-container>
            <v-row>
              <v-col cols="12" v-html="data?.responseContent?.replaceAll('\r\n', '<br />')"></v-col>
            </v-row>
          </v-container>
        </base-material-card>

        <base-material-card
          icon="mdi-folder-arrow-right-outline"
          color="primary"
          class="mt-10"
        >
          <template v-slot:after-heading>
            <div class="font-weight-light card-title mt-2">
              Съдържание на заявка
            </div>
          </template>

          <v-container>
            <v-row>
              <v-col cols="12" v-html="data?.requestContent?.replaceAll('\r\n', '<br />')"></v-col>
            </v-row>
          </v-container>
        </base-material-card>

        <base-material-card
          icon="mdi-database-eye-outline"
          color="error"
          class="mt-10"
          v-if="exceptionData"
        >
          <template v-slot:after-heading>
            <div class="font-weight-light card-title mt-2">
              Exception информация
            </div>
          </template>

          <v-row>
            <v-col cols="12">
              <v-simple-table>
                <template v-slot:default>
                  <tbody>
                    <tr>
                      <td>Дата:</td>
                      <td>{{ formatDateTime(exceptionData.timestamp) }}</td>
                    </tr>
                    <tr>
                      <td>IP Адрес:</td>
                      <td>{{ exceptionData.ipAddress }}</td>
                    </tr>
                    <tr>
                      <td>Тип:</td>
                      <td>{{ exceptionData.type }}</td>
                    </tr>
                    <tr>
                      <td>ID потребител:</td>
                      <td>{{ exceptionData.userId }}</td>
                    </tr>
                    <tr>
                      <td>Съобщение:</td>
                      <td>{{ exceptionData.message }}</td>
                    </tr>
                    <tr>
                      <td>StackTrace:</td>
                      <td v-html="exceptionData?.stackTrace?.replaceAll('\r\n', '<br />')"></td>
                    </tr>
                  </tbody>
                </template>
              </v-simple-table>
            </v-col>
          </v-row>
        </base-material-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString, formatDateTime } from '@/utils';
import { apiGetApiRequestById, apiGetExceptionById } from '@/api/administration';
export default {
  name: "previewWebService",
  components: {
  },
  data(){
    return {
      data: Object.assign({}, {}),
      exceptionData: null
    }
  },
  mounted(){
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.isNewDoc = false;
      this.getData();
    }

  },
  computed: {
    ...mapGetters([
      'isAdministrator'
    ]),
  },
  methods: {
    getData(){
      if(!this.isNewDoc)
        apiGetApiRequestById(this.data.id).then(result => {
          if(result){
            this.$set(this, "data", result);
            this.isNewDoc = false;
            if(result.exceptionId)
              apiGetExceptionById(result.exceptionId).then(exception => {
                if(exception)
                  this.$set(this, 'exceptionData', exception);
              })
          }
        })
    },
    ISODateString: ISODateString,
    formatDateTime
  }
}
</script>