<template>
  <v-menu
    ref="menu"
    v-model="menu"
    :close-on-content-click="false"
    transition="scale-transition"
    offset-y
    min-width="260px"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-text-field
        v-model="dateFormatted"
        :label="label"
        :dense="dense"
        :disabled="disabled"
        :readonly="readonly"
        :color="color"
        :hide-details="hideDetails"
        prepend-icon="mdi-calendar"
        autocomplete="off"
        :rules="checkRules"
        :clearable="disabled || readonly ? false : clearable"
        @click:clear="clearInput"
        @keyup="emitChange"
        v-on="disabled || readonly ? null : on"
        v-bind="attrs"
        :error-messages="errorMessages"
        :outlined="outlined"
      ></v-text-field>
    </template>
    <v-date-picker
      v-model="date"
      first-day-of-week="1"
      no-title
      scrollable
      :disabled="disabled || readonly"
      :rules="checkRules"
      :min="minDate ? minDate : '1900-01-01'"
      :max="maxDate ? maxDate : ''"
      width="260"
      :color="color"
      @input="menu = false"
      @change="emitChange"
    />
  </v-menu>
</template>


<script>
  import moment from "moment";

  export default {
    name: 'MaterialDatePicker',
    props: {
      label: {
        type: String,
        default: null,
      },
      value: {
        type: String,
        default: null
      },
      disabled: {
        type: Boolean,
        default: false
      },
      rules: {
        type: Array,
        default: function(){
          return [];
        }
      },
      color: {
        type: String,
        default: 'secondary'
      },
      clearable: {
        type: Boolean,
        default: true
      },
      dense: {
        type: Boolean,
        default: true
      },
      minDate: {
        type: String,
        default: null
      },
      maxDate: {
        type: String,
        default: null
      },
      hideDetails: {
        type: Boolean,
        default: false
      },
      readonly: {
        type: Boolean,
        default: false
      },
      fieldValidation: {
        type: String,
        default: null
      },
      rulesMessages: {
        type: Array,
        default: null
      },
      errorMessages: {
        type: Array,
        default: null
      },
      outlined: {
        type: Boolean,
        default: false
      }
    },
    mounted(){
      this.checkRules = this.rules.concat([this.myRules.validDate, this.myRules.minDate])
    },
    data(){
      return {
        menu: false,
        date: null,
        checkRules: [],
        myRules: {
          minDate: val => {
            return ((!val || !val.length ? true : moment(val, 'DD.MM.YYYY').isAfter(moment('1900-01-01'))) || "Датата трябва да е по-голяма от 1 Януари 1900г.")
          },
          validDate: val => {
            return (val && val.length ? moment(val, 'DD.MM.YYYY', true).isValid() : true) || "Въведената дата не е валидна."
          }
        }
      }
    },
    computed: {
      dateFormatted: {
        get(){
          return this.formatDate(this.value);
        },
        set(value){
          if(value && value.length === 10 && moment(value, "DD.MM.YYYY").isValid()){
            this.$emit('input', this.parseDate(value));
          }
        }
      }
    },
    watch:{
      date: function(val){
        this.$emit('input', val)

        if(this.rulesMessages && this.fieldValidation){
          let rulesMessages = this.rulesMessages.filter(rule => { return rule.field !== this.fieldValidation})
          this.$emit("update:rulesMessages", rulesMessages);
        }
      },
      rules(rules){
        this.checkRules = rules.concat([this.myRules.validDate, this.myRules.minDate])
      },
      value(val){
        this.date = val ? moment(val).format("YYYY-MM-DD") : null;
      }
    },
    methods: {
      showDateModal(event){
        if(this.disabled) return {};

        return event;
      },
      formatDate(val){
        return val ? moment(val, "YYYY-MM-DD").format("DD.MM.YYYY") : null
      },
      parseDate(val){
        return val ? moment(val, "DD.MM.YYYY").format("YYYY-MM-DD") : null
      },
      clearInput(){
        this.$emit('input', null)
      },
      emitChange(eventOrValue) {
        let value = null
        if(typeof eventOrValue === 'object'){
          value = eventOrValue.target.value;
        } else if(typeof eventOrValue === 'string'){
          value = eventOrValue;
        }

        this.$emit("change", value)
      }
    }
  }
</script>