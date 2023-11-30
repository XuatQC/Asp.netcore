<template>
  <div>
    <header id="top" :class="className">
      <div class="inner">
        <div class="progress-bar-1">
          <h4>商品詳細</h4>
        </div>
        <div class="progress-bar-2">
          <h4>予約入力</h4>
        </div>
        <div class="progress-bar-3">
          <h4>予約入力確認</h4>
        </div>
        <div class="progress-bar-4">
          <h4>予約完了</h4>
        </div>
      </div>
    </header>
  </div>
</template>

<script>
export default {
  name: "HeaderPage",
  props: {
    className: String,
  },
};
</script>
