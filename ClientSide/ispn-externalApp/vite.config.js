import path from 'path'
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue2' // Vue 2.7
import { visualizer } from 'rollup-plugin-visualizer'
import { VuetifyResolver } from 'unplugin-vue-components/resolvers';
import Components from 'unplugin-vue-components/vite';

export default defineConfig({
  plugins: [
    vue(),
    visualizer(),
    Components({
      resolvers: [
        VuetifyResolver(),
      ],
    }),
  ],
  define: {
  },
  server: {
    //host: "192.168.50.102",
    port: 8080,
  },
  resolve: {
    alias: [
      {
        find: '@',
        replacement: path.resolve(__dirname, 'src')
      }
    ]
  },
  build: {
    //outDir: "../../../BIM_Register/wwwroot", // IIS Deploy
    outDir: "./dist",
    chunkSizeWarningLimit: 700,
    emptyOutDir: true
  },
  css: {
    preprocessorOptions: {
      scss: {
        implementation: require("sass"),
        additionalData: `
          @import "./src/sass/overrides.sass";
        `,
      },
      sass: {
        additionalData: "\n@import './src/sass/variables.scss'\n",
      }
    },
  },
})