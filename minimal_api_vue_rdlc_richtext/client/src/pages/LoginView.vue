<template>
  <div></div>
</template>
<script setup>
import { onMounted, defineAsyncComponent } from 'vue';
import dialog from 'utilities/dialog';
// 1) ======= INITIALIZATION ========
const login = defineAsyncComponent(() => import('components/LoginModal.vue'));

// 2) ======= VARIABLE REF ========

// 3) ======= METHOD/FUNCTION ========

// 4) ======= VUEJS LIFECYCLE ========
// コンポーネント初期後
onMounted(() => {
  dialog.showContent('Field Notes Database - Welcome and Login', login, { width: '600px', titleLogo: true });
});
</script>
