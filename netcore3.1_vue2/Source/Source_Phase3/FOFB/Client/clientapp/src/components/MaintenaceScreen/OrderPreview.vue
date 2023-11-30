<template>
  <div>
    <!-- Header Start -->
    <HeaderPage :className="className"></HeaderPage>
    <!-- Header End -->
    <!-- Title Start -->
    <div class="title-order">
      <div class="container">
        <!--Page Title -->
        <h5 class="title-detail">{{ this.textContent.textTitle1 }}</h5>
        <p class="title-note">
          ご予約はまだ完了していないため在庫は確保されていません。
        </p>
      </div>
    </div>
    <!-- Title End -->
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

    <!-- Customer Info Area Start -->
    <div class="customer-info-area">
      <div class="container">
        <h5 class="title-customer-info">■お客様情報</h5>
        <div class="customer-info-detail">
          <!-- お客様名前 -->
          <div class="row cust-info" id="custName">
            <div class="title-kaji mb-15px">
              <div class="title-group">
                <label class="title-kanji-name">お名前</label>
              </div>
              <div class="title-group">
                <span class="required">必須</span>
              </div>
            </div>
            <div class="family-name mb-15px">
              <span class="position-text">姓</span>
            </div>
            <div class="family-name-input mb-15px">
              <b-form-input disabled />
            </div>
            <div class="first-name mb-15px">
              <span class="position-text">名</span>
            </div>
            <div class="first-name-input mb-15px">
              <b-form-input disabled />
            </div>
          </div>
          <div class="row cust-info">
            <div class="title-kana mb-15px">
              <div class="title-group">
                <label class="title-kana-name">ふりがな</label>
              </div>
              <div class="title-group">
                <span class="required">必須</span>
              </div>
            </div>
            <div class="family-name mb-15px">
              <span class="position-text">せい</span>
            </div>
            <div class="family-name-input mb-15px">
              <b-form-input disabled type="text" />
            </div>
            <div class="first-name mb-15px">
              <span class="position-text">めい</span>
            </div>
            <div class="first-name-input mb-15px">
              <b-form-input disabled type="text" />
            </div>
          </div>
          <!--お客様アドレス-->
          <div class="row cust-info" id="zipcodeDiv">
            <div class="title-zipCode mb-15px">
              <div class="title-group">
                <label class="title-zipCode-name">郵便番号</label>
              </div>
              <div class="title-group">
                <span class="required">必須</span>
              </div>
            </div>
            <div class="zipCode-name mb-15px">
              <span class="position-text">〒</span>
            </div>
            <div class="zipCode-name-input mb-15px">
              <b-form-input
                disabled
                type="number"
                name="zipCode"
                autocomplete="off"
              />
            </div>
            <div class="placeholder-text">
              <span>例）1101100（ハイフンなし半角数字）</span>
            </div>
          </div>
          <div class="row cust-info">
            <div class="title-province mb-15px">
              <div class="title-group">
                <label class="title-province-name">都道府県</label>
              </div>
              <div class="title-group">
                <span class="required">必須</span>
              </div>
            </div>
            <div class="province mb-15px">
              <b-form-select
                disabled
                class="province-area"
                name="province"
              ></b-form-select>
            </div>
          </div>
          <div class="row cust-info">
            <div class="title-adrCd1 mb-15px">
              <div class="title-group">
                <label class="title-adrCd1-name">住所１</label>
              </div>
              <div class="title-group">
                <span class="required">必須</span>
              </div>
            </div>
            <div class="adrCd1-input mb-15px">
              <b-form-input
                disabled
                type="text"
                name="adrCd1"
                autocomplete="off"
              />
            </div>
            <div class="placeholder-text">
              <span>例）神戸市中央区磯上通</span>
            </div>
          </div>
          <div class="row cust-info">
            <div class="title-adrCd2 mb-15px">
              <div class="title-group">
                <label class="title-adrCd2-name">住所２</label>
              </div>
              <div class="title-group">
                <span class="required">必須</span>
              </div>
            </div>
            <div class="adrCd2-input mb-15px">
              <b-form-input
                disabled
                type="text"
                name="adrCd2"
                autocomplete="off"
              />
            </div>
            <div class="placeholder-text">
              <span>例）7-1-5</span>
            </div>
          </div>
          <div class="row cust-info">
            <div class="title-adrCd3 mb-15px">
              <div class="title-group">
                <label class="title-adrCd3-name">住所３(建物名等）</label>
              </div>
            </div>
            <div class="adrCd3-input mb-15px">
              <b-form-input disabled type="text" autocomplete="off" />
            </div>
            <div class="placeholder-text">
              <span>例）三宮プラザEAST 8F</span>
            </div>
          </div>
          <!--お電話番号-->
          <div class="row cust-info">
            <div class="title-phone mb-15px">
              <div class="title-group">
                <label class="title-phone-name">お電話番号</label>
              </div>
              <div class="title-group">
                <span class="required">必須</span>
              </div>
            </div>
            <div class="phone-number mb-15px">
              <b-form-input
                disabled
                type="number"
                name="phoneNumber"
                autocomplete="off"
              />
            </div>
            <div class="placeholder-text">
              <span>例）312345678（ハイフンなし半角数字）</span>
            </div>
          </div>
          <!--メールアドレス-->
          <div class="row cust-info">
            <div class="title-mail mb-15px">
              <div class="title-group">
                <label class="title-mail-name">メールアドレス</label>
              </div>
              <div class="title-group">
                <span class="required">必須</span>
              </div>
            </div>
            <div class="mail-input mb-15px">
              <b-form-input
                disabled
                type="text"
                name="mailAddress"
                autocomplete="off"
              />
            </div>
          </div>
          <div class="row cust-info">
            <div class="title-mail-confirm mb-15px">
              <div class="title-group">
                <label class="title-mail-confirm-name"
                  >メールアドレス（ご確認）</label
                >
              </div>
              <div class="title-group">
                <span class="required">必須</span>
              </div>
            </div>
            <div class="mail-conrfirm-input mb-15px">
              <b-form-input
                disabled
                type="text"
                name="mailAddressConfirm"
                autocomplete="off"
              />
            </div>
            <div class="placeholder-text">
              <span>※メールアドレスはお間違いのないようご注意ください。</span>
            </div>
            <div class="placeholder-text">
              <span>※確認のため再度同じメールアドレスを入力してください。</span>
            </div>
          </div>
          <!--商品受け取り方法-->
          <div
            class="row cust-info"
            v-if="this.textContent.checkBoxFlg1 == true"
          >
            <div class="title-receive-product mb-15px">
              <div class="title-group">
                <label class="title-receive-name">商品受け取り方法</label>
              </div>
              <div class="title-group">
                <span class="required">必須</span>
              </div>
            </div>
            <div class="receive-product-radio mb-15px">
              <b-form-group>
                <b-form-radio-group
                  v-model="receiveWay"
                  :options="receiveWayOptionsRadio"
                  name="radio-inline"
                ></b-form-radio-group>
              </b-form-group>
            </div>
          </div>
          <div
            class="row cust-info"
            v-if="receiveWay !== this.IN_SHOP && this.textContent.checkBoxFlg1"
          >
            <div class="title-receive-product mb-15px">
              <div class="title-group">
                <label class="title-receive-name">決済方法</label>
              </div>
              <div class="title-group"><span class="required">必須</span></div>
            </div>
            <div class="receive-product-radio mb-15px">
              <b-form-group>
                <b-form-radio-group
                  v-model="paymentWay"
                  :options="paymentWayOptions"
                  name="radio-paymentWay"
                ></b-form-radio-group>
              </b-form-group>
            </div>
          </div>
        </div>
      </div>
      <!-- Store Area Start -->
      <div
        class="store-area"
        v-if="
          receiveWay === null ||
            (paymentWay !== null &&
              paymentWay === this.PAY_IN_SHOP &&
              receiveWay !== this.IN_SHOP &&
              this.textContent.checkBoxFlg1)
        "
      >
        <div class="container">
          <!-- Text for store title -->
          <h3 class="title-store">■{{ this.textContent.textArea1 }}</h3>
          <div class="store-detail">
            <!-- Text for notify of store -->
            <div v-html="this.textContent.textArea2"></div>
            <div class="store-select-wrapper" id="shop">
              <b-form-select class="shop-area" disabled></b-form-select>
            </div>
          </div>
        </div>
      </div>
      <div
        class="store-area"
        v-if="receiveWay == this.IN_SHOP || !this.textContent.checkBoxFlg1"
      >
        <div class="container">
          <!-- Text for store title -->
          <h3 class="title-store">■{{ this.textContent.textTitle2 }}</h3>
          <div class="store-detail">
            <!-- Text for notify of store -->
            <div v-html="this.textContent.textNortify1"></div>
            <div class="store-select-wrapper" id="shop">
              <b-form-select class="shop-area" disabled></b-form-select>
            </div>
          </div>
        </div>
      </div>

      <!-- Store Area End -->
      <!-- Customer Recieve Info Area Start -->
      <div
        class="customer-recieve-info-area"
        v-if="receiveWay !== this.IN_SHOP && this.textContent.checkBoxFlg1"
      >
        <div class="container">
          <!-- Text for delivery title -->
          <h5 class="title-customer-recieve-info">
            ■{{ this.textContent.textTitle3 }}
          </h5>
          <div class="customer-recieve-info-detail">
            <div class="item-select-address">
              <b-form-group>
                <b-form-radio-group
                  v-model="isAddressOther"
                  :options="adrSelectOptions"
                  name="radio-inline-adr"
                ></b-form-radio-group>
              </b-form-group>
            </div>
            <div v-if="isAddressOther">
              <!-- お客様名前 -->
              <div class="row cust-recieve-info" id="cust-recieve-name">
                <div class="title-kaji mb-15px">
                  <div class="title-group">
                    <label class="title-kanji-name">お名前</label>
                  </div>
                  <div class="title-group">
                    <span class="required">必須</span>
                  </div>
                </div>
                <div class="family-name mb-15px">
                  <span class="position-text">姓</span>
                </div>
                <div class="family-name-input mb-15px">
                  <b-form-input
                    type="text"
                    disabled
                    name="kanjiFamilyName"
                    autocomplete="off"
                  />
                </div>
                <div class="first-name mb-15px">
                  <span class="position-text">名</span>
                </div>
                <div class="first-name-input mb-15px">
                  <b-form-input
                    type="text"
                    name="kanjiFirstName"
                    disabled
                    autocomplete="off"
                  />
                </div>
              </div>
              <div class="row cust-recieve-info">
                <div class="title-kana mb-15px">
                  <div class="title-group">
                    <label class="title-kana-name">ふりがな</label>
                  </div>
                  <div class="title-group">
                    <span class="required">必須</span>
                  </div>
                </div>
                <div class="family-name mb-15px">
                  <span class="position-text">せい</span>
                </div>
                <div class="family-name-input mb-15px">
                  <b-form-input
                    type="text"
                    name="kanaFamilyName"
                    disabled
                    autocomplete="off"
                  />
                </div>
                <div class="first-name mb-15px">
                  <span class="position-text">めい</span>
                </div>
                <div class="first-name-input mb-15px">
                  <b-form-input
                    type="text"
                    name="kanaFirstName"
                    disabled
                    autocomplete="off"
                  />
                </div>
              </div>
              <!--お客様アドレス-->
              <div class="row cust-recieve-info" id="zipcode-cust-recieve">
                <div class="title-zipCode mb-15px">
                  <div class="title-group">
                    <label class="title-zipCode-name">郵便番号</label>
                  </div>
                  <div class="title-group">
                    <span class="required">必須</span>
                  </div>
                </div>
                <div class="zipCode-name mb-15px">
                  <span class="position-text">〒</span>
                </div>
                <div class="zipCode-name-input mb-15px">
                  <b-form-input
                    type="number"
                    name="zipCode"
                    disabled
                    autocomplete="off"
                  />
                </div>
                <div class="placeholder-text">
                  <span>例）1101100（ハイフンなし半角数字）</span>
                </div>
              </div>
              <div class="row cust-recieve-info">
                <div class="title-province mb-15px">
                  <div class="title-group">
                    <label class="title-province-name">都道府県</label>
                  </div>
                  <div class="title-group">
                    <span class="required">必須</span>
                  </div>
                </div>
                <div class="province mb-15px">
                  <b-form-select
                    class="province-area"
                    name="province"
                    disabled
                  ></b-form-select>
                </div>
              </div>
              <div class="row cust-recieve-info">
                <div class="title-adrCd1 mb-15px">
                  <div class="title-group">
                    <label class="title-adrCd1-name">住所１</label>
                  </div>
                  <div class="title-group">
                    <span class="required">必須</span>
                  </div>
                </div>
                <div class="adrCd1-input mb-15px">
                  <b-form-input
                    type="text"
                    name="adrCd1"
                    disabled
                    autocomplete="off"
                  />
                </div>
                <div class="placeholder-text">
                  <span>例）神戸市中央区磯上通</span>
                </div>
              </div>
              <div class="row cust-recieve-info">
                <div class="title-adrCd2 mb-15px">
                  <div class="title-group">
                    <label class="title-adrCd2-name">住所２</label>
                  </div>
                  <div class="title-group">
                    <span class="required">必須</span>
                  </div>
                </div>
                <div class="adrCd2-input mb-15px">
                  <b-form-input
                    type="text"
                    name="adrCd2"
                    disabled
                    autocomplete="off"
                  />
                </div>
                <div class="placeholder-text">
                  <span>例）7-1-5</span>
                </div>
              </div>
              <div class="row cust-recieve-info">
                <div class="title-adrCd3 mb-15px">
                  <div class="title-group">
                    <label class="title-adrCd3-name">住所３(建物名等）</label>
                  </div>
                </div>
                <div class="adrCd3-input mb-15px">
                  <b-form-input type="text" disabled autocomplete="off" />
                </div>
                <div class="placeholder-text">
                  <span>例）三宮プラザEAST 8F</span>
                </div>
              </div>
              <!--お電話番号-->
              <div class="row cust-recieve-info">
                <div class="title-phone mb-15px">
                  <div class="title-group">
                    <label class="title-phone-name">お電話番号</label>
                  </div>
                  <div class="title-group">
                    <span class="required">必須</span>
                  </div>
                </div>
                <div class="phone-number mb-15px">
                  <b-form-input
                    type="number"
                    name="phoneNumber"
                    disabled
                    autocomplete="off"
                  />
                </div>
                <div class="placeholder-text">
                  <span>例）312345678（ハイフンなし半角数字）</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- Customer Recieve Info Area End -->
      <div class="discount-area" v-if="textContent.checkBoxFlg2 != false">
        <div class="container">
          <div class="title-group">
            <h5 class="title-discount">■早期割引特典</h5>
            <div class="title-group">
              <span class="required">必須</span>
            </div>
          </div>
          <div class="discount-group">
            <b-form-group>
              <b-form-radio-group
                v-model="isDiscount"
                :options="discountOptionsRadio"
                name="radio-inline-discount"
              ></b-form-radio-group>
            </b-form-group>
          </div>
        </div>
      </div>
      <!-- Button Area Start -->
      <div class="button-grp">
        <div class="container">
          <!-- Text for button confirm order -->
          <div class="note" v-html="this.textContent.textNortify2"></div>
          <div class="button-confirm">
            <button type="submit" class="btn btn-confirm">
              {{ this.textContent.textButton1 }}
            </button>
          </div>
        </div>
      </div>
      <!-- Button Area End -->
    </div>
    <!-- Customer Info Area End -->
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
      className: "progress-bar-header",

      isAddressOther: true,
      receiveWay: "01",
      paymentWay: null,
      isDiscount: true,
      adrSelectOptions: [
        { text: "上記の住所に配送する", value: false },
        { text: "別の住所に配送する", value: true },
      ],
      receiveWayOptionsRadio: [],
    };
  },
  async created() {
    this.receiveWayOptionsRadio = this.receiveWayOptions.filter(
      (x) => x.value !== ""
    );

    this.discountOptionsRadio = this.discountOptions.filter(
      (x) => x.value !== null
    );
  },
};
</script>
<style lang="scss">
.progress-bar-header .progress-bar-1 {
  background-image: url(/Images/order/progress_bar_1.png);
  background-repeat: no-repeat;
  width: 95px;
}
.progress-bar-header .progress-bar-2 {
  background-image: url(/Images/order/progress_bar_2.png);
  background-repeat: no-repeat;
  width: 100px;
  margin-left: -12px;
}

.progress-bar-header .progress-bar-3 {
  background-image: url(/Images/order/progress_bar_3.png);
  background-repeat: no-repeat;
  width: 119px;
  margin-left: -12px;
}

.progress-bar-header .progress-bar-4 {
  background-image: url(/Images/order/progress_bar_4.png);
  background-repeat: no-repeat;
  width: 97px;
  margin-left: -12px;
}
.progress-bar-header .progress-bar-1,
.progress-bar-header .progress-bar-2,
.progress-bar-header .progress-bar-3,
.progress-bar-header .progress-bar-4 {
  color: white;
  height: 35px;
  float: left;
}

progress-bar-header .inner h4 {
  font-size: 14px;
  font-weight: bold;
  line-height: 35px;
  margin-left: 20px;
}
</style>
