<template>
  <div>
    <v-col cols="2" class="d-print-none">
      <base-select
        v-if="hasPaging"
        :items="pagination.perPageOptions"
        v-model="pagination.itemsPerPage"
        item-text="Value"
        item-value="Key"
        placeholder="-- Избери --"
        label="Резултати на страница"
        :color="color"
        @change="changeRowsPerPage"
      >
      </base-select>
    </v-col>

    <v-data-table
      ref="table"
      :headers="headers"
      :items="data"
      class="elevation-2"
      disable-pagination
      hide-default-footer
      :sort-by.sync="customSortColumn"
      :sort-desc.sync="customSortDesc"
      :disable-sort="!data || !data.length"
    >
      <template v-slot:body="{ items }">
        <tbody>
          <template v-if="items && items.length">
            <tr
              v-for="(item, idx) in items"
              :key="`row_${idx}`"
              :class="rowClass(item)"
            >
              <template v-for="(cell, idx) in headers">
                <td
                  :key="'td_' + idx"
                  v-if="
                    cell.valueType !== 'button' &&
                    cell.valueType !== 'status' &&
                    cell.valueType !== 'textBalance' &&
                    cell.valueType !== 'checkbox'
                  "
                  :class="cell.class"
                  :style="cell.style"
                >
                  {{ getValue(item, cell) }}
                </td>
                <td
                  :key="'td_' + idx"
                  v-if="cell.valueType === 'textBalance'"
                  :class="cell.class"
                  :style="cell.style"
                >
                  <template v-if="getBalanceCondition(item, cell)">
                    <span class="blue--text darken-4">
                      {{ getValue(item, cell) }}
                    </span>
                  </template>
                  <template v-else>
                    <span class="red--text darken-4">
                      {{ getValue(item, cell) }}
                    </span>
                  </template>
                </td>
                <td
                  :key="'td_' + idx"
                  v-if="cell.valueType === 'status'"
                  :class="
                    cell.class +
                    ' ' +
                    (cell.active === item[cell.displayValue || cell.value]
                      ? cell.activeClass
                      : cell.notActive === item[cell.displayValue || cell.value]
                      ? cell.notActiveClass
                      : '')
                  "
                  :style="cell.style"
                >
                  <template v-if="cell.statusIcons">
                    <v-icon
                      class="success--text"
                      v-if="
                        item[cell.displayValue || cell.value] === cell.active
                      "
                      >mdi-check</v-icon
                    >
                    <v-icon
                      class="error--text"
                      v-if="
                        item[cell.displayValue || cell.value] === cell.notActive
                      "
                      >mdi-close</v-icon
                    >
                  </template>
                  <template v-else>
                    <template
                      v-if="
                        item[cell.displayValue || cell.value] == cell.active
                      "
                    >
                      {{ cell.activeText }}
                    </template>
                    <template
                      v-else-if="
                        item[cell.displayValue || cell.value] == cell.notActive
                      "
                    >
                      {{ cell.notActiveText }}
                    </template>
                    <template v-else>
                      {{ getValue(item, cell) }}
                    </template>
                  </template>
                </td>
                <td
                  :key="'td_' + idx"
                  v-if="cell.valueType === 'checkbox'"
                  :class="cell.class"
                  :style="cell.style"
                >
                  <v-checkbox
                    v-model="item[cell.displayValue || cell.value]"
                    color="primary"
                    :value="cell.checked"
                    :false-value="cell.unchecked"
                    hide-details
                    class="mt-0"
                    @change="changeCheckboxValue(item)"
                  />
                </td>
                <td
                  :key="'td_' + idx"
                  v-if="
                    cell.valueType === 'button' &&
                    (Object.prototype.hasOwnProperty.call(cell, 'show')
                      ? cell.show
                      : true)
                  "
                  :class="cell.class"
                  :style="cell.style"
                  class="d-print-none"
                >
                  <template v-for="(button, bIdx) in cell.buttons">
                    <v-tooltip top :key="'td_button_' + bIdx" v-if="button.tooltip">
                      <template v-slot:activator="{ on, attrs }">
                        <v-btn
                          v-if="checkButton(button, item)"
                          :color="button.color"
                          :block="button.block"
                          :title="button.title"
                          :icon="button.onlyIcon ? true : false"
                          @click="doAction(button.click, item)"
                          x-small
                          :class="`v-size--${button.size} ` + button.class"
                          v-bind="attrs"
                          v-on="on"
                        >
                          <v-icon
                            :class="button.label ? 'mr-2' : 'mr-0'"
                            v-if="button.icon && button.icon.length ? true : false"
                            >{{ button.icon }}</v-icon
                          >
                          {{ button.label }}
                        </v-btn>
                      </template>
                      <span>{{ button.tooltip }}</span>
                    </v-tooltip>

                    <v-btn
                      v-if="!button.tooltip && checkButton(button, item)"
                      :key="'td_button_' + bIdx"
                      :block="button.block"
                      :title="button.title"
                      @click="doAction(button.click, item)"
                      x-small
                      :class="`v-size--${button.size} ` + button.class"
                    >
                        <v-icon
                          :class="button.label ? 'mr-2' : 'mr-0'"
                          v-if="button.icon && button.icon.length ? true : false"
                        >
                          {{ button.icon }}
                        </v-icon>
                      {{ button.label }}
                    </v-btn>
                    
                  </template>
                </td>
              </template>
            </tr>
          </template>
          <template v-else>
            <tr>
              <td :colspan="headers.length" class="text-center px-8">
                <v-row>
                  <v-col cols="12" class="pa-7">
                    <h4
                      class="display-1 font-weight-medium blue-grey--text text--darken-2"
                    >
                      Няма намерени резултати
                    </h4>
                  </v-col>
                </v-row>
              </td>
            </tr>
          </template>
        </tbody>
      </template>

      <template v-slot:footer>
        <template v-if="printable || exportable">
          <v-row class="d-print-none">
            <v-col cols="12" md="12" class="text-right px-5 py-3 pt-7">
              <v-spacer></v-spacer>
              <v-text-field
                v-if="showTotal"
                style="display: -webkit-inline-box; width: 90px; float: left"
                v-model="totalResultData"
                readonly
                label="Обща сума"
              >
              </v-text-field>
              <v-btn
                small
                :class="`btn btn-${color} ${color}`"
                @click="doExport"
                v-if="exportable && data.length"
              >
                <v-icon class="mr-2">mdi-file-excel</v-icon>
                Експортирай
              </v-btn>
              <v-btn
                small
                :class="`btn btn-${color} ${color}`"
                @click="doPrint"
                v-if="printable && data.length"
              >
                <v-icon class="mr-2">mdi-printer</v-icon>
                Разпечатай всички
              </v-btn>
            </v-col>
          </v-row>
        </template>
        <template v-if="showTotal && !printable && !exportable">
          <v-row>
            <v-col cols="1" class="text-right px-5 py-3 pt-7">
              <v-text-field
                v-model="totalResultData"
                readonly
                label="Обща сума"
              >
              </v-text-field>
            </v-col>
          </v-row>
        </template>
      </template>
    </v-data-table>
    <v-pagination
      v-if="hasPaging"
      v-model="pagination.page"
      :color="color"
      :length="Math.ceil(pagination.total / pagination.itemsPerPage)"
      :total-visible="10"
      @input="changedPagination"
      class="d-print-none"
    ></v-pagination>
  </div>
