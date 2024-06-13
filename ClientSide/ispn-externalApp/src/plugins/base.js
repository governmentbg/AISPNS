import Vue from 'vue'
import upperFirst from 'lodash/upperFirst'
import camelCase from 'lodash/camelCase'


const requireComponent = import.meta.globEager('@/components/base/*.vue');

for(let key in requireComponent){
  let fileName = key.split('/')[key.split('/').length-1]
  
  const componentConfig = requireComponent[key]

  const componentName = upperFirst(camelCase(fileName.replace(/^\.\//, '').replace(/\.\w+$/, '')))

  Vue.component(`base${componentName}`, componentConfig.default || componentConfig)
}

// const requireComponent = require.context(
//   '@/components/base', true, /\.vue$/,
// )

// requireComponent.keys().forEach(fileName => {
//   const componentConfig = requireComponent(fileName)

//   const componentName = upperFirst(
//     camelCase(fileName.replace(/^\.\//, '').replace(/\.\w+$/, '')),
//   )

//   Vue.component(`base${componentName}`, componentConfig.default || componentConfig)
// })
