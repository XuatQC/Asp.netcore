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
        <!-- Page Title -->
        <h5 class="title-detail">{{ textContent.textTitle1 }}</h5>
        <!-- Page Title Note -->
        <div class="note-group" v-html="textContent.textArea1"></div>
      </div>
    </div>
    <!-- Title End -->
    <!-- BarCode Start -->
    <div class="bar-code">
      <h5 class="title-bar-code">■ご予約受付番号</h5>
      <hr class="line-bottom-title" />
      <div class="content-bar-code">
        <div class="number-bar-code"></div>
        <div class="image-bar-code">
          <img height="120px" :src="IMAGE_BAR_CODE_BASE_PATH" alt="" />
        </div>
      </div>
    </div>
    <!-- BarCode End -->
    <!-- Cart Area Start -->
    <div class="cart-area">
      <div class="container">
        <div class="container">
          <h5 class="title-cart">■ご予約内容</h5>
          <div class="data-cart">
            <div>
              <table class="table-cart table">
                <thead>
                  <tr>
                    <th width="49%" class="product">商品名</th>
                    <th width="16%" class="color">カラー</th>
                    <th width="10%" class="size">サイズ</th>
                    <th width="10%" class="quantity">数量</th>
                    <th width="15%" class="subtotal">小計</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td class="product">
                      <div class="product-name fix-product-name">
                        <span>FOKID 福袋7点セット【予約】</span>
                      </div>
                      <div class="product-code"><span>R182011</span></div>
                    </td>
                    <td class="color">ブラウン</td>
                    <td class="size">XXS</td>
                    <td class="quantity">1</td>
                    <td class="subtotal">¥11,000</td>
                  </tr>
                </tbody>
              </table>
              <div class="row order-total">
                <div class="total-title"><label>ご予約合計金額</label></div>
                <div class="total-quantity"><span>1</span></div>
                <div class="total-subtotal">
                  <span class="price">¥11,000</span
                  ><span class="tax">(税込)</span>
                </div>
              </div>
            </div>
          </div>
          <div class="button-process">
            <button type="submit" class="btn btn-back-screen">
              <span class="arrow-button">←</span>
              商品詳細画面に戻って内容を修正する
            </button>
            <p class="note">【注意】戻るとご入力内容がリセットされます！</p>
          </div>
        </div>
      </div>
    </div>
    <!-- Cart Area End -->
    <div class="discount-area" v-if="isCheckReceiveWay == 0">
      <div class="container">
        <div class="title-group">
          <h5 class="title-discount">■商品受け取り方法</h5>
        </div>
        <div class="discount-group">
          <b-form-group>
            <b-form-radio-group
              v-model="checkOrder"
              :options="receiveWayOptionsRadio"
              name="radio-inline"
            ></b-form-radio-group>
          </b-form-group>
        </div>
      </div>
    </div>
    <div v-if="checkOrder == '01'">
      <!-- Store Area Start -->
      <div class="store-area">
        <div class="container">
          <h3 class="title-store">■ご入金・お受取 店舗</h3>
          <hr class="line-bottom-store" />
          <div class="store-content">
            <label>FOKID ららぽーと甲子園店</label>
          </div>
        </div>
      </div>
      <!-- Store Area End -->
      <!-- Customer Info Confirm Area Start -->
      <div class="customer-info-confirm-area">
        <div class="container">
          <h3 class="title-cust-info-confirm">■お客様情報</h3>
          <div class="cust-info-confirm-detail">
            <div class="row cust-info-confirm">
              <div class="title">
                <label>お名前(漢字)</label>
              </div>
              <div class="cust-info-value">
                <span>山田 太郎</span>
              </div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title">
                <label>お名前(かな)</label>
              </div>
              <div class="cust-info-value">
                <span>やまだ たろう</span>
              </div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title">
                <label>住所</label>
              </div>
              <div class="cust-info-value">
                <span>〒651-0086</span><br />
                <span>神戸市中央区磯上通7-1-5 三宮プラザEAST 8F</span>
              </div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title">
                <label>お電話番号</label>
              </div>
              <div class="cust-info-value">
                <span>0782525658</span>
              </div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title">
                <label>メールアドレス</label>
              </div>
              <div class="cust-info-value">
                <span>yamada@fo-kids.co.jp</span><br />
                <span
                  >こちらのメールアドレスにご予約確認メールが送信されます。</span
                >
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- Customer Info Confirm Area End -->
    </div>
    <div v-else>
      <div class="discount-area" v-if="textContent.checkBoxFlg2 != false">
        <div class="container">
          <div class="title-group">
            <h5 class="title-discount">■決済方法</h5>
          </div>
          <div class="discount-group">
            <b-form-group>
              <b-form-radio-group
                v-model="isShip"
                :options="paymentWayOptions"
                name="radio-inline-discount"
              ></b-form-radio-group>
            </b-form-group>
          </div>
        </div>
      </div>

      <div class="discount-area" v-if="isShip == '01'">
        <!-- Store Area Start -->
        <div class="store-area">
          <div class="container">
            <h3 class="title-store">■ご支払店舗選択</h3>
            <hr class="line-bottom-store" />
            <div class="store-content">
              <label>FOKID ららぽーと甲子園店</label>
            </div>
          </div>
        </div>
        <!-- Store Area End -->
      </div>
      <div v-else class="customer-info-confirm-area">
        <div class="container">
          <h3 class="title-cust-info-confirm">■振込先口座情報</h3>
          <div class="cust-info-confirm-detail">
            <div class="row cust-info-confirm">
              <div class="title"><label>金融機関名</label></div>
              <div class="cust-info-value"><span>US of Bank</span></div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title"><label>口座番号</label></div>
              <div class="cust-info-value"><span>9876543210</span></div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title"><label>口座名義</label></div>
              <div class="cust-info-value"><span>usname</span></div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title"><label>支店名</label></div>
              <div class="cust-info-value"><span>Branch</span></div>
            </div>
          </div>
        </div>
      </div>
      <div class="customer-info-confirm-area">
        <div class="container">
          <h3 class="title-cust-info-confirm">
            ■お客様情報
          </h3>
          <div class="cust-info-confirm-detail">
            <div class="row cust-info-confirm">
              <div class="title">
                <label>お名前(漢字)</label>
              </div>
              <div class="cust-info-value">
                <span> 山田 太郎</span>
              </div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title">
                <label>お名前(かな)</label>
              </div>
              <div class="cust-info-value">
                <span>やま ろう</span>
              </div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title">
                <label>住所</label>
              </div>
              <div class="cust-info-value">
                <span>〒060-0000</span><br /><span
                  >北海道 札幌市中央区 7-1-5
                </span>
              </div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title">
                <label>お電話番号</label>
              </div>
              <div class="cust-info-value">
                <span>312345678</span>
              </div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title">
                <label>メールアドレス</label>
              </div>
              <div class="cust-info-value">
                <span>yamada@fo-kids.co.jp</span><br /><span
                  >こちらのメールアドレスにご予約確認メールが送信されます。</span
                >
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="customer-info-confirm-area">
        <div class="container">
          <h3 class="title-cust-info-confirm">■配送</h3>
          <div class="cust-info-confirm-detail">
            <div class="row cust-info-confirm">
              <div class="title">
                <label>お名前(漢字)</label>
              </div>
              <div class="cust-info-value">
                <span> 山田 太郎</span>
              </div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title">
                <label>お名前(かな)</label>
              </div>
              <div class="cust-info-value">
                <span>やま ろう</span>
              </div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title">
                <label>住所</label>
              </div>
              <div class="cust-info-value">
                <span>〒060-0000</span><br /><span
                  >北海道 札幌市中央区 7-1-5
                </span>
              </div>
            </div>
            <hr class="line-info-confirm" />
            <div class="row cust-info-confirm">
              <div class="title">
                <label>お電話番号</label>
              </div>
              <div class="cust-info-value">
                <span>312345678</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-if="isCheckDiscount == 0" class="discount-area">
      <div class="container">
        <div class="title-group">
          <h5 class="title-discount">■早期割引特典</h5>
        </div>
        <div class="discount-group"><span>あり</span></div>
      </div>
    </div>
    <!-- Payment Time Area Start -->
    <div class="payment-time-area">
      <div class="container">
        <!-- Title PaymentTime -->
        <h3 class="title-payment-time">■{{ textContent.textTitle2 }}</h3>
        <!-- Content PaymentTime -->
        <div
          class="payment-time-content"
          v-html="textContent.textNortify1"
        ></div>
      </div>
    </div>
    <!-- Payment Time Area End -->
    <!-- Notice Q&A Area Start -->
    <div class="notice-area">
      <div class="container">
        <!-- Text Title Notice -->
        <h3 class="title-notice">■{{ textContent.textTitle3 }}</h3>
      </div>
    </div>
    <!-- Static Area Start -->
    <section class="part-rules notice-content">
      <div class="container">
        <!-- Text Content Notice -->
        <div
          class="static-area-wrap scrollingDiv scrollbar-ripe-malinka"
          v-html="textContent.textNortify2"
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
          <!-- Text Button Back To Top -->
          <button type="submit" class="btn btn-finish">
            {{ textContent.textButton1 }}
          </button>
        </div>
        <!-- Text Note Button Back To Top -->
        <span class="note">{{ textContent.textButton2 }}</span>
      </div>
    </div>
    <!-- Button Group Area End -->
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
import HeaderPage from "@/components/DataCommon/HeaderPage.vue";

