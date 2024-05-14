/* eslint-disable vue/valid-template-root */
<template>
  <div>
    <template v-if="item.children && item.children.length">
      <template v-for="(menu, idx) in item.children" >
        <v-list-item
          v-if="!menu.hidden"
          :key="`path-${idx}`"
          :href="menu.path"
          :rel="menu.path && menu.path !== '#' ? 'noopener' : undefined"
          :to="menu.path"
          :active-class="`secondary ${!isDark ? 'black' : 'white'}--text`"
        >
          <v-list-item-icon v-if="menu.meta && menu.meta.icon">
            <v-icon v-if="!menu.meta.icon.includes('svg') && !menu.meta.icon.includes('png')">{{menu.meta.icon}}</v-icon>
            <v-img v-else width="24" :src="require(`@/assets/${menu.meta.icon}`)" class="mt-2" :style="(menu.meta.icon.includes('svg') ? 'height: 9px' : '')" />
          </v-list-item-icon>

          <v-list-item-content v-if="menu.name || menu.subtitle">
            <v-list-item-title v-text="menu.name" class="pt-1 text-uppercase" />

            <v-list-item-subtitle v-text="menu.subtitle" />
          </v-list-item-content>
        </v-list-item>
      </template>
    </template>
    <template v-else>
      <v-list-item
        v-if="!item.hidden"
        :href="href"
        :rel="href && href !== '#' ? 'noopener' : undefined"
        :target="href && href !== '#' ? '_blank' : undefined"
        :to="item.path"
        :active-class="`secondary ${!isDark ? 'black' : 'white'}--text`"
      >
        <v-list-item-icon
          v-if="(item.meta && !item.meta.icon)"
          class="v-list-item__icon--text"
        />

        <v-list-item-icon v-if="item.meta && item.meta.icon">
          <v-icon v-if="!item.meta.icon.includes('svg') && !item.meta.icon.includes('png')" v-text="item.meta.icon" />
          <v-img v-else width="24" :src="require(`@/assets/${item.meta.icon}`)" class="mt-2" />
        </v-list-item-icon>

        <v-list-item-content v-if="item.name || item.subtitle">
          <v-list-item-title v-text="item.name" class="pt-1 text-uppercase" />

          <v-list-item-subtitle v-text="item.subtitle" />
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
