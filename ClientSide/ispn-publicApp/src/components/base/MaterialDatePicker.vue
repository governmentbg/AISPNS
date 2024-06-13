<template>
  <v-menu
    ref="menu"
    v-model="menu"
    :close-on-content-click="false"
    transition="scale-transition"
    offset-y
    min-width="290px"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-text-field
        v-model="dateFormatted"
        :outlined="outlined"
        dense
        :label="label"
        prepend-inner-icon="mdi-calendar"
        autocomplete="off"
        v-mask="'##.##.####'"
        :disabled="disabled"
        v-bind="attrs"
        :rules="checkRules"
        :clearable="isClearable"
        :data-declaration-row-guid="dataDeclarationRowGuid"
        :data-declaration-part-data-guid="dataDeclarationPartDataGuid"
        :data-serialization-name="dataSerializationName"
        @click:clear="clearInput"
        v-on="showDateModal(on)"
      ></v-text-field>
    </template>
    <v-date-picker
      v-model="date"
      no-title
      scrollable
      :disabled="disabled"
      :rules="rules"
      min="1900-01-01"
      color="primary"
      @input="menu = false"
    >
      <template v-if="false">
        <v-spacer></v-spacer>
        <v-btn
          text
          color="primary"
          @click="menu = false"
          class="no-padding pa-2"
        >
          Затвори
        </v-btn>
        <v-btn
          color="primary"
          @click="setDate"
          class="no-padding pa-2 mr-0"
        >
          Избери
        </v-btn>
      </template>
    </v-date-picker>
  </v-menu>
</template>


<script>
  import moment from "moment";

  export default {
    name: 'MaterialDatePicker',
    props: {
      label: {
        type: String,
        default: '',
      },
      value: {
        type: String,
        default: ''
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
      isClearable: {
        type: Boolean,
        default: true
      },
      outlined: {
        type: Boolean,
        default: false
      },
      dataDeclarationRowGuid: {
        type: String,
        default: null
      },
      dataDeclarationPartDataGuid: {
        type: String,
        default: null
      },
      dataSerializationName: {
        type: String,
        default: null
      }
    },
    mounted(){
      this.checkRules = this.rules.concat([this.myRules.validDate, this.myRules.minDate])
    },
    data(){
      return {
        menu: false,
        date: '',
        checkRules: [],
        myRules: {
          minDate: val => {
            return ((!val || !val.length ? true : moment(val, 'DD.MM.YYYY').isAfter(moment('1900-01-01'))) || "Датата трябва да е по-голяма от 1 Януари 1900г.")
          },
          validDate: val => {
            return (val && val.length ? moment(val, 'DD.MM.YYYY').isValid() : true) || "Въведената дата не е валидна."
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
      }
    },
    methods: {
      showDateModal(event){
        if(this.disabled) return {};

        return event;
      },
      setDate(){
        this.menu = false;
        this.$emit('input', this.date)
      },
      formatDate(val){
        return val ? moment(val, "YYYY-MM-DD").format("DD.MM.YYYY") : ''
      },
      parseDate(val){
        return val ? moment(val, "DD.MM.YYYY").format("YYYY-MM-DD") : ''
      },
      clearInput(){
        this.$emit('input', '')
        //this.$emit('onClear', '')
      }
    }
  }
</script>