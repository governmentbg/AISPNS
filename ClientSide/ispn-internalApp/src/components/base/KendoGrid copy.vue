<template>
  <Grid
    :id="id"
    :columns="columnsData"
    :data-items="shownItems"
    :take="pagination.take"
    :skip="pagination.skip"
    :page-size="pagination.take"
    :pageable="pageable"
    :sortable="sortable"
    :sort="sortedColumns"
    :total="pagination.total"
    :filterable="filterable"
    scrollable="none"
    :resizable="true"
    @pagechange="pageChangeHandler"
    @sortchange="sortChangeHandler"
    :row-render="rowRender"
    :class="styleClass"
  >
    <template v-slot:chips="{ props }">
      <td :class="`k-table-td`">
        <template v-for="(chip, idx) in props.dataItem[props.field.split(',')[0]]">
          <v-chip color="grey lighten-2" small class="mr-2 mb-1">
            <template v-if="props.field.includes('|')">
              {{ chip[props.field.split(',')[1].split("|")[0]] }} {{ props.field.includes("|") ? " - "+chip[props.field.split("|")[1]] : '' }}
            </template>
            <template v-else>
              {{ chip[props.field.split(',')[1]] }}
            </template>
          </v-chip>
        </template>
      </td>
    </template>
    <template v-slot:trueOrFalse="{ props }">
      <td :class="`k-table-td${props.className ? ' '+props.className : ''}`">
        {{ getTrueOrFalse(props.dataItem, props.field) }}
      </td>
    </template>
    <template v-slot:activeStatus="{ props }">
      <td :class="`k-table-td${props.className ? ' '+props.className : ''}`">
        {{ getActiveStatus(props.dataItem, props.field) }}
      </td>
    </template>
    <template v-slot:trueOrFalseReverse="{ props }">
      <td :class="`k-table-td${props.className ? ' '+props.className : ''}`">
        {{ getFalseOrTrue(props.dataItem, props.field) }}
      </td>
    </template>
    <template v-slot:status="{ props }">
      <td :class="`k-table-td${props.className ? ' '+props.className : ''}`">
        <v-icon v-if="props.dataItem[props.field]" color="success">mdi-check</v-icon>
        <v-icon v-else color="error">mdi-close</v-icon>
      </td>
    </template>
    <template v-slot:isDraft="{ props }">
      <td :class="`k-table-td${props.className ? ' '+props.className : ''}`">
        {{ !props.dataItem.isDraft ? 'Вписан' : 'Чернова'}}
      </td>
    </template>
    <template v-slot:amount="{ props }">
      <td :class="`k-table-td text-right${props.className ? ' '+props.className : ''}`">
        {{ roundNumber(props.dataItem[props.field], 2) + ' лв.'}}
      </td>
    </template>
    <template v-slot:isScrapped="{ props }">
      <td :class="`k-table-td${props.className ? ' '+props.className : ''}`">
        {{ getIsScrappedValue(props.dataItem) }}
      </td>
    </template>
    <template v-slot:actions="{props}">
      <td :class="props.className ? props.className + ' text-center' : 'text-center 123'">
        <template v-for="(button, bIdx) in buttons">
          <template v-if="!Object.prototype.hasOwnProperty.call(button, 'if') ? true : checkButtonVisibility(button, props.dataItem)">
            <v-tooltip top :key="'td_button_' + bIdx" v-if="button.tooltip" :color="button.class ? button.class : 'primary'">
              <template v-slot:activator="{ on, attrs }">
                <v-btn
                  :title="button.title"
                  :color="button.color ? button.color : 'primary'"
                  :icon="button.icon ? true : false"
                  outlined
                  :rounded="button.icon && !button.label"
                  small 
                  @click="doAction(button.click, props.dataItem)" 
                  :class="button.class"
                  v-bind="attrs"
                  v-on="on"
                  :disabled="disabled"
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
              <span>{{ button.tooltip }}</span>
            </v-tooltip>
            <v-btn
              v-if="!button.tooltip"
              :key="'td_button_' + bIdx"
              :title="button.title"
              :color="button.color ? button.color : 'primary'"
              :icon="button.icon ? true : false"
              outlined
              :rounded="button.icon && !button.label"
              small 
              @click="doAction(button.click, props.dataItem)" 
              :class="button.class"
              :disabled="button.disabled"
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
        </template>
      </td>
    </template>
    <grid-toolbar class="text-right d-block" v-if="exportable || addNewRow">
      <v-btn
        title="Експортирай"
        color="primary"
        @click='addNewRowAction'
        small
        v-if="addNewRow"
        :disabled="disabled"
      >
        <v-icon class="mr-2">mdi mdi-plus</v-icon>
        {{ addNewRowButtonLabel }}
      </v-btn>

      
      <v-btn
        title="Експортирай"
        color="primary"
        @click='exportTableData'
        small
        v-if="exportable"
        :disabled="disabled"
      >
        <v-icon class="mr-2">mdi mdi-file-excel</v-icon>
        Експортирай
      </v-btn>
    </grid-toolbar>
  </Grid>
</template>

