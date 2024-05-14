<template>
  <div>
    <v-dialog v-model="open" width="90%" scrollable>
      <v-card>
        <v-card-title class="headline">
          <template v-if="isNewDoc">Нов сигнал</template>
          <template v-else>Преглед на сигнал</template>
        </v-card-title>

        <v-card-text>
          <v-form ref="form">
            <v-row>
              <v-col cols="12" lg="6">
                <base-material-card 
                  icon="mdi-bell-ring"
                  color="primary"
                  class="elevation-3 mt-5"
                >
                  <template v-slot:after-heading>
                    Сигнал
                  </template>
                  
                  <v-container class="py-3">
                    <v-row>
                      <v-col cols="12" lg="8">
                        <base-input
                          label="Номер"
                          v-model="data.number"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12" lg="4">
                        <base-material-date-picker
                          label="Дата"
                          v-model="data.date"
                        />
                      </v-col>
                      <v-col cols="12">
                        <base-input
                          label="Описание"
                          v-model="data.description"
                        />
                      </v-col>
                      <v-col cols="12">
                        <base-textarea
                          label="Забележки"
                          v-model="data.notes"
                        />
                      </v-col>
                    </v-row>
                  </v-container>
                </base-material-card>
              </v-col>
              <v-col cols="12" lg="6">
                <base-material-card 
                  icon="mdi-gavel"
                  color="primary"
                  class="elevation-3 mt-5"
                >
                  <template v-slot:after-heading>
                    Дело
                  </template>
                  
                  <v-container class="py-3">
                    <v-row>
                      <v-col cols="12" lg="6">
                        <base-input
                          label="Номер"
                          v-model="data.number"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12" lg="6">
                        <base-input
                          label="Година"
                          v-model="data.year"
                          type="number"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12">
                        <base-autocomplete
                          label="Съд"
                          v-model="data.court"
                          :items="nomenclatures.courts"
                          item-text="name"
                          item-value="id"
                        />
                      </v-col>
                      <v-col cols="12">
                        <base-autocomplete
                          label="Длъжник"
                          v-model="data.debtor"
                          :items="nomenclatures.debtors"
                          item-text="name"
                          item-value="id"
                        />
                      </v-col>
                    </v-row>
                  </v-container>
                </base-material-card>
              </v-col>
              <v-col cols="12">
                <base-material-card 
                  icon="mdi-account"
                  color="primary"
                  class="elevation-3 mt-10"
                >
                  <template v-slot:after-heading>
                    Подател
                  </template>
                  
                  <v-container class="py-3">
                    <v-row>
                      <v-col cols="12" lg="4">
                        <base-autocomplete
                          label="Тип"
                          v-model="data.personType"
                          :items="nomenclatures.personTypes"
                          item-text="name"
                          item-value="id"
                        />
                      </v-col>
                    </v-row>
                    <v-row>
                      <v-col cols="12" lg="4">
                        <base-input
                          label="Име"
                          v-model="data.firstName"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12" lg="4">
                        <base-input
                          label="Презиме"
                          v-model="data.middleName"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12" lg="4">
                        <base-input
                          label="Фамилия"
                          v-model="data.lastName"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12" lg="4">
                        <base-input
                          label="ЕГН"
                          v-model="data.egn"
                          type="number"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12" lg="4">
                        <base-input
                          label="E-mail"
                          v-model="data.email"
                          :rules=[rules.required]
                        />
                      </v-col>
                      <v-col cols="12" lg="4">
                        <base-input
                          label="Телефон"
                          v-model="data.phone"
                          :rules=[rules.required]
                        />
                      </v-col>
                    </v-row>
                  </v-container>
                </base-material-card>
              </v-col>
              <v-col cols="12">
                <base-material-card 
                  icon="mdi-file-multiple-outline"
                  color="primary"
                  class="elevation-3 my-10"
                >
                  <template v-slot:after-heading>
                    Документи
                  </template>

                  <template v-slot:after-heading-button>
                    <v-btn small class="primary mr-2"  @click="addNewDocument">
                      <v-icon left dark> mdi-plus </v-icon>
                      Нов документ
                    </v-btn>
                  </template>
                  
                  <v-container class="py-3">
                    <v-row>
                      <v-col cols="12">
                        <base-kendo-grid
                          :columns="table.headers"
                          :items="table.data"
                          :pagination="table.pagination"
                          sortable
                          :sort.sync="table.sort"
                          @getData="getDocumentsData"
                          @click="tableActions"
                        />
                      </v-col>
                    </v-row>
                  </v-container>
                </base-material-card>
              </v-col>
            </v-row>
          </v-form>
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" @click="save"> Запази </v-btn>
          <v-btn color="secondary" @click="save(true)"> Запази и затвори </v-btn>
          <v-btn color="primary" outlined @click="open = false">
            Затвори
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    
    <syndic-signal-document-modal ref="syndicSignalDocumentModal" @update="getDocumentsData" />
  </div>
