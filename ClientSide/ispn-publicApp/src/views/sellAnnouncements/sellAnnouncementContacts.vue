<template>
  <v-container fluid tag="section" class="full-height">
    <v-row>
      <v-col cols="12">
        <base-material-card
          color="primary"
          icon="mdi-human-greeting-proximity"
          inline
          class="px-5 py-5 mt-15"
        >
          <template v-slot:after-heading>
            <div class="display-2 font-weight-light">{{ $t('contacts') }}</div>
          </template>

          <div v-html="data.rawHtml"></div>
        </base-material-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>

import { apiGetContactForm } from "@/api/announcements"
export default {
  name: "sellAnnouncementsContacts",
  components: {
  },
  data: () => ({
    data: {}
  }),
  computed: {
  },
  mounted() {
    this.getData();
  },
  watch: {
  },
  methods: {
    getData() {
      apiGetContactForm().then((result) => {
        this.data = result;
      });
    },
  },
};
</script>
