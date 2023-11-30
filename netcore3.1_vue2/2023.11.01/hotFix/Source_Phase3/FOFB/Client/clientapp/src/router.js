import Router from 'vue-router'
import Vue from 'vue'

//___________ pages __________________
import Top from '@/pages/front/Top'
import ProductList from '@/pages/front/ProductList'
import ProductDetail from '@/pages/front/ProductDetail'
import Order from '@/pages/front/Order'
import OrderFinish from '@/pages/front/OrderFinish'
import Rule from '@/pages/front/Rules'
import OrderConfirm from '@/pages/front/OrderConfirm'
import NotFound from '@//pages/404'
import Login from '@/pages/back/admin/Login'
import MaintainceOrder from '@/pages/back/admin/MaintenaceOrder'
import MaintenaceMailTemplate from '@/pages/back/admin/MaintenaceMailTemplate'
import MaintenaceStock from '@/pages/back/admin/MaintenaceStock'
import MaintenaceShop from '@/pages/back/admin/MaintenaceShop'
import UrlSetting from '@/pages/back/admin/UrlSetting'
import MaintenaceScreen from '@/pages/back/admin/MaintenaceScreen'
import ShopHome from '@/pages/back/shop/Home'
import MaintenaceDateTime from '@/pages/back/admin/MaintenaceDateTime'
import MaintenaceRole from '@/pages/back/admin/MaintenaceRole'
import MaintenaceUser from '@/pages/back/admin/MaintenaceUser'
import OrderHistory from '@/pages/back/shop/OrderHistory'
import ChangePassword from '@/pages/back/admin/ChangePassword'

Vue.use(Router)

const router = new Router({
  mode: 'history',
  routes: [
    // _______________ Front route ________________________________
    {
      path: '/product-list',
      name: 'product-list',
      meta: { requiresAuth: false, forAdmin: false },
      component: ProductList
    },
    {
      path: '/product-detail',
      name: 'product-detail',
      meta: { requiresAuth: false, forAdmin: false },
      component: ProductDetail
    },
    {
      path: '/order',
      name: 'order',
      meta: { requiresAuth: false, forAdmin: false },
      component: Order
    },
    {
      path: '/order-confirm',
      name: 'order-confirm',
      meta: { layout: 'userpages', requiresAuth: false, side: 'front' },
      component: OrderConfirm
    },
    {
      path: '/order-finish',
      name: 'order-finish',
      meta: { requiresAuth: false, forAdmin: false },
      component: OrderFinish
    },
    {
      path: '/rules',
      name: 'rules',
      meta: { requiresAuth: false, forAdmin: false },
      component: Rule
    },    
    // _______________ End front route ________________________________
    // _______________ Admin route ____________________________________
    {
      path: '/admin/login',
      name: 'login',
      meta: { requiresAuth: false, forAdmin: true, fullPage: true },
      component: Login
    },
    {
      path: '/admin/manage-order',
      name: 'manage-order',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: MaintainceOrder
    },
    {
      path: '/admin/',
      name: 'manage-order-alter',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: MaintainceOrder
    },
    {
      path: '/admin/manage-mail',
      name: 'manage-mail',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: MaintenaceMailTemplate
    },
    {
      path: '/admin/manage-stock',
      name: 'manage-stock',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: MaintenaceStock
    },
        {
      path: '/admin/manage-screen',
      name: 'manage-stock',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: MaintenaceScreen
    },
    {
      path: '/admin/manage-shop',
      name: 'manage-shop',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: MaintenaceShop
    },
    {
      path: '/admin/url-setting',
      name: 'url-setting',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: UrlSetting
    },
    {
      path: '/admin/manage-dateTime',
      name: 'manage-dateTime',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: MaintenaceDateTime
    },
    {
      path: '/admin/manage-role',
      name: 'manage-role',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: MaintenaceRole
    },
    {
      path: '/admin/manage-user',
      name: 'manage-user',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: MaintenaceUser
    },
    {
      path: '/admin/change-password',
      name: 'change-password',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: ChangePassword
    },
    // _______________ End Admin route __________________________________
    // _______________ Shop route _______________________________________
    {
      path: '/shop/home',
      name: 'shopHome',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: ShopHome
    },
    {
      path: '/shop/order-history',
      name: 'order-history',
      meta: { requiresAuth: true, forAdmin: true, fullPage: false },
      component: OrderHistory
    },
    // _______________ End Shop route ___________________________________
    // _______________ Error route ______________________________________
    {
      path: '/404',
      name: '404',
      meta: { requiresAuth: false, forAdmin: false },
      component: NotFound
    },
    {
      path: '/:businessUrl',
      name: 'top',
      meta: { requiresAuth: false, forAdmin: false },
      component: Top
    },
    // _______________ End Error route __________________________________
  ]
})
router.beforeEach(async (to, from, next) => {
  if (to.matched.length) {
    if (to.matched.some(record => record.meta.requiresAuth)) {
      const token = Vue.$cookies.get('token')
      // this route requires auth, check if logged in
      // if not, redirect to login page.
      if (token === null) {
        next({ name: 'login' })
      } else { // check if user has permission to view page
        let userRole = 0
        let rolePerPage = []

        let tokenObj = ''
        if (token) {
          tokenObj = JSON.parse(atob(token.split('.')[1]))
          userRole = parseInt(tokenObj.role)
        }
        if (to.meta.role !== undefined) {
          rolePerPage = to.meta.role.filter(pageRole => pageRole === userRole)
          // If staff does not have permision to see this page
          if (rolePerPage.length === 0) {
            Vue.$cookies.remove('token')
            Vue.$cookies.remove('userData')
            localStorage.clear()
            sessionStorage.clear()
            window.toastr.error('この画面にアクセスする権限はありません。')
            next({ name: 'login' })
          }
        }
        next() // go to wherever I'm going
      }
    } else {
      next() // go to wherever I'm going
    }
  }else {
      next({ name: '404' })
  }
})

export default router
