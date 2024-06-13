<template>
  <v-container fluid tag="section" class="full-height">
    <base-v-component :heading="$route.meta.title" />

    <v-row class="my-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          class="primary"
          @click="addNew"
        >
          <v-icon left dark>mdi-plus</v-icon>
          Ново съобщение
        </v-btn>
      </v-col>
    </v-row>

    <base-material-card
      color="primary"
      icon="mdi-message-text-outline"
      inline
      class="px-5 py-5 mt-10"
    >
      <template v-slot:after-heading>
        <div class="display-2 font-weight-light">{{ $route.name }}</div>
      </template>

      
      <base-material-tabs 
        color="primary" 
        fixed-tabs 
        slider-size="3" 
        @change="tabChange" 
        v-model="currentTab"
      >
        <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key">
          {{ tab.name }}
        </v-tab>

        <v-tabs-items v-model="currentTab">
          <template v-for="tab in tabs">
            <v-tab-item eager :value="tab.key" class="mt-5">
              <template v-if="tab.key === 'received'">
                <base-kendo-grid
                  :columns="tables.received.headers"
                  :items="tables.received.data"
                  :pagination="tables.received.pagination"
                  sortable
                  :sort.sync="tables.received.sort"
                  @getData="getReceivedMessagesData"
                  @click="tableActions"
                  @dblclick="preview"
                  class="mt-5"
                  :customRowClass="{
                    key: 'seen',
                    value: false,
                    styleClass: 'black--text brown lighten-4'
                  }"
                />
              </template>

              <template v-else>
                <base-kendo-grid
                  :columns="tables.sent.headers"
                  :items="tables.sent.data"
                  :pagination="tables.sent.pagination"
                  sortable
                  :sort.sync="tables.sent.sort"
                  @getData="getSentMessagesData"
                  @click="tableActions"
                  @dblclick="preview"
                  class="mt-5"
                  :customRowClass="{
                    key: 'seen',
                    value: false,
                    styleClass: 'black--text brown lighten-4'
                  }"
                />
              </template>
            </v-tab-item>
          </template>
          
        </v-tabs-items>
      </base-material-tabs>

      
    </base-material-card>
  </v-container>
</template>

<script>
import { apiGetReceivedMessages, apiGetSentMessages } from "@/api/messages";
export default {
  name: "messagesList",
  components: {},
  data: () => ({
    currentTab: 'received',
    tabs: [
      { name: "Получени", key: "received" },
      { name: "Изпратени", key: "sent"}
    ],
    tables: {
      received: {
        headers: [
          { title: "Дата", field: "sendDate", type: 'date', format: '{0:dd.MM.yyyy HH:mm}', sortable: true, width: "150px" },
          { title: "Подател", field: "senderName", sortable: true, width: "250px"},
          { title: "Относно", field: "subject", sortable: true },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "80px",
            sortable: false,
            buttons: [
              {
                title: "Преглед",
                icon: "mdi-text-box-search-outline",
                color: "white",
                class: "primary mr-1",
                click: "preview",
              },
              {
                title: "Отговори",
                icon: "mdi-reply",
                color: "white",
                class: "secondary",
                click: "reply",
                if_eq: {field: 'replyId', value: '00000000-0000-0000-0000-000000000000'}
              },
            ],
          },
        ],
        data: null,
        sort: {},
        pagination: {
          page: 1,
          skip: 0,
          take: 20,
          perPageOptions: [5, 10, 20, 50, 100],
          total: 0,
        }
      },
      sent: {
        headers: [
          { title: "Дата", field: "sendDate", type: 'date', format: '{0:dd.MM.yyyy HH:mm}', sortable: true, width: '150px' },
          { title: "Получател", field: "receiverName", sortable: true, width: '250px' },
          { title: "Относно", field: "subject", sortable: true },
          { title: "Прочетено", field: "seen", cell: 'status', sortable: true, width: '100px', className: 'text-center' },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              {
                title: "Преглед",
                icon: "mdi-text-box-search-outline",
                color: "white",
                class: "primary",
                click: "preview",
              },
            ],
          },
        ],
        data: null,
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
  }),
  computed: {
  },
  mounted() {
    this.getReceivedMessagesData();
  },
  watch: {
    '$vuetify.breakpoint': {
      handler(breakpoint){
        if(breakpoint.mdAndDown){
          this.tables.received.headers[0].width = "140px";
          this.tables.received.headers[1].width = "190px";

          this.tables.sent.headers[0].width = "150px";
          this.tables.sent.headers[1].width = "200px";
        } else if(breakpoint.lgAndUp){
          this.tables.received.headers[0].width = "150px";
          this.tables.received.headers[1].width = "250px";

          this.tables.sent.headers[0].width = "150px";
          this.tables.sent.headers[1].width = "250px";
        }
      },
      deep: true
    }
  },
  methods: {
    getReceivedMessagesData() {
      let query = Object.assign({filters: {}}, {});
      query.pageNumber = this.tables.received.pagination.skip / this.tables.received.pagination.take + 1;
      query.pageSize = this.tables.received.pagination.take;

      apiGetReceivedMessages(query).then((result) => {
        this.tables.received.data = result.items;
        this.tables.received.pagination.total = result.totalCount;
        this.tables.received.pagination.page = result.currentPage;
      });
    },
    getSentMessagesData(){
      let query = Object.assign({filters: {}}, {});
      query.pageNumber = this.tables.sent.pagination.skip / this.tables.sent.pagination.take + 1;
      query.pageSize = this.tables.sent.pagination.take;

      apiGetSentMessages(query).then((result) => {
        this.tables.sent.data = result.items;
        this.tables.sent.pagination.total = result.totalCount;
        this.tables.sent.pagination.page = result.currentPage;
      });
    },
    preview(item) {
      this.$router.push({ path: `/messages/${item.id}` });
    },
    reply(item) {
      this.$router.push({ path: `/messages/${item.id}/reply` });
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
        case "reply":
          this.reply(rowData);
      }
    },
    addNew(){
      this.$router.push({path: `/messages/create`})
    },
    tabChange(tabKey, force = false){
      switch (tabKey) {
        case "received":
          this.getReceivedMessagesData();
        break;
        case "sent":
          this.getSentMessagesData();
        break;
      }
    }
  },
};
</script>
