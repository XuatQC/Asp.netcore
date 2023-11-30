<template>
  <div>
    <!-- Header Start -->
    <HeaderPage :className="className"></HeaderPage>
    <!-- Header End -->
    <!-- Banner Area Start -->
    <section class="part-select">
      <div class="container">
        <div class="row">
          <!-- Image Product details Start -->
          <div class="group-image">
            <div class="Large-image">
              <div class="img-active">
                <VueSlickCarousel
                  ref="mainImd"
                  :asNavFor="$refs.thumbImg"
                  v-if="this.lstImageProduct !== null"
                >
                  <img
                    v-for="image in this.lstImageProduct"
                    :src="PRODUCT_DETAIL_PATH + image.imageUrl"
                    :key="image.productImgUrlId"
                  />
                </VueSlickCarousel>
              </div>
            </div>
            <div class="Thumbnail-image">
              <div class="thumbnail-img-group">
                <VueSlickCarousel
                  ref="thumbImg"
                  :asNavFor="$refs.mainImd"
                  :slidesToShow="5"
                  v-if="this.lstImageProduct !== null"
                  :focusOnSelect="true"
                >
                  <img
                    v-for="image in this.lstImageProduct"
                    :src="PRODUCT_DETAIL_PATH + image.imageUrl"
                    :key="image.productImgUrlId"
                  />
                </VueSlickCarousel>
              </div>
            </div>
          </div>
          <!-- Image Product details End -->

          <!-- Product details Start -->
          <div class="details-product">
            <div class="product-details-content">
              <p class="product-code">FOKID R182011</p>
              <br />
              <span class="product-name">ジュニア福袋4点セット ON </span>
              <div class="product-price-grp">
                <!-- Product Price -->
                <div class="price">
                  <p>11,000<span class="tax"> (税込)</span></p>
                </div>
              </div>
              <!-- List Product Order Start -->
              <div class="table-responsive pro-details-quality">
                <table class="table">
                  <tbody>
                    <tr>
                      <td><p>S</p></td>
                      <td>
                        <span>在庫</span>
                      </td>
                      <td>
                        <span>〇</span>
                      </td>
                      <td>
                        <div>
                          <select
                            disabled
                            class="product-quantity custom-select"
                            id="__BVID__49"
                            ><option value="0">0</option
                            ><option value="1">1</option
                            ><option value="2">2</option
                            ><option value="3">3</option
                            ><option value="4">4</option></select
                          >
                        </div>
                      </td>
                      <td>
                        <select
                          disabled
                          class="product-color select-as-text custom-select"
                          id="__BVID__50"
                          ><option value="ブラック">ブラック</option></select
                        >
                      </td>
                    </tr>
                    <!---->
                  </tbody>
                  <tbody>
                    <tr>
                      <td><p>M</p></td>
                      <td>
                        <span>在庫</span>
                      </td>
                      <td>
                        <span>〇</span>
                      </td>
                      <td>
                        <div>
                          <select
                            disabled
                            class="product-quantity custom-select"
                            id="__BVID__51"
                            ><option value="0">0</option
                            ><option value="1">1</option
                            ><option value="2">2</option
                            ><option value="3">3</option
                            ><option value="4">4</option></select
                          >
                        </div>
                      </td>
                      <td>
                        <select
                          disabled
                          class="product-color custom-select"
                          id="__BVID__52"
                          ><option value="ネイビーブルー">ネイビーブルー</option
                          ><option value="ブラウン">ブラウン</option
                          ><option value="ブラック">ブラック</option></select
                        >
                      </td>
                    </tr>
                    <!---->
                  </tbody>
                  <tbody>
                    <tr>
                      <td><p>L</p></td>
                      <td>
                        <span>在庫</span>
                      </td>
                      <td>
                        <span>〇</span>
                      </td>
                      <td>
                        <div>
                          <select
                            disabled
                            class="product-quantity custom-select"
                            id="__BVID__53"
                            ><option value="0">0</option
                            ><option value="1">1</option
                            ><option value="2">2</option
                            ><option value="3">3</option
                            ><option value="4">4</option></select
                          >
                        </div>
                      </td>
                      <td>
                        <select
                          disabled
                          class="product-color select-as-text custom-select"
                          id="__BVID__54"
                          ><option value="ブラック">ブラック</option></select
                        >
                      </td>
                    </tr>
                    <!---->
                  </tbody>
                </table>
              </div>
              <!--  List Product Order End -->
              <!-- Notice Product -->
              <p class="note"><span>※数量指定ください。</span></p>
              <p class="note">
                <span>※一度に予約できる金額は300,000円までです。</span>
              </p>
              <p class="note">
                <span
                  >※1サイズに対してお一人様3点までしかご指定できません。</span
                >
              </p>
            </div>
            <div v-if="textContent.isOneProduct" class="button-process">
              <!-- Button back to list Product -->
              <button
                type="submit"
                class="btn btn-back-screen btn-back-to-list"
                style="margin: 8px"
              >
                <span class="arrow-button">←</span>
                {{ textContent.textButton2 }}
              </button>

              <!-- Button back to list Product(Keep selected products)-->
              <button
                type="submit"
                class="btn btn-back-screen btn-back-to-list"
                style="margin: 8px"
              >
                <span class="arrow-button">←</span>
                {{ textContent.textTitle2 }}
              </button>
            </div>
          </div>
          <!-- Product Details End -->
        </div>
      </div>
    </section>
    <!-- Banner Area End -->
    <section class="static-area">
      <div class="container">
        <div class="content-title">
          <p class="title">■商品情報</p>
        </div>
        <!-- Info Product -->
        <div
          class="static-area-wrap scrollingDiv scrollbar-ripe-malinka"
          v-html="textContent.textArea1"
        ></div>
      </div>
    </section>
    <!-- Info Area Start -->
    <section class="static-area">
      <div class="container">
        <div class="content-title">
          <p class="title">■注意事項</p>
        </div>
        <!-- Info Product Sub -->
        <div
          class="static-area-wrap scrollingDiv scrollbar-ripe-malinka"
          v-html="textContent.textArea2"
        ></div>
      </div>
    </section>
    <!-- Info Area End -->
    <div class="form-check">
      <div>
        <div class="button-confirm">
          <!-- Button Router Order Confirm -->
          <button
            type="submit"
            class="btn btn-confirm"
            style="text-transform: none"
          >
            {{ textContent.textButton1 }}
          </button>
        </div>
      </div>
      <div class="form-check-box row" style="margin-bottom: 0px">
        <!--Lable For Button Order-->
        <label class="form-check-label">{{ textContent.textNortify2 }}</label>
      </div>
    </div>
  </div>
