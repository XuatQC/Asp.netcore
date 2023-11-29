<template>
  <dialog-frame />
  <router-view />
</template>

<script setup>
import { onMounted } from 'vue';
import DialogFrame from 'components/common/DialogFrame.vue';
import { useAppStore } from 'stores/app-store';
import { changeLanguage } from 'utilities/language';

// 1) ======= INITIALIZATION ========

// 2) ======= VARIABLE REF ========

// 3) ======= METHOD/FUNCTION ========

// 4) ======= VUEJS LIFECYCLE ========
onMounted(() => {
  const { language } = useAppStore();
  // ブラウザリフレッシュ後にシステム言語を再設定する
  if (Object.keys(language).length > 0) {
    changeLanguage(language);
  }
});
</script>
