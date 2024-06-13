const path = require("path");
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;
const VuetifyLoaderPlugin = require('vuetify-loader/lib/plugin');
const webpack = require('webpack');

module.exports = {
  configureWebpack: config => {
    if(process.env.NODE_ENV === "production"){
      return {
        plugins: [
          new webpack.ContextReplacementPlugin(/moment[/\\]locale$/, /en|bg/), 
          //new VuetifyLoaderPlugin()
        ]
      }
    } else {
      return { 
        plugins: [
          new BundleAnalyzerPlugin(),
          new webpack.ContextReplacementPlugin(/moment[/\\]locale$/, /en|bg/),
          //new VuetifyLoaderPlugin()
        ]
      }
    }
  },
  // configureWebpack: {
  //   plugins: [
  //     new BundleAnalyzerPlugin(),
  //     new webpack.ContextReplacementPlugin(/moment[/\\]locale$/, /en|bg/),
  //     new VuetifyLoaderPlugin()
  //   ],
  // },
  devServer: {
    disableHostCheck: true,
  },
  outputDir: path.resolve(__dirname, "../wwwroot")
  //outputDir: path.resolve(__dirname, "../../../BIM_Register/wwwroot"), // IIS Deploy
  //outputDir: path.resolve(__dirname, "./dist")
}
