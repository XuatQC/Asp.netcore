<template>
  <q-layout view="hHh lpR fFf" style="min-height: auto;">
    <main-header />
    <q-page-container class="tw-h-full" style="padding-top: 0;">
      <router-view v-slot="{ Component, route }">
        <component :is="Component" :key="route.fullPath" />
      </router-view>
    </q-page-container>
  </q-layout>
</template>

<script setup>
import MainHeader from 'components/common/MainHeader.vue';
</script>
