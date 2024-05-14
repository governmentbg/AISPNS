<template>
  <v-row>
    <v-col cols="12">
      <v-card
        id="breadcrumbs"
        dense
        color="white"
      >
        <v-breadcrumbs :items="breadcrumbs">
          <template v-slot:item="{ item }">
            <v-breadcrumbs-item
              :to="item.path"
              :disabled="false"
              exact
            >
              {{ item.name.toUpperCase() }}
            </v-breadcrumbs-item>
          </template>
        </v-breadcrumbs>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>

export default {
    name: 'Breadcrumbs',

    components: {
    },

    props: {
      breadcrumbs: {
        type: Array,
        default: () => {
          return [];
        }
      },
      dense: {
        type: Boolean,
        default: false
      }
    },
  }
</script>

<style lang="sass">
#breadcrumbs
  background-color: #ffffff00 !important
  border: 0
  box-shadow: none
  margin-bottom: 10px

  ul
    padding: 5px 0px

    li
      a
        font-size: 1rem
</style> 