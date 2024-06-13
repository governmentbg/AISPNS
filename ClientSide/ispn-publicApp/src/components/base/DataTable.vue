<template>
  <div>
    <v-row class="mt-2" v-if="hasPaging || (!hasPaging && $slots['buttons'])">
      <v-col cols="12" :md="$slots['buttons'] ? 4 : 12" class="d-print-none">
        <base-select
          :items="pagination.perPageOptions"
          v-model="pagination.itemsPerPage"
          item-text="Value"
          item-value="Key"
          :placeholder="$t('please_select')"
          :label="$t('results_on_page')"
          :color="color"
          @change="changeRowsPerPage"
          style="width:200px"
        >
        </base-select>
      </v-col>
      <v-col cols="12" md="8" v-if="$slots['buttons']" class="text-right">
        <slot name="buttons" />
      </v-col>
    </v-row>
    
    <v-row>
      <v-col cols="12" class="pb-0">
        <v-data-table
          ref="table"
          :headers="headers"
          :items="data"
          :class="`elevation-2${tableClass ? ' '+tableClass : ''}`"
          disable-pagination
          mobile
          hide-default-footer
          :dense="dense"
          :sort-by.sync="customSortColumn"
          :sort-desc.sync="customSortDesc"
          :disable-sort="!data || !data.length"
          mobile-breakpoint="200"
        >
          <template
            v-slot:body="{ items }"
          >
            <tbody>
              <template v-if="items && items.length">
                <tr
                  v-for="(item, idx_row) in items"
                  :key="`row_${idx_row}`"
                  :class="rowClass(item)"
                >
                  <template v-for="(cell, idx) in headers">
                    <td :key="'td_'+idx" v-if="cell.valueType !== 'button' && cell.valueType !== 'customLink' && cell.valueType !== 'status' && cell.valueType !== 'textBalance' && cell.valueType !== 'checkbox' && cell.valueType !== 'invoiceItemRowSelection' && cell.valueType !== 'invoiceItemName' && cell.valueType !== 'isPaid' && cell.valueType !== 'isPaidDropdown' && cell.valueType !== 'color' && cell.valueType !== 'translateValueColor'" :class="(cell.class ? cell.class : '') + (cell.cellClass ? ' '+cell.cellClass : '')" :style="cell.style" v-html="getValue(item, cell, idx_row)"></td>
                    <td :key="'td_'+idx" v-if="cell.valueType === 'invoiceItemName'" :class="'fs14 '+(cell.class ? cell.class : '') + (cell.cellClass ? ' '+cell.cellClass : '')" :style="cell.style">
                      <v-icon left color="primary" :title="$t('transformed_from_proforma_invoice_m')" v-if="item.transformedFromProformaInvoice" class="fs18" @click="$router.push({path: `/proforma-invoices/${item.transformedFromProformaInvoice}`})">mdi-file-arrow-left-right</v-icon>
                      {{ item.name[$i18n.locale] }}
                      <small style="display:block;font-size:90%" class="grey--text lighten-1" v-if="item.comment && item.comment[$i18n.locale] && cell.showComments">{{ item.comment[$i18n.locale] }}</small>
                    </td>
                    <td :key="'td_'+idx" v-if="cell.valueType === 'translateValueColor'" :class="'fs14 '+(cell.class ? cell.class : '') + (cell.cellClass ? ' '+cell.cellClass : '')" :style="cell.style">
                      <span :class="getColor(item, cell)">{{getValue(item, cell, idx_row)}}</span>
                    </td>
                    <td :key="'td_'+idx" v-if="cell.valueType === 'textBalance'" :class="(cell.class ? cell.class : '') + (cell.cellClass ? ' '+cell.cellClass : '')" :style="cell.style">
                      <template v-if="getBalanceCondition(item, cell)">
                        <span class="blue--text darken-4">
                          {{getValue(item, cell)}}
                        </span>
                      </template>
                      <template v-else>
                        <span class="red--text darken-4">
                          {{getValue(item, cell)}}
                        </span>
                      </template>
                    </td>
                    <td :key="`td_${idx}`" v-if="cell.valueType === 'customLink'" :class="(cell.class ? cell.class : '') + (cell.cellClass ? ' '+cell.cellClass : '')" :style="cell.style">
                      <a class="text-decoration-none" :title="cell.linkName" @click="$router.push(cell.path.replace('$$$', item.id))">{{cell.linkName}}</a>
                    </td>
                    <td :key="'td_'+idx" v-if="cell.valueType === 'status'" :class="cell.class + ' ' + (cell.active === item[cell.displayValue || cell.value] ? cell.activeClass : (cell.notActive === item[cell.displayValue || cell.value] ? cell.notActiveClass : '')) +' ' + cell.cellClass" :style="cell.style">
                      <template v-if="cell.statusIcons">
                        <v-icon class="success--text" v-if="item[cell.displayValue || cell.value] === cell.active">mdi-check</v-icon>
                        <v-icon class="error--text" v-if="item[cell.displayValue || cell.value] === cell.notActive">mdi-close</v-icon>
                      </template>
                      <template v-else>
                        <template v-if="item[cell.displayValue || cell.value] == cell.active">
                          {{cell.activeText}}
                        </template>
                        <template v-else-if="item[cell.displayValue || cell.value] == cell.notActive">
                          {{cell.notActiveText}}
                        </template>
                        <template v-else>
                          {{getValue(item, cell)}}
                        </template>
                      </template>
                    </td>
                    <td :key="'td_'+idx" v-if="cell.valueType === 'checkbox'" :class="(cell.class ? cell.class : '') + (cell.cellClass ? ' '+cell.cellClass : '')" :style="cell.style">
                      <v-checkbox
                        v-model="item[cell.displayValue || cell.value]"
                        color="primary"
                        :value="cell.checked"
                        :false-value="cell.unchecked"
                        hide-details
                        class="mt-0"
                        @change="changeCheckboxValue(item)"
                        v-if="false"
                      />
                    </td>
                    <td :key="'td_'+idx" v-if="cell.valueType === 'invoiceItemRowSelection'" :class="(cell.class ? cell.class : '') + (cell.cellClass ? ' '+cell.cellClass : '')" :style="cell.style">
                      <v-checkbox
                        v-model="selected"
                        color="primary"
                        :value="item[cell.value]"
                        :false-value="null"
                        hide-details
                        :ripple="false"
                        small
                        class="mt-0 mx-1"
                        style="width: 20px"
                        @change="changeCheckboxValue(item._id)"
                        v-if="item.isPaid === 1 && (cell.disabledByValue ? !(cell.disabledByValue === item[cell.disabledByItem]) : true)"
                      />
                      <v-icon color="success" :title="$t('view_invoice')" @click="$router.push({path: `/invoices/${item.transformedToInvoice}`})" v-if="item.isTransformed && item.transformedToInvoice" style="margin-left: 5px !important;">mdi-receipt-text-check-outline</v-icon>
                    </td>
                    <td :key="'td_pay_'+idx" v-if="cell.valueType === 'isPaid'" :class="'fs14 text-center '+(cell.class ? cell.class : '') + (cell.cellClass ? ' '+cell.cellClass : '') + (item['isPaid'] === 0 ? ' error' : (item['isPaid'] === 1 ? ' success' : ' warning')) + ' payment-cell'" :style="cell.style" @click="isManagementUser ? openPaymentsModal(item._id) : null">
                      <v-icon color="white" v-if="item['isPaid'] === 0">mdi-currency-usd-off</v-icon>
                      <v-icon color="white" v-if="item['isPaid'] === 1">mdi-check</v-icon>
                      <v-icon color="white" v-if="item['isPaid'] === 2">mdi-check-all</v-icon>
                    </td>
                    <td :key="'td_pay_'+idx" v-if="cell.valueType === 'color'" :class="'fs14 text-center '+(cell.class ? cell.class : '') + (cell.cellClass ? ' '+cell.cellClass : '')" :style="cell.style">
                      <div :style="`height: 20px;width: 20px;border-radius: 4px;transition: border-radius 200ms ease-in-out 0s;display: inline-block;background-color:${item[cell.value]}`" />
                    </td>
                    <td :key="'td_pay_'+idx" v-if="cell.valueType === 'isPaidDropdown'" :class="'fs14 text-center '+(cell.class ? cell.class : '') + (cell.cellClass ? ' '+cell.cellClass : '') + (item['isPaid'] === 0 ? ' error' : (item['isPaid'] === 1 ? ' success' : ' warning')) + ' payment-cell'" :style="cell.style">
                      <v-menu 
                        offset-y
                        bottom
                        rounded="0"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-btn
                            color="primary"
                            text
                            style="min-width: 40px;"
                            class="px-2"
                            v-bind="attrs"
                            v-on="on"
                          >
                            <v-icon color="white" v-if="item['isPaid'] === 0">mdi-currency-usd-off</v-icon>
                            <v-icon color="white" v-if="item['isPaid'] === 1">mdi-check</v-icon>
                            <v-icon color="white" v-if="item['isPaid'] === 2">mdi-check-all</v-icon>
                          </v-btn>
                        </template>
                        <v-list>
                          <v-list-item link dense>
                            <v-list-item-title  @click="setPaid(item._id, 1)">{{ $t('paid') }}</v-list-item-title>
                          </v-list-item>
                          <v-list-item link dense>
                            <v-list-item-title  @click="setPaid(item._id, 2)">{{ $t('partially_paid') }}</v-list-item-title>
                          </v-list-item>
                          <v-list-item link dense>
                            <v-list-item-title  @click="setPaid(item._id, 0)">{{ $t('not_paid') }}</v-list-item-title>
                          </v-list-item>
                        </v-list>
                      </v-menu>
                    </td>
                    <td :key="'td_'+idx" v-if="cell.valueType === 'button' && (Object.prototype.hasOwnProperty.call(cell, 'show') ? cell.show : true)" :class="(cell.class ? cell.class : '') + (cell.cellClass ? ' '+cell.cellClass : '')" :style="cell.style" class="d-print-none">
                      <template v-for="(button, bIdx) in cell.buttons">
                        <v-btn v-if="checkButton(button, item)" :key="'td_button_'+bIdx" :elevation="button.elevation ? button.elevation : 0" depressed :block="button.block" :title="button.title" @click="doAction(button.click, item)" :class="`my-1 v-size--${button.size} ` +button.class" :disabled="button.disabledFn ? button.disabledFn(item, idx_row) : false">
                          <v-icon :class="(button.label ? 'mr-2' : 'mr-0') + (button.class.includes('transparent') ? ' fs22' : '')" v-if="button.icon && button.icon.length ? true : false">{{button.icon}}</v-icon>
                          {{button.label}}
                        </v-btn>
                      </template>
                    </td>
                  </template>
                </tr>
              </template>
              <template v-else>
                <tr>
                  <td :colspan='headers.length' class="text-center px-8">
                    <v-row>
                      <v-col cols="12" class="pa-7">
                        <h4 class="display-1 font-weight-medium blue-grey--text text--darken-2">{{ no_results_text ? no_results_text : $t("no_table_results") }}</h4>
                      </v-col>
                    </v-row>
                  </td>
                </tr>
              </template>
            </tbody>
          </template>

          <template v-slot:footer v-if="printable || exportable">
            <v-row class="d-print-none">
              <v-col cols="12" md="12" class="text-right px-5 py-3 pt-7">
                <v-spacer></v-spacer>
                <v-btn small :class="`btn btn-${color} ${color}`" @click="doExport" v-if="exportable && data.length">
                  <v-icon class="mr-2">mdi-export-variant</v-icon>
                  {{ $t('export') }}
                </v-btn>
                <v-btn small :class="`btn btn-${color} ${color}`" @click="doPrint" v-if="printable && data.length">
                  <v-icon class="mr-2">mdi-printer</v-icon>
                  {{ $t('print') }}
                </v-btn>
              </v-col>
            </v-row>
          </template>
        </v-data-table>
        <v-pagination 
          v-if="hasPaging"
          v-model="pagination.page"
          :color="color"
          :length="Math.ceil(pagination.total / pagination.itemsPerPage)"
          :total-visible="10"
          @input="changedPagination"
          class="mb-5 d-print-none"
        />
      </v-col>
    </v-row>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import moment from "moment";
