<template>
  <v-menu
    :close-on-content-click="true"
    :offset-y='isOffsetY'
    :open-on-hover='isOpenOnHover'
    :transition='transition'
    :value="openMenu"
    v-model="menuOpened"
    content-class="main-menu-container"
  >
    <template
      v-slot:activator="{ on }"
    >
      <v-btn
        :color='color'
        v-if='menu.icon'
        v-on="on"
        :href="menu.href"
        :rel="menu.href && menu.href !== '#' ? 'noopener' : undefined"
        :target="menu.href && menu.href !== '#' ? '_blank' : undefined"
        :to="menu.path"
        class="multi-level-menu-button"
      >
        <v-icon>
          {{ menu.icon }}
        </v-icon>
      </v-btn>
      <v-list-item
        class='d-flex justify-space-between'
        v-else-if='isSubMenu'
        v-on="on"
      >
        {{ menu.name }}
        <div class="flex-grow-1">
        </div>
        <v-icon>
          mdi-chevron-right
        </v-icon>
      </v-list-item>

      <v-btn
        :color='color'
        @click="openMenu=true"
        text
        v-else-if='menu.children && menu.children.length'
        v-on="on"
        class="multi-level-menu-button"
        :href="menu.href"
        :rel="menu.href && menu.href !== '#' ? 'noopener' : undefined"
        :target="menu.href && menu.href !== '#' ? '_blank' : undefined"
        :to="menu.path"
      >
        {{ menu.name }}
        <v-icon class="ml-1">
          mdi-chevron-down
        </v-icon>
      </v-btn>
      <v-btn
        :color='color'
        text
        exact-active-class="lqlq"
        v-else
        :href="menu.href"
        :rel="menu.href && menu.href !== '#' ? 'noopener' : undefined"
        :target="menu.href && menu.href !== '#' ? '_blank' : undefined"
        :to="menu.path"
        class="multi-level-menu-button"
      >
        {{ menu.name }}
      </v-btn>
    </template>
    <v-list>
      <template
        v-for="(item, index) in menu.children"
      >
        <v-divider
          :key='index'
          v-if='item.isDivider'
        />
        <base-main-menu-item
          :is-offset-x="true"
          :is-offset-y="false"
          :is-open-on-hover="false"
          :is-sub-menu="true"
          :key="index"
          :menu="item"
          :name="item.name"
          @sub-menu-click="emitClickEvent"
          v-else-if="item.children && item.children.length"
        />
        <v-list-item
          :key="index"
          @click="emitClickEvent(item)"
          :to="item.path"
          v-else
        >
          <v-list-item-action v-if="item.icon">
            <v-icon>mdi-{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-title>
            {{ item.name }}
          </v-list-item-title>
        </v-list-item>
      </template>
    </v-list>
  </v-menu>
</template>


<script>
  import Themeable from 'vuetify/lib/mixins/themeable'

  export default {
    name: 'MainMenuItem',

    mixins: [Themeable],

    props: {
      name: String,
      icon: String,
      color: { type: String, default: "white" },
      isOffsetY: { type: Boolean, default: true },
      //isOpenOnHover: { type: Boolean, default: false },
      isSubMenu: { type: Boolean, default: false },
      transition: { type: String, default: "scale-transition" },
      menu: {
        type: Object,
        default: () => ({
          name: undefined,
          link: undefined,
          children: []
        }),
      }
    },
    data: () => ({
      openMenu: false,
      isOpenOnHover: false,
      menuOpened: false,
    }),
    watch: {
      menuOpened: function () {
        //this.isOpenOnHover = !this.menuOpened;
      },
      openMenu: function () {
      }
    },
    methods: {
      emitClickEvent(item) {
        this.$emit("sub-menu-click", item);
        this.openMenu = false;
        this.menuOpened = false;
      },
      getColumnMenus(menus){
        let columns = 4;

        return [...Array(columns).keys()].map(c => menus.filter((_, i) => i % columns === c));
      }
    },
    computed: {
      href() {
        return this.item.href || (!this.item.to ? '#' : undefined)
      },
    }
  }
</script>


<style lang="sass" scoped>
.multi-level-menu-button.v-btn
  margin-bottom: 0px !important

.menu-item
  .v-btn 
    margin-bottom: 0px !important

.v-menu__content
  .v-list-item
    .v-btn
      padding-left: 5px !important
      padding-right: 5px !important
      padding-top: 5px !important
      padding-bottom: 5px !important
      min-width: 50px
      text-align: left
      justify-content: left
      font-weight: 500
      

.together
  min-width: 0 !important
  font-size: 0.9rem !important

.main-menu-container
  .v-btn
    display: inline-block
    //height: 30px
    height: auto
    .v-btn__content
      white-space: initial
      strong 
        white-space: initial
        font-size: 0.9rem !important
</style>