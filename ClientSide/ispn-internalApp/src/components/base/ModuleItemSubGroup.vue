<template>
  <base-module-item-group
    v-if="!item.hidden"
    :item="item"
    :text="text"
    sub-group
  />
</template>

<script>
  export default {
    name: 'ModuleItemSubGroup',

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
      text: {
        type: Boolean,
        default: false
      }
    },
  }
</script>