</template>

<script>
import moment from "moment";
import {
  formatDateTime,
  formatDateTimeSeconds,
  getArrayField,
  ISODateString,
  roundNumber,
} from "@/utils";
export default {
  name: "DataTable",
  props: {
    id: {
      type: String,
      default: "",
    },
    headers: {
      type: Array,
      default: () => [],
    },
    pagination: {
      type: Object,
      default: () => {
        return {
          itemsPerPage: 10,
          page: 1,
          perPageOptions: [5, 10, 25, 50, 100],
          total: 0,
        };
      },
    },
    hasPaging: {
      type: Boolean,
      default: true,
    },
    data: {
      type: Array,
      default: () => [],
    },
    printable: {
      type: Boolean,
      default: false,
    },
    totalResultData: {
      type: Number,
      default: () => 0,
    },
    showTotal: {
      type: Boolean,
      default: false,
    },
    exportable: {
      type: Boolean,
      default: false,
    },
    color: {
      type: String,
      default: "primary",
    },
    sortBy: {
      type: String,
      default: () => "",
    },
    sortDesc: {
      type: Boolean,
      default: () => true,
    },
    rowClass: {
      type: Function,
      default: () => null,
    },
  },
  data() {
    return {
      sortColumn: [],
      sortDescending: [],
    };
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
    customSortColumn: {
      get: function () {
        return this.sortColumn;
      },
      set: function (value) {
        this.sortColumn = value;
        this.$emit("update:sortBy", value);
        this.$emit("getData", this.id);
        this.$forceUpdate();
      },
    },
    customSortDesc: {
      get: function () {
        return this.sortDesc;
      },
      set: function (value) {
        this.sortDescending = value;
        this.$emit("update:sortDesc", value);
        this.$emit("getData", this.id);
        this.$forceUpdate();
      },
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
  created() {
    this.sortColumn = this.sortBy;
    this.sortDescending = this.sortDesc;
  },
  methods: {
    getData() {
      this.$emit("getData", this.id);
    },
    changeRowsPerPage() {
      if (
        this.pagination.total > 0 &&
        this.data &&
        this.pagination.page >=
          Math.ceil(this.pagination.total / this.pagination.itemsPerPage)
      ) {
        this.pagination.page = Math.ceil(
          this.pagination.total / this.pagination.itemsPerPage
        );
      }
      this.getData();
    },
    checkButton(button, item) {
      let ifValue = true;

      if (button.if) {
        if (button.ifValueObjectExists) {
          let props = button.ifValueObjectExists.split(".");
          if (item[props[0]]) {
            let value = props.reduce((a, v) => a[v], item);

            if (!value) ifValue = false;
          }
        }
        return (
          (button.if
            ? button.if.type
              ? button.if.type === "equal"
                ? item[button.if.item] === button.if.value
                : false
              : item[button.if.item] !== button.if.value
            : true) && ifValue
        );
      } else if (Object.prototype.hasOwnProperty.call(button, "show")) {
        return button["show"];
      }

      return true;
    },
    getValue(item, options) {
      let template = "";
      let keys = [];
      let props = [];
      let prop = options.displayValue || options.value;
      let result;
      let itemData;

      let key;
      let firstKeys;
      let secondKeys;
      switch (options.valueType) {
        case "text":
          return item[prop] || item[prop] === 0 ? item[prop] : " — ";
        case "textBalance":
          // result = (item[options.value] + '').split('.')[1];
          // if(result && result.length <= 1 || (item[options.value] + '').length === 1){
          return roundNumber(item[prop || options.value], 2);
        // }
        // return item[options.value];
        case "price":
          return roundNumber(item[prop || options.value], 2) + " лв.";
        case "priceAccounting":
          var num = item[prop || options.value];
          if (typeof num === "number") num = num + "";

          var value = Number(num);

          var res = num.split(".");
          if (res.length == 1 || res[1].length < 3)
            value = roundNumber(value, 2);

          return value;
        case "array":
          // eslint-disable-next-line no-case-declarations
          let val = getArrayField(item, prop, options.comma);
          if (val && val !== "null") {
            return val;
          }
          return "";
        //return val != null ? val : '';
        case "date":
          if (item[prop]) {
            // eslint-disable-next-line no-case-declarations
            let d = moment();

            if (
              (typeof item[prop] === "number" &&
                item[prop].toString().length === 8) ||
              (typeof item[prop] === "string" && item[prop].length === 8)
            ) {
              d = moment(item[prop], "DDMMYYYY");
              if (!d.isValid()) d = moment(item[prop], "YYYYMMDD");
            } else if (
              (typeof item[prop] === "number" &&
                item[prop].toString().length === 14) ||
              (typeof item[prop] === "string" && item[prop].length === 14)
            ) {
              d = moment(item[prop], "YYYYMMDDHHmmss");
            } else {
              d = moment(item[prop]);
            }
            if (d.isValid()) {
              return ISODateString(d.toISOString());
            }
          }

          return "—";
        case "hour":
          if (item[prop]) {
            // eslint-disable-next-line no-case-declarations
            let d = moment();

            if (
              (typeof item[prop] === "number" &&
                item[prop].toString().length === 8) ||
              (typeof item[prop] === "string" && item[prop].length === 8)
            ) {
              d = moment(item[prop], "DDMMYYYY");
              if (!d.isValid()) d = moment(item[prop], "YYYYMMDD");
            } else if (
              (typeof item[prop] === "number" &&
                item[prop].toString().length === 14) ||
              (typeof item[prop] === "string" && item[prop].length === 14)
            ) {
              d = moment(item[prop], "YYYYMMDDHHmmss");
            } else {
              d = moment(item[prop]);
            }
            if (d.isValid()) {
              return d.format("HH:mm");
            }
          }

          return "—";
        case "dateTime":
          if (item[prop]) {
            // eslint-disable-next-line no-case-declarations
            let d = moment(item[prop]);
            if (d.isValid()) {
              return formatDateTime(item[prop]);
            }
          }

          return "—";
        case "dateTimeSeconds":
          if (item[prop]) {
            // eslint-disable-next-line no-case-declarations
            let d = moment(item[prop]);
            if (d.isValid()) {
              return formatDateTimeSeconds(item[prop]);
            }
          }

          return "—";
        case "firstElement":
          props = prop.split(".");
          return item[props[0]] ? item[props[0]][0][props[1]] : "";
        case "firstElementObject":
          result = "";
          keys = prop.split(".");
          var firstChildren = item[keys[0]][0];
          keys.shift();
          if (firstChildren) {
            if (firstChildren[keys[0]]) {
              result = keys.reduce((a, v) => a[v], firstChildren);
            }
          }
          return result ? result : "";
        case "object":
          keys = prop.split(".");
          // eslint-disable-next-line no-case-declarations
          result = "";
          try {
            if (item[keys[0]]) {
              result = keys.reduce((a, v) => a[v], item);
            }
            return result ? result : " — ";
          } catch (e) {
            return " — ";
          }
        case "objectDate":
          keys = prop.split(".");

          // eslint-disable-next-line no-case-declarations
          let date = keys.reduce((a, v) => a[v], item);

          if (date) {
            // eslint-disable-next-line no-case-declarations
            let d;
            if (typeof date === "number" && date.toString().length === 8) {
              d = moment(date, "YYYYMMDD");
            } else {
              d = moment(date);
            }

            if (d.isValid()) {
              return ISODateString(d.toISOString());
            }
          }

          return " — ";
        case "objectPrice":
          keys = prop.split(".");
          // eslint-disable-next-line no-case-declarations
          result = "";
          try {
            if (item[keys[0]]) {
              result = keys.reduce((a, v) => a[v], item);
            }
            return result ? roundNumber(result, 2) : " — ";
          } catch (e) {
            return " — ";
          }
        case "twoObjectsPrice":
          result = "";
          try {
            for (let obj of prop) {
              keys = obj.split(".");
              if (item[keys[0]]) {
                if (result.length) {
                  let val = keys.reduce((a, v) => a[v], item);
                  result += val ? " " + val : "";
                } else {
                  result += roundNumber(
                    keys.reduce((a, v) => a[v], item),
                    2
                  );
                }
              }
            }

            return result ? result : " — ";
          } catch (e) {
            return " — ";
          }
        case "moreThanOneField":
          result = "";
          try {
            for (let obj of prop) {
              keys = obj.split(".");
              if (item[keys[0]]) {
                if (result.length) {
                  let val = keys.reduce((a, v) => a[v], item);
                  result += val ? " " + val : "";
                } else {
                  result += keys.reduce((a, v) => a[v], item);
                }
              }
            }

            return result ? result : " — ";
          } catch (e) {
            return " — ";
          }
        case "correspondentNameIsLegalEntity":
          if (item.isLegalEntity) {
            return item.entityName;
          } else {
            return (
              (item.firstName ? item.firstName : "") +
              (item.middleName ? " " + item.middleName : "") +
              (item.lastName ? " " + item.lastName : "")
            )
          }

        case "firstElementMoreThanOneField":
          result = "";
          try {
            let funcReduce = function (obj, key) {
              if (obj[key]) {
                if (obj[key] instanceof Array && obj[key].length) {
                  return obj[key][0];
                } else {
                  return obj[key];
                }
              }
              return "";
            };

            for (let obj of prop) {
              keys = obj.split(".");
              if (item[keys[0]]) {
                if (result.length) {
                  let val = keys.reduce(funcReduce, item);
                  result += val ? " " + val : "";
                } else {
                  result += keys.reduce(funcReduce, item);
                }
              }
            }

            return result ? result : " — ";
          } catch (e) {
            return " — ";
          }
        case "valueOrValue":
          keys = prop.split(".");

          try {
            if (item[keys[0]]) {
              return item[keys[0]];
            }

            return item[keys[1]] || " — ";
          } catch (e) {
            return " — ";
          }
        case "valueOrValueObject":
          keys = prop[0].split(".");
          try {
            result = "";
            if (item[keys[0]]) {
              result = keys.reduce((a, v) => a[v], item);
            }

            if (result) {
              return result;
            }

            // checking next value
            keys = prop[1].split(".");

            result = "";
            if (item[keys[0]]) {
              result = keys.reduce((a, v) => a[v], item);
            }

            return result ? result : " — ";
          } catch (e) {
            return " — ";
          }
        case "valueOrValueArrayFirstElementObject":
          key = prop[0].split("|")[0];
          firstKeys = keys = prop[0].split("|")[1].split("+")[0];
          secondKeys = keys = prop[0].split("|")[1].split("+")[1];
          try {
            result = "";
            if (item[key]) {
              keys = firstKeys.split(".");
              if (item[keys[0]]) {
                result = keys.reduce((a, v) => a[v], item);
              }

              if (result) {
                return result;
              }

              // checking next value
              keys = prop[1].split(".");

              result = "";
              if (item[keys[0]]) {
                result = keys.reduce((a, v) => a[v], item);
              }
            } else {
              keys = secondKeys.split(".");

              if (item[keys[0]]) {
                result = keys.reduce((a, v) => a[v], item);
              }

              if (result) {
                return result;
              }

              // checking next value
              keys = prop[1].split(".");

              result = "";
              if (item[keys[0]]) {
                result = keys.reduce((a, v) => a[v], item);
              }
            }

            return result ? result : " — ";
          } catch (e) {
            return " — ";
          }
        case "valueOrValueArray":
          result = "";
          for (let i = 0; i < prop.length; i++) {
            if (prop[i].includes(".")) {
              keys = prop[i].split(".");
            } else {
              keys = [prop[i]];
            }

            for (let k = 0; k < keys.length; k++) {
              if (item[keys[k]]) {
                if (result.length) result += options.separator || " ";
                result += item[keys[k]];
              }
            }
          }
          return result;
        case "addressObj":
          result = "";

          result = item[prop]["settlementName"] || "";

          if (result.length) {
            result += item[prop]["streetName"]
              ? `, ${item[prop]["streetName"]} ${item[prop]["streetNumber"]}`
              : "";
          } else {
            result += item[prop]["streetName"]
              ? `${item[prop]["streetName"]} ${item[prop]["streetNumber"]}`
              : "";
          }

          if (!result.length) result = item[prop]["additional"];

          return result;
        case "button":
          for (let i = 0; i < options.buttons.length; i++) {
            template += ``;
          }
          return template;
        case "sort":
          itemData = Object.assign([], item[options["sortBy"]]);

          if (options["sortType"] === "date") {
            itemData.sort(function (x, y) {
              if (options["sortDesc"]) {
                return (
                  new Date(y[options["sortValue"]]).getTime() -
                  new Date(x[options["sortValue"]]).getTime()
                );
              } else {
                return (
                  new Date(x[options["sortValue"]]).getTime() -
                  new Date(y[options["sortValue"]]).getTime()
                );
              }
            });
          }

          if (prop.includes(".")) {
            keys = prop.split(".");
            result = "";
            if (itemData[0][keys[0]]) {
              result = keys.reduce((a, v) => a[v], itemData[0]);
            }
            return result ? result : " — ";
          }

          return itemData[0][prop];
        case "status":
          if (item[prop] != null) {
            return item[prop].toString() || " - ";
          }
          return " - ";
        case "translateValue":
          result = " - ";
          for (let obj of options.translateValues) {
            if (obj.value === item[prop]) {
              result = obj.label;
            }
          }
          return result;
      }
    },
    getBalanceCondition(item, options) {
      let prop = options.displayValue || options.value;
      if (item[prop] >= 0) {
        return true;
      }

      return false;
    },
    doAction(action, data) {
      this.$emit("doAction", { action: action, item: data });
    },
    doPrint() {
      this.$emit("print");
    },
    doExport() {
      this.$emit("export");
    },
    changedPagination() {
      this.getData();
    },
    changeCheckboxValue(item) {
      this.$emit("changeCheckbox", item);
    },
  },
};
</script>
