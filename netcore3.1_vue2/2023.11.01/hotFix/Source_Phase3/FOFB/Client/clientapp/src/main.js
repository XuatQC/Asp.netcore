import Vue from 'vue'
import App from './App.vue'
import axios from 'axios'
import vuetify from './plugins/vuetify';
import router from './router'
import VueElementLoading from 'vue-element-loading'
import VueCookies from 'vue-cookies'
import VueAxios from 'vue-axios'
import Vuex from 'vuex'
import  store  from './store'
import common from './util/common.js'
import constant from './util/constant.js'
import commonCallApi from './util/cmnCallApi.js'
import VueSlickCarousel from 'vue-slick-carousel'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import VueToastr from 'vue-toastr'
import VeeValidate from 'vee-validate';
import CKEditor from 'ckeditor4-vue'
import money from 'v-money'
// Import Bootstrap an BootstrapVue CSS files (order is important)
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
Vue.mixin(common)
Vue.mixin(constant)
Vue.mixin(commonCallApi)

const dictionary = {
  en: {
    messages: {
      required: () => '必須項目です。',
      email: () => 'メールのフォーマットが正しくありません。',
      min: () => 'フォーマットが正しくありません。',
      max: () => 'フォーマットが正しくありません。',
      regex: () => 'フォーマットが正しくありません。'
    }
  }
}

Vue.use(Vuex)
Vue.use(money, {precision: 0,thousands: ",",});
Vue.config.productionTip = false
Vue.component('VueElementLoading', VueElementLoading)
Vue.use(VueCookies)
Vue.use(VueAxios, axios)
Vue.component('VueSlickCarousel', VueSlickCarousel)
// Make BootstrapVue available throughout your project
Vue.use(BootstrapVue)
// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin)
Vue.use(CKEditor)
Vue.use(VeeValidate);
VeeValidate.Validator.localize(dictionary)

Vue.use(VueToastr, {
  defaultTimeout: 2000,
  defaultProgressBar: false,
  defaultProgressBarValue: 0,
  defaultStyle: { width: '100%' }
})

// ---------------Check login--------------------
Vue.axios.interceptors.request.use((config) => {
  const authToken = Vue.$cookies.get('token')
  if (authToken) {
    // Set token to header
    config.headers.Authorization = `Bearer ${authToken}`
  }
  return config
}, (err) => {
  return Promise.reject(err)
})

// User wrong or deleted
Vue.axios.interceptors.response.use((response) => {
  if (response.data !== null && response.data.msgUserErr !== undefined) {
    try {
      router.push({
        name: 'login',
        params: {
          errorType: 'USER_ERR',
          errorMsg: response.data.msgUserErr

        }
      }).catch(() => {})
    } catch (e) {
      return Promise.reject(e.message)
    }
    return Promise.reject(response.data.msgUserErr)
  }
  if (response.data !== null && response.data.msgShopAaccessErr !== undefined) {
    try {
      router.push({
        name: 'login',
        params: {
          errorType: 'OUT_OF_PERIOD',
          errorMsg: response.data.msgShopAaccessErr

        }
      }).catch(() => {})
    } catch (e) {
      return Promise.reject(e.message)
    }
    return Promise.reject(response.data.msgUserErr)
  }

  // User information has changed
  if (response.data !== null &&
    response.data.norticeMsgId !== undefined &&
    response.data.norticeMsgId === 'USER_CHANGED') {
    try {
      router.push({
        name: 'login',
        params: {
          errorType: 'USER_CHANGED',
          errorMsg: ''

        }
      }).catch(() => {})
    } catch (e) {
      return Promise.reject(e.message)
    }
    var errorMsg = 'Infor updated'
    return Promise.reject(errorMsg)
  }

  // Sesstion timeout
  if (response.data !== null &&
    response.data.norticeMsgId !== undefined &&
    response.data.norticeMsgId === 'SESSION_EXPIRED') {
    try {
      router.push({
        name: 'login',
        params: {
          errorType: 'SESSION_EXPIRED',
          errorMsg: ''

        }
      }).catch(() => {})
    } catch (e) {
      return Promise.reject(e.message)
    }
    var sessionExpireMsg = 'Session Expired'
    return Promise.reject(sessionExpireMsg)
  }

  // Check permission
  if (response.data !== null &&
    response.data.norticeMsgId !== undefined &&
    response.data.norticeMsgId === 'ACCESS_DENIED') {
    try {
      router.push({
        name: 'login',
        params: {
          errorType: 'ACCESS_DENIED',
          errorMsg: response.data.msgAccessErr

        }
      }).catch(() => {})
    } catch (e) {
      return Promise.reject(e.message)
    }
    return Promise.reject('access deined')
  }
  return response
}, function (error) {
  if (!error.request.status) { // network error
    window.toastr.error('接続できていません')
    return Promise.reject(error)
  } else {
    if (error.request.status === 401) { // Not authentication
      try {
        router.push({
          name: 'login',
          params: {
            errorType: 'AUTO_LOGIN_EXPIRED',
            errorMsg: ''

          }
        }).catch(() => {})
      } catch (e) {
        return Promise.reject(e.message)
      }
      return Promise.reject(error)
    }
  }
})

new Vue({
  store,
  router,
  vuetify,
  render: h => h(App)
}).$mount('#app')
