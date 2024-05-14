<template>
  <v-container
    fluid
    tag="section"
    class="full-height"
    v-if="isSyndic"
  >
    <base-v-component
      :heading="isNewDoc ? 'Създаване на дневник' : 'Преглед на дневник'"
      :goBackText="$route.meta.goBackTitle"
      goBackTo="/journals"
    />

    <v-row class="mt-5 d-print-none">
      <v-col cols="12" class="text-right">
        <v-btn
          color="primary"
          @click="save"
        >
          <v-icon left dark>mdi-check</v-icon>
          Запази
        </v-btn>
        <v-btn
          color="secondary"
          @click="print"
        >
          <v-icon left dark>mdi-printer</v-icon>
          Разпечатай
        </v-btn>
        <v-btn
          color="secondary"
          @click="sendByEmail"
        >
          <v-icon left dark>mdi-email-arrow-right-outline</v-icon>
          Изпрати по мейл
        </v-btn>
      </v-col>
    </v-row>

    <v-row class="d-print-none mt-10">
      <v-col cols="12" lg="6" class="mx-auto">
        <base-material-card 
          icon="mdi-gavel"
          color="primary"
          inline
        >
          <template v-slot:after-heading>
            <div>Дело</div>
          </template>

          <template v-slot:after-heading-button>
            <v-btn small class="primary mr-2"  @click="openCasesModal">
              <v-icon left dark> mdi-book-search-outline </v-icon>
              Избери дело
            </v-btn>
          </template>

          <v-container class="py-0">
            <v-simple-table class="table-presentation">
              <template v-slot:default>
                <tbody>
                  <tr>
                    <td style="width:100px">Съд:</td>
                    <td>Окръжен съд – Благоевград</td>
                  </tr>
                  <tr>
                    <td>Номер:</td>
                    <td>189/2023</td>
                  </tr>
                  <tr>
                    <td>Дата:</td>
                    <td>2023-10-18</td>
                  </tr>
                  <tr>
                    <td>Длъжник:</td>
                    <td>Иван Иванов ООД</td>
                  </tr>
                  <tr>
                    <td>ЕИК:</td>
                    <td>143242344</td>
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
                  <td>Броя на съкратените работни места, свързани с ПНС:</td>
                  <td>
                    <base-input
                      v-model="data.count"
                      type="number"
                      hide-details
                    />
                  </td>
                </tr>
                <tr>
                  <td>Бил ли е обект на план за преструктуриране:</td>
                  <td>
                    <v-checkbox 
                      v-model="data.isRestructuringPlan"
                      dense
                      hide-details
                    />
                  </td>
                </tr>
                <tr>
                  <td>Размер на длъжника:</td>
                  <td>
                    <base-autocomplete
                      v-model="data.debtorSize"
                      hide-details
                    />
                  </td>
                </tr>
                <tr>
                  <td>Длъжник:</td>
                  <td>
                    <base-autocomplete
                      v-model="data.debtor"
                      hide-details
                    />
                  </td>
                </tr>
              </tbody>
            </template>
          </v-simple-table>
        </base-material-card>
      </v-col>

      <v-col cols="12">
        <base-material-card
          color="primary"
          icon="mdi-format-list-bulleted"
          inline
          class="px-5 py-5 mt-5"
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">Действия</div>
          </template>

          <template v-slot:after-heading-button>
            <v-btn small class="primary mr-2"  @click="openJournalActionModal">
              <v-icon left dark> mdi-plus </v-icon>
              Ново действие
            </v-btn>
          </template>

          <base-kendo-grid
            :columns="actionTable.headers"
            :items="actionTable.data"
            :pagination="actionTable.pagination"
            sortable
            :sort.sync="actionTable.sort"
            @getData="getActionsData"
            @click="tableActions"
            @dblclick="preview"
            class="mt-5"
          />
        </base-material-card>
      </v-col>
    </v-row>
    <cases-list-selection-modal ref="casesListSelectionModal" @selected="selectedCase" />
    <journal-action-modal ref="journalActionModal" @reload="getActionsData" />
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { ISODateString } from '@/utils';
import { CasesListSelectionModal, JournalActionModal } from '@/components';
export default {
  name: "previewJournal",
  components: {
    CasesListSelectionModal,
    JournalActionModal
  },
  data(){
    return {
      isNewDoc: true,
      data: {},
      actionTable: {
        headers: [
          { title: "Вид", field: "", sortable: true },
          { title: "Дата", field: "", sortable: true },
          { title: "Пореден номер", field: "", sortable: true },
          { title: "Описание", field: "", sortable: false },
          { title: "Синдик", field: "", sortable: true },
          {
            title: "",
            cell: "actions",
            filterable: false,
            width: "50px",
            sortable: false,
            buttons: [
              { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewJournal" },
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
    }
  },
  mounted(){
    if(this.$route.params.id === 'create'){
      this.isNewDoc = true;
    } else {
      this.data.id = this.$route.params.id;
      this.getData();
    }
  },
  computed: {
    ...mapGetters([
      'isSyndic'
    ]),
  },
  methods: {
    getData(){
      // apiGetCaseById(this.data.id).then(data => {
      //   this.data = data;
      //   this.isNewDoc = false;
      // })
    },
    save(){

    },
    print(){

    },
    sendByEmail(){

    },
    selectedCase(){

    },
    getActionsData(){
      let query = Object.assign({}, {});
      query.pageNumber = this.actionTable.pagination.skip / this.actionTable.pagination.take + 1;

      query.pageSize = this.actionTable.pagination.take;
      if(query.filters.page === 1){
        query.pageNumber = 1;
        this.actionTable.pagination.skip = 0;
      }

      // apiGetCaseSides(query).then(result => {
      //   this.actionTable.data = result.items;
      //   this.actionTable.pagination.total = result.totalCount;
      //   this.actionTable.pagination.page = result.currentPage;
      // })
    },
    preview(){

    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.preview(rowData);
        break;
      }
    },
    addAction(){

    },
    openCasesModal(){
      this.$refs.casesListSelectionModal.openModal();
    },
    openJournalActionModal(){
      this.$refs.journalActionModal.openModal();
    },
    ISODateString: ISODateString
  }
}
</script>