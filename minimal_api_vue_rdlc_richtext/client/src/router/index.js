import { createRouter, createWebHistory } from 'vue-router';
import { useAuthStore } from 'stores/auth-store';
import AuthService from 'services/auth.service';
import routes from './routes';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

router.beforeEach(async (to, from, next) => {
  const { isLoggedIn } = useAuthStore();
  if (to.meta.noRequiredAuth) {
    if (isLoggedIn && (to.name === 'login' || to.path === '/')) {
      next({ name: 'main-menu' });
    } else if (!isLoggedIn && to.path === '/') {
      next({ name: 'login' });
    } else next();
  } else if (!isLoggedIn) {
    const verify = await AuthService.check();
    if (verify) {
      next();
    } else {
      next({ name: 'login' });
    }
  } else next();
});

export default router;
