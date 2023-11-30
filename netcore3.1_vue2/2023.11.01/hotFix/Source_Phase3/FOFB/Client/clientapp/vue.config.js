const path = require("path");

module.exports = {
    "outputDir": path.resolve(__dirname, "../../Server/bin/Release/publish/wwwRoot/dist"),
  "runtimeCompiler": true,
  "devServer": {
    "proxy": {
      "^/api": {
        "target": process.env.ASPNET_URL || "https://localhost:5011"
      },
      "^/admin/api": {
        "target": process.env.ASPNET_URL || "https://localhost:5011"
      },
      "^/shop/api": {
        "target": process.env.ASPNET_URL || "https://localhost:5011"
      },
      "^/Images": {
        "target": process.env.ASPNET_URL || "https://localhost:5011"
      }
    }
  },
  "configureWebpack": {
    "devtool": "source-map",
    "output": {},
    "resolve": {
      "alias": {
        "vue": "vue/dist/vue.esm.js"
      }
    }
  },
  "transpileDependencies": [
    "vuetify"
  ]
}