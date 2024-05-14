<template>
  <div class="text-center multi-level-menu" style="width:100%">
    <template v-if="menu.children && menu.children.length">
      <v-menu
        open-on-hover
        offset-x
      >
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            dark
            text
            v-bind="attrs"
            v-on="on"
            style="width:100%"
            class="multi-level-menu-button"
          >
            {{ menu.name }}

            <v-icon class="pull-right">mdi-menu-right</v-icon>
             
          </v-btn>
        </template>

        <v-list>
          <v-list-item
            v-for="(item, index) in menu.children"
            :key="index"
          >
            <v-list-item-title>
              <v-btn
                text
                plain
                block
                :ripple="false"
              >
                {{ item.name }}
              </v-btn>

            </v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </template>
    <template v-else>
      <v-btn
        color="white"
        dark
        text
        
        v-bind="attrs"
        v-on="on"
      >
        {{ menu.name }}
      </v-btn>
    </template>
  </div>
</template>

<script>
  import Themeable from 'vuetify/lib/mixins/themeable'

  export default {
    name: 'MenuSubItem',

    mixins: [Themeable],

    props: {
      menu: {
        type: Object,
        default: () => ({
          name: undefined,
          link: undefined,
          children: []
        }),
      },
    },

    computed: {
      computedText () {
        if (!this.item || !this.item.name) return ''

        let text = ''

        this.item.name.split(' ').forEach(val => {
          text += val.substring(0, 1)
        })

        return text.toUpperCase();
      },
      href () {
        return this.item.href || (!this.item.to ? '#' : undefined)
      },
    },
  }
</script>


<style lang="sass" scoped>
  .multi-level-menu
    .multi-level-menu-button
      color: #121212

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
</style>