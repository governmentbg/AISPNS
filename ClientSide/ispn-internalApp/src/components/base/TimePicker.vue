<template>
  <v-menu
    ref="menu"
    v-model="menu"
    :close-on-content-click="false"
    transition="scale-transition"
    :return-value.sync="time"
    offset-y
    min-width="290px"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-text-field
        v-model="timeFormatted"
        :label="label"
        :dense="dense"
        :color="color"
        :hide-details="hideDetails"
        :disabled="disabled"
        prepend-icon="mdi-clock-time-four-outline"
        autocomplete="off"
        :rules="checkRules"
        :clearable="disabled ? false : clearable"
        @click:clear="clearInput"
        v-bind="attrs"
        v-on="on"
      ></v-text-field>
    </template>
    <v-time-picker
      v-if="menu"
      v-model="timeFormatted"
      :disabled="disabled"
      full-width
      :color="color"
      scrollable
      format="24hr"
      @click:minute="menu = false"
    />
  </v-menu>
</template>


<script>
  import moment from "moment";

  export default {
    name: 'TimePicker',
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
      hideDetails: {
        type: Boolean,
        default: false
      }
    },
    mounted(){
      this.checkRules = this.rules.concat([this.myRules.validTime])
    },
    data(){
      return {
        menu: false,
        time: null,
        checkRules: [],
        myRules: {
          validTime: val => {
            return (val && val.length ? moment(val, 'HH:mm').isValid() : true) || "Въведения час не е валиден."
          }
        }
      }
    },
    computed: {
      timeFormatted: {
        get(){
          return this.formatTime(this.value);
        },
        set(value){
          if(value && value.length === 5 && moment(value, "HH:mm").isValid()){
            this.$emit('input', this.parseTime(value));
          }
        }
      }
    },
    watch:{
      time: function(val){
        this.$emit('input', val)
      }
    },
    methods: {
      formatTime(val){
        return val ? moment(val).format("HH:mm") : ''
      },
      parseTime(val){
        return val ? moment(val, "HH:mm").format("HH:mm") : ''
      },
      clearInput(){
        this.$emit('input', '')
        //this.$emit('onClear', '')
      }
    }
  }
</script>