<template>
  <div>
    <!-- Slider Arae Start -->
    <b-carousel id="carousel-fade" indicators>
      <b-carousel-slide
        v-for="image in this.lstImage"
        :key="image.scrImgUrlId"
        :img-src="image.imageUrl"
      ></b-carousel-slide>
    </b-carousel>

    <!-- Slider Arae End -->
    <!-- title Start -->
    <div class="title-shop">
      <!--  Title page -->
      <h5 class="title_text">{{ textContent.textTitle1 }}</h5>
    </div>
    <!-- title End -->
    <!-- Img Step Start-->
    <div class="step_book">
      <img class="img_mid" :src="urlImgMid.imageUrl" />
    </div>
    <!-- Img Step End -->

    <!-- Static Area Start -->
    <section class="part-rules static-area">
      <div class="container">
        <!-- Text Notice -->
        <div
          class="static-area-wrap scrollingDiv scrollbar-ripe-malinka"
          v-html="textContent.textArea1"
        ></div>
      </div>
    </section>
    <!-- Static Area End -->
    <!-- Brand area start -->
    <div class="form-check">
      <div class="form-check-box row">
        <input :disabled="true" type="checkbox" id="chkOpenShop" />
        <label class="form-check-label" for="chkOpenShop"
          >上記確認しました</label
        >
      </div>
      <div>
        <button type="submit" class="btn btn-submit btn-Agree">
          <!-- text Button Next Page -->
          {{ textContent.textButton1 }}
        </button>
      </div>
    </div>
    <!-- Brand area end -->
  </div>
</template>

<script>
export default {
  props: {
    textContent: null,
    urlImgMid: Object,
    lstImage: Array,
  },
  data() {
    return {};
  },
  async created() {},
  methods: {},
};
</script>

<style scoped></style>
