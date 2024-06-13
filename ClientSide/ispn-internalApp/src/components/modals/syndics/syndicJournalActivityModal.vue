<template>
  <div>
    <v-dialog v-model="open" width="85%" scrollable>
      <v-card>
        <v-card-title class="headline">
          Преглед на дневник
        </v-card-title>

        <v-card-text ref="cardContent">
          <v-row class="d-print-none mt-1">
            <v-col cols="12" lg="6" class="mx-auto">
              <base-material-card 
                icon="mdi-gavel"
                color="primary"
                inline
              >
                <template v-slot:after-heading>
                  <div>Дело</div>
                </template>

                <v-container class="py-0">
                  <v-simple-table class="table-presentation">
                    <template v-slot:default>
                      <tbody>
                        <tr>
                          <td style="width:100px">Съд:</td>
                          <td>{{ getCourtName }}</td>
                        </tr>
                        <tr>
                          <td>Номер:</td>
                          <td>{{ data.number }}</td>
                        </tr>
                        <tr>
                          <td>Дата:</td>
                          <td>{{ ISODateString(data.formationDate) }}</td>
                        </tr>
                        <tr>
                          <td>Длъжник:</td>
                          <td>{{ debtor.name }}</td>
                        </tr>
                        <tr>
                          <td>ЕИК:</td>
                          <td>{{ debtor.bulstat }}</td>
                        </tr>
                      </tbody>
                    </template>
                  </v-simple-table>
                </v-container>
              </base-material-card>
            </v-col>
            <v-col cols="12" lg="6" class="mx-auto">
              <base-material-card
                color="primary"
                icon="mdi-account"
                inline
              >
                <template v-slot:after-heading>
                  <div class="display-2 font-weight-light">Длъжник</div>
                </template>

                <v-simple-table class="table-presentation">
                  <template v-slot:default>
                    <tbody>
                      <tr>
                        <td style="width: 50%">Броя на съкратените работни места, свързани с ПНС:</td>
                        <td>
                          <base-input
                            v-model="entityStatisticalData.numberJobCuts"
                            type="number"
                            hide-details
                            readonly
                          />
                        </td>
                      </tr>
                      <tr>
                        <td>Бил ли е обект на план за преструктуриране:</td>
                        <td>
                          <v-checkbox 
                            v-model="entityStatisticalData.wasRestructured"
                            dense
                            hide-details
                            readonly
                          />
                        </td>
                      </tr>
                      <tr>
                        <td>Размер на длъжника:</td>
                        <td>
                          <base-input
                            v-model="entityStatisticalData.companySizeName"
                            hide-details
                            readonly
                          />
                        </td>
                      </tr>
                    </tbody>
                  </template>
                </v-simple-table>
              </base-material-card>
            </v-col>

            <v-col cols="12" class="mt-5">
              <base-material-card
                color="primary"
                icon="mdi-format-list-bulleted"
                inline
                class="px-5 py-5 mt-5"
              >
                <template v-slot:after-heading>
                  <div class="display-2 font-weight-light">Действия</div>
                </template>

                <base-kendo-grid
                  :columns="actionTable.headers"
                  :items="actionTable.data"
                  :pagination="actionTable.pagination"
                  sortable
                  :sort.sync="actionTable.sort"
                  @getData="getActivitiesData"
                  @click="tableActions"
                  class="mt-5"
                />
              </base-material-card>
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
      <SyndicActivityModal ref="actionModal" />
    </v-dialog>
  </div>
</template>

<script>
import { ISODateString } from "@/utils";
import { apiGetSyndicCaseActivities } from "@/api/syndics";
import { apiGetCaseById, apiGetEntityStatisticalDataByEntityId } from "@/api/cases";
import SyndicActivityModal from "@/components/modals/syndics/syndicActivityModal.vue";
export default {
  name: "syndicJournalActivityModal",
  components: {
    SyndicActivityModal
  },
  props: {},
  mounted() {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {},
      entityStatisticalData: {},
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
      actionTable: {
        headers: [
          { title: "Вид", field: "activityKindName", sortable: true },
          { title: "Дата", field: "activityDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: true },
          { title: "Описание", field: "description", sortable: false },
          { title: "Синдик", field: "syndicName", sortable: true },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary mr-1", click: "previewActivity" },
            ],
          },
        ],
        data: [],
        sort: {
          field: 'date',
          dir: 'desc'
        },
        pagination: {
          skip: 0,
          take: 10,
          total: 0,
          page: 1
        }
      },
    };
  },
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.$set(this, 'data', {});
        this.$set(this.actionTable, 'data', []);
      }
    },
  },
  computed: {
    getCourtName(){
      return this.data.court ? this.data.court.name : '';
    },
    debtor(){
      let debtor = {};
      if(this.data.sides)
        debtor = this.data.sides.map(side => {
          if(side.isDebtor)
            return side.entity;
        })

      return debtor[0] || {name: ' — ', bulstat: ' — '};
    }
  },
  methods: {
    getData() {
      apiGetCaseById(this.data.id).then((result) => {
        this.$set(this, "data", result);
        this.getActivitiesData();
        this.open = true;

        this.getEntityStatisticalData();

        this.$nextTick(() => {
          this.$refs.cardContent.scrollTop = 0;
        })
      });
    },
    getEntityStatisticalData(){
      if(this.debtor.id){
        apiGetEntityStatisticalDataByEntityId(this.debtor.id).then(result => {
          if(result)
            this.$set(this, 'entityStatisticalData', result);
        })
      }
    },
    openModal(id = null) {
      if(id){
        this.data.id = id;
        this.getData();
      } else {
        this.open = true;
      }

      if(this.$refs.form)
        this.$refs.form.resetValidation();
    },
    closeModal(){
      this.open = false;
    },
    
    getActivitiesData(){
      let query = Object.assign({}, {});
      query.filter = {
        caseId: this.data.id
      }
      query.pageNumber = this.actionTable.pagination.skip / this.actionTable.pagination.take + 1;
      query.pageSize = this.actionTable.pagination.take;
      

      apiGetSyndicCaseActivities(query).then(result => {
        this.actionTable.data = result.items;
        this.actionTable.pagination.total = result.totalCount;
        this.actionTable.pagination.page = result.currentPage;
      })
    },
    previewActivity(data){
      this.$refs.actionModal.openModal(data.id);
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "previewActivity":
          this.previewActivity(rowData);
        break;
      }
    },
    ISODateString
  },
};
</script>