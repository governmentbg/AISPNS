<template>
  <v-dialog v-model="open" width="70%" scrollable>
    <v-card>
      <v-card-title class="headline">
        Преглед на грешка
      </v-card-title>

      <v-card-text class="py-2">
        <v-row class="mt-0">
          <v-col cols="12">
            <v-simple-table>
              <template v-slot:default>
                <tbody>
                  <tr>
                    <td>Дата:</td>
                    <td>{{ formatDateTime(data.timestamp) }}</td>
                  </tr>
                  <tr>
                    <td>IP Адрес:</td>
                    <td>{{ data.ipAddress }}</td>
                  </tr>
                  <tr>
                    <td>Тип:</td>
                    <td>{{ data.type }}</td>
                  </tr>
                  <tr>
                    <td>ID потребител:</td>
                    <td>{{ data.userId }}</td>
                  </tr>
                  <tr>
                    <td>Съобщение:</td>
                    <td>{{ data.message }}</td>
                  </tr>
                  <tr>
                    <td>StackTrace:</td>
                    <td v-html="data?.stackTrace?.replaceAll('\r\n', '<br />')"></td>
                  </tr>
                </tbody>
              </template>
            </v-simple-table>
          </v-col>
        </v-row>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" outlined class="ml-3" @click="open = false">Затвори</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { apiGetExceptionById } from '@/api/administration';
import { formatDateTime } from "@/utils";

export default {
  name: "webServiceExceptionModal",
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      data: {},
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen)
        this.data = {};
    },
  },
  computed: {},
  methods: {
    getData() {
      this.data.id && 
        apiGetExceptionById(this.data.id).then((result) => {
          this.$set(this, "data", result);

          this.open = true;
        });
    },
    openModal(id = null) {
      if(id){
        this.data.id = id;
        this.getData();
      }
    },
    closeModal(){
      this.open = false;
    },
    formatDateTime
  },
};
</script>