</template>

<script>
import HeaderPage from "@/components/DataCommon/HeaderPage.vue";

export default {
  components: {
    HeaderPage,
  },
  props: {
    textContent: {},
  },
  data: () => ({
    className: "progress-bar-product-detail",
    lstImageProduct: [
      {
        imageUrl: "breeze/J182011/J182011_BK_B_2.jpg",
        productImgUrlId: 0,
      },
      {
        imageUrl: "breeze/J182011/J182011_sub01_2.jpg",
        productImgUrlId: 1,
      },
      {
        imageUrl: "breeze/J182011/J182011_sub02_2.jpg",
        productImgUrlId: 2,
      },
      {
        imageUrl: "breeze/J182011/J182011_sub03_2.jpg",
        productImgUrlId: 3,
      },
      {
        imageUrl: "breeze/J182011/J182011_sub04_2.jpg",
        productImgUrlId: 4,
      },
    ],
  }),
  async created() {},
  mounted() {},
};
</script>
<style lang="scss">
.progress-bar-product-detail .progress-bar-1 {
  background-image: url(/Images/product-detail/progress_bar_1.png);
  background-repeat: no-repeat;
  width: 95px;
}
.progress-bar-product-detail .progress-bar-2 {
  background-image: url(/Images/product-detail/progress_bar_2.png);
  background-repeat: no-repeat;
  width: 100px;
  margin-left: -12px;
}

.progress-bar-product-detail .progress-bar-3 {
  background-image: url(/Images/product-detail/progress_bar_3.png);
  background-repeat: no-repeat;
  width: 119px;
  margin-left: -12px;
}

.progress-bar-product-detail .progress-bar-4 {
  background-image: url(/Images/product-detail/progress_bar_4.png);
  background-repeat: no-repeat;
  width: 97px;
  margin-left: -12px;
}
.progress-bar-product-detail .progress-bar-1,
.progress-bar-product-detail .progress-bar-2,
.progress-bar-product-detail .progress-bar-3,
.progress-bar-product-detail .progress-bar-4 {
  color: white;
  height: 35px;
  float: left;
}
.progress-bar-product-detail .inner h4 {
  font-size: 14px;
  font-weight: bold;
  line-height: 35px;
  margin-left: 20px;
}
.static-area .static-area-wrap {
  font-weight: 350 !important;
}
</style>
