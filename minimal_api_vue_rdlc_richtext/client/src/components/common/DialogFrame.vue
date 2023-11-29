<template>
  <q-dialog v-for="item in info" :key="item" :model-value="item.show" @hide="cancel" persistent
    class="tw-bg-secondary tw-w-full">
    <q-card :style="{ width: item.config.width, maxWidth: item.config.maxWidth }" class="bg-white">
      <q-card-section class="tw-text-lg tw-font-bold tw-border-b tw-flex items-center tw-bg-[#edf4f0]" :class="{
        'justify-center': !item.config.closeButtonCornerRight,
        'justify-between': item.config.closeButtonCornerRight,
        'tw-justify-start': item.config.headerLeft
      }">
        <img v-if="item.config?.titleLogo" src="logo2.png" class="tw-w-[50px] tw-absolute tw-left-0 tw-p-2" alt="title logo"/>
        <span>{{ item.title }}</span>
        <div v-if="item.config.closeButtonCornerRight" class="tw-cursor-pointer" @click="cancel">
          &#10005;
        </div>
      </q-card-section>
      <q-card-section class="tw-min-h-[80px]" :class="{ 'tw-mx-3': item.config.isComponent }">
        <div v-if="item.config.isComponent">
          <component :is="item.content" v-bind="item.config.params"></component>
        </div>
        <div v-else style="white-space: break-spaces;">{{ item.content }}</div>
      </q-card-section>
      <q-card-section class="tw-border-t tw-text-right" v-if="item.config.showFooter">
        <div class="tw-space-x-2">
          <button class="btn" v-for="btn in item.config.buttons" :key="btn" @click="btnClick(btn)">
            <template v-if="item.config.isUseI18n">
              {{ $t(btn) }}
            </template>
            <template v-else>
              {{ btn }}
            </template>
          </button>
        </div>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script setup>
import { useDialogStore } from 'stores/dialog-store';
import dialog from 'utilities/dialog';

// 1) ======= INITIALIZATION ========
const dialogStore = useDialogStore();
const info = dialogStore.dialog;

// 3) ======= METHOD/FUNCTION ========
// 現在ポップアップを閉じるイベント
const cancel = () => {
  dialog.hide();
};

// フッターに該当するイベントをキャッチする
const btnClick = (btn) => {
  dialog.hide(btn);
};
</script>
