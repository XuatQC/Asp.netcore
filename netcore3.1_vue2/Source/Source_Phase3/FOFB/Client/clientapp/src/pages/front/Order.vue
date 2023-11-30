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
    <div class="title-order">
      <div class="container">
        <!-- Text for title page order -->
        <h5 class="title-detail">{{ pageTitle }}</h5>
        <p class="title-note">
          ご予約はまだ完了していないため在庫は確保されていません。
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
            id="button-back"
            @click="backProductDetail()"
            type="submit"
            class="btn btn-back-screen"
          >
            <span class="arrow-button">←</span> 前の画面に戻って数量を修正する
          </button>
          <p class="note">【注意】戻るとご入力内容がリセットされます！</p>
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
          <div class="row cust-info" id="cust-name">
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
                name="kanjiFamilyName"
                v-model="custInfo.kanjiFamilyName"
                @keyup="checkValidKanjiFamily()"
                autocomplete="off"
              />
              <span
                class="invalid-feedback d-block"
                v-if="kanjiFamilyError != ''"
                >{{ this.kanjiFamilyError }}</span
              >
            </div>
            <div class="first-name mb-15px">
              <span class="position-text">名</span>
            </div>
            <div class="first-name-input mb-15px">
              <b-form-input
                type="text"
                name="kanjiFirstName"
                v-model="custInfo.kanjiFirstName"
                @keyup="checkValidKanjiFirst()"
                autocomplete="off"
              />
              <span
                class="invalid-feedback d-block"
                v-if="kanjiFirstError != ''"
                >{{ this.kanjiFirstError }}</span
              >
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
              <b-form-input
                type="text"
                name="kanaFamilyName"
                v-model="custInfo.kanaFamilyName"
                @keyup="checkValidKanaFamily()"
                autocomplete="off"
              />
              <span
                class="invalid-feedback d-block"
                v-if="kanaFamilyError != ''"
                >{{ this.kanaFamilyError }}</span
              >
            </div>
            <div class="first-name mb-15px">
              <span class="position-text">めい</span>
            </div>
            <div class="first-name-input mb-15px">
              <b-form-input
                type="text"
                name="kanaFirstName"
                v-model="custInfo.kanaFirstName"
                @keyup="checkValidKanaFirst()"
                autocomplete="off"
              />
              <span
                class="invalid-feedback d-block"
                v-if="kanaFirstError != ''"
                >{{ this.kanaFirstError }}</span
              >
            </div>
          </div>
          <!--お客様アドレス-->
          <div class="row cust-info" id="zipcode">
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
                v-model="custInfo.zipCd"
                v-on:keypress="isNumber()"
                @keyup="checkValidZipCode(true)"
                autocomplete="off"
              />
              <span
                class="invalid-feedback d-block"
                v-if="zipCodeError != ''"
                >{{ this.zipCodeError }}</span
              >
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
                class="province-area"
                name="province"
                v-model="custInfo.province"
                :options="lstProvince"
                @change="checkValidProvince()"
              ></b-form-select>
              <span
                class="invalid-feedback d-block"
                v-if="provinceError != ''"
                >{{ this.provinceError }}</span
              >
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
                type="text"
                name="adrCd1"
                v-model="custInfo.adrCd1"
                @keyup="checkValidAdrCd1()"
                autocomplete="off"
              />
              <span class="invalid-feedback d-block" v-if="adrCd1Error != ''">{{
                this.adrCd1Error
              }}</span>
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
                type="text"
                name="adrCd2"
                v-model="custInfo.adrCd2"
                @keyup="checkValidAdrCd2()"
                autocomplete="off"
              />
              <span class="invalid-feedback d-block" v-if="adrCd2Error != ''">{{
                this.adrCd2Error
              }}</span>
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
              <b-form-input
                type="text"
                v-model="custInfo.adrCd3"
                @keyup="checkValidAdrCd3()"
                autocomplete="off"
              />
              <span class="invalid-feedback d-block" v-if="adrCd3Error != ''">{{
                this.adrCd3Error
              }}</span>
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
                type="number"
                name="phoneNumber"
                v-model="custInfo.phoneNumber"
                v-on:keypress="isNumber()"
                @keyup="checkValidPhoneNumber()"
                autocomplete="off"
              />
              <span
                class="invalid-feedback d-block"
                v-if="phoneNumberError != ''"
                >{{ this.phoneNumberError }}</span
              >
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
                type="text"
                name="mailAddress"
                v-model="custInfo.mailAddress"
                @keyup="checkValidMailAddress()"
                autocomplete="off"
              />
              <span
                class="invalid-feedback d-block"
                v-if="mailAddressError != ''"
                >{{ this.mailAddressError }}</span
              >
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
                type="text"
                name="mailAddressConfirm"
                v-model="custInfo.mailAddressConfirm"
                @keyup="checkValidMailAddressConfirm()"
                autocomplete="off"
              />
              <span
                class="invalid-feedback d-block"
                v-if="mailAddressConfirmError != ''"
                >{{ this.mailAddressConfirmError }}</span
              >
            </div>
            <div class="placeholder-text">
              <span>※メールアドレスはお間違いのないようご注意ください。</span>
            </div>
            <div class="placeholder-text">
              <span>※確認のため再度同じメールアドレスを入力してください。</span>
            </div>
          </div>
          <!--商品受け取り方法-->
          <div class="row cust-info" v-if="isCheckReceiveWay == this.ITEM_SHOW">
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
                  @change="changeReceiveProduct()"
                ></b-form-radio-group>
              </b-form-group>
            </div>
          </div>
          <!--決済方法-->
          <div class="row cust-info" v-if="receiveWay == this.SHIPPING">
            <div class="title-receive-product mb-15px">
              <div class="title-group">
                <label class="title-receive-name">決済方法</label>
              </div>
              <div class="title-group">
                <span class="required">必須</span>
              </div>
            </div>
            <div class="receive-product-radio mb-15px">
              <b-form-group>
                <b-form-radio-group
                  v-model="paymentWay"
                  :options="paymentWayOptions"
                  @change="checkValidSelectPaymentWay()"
                  name="radio-paymentWay"
                ></b-form-radio-group>
              </b-form-group>
              <span
                class="invalid-feedback d-block"
                v-if="paymentWayError != ''"
                >{{ this.paymentWayError }}</span
              >
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Customer Info Area End -->
    <!-- Store Area Start -->
    <div
      class="store-area"
      v-if="
        receiveWay === null ||
          (receiveWay !== null && receiveWay === this.IN_SHOP) ||
          (paymentWay !== null && paymentWay === this.PAY_IN_SHOP)
      "
    >
      <div class="container">
        <!-- Text for store title -->
        <h3 class="title-store">■{{ receiveWay === null || receiveWay === this.IN_SHOP ? stroreTitle : strorePayTitle }}</h3>
        <div class="store-detail">
          <!-- Text for notify of store -->
          <div v-html="receiveWay === null || receiveWay === this.IN_SHOP ? stroreNotify : strorePayNotify"></div>
          <div class="store-select-wrapper" id="shop">
            <b-form-select
              class="shop-area"
              v-if="isShowShop"
              v-model="shopSelect"
              :options="lstShop"
              @change="changeShopSelect()"
            ></b-form-select>
            <b-form-select
              class="shop-area"
              v-else
              v-model="areaSelect"
              :options="lstArea"
              @change="changeAreaSelect()"
            ></b-form-select>
            <span
              class="invalid-feedback d-block"
              v-if="shopSelectError != ''"
              >{{ this.shopSelectError }}</span
            >
          </div>
        </div>
      </div>
    </div>
    <!-- Store Area End -->
    <!-- Customer Recieve Info Area Start -->
    <div
      class="customer-recieve-info-area"
      v-if="receiveWay !== null && receiveWay === this.SHIPPING"
    >
      <div class="container">
        <!-- Text for delivery title -->
        <h5 class="title-customer-recieve-info">■{{ deliveryTitle }}</h5>
        <div class="customer-recieve-info-detail">
          <div class="item-select-address">
            <b-form-group>
              <b-form-radio-group
                v-model="isAddressOther"
                :options="adrSelectOptions"
                name="radio-inline-adr"
                @change="changeAddressOther()"
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
                  name="kanjiFamilyName"
                  v-model="custRecieveInfo.kanjiFamilyName"
                  @keyup="checkValidKanjiFamilyCustRecieve()"
                  autocomplete="off"
                />
                <span
                  class="invalid-feedback d-block"
                  v-if="kanjiFamilyCustRecieveError != ''"
                  >{{ this.kanjiFamilyCustRecieveError }}</span
                >
              </div>
              <div class="first-name mb-15px">
                <span class="position-text">名</span>
              </div>
              <div class="first-name-input mb-15px">
                <b-form-input
                  type="text"
                  name="kanjiFirstName"
                  v-model="custRecieveInfo.kanjiFirstName"
                  @keyup="checkValidKanjiFirstCustRecieve()"
                  autocomplete="off"
                />
                <span
                  class="invalid-feedback d-block"
                  v-if="kanjiFirstCustRecieveError != ''"
                  >{{ this.kanjiFirstCustRecieveError }}</span
                >
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
                  v-model="custRecieveInfo.kanaFamilyName"
                  @keyup="checkValidKanaFamilyCustRecieve()"
                  autocomplete="off"
                />
                <span
                  class="invalid-feedback d-block"
                  v-if="kanaFamilyCustRecieveError != ''"
                  >{{ this.kanaFamilyCustRecieveError }}</span
                >
              </div>
              <div class="first-name mb-15px">
                <span class="position-text">めい</span>
              </div>
              <div class="first-name-input mb-15px">
                <b-form-input
                  type="text"
                  name="kanaFirstName"
                  v-model="custRecieveInfo.kanaFirstName"
                  @keyup="checkValidKanaFirstCustRecieve()"
                  autocomplete="off"
                />
                <span
                  class="invalid-feedback d-block"
                  v-if="kanaFirstCustRecieveError != ''"
                  >{{ this.kanaFirstCustRecieveError }}</span
                >
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
                  v-model="custRecieveInfo.zipCd"
                  v-on:keypress="isNumber()"
                  @keyup="checkValidZipCodeCustRecieve(true)"
                  autocomplete="off"
                />
                <span
                  class="invalid-feedback d-block"
                  v-if="zipCodeCustRecieveError != ''"
                  >{{ this.zipCodeCustRecieveError }}</span
                >
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
                  v-model="custRecieveInfo.province"
                  :options="lstProvince"
                  @change="checkValidProvinceCustRecieve()"
                ></b-form-select>
                <span
                  class="invalid-feedback d-block"
                  v-if="provinceCustRecieveError != ''"
                  >{{ this.provinceCustRecieveError }}</span
                >
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
                  v-model="custRecieveInfo.adrCd1"
                  @keyup="checkValidAdrCd1CustRecieve()"
                  autocomplete="off"
                />
                <span
                  class="invalid-feedback d-block"
                  v-if="adrCd1CustRecieveError != ''"
                  >{{ this.adrCd1CustRecieveError }}</span
                >
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
                  v-model="custRecieveInfo.adrCd2"
                  @keyup="checkValidAdrCd2CustRecieve()"
                  autocomplete="off"
                />
                <span
                  class="invalid-feedback d-block"
                  v-if="adrCd2CustRecieveError != ''"
                  >{{ this.adrCd2CustRecieveError }}</span
                >
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
                <b-form-input
                  type="text"
                  v-model="custRecieveInfo.adrCd3"
                  @keyup="checkValidAdrCd3CustRecieve()"
                  autocomplete="off"
                />
                <span
                  class="invalid-feedback d-block"
                  v-if="adrCd3CustRecieveError != ''"
                  >{{ this.adrCd3CustRecieveError }}</span
                >
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
                  v-model="custRecieveInfo.phoneNumber"
                  v-on:keypress="isNumber()"
                  @keyup="checkValidPhoneNumberCustRecieve()"
                  autocomplete="off"
                />
                <span
                  class="invalid-feedback d-block"
                  v-if="phoneNumberCustRecieveError != ''"
                  >{{ this.phoneNumberCustRecieveError }}</span
                >
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
    <div class="discount-area" v-if="isCheckDiscount != 1">
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
              @change="checkValidSelectDiscount()"
            ></b-form-radio-group>
          </b-form-group>
          <span class="invalid-feedback d-block" v-if="discountError != ''">{{
            this.discountError
          }}</span>
        </div>
      </div>
    </div>
    <!-- Button Area Start -->
    <div class="button-grp">
      <div class="container">
        <!-- Text for button confirm order -->
        <div class="note" v-html="textNoteBtnConfirmOrder"></div>
        <div class="button-confirm">
          <button
            @click="sendDataOrder()"
            type="submit"
            class="btn btn-confirm"
          >
            {{ textBtnConfirmOrder }}
          </button>
        </div>
      </div>
    </div>
    <!-- Button Area End -->
  </div>