<script>
import { Grid, GridToolbar } from "@progress/kendo-vue-grid";
import { roundNumber } from '@/utils';
const ADJUST_PADDING = 4;
const COLUMN_MIN = 4;
let minGridWidth = 600;
export default {
  name: "Kendo-Grid",
  components: {
    Grid,
    GridToolbar
  },
  props: {
    id: {
      type: String,
      default: "",
    },
    columns: {
      type: Array,
      default: () => [],
    },
    pagination: {
      type: Object,
      default: () => {
        return {
          take: 10,
          skip: 0,
          page: 1,
          perPageOptions: [5, 10, 25, 50, 100],
          total: 0,
        };
      },
    },
    items: {
      type: Array,
      default: () => [],
    },
    hasPaging: {
      type: Boolean,
      default: true,
    },
    exportable: {
      type: Boolean,
      default: false,
    },
    addNewRow: {
      type: Boolean,
      default: false
    },
    addNewRowButtonLabel: {
      type: String,
      default: "Добави"
    },
    styleClass: {
      type: String,
      default: null
    },
    serverSideProcessing: {
      type: Boolean,
      default: true
    },
    filterable: {
      type: Boolean,
      default: false
    },
    disabled: {
      type: Boolean,
      default: false
    },
    customRowClass: {
      type: Object,
      default: null
    },
    sortable: {
      type: Boolean,
      default: false
    },
    sort: {
      type: Object,
      default: () => {}
    }
  },
  data() {
    return {
      grid: "",
      setMinWidth: false,
      gridCurrent: 0,
      sortColumn: [],
      sortDescending: [],
      columnsData: [],
    };
  },
  // watch: {},
  computed: {
    shownItems(){
      if(this.serverSideProcessing){
        return this.items;
      } else {
        return this.items.slice(this.pagination.skip, this.pagination.take + this.pagination.skip);
      }
    },
    pageable() {
      if(this.hasPaging)
        return {
          pageSizes: this.pagination.perPageOptions
        }

      return false;
    },
    buttons(){
      let buttons = [];
      for(let column of this.columns){
        if(column.cell === "actions"){
          buttons = column.buttons;
          break;
        }
      }

      return buttons;
    },
    sortedColumns(){
      if(this.sort && Object.keys(this.sort).length)
        return [this.sort]

      return []
    }
  },
  mounted(){
    this.grid = document.querySelector(".k-grid");
    window.addEventListener("resize", this.handleResize);
    this.columns.map(item => {
      return (minGridWidth += item.minWidth);
    });

    this.gridCurrent = this.grid.offsetWidth;
    this.setMinWidth = this.grid.offsetWidth < minGridWidth;
    this.defineColumns();
  },
  created() {
  },
  methods: {
    getData() {
      this.$emit("getData", this.id);
    },
    pageChangeHandler(e){
      this.pagination.take = e.page.take;
      this.pagination.skip = e.page.skip;
      this.getData()
      this.$forceUpdate();
    },
    rowRender(h, trElement , defaultSlots, props ) {
      return h('tr', {
        on: {
          dblclick: () => {
            this.$emit("dblclick", props.dataItem)
          }
        },
        staticClass: trElement.data.class + (this.customRowClass ? (this.customRowClass.value === props.dataItem[this.customRowClass.key] ? ' '+this.customRowClass.styleClass : '') : '')
      }, defaultSlots);
    },
    doAction(action, rowData) {
      this.$emit("click", { action, rowData });
    },
    exportTableData() {
      this.$emit("export")
    },
    addNewRowAction() {
      this.$emit("onNewRow")
    },
    getTrueOrFalse(item, prop){
      if(item[prop])
        return "Да"

      return "Не"
    },
    getActiveStatus(item, prop){
      if(item[prop])
        return "Активен"

      return "Неактивен"
    },
    getFalseOrTrue(item, prop){
      if(item[prop])
        return "Не"

      return "Да"
    },
    getIsScrappedValue(item){
      if(item.isScrapped === true || item.isScrapped === false){
        if(!item.isScrapped) return "В движение"
        if(item.isScrapped) return "Бракуван"
      }

      return " - "
    },
    sortChangeHandler(e){
      if(e.sort.length){
        this.$emit("update:sort", e.sort[0]);
      } else {
        this.$emit("update:sort", {})
      }
    },
    checkButtonVisibility(button, rowData){
      return rowData[button.if.field] !== button.if.value;
    },
    defineColumns(){
      this.$nextTick(() => {
        this.columnsData = this.columns.map((column, index) => {
          let result = {};
          for(let key in column){
            result[key] = column[key]
          }
          result.width = this.setWidth(column);
          return result;
        });
      })
    },
    handleResize() {
      if (this.grid.offsetWidth < minGridWidth && !this.setMinWidth) {
        this.setMinWidth = true;
      } else if (this.grid.offsetWidth > minGridWidth) {
        this.gridCurrent = this.grid.offsetWidth;
        this.setMinWidth = false;
      }
      this.defineColumns();
    },
    setWidth(column) {
      let width = this.setMinWidth
        ? column.minWidth ? column.minWidth : column.width ? column.width : 100
        : column.minWidth ? column.minWidth  : column.width ? column.width : 'auto';

      
      if(typeof width === 'number')
        width < COLUMN_MIN ? width : (width -= ADJUST_PADDING);
      return width;
    },
    roundNumber: roundNumber,
  },
};
</script>
