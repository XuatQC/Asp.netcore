<template>
  <div>
    <VueElementLoading
      :active="loading"
      spinner="ring"
      color="var(--success)"
    />
    <!-- Header Start -->
    <HeaderPage :className="className"></HeaderPage>
    <!-- Header End -->
    <!-- Title Start -->
    <div class="title-order-finish">
      <div class="container">
        <!-- Text for title page order finish -->
        <h5 class="title-detail">{{ pageTitle }}</h5>
        <!-- Text for note of title page order finish -->
        <div class="note-group" v-html="pageTitleNote"></div>
      </div>
    </div>
    <!-- Title End -->
    <!-- BarCode Start -->
    <div class="bar-code">
      <h5 class="title-bar-code">■ご予約受付番号</h5>
      <hr class="line-bottom-title" />
      <div class="content-bar-code">
        <div class="image-bar-code">
          <img class="d-block w-100" :src="urlImageBarCode" />
        </div>
      </div>
    </div>
    <!-- BarCode End -->
    <!-- Cart Area Start -->
    <div class="cart-area">
      <div class="container">
        <h5 class="title-cart">■ご予約内容</h5>
        <div class="data-cart">
          <cart
            :dataCart="this.dataOrder"
            :quantityTotal="quantityTotal"
            :amountTotal="amountTotal"
          ></cart>
        </div>
      </div>
    </div>
    <!-- Cart Area End -->
    <!--商品受け取り方法-->
    <div class="discount-area" v-if="this.receiveWay !== null">
      <div class="container">
        <div class="title-group">
          <h5 class="title-discount">■商品受け取り方法</h5>
        </div>
        <div class="discount-group">
          <span>{{ this.recieveWayText }}</span>
        </div>
      </div>
    </div>
    <!--決済方法-->
    <div
      class="discount-area"
      style="margin-top: -7px !important"
      v-if="this.paymentWay !== null && this.paymentWay !== undefined"
    >
      <div class="container">
        <div class="title-group">
          <h5 class="title-discount">■決済方法</h5>
        </div>
        <div class="discount-group">
          <span>{{ this.paymentWayText }}</span>
        </div>
      </div>
    </div>
    <div
      class="customer-info-confirm-area"
      v-if="bankInfo !== null && paymentWay === this.TRANSFER"
      style="margin-top: 0px !important; margin-bottom: 7px !important "
    >
      <div class="container">
        <h3 class="title-cust-info-confirm">■振込先口座情報</h3>
        <div class="cust-info-confirm-detail">
          <div class="row cust-info-confirm">
            <div class="title">
              <label>金融機関名</label>
            </div>
            <div class="cust-info-value">
              <span>{{ bankInfo.bankName }}</span>
            </div>
          </div>
          <hr class="line-info-confirm" />
          <div class="row cust-info-confirm">
            <div class="title">
              <label>口座番号</label>
            </div>
            <div class="cust-info-value">
              <span>{{ bankInfo.accountNumber }}</span>
            </div>
          </div>
          <hr class="line-info-confirm" />
          <div class="row cust-info-confirm">
            <div class="title">
              <label>口座名義</label>
            </div>
            <div class="cust-info-value">
              <span>{{ bankInfo.accountName }}</span>
            </div>
          </div>
          <hr class="line-info-confirm" />
          <div class="row cust-info-confirm">
            <div class="title">
              <label>支店名</label>
            </div>
            <div class="cust-info-value">
              <span>{{ bankInfo.branch }}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Store Area Start -->
    <div class="store-area" v-if="isShowShopArea">
      <div class="container">
        <h3 class="title-store">■ご入金・お受取 店舗</h3>
        <hr class="line-bottom-store" />
        <div class="store-content">
          <label>{{ shopInfo.shopName }}</label>
        </div>
      </div>
    </div>
    <!-- Store Area End -->
    <!-- Customer Info Confirm Area Start -->
    <custInfo :dataCustInfo="this.custInfo" />
    <!-- Customer Info Confirm Area End -->
    <!-- Delivery Recieve Info Confirm Area Start -->
    <custRecieveInfo
      :custRecieveInfo="this.custRecieveInfo"
      v-if="Object.keys(this.custRecieveInfo).length !== 0"
    />
    <!-- Delivery Recieve Info ConfirmArea End -->
    <div class="discount-area" v-if="this.isDiscount !== null">
      <div class="container">
        <div class="title-group">
          <h5 class="title-discount">■早期割引特典</h5>
        </div>
        <div class="discount-group">
          <span>{{ isDiscount ? this.HAVE_DISCOUNT : this.NOT_DISCOUNT }}</span>
        </div>
      </div>
    </div>
    <!-- Payment Time Area Start -->
    <div class="payment-time-area">
      <div class="container">
        <!-- Text for title payment time -->
        <h3 class="title-payment-time">■{{ pageTitlePaymentTime }}</h3>
        <!-- Text for content of payment time -->
        <div class="payment-time-content" v-html="pageContentPaymentTime"></div>
      </div>
    </div>
    <!-- Payment Time Area End -->
    <!-- Notice Q&A Area Start -->
    <div class="notice-area">
      <div class="container">
        <!-- Text for title notice -->
        <h3 class="title-notice">■{{ textTitleNotice }}</h3>
      </div>
    </div>
    <!-- Static Area Start -->
    <section class="part-rules notice-content">
      <div class="container">
        <!-- Text for content of notice -->
        <div
          class="static-area-wrap scrollingDiv scrollbar-ripe-malinka"
          v-html="textContentNotice"
        ></div>
      </div>
    </section>
    <!-- Static Area End -->
    <!-- Notice Q&A Time Area End -->
    <!-- Button Group Area Start -->
    <div class="button-finish-area">
      <div class="container">
        <div class="button-top">
          <a class="page-top" href="#">画面上部に戻る</a>
        </div>
        <div class="button-finish">
          <!-- Text for button back to Top -->
          <button @click="routerToTop()" type="submit" class="btn btn-finish">
            {{ textBtnBackToTop }}
          </button>
        </div>
        <!-- Text for content button back to Top -->
        <span class="note">{{ textNoteBtnBackToTop }}</span>
      </div>
    </div>
    <!-- Button Group Area End -->
  </div>
