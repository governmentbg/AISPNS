const path = require("path");

module.exports = {
  devServer: {
    disableHostCheck: true,
  },
  //outputDir: path.resolve(__dirname, "../wwwroot")
  outputDir: path.resolve(__dirname, "./dist")
}