export default {
  components: {
    HeaderPage,
  },
  props: {
    textContent: {},
  },
  data() {
    return {
      className: "progress-bar-order-finish",
      checkOrder: "01",
      isShip: "01",
      loading: false,
      receiveWayOptionsRadio: [],
      bussinessCd: null,
      isCheckDiscount: 0,
      isCheckReceiveWay: 0,
    };
  },
  async created() {
    this.loading = true;
    this.bussinessCd = this.getUserData().bussinessCd;
    this.receiveWayOptionsRadio = this.receiveWayOptions.filter(
      (x) => x.value !== ""
    );
    await this.getConfig(this.bussinessCd);
  },
  mounted() {},
  methods: {
    // 画面のConfigデータを取得する
    async getConfig(bussinessCd) {
      try {
        var configScreen = await this.getConfigScreen(
          this.SCREEN_ORDER,
          bussinessCd
        );
        if (configScreen != null && configScreen.length > 0) {
          this.isCheckReceiveWay = configScreen[0].checkBoxFlg1;
          this.isCheckDiscount = configScreen[0].checkBoxFlg2;
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loading = false;
      }
    },
  },
};
</script>
<style lang="scss">
.progress-bar-order-finish .progress-bar-1 {
  background-image: url(/Images/order-finish/progress_bar_1.png);
  background-repeat: no-repeat;
  width: 95px;
}
.progress-bar-order-finish .progress-bar-2 {
  background-image: url(/Images/order-finish/progress_bar_2.png);
  background-repeat: no-repeat;
  width: 100px;
  margin-left: -12px;
}

.progress-bar-order-finish .progress-bar-3 {
  background-image: url(/Images/order-finish/progress_bar_3.png);
  background-repeat: no-repeat;
  width: 119px;
  margin-left: -12px;
}

.progress-bar-order-finish .progress-bar-4 {
  background-image: url(/Images/order-finish/progress_bar_4.png);
  background-repeat: no-repeat;
  width: 97px;
  margin-left: -12px;
}
.progress-bar-order-finish .progress-bar-1,
.progress-bar-order-finish .progress-bar-2,
.progress-bar-order-finish .progress-bar-3,
.progress-bar-order-finish .progress-bar-4 {
  color: white;
  height: 35px;
  float: left;
}

.progress-bar-order-finish .inner h4 {
  font-size: 14px;
  font-weight: bold;
  line-height: 35px;
  margin-left: 20px;
}
</style>
