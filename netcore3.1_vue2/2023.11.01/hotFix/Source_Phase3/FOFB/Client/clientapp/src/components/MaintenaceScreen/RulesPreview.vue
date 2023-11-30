<template>
  <div>
    <!-- Title Start -->
    <div class="title-rules">
      <div class="container">
        <!-- Text for title page rules -->
        <h5 class="title-detail">{{ textContent.textTitle1 }}</h5>
      </div>
    </div>
    <!-- Title End -->
    <!-- Rules Content Start -->
    <div class="rules-content">
      <!-- Text for content page rules -->
      <div class="container" v-html="textContent.textArea1"></div>
    </div>
    <!-- Rules Content End -->
    <!-- Button Group Area Start -->
    <div class="button-back-top-area">
      <div class="container">
        <div class="button-back-top">
          <!-- Text for button back to Top -->
          <button @click="routerToTop()" type="submit" class="btn btn-back-top">
            {{ textContent.textButton1 }}
          </button>
        </div>
      </div>
    </div>
    <!-- Button Group Area End -->
  </div>
</template>

<script>
export default {
  props: {
    textContent: null,
  },
  data: () => ({}),
  async created() {},
  methods: {},
};
</script>

<style></style>
