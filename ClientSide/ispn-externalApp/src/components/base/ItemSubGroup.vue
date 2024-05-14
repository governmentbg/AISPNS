<template>
  <base-item-group
    v-if="!item.hidden"
    :item="item"
    text
    sub-group
  />
</template>

<script>
  export default {
    name: 'ItemSubGroup',

    props: {
      item: {
        type: Object,
        default: () => ({
          avatar: undefined,
          group: undefined,
          title: undefined,
          children: [],
        }),
      },
    },
  }
</script>