</template>

<script>
import { SyndicSignalDocumentModal } from "@/components";
export default {
  name: "syndicSignalModal",
  components: {
    SyndicSignalDocumentModal
  },
  props: {},
  data() {
    return {
      open: false,
      isNewDoc: true,
      data: {},
      nomenclatures: {
        courts: [],
        debtors: [],
        personTypes: []
      },
      table: {
        headers: [
          { title: "Дата", field: "", sortable: true },
          { title: "Вид документ", field: "", sortable: true },
          { title: "Описание", field: "", sortable: true },
          { title: "Бележки", field: "", sortable: true },
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
              {
                title: "Свали",
                icon: "mdi-download",
                color: "white",
                class: "primary",
                click: "download",
              },
            ],
          },
        ],
        data: [],
        sort: {},
        pagination: {
          page: 1,
          skip: 0,
          take: 20,
          perPageOptions: [5, 10, 20, 50, 100],
          total: 0,
        },
      },
      rules: {
        required: (v) => !!v || "Полето е задължително",
      },
    };
  },
  mounted() {},
  watch: {
    open(isOpen) {
      if (!isOpen) {
        this.data = {};
      } else {
        if (this.data.id) {
          this.isNewDoc = false;
        } else {
          this.isNewDoc = true;
        }
      }
    },
  },
  computed: {
    
  },
  methods: {
    getData(id) {
      // apiGetSyndicPaymentById(this.data.id).then((result) => {
      //   this.$set(this, "data", result);
      //   this.open = true;
      // });
    },
    save(closeModal = false) {
      if(this.$refs.form.validate())
        if (this.isNewDoc) {
          // apiInsertSyndicPayment(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        } else {
          // apiUpdateSyndicPayment(this.data).then((result) => {
          //   if (result && result.id) {
          //     this.$emit("update");
          //     if(closeModal)
          //       this.closeModal();
          //   }
          // });
        }
    },
    getDocumentsData(){
      let query = Object.assign({}, {});
      
      query.pageNumber = this.table.pagination.skip / this.table.pagination.take + 1;
      query.id = this.data.id;
      query.pageSize = this.table.pagination.take;
      if(query.filters.page === 1){
        query.pageNumber = 1;
        this.table.pagination.skip = 0;
      }

      if(this.table.sort)
        query.filters.sort = this.table.sort

      // apiGetSignalDocuments(query).then(result => {
      //   this.table.data = result.items;
      //   this.table.pagination.total = result.totalCount;
      //   this.table.pagination.page = result.currentPage;
      // })
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "preview":
          this.previewDocument(rowData);
        break;
        case "download":
          this.documentDownload(rowData);
        break;
      }
    },
    documentDownload(){

    },
    previewDocument(data){
      this.$refs.syndicSignalDocumentModal.openModal(data.id)
    },
    addNewDocument(){
      this.$refs.syndicSignalDocumentModal.openModal()
    },
    openModal(id = null) {
      if(id){
        this.data.id = id;
        this.getData();
      } else {
        this.open = true;
        this.isNewDoc = true;
      }
    },
    closeModal(){
      this.open = false;
    }
  },
};
</script>

<style></style>
