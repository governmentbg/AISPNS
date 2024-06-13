/* eslint-disable vue/valid-template-root */
<template>
  <div :class="defaultMenuColors ? '' : 'appBarSubMenu'">
    <template v-if="item.children">
      <template v-for="(menu, idx) in item.children" >
        <template v-if="!menu.hidden">
          <base-item-group
            v-if="menu.children"
            :key="`group-${idx}`"
            :item="menu"
          >
          </base-item-group>
          <template v-else>
            <v-list-item
              :key="`path-${idx}`"
              :href="menu.path"
              :rel="menu.path && menu.path !== '#' ? 'noopener' : undefined"
              :to="menu.path"
              :active-class="defaultMenuColors ? `primary ${!isDark ? 'black' : 'white'}--text` : 'primary white--text'"
            >
              <v-list-item-icon v-if="menu.meta && menu.meta.icon">
                <v-icon v-if="!menu.meta.icon.includes('svg') && !menu.meta.icon.includes('png')">{{menu.meta.icon}}</v-icon>
                <v-img v-else width="24" :src="require(`@/assets/${menu.meta.icon}`)" class="mt-2" :style="(menu.meta.icon.includes('svg') ? 'height: 9px' : '')" />
              </v-list-item-icon>

              <v-list-item-content v-if="menu.name || menu.subtitle">
                <v-list-item-title>
                  {{menu.name}}
                </v-list-item-title>

                <v-list-item-subtitle>
                  {{menu.subtitle}}
                </v-list-item-subtitle>
              </v-list-item-content>

              <v-list-item-action class="ma-0" v-if="shownBadgesPaths.includes(menu.path)">
                <v-list-item-icon class="ma-0">
                  <v-badge
                    :color="badges[menu.badgeItem] ? 'red' : 'green'"
                    inline
                    :content="badges[menu.badgeItem] ? badges[menu.badgeItem] : '0'"
                  >
                  </v-badge>
                </v-list-item-icon>
              </v-list-item-action>
            </v-list-item>
          </template>
        </template>
      </template>
    </template>
    <template v-else>
      <v-list-item
        v-if="!item.hidden"
        :href="href"
        :rel="href && href !== '#' ? 'noopener' : undefined"
        :target="href && href !== '#' ? '_blank' : undefined"
        :to="item.path"
        :active-class="defaultMenuColors ? `primary ${!isDark ? 'black' : 'white'}--text` : 'primary white--text'"
      >
        <v-list-item-icon
          v-if="(!item.meta || !item.meta.icon) && text"
          class="v-list-item__icon--text"
        >
          {{computedText}}
        </v-list-item-icon>

        <v-list-item-icon v-if="item.meta && item.meta.icon">
          <v-icon v-if="!item.meta.icon.includes('svg') && !item.meta.icon.includes('png')">
            {{item.meta.icon}}
          </v-icon>
          <v-img v-else width="24" :src="require(`@/assets/${item.meta.icon}`)" class="mt-2" />
        </v-list-item-icon>

        <v-list-item-content v-if="item.name || item.subtitle">
          <v-list-item-title class="pt-1">
            {{item.name}}
          </v-list-item-title>

          <v-list-item-subtitle>
            {{item.subtitle}}
          </v-list-item-subtitle>

          <v-list-item-action class="ma-0" v-if="shownBadgesPaths.includes(item.path)">
            <v-list-item-icon class="ma-0">
              <v-badge
                :color="badges[item.badgeItem] ? 'red' : 'green'"
                inline
                :content="badges[menu.badgeItem] ? badges[menu.badgeItem] : '0'"
              >
              </v-badge>
            </v-list-item-icon>
          </v-list-item-action>
        </v-list-item-content>
      </v-list-item>
    </template>
  </div>
</template>

<script>
  import Themeable from 'vuetify/lib/mixins/themeable'

  export default {
    name: 'Item',

    mixins: [Themeable],

    props: {
      item: {
        type: Object,
        default: () => ({
          href: undefined,
          icon: undefined,
          subtitle: undefined,
          name: undefined,
          to: undefined,
        }),
      },
      text: {
        type: Boolean,
        default: false,
      },
      defaultMenuColors: {
        type: Boolean,
        default: true
      },
      shownBadgesPaths: {
        type: Array,
        default: () => [],
      },
      badges: {
        type: Object,
        default: () => ({}),
      }
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
