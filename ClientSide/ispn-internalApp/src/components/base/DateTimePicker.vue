<template>
  <v-dialog
    v-model="display"
    :width="dialogWidth"
    :persistent="persistent"
    @click:outside="outsiteHandler"
    @keydown.esc="outsiteHandler"
  >
    <template v-slot:activator="{ on }">
      <base-input
        v-bind="textFieldProps"
        :disabled="disabled"
        :loading="loading"
        :label="label"
        :value="formattedDatetime"
        v-on="on"
        :rules="rules"
        readonly
        v-if="!readonly"
      >
        <template v-slot:progress>
          <slot name="progress">
            <v-progress-linear color="primary" indeterminate absolute height="2"></v-progress-linear>
          </slot>
        </template>
      </base-input>
      <base-input
        v-bind="textFieldProps"
        :disabled="disabled"
        :loading="loading"
        :label="label"
        :value="formattedDatetime"
        :rules="rules"
        readonly
        v-else
      >
        <template v-slot:progress>
          <slot name="progress">
            <v-progress-linear color="primary" indeterminate absolute height="2"></v-progress-linear>
          </slot>
        </template>
      </base-input>
    </template>

    <v-card>
      <v-card-text class="px-0 py-0">
        <v-tabs fixed-tabs v-model="activeTab" v-if="!withoutTime" color="primary">
          <v-tabs-slider color="primary darken-4" />
          <v-tab key="calendar">
            <slot name="dateIcon">
              <v-icon>mdi-calendar</v-icon>
            </slot>
          </v-tab>
          <v-tab key="timer" :disabled="dateSelected">
            <slot name="timeIcon">
              <v-icon>mdi-clock</v-icon>
            </slot>
          </v-tab>
          <v-tab-item key="calendar">
            <v-date-picker 
              v-model="date" 
              v-bind="datePickerProps" 
              @input="showTimePicker" 
              full-width
              first-day-of-week="1"
              no-title
              scrollable 
              v-if="false"
            />
            <v-date-picker
              v-model="date"
              first-day-of-week="1"
              no-title
              scrollable
              :min="minDate ? minDate : '1900-01-01'"
              :max="maxDate ? maxDate : ''"
              full-width
              color="primary"
              @input="showTimePicker" 
              style="height:320px"
            />
          </v-tab-item>
          <v-tab-item key="timer">
            <v-time-picker
              ref="timer"
              class="v-time-picker-custom"
              v-model="time"
              v-bind="timePickerProps"
              full-width
              no-title
              scrollable 
              color="primary"
              v-if="false"
            ></v-time-picker>
            <v-time-picker
              v-model="time"
              full-width
              :max="maxTime ? maxTime : ''"
              :min="minTime ? minTime : '00:00'"
              color="primary"
              header-color="white"
              scrollable
              format="24hr"
            />
          </v-tab-item>
        </v-tabs>
        <v-date-picker
          v-if="withoutTime"
          v-model="date"
          v-bind="datePickerProps"
          @input="showTimePicker"
          first-day-of-week="1"
          no-title
          scrollable
          full-width
        ></v-date-picker>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <slot name="actions" :parent="this">
          <v-btn color="grey lighten-1" text @click.native="clearHandler" v-if="clearText.length > 0">{{
            clearText
          }}</v-btn>
          <v-btn color="green darken-1" text @click="okHandler">{{ okText }}</v-btn>
          <v-btn color="grey lighten-1" text @click.native="cancelHandler" v-if="cancelText.length > 0">{{
            cancelText
          }}</v-btn>
        </slot>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
// import {
//   VDialog,
//   VCard,
//   VCardText,
//   VTextField,
//   VBtn,
//   VSpacer,
//   VTabItem,
//   VTabs,
//   VDatePicker,
//   VCardActions,
// } from 'vuetify/lib/components'

import { format, parse, parseISO } from 'date-fns'

