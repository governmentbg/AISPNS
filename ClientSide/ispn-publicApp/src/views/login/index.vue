<template>
  <v-container
    id="sign-in"
    tag="section"
    class="full-height"
  >
    <v-slide-x-transition group leave-absolute hide-on-leave>
      <v-row class="py-5" key="entrance">
        <v-col cols="12" md="4" offset-md="1" class="pa-0 mx-auto">
          <v-card class="v-card--sign-in pa-2" outlined hover>
            <v-card-title class="text-center d-block text-uppercase">{{$t('sign_in')}}</v-card-title>
            <v-divider class="my-2" />
            <v-card-text>
              <v-row>
                <v-col cols="12" class="text-center my-7">
                  <v-avatar
                    size="95"
                  >
                    <v-icon
                      color="primary"
                      size="55"
                    >
                      mdi-account-tie
                    </v-icon>
                  </v-avatar>
                </v-col>
                <v-col cols="12" class="text-center">
                  {{ $t('sign_in_syndics')}} <br />
                  {{ $t('sign_in_info_message') }}
                </v-col>
                <v-col cols="12">
                  <v-btn
                    block
                    :color="!$vuetify.theme.isDark ? 'primary' : ''"
                    class="mt-5"
                    @click="signIn"
                  >
                    {{ $t('sign_in') }}
                  </v-btn>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-slide-x-transition>
  </v-container>
</template>

<script>
export default {
  name: "signInPage",
  components: {
  },
  data(){
    return {
      eAuthentication: {
        data: ''
      },
    }
  },
  computed: {
  },
  watch:{
  },
  mounted(){
        
  },
  methods: {
    signIn(){

    },
  }
}
</script>


<style lang="sass">
  .v-card--sign-in
    .v-avatar
      border-radius: 50%
      border: 1px solid #E5E5E5
</style>