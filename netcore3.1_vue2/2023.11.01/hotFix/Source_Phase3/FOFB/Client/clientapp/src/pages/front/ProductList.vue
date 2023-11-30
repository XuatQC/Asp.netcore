<template>
  <div>
    <VueElementLoading
      :active="loading"
      spinner="ring"
      color="var(--success)"
    />
    <!-- Image Start -->
    <div v-if="urlImageTop != ''">
      <img class="d-block w-100" :src="urlImageTop" />
    </div>
    <!-- Image End -->
    <!-- Product List Area Start -->
    <section class="product-list">
      <div class="container">
        <div class="row product-list-content" v-if="listProduct.length > 0">
          <div
            class="product-item"
            v-for="product in this.listProduct"
            :key="product.productCd"
          >
            <div class="content">
              <div class="image-product-item">
                <img
                  class="d-block w-100"
                  @click="routerToDetail(product.productCd)"
                  :src="PRODUCT_LIST_PATH + product.imageUrl"
                  :alt="product.imageUrl"
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
        class="content"
        v-if="isShowImage !== 0"
        style="word-wrap: break-word;max-width: 1024px;"
        v-html="textNote"
      ></div>
      <div v-else v-for="image in lstImage" :key="image.scrImgUrlId">
        <img
          class="d-block w-100"
          :src="PRODUCT_LIST_PATH + image.imageUrl"
          alt=""
        />
      </div>
    </div>
    <!-- Note Area End -->
  </div>
</template>

<script>
export default {
  data() {
    return {
      loading: false,
      imageTop: {},
      lstImage: null,
      listProduct: [],
      urlImageTop: "",
      textNote: "",
      bussinessCd: "",
      isShowImage: 0,
    };
  },
  async created() {
    this.loading = true;
    this.bussinessCd = localStorage.getItem("bussinessCd");
    if (this.bussinessCd != null && this.bussinessCd != undefined) {
      const isValidOrdDTmRespone = await this.chkTimeOpenShop(this.bussinessCd);
      if (isValidOrdDTmRespone) {
        this.checkEndOrder();
      } else {
        this.$router.push({ name: "404" });
      }
      await this.getConfig(this.bussinessCd);
      await this.loadData(this.bussinessCd);
      if (this.$route.params.keepSelectedPrds === undefined) {
        this.clearSelectedProduct();
      }
    } else {
      this.$router.push({ name: "404" });
    }
  },
  methods: {
    async loadData(bussinessCd) {
      const config = {
        method: "get",
        url: "api/MProduct/GetListProduct",
        params: {
          bussinessCd: bussinessCd,
          isMainImgInList: true,
        },
      };
      try {
        const listProduct = await require("axios")(config);
        if (listProduct != null && listProduct.data.length > 0) {
          this.listProduct = listProduct.data;
        } else {
          this.backToTop();
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loading = false;
      }
    },
    // 画面のConfigデータを取得する
    async getConfig(bussinessCd) {
      try {
        var configScreen = await this.getConfigScreen(
          this.SCREEN_LIST,
          bussinessCd
        );
        if (configScreen != null && configScreen.length > 0) {
          this.imageTop = configScreen.filter(
            (x) => x.imagePosition === this.POSITION_TOP
          )[0];

          this.urlImageTop =
            this.imageTop != null
              ? "/Images/product-list/" + this.imageTop.imageUrl
              : "";

          this.lstImage = configScreen.filter(
            (x) => x.imagePosition === this.POSITION_BOTTOM
          );

          this.textNote = configScreen[0].textNortify1;
          this.isShowImage = configScreen[0].buttonFlg;
        }
      } catch (error) {
        console.log(error);
      }
    },
    // 商品詳細画面へのルーター
    routerToDetail(productCd) {
      localStorage.setItem("isRouteFromTop", false);
      this.$router.push({
        name: "product-detail",
        params: {
          productCd: productCd,
        },
      });
    },
    // TOP画面に戻る
    backToTop() {
      if (localStorage.getItem("routerTop") != null) {
        this.$router.push({ path: localStorage.getItem("routerTop") });
      } else {
        this.$router.push({ name: "top" });
      }
    },
  },
};
</script>

<style></style>
