<template>
  <v-container
    id="login"
    class="fill-height justify-center"
    tag="section"
  > 
    <v-row class="my-5">
      <v-col cols="12" md="2" offset-md="5" class="dev_environment" v-if="!isProduction">ТЕСТОВА СРЕДА</v-col>
    </v-row>
    <v-row justify="center">
      <v-slide-y-transition appear>
        <base-material-card
          color="primary"
          light
          max-width="100%"
          width="400"
          class="px-5 py-3"
        >
          <template v-slot:heading>
            <div class="text-center">
              <h1 class="display-2 font-weight-bold mb-0">
                Вход в системата
              </h1>
            </div>
          </template>

          <v-card-text class="text-center">
            <v-form 
              ref="loginForm" 
              v-model="valid" 
              lazy-validation
            >
              <v-text-field
                v-model="data.username"
                ref="username"
                color="secondary"
                label="Потребителско име"
                prepend-icon="mdi-account"
                class="mt-2"
                :rules="[rules.required]"
                @keyup.enter.native="login"
                autocomplete="off"
                placeholder=" "
              />

              <v-text-field
                v-model="data.password"
                ref="password"
                class="mb-8 mt-5"
                :type="showPassword ? 'text' : 'password'"
                color="secondary"
                label="Парола"
                prepend-icon="mdi-lock-outline"
                :rules="[rules.required]"
                @keyup.enter.native="login"
                autocomplete="off"
                placeholder=" "
              />

              <login-button
                color="secondary"
                depressed
                class="white--text"
                @click="login"
              >
                Вход
              </login-button>
            </v-form>
          </v-card-text>
        </base-material-card>
      </v-slide-y-transition>
    </v-row>
  </v-container>
</template>

<script>
import btn from "@/components/utils/Btn.vue";
import { getCookie } from '@/utils/auth';
import { isGuid } from '@/utils';
import { mapGetters } from "vuex";
  export default {
    name: 'PP_ERP_Login',
    components: {
      LoginButton: btn,
    },
    data(){
      return {
        redirect: '',
        notify: '',
        otherQuery: '',
        data: {
          username: getCookie("LastUser"),
          password: ''
        },
        valid: true,
        showPassword: false,
        rules: {
          required: value => !!value || 'Задължително поле.',
        },
      }
    },
    computed: {
      ...mapGetters(['isProduction']),
    },
    mounted() {
      if(this.notify)
        this.$snotify.error(this.notify);
    },
    watch: {
      $route: {
        handler: function(route) {
          const query = route.query
          if (query) {
            this.redirect = query.redirect
            this.notify = query.notify
            this.otherQuery = this.getOtherQuery(query)
          }
        },
        immediate: true
      }
    },
    methods: {
      login(){
        if(this.$refs.loginForm.validate()){
          let vue = this;
          this.$store.dispatch('user/login', this.data).then(() => {
            if(this.redirect){
              if(Object.keys(this.otherQuery).length){
                vue.$router.push({ path:  this.getAvailablePath(this.redirect), query: this.otherQuery })
              } else {
                vue.$router.push({ path: this.getAvailablePath(this.redirect) })
              }
            } else {
              vue.$router.push({ path: this.getAvailablePath() })
            }
          }).catch(() => {
            vue.$snotify.error('Грешно потребителско име или парола');
          })
        }
      },
      getAvailablePath(path = '/'){
        let goTo = null;
        try {
          let splittedPath = path.split('/');
          let lastPathSegment = splittedPath.pop();

          let checkPath = null;
          if(isGuid(lastPathSegment)){
            splittedPath.push(':id');
          } else {
            splittedPath.push(lastPathSegment);
          }
          checkPath = splittedPath.join("/");
          let isRouteExists = this.$router.resolve({path: checkPath});
          if(isRouteExists.resolved.matched.length === 0 || isRouteExists.resolved.matched[0].path === '*'){
            for(let route of this.$router.options.routes){
              if(route.path !== '/' && route.path !== '/login' && route.path != '/*' && route.path != '*'){
                goTo = route;
                break;
              }
            }
          } else {
            let routePath = null;
            for(let route of this.$router.options.routes){
              if(route.path === checkPath){
                routePath = path;
              } else if(route.children) {
                for(let routeChild of route.children){
                  if(routeChild.path === checkPath){
                    routePath = path;
                  }
                }
              }
            }

            if(routePath){
              return routePath;
            } else {
              return isRouteExists.resolved.matched[0].path;
            }
          }
        } catch(e){
          for(let route of this.$router.options.routes){
            if(route.path !== '/' && route.path !== '/login' && route.path != '/*' && route.path != '*'){
              goTo = route;
              break;
            }
          }
        }
        return goTo.path;
      },
      getOtherQuery(query) {
        return Object.keys(query).reduce((acc, cur) => {
          if (cur !== 'redirect' && cur !== 'notify') {
            acc[cur] = query[cur];
          }
          return acc;
        }, {})
      },
    }
  }
</script>

<style lang="sass" scoped>
.dev_environment 
  background-color: #efd629
  font-size: 19px
  text-align: center
  padding: 15px
  margin-bottom: 25px
  color: #000
  font-weight: 700
</style>