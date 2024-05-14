import router from './router';
import store from './store';
import { getToken } from '@/utils/auth'; // get token from cookie
import getPageTitle from '@/utils/getPageTitle';
import config from "@/config";
//TODO: remove this comment
//import { saveWrongPath } from './api/error';
import { isGuid } from './utils';


const whiteList = ['/login', '/auth-redirect'] // no redirect whitelist

router.beforeEach(async(to, from, next) => {
  // set page title
  
  document.title = getPageTitle((to.meta.title || 'Вътрешна система') + " :: "+config.title)
  // determine whether the user has logged in
  const hasToken = getToken() || store.getters.token;
  if(hasToken) {
    // if user wants to go to login page, firstly we check if he has a valid token by dispatch event 'getInfo'.. if he has - we redirect him to the first route he has access
    if(to.path === '/login') {
      if(router.options.routes.length === 1){
        //const { access } = await store.dispatch('user/getInfo')
        const { roles } = await store.dispatch('user/getInfo')
        
        // generate accessible routes map based on roles
        const accessRoutes = await store.dispatch('permission/generateRoutes', roles)
        
        // dynamically add accessible routes
        router.addRoutes(accessRoutes);
      }
      // if is logged in, redirect to the home page
      next({ path: getFirstAvailablePath(router) })
      //vue.$loading(false);
    } else {
      // determine whether the user has obtained his permission roles through getInfo
      const hasRoles = store.getters.roles && store.getters.roles.length > 0;

      if(hasRoles) {
        store.dispatch('permission/addLastRoute', from);
        
        next();
      } else {
        try {
          // get user info
          const { roles } = await store.dispatch('user/getInfo')
          
          // generate accessible routes map based on roles
          const accessRoutes = await store.dispatch('permission/generateRoutes', roles)

          // dynamically add accessible routes
          router.addRoutes(accessRoutes);
          
          let goTo;
          
          // check if user has access to the requested route by its roles... if not - catch first available route and redirect user there
          // it is possible to get an error in 'router.resolve' so that's the reason of catch block
          try {
            let isRouteExists = router.resolve({path: to.path});
            
            //if(isRouteExists.resolved.matched.length === 0 || isRouteExists.resolved.matched[0].path === '*'){
            if(isRouteExists.resolved.matched.length === 0 || to.path === "/"){
              for(let route of router.options.routes){
                if(route.path !== '/' && route.path !== '/login' && route.path != '/*' && route.path != '*'){
                  goTo = route;
                  break;
                }
              }
              //TODO: remove this comment
              // if(to.path !== '/')
              //   saveWrongPath(to.path);
              
              next({path: goTo.path});
              return;
            }
          } catch(e) {
            // we are here because of router.resolve :)
            for(let route of router.options.routes){
              if(route.path !== '/' && route.path !== '/login' && route.path != '/*' && route.path != '*'){
                goTo = route;
                break;
              }
            }
            //TODO: remove this comment
            // if(to.path !== '/')
            //   saveWrongPath(to.path);

            next({path: goTo.path});
            return;
          }

          let splittedPath = to.path.split('/');
          let lastPathSegment = splittedPath.pop();

          let checkPath = null;
          if(isGuid(lastPathSegment)){
            splittedPath.push(':id');
          } else {
            splittedPath.push(lastPathSegment);
          }
          
          checkPath = splittedPath.join("/");

          // user has access to the route.. so redirect it
          next({ ...to, replace: true })
        } catch (error) {
          // remove token and go to login page to re-login
          await store.dispatch('user/resetToken')
          
          next(`/login?redirect=${to.path}`)
        }
      }
    }
  } else {
    // has no token
    if (whiteList.indexOf(to.path) !== -1) {
      // in the free login whitelist, go directly
      next()
    } else {
      next(`/login?redirect=${to.path}`);
    }
  }
})


function getFirstAvailablePath(router){
  let path;
  for(let route of router.options.routes){
    if(route.path !== '/' && route.path !== '/login' && route.path != '/*' && route.path != '*'){
      path = route.path;
      break;
    }
  }
  return path;
}