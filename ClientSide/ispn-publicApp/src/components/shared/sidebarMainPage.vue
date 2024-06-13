<template>
  <div>
    <base-material-card
      id="useful_links"
      icon="mdi-link"
      smIcon
      :title="$t('useful_links')"
    >
      <v-list >
        <v-list-item-group
          v-model="selectedItem"
          color="primary"
        >
          <v-list-item
            v-for="(item, i) in items"
            :key="i"
            :to="item.path"
          >
            <v-list-item-icon>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title>{{ item.text.toUpperCase() }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </base-material-card>
    <template v-if="false">
      <base-poll/>
      <base-mini-card :data="eservices" />
      <base-mini-card :data="signals" />
    </template>
  </div>
</template>

<script>
export default {
  name: "sidebar",
  components: {},
  data(){
    return {
      selectedItem: null,
      items: [
        { text: this.$t('contacts'), path: "/contacts", icon: 'mdi-phone' },
        { text: this.$t('staff'), path: "/staff", icon: 'mdi-account-multiple' },
        { text: this.$t('news'), path: "/news", icon: 'mdi-newspaper' },
        { text: this.$t('events'), path: "/events", icon: 'mdi-bell-check' },
        { text: this.$t('calendar'), path: "/calendar", icon: 'mdi-calendar' },
        { text: this.$t('inspection_reports'), path: "/inspection-reports", icon: 'mdi-text-box-check-outline' },
        { text: this.$t('buyer_profile'), path: "", icon: 'mdi-account-tie' },
        { text: this.$t('bulletin'), path: "", icon: 'mdi-email-newsletter' },
        //{ text: this.$t('verification_financial_interests_disclosure_declarations'), path: "", icon: 'mdi-file-document-outline' },
        //{ text: this.$t('competitions'), path: "", icon: 'mdi-flag' },
      ],
      eservices: {
        best: true,
        background: 'white lighten-5',
        color: 'secondary',
        heading: this.$t('e_services'),
        icon: 'mdi-at',
        title: this.$t('submit_your_declarations_here'),
        to: `/e-services`,
        subTitle: '',
      },
      signals: {
        best: true,
        background: 'white lighten-5',
        color: 'secondary',
        heading: this.$t('signals_and_complaints'),
        icon: 'mdi-email',
        title: this.$t('submit_your_signal_here'),
        to: `/e-signals`,
        subTitle: '',
      }
    }
  },
  computed: {},
  mounted() {},
  methods: {}
}
</script>

<style lang="sass">
#useful_links
  .v-list-item
    min-height: 40px
  .v-list-item__icon
    margin: 12px 0 !important
    margin-right: 14px !important
  
  .v-list-item__content
    padding: 15px 0
    
    .v-list-item__title
      font-weight: 400
</style>
