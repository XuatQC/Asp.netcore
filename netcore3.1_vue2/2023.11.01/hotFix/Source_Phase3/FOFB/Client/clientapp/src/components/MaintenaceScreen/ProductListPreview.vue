<template>
  <div>
    <!-- Image Start -->
    <div v-if="urlImgTop != ''">
      <img class="d-block w-100" :src="urlImgTop.imageUrl" />
    </div>
    <!-- Image End -->
    <!-- Product List Area Start -->
    <section class="product-list">
      <div class="container" v-if="urlImgProducts.length > 1">
        <div class="row product-list-content" >
          <div class="product-item" v-for="product in urlImgProducts"
            :key="product.productCd">
            <div class="content">
              <div class="image-product-item">
                <img
                  :src="PRODUCT_LIST_PATH + product.imageUrl"
                  :alt="product.imageUrl"
                  class="d-block w-100"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <!-- Product List Area End -->
    <!-- Note Area Start -->
    <div class="product-list-note">
      <!-- Text notify config -->
      <div
        v-if="textContent.buttonFlg === true"
        v-html="textContent.textNortify1"
      ></div>
      <div v-else>
        <img class="d-block w-100" :src="urlImgBottom1.imageUrl" alt="" />
        <img class="d-block w-100" :src="urlImgBottom2.imageUrl" alt="" />
      </div>
    </div>
    <!-- Note Area End -->
    <!-- Footer Area start -->
    <footer class="footer-area">
      <div class="footer-menu-2">
        <ul>
          <li><span>プライバシーポリシー&nbsp;&nbsp;｜&nbsp;&nbsp;</span></li>
          <li><span>特定商取引に基づく表示&nbsp;&nbsp;｜&nbsp;&nbsp;</span></li>
          <li><span>会社概要&nbsp;&nbsp;｜&nbsp;&nbsp;</span></li>
          <li><span>お問合せ</span></li>
        </ul>
      </div>
      <hr />
      <p class="copyright">© F.O.INTERNATIONAL CO., LTD.</p>
    </footer>
    <!--  Footer Area End -->
  </div>
</template>

<script>

export default {
  props: {
    textContent: null,
    urlImgTop: Object,
    urlImgProducts: {},
    urlImgBottom1: Object,
    urlImgBottom2: Object,
  },
  data: () => ({}),
  async created() {
   
  },
  methods: {},
};
</script>

<style></style>
