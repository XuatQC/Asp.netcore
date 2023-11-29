const routes = [
  {
    path: '/',
    component: () => import('layouts/EmptyLayout.vue'),
    meta: {
      noRequiredAuth: true,
    },
  },
  {
    path: '/main-menu',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        name: 'main-menu',
        path: '',
        meta: {
          title: 'メインメニュー／Main menu',
          breadcrumbs: [],
        },
        component: () => import('pages/MainMenu.vue'),
      },
    ],
  },
  {
    path: '/obs',
    component: () => import('layouts/MainLayout.vue'),
    children: [{
      name: 'obs-list',
      path: 'list',
      meta: {
        title: '観察シート一覧／OBS List',
        breadcrumbs: ['main-menu'],
      },
      component: () => import('pages/obs/OBSList.vue'),
    },
    {
      name: 'obs-input',
      path: 'input/:num',
      meta: {
        title: '観察シート入力／OBS Input',
        breadcrumbs: ['main-menu', 'obs-list'],
      },
      component: () => import('pages/obs/OBSInput.vue'),
    },
    {
      name: 'obs-preview',
      path: 'preview/:num',
      meta: {
        title: '観察シートプレビュー／OBS Preview',
        breadcrumbs: ['main-menu', 'obs-list', 'obs-input'],
      },
      component: () => import('pages/obs/OBSInput.vue'),
    },
    {
      name: 'obs-word-output',
      path: 'word-output',
      meta: {
        title: 'Word出力／Word Output',
        breadcrumbs: ['main-menu', 'obs-list'],
      },
      component: () => import('pages/obs/WordOutput.vue'),
    }],
  },
  {
    path: '/login',
    component: () => import('layouts/EmptyLayout.vue'),
    children: [
      {
        name: 'login',
        path: '',
        component: () => import('pages/LoginView.vue'),
        meta: {
          title: 'ログイン',
          noRequiredAuth: true,
        },
      },
    ],
  },
  {
    path: '/:catchAll(.*)',
    redirect: 'main-menu',
  },
];

export default routes;