import { formatDateTime, formatDateTimeSeconds, getArrayField, ISODateString, roundNumber, secondsToDHMS, bytesToSize } from "@/utils"
export default {
  name: "DataTable",
  props: {
    id: {
      type: String,
      default: ''
    },
    headers: {
      type: Array,
      default: () => []
    },
    pagination: {
      type: Object,
      default: () => {
        return {
          take: 10,
          page: 1,
          perPageOptions: [5, 10, 25, 50, 100],
          total: 0
        }
      }
    },
    hasPaging: {
      type: Boolean,
      default: true
    },
    data: {
      type: Array,
      default: () => []
    },
    printable: {
      type: Boolean,
      default: false
    },
    dense: {
      type: Boolean,
      default: true
    },
    exportable: {
      type: Boolean,
      default: false
    },
    color: {
      type: String,
      default: 'primary'
    },
    sortBy: {
      type: String,
      default: () => ''
    },
    sortDesc: {
      type: Boolean,
      default: () => true
    },
    rowClass: {
      type: Function,
      default: () => null
    },
    tableClass: {
      type: String,
      default: ''
    },
    no_results_text: {
      type: String,
      default: null
    }
  },
  data(){
    return {
      sortColumn: [],
      sortDescending: [],
      selected: []
    }
  },
  // watch: {
  //   sortColumn(value){
  //     this.$emit('update:sortColumnName', value);
  //   },
  //   sortDesc(val){
  //     this.$emit('update:sortDescending', val);
  //     this.$emit("getData", this.id);
  //   }
  // },
  computed: {
    ...mapGetters([
      'isManagementUser'
    ]),
    customSortColumn: {
      get: function(){
        return this.sortColumn;
      },
      set: function(value){
        this.sortColumn = value;
        this.$emit('update:sortBy', value);
        this.$emit("getData", this.id);
        this.$forceUpdate();
      }
    },
    customSortDesc: {
      get: function(){
        return this.sortDesc;
      },
      set: function(value){
        this.sortDescending = value;
        this.$emit('update:sortDesc', value);
        this.$emit("getData", this.id);
        this.$forceUpdate();
      }
    },
    // customSortColumn: {
    //   get: function(){
    //     return this.sortColumn;
    //   },
    //   set: function(value){
    //     if(value instanceof Array && value.length > 1){
    //       value = [value[0]];
    //     }
    //     this.sortColumn = value;
    //     this.$emit('update:sortBy', value);
    //   }
    // },
    // customSortDesc: {
    //   get: function(){
    //     return this.sortDesc;
    //   },
    //   set: function(value){
    //     if(value instanceof Array && value.length > 1){
    //       value = [value[0]];
    //     }
    //     this.sortDescending = value;
    //     this.$emit('update:sortDesc', value);
    //     this.$emit("getData", this.id);
    //   }
    // }
  },
  created(){
    this.sortColumn = this.sortBy;
    this.sortDescending = this.sortDesc;
  },
  methods: {
    getData(){
      this.$emit("getData", this.id);
    },
    changeRowsPerPage(){
      if((this.pagination.total > 0 && this.data) && this.pagination.page >= Math.ceil(this.pagination.total / this.pagination.itemsPerPage)){
        this.pagination.page = Math.ceil(this.pagination.total / this.pagination.itemsPerPage)
      }
      this.getData();
    },
    checkButton(button, item){
      let ifValue = true;
      
      if(button.if){
        if(button.ifValueObjectExists){
          let props = button.ifValueObjectExists.split('.');
          if(item[props[0]]){
            let value = props.reduce((a, v) => a[v], item);

            if(!value)
              ifValue = false
          }

        }
        
        return (button.if ? 
          (button.if.type ? 
            (button.if.type === "equal" ? 
              item[button.if.item] === button.if.value : 
                (button.if.type === "notEmptyList" ? 
                  item[button.if.item].length : 
                    (button.if.type === "notEmpty" ? 
                      item[button.if.item] : false ))) :
                  (button.if.fn ? button.if.fn(item) : item[button.if.item] !== button.if.value)) : true) && ifValue
      } else if(Object.prototype.hasOwnProperty.call(button, "show")){
        return button['show'];
      }

      return true;
    },
    getColor(item, options){
      let result = '';
      let prop = options.displayValue || options.value;
      for(let obj of options.translateValues){
        if(obj.value === item[prop])
          result = obj.color
      }
      return result;
    },
    getValue(item, options, idx){
      let template = '';
      let keys = [];
      let props = [];
      let prop = options.displayValue || options.value;
      let result;
      let itemData;
      switch(options.valueType){
        case 'text':
          return item[prop] || item[prop] === 0 ? item[prop] : ' — ';
        case 'textBalance':
          // result = (item[options.value] + '').split('.')[1];
          // if(result && result.length <= 1 || (item[options.value] + '').length === 1){
          return roundNumber(item[prop || options.value], 2)
          // }
          // return item[options.value];
        case 'price':
          return roundNumber(item[prop || options.value], 2) + ` ${options.currency ? options.currency : item.currency?.name?.[this.$i18n.locale] ? item.currency.name[this.$i18n.locale] : this.$t("bgn")}`;
        case 'onlyPrice':
          return roundNumber(item[prop || options.value], 2)
        case 'onlyPriceMultiplyByRate':
          return roundNumber(item[prop || options.value] * options.exchangeRate, 2)
        case 'priceAccounting':
          var num = item[prop || options.value];
          if(typeof num === 'number')
            num = num+'';
      
          var value = Number(num);      
          
          var res = num.split(".");
          if(res.length == 1 || res[1].length < 3)
            value = roundNumber(value, 2);
            
          return value;
        case 'array':
          // eslint-disable-next-line no-case-declarations
          let val = getArrayField(item, prop, options.comma);
          if(val && val !== 'null'){
            return val;
          }
          return '';
          //return val != null ? val : '';
        case 'date':
          if(item[prop]){
            // eslint-disable-next-line no-case-declarations
            let d = moment();

            if((typeof item[prop] === 'number' && item[prop].toString().length === 8) || (typeof item[prop] === 'string' && item[prop].length === 8)){
              d = moment(item[prop], "DDMMYYYY");
              if(!d.isValid())
                d = moment(item[prop], "YYYYMMDD");
            } else if((typeof item[prop] === 'number' && item[prop].toString().length === 14) || (typeof item[prop] === 'string' && item[prop].length === 14)) {
              d = moment(item[prop], "YYYYMMDDHHmmss")
            } else {
              d = moment(item[prop]);
            }
            if(d.isValid()){
              return ISODateString(d.toISOString());
            }
          }

          return '—';
        case 'dateTime':
          if(item[prop]){
            // eslint-disable-next-line no-case-declarations
            let d = moment(item[prop]);
            if(d.isValid()){
              return formatDateTime(item[prop], this);
            }
          }

          return '—';
        case 'dateTimeSeconds':
          if(item[prop]){
            // eslint-disable-next-line no-case-declarations
            let d = moment(item[prop]);
            if(d.isValid()){
              return formatDateTimeSeconds(item[prop]);
            }
          }

          return '—';
        case 'firstElement':
          props = prop.split('.');
          return item[props[0]] ? item[props[0]][0][props[1]] : '';
        case 'firstElementObject':
          result = '';
          keys = prop.split('.');
          var firstChildren = item[keys[0]][0];
          keys.shift();
          if(firstChildren){
            if(firstChildren[keys[0]]){
              result = keys.reduce((a, v) => a[v], firstChildren);
            }
          }
          return result ? result : '';
        case 'object':
          keys = prop.split('.');
          // eslint-disable-next-line no-case-declarations
          result = '';
          try { 
            if(item[keys[0]]){
              result = keys.reduce((a, v) => a[v], item);
            }
            return result ? result : ' — ';
          } catch(e){
            return ' — '
          }
        case 'objectDate':
          keys = prop.split('.');
          
          // eslint-disable-next-line no-case-declarations
          let date = keys.reduce((a, v) => a[v], item);
          
          if(date){
            // eslint-disable-next-line no-case-declarations
            let d;
            if(typeof date === 'number' && date.toString().length === 8){
              d = moment(date, "YYYYMMDD");
            } else {
              d = moment(date);
            }
            
            if(d.isValid()){
              return ISODateString(d.toISOString());
            }
          }

          return ' — ';
        case 'duration':
          result = '';

          result = secondsToDHMS(item[prop])

          return result;
        case 'objectPrice':
          keys = prop.split('.');
          // eslint-disable-next-line no-case-declarations
          result = '';
          try {
            if(item[keys[0]]){
              result = keys.reduce((a, v) => a[v], item);
            }
            return result ? roundNumber(result, 2) : ' — ';
          } catch(e){
            return ' — '
          }
        case 'twoObjectsPrice':
          result = '';
          try { 
            for(let obj of prop){
              keys = obj.split('.');
              if(item[keys[0]]){
                if(result.length){
                  let val = keys.reduce((a, v) => a[v], item);
                  result += val ? ' ' + val : '';
                } else {
                  result += roundNumber(keys.reduce((a, v) => a[v], item), 2);
                }
              }
            }
            
            return result ? result : ' — ';
          } catch(e){
            return ' — '
          }
        case 'moreThanOneField':
          result = '';
          try { 
            for(let obj of prop){
              keys = obj.split('.');
              if(item[keys[0]]){
                if(result.length){
                  let val = keys.reduce((a, v) => a[v], item);
                  result += val ? ' ' + val : '';
                } else {
                  result += keys.reduce((a, v) => a[v], item);
                }
              }
            }
            
            return result ? result : ' — ';
          } catch(e){
            return ' — '
          }
        case 'valueOrValue':
          keys = prop.split('.');

          try {
            if(item[keys[0]]){
              return item[keys[0]]
            }

            return item[keys[1]] || ' — ';
          } catch(e){
            return ' — '
          }
        case 'menuName':
          if(item.type === 0){
            return item.page.name[this.$i18n.locale];
          }
          return item.name[this.$i18n.locale];
        case 'menuLink':
          if(item.type === 0){
            return item.page.url;
          }
          return item.url;
        case 'valueOrValueObject':
          try {
            for(let i=0;i<prop.length;i++){
              if(typeof prop[i] === 'string'){
                keys = prop[i].split('.');
                if(item[keys[0]]){
                  result = keys.reduce((a, v) => a[v], item);
                  if(result)
                    break
                }
              } else {
                result = '';
                for(let obj of prop[i]){
                  keys = obj.split('.');
                  if(item[keys[0]]){
                    if(result.length){
                      let val = keys.reduce((a, v) => a[v], item);
                      result += val ? ' ' + val : '';
                    } else {
                      result += keys.reduce((a, v) => a[v], item);
                    }
                  }
                }
              }
            }

            return result ? result : ' — ';
          } catch(e) {
            return ' — '
          }
        case 'combineValuesObject':
          try {
            let separator = ' ';
            if(options.valueSeparator)
              separator = options.valueSeparator

            result = '';
            for(let obj of prop){
              keys = obj.split('.');
              if(item[keys[0]]){
                if(result.length){
                  let val = keys.reduce((a, v) => a[v], item);
                  try{
                    let date = Date.parse(val)
                    if(!isNaN(date)){
                      let dateVal = moment(val);
                      if(dateVal.isValid())
                        val = dateVal.format('DD.MM.YYYY');
                    }
                  } catch(e){}
                  result += val ? separator + val : '';
                } else {
                  if(keys.length > 1){
                    let val = keys.reduce((a, v) => a[v], item);
                    try{
                      let date = Date.parse(val)
                      if(!isNaN(date)){
                        let dateVal = moment(val);
                        if(dateVal.isValid())
                          val = dateVal.format('DD.MM.YYYY');
                      }
                    } catch(e){}
                    result += val;
                  } else {
                    let val = item[keys[0]];
                    try{
                      let date = Date.parse(val)
                      if(!isNaN(date)){
                        let dateVal = moment(val);
                        if(dateVal.isValid())
                          val = dateVal.format('DD.MM.YYYY');
                      }
                    } catch(e){}
                    result += val
                  }
                }
              }
            }

            return result ? result : ' — ';
          } catch(e) {
            return ' — '
          }
        case 'valueOrValueArray':
          result = '';
          for(let i=0;i<prop.length;i++){
            if(prop[i].includes('.')){
              keys = prop[i].split('.');
            } else {
              keys = [prop[i]];
            }

            for(let k=0;k<keys.length;k++){
              if(item[keys[k]]){
                if(result.length) result += options.separator || ' ';
                result += item[keys[k]];
              }
            }
          }
          return result;
        case 'button':
          for(let i=0;i<options.buttons.length;i++){
              template += ``;
          }
          return template;
        case 'sort':
          itemData = Object.assign([], item[options['sortBy']]);

          if(options['sortType'] === 'date'){
            itemData.sort(function(x, y){
              if(options['sortDesc']){
                return (new Date(y[options['sortValue']])).getTime() - (new Date(x[options['sortValue']])).getTime();
              } else {
                return (new Date(x[options['sortValue']])).getTime() - (new Date(y[options['sortValue']])).getTime();
              }
            })
          }

          if(prop.includes('.')){
            keys = prop.split('.');
            result = '';
            if(itemData[0][keys[0]]){
              result = keys.reduce((a, v) => a[v], itemData[0]);
            }
            return result ? result : ' — ';
          }

          return itemData[0][prop];
        case 'status':
          if (item[prop] != null){
            return item[prop].toString() || ' - ';
          }
          return ' - ';
        case 'fileSize':
          return bytesToSize(item[prop]);
        case 'translateValue':
        case 'translateValueColor':
          result = ' - ';
          for(let obj of options.translateValues){
            if(obj.value === item[prop])
              result = obj.label
          }
          return result;
        case 'count': 
          return (idx + 1) + '.'
        case 'address':
          result = '';
          
          let address = item[prop];

          if(address.regionName)
            result += address.regionName;

          if(address.municipalityName)
            result += (result.length ? ', ' : '') + address.municipalityName;

          if(address.settlementName)
            result += (result.length ? ', ' : '') + address.settlementName;
          
          if(address.postCode)
            result += (result.length ? ', ' : '') + address.postCode;

          if(address.streetName)
            result += (result.length ? ', ' : '') + address.streetName;

          if(address.streetNumber)
            result += (result.length ? ', ' : '') + address.streetNumber;

          return result;
      }
    },
    getBalanceCondition(item, options){
      let prop = options.displayValue || options.value;
      if(item[prop] >= 0){
        return true;
      }
      
      return false;
    },
    doAction(action, data){
      this.$emit("doAction", {action: action, item: data})
    },
    doPrint(){
      this.$emit("print");
    },
    doExport(){
      this.$emit("export");
    },
    changedPagination(){

      this.getData();
    },
    openPaymentsModal(id){
      this.$emit("openPaymentsModal", id)
    },
    changeCheckboxValue(){
      this.$emit("changeCheckbox", this.selected);
    },
    setPaid(id, paidType){
      this.$emit("setPaid", {itemId: id, isPaid: paidType})
    }
  }
}
</script>

<style lang="sass">
.invoiceTable
  table
    tbody
      tr
        td
          height: 40px !important
</style>