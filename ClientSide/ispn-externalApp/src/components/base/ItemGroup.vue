<template>
  <v-list-group
    :group="group"
    :sub-group="subGroup"
    :value="hasActiveChildren"
    append-icon="mdi-menu-down"
    :color="defaultMenuColors ? barColor !== 'rgba(255, 255, 255, 1), rgba(255, 255, 255, 0.7)' ? 'white' : 'grey darken-1' : 'grey darken-1'"
    @click.stop
    :class="defaultMenuColors ? '' : 'appBarMenu'"
  >
    <template v-slot:activator>
      <v-list-item-icon v-if="item.meta && item.meta.icon">
        <v-icon>{{item.meta.icon}}</v-icon>
      </v-list-item-icon>

      <v-list-item-content>
        <v-list-item-title>
          {{item.name}}
        </v-list-item-title>
      </v-list-item-content>
    </template>

    <template v-for="(child, i) in children">
      <base-item-sub-group
        v-if="child.children"
        :key="`sub-group-${i}`"
        :item="child"
      />

      <base-item
        v-else
        :key="`item-${i}`"
        :item="child"
        :text="text"
        :default-menu-colors="defaultMenuColors"
      />
    </template>
  </v-list-group>
</template>

<script>
  // Utilities
  import kebabCase from 'lodash/kebabCase'
  import { mapState } from 'vuex'

  export default {
    name: 'ItemGroup',

    inheritAttrs: false,

    props: {
      item: {
        type: Object,
        default: () => ({
          avatar: undefined,
          group: undefined,
          name: undefined,
          children: [],
        }),
      },
      subGroup: {
        type: Boolean,
        default: false,
      },
      text: {
        type: Boolean,
        default: false,
      },
      defaultMenuColors: {
        type: Boolean,
        default: true
      }
    },

    computed: {
      ...mapState(['barColor']),
      children () {
        return this.item.children.map(item => ({
          ...item,
          to: !item.to ? undefined : `${this.item.group}/${item.to}`,
        }))
      },
      hasActiveChildren(){
        let currentRoutePath = this.$router.currentRoute.path;
        let hasActiveChildren = false;
        for(let child of this.item.children){
          if(currentRoutePath.includes(child.path))
            hasActiveChildren = true;
        }

        return hasActiveChildren;
      },
      computedText () {
        if (!this.item || !this.item.name) return ''

        let text = ''

        this.item.name.split(' ').forEach(val => {
          text += val.substring(0, 1)
        })

        return text
      },
      group () {
        return this.genGroup(this.item.children)
      },
    },

    methods: {
      genGroup (children) {
        return children
          .filter(item => item.to)
          .map(item => {
            const parent = item.group || this.item.group
            let group = `${parent}/${kebabCase(item.to)}`

            if (item.children) {
              group = `${group}|${this.genGroup(item.children)}`
            }

            return group
          }).join('|')
      },
    },
  }
</script>

<style>
.v-list-group__activator p {
  margin-bottom: 0;
}

.v-list-group.v-list-group--active {
  background-color: #ffffff1c;
  border-radius: 3px;
}
</style>
