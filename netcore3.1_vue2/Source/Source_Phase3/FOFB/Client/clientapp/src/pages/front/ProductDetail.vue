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
                    :key="image.ProductImgUrlId"
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
                    :key="image.ProductImgUrlId"
                  />
                </VueSlickCarousel>
              </div>
            </div>
          </div>
          <!-- Image Product details End -->

          <!-- Product details Start -->
          <div class="details-product">
            <div class="product-details-content">
              <p class="product-code">{{ this.productTitle }}</p>
              <br />
              <span class="product-name">{{ this.productName }}</span>
              <div class="product-price-grp">
                <!-- Product Price -->
                <div class="price">
                  <p>¥{{ this.formatPrice(this.price) + "(税込)" }}</p>
                </div>
                <!-- Message OutOfPrice -->
                <div hidden v-if="checkChange"></div>
                <span
                  class="invalid-feedback d-block"
                  hidden
                  v-if="isOutOfPrice"
                  >{{ msgOutOfPrice }}</span
                >
                <!-- Message QualityWrong -->
                <span class="invalid-feedback d-block" v-if="isQualityWrong">{{
                  msgQualityWrong
                }}</span>
              </div>
              <!-- List Product Order Start -->
              <div class="table-responsive pro-details-quality">
                <table class="table">
                  <tbody v-for="(product, index) in dataProduct" :key="index">
                    <tr>
                      <!-- Product Size -->
                      <td>
                        <p>{{ product.sizeName }}</p>
                      </td>
                      <!-- Inventory Label -->
                      <td>
                        <span>在庫</span>
                      </td>
                      <!-- Product Inventory  -->
                      <td v-if="product.inventoryNumber > 0">
                        <span v-if="product.inventoryNumber < inventoryNumCheck"
                          >△</span
                        >
                        <span v-else>〇</span>
                      </td>
                      <td v-else>
                        <span>✕</span>
                      </td>

                      <td
                        v-if="
                          product.inventoryNumber == 0 && product.quantity === 0
                        "
                      >
                        <button class="button-quantity">在庫なし</button>
                      </td>
                      <td v-else>
                        <div>
                          <b-form-select
                            class="product-quantity"
                            v-model="product.quantity"
                            :options="product.maxCountCanOrder"
                            @change="changeQuantity(product)"
                          ></b-form-select>
                        </div>
                      </td>
                      <!-- Color Product  -->
                      <td>
                        <b-form-select
                          v-if="product.lstColor.length > 1"
                          class="product-color"
                          v-model="product.colorName"
                          :options="product.lstColor"
                          @change="changeColorProduct(product)"
                        ></b-form-select>
                        <b-form-select
                          v-else
                          class="product-color select-as-text"
                          disabled
                          v-model="product.colorName"
                          :options="product.lstColor"
                        ></b-form-select>
                      </td>
                    </tr>
                    <tr
                      v-if="
                        product.messErr != undefined && product.messErr != ''
                      "
                    >
                      <td colspan="5">
                        <span class="invalid-feedback d-block">
                          {{ product.messErr }}
                        </span>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
              <!--  List Product Order End -->
              <!-- Notice Product -->
              <p class="note"><span>※数量指定ください。</span></p>
              <p class="note" v-if="dataProduct.length > 0">
                <span
                  >※一度に予約できる金額は{{
                    this.formatPrice(dataProduct[0].maxAmountCanOrder)
                  }}円までです。</span
                >
              </p>
              <p class="note" v-if="dataProduct.length > 0">
                <span
                  >※1サイズに対してお一人様{{
                    dataProduct[0].maxSizeCanOrder
                  }}点までしかご指定できません。</span
                >
              </p>
            </div>
            <div v-if="!isRouteFromTop" class="button-process">
              <!-- Button back to list Product -->
              <button
                @click="backProductList()"
                type="submit"
                class="btn btn-back-screen btn-back-to-list"
                style="margin: 8px"
              >
                <span class="arrow-button">←</span>
                {{ textBtnBackToLstProduct }}
              </button>

              <!-- Button back to list Product(Keep selected products)-->
              <button
                @click="backProductListKeepSelected()"
                type="submit"
                class="btn btn-back-screen btn-back-to-list"
                style="margin: 8px"
              >
                <span class="arrow-button">←</span>
                {{ textBtnBackToLstKeepProducts }}
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
          v-html="textInfoProduct"
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
          v-html="textInfoProductSub"
        ></div>
      </div>
    </section>
    <!-- Info Area End -->
    <div class="form-check">
      <div>
        <div class="button-confirm">
          <!-- Button Router Order Confirm -->
          <button
            @click="routeToOrder('order')"
            type="submit"
            class="btn btn-confirm"
            style="text-transform: none"
          >
            {{ textBtnRouterOrders }}
          </button>
        </div>
      </div>
      <div class="form-check-box row" style="margin-bottom: 0px">
        <!--Lable For Button Order-->
        <label class="form-check-label">{{ LabelBtnRouterOrders }}</label>
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
  data: () => ({
    className: "progress-bar-product-detail",
    loading: null,
    isOutOfPrice: false,
    isQualityWrong: false,
    productCd: null,
    productTitle: "",
    dataProduct: [],
    currentProducts: [],
    colorName: "",
    productName: "",
    price: "",
    isMaxCountCanOrder: false,
    lstImageProduct: null,
    isRouteFromTop: false,
    inventoryNumCheck: 0,
    checkChange: false,
    textBtnBackToLstProduct: "",
    textInfoProduct: "",
    textInfoProductSub: "",
    textBtnRouterOrders: "",
    LabelBtnRouterOrders: "",
    bussinessCd: "",
    textBtnBackToLstKeepProducts: "",
    checkBackFromConfirm: false,
  }),
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
      this.checkRouterTo();
      if (this.$route.params.backToProduct !== undefined) {
        this.dataProduct = JSON.parse(localStorage.getItem("dataProduct"));
        this.checkBackFromConfirm = true;
      }
      await this.getInventorySymbol();
      await this.chkTimeOpenShop();
      await this.getImageDetail();
      await this.getScreenText();
      await this.checkInventoryProduct();
      this.clearProduct();
    } else {
      this.$router.push({ name: "404" });
    }
  },
  mounted() {},
  methods: {
    // 商品一覧画面又はTOP画面へのルーターをチェックする
    checkRouterTo() {
      this.getProductCd();
      this.isRouteFromTop = localStorage.getItem("isRouteFromTop") === "true";
    },
    // 商品一覧画面に戻る
    backProductList() {
      this.$router.push({
        name: "product-list",
      });
    },
    // 商品一覧画面に戻る
    backProductListKeepSelected() {
      // this.setSelectedProducts();
      // this.$router.push({
      //   name: "product-list",
      //   params: {
      //     keepSelectedPrds: "keepSelectedPrds",
      //   },
      // });
      this.routeToOrder("product-list");
    },
    setSelectedProducts() {
      var selectedProducts = this.getSelectedProducts();
      if (selectedProducts !== null && this.currentProducts !== null) {
        var index = -1;
        this.currentProducts.forEach(function(item) {
          index = selectedProducts.findIndex(
            (x) =>
              x.productCd === item.productCd &&
              x.sizeCd == item.sizeCd &&
              x.colorCd == item.colorCd
          );
          if (index !== -1) {
            selectedProducts[index] = item;
          } else {
            selectedProducts.push(item);
          }
        });
      } else if (selectedProducts === null && this.currentProducts !== null) {
        selectedProducts = this.currentProducts;
      }
      sessionStorage.setItem(
        "selectedProducts",
        JSON.stringify(selectedProducts)
      );
    },
    getSelectedProducts() {
      return JSON.parse(sessionStorage.getItem("selectedProducts"));
    },
    // localStorageから商品コードを取得する
    getProductCd() {
      this.productCd = this.$route.params.productCd;
      if (this.productCd !== undefined) {
        localStorage.setItem("productCd", this.productCd);
      } else {
        this.productCd = localStorage.getItem("productCd");
      }
    },
    async getConfigScreenByProductCd(screenType, ProductCd) {
      const config = {
        method: "get",
        url: "api/FrontScreen/GetTextInfoByProduct",
        params: {
          screenType: screenType,
          ProductCd: ProductCd,
        },
      };
      try {
        const res = await require("axios")(config);
        return res.data;
      } catch (error) {
        console.log(error);
      }
    },
    // お客様が商品数量を変更する
    changeQuantity(product) {
      product.messErr = "";
      if (product.quantity > Number(product.inventoryNumber)) {
        product.messErr = this.msgQualityBiggerInventory;
      }
      this.isQualityWrong = false;
      this.isMaxCountCanOrder =
        product.quantity > Number(product.inventoryNumber);
      // inventoryNumberをチェックする
      this.checkChange = !this.checkChange;
      // outOfPriceをチェックする
      const totalPrice = this.dataProduct.reduce(
        (a, b) => a + b.price * b.quantity,
        0
      );
      this.isOutOfPrice = totalPrice > product.maxAmountCanOrder;
    },
    // お客様が商品色を変更する
    changeColorProduct(product) {
      product.messErr = "";
      // inventoryNumberを設定する
      product.inventoryNumber =
        product.lstInventoryNumber[this.getIndexProduct(product)];
      product.colorCd = product.lstColorCd[this.getIndexProduct(product)];
      // 数量をリセットする
      this.changeQuantity(product);
    },
    // 色一覧から商品順番を取得する
    getIndexProduct(product) {
      return product.lstColor.indexOf(product.colorName);
    },
    // 画面をローディングする時に商品をリセットする
    clearProduct() {
      if (this.$route.params.clear !== undefined) {
        localStorage.removeItem("dataOrder");
        localStorage.removeItem("shopInfo");
        sessionStorage.removeItem("selectedProducts");
      }
      this.dataProduct = this.dataProduct.map((x) => {
        x.quantity = 0;
        return x;
      });
    },
    // 予約画面にルーターする
    async routeToOrder(routeName) {
      if (routeName === "order") {
        if (this.checkQualityWrong()) {
          this.$vuetify.goTo("#top");
          return;
        }
      }
      if (!this.isMaxCountCanOrder && !this.isOutOfPrice) {
        var isInventoryProductErr = await this.checkInventoryProduct();
        if (isInventoryProductErr) {
          this.$vuetify.goTo("#top");
          return;
        }

        this.setSelectedProducts();
        var selectedProducts = this.getSelectedProducts();
        if (selectedProducts === null || selectedProducts === []) {
          this.isQualityWrong = true;
          this.$vuetify.goTo("#top");
          return;
        }
        switch (routeName) {
          case "order":
            this.$router.push({
              name: "order",
              params: {
                dataProduct: selectedProducts,
              },
            });
            break;
          case "product-list":
            this.$router.push({
              name: "product-list",
              params: {
                keepSelectedPrds: "keepSelectedPrds",
              },
            });
            break;
          default:
            break;
        }
      }
    },
    // 在庫チェック
    async checkInventoryProduct() {
      this.checkChange = !this.checkChange;
      let isInventoryProductErr = false;

      this.currentProducts = this.dataProduct.filter((x) => x.quantity > 0);
      await this.getListProduct();

      if (this.dataProduct !== null && this.currentProducts !== null) {
        var index = -1;
        var indexProduct = -1;

        for (var i = 0; i < this.currentProducts.length; i++) {
          index = this.dataProduct.findIndex(
            (x) =>
              x.productCd == this.currentProducts[i].productCd &&
              x.sizeCd == this.currentProducts[i].sizeCd &&
              x.colorCd == this.currentProducts[i].colorCd
          );

          indexProduct = this.getIndexProduct(this.currentProducts[i]);

          if (index != -1) {
            if (this.dataProduct[index].lstInventoryNumber[indexProduct] == 0) {
              this.dataProduct[index].messErr = this.msgProductOutOfStock;
              this.dataProduct[index].quantity = 0;

              isInventoryProductErr = true;
            } else if (
              this.currentProducts[i].quantity >
              this.dataProduct[index].lstInventoryNumber[indexProduct]
            ) {
              this.dataProduct[index].messErr = this.msgQualityBiggerInventory;
              this.dataProduct[index].quantity = this.currentProducts[
                i
              ].quantity;

              isInventoryProductErr = true;
            } else {
              this.dataProduct[index].quantity = this.currentProducts[
                i
              ].quantity;
            }
          }
        }
      }

      if (isInventoryProductErr) {
        this.checkChange = !this.checkChange;
        this.currentProducts = [];
      }
      if (this.checkBackFromConfirm) {
        sessionStorage.removeItem("selectedProducts");
      }
      return isInventoryProductErr;
    },
    // 数量を選択するかどうかチェックする
    checkQualityWrong() {
      if (this.dataProduct.filter((x) => x.quantity > 0).length === 0) {
        this.isQualityWrong = true;
        return this.isQualityWrong;
      }
    },
    // 画面のConfigテキストデータを取得する
    async getScreenText() {
      try {
        const resConfig = await this.getConfigScreenByProductCd(
          this.SCREEN_DETAIL,
          this.productCd
        );
        if (resConfig !== null) {
          this.textBtnBackToLstProduct = resConfig[0].textButton2;
          this.textInfoProduct = resConfig[0].textArea1;
          this.textInfoProductSub = resConfig[0].textArea2;
          this.textBtnRouterOrders = resConfig[0].textButton1;
          this.LabelBtnRouterOrders = resConfig[0].textNortify2;
          this.textBtnBackToLstKeepProducts = resConfig[0].textTitle2;
        }
      } catch (error) {
        console.log(error);
      }
    },
    // 商品画像を取得する
    async getImageDetail() {
      const config = {
        method: "get",
        url: "api/MProduct/GetImageProduct",
        params: {
          productCd: this.productCd,
        },
      };
      try {
        const lstImageDetail = await require("axios")(config);
        if (lstImageDetail != null) {
          this.lstImageProduct = lstImageDetail.data.sort((a, b) =>
            a.isMainInDetailPage < b.isMainInDetailPage
              ? 1
              : b.isMainInDetailPage < a.isMainInDetailPage
              ? -1
              : 0
          );
        }
      } catch (error) {
        console.log(error);
      }
    },
    // 商品詳細一覧を取得する
    async getListProduct() {
      const config = {
        method: "get",
        url: "api/MProduct/GetlistProductForScreenDetail",
        params: {
          productCd: this.productCd,
        },
      };
      try {
        const lstProduct = await require("axios")(config);
        if (lstProduct.data !== null) {
          let brandInfo = {
            brandCd: lstProduct.data[0].brandCd,
            brandName: lstProduct.data[0].brandName,
          };
          localStorage.setItem("brandInfo", JSON.stringify(brandInfo));
          this.dataProduct = lstProduct.data;
          this.upDateDataProduct();
          this.showProductToScreen();
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loading = false;
      }
    },
    // 商品詳細を表示する
    showProductToScreen() {
      this.productTitle =
        this.dataProduct[0].brandName + " " + this.dataProduct[0].productCd;
      this.colorName = this.dataProduct[0].colorName;
      this.price = this.dataProduct[0].price;
      this.productName = this.dataProduct[0].productName;
    },
    // 画面に商品データを更新する
    upDateDataProduct() {
      this.dataProduct.forEach((product) => {
        // maxCountCanOrderを追加する
        product.maxCountCanOrder = [
          ...Array(product.maxSizeCanOrder + 1).keys(),
        ];
        // 色一覧と在庫数を追加する
        product.lstColor = [];
        product.lstInventoryNumber = [];
        product.inventoryNumber = product.inventoryNumberSub;
        // 1サイズの複数色をチェックする
        if (product.colorName.includes(",")) {
          product.lstColorCd = product.colorCd.split(",");
          product.colorCd = product.colorCd.split(",")[0];
          product.lstColor = product.colorName.split(",");
          product.colorName = product.colorName.split(",")[0];
          product.lstInventoryNumber = product.inventoryNumberSub.split(",");
          product.inventoryNumber = product.inventoryNumberSub.split(",")[0];
        } else {
          product.lstColor.push(product.colorName);
          product.lstInventoryNumber.push(product.inventoryNumberSub);
        }
      });
    },
    // 商品の在庫マークを取得する
    async getInventorySymbol() {
      const config = {
        method: "get",
        url: "api/InventorySymbol/GetInventorySymbol",
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      try {
        const inventorySymbol = await require("axios")(config);
        if (inventorySymbol.data !== null) {
          this.inventoryNumCheck = Number(
            inventorySymbol.data[0].inventoryNumCheck
          );
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style lang="scss" scoped></style>
