<template>
  <v-container
    id="previewSyndic"
    fluid
    tag="section"
    class="full-height"
  >
    <base-v-component
      :heading="isNewDoc ? 'Добавяне на синдик' : 'Преглед на синдик'"
      goBackText="Обратно към всички синдици"
      goBackTo="/syndics"
    />

    <v-row class="mt-5 d-print-none">
      
      <v-col cols="12" lg="5" xl="4">
        <v-sheet class="pa-3 cardDetails" color="grey lighten-4" v-if="!isNewDoc">
          <template v-if="data.dateCreated">
            Създаден на: <strong> {{ formatDateTime(data.dateCreated) }} </strong> <br />
          </template>
          <template v-if="data.dateModified">
            Последна промяна: <strong> {{ formatDateTime(data.dateModified) }} </strong> <br />
          </template>
        </v-sheet>
      </v-col>
      <v-col cols="12" lg="7" xl="8" class="text-right">
        <v-btn
          color="primary"
          @click="save"
        >
          <v-icon
            left
            dark
          >
            mdi-check
          </v-icon>
          Запази
        </v-btn>
        <v-menu offset-y v-if="!isNewDoc">
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              color="secondary"
              dark
              v-bind="attrs"
              v-on="on"
            >
              Изпрати съобщение
              <template slot:prepend-icon>
                <v-icon right>mdi-menu-down</v-icon>
              </template>
            </v-btn>
          </template>
          <v-list>
            <v-list-item
              link
            >
              <v-list-item-title>По имейл</v-list-item-title>
            </v-list-item>
            <v-list-item
              link
            >
              <v-list-item-title>Чрез ССЕВ</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
      </v-col>
    </v-row>
    <v-row class="my-5">
      <v-col cols="12">
        <base-material-tabs 
          v-model="currentTab"
          fixed-tabs 
          slider-size="3" 
          color="primary" 
          @change="tabChange" 
        >
          <v-tabs-slider color="secondary" />
          <v-tab v-for="tab in tabs" :key="`${tab.key}`" :href="'#'+tab.key">
            {{ tab.name }}
          </v-tab>
          <v-tabs-items v-model="currentTab">
            <v-tab-item v-for="tab in tabs" :key="tab.key" :value="tab.key" class="mt-5">
              <v-card flat>
                <v-card-text>
                  <v-row class="d-print-none mt-2">
                    <v-col cols="12" class="mx-auto">

                      <base-material-card 
                        icon="mdi-account-star"
                        color="primary"
                        class="elevation-3"
                        v-if="tab.key === 'generalData'"
                      >
                        <template v-slot:after-heading>
                          Доверено лице
                        </template>

                        
                        <template v-slot:after-heading-button>
                          <v-switch
                            v-model="data.active"
                            :true-value="true"
                            :false-value="false"
                            label="Активност"
                            class="d-inline-block"
                          />
                        </template>
                        <v-form
                          ref="form"
                          lazy-validation
                          class="pa-3"
                        >
                          <v-row>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-input
                                label="Име"
                                v-model="data.firstName"
                                :rules="[rules.required, rules.min]"
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-input
                                label="Презиме"
                                v-model="data.secondName"
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-input
                                label="Фамилия"
                                v-model="data.lastName"
                                :rules="[rules.required, rules.min]"
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-input
                                label="Втора фамилия"
                                v-model="data.secondLastName"
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-input
                                label="ЕГН"
                                v-model="data.egn"
                                :rules="[rules.required, rules.egn]"
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-input
                                label="E-mail"
                                :rules="[rules.email]"
                                v-model="data.email"
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-input
                                label="Телефон"
                                v-model="data.phone"
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-autocomplete
                                label="Специалност"
                                v-model="data.specialty"
                                :items="nomenclatures.specialities"
                                item-text="description"
                                item-value="id"
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-select
                                label="Статус"
                                v-model="data.syndicStatusId"
                                :items="nomenclatures.statuses"
                                item-text="description"
                                item-value="id"
                                :rules="[rules.required]"
                              />
                            </v-col>
                            <v-col cols="12" xl="1" lg="4" md="6">
                              <base-select
                                label="Заключен"
                                v-model="data.locked"
                                :items="nomenclatures.locked"
                                item-text="label"
                                item-value="value"
                              />
                            </v-col>
                          </v-row>
                          <v-row>
                            <v-col cols="12" xl="3" lg="4" md="4">
                              <base-autocomplete
                                label="Област"
                                class="pb-0"
                                v-model="data.address.regionId"
                                :items="nomenclatures.districts"
                                item-text="name"
                                item-value="id"
                                @change="getMunicipalities"
                                clearable
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="4">
                              <base-autocomplete
                                v-model="data.address.municipalityId"
                                label="Община"
                                :items="nomenclatures.municipalities"
                                item-text="name"
                                item-value="id"
                                class="pb-0"
                                @change="getSettlements"
                                clearable
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="4">
                              <base-autocomplete
                                label="Населено място"
                                class="pb-0"
                                v-model="data.address.settlementId"
                                :items="nomenclatures.settlements"
                                item-text="name"
                                item-value="id"
                                clearable
                              />
                            </v-col>
                            <v-col cols="12" xl="1" lg="2" md="2">
                              <v-text-field
                                v-model="data.address.postCode"
                                label="ПК"
                                color="secondary"
                                dense
                              />
                            </v-col>
                            <v-col cols="12" xl="5" lg="6" md="4">
                              <v-text-field
                                v-model="data.address.streetName"
                                label="Улица"
                                color="secondary"
                                dense
                              />
                            </v-col>
                            <v-col cols="12" xl="1" lg="2" md="2">
                              <v-text-field
                                v-model="data.address.streetNumber"
                                label="Улица №"
                                color="secondary"
                                dense
                              />
                            </v-col>
                            <v-col cols="12" xl="1" lg="2" md="2">
                              <v-text-field
                                v-model="data.address.buildingNumber"
                                label="Сграда №"
                                color="secondary"
                                dense
                              />
                            </v-col>
                            <v-col cols="12" xl="1" lg="2" md="2">
                              <v-text-field
                                v-model="data.address.entranceNumber"
                                label="Вход"
                                color="secondary"
                                dense
                              />
                            </v-col>
                            <v-col cols="12" xl="1" lg="2" md="2">
                              <v-text-field
                                v-model="data.address.floorNumber"
                                label="Етаж"
                                color="secondary"
                                dense
                              />
                            </v-col>
                            <v-col cols="12" xl="1" lg="2" md="2">
                              <v-text-field
                                v-model="data.address.apartmentNumber"
                                label="Ап. №"
                                color="secondary"
                                dense
                              />
                            </v-col>
                            <v-col cols="12" xl="2" lg="3" md="4">
                              <v-text-field
                                v-model="data.address.ekkate"
                                readonly
                                label="ЕКАТТЕ"
                                color="secondary"
                                dense
                              />
                            </v-col>
                          </v-row>
                          <v-row>
                            <v-col cols="12">
                              <v-textarea
                                label="Бележки"
                                v-model="data.notes"
                                dense
                                rows="3"
                                auto-grow
                              />
                            </v-col>
                          </v-row>
                        </v-form>
                      </base-material-card>

                      <base-material-card 
                        icon="mdi-stamper"
                        color="primary"
                        class="elevation-3 mt-10"
                        v-if="tab.key === 'generalData' && !isNewDoc && data.order"
                      >
                        <template v-slot:after-heading>
                          Заповед
                        </template>
                        
                        <v-form
                          ref="orderForm"
                          lazy-validation
                          class="pa-3"
                        >
                          <v-row>
                            <v-col cols="12">
                              <base-input
                                label="Утвърден/а със заповед след изпит за придобиване на квалификация синдик"
                                :value="data.order?.number"
                                readonly
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-input
                                label="Номер заповед"
                                :value="data.order?.number"
                                readonly
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-material-date-picker
                                label="Дата заповед"
                                :value="data.order?.date"
                                readonly
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-input
                                label="Държавен вестник брой"
                                :value="data.order?.stateGazetteEdition"
                                readonly
                              />
                            </v-col>
                            <v-col cols="12" xl="3" lg="4" md="6">
                              <base-input
                                label="Държавен вестник година"
                                :value="data.order?.stateGazetteYear"
                                type="number"
                                readonly
                              />
                            </v-col>
                          </v-row>
                        </v-form>
                      </base-material-card>

                      <base-material-card 
                        icon="mdi-stamper"
                        color="primary"
                        class="elevation-3"
                        v-if="tab.key === 'orders'"
                      >
                        <template v-slot:after-heading>
                          Заповеди
                        </template>

                        <template v-slot:after-heading-button>
                          <v-btn small class="primary px-5"  @click="addNewOrder">
                            <v-icon left dark> mdi-plus </v-icon>
                            Нова заповед
                          </v-btn>
                        </template>
                        
                        <base-kendo-grid
                          :columns="tables.orders.headers"
                          :items="tables.orders.data"
                          :pagination="tables.orders.pagination"
                          sortable
                          :sort.sync="tables.sort"
                          @getData="getOrdersData"
                          @click="tableActions"
                          @dblclick="previewOrder"
                        />
                      </base-material-card>


                      <base-material-card 
                        icon="mdi-cash-fast"
                        color="primary"
                        class="elevation-3"
                        v-if="tab.key === 'payments'"
                      >
                        <template v-slot:after-heading>
                          Вноски
                        </template>

                        <template v-slot:after-heading-button>
                          <v-btn small class="primary mr-2"  @click="addNewPayment">
                            <v-icon left dark> mdi-plus </v-icon>
                            Отбелязване на плащане
                          </v-btn>
                          <v-btn small class="primary"  @click="sendEmailNotificationForRequiredPaymentAsk">
                            <v-icon left dark> mdi-email-arrow-right-outline </v-icon>
                            Изпрати съобщение по имейл за изтичащ срок за плащане
                          </v-btn>
                        </template>
                        
                        <base-kendo-grid
                          :columns="tables.payments.headers"
                          :items="tables.payments.data"
                          :pagination="tables.payments.pagination"
                          sortable
                          :sort.sync="tables.payments.sort"
                          @getData="getPaymentsData"
                          @click="tableActions"
                          @dblclick="previewPayment"
                          :exportable="tables.payments.data && tables.payments.data.length > 0"
                          @export="exportPaymentsTable"
                        />
                      </base-material-card>


                      <base-material-card 
                        icon="mdi-school-outline"
                        color="primary"
                        class="elevation-3"
                        v-if="tab.key === 'trainings'"
                      >
                        <template v-slot:after-heading>
                          Oбучения
                        </template>

                        <base-material-tabs 
                          v-model="currentTrainingsTab"
                          fixed-tabs 
                          slider-size="3" 
                          color="primary" 
                          @change="getTrainingsData" 
                        >
                          <v-tab>
                            Заявки за обучение
                          </v-tab>
                          <v-tab>
                            Резултати от обучение
                          </v-tab>

                          <v-tabs-items v-model="currentTrainingsTab">
                            <v-tab-item class="mt-5">
                              <v-card>
                                <v-card-text>
                                  <v-row>
                                    <v-col cols="12" class="text-right">
                                      <v-btn small class="primary mr-2"  @click="addNewTraining">
                                        <v-icon left dark> mdi-plus </v-icon>
                                        Добавяне на заявка
                                      </v-btn>
                                    </v-col>
                                    <v-col cols="12">
                                      <base-kendo-grid
                                        :columns="tables.trainings.headers"
                                        :items="tables.trainings.data"
                                        :pagination="tables.trainings.pagination"
                                        sortable
                                        :sort.sync="tables.trainings.sort"
                                        @getData="getTrainingsData"
                                        @click="tableActions"
                                        @dblclick="previewTraining"
                                        :exportable="tables.trainings.data && tables.trainings.data.length > 0"
                                        @export="exportTrainingsTable"
                                      />
                                    </v-col>
                                  </v-row>
                                </v-card-text>
                              </v-card>
                            </v-tab-item>
                            <v-tab-item class="mt-5">
                              <v-card flat>
                                <v-card-text>
                                  <base-kendo-grid
                                    :columns="tables.trainingResults.headers"
                                    :items="tables.trainingResults.data"
                                    :pagination="tables.trainingResults.pagination"
                                    sortable
                                    :sort.sync="tables.trainingResults.sort"
                                    @getData="getTrainingResultsData"
                                    @click="tableActions"
                                    :exportable="tables.trainingResults.data && tables.trainingResults.data.length > 0"
                                    @export="exportTrainingResultsTable"
                                  />
                                </v-card-text>
                              </v-card>
                            </v-tab-item>
                          </v-tabs-items>
                        </base-material-tabs>
                      </base-material-card>

                      <base-material-card 
                        icon="mdi-gavel"
                        color="primary"
                        class="elevation-3"
                        v-if="tab.key === 'cases'"
                      >
                        <template v-slot:after-heading>
                          Дела по стабилизация
                        </template>

                        <syndics-cases-filter ref="syndicsCasesFilter" @doFilter="getCasesData" />

                        <base-kendo-grid
                          :columns="tables.cases.headers"
                          :items="tables.cases.data"
                          :pagination="tables.cases.pagination"
                          sortable
                          :sort.sync="tables.cases.sort"
                          @getData="getCasesData"
                          @click="tableActions"
                          @dblclick="previewCase"
                          class="mt-15"
                        />
                      </base-material-card>

                      <base-material-card 
                        icon="mdi-bell-ring"
                        color="primary"
                        class="elevation-3"
                        v-if="tab.key === 'signals'"
                      >
                        <template v-slot:after-heading>
                          Сигнали
                        </template>

                        <template v-slot:after-heading-button>
                          <v-btn small class="primary mr-2"  @click="addNewSignal">
                            <v-icon left dark> mdi-plus </v-icon>
                            Нов сигнал
                          </v-btn>
                        </template>
                        
                        <base-kendo-grid
                          :columns="tables.signals.headers"
                          :items="tables.signals.data"
                          :pagination="tables.signals.pagination"
                          sortable
                          :sort.sync="tables.signals.sort"
                          @getData="getSignalsData"
                          @click="tableActions"
                          @dblclick="previewSignal"
                        />
                      </base-material-card>

                      

                      <base-material-card 
                        icon="mdi-account-file-text"
                        color="primary"
                        class="elevation-3"
                        v-if="tab.key === 'appeals'"
                      >
                        <template v-slot:after-heading>
                          Обжалвания
                        </template>

                        <template v-slot:after-heading-button>
                          <v-btn small class="primary mr-2"  @click="addNewAppeal">
                            <v-icon left dark>mdi-plus</v-icon>
                            Ново обжалване
                          </v-btn>
                        </template>
                        
                        <base-kendo-grid
                          :columns="tables.appeals.headers"
                          :items="tables.appeals.data"
                          :pagination="tables.appeals.pagination"
                          sortable
                          :sort.sync="tables.appeals.sort"
                          @getData="getAppealsData"
                          @click="tableActions"
                          @dblclick="previewAppeal"
                        />
                      </base-material-card>
                      
                    </v-col>
                  </v-row>
                </v-card-text>
              </v-card>
            </v-tab-item>
          </v-tabs-items>
        </base-material-tabs>
      </v-col>
    </v-row>
    <syndic-order-modal ref="syndicOrderModal" @update="getOrdersData" :syndic-data="data" @confirm="getConfirm" @closeConfirm="$refs.confirmModal.closeModal()" />
    <syndic-payment-modal ref="syndicPaymentModal" @update="getPaymentsData" :syndic-data="data" @confirm="getConfirm" @closeConfirm="$refs.confirmModal.closeModal()" />
    <syndic-training-modal ref="syndicTrainingModal" @update="getTrainingsData" :syndic-data="data" />
    <syndic-signal-modal ref="syndicSignalModal" @update="getSignalsData" :syndic-data="data" @confirm="getConfirm" @closeConfirm="$refs.confirmModal.closeModal()" />
    <syndic-appeal-modal ref="syndicAppealModal" @update="getAppealsData" :syndic-data="data" @confirm="getConfirm" @closeConfirm="$refs.confirmModal.closeModal()" />
    <base-confirm-modal ref="confirmModal" />
  </v-container>
