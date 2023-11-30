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
    <div class="title-order-confirm">
      <div class="container">
        <h5 class="title-detail">{{ pageTitle }}</h5>
        <p class="title-note">
            ※予約確定しますとサイズ交換・キャンセルは一切お受けできません。
        </p>
      </div>
    </div>
    <!-- Title End -->
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
        <div class="button-process">
          <button
            @click="backProductDetail()"
            type="submit"
            class="btn btn-back-screen"
          >
            <span class="arrow-button">←</span>
            商品詳細画面に戻って内容を修正する
          </button>
          <p class="note">【注意】戻るとご入力内容がリセットされます！</p>
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
    <div class="discount-area" v-if="this.paymentWay !== null">
      <div class="container">
        <div class="title-group">
          <h5 class="title-discount">■決済方法</h5>
        </div>
        <div class="discount-group">
          <span>{{ this.paymentWayText }}</span>
        </div>
      </div>
    </div>

    <!-- Store Area Start -->
    <div class="store-area" v-if="shopInfo !== null">
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
    <custInfo :dataCustInfo="this.custInfo"></custInfo>
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
    <!-- Note And Button Area Start -->
    <div class="button-area">
      <div class="container">
        <div class="note">
          <p v-html="mailInputNortity"></p>
        </div>
        <div class="button-back">
          <button @click="backOrder()" type="submit" class="btn btn-back">
            <span class="arrow-button">←</span> {{ textBtnBackPrevPage }}
            <!-- 前の画面に戻って内容を修正する -->
          </button>
        </div>
        <div class="button-confirm">
          <button @click="submitForm()" type="submit" class="btn btn-confirm">
            {{ textBtnConfirmOrder }}
            <!-- ご予約内容を確定して送信する -->
          </button>
        </div>
      </div>
    </div>
    <!-- Note And Button Area End -->
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
      className: "progress-bar-order-confirm",

      loading: null,
      dataOrder: [],
      dataProduct: [],
      quantityTotal: 0,
      amountTotal: 0,
      shopInfo: null,
      pageTitle: "",
      mailInputNortity: "",
      textBtnBackPrevPage: "",
      textBtnConfirmOrder: "",
      custInfo: {},
      custRecieveInfo: {},
      receiveWay: "",
      paymentWay: null,
      bussinessCd: "",
      isDiscount: false,
      brandInfo: "",
      recieveWayText: "",
      paymentWayText: "",
      bussinessName: "",
    };
  },
  async created() {
    this.loading = true;
    this.bussinessCd = localStorage.getItem("bussinessCd");
    this.bussinessName = localStorage.getItem("bussinessName");
    if (this.bussinessCd != null && this.bussinessCd != undefined) {
      const isValidOrdDTmRespone = await this.chkTimeOpenShop(this.bussinessCd);
      if (isValidOrdDTmRespone) {
        this.checkEndOrder();
      } else {
        this.$router.push({ name: "404" });
      }
      await this.getConfig();
      this.getDataFromLocalStorage();
    } else {
      this.$router.push({ name: "404" });
    }
  },
  methods: {
    // localStorageからデータを取得・チェックする
    getDataFromLocalStorage() {
      // 予約のパラメーターをチェックする
      if (
        this.$route.params.custInfo !== undefined &&
        this.$route.params.custRecieveInfo !== undefined &&
        this.$route.params.receiveWay !== undefined &&
        this.$route.params.isDiscount !== undefined
      ) {
        this.custInfo = this.$route.params.custInfo;
        this.custRecieveInfo = this.$route.params.custRecieveInfo;
        this.receiveWay = this.$route.params.receiveWay;
        this.isDiscount = this.$route.params.isDiscount;
        // localStorageに設定する
        let dataOrder = null;

        if (
          this.$route.params.paymentWay !== undefined &&
          this.$route.params.paymentWay !== null
        ) {
          this.paymentWay = this.$route.params.paymentWay;
          this.paymentWayText = this.paymentWayOptions.find(
            (x) => x.value == this.paymentWay
          ).text;
          dataOrder = {
            custInfo: this.$route.params.custInfo,
            custRecieveInfo: this.$route.params.custRecieveInfo,
            receiveWay: this.$route.params.receiveWay,
            paymentWay: this.paymentWay,
            isDiscount: this.$route.params.isDiscount,
            isAddressOther: this.$route.params.isAddressOther,
          };
        } else {
          dataOrder = {
            custInfo: this.$route.params.custInfo,
            custRecieveInfo: this.$route.params.custRecieveInfo,
            receiveWay: this.$route.params.receiveWay,
            isDiscount: this.$route.params.isDiscount,
            isAddressOther: this.$route.params.isAddressOther,
          };
        }
        localStorage.setItem("dataOrder", JSON.stringify(dataOrder));
      } else if (localStorage.getItem("dataOrder") != null) {
        this.custInfo = JSON.parse(localStorage.getItem("dataOrder")).custInfo;
        this.custRecieveInfo = JSON.parse(
          localStorage.getItem("dataOrder")
        ).custRecieveInfo;
        this.receiveWay = JSON.parse(
          localStorage.getItem("dataOrder")
        ).receiveWay;
        this.isDiscount = JSON.parse(
          localStorage.getItem("dataOrder")
        ).isDiscount;
      }
      if (
        localStorage.getItem("dataProduct") === null ||
        localStorage.getItem("dataOrder") === null ||
        localStorage.getItem("brandInfo") === null
      ) {
        if (localStorage.getItem("routerTop") != null) {
          this.$router.push({ path: localStorage.getItem("routerTop") });
        } else {
          this.$router.push({ name: "404" });
        }
      } else {
        if (this.receiveWay !== null) {
          this.recieveWayText = this.receiveWayOptions.find(
            (x) => x.value == this.receiveWay
          ).text;
        }

        this.dataProduct = JSON.parse(localStorage.getItem("dataProduct"));

        this.brandInfo = JSON.parse(localStorage.getItem("brandInfo"));

        this.caculateProduct(this.dataProduct);
      }
    },
    // 画面のConfigデータを取得する
    async getConfig() {
      if (localStorage.getItem("shopInfo") !== null) {
        this.shopInfo = JSON.parse(localStorage.getItem("shopInfo"));
      }
      try {
        const resConfig = await this.getConfigScreen(
          this.SCREEN_ORDER_CONFIRM,
          this.bussinessCd
        );
        if (resConfig !== null) {
          this.pageTitle = resConfig[0].textTitle1;
          this.mailInputNortity = resConfig[0].textNortify1;
          this.textBtnBackPrevPage = resConfig[0].textButton1;
          this.textBtnConfirmOrder = resConfig[0].textButton2;
        }
      } catch (error) {
        console.log(error);
      }
    },
    // 商品に対してSKUを追加する
    caculateProduct(lstProductOrder) {
      for (var i = 0; i < lstProductOrder.length; i++) {
        lstProductOrder[i].subTotal =
          lstProductOrder[i].price * lstProductOrder[i].quantity;
        this.quantityTotal += lstProductOrder[i].quantity;
        this.amountTotal += lstProductOrder[i].subTotal;
        // 商品に複数色がある
        if (lstProductOrder[i].colorCd.includes(",")) {
          lstProductOrder[i].colorCd = lstProductOrder[i].colorCd.split(",")[
            lstProductOrder[i].lstColor.indexOf(lstProductOrder[i].colorName)
          ];
        }
        lstProductOrder[i].sku =
          lstProductOrder[i].productCd +
          lstProductOrder[i].colorCd +
          lstProductOrder[i].sizeCd;
      }

      this.dataOrder = lstProductOrder;
      this.loading = false;
    },
    // 予約をInsertする
    async submitForm() {
      this.loading = true;
      const dataOrder = {
        status: this.RSV_STATUS_ORDER,
        shopCd: this.shopInfo == null ? null : this.shopInfo.shopCd,
        total: this.amountTotal,
        totalQuantity: this.quantityTotal,
        receiveWay: this.receiveWay,
        paymentWay: this.paymentWay,
        isDiscount: this.isDiscount,
        bussinessCd: this.bussinessCd,
        bussinessName: this.bussinessName,
        brandCd: this.brandInfo.brandCd,
        brandname: this.brandInfo.brandName,
        shopName: this.shopInfo == null ? null : this.shopInfo.shopName,
      };
      const dataOrderSub = {
        dataOrder: dataOrder,
        dataProduct: this.dataProduct,
        custInfo: this.custInfo,
        custRecieveInfo: this.custRecieveInfo,
      };
      const config = {
        method: "post",
        url: "/api/Order/InsertOrders",
        data: dataOrderSub,
      };
      const res = await require("axios")(config);
      try {
        if (res.data !== null) {
          if (res.data.result !== this.ADD_PRODUCT_FAIL) {
            // 在庫不足
            if (res.data.result === this.PRODUCT_OUT_QUANTITY) {
              this.$router.push({
                name: "product-detail",
                params: {
                  backToProduct: "backToProduct",
                },
              });
            } else if (
              res.data.result !== this.ADD_PRODUCT_FAIL &&
              res.data.result !== null &&
              res.data.barCode !== null
            ) {
              localStorage.setItem(
                "barCode",
                JSON.stringify(res.data.barCode.substring(1, 13))
              );
              this.dataProduct[0].ordersId = res.data.ordersId;
              this.$router.push({
                name: "order-finish",
              });
            }
          } else {
            this.caculateProduct(this.dataProduct);
          }
        } else {
          this.caculateProduct(this.dataProduct);
        }
      } catch (error) {
        console.log(error);
      }

      this.loading = false;
    },
    // 商品詳細画面に戻る
    backProductDetail() {
      this.$router.push({
        name: "product-detail",
        params: {
          clear: "clear",
        },
      });
      localStorage.removeItem("dataProduct");
    },
    // 予約画面に戻る
    backOrder() {
      this.$router.push({
        name: "order",
      });
    },
  },
};
</script>
<style></style>
