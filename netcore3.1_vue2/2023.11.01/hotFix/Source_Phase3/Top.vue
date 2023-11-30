<template>
  <div>
    <VueElementLoading
      :active="loadding"
      spinner="ring"
      color="var(--success)"
    />
    <!-- Slider Arae Start -->
    <b-carousel id="carousel-fade" indicators>
      <b-carousel-slide
        v-for="image in this.lstImage"
        :key="image.scrImgUrlId"
        :img-src="IMAGE_TOP_PATH + image.imageUrl"
      ></b-carousel-slide>
    </b-carousel>

    <!-- Slider Arae End -->
    <!-- title Start -->
    <div class="title-shop">
      <h5 class="title_text">{{ pageTitle }}</h5>
    </div>
    <!-- title End -->
    <!-- Img Step Start-->
    <div class="step_book">
      <img class="img_mid" :src="IMAGE_TOP_PATH + urlImgMid" />
    </div>
    <!-- Img Step End -->

    <!-- Static Area Start -->
    <section class="part-rules static-area">
      <div class="container">
        <div
          class="static-area-wrap scrollingDiv scrollbar-ripe-malinka"
          @scroll="onScroll"
          v-html="textNotice"
        ></div>
      </div>
    </section>
    <!-- Static Area End -->
    <!-- Brand area start -->
    <div class="form-check" v-if="isShowButton">
      <div class="form-check-box row" :hidden="!isHiddenChecBox">
        <input
          type="checkbox"
          id="chkOpenShop"
          v-model="chbEffective"
          @change="chkEffective()"
        />
        <label class="form-check-label" for="chkOpenShop"
          >上記確認しました</label
        >
      </div>
      <div :hidden="!isHiddenBtn">
        <button
          type="submit"
          class="btn btn-submit btn-Agree"
          :disabled="!disabledBtn"
          @click="goNextPage()"
        >
          {{ textButtonNextPage }}
        </button>
      </div>
    </div>
    <!-- Brand area end -->
  </div>
</template>

<script>
export default {
  data() {
    return {
      loadding: false,
      chbEffective: false,
      isChkScroll: false,
      isChkCheckbox: false,
      isShowButton: false,
      disabledBtn: false,
      imageContentMid: {},
      urlImgMid: "",
      lstImage: null,
      productCd: null,
      bussinessCd: null,
      pageTitle: "",
      textNotice: "",
      isHiddenChecBox: "",
      isHiddenBtn: "",
      textButtonNextPage: "",
    };
  },
  async created() {
    this.loadding = true;
    // 予約終了をチェックする
    if (localStorage.getItem("orderFinish") !== null) {
      localStorage.clear();
    }
    await this.checkRouter();
    await this.getConfig();
    await this.getBussinessName();
    await this.checkTimeOpenShop();
    this.loadding = false;
  },
  methods: {
    // 画面のConfigデータを取得する
    async getConfig() {
      try {
        const resConfig = await this.getConfigScreen(
          this.SCREEN_TOP,
          this.bussinessCd
        );
        if (resConfig !== null) {
          this.pageTitle = resConfig[0].textTitle1;
          this.textNotice = resConfig[0].textArea1;
          this.isHiddenChecBox = resConfig[0].checkBoxFlg1;
          this.isHiddenBtn = resConfig[0].buttonFlg;
          this.textButtonNextPage = resConfig[0].textButton1;
          this.imageContentMid = resConfig.filter(
            (x) => x.imagePosition === this.POSITION_MID
          )[0];
          this.lstImage = resConfig.filter(
            (x) => x.imagePosition === this.POSITION_TOP
          );
          this.urlImgMid = this.imageContentMid.imageUrl;
        }
      } catch (error) {
        console.log(error);
      }
    },
    //業態名を取得する
    async getBussinessName() {
      try {
        const config = {
          method: "get",
          url: "api/MBussiness/GetAllByBussinessCd",
          params: {
            bussinessCd: this.bussinessCd,
          },
        };
        const getListBussinessRespone = await require("axios")(config);
        let currentBussinessName = getListBussinessRespone.data[0].bussinessName.toLowerCase();
        localStorage.setItem("bussinessName", currentBussinessName);
      } catch (error) {
        console.log(error);
      }
    },
    // ルーターをチェックする
    async checkRouter() {
      const config = {
        method: "get",
        url: "api/Urlsetting/GetAll",
        params: {
          url: this.$route.params.businessUrl,
        },
      };
      try {
        const resUrl = await require("axios")(config);
        if (Object.keys(resUrl.data).length > 0) {
          this.bussinessCd = resUrl.data.find(
            (x) => x.url === this.$route.params.businessUrl
          ).bussinessCd;
          localStorage.setItem("bussinessCd", this.bussinessCd);
          localStorage.setItem("routerTop", this.$route.params.businessUrl);
        } else {
          this.$router.push({
            name: "404",
          });
        }
      } catch (error) {
        console.log(error);
      }
    },
    async checkTimeOpenShop() {
      try {
        const isValidOrdDTmRespone = await this.chkTimeOpenShop(
          this.bussinessCd
        );
        if (isValidOrdDTmRespone) {
          this.isShowButton = true;
        }
      } catch (error) {
        console.log(error);
      }
    },
    // 有効期限チェック
    async chkEffective() {
      if (this.chbEffective) {
        this.disabledBtn = true;
      }
       else {
        this.disabledBtn = false;
      }
    },
    // スクロールするチェック
    onScroll({ target: { scrollTop, clientHeight, scrollHeight } }) {
      if (scrollTop + clientHeight >= scrollHeight) {
        this.isChkScroll = true;
        this.isChkCheckbox = true;
      }
    },
    // 商品詳細画面へのルーター
    async goNextPage() {
      let routerName = "product-detail";
      // 1オブジェクトから配列をグループする
      let groupBy = (array, key) => {
        return array.reduce((result, obj) => {
          (result[obj[key]] = result[obj[key]] || []).push(obj);
          return result;
        }, {});
      };
      let lstProductCd = await this.getProductCd();
      if (
        lstProductCd != null &&
        Object.keys(groupBy(lstProductCd, "productCd")).length > 1
      ) {
        routerName = "product-list";
      }

      localStorage.setItem("isRouteFromTop", true);
      this.$router.push({
        name: routerName,
        params: {
          productCd: this.productCd,
        },
      });
    },
    // 商品コード一覧を取得する
    async getProductCd() {
      if (this.isShowButton) {
        localStorage.setItem("openShop", JSON.stringify("openShop"));
        const config = {
          method: "get",
          url: "api/MProduct/GetProductCd",
          params: {
            bussinessCd: this.bussinessCd,
          },
        };
        try {
          const productCd = await require("axios")(config);
          if (productCd.data !== null) {
            this.productCd = productCd.data[0].productCd;
            return productCd.data;
          }
        } catch (error) {
          console.log(error);
        }
      }
    },
  },
};
</script>

<style scoped></style>
