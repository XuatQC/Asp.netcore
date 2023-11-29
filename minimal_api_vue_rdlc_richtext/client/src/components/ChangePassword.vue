<template>
  <div class="container">
    <div class="tw-text-center tw-pb-4">パスワード変更画面 Password Change</div>
    <div class="tw-grid tw-grid-cols-5">
      <div class="tw-col-span-3">
        <div class="tw-grid tw-grid-cols-3 tw-gap-2">
          <div class="tw-col-span-1">
            <div>現パスワード</div>
            <div>Current Password</div>
          </div>
          <div class="tw-col-span-2 tw-flex tw-items-center">
            <q-input outlined v-model="changePasswordObject.currentPassword" dense class="tw-w-full" type="password"/>
          </div>
        </div>
        <div class="tw-grid tw-grid-cols-3 tw-gap-2">
          <div class="tw-col-span-1">
            <div>新パスワード</div>
            <div>New Password</div>
          </div>
          <div class="tw-col-span-2 tw-flex tw-items-center">
            <q-input outlined v-model="changePasswordObject.newPassword" dense class="tw-w-full" type="password"/>
          </div>
        </div>
        <div class="tw-grid tw-grid-cols-3 tw-gap-2">
          <div class="tw-col-span-1">
            <div>新パスワード確認</div>
            <div>Confirm Password</div>
          </div>
          <div class="tw-col-span-2 tw-flex tw-items-center">
            <q-input outlined v-model="changePasswordObject.confirmPassword" dense class="tw-w-full" type="password"/>
          </div>
        </div>
      </div>
      <div class="tw-ml-4 tw-col-span-2 tw-whitespace-pre-line">
        {{ $t('passwordChange.tutorMessage') }}
      </div>
    </div>
  </div>
  <div class="tw-flex tw-justify-center tw-pt-7">
    <button class="btn tw-mr-12" @click="changePasswordHandler">変更／Change</button>
    <button class="btn close" @click="closePopup">キャンセル／Cancel</button>
  </div>
</template>

<script setup>
import dialog from 'utilities/dialog';
import { ref } from 'vue';

// 1) ======= INITIALIZATION ========

// 2) ======= VARIABLE REF ========
const changePasswordObject = ref({
  currentPassword: '',
  newPassword: '',
  confirmPassword: '',
});

// 3) ======= METHOD/FUNCTION ========
// 入力値が有効か確認する
const checkValidForm = () => {
  if (changePasswordObject.value.currentPassword === '' || changePasswordObject.value.newPassword === '' || changePasswordObject.value.confirmPassword === '') {
    return false;
  }
  return changePasswordObject.value.newPassword === changePasswordObject.value.confirmPassword;
};

// ポップアップを閉じるイベント
const closePopup = () => {
  dialog.hide();
};

// 'OK'ボタン押下イベント
const changePasswordHandler = () => {
  if (checkValidForm()) {
    dialog.hide(changePasswordObject);
  }
};

// 4) ======= VUEJS LIFECYCLE ========
</script>

<style scoped>
.text-input {
  @apply tw-border tw-w-full tw-pl-3;
}
</style>