</template>

<script>
import { ISODateString, downloadFileFromResponse, formatDateTime } from '@/utils';
import { 
  SyndicOrderModal, 
  SyndicPaymentModal, 
  SyndicTrainingModal, 
  SyndicSignalModal, 
  SyndicAppealModal, 
  SyndicsCasesFilter
} from "@/components";

import EGN from "@/utils/validations/egn";

import { 
  apiGetSyndicById, 
  apiCreateSyndic, 
  apiUpdateSyndic, 
  apiGetSyndicInstallments, 
  apiSendExpirationDateEmail,
  apiGetSyndicOrders, 
  apiGetSyndicCases, 
  apiGetSyndicSignals, 
  apiExportSyndicCoursesToExcel, 
  apiExportSyndicInstallmentsToExcel,
  apiGetSyndicCourseResults,
  apiExportSyndicCourseResultsToExcel, 
  apiGetSyndicCourseApplications,
  apiGetSyndicPleas
} from "@/api/syndics";
import { apiMetaDataGetSpecialties, apiMetaDataGetSyndicStatuses, apiMetaDataGetDistricts, apiMetaDataGetMunicipalities, apiMetaDataGetSettlements } from "@/api/metaData";
import SyndicModel from '@/models/syndics/SyndicModel';
export default {
  name: "trustedPersonPreview",
  components: {
    SyndicOrderModal,
    SyndicPaymentModal,
    SyndicTrainingModal,
    SyndicSignalModal,
    SyndicAppealModal,
    SyndicsCasesFilter
  },
  data(){
    return {
      isNewDoc: true,
      nomenclatures: {
        statuses: [],
        specialities: [],
        districts: [],
        municipalities: [],
        settlements: [],
        locked: [
          {label: "Да", value: true},
          {label: "Не", value: false}
        ]
      },
      data: Object.assign({isCustodian: true}, new SyndicModel()),
      currentTab: 0,
      tabs: [],
      currentTrainingsTab: 0,
      tables: {
        orders: {
          headers: [
            { title: "Вид", field: "orderKindName", sortable: false },
            { title: "Номер", field: "number", sortable: false },
            { title: "Дата", field: "date", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false },
            { title: "ДВ брой", field: "stateGazetteEdition", sortable: false },
            { title: "ДВ година", field: "stateGazetteYear", sortable: false },
            { title: "Заповед за изключване", field: "isExclusion", cell: 'status', sortable: false, width: '110px', className: 'text-center' },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewOrder" },
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
        },
        payments: {
          headers: [
            { title: "Вид", field: "installmentKindName", sortable: false },
            { title: "За година", field: "installmentYear", sortable: false },
            { title: "Пл. документ №", field: "number", sortable: false },
            { title: "Пл. документ дата", field: "paymentDate", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false },
            { title: "Банка", field: "bank", sortable: false },
            { title: "Проверено плащане", field: "verified", cell: "status", sortable: false, className: "text-center", headerClassName: 'text-center' },
            { title: "Сума (лв.)", field: "amount", sortable: false, className: 'text-right', headerClassName: 'text-right' },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewPayment" },
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
        },
        trainings: {
          headers: [
            { title: "Програма", field: "programDescription", sortable: false },
            { title: "Регистрация на", field: "dateCreated", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false },
            { title: "Основно обучение", field: "mainCourseName", sortable: false },
            { title: "Алтерн. обучение", field: "alternateCourseName", sortable: false },
            { title: "Лектор осн. об.", field: "lecturerDate1", cell: 'trueOrFalse', sortable: true, className: 'text-center'},
            { title: "Лектор алт. об.", field: "lecturerDate2", cell: 'trueOrFalse', sortable: true, className: 'text-center' },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewTraining" },
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
        },
        trainingResults: {
          headers: [
            { title: "Тема", field: "", sortable: false },
            { title: "От дата", field: "", sortable: false },
            { title: "До дата", field: "", sortable: false },
            { title: "Място на провеждане", field: "", sortable: false },
            { title: "Осн. дата", field: "", sortable: false },
            { title: "Преминал", field: "", sortable: false },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewTraining" },
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
        },
        cases: {
          headers: [
            { title: "№", field: "number", sortable: false },
            { title: "Година", field: "year", sortable: false },
            { title: "Дата", field: "formationDate", type: 'date', format: "{0:dd.MM.yyyyy}", sortable: false },
            { title: "Съд", field: "court.name", sortable: false },
            { title: "Вид", field: "caseKindDescription", sortable: false },
            { title: "Актове подлежащи на обжалване", field: "acts", cell: this.renderActsLink, sortable: false, width: "300px" },
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
                  click: "previewCase",
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
        },
        bankruptcy: {
          headers: [
            { title: "Дело", field: "", sortable: true },
            { title: "Номер", field: "", sortable: true },
            { title: "Година", field: "", sortable: true },
            { title: "Дата на последна редакция", field: "", sortable: true },
            { title: "Длъжник", field: "", sortable: false },
            { title: "Синдик", field: "", sortable: false },
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
          data: null,
          sort: {},
          pagination: {
            page: 1,
            skip: 0,
            take: 20,
            perPageOptions: [5, 10, 20, 50, 100],
            total: 0,
          },
        },
        signals: {
          headers: [
            { title: "Номер", field: "number", sortable: false },
            { title: "Дата", field: "date", type: 'date', format: '{0:dd.MM.yyyy}', sortable: false },
            { title: "Дело номер/година", cell: this.renderCaseNumberYear, sortable: false },
            { title: "Дело съд", field: "courtName", sortable: false },
            { title: "Подател", field: "senderName", sortable: false },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewSignal" },
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
        },
        appeals: {
          headers: [
            { title: "Заповед номер/Дата", cell: this.renderOrderNumberAndDate, sortable: false },
            { title: "Жалба номер/дата", cell: this.renderPleaNumberAndDate, sortable: false },
            { title: "Решение на ВАС", field: "courtDecision", sortable: false },
            {
              title: "",
              cell: "actions",
              filterable: false,
              width: "50px",
              sortable: false,
              buttons: [
                { title: "Преглед", icon: "mdi-text-box-search-outline", color: "white", class: "primary", click: "previewAppeal" },
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
        },
      },
      rules: {
        required: v => !!v || 'Полето е задължително.',
        min: v => (v && v.length >= 3) || 'Полето трябва да съдържа поне 3 символа',
        email: v => !v || /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) || 'E-mail-a трябва да бъде валиден адрес.',
        egn: function(v) {
          if(v)
            return new EGN(v).isValid || 'Невалидно ЕГН'

          return true;
        }
      },
    }
  },
  mounted(){
    this.isNewDoc = true;
    if(this.$route.params.id != 'create'){
      this.data.id = this.$route.params.id;
      this.tabs = [
        { name: "Основни данни", key: "generalData" },
        { name: "Заповеди", key: "orders" },
        { name: "Вноски", key: "payments" },
        { name: "Обучение", key: "trainings" },
        { name: "Дела", key: "cases" },
        { name: "Сигнали", key: "signals" },
        { name: "Обжалвания", key: "appeals" }
      ]
      this.getData();
    } else {
      this.tabs = [
        { name: "Основни данни", key: "generalData" },
      ]
    }

    this.getSpecialities();
    this.getStatuses();
    this.getDistricts();
  },
  computed: {
  },
  methods: {
    getData(){
      apiGetSyndicById(this.data.id).then(result => {
        if(result){
          this.$set(this, "data", Object.assign(new SyndicModel(), result));
          this.getAddressNomenclatures();
          this.isNewDoc = false;
        }
      })
    },
    getSpecialities(){
      apiMetaDataGetSpecialties().then(result => {
        this.nomenclatures.specialities = result;
      })
    },
    getStatuses(){
      apiMetaDataGetSyndicStatuses().then(result => {
        this.nomenclatures.statuses = result;
      })
    },
    save(){
      if(this.$refs.form[0].validate()){
        if(this.isNewDoc){
          apiCreateSyndic(this.data).then(result => {
            if(this.isNewDoc && result && result != '00000000-0000-0000-0000-000000000000')
              this.$router.push({path: `/trusted-persons/${result}`})
          })
        } else {
          apiUpdateSyndic(this.data).then(result => {
            if(result)
              this.$set(this, 'data', result)
          })
        }
      }
    },
    getOrders(){

    },
    getAddressNomenclatures(){
      if(this.data.address.regionId)
        apiMetaDataGetMunicipalities(this.data.address.regionId).then((result) => {
          this.$set(this.nomenclatures, "municipalities", result);

          if(this.data.address.municipalityId)
            apiMetaDataGetSettlements(this.data.address.municipalityId).then((result) => {
              this.$set(this.nomenclatures, "settlements", result);
            });
        });
    },
    getDistricts(){
      apiMetaDataGetDistricts().then((result) => {
        this.$set(this.nomenclatures, "districts", result);
      });
    },
    getMunicipalities() {
      apiMetaDataGetMunicipalities(this.data.address.regionId).then((result) => {
        this.$set(this.nomenclatures, "municipalities", result);
        this.$set(this.data.address, "settlementId", null);
        this.$set(this.data.address, "municipalityId", null);
      });
    },
    getSettlements() {
      apiMetaDataGetSettlements(this.data.address.municipalityId).then((result) => {
        this.$set(this.nomenclatures, "settlements", result);
        this.$set(this.data.address, "settlementId", null);
      });
    },
    tabChange(tabKey, force = false) {
      if(tabKey && this.tables[tabKey] && this.tables[tabKey].data === null || force)
        switch (tabKey) {
          case "orders":
            this.getOrdersData()
          break;
          case "payments":
            this.getPaymentsData()
          break;
          case "trainings":
            this.getTrainingsData()
          break;
          case "cases":
            this.getCasesData();
          break;
          case "signals":
            this.getSignalsData();
          break;
          case "appeals":
            this.getAppealsData();
          break;
        }
    },
    tableActions({ action, rowData }) {
      switch (action) {
        case "previewOrder":
          this.previewOrder(rowData);
        break;
        case "previewPayment":
          this.previewPayment(rowData);
        break;
        case "previewTraining":
          this.previewTraining(rowData);
        break;
        case "previewCase":
          this.previewCase(rowData);
        break;
        case "previewSignal":
          this.previewSignal(rowData);
        break;
        case "previewAppeal":
          this.previewAppeal(rowData);
        break;
        case "previewCase":
          this.$router.push('/cases/stabilization/' + rowData.id);
        break;
      }
    },

    // ORDERS
    getOrdersData(){
      let query = Object.assign({}, {});
      
      query.id = this.data.id;
      query.pageNumber = this.tables.orders.pagination.skip / this.tables.orders.pagination.take + 1;
      query.pageSize = this.tables.orders.pagination.take;

      apiGetSyndicOrders(query).then((result) => {
        this.tables.orders.data = result.items;
        this.tables.orders.pagination.total = result.totalCount;
        this.tables.orders.pagination.page = result.currentPage;
      });
    },
    previewOrder(data){
      this.$refs.syndicOrderModal.openModal(data.id);
    },
    addNewOrder(){
      this.$refs.syndicOrderModal.openModal();
    },



    // PAYMENTS
    getPaymentsData(){
      let query = Object.assign({}, {});
      
      query.id = this.data.id;
      query.pageNumber = this.tables.payments.pagination.skip / this.tables.payments.pagination.take + 1;
      query.pageSize = this.tables.payments.pagination.take;

      apiGetSyndicInstallments(query).then((result) => {
        this.tables.payments.data = result.items;
        this.tables.payments.pagination.total = result.totalCount;
        this.tables.payments.pagination.page = result.currentPage;
      });
    },
    previewPayment(data){
      this.$refs.syndicPaymentModal.openModal(data.id);
    },
    addNewPayment(){
      this.$refs.syndicPaymentModal.openModal();
    },
    sendEmailNotificationForRequiredPaymentAsk(){
      let confirmData = {
        title: "Изпращане на съобщение",
        body: `Сигурни ли сте, че искате да изпратите съобщение за изтичащ срок за плащане на синдика?`,
        callback: this.sendEmailNotificationForRequiredPayment
      };
      this.$refs.confirmModal.openModal(confirmData);
    },
    sendEmailNotificationForRequiredPayment(){
      apiSendExpirationDateEmail(this.data.id).then(result => {
        this.$refs.confirmModal.closeModal();
      })
    },
    exportPaymentsTable(){
      apiExportSyndicInstallmentsToExcel(this.data.id).then(result => {
        downloadFileFromResponse(result);
      })
    },




    // TRAININGS
    getTrainingsData(){
      if(this.currentTrainingsTab === 0){
        this.getTrainingsApplicationsData();
      } else {
        this.getTrainingResultsData();
      }
    },
    getTrainingsApplicationsData(){
      let query = Object.assign({}, {});
      
      query.id = this.data.id;
      query.pageNumber = this.tables.payments.pagination.skip / this.tables.payments.pagination.take + 1;
      query.pageSize = this.tables.payments.pagination.take;
      
      //apiGetSyndicCourses(query).then(result => {
      apiGetSyndicCourseApplications(query).then(result => {
        this.tables.trainings.data = result.items;
        this.tables.trainings.pagination.total = result.totalCount;
        this.tables.trainings.pagination.page = result.currentPage;
      })
    },
    previewTraining(data){
      this.$refs.syndicTrainingModal.openModal(data.id)
    },
    addNewTraining(){
      this.$refs.syndicTrainingModal.openModal();
    },
    exportTrainingsTable(){
      apiExportSyndicCoursesToExcel(this.data.id).then(result => {
        downloadFileFromResponse(result);
      })
    },

    // TRAINING RESULTS
    getTrainingResultsData(){
      let query = Object.assign({}, {});
      
      query.id = this.data.id;
      query.pageNumber = this.tables.trainingResults.pagination.skip / this.tables.trainingResults.pagination.take + 1;
      query.pageSize = this.tables.trainingResults.pagination.take;
      
      apiGetSyndicCourseResults(query).then(result => {
        this.tables.trainingResults.data = result.items;
        this.tables.trainingResults.pagination.total = result.totalCount;
        this.tables.trainingResults.pagination.page = result.currentPage;
      })
    },
    exportTrainingResultsTable(){
      apiExportSyndicCourseResultsToExcel(this.data.id).then(result => {
        downloadFileFromResponse(result);
      })
    },

    // CASES
    getCasesData(){
      let query = Object.assign({}, {});
      
      query.id = this.data.id;
      query.pageNumber = this.tables.cases.pagination.skip / this.tables.cases.pagination.take + 1;
      query.pageSize = this.tables.cases.pagination.take;
      if(this.$refs.syndicsCasesFilter){
        query.filter = this.$refs.syndicsCasesFilter.filters;
      } else {
        query.filter = {page: 0};
      }

      query.filter.isStabilization = true;
      
      if(query.filter.page === 1){
        query.pageNumber = 1;
        this.tables.cases.pagination.skip = 0;
      }

      delete query.filter.page;

      apiGetSyndicCases(query).then((result) => {
        this.tables.cases.data = result.items;
        this.tables.cases.pagination.total = result.totalCount;
        this.tables.cases.pagination.page = result.currentPage;
      });
    },
    previewCase(data){
      this.$router.push({path: `/cases/stabilization/${data.id}`})
    },


    // SIGNALS
    getSignalsData(){
      let query = Object.assign({}, {});
      
      query.id = this.data.id;
      query.pageNumber = this.tables.signals.pagination.skip / this.tables.signals.pagination.take + 1;
      query.pageSize = this.tables.signals.pagination.take;
      

      apiGetSyndicSignals(query).then((result) => {
        this.tables.signals.data = result.items;
        this.tables.signals.pagination.total = result.totalCount;
        this.tables.signals.pagination.page = result.currentPage;
      });
    },
    previewSignal(data){
      this.$refs.syndicSignalModal.openModal(data.id)
    },
    addNewSignal(){
      this.$refs.syndicSignalModal.openModal();
    },


    


    // APPEALS
    getAppealsData(){
      let query = Object.assign({}, {});
      query.id = this.data.id;
      query.pageNumber = this.tables.appeals.pagination.skip / this.tables.appeals.pagination.take + 1;
      query.pageSize = this.tables.appeals.pagination.take;
      
      //apiGetSyndicAppeals(query).then((result) => {
      apiGetSyndicPleas(query).then(result => {
        this.tables.appeals.data = result.items;
        this.tables.appeals.pagination.total = result.totalCount;
        this.tables.appeals.pagination.page = result.currentPage;
      });
    },
    previewAppeal(data){
      this.$refs.syndicAppealModal.openModal(data.id)
    },
    addNewAppeal(){
      this.$refs.syndicAppealModal.openModal();
    },
    renderCaseNumberYear(h, tdElement , props ) {
      return h('td', null, [
        props.dataItem.caseNumber + ' / '+props.dataItem.caseYear
      ]);
    },
    renderOrderNumberAndDate(h, tdElement , props ) {
      return h('td', null, [
        props.dataItem.orderNumber + ' / '+ISODateString(props.dataItem.orderDate)
      ]);
    },
    renderPleaNumberAndDate(h, tdElement , props ) {
      return h('td', null, [
        props.dataItem.pleaNumber + ' / '+ISODateString(props.dataItem.pleaDate)
      ]);
    },
    renderActsLink(h, tdElement , props) {
      let item = props.dataItem
      return h('td', {class: 'text-center'}, [
        h('v-btn', {
          props: {
            icon: false,
            text: true,
            small: true,
            color: 'primary',
          },
          on: {
            click: () => {
              this.$router.push(`/cases/stabilization/${item.id}/acts`)
            },
          },
        }, [
          'Актове',
        ]),
      ]);
    },

    getConfirm(confirmData){
      this.$refs.confirmModal.openModal(confirmData);
    },
    ISODateString,
    formatDateTime
  }
}
</script>