</template>

<script>
import cart from "@/components/DataCommon/Cart.vue";
import custInfo from "@/components/DataCommon/CustInfo.vue";
import custRecieveInfo from "@/components/DataCommon/CustRecieveInfo.vue";
import HeaderPage from "@/components/DataCommon/HeaderPage.vue";

export default {
  components: {
    cart,
    custInfo,
    custRecieveInfo,
    HeaderPage,
  },
  data() {
    return {
      className: "progress-bar-order-finish",
      loading: false,
      bussinessCd: "",
      textContent: {},
      routerTop: "",
      dataOrder: [],
      dataProduct: [],
      custInfo: {},
      custRecieveInfo: {},
      quantityTotal: 0,
      amountTotal: 0,
      isShowShopArea: false,
      shopInfo: {},
      bankInfo: {},
      barCode: "",
      urlImageBarCode: "",
      isDiscount: false,
      receiveWay: "",
      recieveWayText: "",
      paymentWay: null,
      paymentWayText: "",
      pageTitle: "",
      pageTitleNote: "",
      pageTitlePaymentTime: "",
      pageContentPaymentTime: "",
      textTitleNotice: "",
      textContentNotice: "",
      textBtnBackToTop: "",
      textNoteBtnBackToTop: "",
    };
  },
  async created() {
    this.loading = true;
    this.bussinessCd = localStorage.getItem("bussinessCd");
    if (this.bussinessCd != null && this.bussinessCd != undefined) {
      const isValidOrdDTmRespone = await this.chkTimeOpenShop(this.bussinessCd);
      if (isValidOrdDTmRespone) {
        this.checkFinish();
      } else {
        this.$router.push({ name: "404" });
      }

      // 画面のConfigデータを取得する
      await this.getConfig(this.bussinessCd);

      // ページでルータを設定する
      this.setRouterTop();

      // localStorageから商品データを取得する
      this.getDataProduct();

      // localStorageから予約データを取得する
      this.getDataOrder();

      // localStorageからshopInfoデータを取得する
      this.getShopInfo();

      // バーコード画像を取得する
      this.getUrlImageBarCode();

      // 注文データ処理
      this.listDataOrder(this.dataProduct);

      // 予約完了をチェックする
      localStorage.setItem("orderFinish", JSON.stringify("orderFinish"));

      this.$vuetify.goTo("#top");
    } else {
      this.$router.push({ name: "404" });
    }

    this.loading = false;
  },
  methods: {
    // 予約終了をチェックする
    checkFinish() {
      if (localStorage.getItem("orderFinish") === null) {
        this.checkEndOrder();
      }
    },
    // 画面のConfigデータを取得する
    async getConfig(bussinessCd) {
      try {
        var configScreen = await this.getConfigScreen(
          this.SCREEN_ORDER_FINISH,
          bussinessCd
        );

        if (configScreen != null && configScreen.length > 0) {
          this.textContent = configScreen.filter(
            (x) => x.dspType === this.CONTENT_TEXT
          )[0];
          this.pageTitle = this.textContent.textTitle1;
          this.pageTitleNote = this.textContent.textArea1;
          this.pageTitlePaymentTime = this.textContent.textTitle2;
          this.pageContentPaymentTime = this.textContent.textNortify1;
          this.textTitleNotice = this.textContent.textTitle3;
          this.textContentNotice = this.textContent.textNortify2;
          this.textBtnBackToTop = this.textContent.textButton1;
          this.textNoteBtnBackToTop = this.textContent.textButton2;
        }
      } catch (error) {
        console.log(error);
      }
    },
    // 注文商品一覧
    listDataOrder(lstProductOrder) {
      for (var i = 0; i < lstProductOrder.length; i++) {
        lstProductOrder[i].subTotal =
          lstProductOrder[i].price * lstProductOrder[i].quantity;
        this.quantityTotal += lstProductOrder[i].quantity;
        this.amountTotal += lstProductOrder[i].subTotal;
      }
      this.dataOrder = lstProductOrder;
    },
    // localStorageから予約データを取得する
    getDataOrder() {
      if (localStorage.getItem("dataOrder") != null) {
        // パラメーター又はlocalStorageからお客様データを取得する
        this.getCustInfo();

        // 商品受取情報を取得する
        this.getCustRecieveInfo();

        // disCountを取得する
        this.getDisCountInfo();

        // receiveWayを取得する
        this.getRecieveWay();

        // paymentWayを取得する
        this.getPaymentWay();
      } else {
        this.$router.push({ name: "top" });
      }
    },
    // 商品データを取得する
    getDataProduct() {
      if (localStorage.getItem("dataProduct") != null) {
        this.dataProduct = JSON.parse(localStorage.getItem("dataProduct"));
      } else {
        this.$router.push({ name: "top" });
      }
    },
    // TOPへのルーターを設定する
    setRouterTop() {
      if (localStorage.getItem("routerTop") != null) {
        this.routerTop = localStorage.getItem("routerTop");
      } else {
        this.$router.push({ name: "top" });
      }
    },
    // お客様情報を取得する
    getCustInfo() {
      let custInfo = JSON.parse(localStorage.getItem("dataOrder")).custInfo;
      if (Object.keys(custInfo).length !== 0) {
        this.custInfo = custInfo;
      } else {
        this.$router.push({ name: "top" });
      }
    },
    // お客様の商品受取情報を取得する
    getCustRecieveInfo() {
      let custRecieveInfo = JSON.parse(localStorage.getItem("dataOrder"))
        .custRecieveInfo;
      if (Object.keys(custRecieveInfo).length !== 0) {
        this.custRecieveInfo = custRecieveInfo;
      }
    },
    // disCountを取得する
    getDisCountInfo() {
      this.isDiscount = JSON.parse(
        localStorage.getItem("dataOrder")
      ).isDiscount;
    },
    // receiveWayを取得する
    getRecieveWay() {
      this.receiveWay = JSON.parse(
        localStorage.getItem("dataOrder")
      ).receiveWay;
      if (this.receiveWay !== null && this.paymentWay !== undefined) {
        this.recieveWayText = this.receiveWayOptions.find(
          (x) => x.value == this.receiveWay
        ).text;
      }
    },
    // paymentWayを取得する
    getPaymentWay() {
      this.paymentWay = JSON.parse(
        localStorage.getItem("dataOrder")
      ).paymentWay;
      if (this.paymentWay !== null && this.paymentWay !== undefined) {
        this.paymentWayText = this.paymentWayOptions.find(
          (x) => x.value == this.paymentWay
        ).text;
        if (this.paymentWay === this.TRANSFER) {
          this.getBankInfo();
        }
      }
    },
    // 店舗情報を取得する
    getShopInfo() {
      if (localStorage.getItem("shopInfo") != null) {
        this.shopInfo = JSON.parse(localStorage.getItem("shopInfo"));
        this.isShowShopArea = true;
      }
    },
    // バーコード画像URLを取得する
    getUrlImageBarCode() {
      let bussinessName = localStorage.getItem("bussinessName").toLowerCase();
      this.barCode = JSON.parse(localStorage.getItem("barCode")).substring(
        0,
        JSON.parse(localStorage.getItem("barCode")).length - 1
      );
      this.urlImageBarCode =
        "/Images/bar-code/" + bussinessName + "/" + this.barCode + ".jpg";
    },
    // 金融機関情報
    async getBankInfo() {
      const config = {
        method: "get",
        url: "api/Mbank/GetListMbank",
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      try {
        const resMbank = await require("axios")(config);
        if (resMbank.data !== null && resMbank.data.length > 0) {
          this.bankInfo = resMbank.data[0];
        } else {
          this.$toastr.e(this.msgNoData);
        }
      } catch (error) {
        console.log(error);
      }
    },
    // TOP画面に戻る
    routerToTop() {
      localStorage.clear();
      this.clearSelectedProduct();
      this.$router.push("/" + this.routerTop);
    },
  },
};
</script>