const DEFAULT_DATE = ''
const DEFAULT_TIME = '00:00:00'
const DEFAULT_DATE_FORMAT = 'yyyy-MM-dd'
const DEFAULT_TIME_FORMAT = 'HH:mm:ss'
const DEFAULT_DIALOG_WIDTH = 340
const DEFAULT_CLEAR_TEXT = "Изчисти"
const DEFAULT_OK_TEXT = "ОК"
const DEFAULT_CANCEL_TEXT = "Затвори"
import moment from "moment";
export default defineComponent({
  name: 'datetime-picker',
  components: {
    // VDialog,
    // VCard,
    // VCardText,
    // VTextField,
    // VBtn,
    // VSpacer,
    // VTabItem,
    // VTabs,
    // VDatePicker,
    // VCardActions,
  },
  model: {
    prop: 'datetime',
    event: 'input',
  },
  props: {
    datetime: {
      type: [Date, String],
      default: null,
    },
    disabled: {
      type: Boolean,
    },
    loading: {
      type: Boolean,
    },
    persistent: {
      type: Boolean,
      default: false,
    },
    label: {
      type: String,
      default: '',
    },
    dialogWidth: {
      type: Number,
      default: DEFAULT_DIALOG_WIDTH,
    },
    dateFormat: {
      type: String,
      default: 'dd.MM.yyyy',
    },
    timeFormat: {
      type: String,
      default: 'HH:mm',
    },
    clearText: {
      type: String,
      default: DEFAULT_CLEAR_TEXT,
    },
    okText: {
      type: String,
      default: DEFAULT_OK_TEXT,
    },
    cancelText: {
      type: String,
      default: DEFAULT_CANCEL_TEXT,
    },
    textFieldProps: {
      type: Object,
    },
    datePickerProps: {
      type: Object,
    },
    timePickerProps: {
      type: Object,
    },
    useIso: {
      type: Boolean,
      default: true,
    },
    withoutTime: {
      type: Boolean,
      default: false,
    },
    minDate: {
      type: String,
      default: null
    },
    maxDate: {
      type: String,
      default: null
    },
    rules: {
      type: Array,
      default: function(){
        return [];
      }
    },
    readonly: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      display: false,
      activeTab: 0,
      date: DEFAULT_DATE,
      time: DEFAULT_TIME,
    }
  },
  mounted() {
    this.init()
  },
  computed: {
    dateTimeFormat() {
      return this.withoutTime ? this.dateFormat : this.dateFormat + ' ' + this.timeFormat
    },
    defaultDateTimeFormat() {
      return DEFAULT_DATE_FORMAT + ' ' + DEFAULT_TIME_FORMAT
    },
    formattedDatetime() {
      return this.selectedDatetime ? format(this.selectedDatetime, this.dateTimeFormat) : ''
    },
    selectedDatetime() {
      if (this.date && this.time) {
        let datetimeString = this.date + ' ' + this.time
        if (this.time.length === 5) {
          datetimeString += ':00'
        }
        return parse(datetimeString, this.defaultDateTimeFormat, new Date())
      } else {
        return null
      }
    },
    dateSelected() {
      return !this.date
    },
    minTime(){
      if(this.minDate && moment(this.minDate).isValid()){

        if(moment(this.minDate).isSame(this.date, 'day')){
          //return "00:00"
          return moment(this.minDate).format("HH:mm")
        } else {
          return "00:00"
        }
      }

      return '00:00';
    },
    maxTime(){
      if(moment(this.maxDate).isValid()){
        if(moment(this.maxDate).isSame(this.date, 'day')){
          return moment(this.maxDate).format("HH:mm")
        } else {
          return '24:00'
        }
      }

      return '24:00';
    }
  },
  methods: {
    init() {
      if (!this.datetime) {
        this.date = DEFAULT_DATE
        this.time = DEFAULT_TIME
        return
      }

      let initDateTime = new Date()
      if (this.datetime instanceof Date) {
        initDateTime = this.datetime
      } else if (typeof this.datetime === 'string' || this.datetime instanceof String) {
        // see https://stackoverflow.com/a/9436948
        initDateTime = this.useIso ? parseISO(this.datetime) : parse(this.datetime, this.dateTimeFormat, new Date())
      }

      if (this.withoutTime) {
        initDateTime.setHours(0, 0, 0, 0)
      }

      this.date = format(initDateTime, DEFAULT_DATE_FORMAT)
      this.time = format(initDateTime, DEFAULT_TIME_FORMAT)
    },
    okHandler() {
      this.resetPicker()
      return this.useIso
        ? this.$emit('input', this.selectedDatetime.toISOString())
        : this.$emit('input', this.selectedDatetime)
    },
    clearHandler() {
      this.resetPicker()
      this.date = DEFAULT_DATE
      this.time = DEFAULT_TIME
      this.$emit('input', null)
    },
    cancelHandler() {
      this.resetPicker()
      this.init()
    },
    outsiteHandler() {
      if (!this.persistent) {
        this.cancelHandler()
      }
    },
    resetPicker() {
      this.display = false
      this.activeTab = 0
      if (this.$refs.timer) {
        this.$refs.timer.selectingHour = true
      }
    },
    showTimePicker() {
      this.activeTab = 1
    },
  },
  watch: {
    datetime: function () {
      this.init()
    },
  },
})
</script>

<style lang="sass">
.v-time-picker-title__time
  color: $primary
</style>