</template>

<script>
import cart from "../../components/DataCommon/Cart.vue";
import HeaderPage from "@/components/DataCommon/HeaderPage.vue";

export default {
  components: {
    cart,
    HeaderPage,
  },
  data() {
    return {
      className: "progress-bar-header",
      loading: false,
      bussinessCd: "",
      pageTitle: "",
      isCheckReceiveWay: "",
      stroreTitle: "",
      strorePayTitle: "",
      stroreNotify: "",
      strorePayNotify: "",
      deliveryTitle: "",
      isCheckDiscount: "",
      textNoteBtnConfirmOrder: "",
      textBtnConfirmOrder: "",
      dataOrder: [],
      dataProduct: [],
      quantityTotal: 0,
      amountTotal: 0,
      lstArea: [],
      lstShop: [],
      lstProvince: [],
      isShowShop: false,
      areaSelect: "",
      shopSelect: "",
      custInfo: {
        province: "",
        adrCd1: "",
        adrCd2: "",
        zipCodeDsp: "",
      },
      custRecieveInfo: {
        province: "",
        adrCd1: "",
        adrCd2: "",
        zipCodeDsp: "",
      },
      shopSelectError: "",
      kanjiFamilyError: "",
      kanjiFirstError: "",
      kanaFamilyError: "",
      kanaFirstError: "",
      zipCodeError: "",
      provinceError: "",
      adrCd1Error: "",
      adrCd2Error: "",
      adrCd3Error: "",
      phoneNumberError: "",
      mailAddressError: "",
      mailAddressConfirmError: "",
      kanjiFamilyCustRecieveError: "",
      kanjiFirstCustRecieveError: "",
      kanaFamilyCustRecieveError: "",
      kanaFirstCustRecieveError: "",
      zipCodeCustRecieveError: "",
      provinceCustRecieveError: "",
      adrCd1CustRecieveError: "",
      adrCd2CustRecieveError: "",
      adrCd3CustRecieveError: "",
      phoneNumberCustRecieveError: "",
      paymentWayError: "",
      discountError: "",
      receiveWay: null,
      paymentWay: null,
      isAddressOther: true,
      isDiscount: null,
      adrSelectOptions: [
        { text: "上記の住所に配送する", value: false },
        { text: "別の住所に配送する", value: true },
      ],
      discountOptionsRadio: [],
      receiveWayOptionsRadio: [],
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

      this.receiveWayOptionsRadio = this.receiveWayOptions.filter(
        (x) => x.value !== ""
      );

      this.discountOptionsRadio = this.discountOptions.filter(
        (x) => x.value !== null
      );

      // パラメーター又はlocalStorageから商品データを取得する
      this.getDataProduct();

      // 画面のConfigデータを取得する
      await this.getConfig(this.bussinessCd);

      // 注文データ処理
      this.listDataOrder(this.dataProduct);

      // エリア一覧取得
      await this.listArea();

      // 都道府県一覧取得
      await this.listProvince();

      // get data order from localStorage
      this.getDataOrder();

      // get data shopInfo from localStorage
      this.getShopInfo();

      this.$vuetify.goTo("#top");
    } else {
      this.$router.push({ name: "404" });
    }
    this.loading = false;
  },
  methods: {
    // 姓（漢字）バリデーション
    checkValidKanjiFamily() {
      this.kanjiFamilyError = this.validateKanjiName(
        this.custInfo.kanjiFamilyName,
        20,
        this.KANJI_FAMILY
      );
    },
    // 名（漢字）バリデーション
    checkValidKanjiFirst() {
      this.kanjiFirstError = this.validateKanjiName(
        this.custInfo.kanjiFirstName,
        20,
        this.KANJI_FIRST
      );
    },
    // 姓（ひらがな）バリデーション
    checkValidKanaFamily() {
      this.kanaFamilyError = this.validateKanaName(
        this.custInfo.kanaFamilyName,
        20,
        this.KANA_FAMILY
      );
    },
    // 名（ひらがな）バリデーション
    checkValidKanaFirst() {
      this.kanaFirstError = this.validateKanaName(
        this.custInfo.kanaFirstName,
        20,
        this.KANA_FIRST
      );
    },
    // 郵便番号バリデーション
    async checkValidZipCode(isGetZipCode) {
      this.zipCodeError = this.validateZipCode(
        this.custInfo.zipCd,
        7,
        this.ZIPCODE
      );
      if (this.zipCodeError == "") {
        // 入力した場合
        this.provinceError = "";
        this.adrCd1Error = "";
        this.adrCd2Error = "";

        if (isGetZipCode) {
          this.zipCodeError = "";
          let zipCodeInfo = {};
          this.loading = true;
          zipCodeInfo = await this.getListMPostCodeByZipcode(this.custInfo.zipCd);
          if (zipCodeInfo != null && zipCodeInfo.length > 0) {
            var zipCodeIndex = 0;

            if (zipCodeInfo.length > 1) {
              for (let i = 0; i < zipCodeInfo.length; i++) {
                if(zipCodeInfo[i].isPriority === true){
                  zipCodeIndex = i;
                }
              }
            }
            this.custInfo.zipCodeDsp = zipCodeInfo[zipCodeIndex].zipCodeDsp;
            this.custInfo.province =
              zipCodeInfo[zipCodeIndex].regionCd < 9
                ? "0" + zipCodeInfo[zipCodeIndex].regionCd
                : zipCodeInfo[zipCodeIndex].regionCd.toString();

            this.custInfo.adrCd1 =
              zipCodeInfo[zipCodeIndex].city +
              (zipCodeInfo[zipCodeIndex].town != null
                ? zipCodeInfo[zipCodeIndex].town
                : "");
            this.custInfo.adrCd2 =
              zipCodeInfo[zipCodeIndex].nom != null
                ? zipCodeInfo[zipCodeIndex].nom
                : "";
          } else {
            this.zipCodeError = this.msgZipCodeExist;
            this.custInfo.province = "";
            this.custInfo.adrCd1 = "";
            this.custInfo.adrCd2 = "";
          }
          this.loading = false;
        }
      }
    },
    // 都道府県選択バリデーション
    checkValidProvince() {
      this.provinceError = this.validateProvince(
        this.custInfo.province,
        this.PROVINCE
      );
    },
    // 住所1入力バリデーション
    checkValidAdrCd1() {
      this.adrCd1Error = this.validateAdrCdRequired(
        this.custInfo.adrCd1,
        50,
        this.ADR_CD1
      );
    },
    // 住所2入力バリデーション
    checkValidAdrCd2() {
      this.adrCd2Error = this.validateAdrCdRequired(
        this.custInfo.adrCd2,
        50,
        this.ADR_CD2
      );
    },
    // 住所3入力バリデーション
    checkValidAdrCd3() {
      this.adrCd3Error = this.validateAdrCdUnRequired(
        this.custInfo.adrCd3,
        100,
        this.ADR_CD3
      );
    },
    // 電話番号バリデーション
    checkValidPhoneNumber() {
      this.phoneNumberError = this.validatePhoneNumber(
        this.custInfo.phoneNumber,
        11,
        this.PHONE_NUMBER
      );
    },
    // メールアドレスバリデーション
    checkValidMailAddress() {
      this.mailAddressError = this.validateMailAddress(
        this.custInfo.mailAddress,
        50,
        this.MAIL_ADDRESS
      );

      // メールアドレス（確認）チェック
      if (
        this.custInfo.mailAddressConfirm !== "" &&
        this.custInfo.mailAddressConfirm !== undefined
      ) {
        if (this.custInfo.mailAddressConfirm !== this.custInfo.mailAddress) {
          this.mailAddressConfirmError = this.msgMailAddressMatched.replace(
            "*",
            this.MAIL_ADDRESS_CONFIRM
          );
        } else if (
          this.custInfo.mailAddressConfirm === this.custInfo.mailAddress
        ) {
          this.mailAddressConfirmError = "";
        }
      }
    },
    // メールアドレス（確認）バリデーション
    checkValidMailAddressConfirm() {
      this.mailAddressConfirmError = this.validateMailAddressConfirm(
        this.custInfo.mailAddressConfirm,
        this.custInfo.mailAddress,
        this.MAIL_ADDRESS_CONFIRM
      );
    },
    // 店舗選択バリデーション
    checkValidShopSelect() {
      this.shopSelectError = this.validateShopSelect(
        this.areaSelect,
        this.shopSelect,
        this.AREA,
        this.SHOP
      );
    },
    // 姓（漢字）バリデーション
    checkValidKanjiFamilyCustRecieve() {
      this.kanjiFamilyCustRecieveError = this.validateKanjiName(
        this.custRecieveInfo.kanjiFamilyName,
        20,
        this.KANJI_FAMILY
      );
    },
    // 名（漢字）バリデーション
    checkValidKanjiFirstCustRecieve() {
      this.kanjiFirstCustRecieveError = this.validateKanjiName(
        this.custRecieveInfo.kanjiFirstName,
        20,
        this.KANJI_FIRST
      );
    },
    // 姓（ひらがな）バリデーション
    checkValidKanaFamilyCustRecieve() {
      this.kanaFamilyCustRecieveError = this.validateKanaName(
        this.custRecieveInfo.kanaFamilyName,
        20,
        this.KANA_FAMILY
      );
    },
    // 名（ひらがな）バリデーション
    checkValidKanaFirstCustRecieve() {
      this.kanaFirstCustRecieveError = this.validateKanaName(
        this.custRecieveInfo.kanaFirstName,
        20,
        this.KANA_FIRST
      );
    },
    // 郵便番号バリデーション
    async checkValidZipCodeCustRecieve(isGetZipCode) {
      this.zipCodeCustRecieveError = this.validateZipCode(
        this.custRecieveInfo.zipCd,
        7,
        this.ZIPCODE
      );
      if (this.zipCodeCustRecieveError == "") {
        // 入力した場合
        this.provinceCustRecieveError = "";
        this.adrCd1CustRecieveError = "";
        this.adrCd2CustRecieveError = "";

        if (isGetZipCode) {
          let zipCodeInfo = {};
          this.loading = true;
          zipCodeInfo = await this.getListMPostCodeByZipcode(
            this.custRecieveInfo.zipCd
          );
          if (zipCodeInfo != null && zipCodeInfo.length > 0) {
            var zipCodeIndex = 0;
            
           if (zipCodeInfo.length > 1) {
              for (let i = 0; i < zipCodeInfo.length; i++) {
                if(zipCodeInfo[i].isPriority === true){
                  zipCodeIndex = i;
                }
              }
            }
            this.custRecieveInfo.zipCodeDsp =
              zipCodeInfo[zipCodeIndex].zipCodeDsp;
            this.custRecieveInfo.province =
              zipCodeInfo[zipCodeIndex].regionCd < 9
                ? "0" + zipCodeInfo[zipCodeIndex].regionCd
                : zipCodeInfo[zipCodeIndex].regionCd.toString();

            this.custRecieveInfo.adrCd1 =
              zipCodeInfo[zipCodeIndex].city +
              (zipCodeInfo[zipCodeIndex].town != null
                ? zipCodeInfo[zipCodeIndex].town
                : "");
            this.custRecieveInfo.adrCd2 =
              zipCodeInfo[zipCodeIndex].nom != null
                ? zipCodeInfo[zipCodeIndex].nom
                : "";
          } else {
            this.zipCodeCustRecieveError = this.msgZipCodeExist;
            this.custRecieveInfo.province = "";
            this.custRecieveInfo.adrCd1 = "";
            this.custRecieveInfo.adrCd2 = "";
          }
          this.loading = false;
        }
      }
    },
    // 都道府県選択バリデーション
    checkValidProvinceCustRecieve() {
      this.provinceCustRecieveError = this.validateProvince(
        this.custRecieveInfo.province,
        this.PROVINCE
      );
    },
    // 住所1入力バリデーション
    checkValidAdrCd1CustRecieve() {
      this.adrCd1CustRecieveError = this.validateAdrCdRequired(
        this.custRecieveInfo.adrCd1,
        50,
        this.ADR_CD1
      );
    },
    // 住所2入力バリデーション
    checkValidAdrCd2CustRecieve() {
      this.adrCd2CustRecieveError = this.validateAdrCdRequired(
        this.custRecieveInfo.adrCd2,
        50,
        this.ADR_CD2
      );
    },
    // 住所3入力バリデーション
    checkValidAdrCd3CustRecieve() {
      this.adrCd3CustRecieveError = this.validateAdrCdUnRequired(
        this.custRecieveInfo.adrCd3,
        100,
        this.ADR_CD3
      );
    },
    // 電話番号バリデーション
    checkValidPhoneNumberCustRecieve() {
      this.phoneNumberCustRecieveError = this.validatePhoneNumber(
        this.custRecieveInfo.phoneNumber,
        11,
        this.PHONE_NUMBER
      );
    },
    // 早期割引特典バリデーション
    checkValidSelectDiscount() {
      if (this.isDiscount === null) {
        this.discountError = this.msgPleaseSelect.replace("*", this.DISCOUNT);
      } else {
        this.discountError = "";
      }
    },
    // 決済方法バリデーション
    checkValidSelectPaymentWay() {
      if (this.paymentWay === null) {
        this.paymentWayError = this.msgPleaseSelect.replace(
          "*",
          this.PAYMENT_WAY
        );
      } else {
        if (this.paymentWay == this.TRANSFER) {
          this.areaSelect = "";
          this.shopSelect = "";
          this.lstShop = [];
          this.shopSelectError = "";
          this.isShowShop = false;
        }
        this.paymentWayError = "";
      }
    },
    // 画面のConfigデータを取得する
    async getConfig(bussinessCd) {
      try {
        var configScreen = await this.getConfigScreen(
          this.SCREEN_ORDER,
          bussinessCd
        );

        if (configScreen != null && configScreen.length > 0) {
          this.pageTitle = configScreen[0].textTitle1;
          this.isCheckReceiveWay = configScreen[0].checkBoxFlg1;
          this.stroreTitle = configScreen[0].textTitle2;
          this.strorePayTitle = configScreen[0].textArea1;
          this.stroreNotify = configScreen[0].textNortify1;
          this.strorePayNotify = configScreen[0].textArea2;
          this.deliveryTitle = configScreen[0].textTitle3;
          this.isCheckDiscount = configScreen[0].checkBoxFlg2;
          this.textNoteBtnConfirmOrder = configScreen[0].textNortify2;
          this.textBtnConfirmOrder = configScreen[0].textButton1;

          if (this.isCheckReceiveWay == this.ITEM_SHOW) {
            this.receiveWay = this.IN_SHOP;
          }
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
    // パラメーター又はlocalStorageから商品データを取得する
    getDataProduct() {
      if (this.$route.params.dataProduct !== undefined) {
        this.dataProduct = this.$route.params.dataProduct;
        localStorage.setItem(
          "dataProduct",
          JSON.stringify(this.$route.params.dataProduct)
        );
      } else if (localStorage.getItem("dataProduct") != null) {
        this.dataProduct = JSON.parse(localStorage.getItem("dataProduct"));
      } else {
        this.$router.push({ name: "404" });
      }
    },
    // 予約データを取得する
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

        this.isAddressOther = JSON.parse(
          localStorage.getItem("dataOrder")
        ).isAddressOther;
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
    },
    // paymentWayを取得する
    getPaymentWay() {
      this.paymentWay = JSON.parse(
        localStorage.getItem("dataOrder")
      ).paymentWay;
    },
    // 店舗情報を取得する
    async getShopInfo() {
      if (localStorage.getItem("shopInfo") != null) {
        this.shopInfo = JSON.parse(localStorage.getItem("shopInfo"));
        this.areaSelect = this.shopInfo.areaCd;
        this.shopSelect = this.shopInfo.shopCd;
        await this.changeAreaSelect();
        this.isShowShop = true;
      } else {
        this.isShowShop = false;
      }
    },
    // 拠点一覧
    async listArea() {
      let lstArea = await this.getParameterByCd(this.KBN_AREA);
      if (lstArea.length > 0) {
        this.lstArea.push({
          value: "",
          text: this.msgAreaOrShopPleaseSelect,
          disabled: true,
        });
        for (var i = 0; i < lstArea.length; i++) {
          this.lstArea.push({
            value: lstArea[i].kbnValue,
            text: lstArea[i].kbnValueName,
          });
        }
      }
    },
    // 都道府県一覧
    async listProvince() {
      let lstProvince = await this.getParameterByCd(this.KBN_PROVINCE);
      if (lstProvince.length > 0) {
        this.lstProvince.push({
          value: "",
          text: "",
          disabled: true,
        });
        for (var i = 0; i < lstProvince.length; i++) {
          this.lstProvince.push({
            value: lstProvince[i].kbnValue,
            text: lstProvince[i].kbnValueName,
          });
        }
      }
    },
    // 店舗選択
    changeShopSelect() {
      if (this.shopSelect === "00") {
        this.areaSelect = "";
        this.shopSelect = "";
        this.lstShop = [];
        this.shopSelectError = "";
        this.isShowShop = false;
      } else if (this.shopSelect !== undefined) {
        this.shopSelectError = "";
      }
    },
    // エリア選択
    async changeAreaSelect() {
      this.loading = true;
      // エラーメッセージ削除

      this.shopSelectError = "";
      var area2 = this.lstArea.filter((item) => item.value === this.areaSelect);

      if (this.shopSelect !== "00") {
        const config = {
          method: "get",
          url: "api/Order/GetListShop",
          params: {
            area2Cd: this.areaSelect,
            bussinessCd: this.bussinessCd,
          },
        };

        try {
          const lstShopByArea = await require("axios")(config);
          this.lstShop = [];
          this.lstShop.push(
            { value: "00", text: "他のエリアを選択する" },
            {
              value: "",
              text: "---- " + area2[0].text + " ----",
              disabled: true,
            }
          );

          if (lstShopByArea.data !== null && lstShopByArea.data.length > 0) {
            for (var i = 0; i < lstShopByArea.data.length; i++) {
              this.lstShop.push({
                value: lstShopByArea.data[i].shopCd,
                text: lstShopByArea.data[i].shopName,
              });
            }
          }
          this.isShowShop = true;
        } catch (error) {
          console.log(error);
        } finally {
          this.loading = false;
        }
      } else {
        this.shopSelect = "";
      }
    },
    // 商品受取方法を変更する
    changeReceiveProduct() {
      this.isAddressOther = true;
      this.paymentWay = null;
      this.shopSelectError = "";
      this.areaSelect = "";
      this.shopSelect = "";
      this.isShowShop = false;

      if (this.receiveWay === this.SHIPPING) {
        this.clearCustRecieveInfoErr();
        this.custRecieveInfo = {};
      }
    },
    // 住所を変更する
    changeAddressOther() {
      if (this.isAddressOther || this.receiveWay === this.IN_SHOP) {
        this.clearCustRecieveInfoErr();
        this.custRecieveInfo = {};
      }
    },
    // 注文入力確認ボタン押下処理
    sendDataOrder() {
      if (this.isValidateAll()) {
        this.custInfo.provinceName = this.lstProvince.filter(
          (x) => x.value === this.custInfo.province
        )[0].text;

        if (
          this.receiveWay === null ||
          this.paymentWay === null ||
          (this.receiveWay !== null && this.receiveWay === this.IN_SHOP) ||
          (this.paymentWay !== null && this.paymentWay === this.PAY_IN_SHOP)
        ) {
          if (
            this.paymentWay !== null &&
            this.paymentWay === this.PAY_IN_SHOP
          ) {
            this.setCustDeliveryInfo();
          } else {
            this.custRecieveInfo = {};
          }
          this.setSelectedHistShop();
        } else {
          localStorage.removeItem("shopInfo");
          this.setCustDeliveryInfo();
        }

        this.$router.push({
          name: "order-confirm",
          params: {
            custInfo: this.custInfo,
            custRecieveInfo: this.custRecieveInfo,
            receiveWay: this.receiveWay,
            paymentWay: this.paymentWay,
            isDiscount: this.isDiscount,
            isAddressOther: this.isAddressOther,
          },
        });
      }
    },
    // localStorageに店舗情報を設定する
    setSelectedHistShop() {
      let shopInfo = {
        areaCd: this.areaSelect,
        shopCd: this.shopSelect,
        shopName: this.lstShop.filter((x) => x.value === this.shopSelect)[0]
          .text,
      };
      localStorage.setItem("shopInfo", JSON.stringify(shopInfo));
    },
    // お客様の商品受取情報を設定する
    setCustReceiveInfo() {
      this.custRecieveInfo.kanjiFamilyName = this.custInfo.kanjiFamilyName;
      this.custRecieveInfo.kanjiFirstName = this.custInfo.kanjiFirstName;
      this.custRecieveInfo.kanaFamilyName = this.custInfo.kanaFamilyName;
      this.custRecieveInfo.kanaFirstName = this.custInfo.kanaFirstName;
      this.custRecieveInfo.zipCd = this.custInfo.zipCd;
      this.custRecieveInfo.zipCodeDsp = this.custInfo.zipCodeDsp;
      this.custRecieveInfo.province = this.custInfo.province;
      this.custRecieveInfo.provinceName = this.custInfo.provinceName;
      this.custRecieveInfo.adrCd1 = this.custInfo.adrCd1;
      this.custRecieveInfo.adrCd2 = this.custInfo.adrCd2;
      this.custRecieveInfo.adrCd3 = this.custInfo.adrCd3;
      this.custRecieveInfo.phoneNumber = this.custInfo.phoneNumber;
    },
    setCustDeliveryInfo() {
      if (!this.isAddressOther) {
        this.setCustReceiveInfo();
      } else {
        this.custRecieveInfo.provinceName = this.lstProvince.filter(
          (x) => x.value === this.custRecieveInfo.province
        )[0].text;
      }
    },
    // 全てのデータのバリデーションチェックを行う
    isValidateAll() {
      this.checkValidKanjiFamily();
      this.checkValidKanjiFirst();
      this.checkValidKanaFamily();
      this.checkValidKanaFirst();
      this.checkValidZipCode(false);
      this.checkValidProvince();
      this.checkValidAdrCd1();
      this.checkValidAdrCd2();
      this.checkValidAdrCd3();
      this.checkValidPhoneNumber();
      this.checkValidMailAddress();
      this.checkValidMailAddressConfirm();

      // 早期割引特典が表示される場合、早期割引特典のバリデーションチェックを行う
      if (this.isCheckDiscount == this.ITEM_SHOW) {
        this.checkValidSelectDiscount();
      }

      if (
        this.receiveWay === null ||
        (this.receiveWay !== null && this.receiveWay === this.IN_SHOP)
      ) {
        this.checkValidShopSelect();
      } else if (this.receiveWay === this.SHIPPING) {
        this.checkValidSelectPaymentWay();

        if (this.isAddressOther) {
          this.checkValidKanjiFamilyCustRecieve();
          this.checkValidKanjiFirstCustRecieve();
          this.checkValidKanaFamilyCustRecieve();
          this.checkValidKanaFirstCustRecieve();
          this.checkValidZipCodeCustRecieve(false);
          this.checkValidProvinceCustRecieve();
          this.checkValidAdrCd1CustRecieve();
          this.checkValidAdrCd2CustRecieve();
          this.checkValidAdrCd3CustRecieve();
          this.checkValidPhoneNumberCustRecieve();
        }

        if (this.paymentWay !== null && this.paymentWay === this.PAY_IN_SHOP) {
          this.checkValidShopSelect();
        }
      }

      if (
        this.shopSelectError === "" &&
        this.discountError === "" &&
        this.isValidateCust()
      ) {
        if (
          this.receiveWay === this.SHIPPING &&
          this.isAddressOther &&
          this.paymentWayError === "" &&
          this.isValidateCustRecieve()
        ) {
          return true;
        } else if (
          (this.receiveWay === null || this.receiveWay === this.IN_SHOP) &&
          this.shopSelectError === ""
        ) {
          return true;
        } else if (
          this.receiveWay === this.SHIPPING &&
          this.paymentWay === this.TRANSFER
        ) {
          return true;
        } else if (
          this.paymentWay === this.PAY_IN_SHOP &&
          this.shopSelectError === ""
        ) {
          return true;
        } else {
          // 画面上のエラー位置に遷移する
          this.gotoViewErr();
          return false;
        }
      } else {
        // 画面上のエラー位置に遷移する
        this.gotoViewErr();
        return false;
      }
    },
    // 受取人のエラー情報を削除する
    isValidateCust() {
      if (
        this.kanjiFamilyError === "" &&
        this.kanjiFirstError === "" &&
        this.kanaFamilyError === "" &&
        this.kanaFirstError === "" &&
        this.zipCodeError === "" &&
        this.provinceError === "" &&
        this.adrCd1Error === "" &&
        this.adrCd2Error === "" &&
        this.adrCd3Error === "" &&
        this.phoneNumberError === "" &&
        this.mailAddressError === "" &&
        this.mailAddressConfirmError === ""
      ) {
        return true;
      } else {
        return false;
      }
    },
    // check validate Cust Recieve infor
    isValidateCustRecieve() {
      if (
        this.kanjiFamilyCustRecieveError === "" &&
        this.kanjiFirstCustRecieveError === "" &&
        this.kanaFamilyCustRecieveError === "" &&
        this.kanaFirstCustRecieveError === "" &&
        this.zipCodeCustRecieveError === "" &&
        this.provinceCustRecieveError === "" &&
        this.adrCd1CustRecieveError === "" &&
        this.adrCd2CustRecieveError === "" &&
        this.adrCd3CustRecieveError === "" &&
        this.phoneNumberCustRecieveError === ""
      ) {
        return true;
      } else {
        return false;
      }
    },
    // go to location in view error
    gotoViewErr() {
      if (this.receiveWay === this.SHIPPING && this.isAddressOther) {
        if (
          this.kanjiFamilyCustRecieveError !== "" ||
          this.kanjiFirstCustRecieveError !== "" ||
          this.kanaFamilyCustRecieveError !== "" ||
          this.kanaFirstCustRecieveError !== ""
        ) {
          this.$vuetify.goTo("#cust-recieve-name");
          return;
        } else if (
          this.zipCodeCustRecieveError !== "" ||
          this.provinceCustRecieveError !== "" ||
          this.adrCd1CustRecieveError !== "" ||
          this.adrCd2CustRecieveError !== "" ||
          this.adrCd3CustRecieveError !== ""
        ) {
          this.$vuetify.goTo("#zipcode-cust-recieve");
          return;
        }
      } else {
        if (
          this.kanjiFamilyError !== "" ||
          this.kanjiFirstError !== "" ||
          this.kanaFamilyError !== "" ||
          this.kanaFirstError !== ""
        ) {
          this.$vuetify.goTo("#cust-name");
          return;
        } else if (
          this.zipCodeError !== "" ||
          this.provinceError !== "" ||
          this.adrCd1Error !== "" ||
          this.adrCd2Error !== "" ||
          this.adrCd3Error !== ""
        ) {
          this.$vuetify.goTo("#zipcode");
          return;
        }
      }

      if (this.receiveWay === this.IN_SHOP) {
        this.$vuetify.goTo("#shop");
      }
    },
    // 商品詳細画面へ戻るルーター
    backProductDetail() {
      this.$router.push({
        name: "product-detail",
        params: {
          clear: "clear",
        },
      });
    },
    // clear customer recivce info error
    clearCustRecieveInfoErr() {
      this.kanjiFamilyCustRecieveError = "";
      this.kanjiFirstCustRecieveError = "";
      this.kanaFamilyCustRecieveError = "";
      this.kanaFirstCustRecieveError = "";
      this.zipCodeCustRecieveError = "";
      this.provinceCustRecieveError = "";
      this.adrCd1CustRecieveError = "";
      this.adrCd2CustRecieveError = "";
      this.adrCd3CustRecieveError = "";
      this.phoneNumberCustRecieveError = "";
    },
  },
};
</script>
