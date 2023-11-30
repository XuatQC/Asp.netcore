<template>
  <div>
    <VueElementLoading
      :active="loading"
      spinner="ring"
      color="var(--success)"
    />
    <div class="main-card">
      <!-- Search bar -->
      <div class="card card-body">
        <div class="row">
          <div class="col-md-4">
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search">予約受付番号</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-input v-model="searchOrder.orderId" placeholder="" single-line hide-details />
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search">お客様名前</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-input v-model="searchOrder.custName" placeholder="" single-line hide-details />
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search">お電話番号</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-input v-model="searchOrder.phoneNumber" placeholder="" single-line hide-details />
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search">メールアドレス</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-input v-model="searchOrder.mailAddress" placeholder="" single-line hide-details />
              </div>
            </div>
          </div>
          <div class="col-md-4">
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search">ブランド名</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-select :options="brandOptions" v-model="searchOrder.brandCd"></b-form-select>
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search">商品受け取り方法</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-select :options="receiveWayOptions" v-model="searchOrder.receiveWay"></b-form-select>
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search">早期割引特典</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-select :options="discountOptions" v-model="searchOrder.isDiscount"></b-form-select>
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search">入金・受取店舗</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-input v-model="searchOrder.shopName" placeholder="" single-line hide-details />
              </div>
            </div>
          </div>
          <div class="col-md-4">
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-5 label-search fix-padding-search">予約メールステータス</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-select :options="mailStatusOptions" v-model="searchOrder.orderMailStatus"></b-form-select>
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-5 label-search fix-padding-search">入金リマインドステータス</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-select :options="mailStatusOptions" v-model="searchOrder.remindMailStatus"></b-form-select>
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-5 label-search fix-padding-search">入金ステータス</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-select :options="mailStatusOptions" v-model="searchOrder.finishedMailStatus"></b-form-select>
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-5 label-search fix-padding-search">予約ステータス</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-select :options="rsvStatusOptions" v-model="searchOrder.status"></b-form-select>
              </div>
            </div>
          </div>
        </div>
        <div class="row col-md-12 btn-grp-search">
          <div class="col-md-1 offset-md-10" style="text-align: right;">
            <button type="button" class="btn btn-primary btn-search" @click="search()">
              検索
            </button>
          </div>
          <div class="col-md-1" style="text-align: right;">
            <button type="button" class="btn btn-secondary btn-clear" @click="clearSearch()">
              クリア
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="main-card">
      <div class="card card-body">
        <!-- Button group -->
        <div class="row button-group col-md-12" v-if="!isUserReadOnly || isUserCanDownCsv">
          <div class="button-item" v-if="!isUserReadOnly">
            <button
              :disabled="resendDisabled"
              @click="resendReserveMailDialog()"
              type="submit"
              class="btn btn-primary button-width button-disabled"
            >
              予約メール再送
            </button>
          </div>
          <div class="button-item" v-if="!isUserReadOnly">
            <button
              :disabled="remindDisabled"
              @click="sendRemindMailDialog()"
              type="submit"
              class="btn btn-primary button-width button-disabled"
            >
              入金リマインドメール送信
            </button>
          </div>
          <div class="button-item" v-if="!isUserReadOnly">
            <button
              :disabled="cancelDisabled"
              @click="cancelOrderDialog()"
              type="submit"
              class="btn btn-primary button-width button-disabled"
            >
              予約キャンセル
            </button>
          </div>
          <div class="button-item" v-if="isUserCanDownCsv">
            <button
              :disabled="downloadCSVDisabled"
              type="submit"
              @click="downloadCSV()"
              class="btn btn-primary button-width button-disabled"
            >
              CSV出力
            </button>
          </div>
          <div class="button-item" v-if="!isUserReadOnly">
            <button
              :disabled="memoDisabled"
              type="submit"
              @click="memoDialog(true, null)"
              class="btn btn-primary button-width button-disabled"
            >
              メモ追加
            </button>
          </div>
        </div>
        <div class="pagination">
          <div class="col-md-12" v-if="totalItem > 0">
            <div class="row" :style="`float: right; ${totalItem > itemsPerPage ? 'margin-right: -22px;' : ''}`">
              <p :class="`${totalItem > itemsPerPage ? 'page-info' : 'no-pagination'}`">
                全 {{ totalItem }} 件中 {{ pageStart }} 件 〜 {{ pageStop }} 件を表示
              </p>
              <v-pagination
                v-if="totalItem > itemsPerPage"
                @input="changePage(page)"
                v-model="page"
                :total-visible="7"
                :length="totalPage"
              ></v-pagination>
            </div>
          </div>
          <div v-else style="height: 42px"></div>
        </div>
        <!-- Button group -->
        <v-data-table
          v-model="orderSelected"
          :headers="headersLstOrder"
          :items="lstOrder"
          item-key="orderId"
          hide-default-footer
          show-select
          :items-per-page="itemsPerPage"
          :no-results-text="msgNoData"
          :no-data-text="msgNoData"
          class="table-order"
          @toggle-select-all="selectAllOrder"
          @update:options="changeSort"
        >
          <template v-slot:item="{ item }">
            <tr>
              <td @click="toggleOrder(item)">
                <v-checkbox
                  class="pa-0 ma-0"
                  :ripple="false"
                  :input-value="orderSelected"
                  :value="item"
                  hide-details
                >
                </v-checkbox>
              </td>
              <td class="text-left">
                <a @click="openOrderDialog(item)" class="router">{{
                  item.orderId
                }}</a>
              </td>
              <td>
                <a @click="openCustDialog(item)" class="router"
                  >{{ item.kanjiFamilyName + item.kanjiFirstName }}</a
                >
              </td>
              <td>
                {{ maskPhone(item.phoneNumber) }}
              </td>
              <td class="text-left">{{ maskMail(item.mailAddress) }}</td>
              <td class="text-left">
                <a @click="openBrandNameDialog(item)" class="router">{{ item.brandName }}</a>
              </td>
              <td class="text-left">
                <a @click="openConfirmUpdateStatusOrderDialog(item, true)"
                    class="router"
                    v-if="item.status === RSV_STATUS_PAID && item.receiveWay === SHIPPING && !isUserReadOnly">
                    {{item.receiveWayName}}
                </a>
                <span v-else>{{ item.receiveWayName }}</span>
              </td>
              <td class="text-left">
                <a @click="openConfirmUpdateStatusOrderDialog(item, false)"
                    class="router"
                    v-if="item.status === RSV_STATUS_ORDER && item.paymentWay === TRANSFER && !isUserReadOnly">
                    {{item.paymentWayName}}
                </a>
                <span v-else>{{ item.paymentWayName }}</span>
              </td>
              <td class="text-left">{{ item.shopName }}</td>
              <td class="text-left">
                {{ item.zipCodeDspMCustReceive !== null ? "〒"
                                                          + item.zipCodeDspMCustReceive
                                                          + item.provinceNameMCustReceive
                                                          + item.adrCd1MCustReceive
                                                          + item.adrCd2MCustReceive
                                                          + (item.adrCd3MCustReceive != null ? item.adrCd3MCustReceive : "")
                                                        : "" }}
              </td>
              <td class="text-left">
                {{ item.discountName }}
              </td>
              <td class="text-left">
                {{ item.orderMailStatusName }}
              </td>
              <td class="text-left">
                {{ item.remindMailStatusName }}
              </td>
              <td class="text-left">
                {{ item.finishedMailStatusName }}
              </td>
              <td class="text-left">{{ item.statusName }}</td>
              <td class="text-left">
                <a @click="mailBounceDialog(item)" class="router"
                  >{{ item.mailBounceName }}
                </a>
              </td>
              <td class="text-left">
                <a @click="memoDialog(false, item)" class="router"
                  >{{ item.memo }}
                </a>
              </td>
            </tr>
          </template>
        </v-data-table>
        <!-- update order dialog-->
        <v-dialog v-model="dialogOrderUpdate" max-width="1000px">
          <v-card>
            <VueElementLoading
              :active="loadingPopup"
              spinner="ring"
              color="var(--success)"
            />
            <v-card-title>
              <span class="headline">予約情報変更</span>
            </v-card-title>
            <v-card-text>
              <div class="main-card">
                <!-- cust receive info -->
                <div v-if="orderDetail.receiveWay === this.SHIPPING">
                  <b-row>
                    <b-col>
                      <div>
                        <b-row>
                          <b-col>
                            <label id="title-shipping"
                              ><strong>配送情報</strong></label
                            >
                          </b-col>
                        </b-row>
                        <!-- Kanji Family Name -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>姓（漢字）</strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-5 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="kanjiFamilyName"
                              v-model="orderDetail.kanjiFamilyNameMCustReceive"
                              @keyup="checkValidKanjiFamilyCustRecieve()"
                              autocomplete="off"
                            />
                            <span v-else>{{orderDetail.kanjiFamilyNameMCustReceive}}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="kanjiFamilyCustRecieveError != ''"
                              >{{ this.kanjiFamilyCustRecieveError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- Kanji First Name -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>名（漢字）</strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-5 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="kanjiFirstName"
                              v-model="orderDetail.kanjiFirstNameMCustReceive"
                              @keyup="checkValidKanjiFirstCustRecieve()"
                              autocomplete="off"
                            />
                            <span v-else>{{ orderDetail.kanjiFirstNameMCustReceive }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="kanjiFirstCustRecieveError != ''"
                              >{{ this.kanjiFirstCustRecieveError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- Kana family Name -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>姓（かな）</strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-5 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="kanaFamilyName"
                              v-model="orderDetail.kanaFamilyNameMCustReceive"
                              @keyup="checkValidKanaFamilyCustRecieve()"
                              autocomplete="off"
                            />
                            <span v-else>{{ orderDetail.kanaFamilyNameMCustReceive }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="kanaFamilyCustRecieveError != ''"
                              >{{ this.kanaFamilyCustRecieveError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- Kana First Name -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>名（かな）</strong
                              ><span v-if="!isUserReadOnly" class="text-danger"> *</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-5 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="kanaFirstName"
                              v-model="orderDetail.kanaFirstNameMCustReceive"
                              @keyup="checkValidKanaFirstCustRecieve()"
                              autocomplete="off"
                            />
                            <span v-else>{{ orderDetail.kanaFirstNameMCustReceive }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="kanaFirstCustRecieveError != ''"
                              >{{ this.kanaFirstCustRecieveError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- zipCd -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>郵便番号 </strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-5 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="zipCode"
                              v-model="orderDetail.zipCdMCustReceive"
                              v-on:keypress="isNumber()"
                              @keyup="checkValidZipCodeCustRecieve(true)"
                              autocomplete="off"
                            />
                            <span v-else>{{ orderDetail.zipCodeDspMCustReceive }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="zipCodeCustRecieveError != ''"
                              >{{ this.zipCodeCustRecieveError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- Province -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>都道府県 </strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-select
                              v-if="!isUserReadOnly"
                              :disabled="isCancel"
                              class="province-area col-lg-5 col-md-12"
                              name="province"
                              v-model="orderDetail.provinceMCustReceive"
                              :options="lstProvince"
                              @change="checkValidProvinceCustRecieve()"
                            ></b-form-select>
                            <span v-else>{{ orderDetail.provinceNameMCustReceive }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="provinceCustRecieveError != ''"
                              >{{ this.provinceCustRecieveError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- address 1 -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>住所１</strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-5 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="adrCd1"
                              v-model="orderDetail.adrCd1MCustReceive"
                              @keyup="checkValidAdrCd1CustRecieve()"
                              autocomplete="off"
                            />
                            <span v-else>{{ orderDetail.adrCd1MCustReceive }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="adrCd1CustRecieveError != ''"
                              >{{ this.adrCd1CustRecieveError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- address 2 -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>住所2 </strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-5 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="adrCd2"
                              v-model="orderDetail.adrCd2MCustReceive"
                              @keyup="checkValidAdrCd2CustRecieve()"
                              autocomplete="off"
                            />
                            <span v-else>{{ orderDetail.adrCd2MCustReceive }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="adrCd2CustRecieveError != ''"
                              >{{ this.adrCd2CustRecieveError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- address 3 -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>住所３</strong></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-5 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              v-model="orderDetail.adrCd3MCustReceive"
                              @keyup="checkValidAdrCd3CustRecieve()"
                              autocomplete="off"
                            />
                            <span v-else>{{ orderDetail.adrCd3MCustReceive }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="adrCd3CustRecieveError != ''"
                              >{{ this.adrCd3CustRecieveError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- phone number -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>電話番号 </strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-5 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="phoneNumber"
                              v-model="orderDetail.phoneNumberMCustReceive"
                              v-on:keypress="isNumber()"
                              @keyup="checkValidPhoneNumberCustRecieve()"
                              autocomplete="off"
                            />
                            <span v-else>{{ orderDetail.phoneNumberMCustReceive }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="phoneNumberCustRecieveError != ''"
                              >{{ this.phoneNumberCustRecieveError }}</span
                            >
                          </b-col>
                        </b-row>
                      </div>
                    </b-col>
                  </b-row>
                </div>
                <div style="margin-top: 8px">
                  <b-row>
                    <b-col>
                      <div>
                        <!-- Cust Name & OrderId-->
                        <div class="col-md-10">
                          <div class="row">
                            <div class="col-md-6">
                              <div class="row">
                                <label
                                  class="col-md-5 fix-padding-lable color-black"
                                >
                                  <strong>予約受付番号</strong>
                                </label>
                                <span>{{ orderDetail.orderId }}</span>
                              </div>
                            </div>
                            <div class="col-md-5">
                              <div class="row">
                                <label
                                  class="col-md-4 fix-padding-lable color-black"
                                >
                                  <strong>お客様名前</strong>
                                </label>
                                <span>{{
                                  orderDetail.kanjiFamilyName +
                                  orderDetail.kanjiFirstName
                                }}</span>
                              </div>
                            </div>
                          </div>
                        </div>
                        <br />
                        <!-- Shop Name-->
                        <div
                          class="col-md-6"
                          v-if="orderDetail.receiveWay === null ||
                                orderDetail.receiveWay === IN_SHOP ||
                                (orderDetail.receiveWay === SHIPPING && orderDetail.paymentWay === PAY_IN_SHOP)"
                        >
                          <div class="row">
                            <label class="col-md-4 fix-lable-shop color-black">
                              <strong>入金・受取店舗</strong>
                            </label>
                            <b-form-select
                              :disabled="isCancel"
                              class="shop-area col-md-8"
                              v-if="isShowShop && !isUserReadOnly"
                              v-model="shopSelect"
                              :options="lstShop"
                              @change="changeShopSelect()"
                            ></b-form-select>
                            <span v-else-if="isUserReadOnly" class="col-md-8" style="padding-left: 7px;">{{ orderDetail.shopName }}</span>
                            <b-form-select
                              :disabled="isCancel"
                              class="shop-area col-md-8"
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
                          <br />
                        </div>
                        <div hidden v-if="checkChange"></div>
                        <!-- table order detail-->
                        <div>
                          <div class="row mb-1">
                            <div
                              class="col-md-12 col-lg-12 text-xs-right"
                              v-if="lstOrderDetail.length > this.itemsPerPageDetail"
                            >
                              <div class="row" style="float: right; padding-right: 6px">
                                <p
                                  style="
                                    margin-bottom: unset;
                                    line-height: 3;
                                    padding-right: 10px;
                                  "
                                >
                                  全 {{ itemsLengthOrdDetail }} 件中 {{ pageStartOrdDetail }} 件 〜
                                  {{ pageStopOrdDetail }} 件を表示
                                </p>
                                <v-pagination
                                  :total-visible="7"
                                  v-model="pageOrdDetail"
                                  :length="pageCount"
                                ></v-pagination>
                              </div>
                            </div>
                            <div v-else style="height: 10px"></div>
                          </div>
                          <v-data-table
                            :headers="headersLstOrderDetail"
                            :items="lstOrderDetail"
                            hide-default-footer
                            item-key="orderDtlId"
                            :page.sync="pageOrdDetail"
                            :items-per-page="this.itemsPerPageDetail"
                            @page-count="pageCount = $event"
                            :no-results-text="msgNoData"
                            :no-data-text="msgNoData"
                          >
                            <template v-slot:item="{ item }">
                              <!-- <tr :style="`${item.backgroundErr !== undefined ? 'background-color: #f75d5d;':''}`"> -->
                              <tr>
                                <td class="product-group">
                                  <span class="product-name">{{ item.productName }}</span>
                                  <br/>
                                  <span class="product-code">{{ item.productCd }}</span>
                                </td>
                                <td class="text-left">
                                  <b-form-select
                                    :style="`${item.backgroundErr !== undefined ? 'border-color: red;':''}`"
                                    v-if="!isUserReadOnly"
                                    v-model="item.colorName"
                                    :disabled="isCancel"
                                    :options="item.lstColorName"
                                    @change="changeColor(item)"
                                  ></b-form-select>
                                  <span v-else>{{ item.colorName }}</span>
                                </td>
                                <td class="text-left">
                                  <b-form-select
                                    :style="`${item.backgroundErr !== undefined ? 'border-color: red;':''}`"
                                    v-if="!isUserReadOnly"
                                    v-model="item.sizeName"
                                    :disabled="isCancel"
                                    :options="item.lstSizeName"
                                    @change="changeSize(item)"
                                  ></b-form-select>
                                  <span v-else>{{ item.sizeName }}</span>
                                </td>
                                <td class="text-left">
                                  <b-form-select
                                    :style="`${item.backgroundErr !== undefined ? 'border-color: red;':''}`"
                                    v-if="!isUserReadOnly"
                                    class="product-count"
                                    v-model="item.quantity"
                                    :disabled="isCancel"
                                    :options="item.lstProductInventory"
                                    @change="changePrice(item)"
                                  ></b-form-select>
                                  <span v-else>{{ item.quantity }}</span>
                                </td>
                                <td class="text-left">
                                  ¥{{ formatPrice(item.subTotal) }}
                                </td>
                                <td class="text-left">
                                  <v-btn
                                    v-if="!isUserReadOnly"
                                    :disabled="isCancel"
                                    :loading="loadingBtn"
                                    @click="removeOrdDetailItem(item)"
                                    class="btn btn-danger"
                                  >
                                    削除
                                  </v-btn>
                                  <div v-else style="width: 67px;"></div>
                                </td>
                              </tr>
                            </template>
                          </v-data-table>
                        </div>
                        <div class="row col-md-8 offset-md-4">
                          <div class="col-md-6 color-black">
                            <strong>ご予約合計金額</strong>
                          </div>
                          <td class="col-md-2 color-black padding-price" :id="`${isUserReadOnly ? 'padding-readonly':''}`">
                            <strong>{{ orderDetail.totalQuantity }}</strong>
                          </td>
                          <div class="col-md-4 color-black padding-price">
                            <span class="price"
                              ><strong
                                >¥{{ formatPrice(orderDetail.total) }}</strong
                              ></span
                            >
                            <span class="tax"><strong>(税込)</strong></span>
                          </div>
                        </div>
                        <div class="row col-md-8 offset-md-4 text-error">
                          <span
                            class="invalid-feedback d-block"
                            v-if="sizeOrColorError != ''"
                            >{{ this.sizeOrColorError }}</span
                          >
                          <span
                            class="invalid-feedback d-block"
                            v-if="maxAmountCanOrderError != ''"
                            >{{ this.maxAmountCanOrderError }}</span
                          >
                        </div>
                        <br />
                        <!-- bottom button-->
                        <div>
                          <v-card-actions style="width: 100%">
                            <v-spacer></v-spacer>
                            <button
                              class="btn btn-primary"
                              @click="dialogOrderUpdate = false"
                            >
                              キャンセル
                            </button>
                            <v-spacer></v-spacer>
                            <button
                              v-if="!isUserReadOnly"
                              :disabled="isCancel"
                              @click="showDialogOrderUpdateConfirm()"
                              style="width: 90px"
                              class="btn btn-danger"
                            >
                              変更
                            </button>
                            <v-spacer></v-spacer>
                          </v-card-actions>
                        </div>
                      </div>
                      <div style="margin-top: 10px;">
                        <div class="row">
                          <div class="col-md-12">
                            <div class="row">
                              <label class="col-md-2 color-black" style="margin-bottom: unset;">
                                <strong>早期割引特典:</strong>
                              </label>
                              <span class="col-md-10">{{ orderDetail.discountName }}</span>
                            </div>
                          </div>
                        </div>
                        <div class="row">
                          <div class="col-md-12">
                            <div class="row">
                              <label class="col-md-2 color-black">
                                <strong>メモ:</strong>
                              </label>
                              <p class="col-md-10">
                                <span>{{ orderDetail.memo }}</span>
                              </p>
                            </div>
                          </div>
                        </div>
                      </div>
                    </b-col>
                  </b-row>
                  <b-row>
                    <b-col>
                      <!-- order history -->
                      <OrderHistory :lstOrderHistory="lstOrderHistory" />
                    </b-col>
                  </b-row>
                </div>
              </div>
            </v-card-text>
          </v-card>
        </v-dialog>
        <!-- update customer dialog -->
        <v-dialog v-model="dialogMcust" max-width="670px">
          <v-card>
            <VueElementLoading
              :active="loadingPopup"
              spinner="ring"
              color="var(--success)"
            />
            <v-card-title>
              <span class="headline">お客様情報</span>
            </v-card-title>
            <v-card-text>
              <div class="main-card">
                <!-- cust info -->
                <div>
                  <b-row>
                    <b-col>
                      <div>
                        <!-- Kanji Family Name -->
                        <b-row>
                          <b-col lg="3" md="12">
                            <label class="color-black"
                              ><strong>姓（漢字）</strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-7 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="kanjiFamilyName"
                              v-model="custInfo.kanjiFamilyName"
                              @keyup="checkValidKanjiFamily()"
                              autocomplete="off"
                            />
                            <span v-else>{{ custInfo.kanjiFamilyName }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="kanjiFamilyError != ''"
                              >{{ this.kanjiFamilyError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- Kanji First Name -->
                        <b-row>
                          <b-col lg="3" md="12">
                            <label class="color-black"
                              ><strong>名（漢字）</strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-7 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="kanjiFirstName"
                              v-model="custInfo.kanjiFirstName"
                              @keyup="checkValidKanjiFirst()"
                              autocomplete="off"
                            />
                            <span v-else>{{ custInfo.kanjiFirstName }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="kanjiFirstError != ''"
                              >{{ this.kanjiFirstError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- Kana family Name -->
                        <b-row>
                          <b-col lg="3" md="12">
                            <label class="color-black"
                              ><strong>姓（かな）</strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-7 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="kanaFamilyName"
                              v-model="custInfo.kanaFamilyName"
                              @keyup="checkValidKanaFamily()"
                              autocomplete="off"
                            />
                            <span v-else>{{ custInfo.kanjiFirstName }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="kanaFamilyError != ''"
                              >{{ this.kanaFamilyError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- Kana First Name -->
                        <b-row>
                          <b-col lg="3" md="12">
                            <label class="color-black"
                              ><strong>名（かな）</strong
                              ><span v-if="!isUserReadOnly" class="text-danger"> *</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-7 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="kanaFirstName"
                              v-model="custInfo.kanaFirstName"
                              @keyup="checkValidKanaFirst()"
                              autocomplete="off"
                            />
                            <span v-else>{{ custInfo.kanaFirstName }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="kanaFirstError != ''"
                              >{{ this.kanaFirstError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- phone number -->
                        <b-row>
                          <b-col lg="3" md="12">
                            <label class="color-black"
                              ><strong>電話番号 </strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-7 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="phoneNumber"
                              v-model="custInfo.phoneNumber"
                              v-on:keypress="isNumber()"
                              @keyup="checkValidPhoneNumber()"
                              autocomplete="off"
                            />
                            <span v-else>{{ custInfo.kanaFirstName }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="phoneNumberError != ''"
                              >{{ this.phoneNumberError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- zipCd -->
                        <b-row>
                          <b-col lg="3" md="12">
                            <label class="color-black"
                              ><strong>郵便番号 </strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-7 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="zipCode"
                              v-model="custInfo.zipCd"
                              v-on:keypress="isNumber()"
                              @keyup="checkValidZipCode(true)"
                              autocomplete="off"
                            />
                            <span v-else>{{ custInfo.zipCodeDsp }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="zipCodeError != ''"
                              >{{ this.zipCodeError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- Province -->
                        <b-row>
                          <b-col lg="3" md="12">
                            <label class="color-black"
                              ><strong>都道府県 </strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-select
                              v-if="!isUserReadOnly"
                              :disabled="isCancel"
                              class="province-area col-lg-7 col-md-12"
                              name="province"
                              v-model="custInfo.province"
                              :options="lstProvince"
                              @change="checkValidProvince()"
                            ></b-form-select>
                            <span v-else>{{ custInfo.provinceName }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="provinceError != ''"
                              >{{ this.provinceError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- address 1 -->
                        <b-row>
                          <b-col lg="3" md="12">
                            <label class="color-black"
                              ><strong>住所１</strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-7 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="adrCd1"
                              v-model="custInfo.adrCd1"
                              @keyup="checkValidAdrCd1()"
                              autocomplete="off"
                            />
                            <span v-else>{{ custInfo.adrCd1 }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="adrCd1Error != ''"
                              >{{ this.adrCd1Error }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- address 2 -->
                        <b-row>
                          <b-col lg="3" md="12">
                            <label class="color-black"
                              ><strong>住所2 </strong
                              ><span v-if="!isUserReadOnly" class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-7 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="adrCd2"
                              v-model="custInfo.adrCd2"
                              @keyup="checkValidAdrCd2()"
                              autocomplete="off"
                            />
                            <span v-else>{{ custInfo.adrCd2 }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="adrCd2Error != ''"
                              >{{ this.adrCd2Error }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- address 3 -->
                        <b-row>
                          <b-col lg="3" md="12">
                            <label class="color-black"
                              ><strong>住所３(建物名等）</strong></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-7 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              v-model="custInfo.adrCd3"
                              @keyup="checkValidAdrCd3()"
                              autocomplete="off"
                            />
                            <span v-else>{{ custInfo.adrCd3 }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="adrCd3Error != ''"
                              >{{ this.adrCd3Error }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- mail address -->
                        <b-row>
                          <b-col lg="3" md="12">
                            <label class="color-black"
                              ><strong>メールアドレス</strong
                              ><span v-if="!isUserReadOnly" class="text-danger"> *</span></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <b-form-input
                              v-if="!isUserReadOnly"
                              class="col-lg-7 col-md-12"
                              :disabled="isCancel"
                              type="text"
                              name="mailAddress"
                              v-model="custInfo.mailAddress"
                              @keyup="checkValidMailAddress()"
                              autocomplete="off"
                            />
                            <span v-else>{{ custInfo.mailAddress }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="mailAddressError != ''"
                              >{{ this.mailAddressError }}</span
                            >
                          </b-col>
                        </b-row>
                      </div>
                    </b-col>
                  </b-row>
                </div>
                <!-- bottom button -->
                <div style="margin-top: 15px">
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <button
                      class="btn btn-primary"
                      @click="dialogMcust = false"
                    >
                      キャンセル
                    </button>
                    <v-spacer></v-spacer>
                    <button
                      v-if="!isUserReadOnly"
                      :disabled="isCancel"
                      @click="showDialogMcustConfirm()"
                      style="width: 90px"
                      class="btn btn-danger"
                    >
                      保存
                    </button>
                    <v-spacer></v-spacer>
                  </v-card-actions>
                </div>
              </div>
            </v-card-text>
          </v-card>
        </v-dialog>
        <!-- Mail Bounce dialog-->
        <v-dialog
          v-model="dialogMailBounce"
          max-width="750px"
        >
          <v-card>
            <v-card-title>
              <span class="headline">バウンス情報詳細</span>
            </v-card-title>
            <v-card-text>
              <div class="main-card">
                <div class="row">
                  <div class="col-md-3">
                    <label class="title-mailbouce">バウンスタイプ</label>
                  </div>
                  <div class="col-md-9">
                    {{ mailBounceInfo.mailBounceName }}
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-3">
                    <label class="title-mailbouce">コード</label>
                  </div>
                  <div class="col-md-9">
                    {{ mailBounceInfo.mailBounce }}
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-3">
                    <label class="title-mailbouce">詳細内容</label>
                  </div>
                  <div class="col-md-9">
                    {{ mailBounceInfo.mailBounceDescription }}
                  </div>
                </div>
              </div>
            </v-card-text>
          </v-card>
        </v-dialog>
        <!-- memo dialog -->
        <v-dialog v-model="dialogMemo" max-width="670px">
          <v-card>
            <v-card-title>
              <span class="headline">メモ</span>
            </v-card-title>
            <v-card-text>
              <div class="main-card">
                <!-- memo detail -->
                <div class="memo-content">
                  <v-col>
                    <v-textarea
                      v-model="memoDetail"
                      :disabled="isCancel || isUserReadOnly"
                      solo
                      rows="10"
                      @keyup="checkValidMemo()"
                    ></v-textarea>
                    <span
                      class="invalid-feedback d-block"
                      v-if="memoError != ''"
                      >{{ this.memoError }}</span
                    >
                  </v-col>
                </div>
                <!-- bottom button -->
                <div>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <button class="btn btn-primary" @click="dialogMemo = false">
                      キャンセル
                    </button>
                    <v-spacer></v-spacer>
                    <button
                      v-if="!isUserReadOnly"
                      :disabled="isCancel"
                      @click="showDialogMemoConfirm()"
                      style="width: 90px"
                      class="btn btn-danger"
                    >
                      保存
                    </button>
                    <v-spacer></v-spacer>
                  </v-card-actions>
                </div>
              </div>
            </v-card-text>
          </v-card>
        </v-dialog>
        <!-- Order update confirm dialog -->
        <v-dialog
          v-model="dialogOrderUpdateConfirm"
          max-width="470px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="title-confirm">
                予約を変更します。よろしいですか。
              </div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn
                color="primary"
                class="btn_confirm_no"
                @click="dialogOrderUpdateConfirm = false"
                style="margin-right: 15px; border: unset"
                >キャンセル
              </v-btn>
              <v-btn
                :loading="loadingBtn"
                color="warning"
                style="
                  border-radius: 10px;
                  border: unset;
                  margin-right: 15px;
                  color: black;
                "
                @click="updateReservation(false)"
                >予約変更</v-btn
              >
              <v-btn
                :loading="loadingBtn"
                color="error"
                style="border-radius: 10px; border: unset"
                @click="updateReservation(true)"
                >予約変更＆メール送信</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- Update confirm dialog-->
        <v-dialog
          v-model="dialogMcustConfirm"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="title-confirm">
                お客様情報を変更します。よろしいですか。
              </div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn class="btn_confirm_no" @click="dialogMcustConfirm = false"
                >キャンセル</v-btn
              >
              <v-btn
                class="btn_confirm_yes"
                :loading="loadingBtn"
                @click="updateCustInfo()"
                >保存</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- Resend Reserve Mail Dialog -->
        <v-dialog
          v-model="dialogResendReserveMail"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="title-confirm">
                予約メールを再送します。よろしいですか。
              </div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn
                color="primary"
                @click="dialogResendReserveMail = false"
                style="margin-right: 30px"
                >キャンセル
              </v-btn>
              <v-btn
                :loading="loadingBtn"
                color="error"
                @click="resendReserveMail()"
                >予約メール再送</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- Send Remind Mail Dialog -->
        <v-dialog
          v-model="dialogSendRemindMail"
          max-width="430px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="title-confirm">
                入金リマインドメールを送信します。よろしいですか。
              </div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn
                color="primary"
                @click="dialogSendRemindMail = false"
                style="margin-right: 30px"
                >キャンセル
              </v-btn>
              <v-btn
                :loading="loadingBtn"
                color="error"
                @click="sendRemindMail()"
                >入金リマインドメール送信</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- Cancel Order Dialog-->
        <v-dialog
          v-model="dialogCancelOrder"
          max-width="555px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="title-confirm">
                予約をキャンセルします。よろしいですか。
              </div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn
                color="primary"
                @click="dialogCancelOrder = false"
                style="margin-right: 15px"
                >キャンセル
              </v-btn>
              <v-btn
                :loading="loadingBtn"
                color="warning"
                @click="cancelOrder(false)"
                style="margin-right: 15px; color: black"
                >予約キャンセル
              </v-btn>
              <v-btn
                :loading="loadingBtn"
                color="error"
                @click="cancelOrder(true)"
                >予約キャンセル＆メール送信
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- memo confirm dialog-->
        <v-dialog
          v-model="dialogMemoConfirm"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="title-confirm">
                メモを{{
                  isAddMemo ? this.TEXT_ADD : this.TEXT_UPD
                }}します。よろしいですか。
              </div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn class="btn_confirm_no" @click="dialogMemoConfirm = false"
                >キャンセル</v-btn
              >
              <v-btn
                class="btn_confirm_yes"
                :loading="loadingBtn"
                @click="submitMemo()"
                >保存</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- Confirm Transfer dialog -->
         <v-dialog v-model="dialogConfirmUpdateStatusOrder" max-width="401px"  content-class="v-dialog-border" >
          <v-card>
            <v-card-text>
              <div class="text-center">{{ txtMesConfirm }}。よろしいですか。</div>
            </v-card-text>
            <v-card-actions style="text-align: center;display: block;">
              <v-btn class="btn_confirm_no" @click="dialogConfirmUpdateStatusOrder = false"
                >キャンセル</v-btn
              >
              <v-btn
                class="btn_confirm_yes"
                :loading="loadingBtn"
                @click="updateStatusOrder()"
                >保存</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- infor send mail error dialog-->
        <v-dialog v-model="dialogInforSendMailErr" max-width="400px">
          <v-card>
            <v-card-title>
              <span class="headline">メール送信エラー一覧</span>
            </v-card-title>
            <v-card-text>
              <div class="main-card">
                <div>
                  <table class="table table-hover">
                    <thead>
                      <tr>
                        <th style="text-align: center">予約番号</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr
                        v-for="(inforErr, index) in this.lstInforSendMailErr"
                        :key="index"
                      >
                        <td>{{ inforErr.orderId }}</td>
                      </tr>
                    </tbody>
                  </table>
                </div>
              </div>
            </v-card-text>
          </v-card>
        </v-dialog>
        <!-- Change password confirm dialog-->
        <v-dialog v-model="dialogChangePwConfirm" max-width="490px"  content-class="v-dialog-border" >
          <v-card>
            <v-card-text>
              <div class="text-center">パスワードはデフォルトのパスワードになっています。</div>
              <div class="text-center">セキュリティを確保するため、パスワードを変更してください。</div>
            </v-card-text>
            <v-card-actions style="text-align: center;display: block;">
              <v-btn
                class="btn_confirm_no"
                text
                @click="dialogChangePwConfirm = false;"
              >キャンセル</v-btn>
              <v-btn class="btn_confirm_yes"
               style="width: 120px; !important"
              text @click="gotoChangePw()">パスワード変更</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!--  dialog BrandName start -->
        <v-dialog v-model="dialogBrandName" max-width="700px">
          <v-card>
            <v-card-title>
              <span class="headline">ブランド名</span>
            </v-card-title>
            <v-card-text>
              <div>
                {{ selectedBrandName }}
              </div>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <button class="btn btn-primary" @click="dialogBrandName = false">
                キャンセル
              </button>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!--  dialog BrandName end -->
      </div>
    </div>
  </div>
</template>

<script>
import moment from "moment";
import OrderHistory from "@/components/OrderHistory";
export default {
  data() {
    return {
      loading: false,
      loadingBtn: false,
      loadingPopup: false,
      bussinessCd: "",
      updateUserCd: "",
      itemsPerPage: 0,
      itemsPerPageDetail: 0,
      totalData: 0,
      page: 1,
      pageOrdDetail: 1,
      pageCount: 0,
      sortColumnName: "",
      isDesc: true,
      checkSelectedAll: false,
      orderSelected: [],
      orderSelectedAll: [],
      orderUnSelected: [],
      areaSelect: "",
      shopSelect: "",
      shopCdBefore: "",
      isShowShop: false,
      isChangeCustReceive: false,
      lstArea: [],
      lstShop: [],
      lstOrder: [],
      lstOrderStatus: [],
      lstProvince: [],
      lstOrderDetail: [],
      oldLstOrderDetail: [],
      lstOrderHistory: {},
      lstInforSendMailErr: [],
      mailBounceInfo: {},
      transferOrder: {},
      moment: moment,
      headersLstOrder: [
        {
          text: "予約受付番号",
          sortable: true,
          align: "center",
          value: "orderId",
          width: "7%",
        },
        {
          text: "お客様名前",
          align: "center",
          sortable: false,
          width: "7%",
        },
        {
          text: "お電話番号",
          align: "center",
          sortable: false,
          width: "6%",
        },
        {
          text: "メールアドレス",
          align: "center",
          sortable: false,
          width: "6%",
        },
        {
          text: "ブランド名",
          align: "center",
          sortable: false,
          width: "5%",
        },
        {
          text: "商品受け取り方法",
          align: "center",
          sortable: false,
          width: "6%",
        },
        {
          text: "決済方法",
          align: "center",
          sortable: false,
          width: "6%",
        },
        {
          text: "入金・受取店舗",
          align: "center",
          sortable: true,
          value: "shopName",
          width: "9%",
        },
        {
          text: "配送先住所",
          align: "center",
          sortable: false,
          width: "9%",
        },
        {
          text: "早期割引特典",
          align: "center",
          sortable: false,
          width: "5%"
        },
        {
          text: "予約メール",
          align: "center",
          sortable: true,
          value: "orderMailStatus",
          width: "5%"
        },
        {
          text: "入金リマインド\nメール",
          align: "center",
          sortable: true,
          value: "remindMailStatus",
          width: "5%"
        },
        {
          text: "入金メール",
          align: "center",
          sortable: true,
          value: "finishedMailStatus",
          width: "5%"
        },
        {
          text: "予約ステータス",
          align: "center",
          sortable: true,
          value: "status",
          width: "5%"
        },
        {
          text: "バウンス情報",
          align: "center",
          sortable: false,
          width: "5%"
        },
        {
          text: "メモ",
          align: "center",
          sortable: false,
          width: "5%"
        },
      ],
      headersLstOrderDetail: [
        {
          text: "商品名",
          width: "30%",
          sortable: false,
          align: "left",
        },
        { text: "色", width: "15%", sortable: false },
        { text: "サイズ", width: "10%", sortable: false },
        {
          text: "数量",
          sortable: false,
          width: "10%",
          align: "left",
        },
        { text: "小計", width: "10%", sortable: false },

        {
          text: "",
          sortable: false,
          width: "5%",
          align: "left",
        },
      ],
      brandOptions: [{ text: "", value: "" }],
      mailStatusOptions: [
        { text: "", value: "" },
        { text: "送信済", value: "01" },
        { text: "送信エラー", value: "02" },
        { text: "再送", value: "03" },
        { text: "未送信", value: "04" },
      ],
      rsvStatusOptions: [
        { text: "", value: "" },
        { text: "予約(入金待ち)", value: "01" },
        { text: "入金完了・引渡し完了", value: "02" },
        { text: "引渡し完了", value: "04" },
        { text: "キャンセル", value: "05" },
      ],
      searchOrder: {},
      checkChange: false,
      orderDetail: {},
      oldOrderDetail: {},
      lstOrderDetailRemove: [],
      custInfo: {},
      oldCustInfo: {},
      isCancel: false,
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
      shopSelectError: "",
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
      maxAmountCanOrderError: "",
      sizeOrColorError: "",
      memoError: "",
      memoDetail: "",
      isZipCodeNotExist: null,
      isAddMemo: null,
      resendDisabled: false,
      remindDisabled: false,
      cancelDisabled: false,
      downloadCSVDisabled: false,
      memoDisabled: false,
      dialogOrderUpdate: false,
      dialogOrderUpdateConfirm: false,
      dialogMcust: false,
      dialogMcustConfirm: false,
      dialogResendReserveMail: false,
      dialogSendRemindMail: false,
      dialogCancelOrder: false,
      dialogMailBounce: false,
      dialogMemo: false,
      dialogMemoConfirm: false,
      dialogConfirmUpdateStatusOrder: false,
      dialogInforSendMailErr: false,
      dialogBrandName:false,
      histTypeValue: {},
      orderStatusKbn: {},
      mailType: {},
      selectedBrandName:'',
      dialogChangePwConfirm: false,
      txtMesConfirm: "",
      isStatusDelivery: false
    };
  },
  components: {
    OrderHistory,
  },
  computed: {
    totalPage () {
      if (
        this.itemsPerPage == null ||
        this.totalData == null
      ) {
        return 0
      }
      return Math.ceil(this.totalData / this.itemsPerPage)
    },
    pageStop () {
      if (this.page < Math.ceil(this.totalData / this.itemsPerPage)) {
        return this.itemsPerPage * this.page
      } else {
        return this.totalData
      }
    },
    pageStart () {
      return (this.page - 1) * this.itemsPerPage + 1
    },
    totalItem () {
      return this.totalData
    },
    pageStopOrdDetail() {
      if (this.pageOrdDetail < Math.ceil(this.lstOrderDetail.length / this.itemsPerPageDetail)) {
        return this.pageOrdDetail * this.itemsPerPageDetail;
      } else {
        return this.lstOrderDetail.length;
      }
    },
    pageStartOrdDetail() {
      if (this.lstOrderDetail.length == 0) {
        return 0;
      } else {
        return (this.pageOrdDetail - 1) * this.itemsPerPageDetail + 1;
      }
    },
    itemsLengthOrdDetail() {
      return this.lstOrderDetail.length;
    },
  },
  async created() {
    this.loading = true;
    this.bussinessCd = this.getUserData().bussinessCd;
    this.updateUserCd = this.getUserData().userCd;
    this.itemsPerPage = this.PAGE_SIZE_DEFAULT;
    this.itemsPerPageDetail = this.PAGE_SIZE_DETAIL_DEFAULT;

    await this.getListBranch();

    await this.callGetKbnValue();

    await this.listArea();

    await this.listProvince();

    await this.getListOrder(this.PAGE_START_DEFAULT);

    this.checkDefaultPassword();
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
          this.loadingPopup = true;
          zipCodeInfo = await this.getListMPostCodeByZipcode(this.custInfo.zipCd);
          if (zipCodeInfo != null && zipCodeInfo.length > 0) {
            this.isZipCodeNotExist = false;
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
            this.isZipCodeNotExist = true;
            this.custInfo.province = "";
            this.custInfo.adrCd1 = "";
            this.custInfo.adrCd2 = "";
          }

          this.loadingPopup = false;
        }

        if (this.isZipCodeNotExist) {
          this.zipCodeError = this.msgZipCodeExist;
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
    },
    // 姓（漢字）バリデーション
    checkValidKanjiFamilyCustRecieve() {
      this.kanjiFamilyCustRecieveError = this.validateKanjiName(
        this.orderDetail.kanjiFamilyNameMCustReceive,
        20,
        this.KANJI_FAMILY
      );
    },
    // 名（漢字）バリデーション
    checkValidKanjiFirstCustRecieve() {
      this.kanjiFirstCustRecieveError = this.validateKanjiName(
        this.orderDetail.kanjiFirstNameMCustReceive,
        20,
        this.KANJI_FIRST
      );
    },
    // 姓（ひらがな）バリデーション
    checkValidKanaFamilyCustRecieve() {
      this.kanaFamilyCustRecieveError = this.validateKanaName(
        this.orderDetail.kanaFamilyNameMCustReceive,
        20,
        this.KANA_FAMILY
      );
    },
    // 名（ひらがな）バリデーション
    checkValidKanaFirstCustRecieve() {
      this.kanaFirstCustRecieveError = this.validateKanaName(
        this.orderDetail.kanaFirstNameMCustReceive,
        20,
        this.KANA_FIRST
      );
    },
    // 郵便番号バリデーション
    async checkValidZipCodeCustRecieve(isGetZipCode) {
      this.zipCodeCustRecieveError = this.validateZipCode(
        this.orderDetail.zipCdMCustReceive,
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
          this.loadingPopup = true;
          zipCodeInfo = await this.getListMPostCodeByZipcode(
            this.orderDetail.zipCdMCustReceive
          );
          if (zipCodeInfo != null && zipCodeInfo.length > 0) {
            this.isZipCodeNotExist = false;
            var zipCodeIndex = 0;
            
            if (zipCodeInfo.length > 1) {
              for (let i = 0; i < zipCodeInfo.length; i++) {
                if(zipCodeInfo[i].isPriority === true){
                  zipCodeIndex = i;
                }
              }
            }
            this.orderDetail.zipCodeDspMCustReceive =
              zipCodeInfo[zipCodeIndex].zipCodeDsp;
            this.orderDetail.provinceMCustReceive =
              zipCodeInfo[zipCodeIndex].regionCd < 9
                ? "0" + zipCodeInfo[zipCodeIndex].regionCd
                : zipCodeInfo[zipCodeIndex].regionCd.toString();

            this.orderDetail.adrCd1MCustReceive =
              zipCodeInfo[zipCodeIndex].city +
              (zipCodeInfo[zipCodeIndex].town != null
                ? zipCodeInfo[zipCodeIndex].town
                : "");
            this.orderDetail.adrCd2MCustReceive =
              zipCodeInfo[zipCodeIndex].nom != null
                ? zipCodeInfo[zipCodeIndex].nom
                : "";
          } else {
            this.isZipCodeNotExist = true;
            this.orderDetail.provinceMCustReceive = "";
            this.orderDetail.adrCd1MCustReceive = "";
            this.orderDetail.adrCd2MCustReceive = "";
          }
          this.loadingPopup = false;
        }

        if (this.isZipCodeNotExist) {
          this.zipCodeCustRecieveError = this.msgZipCodeExist;
        }
      }
    },
    // 都道府県選択バリデーション
    checkValidProvinceCustRecieve() {
      this.provinceCustRecieveError = this.validateProvince(
        this.orderDetail.provinceMCustReceive,
        this.PROVINCE
      );
    },
    // 住所1入力バリデーション
    checkValidAdrCd1CustRecieve() {
      this.adrCd1CustRecieveError = this.validateAdrCdRequired(
        this.orderDetail.adrCd1MCustReceive,
        50,
        this.ADR_CD1
      );
    },
    // 住所2入力バリデーション
    checkValidAdrCd2CustRecieve() {
      this.adrCd2CustRecieveError = this.validateAdrCdRequired(
        this.orderDetail.adrCd2MCustReceive,
        50,
        this.ADR_CD2
      );
    },
    // 住所3入力バリデーション
    checkValidAdrCd3CustRecieve() {
      this.adrCd3CustRecieveError = this.validateAdrCdUnRequired(
        this.orderDetail.adrCd3MCustReceive,
        100,
        this.ADR_CD3
      );
    },
    // 電話番号バリデーション
    checkValidPhoneNumberCustRecieve() {
      this.phoneNumberCustRecieveError = this.validatePhoneNumber(
        this.orderDetail.phoneNumberMCustReceive,
        11,
        this.PHONE_NUMBER
      );
    },
    // メモバリデーション
    checkValidMemo() {
      if (
        this.memoDetail === "" ||
        this.memoDetail === undefined ||
        (this.memoDetail.length > 0 && this.memoDetail.trim() === "")
      ) {
        this.memoError = this.msgPleaseInput.replace("*", this.MEMO);
      } else {
        this.memoError = "";
      }
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
        this.mailAddressError === ""
      ) {
        return true;
      } else {
        return false;
      }
    },
    // 注文更新バリデーション
    isValidateOrderUpdate() {
      if (this.orderDetail.receiveWay === null || this.orderDetail.receiveWay === this.IN_SHOP) {
        this.checkValidShopSelect();

      } else if (this.orderDetail.receiveWay === this.SHIPPING) {
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

        if (this.orderDetail.paymentWay === this.PAY_IN_SHOP) {
          this.checkValidShopSelect();
        }
      }

      if (this.orderDetail.receiveWay === null || this.orderDetail.receiveWay === this.IN_SHOP) {
        if (this.shopSelectError === "") {
          return true;
        }
      } else if (this.orderDetail.receiveWay === this.SHIPPING &&
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
        if (this.orderDetail.paymentWay === this.PAY_IN_SHOP && this.shopSelectError !== "") {
          return false;
        } else {
          return true;
        }
      } else {
        return false;
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
        this.orderDetail.shopCd = "";
        this.shopSelectError = "";
        this.isShowShop = false;
      } else if (this.shopSelect !== undefined) {
        this.checkChange = !this.checkChange;
        this.orderDetail.shopCd = this.shopSelect;
        this.orderDetail.shopName = this.lstShop.filter(
          (x) => x.value === this.shopSelect
        )[0].text;
        this.shopSelectError = "";
      }
    },
    // エリア選択
    async changeAreaSelect() {
      this.loadingPopup = true;
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

          if (lstShopByArea.data !== null || lstShopByArea.data.length > 0) {
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
          this.loadingPopup = false;
        }
      } else {
        this.shopSelect = "";
      }
    },
    // 注文リストデータを取得する
    async getListOrder(page) {
      this.loading = true;
      this.searchOrder.bussinessCd = this.bussinessCd;
      const config = {
        method: "post",
        url: "api/Order/GetDataOrdersPagination",
        data: this.searchOrder,
        params: {
          currentpage: page,
          pageSize: this.PAGE_SIZE_DEFAULT,
          sortColumnName: this.sortColumnName,
          isDesc: this.isDesc
        },
      };
      try {
        const resultGetLstOrder = await require("axios")(config);
        if (resultGetLstOrder.data !== null) {
          if (resultGetLstOrder.data.error === undefined) {
            this.lstOrder = resultGetLstOrder.data.data;
            this.totalData = resultGetLstOrder.data.totalData;
            this.orderSelected = [];
          }
        } else {
          this.$toastr.e(this.msgNoData);
        }
        this.page = page;
      } catch (error) {
        console.log(error);
      } finally {
        this.loading = false;
      }
    },
    // 注文情報を表示する
    async openOrderDialog(orderItem) {
      this.loading = true;
      this.isCancel = false;
      this.shopSelectError = "";
      this.sizeOrColorError = "";
      this.maxAmountCanOrderError = "";

      this.clearCustReceiveInfoErr();

      if (orderItem.status !== this.RSV_STATUS_ORDER) {
        this.isCancel = true;
      }

      this.orderDetail = { ...orderItem };
      this.oldOrderDetail = { ...orderItem };

      await this.getOrderDetail(orderItem.orderId);

      if (!this.isUserReadOnly) {
        if (orderItem.receiveWay === null ||
            orderItem.receiveWay === this.IN_SHOP ||
            (orderItem.receiveWay === this.SHIPPING && orderItem.paymentWay === this.PAY_IN_SHOP)
          ) {
          this.areaSelect =
            orderItem.area2Cd < 10 ? "0" + orderItem.area2Cd : orderItem.area2Cd;
          this.shopSelect = orderItem.shopCd;
          this.shopCdBefore = orderItem.shopCd;

          await this.changeAreaSelect();
        }
      }

      await this.getOrderHistory(orderItem.orderId);

      this.dialogOrderUpdate = true;
      this.loading = false;
      this.loadingBtn = false;
      this.lstOrderDetailRemove = [];
    },
    // 注文更新確認ダイアログを表示する
    showDialogOrderUpdateConfirm() {
      this.checkChange = !this.checkChange;
      this.loadingBtn = false;
      let chkOrdDtlSame = 0;
      let detailByproductCd = null;
      let totalSub = 0;

      // サイズ又はカラーは重複しているか確認する
      if (this.lstOrderDetail.length > 1) {
        for (var i = 0; i < this.lstOrderDetail.length; i++) {
          chkOrdDtlSame = this.lstOrderDetail.filter(
            (x) =>
              x.productCd == this.lstOrderDetail[i].productCd &&
              x.colorCd == this.lstOrderDetail[i].colorCd &&
              x.sizeCd == this.lstOrderDetail[i].sizeCd
          ).length;
          if (chkOrdDtlSame > 1) {
            this.sizeOrColorError = this.msgNotChooseSame;
            return;
          }
          detailByproductCd = this.lstOrderDetail.filter(
            (x) =>
              x.productCd == this.lstOrderDetail[i].productCd
          );
          totalSub = 0;
          detailByproductCd.map((order) => {
            totalSub += order.subTotal;
          })
          if (totalSub > this.lstOrderDetail[i].maxAmountCanOrder) {
            this.maxAmountCanOrderError = this.msgOutOfPrice;
            return;
          }
        }
      }

      if (this.sizeOrColorError === "" && this.maxAmountCanOrderError === "") {
        if (
          JSON.stringify(this.orderDetail) ===
            JSON.stringify(this.oldOrderDetail) &&
          JSON.stringify(this.lstOrderDetail) ===
            JSON.stringify(this.oldLstOrderDetail)
        ) {
          this.$toastr.w(this.msgNotthingChanged);
        } else if (this.isValidateOrderUpdate()) {
          this.isChangeCustReceive = this.isChangeCustReceiveInfo();
          this.dialogOrderUpdateConfirm = true;
        }
      }
    },
    // 注文更新
    async updateReservation(isSendMail) {
      this.loading = true;
      this.dialogOrderUpdate = false;
      this.dialogOrderUpdateConfirm = false;
      this.loadingBtn = true;

      let lstProductCd = "";
      this.lstOrderDetail.map((orderDetail) => {
        if (lstProductCd.indexOf(orderDetail.productCd) == -1) {
          lstProductCd += "'" + orderDetail.productCd + "',";
        }
      })

      if (lstProductCd !== "") {
        lstProductCd = lstProductCd.substring(0 , lstProductCd.length -1);
      }

      let orderUdpate = {
        order: this.orderDetail,
        orderDetails: this.lstOrderDetail,
        orderDetailRemoves: this.lstOrderDetailRemove,
      };

      const config = {
        method: "post",
        url: "api/Order/OrderUdpate",
        data: orderUdpate,
        params: {
          isSendMail: isSendMail,
          shopCdBefore: this.shopCdBefore,
          isChangeCustReceive: this.isChangeCustReceive,
          lstProductCd: lstProductCd,
          updateUserCd: this.updateUserCd,
        },
      };
      try {
        const responOrderUpd = await require("axios")(config);
        if (responOrderUpd.data.error === undefined) {
          if (responOrderUpd.data.isOrderUpdate) {
            this.$toastr.s(this.msgUpdateSucces);
          } else {
            this.$toastr.e(this.msgUpdateFailed);
          }
          if (isSendMail && !responOrderUpd.data.isSendMailUpdSucces) {
            this.$toastr.e(this.msgSendMailFailed);
          }
          await this.getListOrder(this.PAGE_START_DEFAULT);
        } else {
          this.dialogOrderUpdate = true;
          if(responOrderUpd.data.lstSkuErr !== undefined ){
              this.setOrderUpdateErr(responOrderUpd.data.lstSkuErr)
          }
          this.$toastr.e(responOrderUpd.data.error);
        }
      } catch {
        this.$toastr.e(this.msgUpdateFailed);
      } finally {
        this.shopCdBefore = "";
        this.loading = false;
        this.loadingBtn = false;
      }
    },
    setOrderUpdateErr(lstSkuName) {
      let index = -1;

      for (var i = 0; i < lstSkuName.length; i++) {
        index = this.lstOrderDetail.findIndex((x) => x.sku === lstSkuName[i]);
        if (index !== -1) {
          this.lstOrderDetail[index].backgroundErr = true;
        }
      }
    },
    // お客様詳細情報
    async openCustDialog(orderItem) {
      this.loading = true;

      this.clearCustInfoErr();

      this.isCancel = false;
      if (orderItem.status !== this.RSV_STATUS_ORDER) {
        this.isCancel = true;
      }

      const config = {
        method: "get",
        url: "api/MCust/GetMCustInfo",
        params: {
          custId: orderItem.custId,
        },
      };
      try {
        const custInfoDetail = await require("axios")(config);
        if (custInfoDetail.data !== null) {
          this.custInfo = custInfoDetail.data;
          this.custInfo.provinceName = this.lstProvince.filter((x) => x.value == this.custInfo.province)[0].text;
          this.oldCustInfo = { ...this.custInfo };
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loadingBtn = false;
        this.dialogMcust = true;
        this.loading = false;
      }
    },
    // mcust更新確認ダイアログを表示する
    showDialogMcustConfirm() {
      if (JSON.stringify(this.custInfo) === JSON.stringify(this.oldCustInfo)) {
        this.$toastr.w(this.msgNotthingChanged);
      } else if (this.isValidateAll()) {
        this.dialogMcustConfirm = true;
        this.loadingBtn = false;
      }
    },
    // お客様情報を更新する
    async updateCustInfo() {
      this.loading = true;
      this.dialogMcust = false;
      this.dialogMcustConfirm = false;
      this.loadingBtn = true;
      this.custInfo.updateUserCd = this.updateUserCd;
      const config = {
        method: "post",
        url: "api/MCust/UpdateCustInfo",
        params: {},
        data: this.custInfo,
      };
      try {
        const resultUpdCust = await require("axios")(config);
        if (resultUpdCust.data !== this.RESULT_ERROR) {
          if (resultUpdCust.data === this.RESULT_SUCCESS) {
            this.$toastr.s(this.msgUpdateSucces);
          } else {
            this.$toastr.w(this.msgUpdtedBefore);
          }
          await this.getListOrder(this.PAGE_START_DEFAULT);
        } else {
          this.$toastr.e(this.msgUpdateFailed);
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loading = false;
      }
    },
    // お客様が振り込んだことを確認するダイアログを表示する
    openConfirmUpdateStatusOrderDialog(orderItem, isStatusDelivery) {
      this.isStatusDelivery = isStatusDelivery
      this.txtMesConfirm = isStatusDelivery ? this.msgStatusDelivery : this.msgStatusPaid;
      this.transferOrder = {...orderItem};
      this.loadingBtn = false;
      this.dialogConfirmUpdateStatusOrder = true;
    },
    // お客様が振り込んだ時の予約状態を更新する
    async updateStatusOrder() {
      this.loading = true;
      this.loadingBtn = true;
      this.dialogConfirmUpdateStatusOrder = false;
      let lastStatus = this.transferOrder.status;
      if (this.isStatusDelivery) {
        this.transferOrder.status = this.RSV_STATUS_COMPLETED_DELIVERY;
      } else {
        this.transferOrder.status = this.RSV_STATUS_PAID;
      }
      this.transferOrder.updateUserCd = this.updateUserCd;

      const config = {
        method: "post",
        url: "api/Order/UpdateStatusTransferOrder",
        data: this.transferOrder,
        params: {
          lastStatus: lastStatus
        },
      };
      try {
        const responUpdStatusOrder = await require("axios")(config);
        if (responUpdStatusOrder.data.error === undefined) {
          if (responUpdStatusOrder.data.isStatusUpdate) {
            this.$toastr.s(this.msgUpdateSucces);
          } else {
            this.$toastr.e(this.msgUpdateFailed);
          }

          if (!this.isStatusDelivery && !responUpdStatusOrder.data.isSendMailSuccess) {
            this.$toastr.e(this.msgSendMailFailed);
          }
          await this.getListOrder(this.PAGE_START_DEFAULT);
        } else {
          this.$toastr.e(responUpdStatusOrder.data.error);
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loadingBtn = false;
        this.loading = false;
      }
    },
    // 注文メール再送信の確認ダイアログ
    async resendReserveMailDialog() {
      if (this.orderSelectedAll.length === 0) {
        this.resendDisabled = true;
        this.$toastr.w(this.msgNotChooseOrderId);
        await this.sleep(2000);
        this.resendDisabled = false;
        return;
      }

      if (this.checkSelectedAll) {
        let allOrder = await this.getAllOrder()
        if (allOrder.length > 0) {
          this.orderSelectedAll = allOrder
        }
        this.checkSelectedAll = false
      }

      for (var i = 0; i < this.orderSelectedAll.length; i++) {
        if (this.orderSelectedAll[i].status !== this.RSV_STATUS_ORDER) {
          this.resendDisabled = true;
          this.$toastr.w(this.msgResendOrderMailError);
          await this.sleep(2000);
          this.resendDisabled = false;
          return;
        }
      }
      this.loadingBtn = false;
      this.dialogResendReserveMail = !this.dialogResendReserveMail;
    },
    // 注文メール再送
    async resendReserveMail() {
      this.loading = true;
      this.loadingBtn = true;
      this.dialogResendReserveMail = !this.dialogResendReserveMail;

      const config = {
        method: "post",
        url: "api/Order/ResendOrderMail",
        data: this.orderSelectedAll,
        params: { updateUserCd: this.updateUserCd },
      };
      try {
        const responResendMailOrder = await require("axios")(config);
        await this.getListOrder(this.PAGE_START_DEFAULT);
        if (responResendMailOrder.data.length === 0) {
          this.$toastr.s(this.msgSendMailSucces);
        } else {
          this.lstInforSendMailErr = [];
          this.lstInforSendMailErr = responResendMailOrder.data;
          this.$toastr.e(this.msgSendMailFailed);
          this.dialogInforSendMailErr = !this.dialogInforSendMailErr;
        }
      } catch {
        this.$toastr.e(this.msgSendMailFailed);
      } finally {
        this.clearSelected()
        this.checkSelectedAll = false;
        this.resendDisabled = false;
        this.loadingBtn = false;
        this.loading = false;
      }
    },
    // 入金リマインドメール送信の確認ダイアログ
    async sendRemindMailDialog() {
      if (this.orderSelectedAll.length === 0) {
        this.remindDisabled = true;
        this.$toastr.w(this.msgNotChooseOrderId);
        await this.sleep(2000);
        this.remindDisabled = false;
        return;
      }

      if (this.checkSelectedAll) {
        let allOrder = await this.getAllOrder()
        if (allOrder.length > 0) {
          this.orderSelectedAll = allOrder
        }
        this.checkSelectedAll = false
      }

      for (var i = 0; i < this.orderSelectedAll.length; i++) {
        if (
          this.orderSelectedAll[i].status !== this.RSV_STATUS_ORDER ||
          (this.orderSelectedAll[i].status === this.RSV_STATUS_ORDER &&
            (this.orderSelectedAll[i].orderMailStatus === this.MAIL_STATUS_ERROR ||
              this.orderSelectedAll[i].orderMailStatus === this.MAIL_STATUS_NOTSEND))
        ) {
          this.remindDisabled = true;
          this.$toastr.w(this.msgSendRemindMailError);
          await this.sleep(2000);
          this.remindDisabled = false;
          return;
        }
      }
      this.loadingBtn = false;
      this.dialogSendRemindMail = !this.dialogSendRemindMail;
    },
    // リマインドメール送信
    async sendRemindMail() {
      this.loading = true;
      this.loadingBtn = true;
      this.dialogSendRemindMail = !this.dialogSendRemindMail;

      const config = {
        method: "post",
        url: "api/Order/SendRemindMail",
        data: this.orderSelectedAll,
        params: { updateUserCd: this.updateUserCd },
      };
      try {
        const responSendRemindMail = await require("axios")(config);
        await this.getListOrder(this.PAGE_START_DEFAULT);
        if (responSendRemindMail.data.length === 0) {
          this.$toastr.s(this.msgSendMailSucces);
        } else {
          this.lstInforSendMailErr = [];
          this.lstInforSendMailErr = responSendRemindMail.data;
          this.$toastr.e(this.msgSendMailFailed);
          this.dialogInforSendMailErr = !this.dialogInforSendMailErr;
        }
      } catch {
        this.$toastr.e(this.msgSendMailFailed);
      } finally {
        this.clearSelected()
        this.checkSelectedAll = false;
        this.remindDisabled = false;
        this.loading = false;
        this.loadingBtn = false;
      }
    },
    // 注文キャンセルの確認ダイアログ
    async cancelOrderDialog() {
      if (this.orderSelectedAll.length === 0) {
        this.cancelDisabled = true;
        this.$toastr.w(this.msgNotChooseOrderId);
        await this.sleep(2000);
        this.cancelDisabled = false;
        return;
      }

      if (this.checkSelectedAll) {
        let allOrder = await this.getAllOrder()
        if (allOrder.length > 0) {
          this.orderSelectedAll = allOrder
        }
        this.checkSelectedAll = false
      }

      var checkStatusCancel = 0;
      var checkStatusCompleteDelivery = 0;
      for (var i = 0; i < this.orderSelectedAll.length; i++) {
        if (this.orderSelectedAll[i].status === this.RSV_STATUS_CANCEL) {
          checkStatusCancel++;
        }
        if (
          this.orderSelectedAll[i].status === this.RSV_STATUS_COMPLETED_DELIVERY
        ) {
          checkStatusCompleteDelivery++;
        }
      }
      // 予約ステータスが「キャンセル」の予約はキャンセルできません。
      if (checkStatusCancel !== 0 && checkStatusCompleteDelivery === 0) {
        this.cancelDisabled = true;
        this.$toastr.w(this.msgCheckStatusCancel);
        await this.sleep(2000);
        this.cancelDisabled = false;
        return;
      }
      // 予約ステータスが「引渡完了」の予約はキャンセルできません。
      if (checkStatusCancel === 0 && checkStatusCompleteDelivery !== 0) {
        this.cancelDisabled = true;
        this.$toastr.w(this.msgCheckStatusCompleteDelivery);
        await this.sleep(2000);
        this.cancelDisabled = false;
        return;
      }
      // 予約ステータスが「引渡完了」又は「キャンセル」の予約はキャンセルできません。
      if (checkStatusCancel !== 0 && checkStatusCompleteDelivery !== 0) {
        this.cancelDisabled = true;
        this.$toastr.w(this.msgCheckStatusCompleteDeliveryAndCancel);
        await this.sleep(2000);
        this.cancelDisabled = false;
        return;
      }
      this.loadingBtn = false;
      this.dialogCancelOrder = !this.dialogCancelOrder;
    },
    // 注文キャンセル
    async cancelOrder(isSendMail) {
      this.loading = true;
      this.loadingBtn = true;
      this.dialogCancelOrder = !this.dialogCancelOrder;

      for (var i = 0; i < this.orderSelectedAll.length; i++) {
        this.orderSelectedAll[i].status = this.RSV_STATUS_CANCEL;
      }

      const config = {
        method: "post",
        url: "api/Order/CancelOrder",
        data: this.orderSelectedAll,
        params: {
          isSendMail: isSendMail,
          updateUserCd: this.updateUserCd,
        },
      };
      try {
        const responCancelOrder = await require("axios")(config);
        if (responCancelOrder.data !== null) {
          await this.getListOrder(this.PAGE_START_DEFAULT);
          if (responCancelOrder.data.isCancelSuccess) {
            this.$toastr.s(this.msgCancelSucces);
          } else {
            this.$toastr.e(this.msgCancelFailed);
          }

          if (isSendMail) {
            if (responCancelOrder.data.sendMailErrors !== null) {
              if (responCancelOrder.data.sendMailErrors.length > 0) {
                this.lstInforSendMailErr = [];
                this.lstInforSendMailErr =
                  responCancelOrder.data.sendMailErrors;
                this.$toastr.e(this.msgSendMailFailed);
                this.dialogInforSendMailErr = !this.dialogInforSendMailErr;
              }
            }
          }
        } else {
          this.$toastr.e(this.msgCancelFailed);
        }
      } catch {
        this.$toastr.e(this.msgCancelFailed);
      } finally {
        this.clearSelected()
        this.checkSelectedAll = false;
        this.cancelDisabled = false;
        this.loading = false;
        this.loadingBtn = false;
      }
    },
    // CSVダウンロード
    async downloadCSV() {
      if (this.orderSelectedAll.length === 0) {
        this.downloadCSVDisabled = true;
        this.$toastr.w(this.msgNotChooseOrderId);
        await this.sleep(2000);
        this.downloadCSVDisabled = false;
        return;
      }

      this.loading = true;
      let orders = {
        order: this.searchOrder,
        ordersSubs: this.orderSelectedAll,
        orderUnSelected: this.orderUnSelected
      }
      const config = {
        method: "post",
        url: "api/Order/DownloadCSV",
        data: orders,
        params: {
          isDownloadAll: this.checkSelectedAll,
        },
      };
      try {
        const dtNow = moment().format("YYYYMMDD");
        const csvExport = await require("axios")(config);
        if (csvExport.data != null) {
          const csvContent = csvExport.data;
          var universalBOM = "\uFEFF";
          var a = window.document.createElement("a");
          a.setAttribute(
            "href",
            "data:text/csv; charset=utf-8," +
              encodeURIComponent(universalBOM + csvContent)
          );
          a.setAttribute("download", "予約情報一覧_" + dtNow + ".csv");
          window.document.body.appendChild(a);
          a.click();
        }
      } catch {
        this.$toastr.e(this.msgDownloadFailed);
      } finally {
        this.clearSelected()
        this.checkSelectedAll = false;
        this.downloadCSVDisabled = false;
        this.loading = false;
      }
    },
    // バウンス情報
    mailBounceDialog (orderItem) {
      this.mailBounceInfo.mailBounce = orderItem.mailBounce
      this.mailBounceInfo.mailBounceName = orderItem.mailBounceName
      this.mailBounceInfo.mailBounceDescription = orderItem.mailBounceDescription
      this.dialogMailBounce = !this.dialogMailBounce
    },
    // メモダイアログを表示する
    async memoDialog(isAddMemo, orderItem) {
      if (isAddMemo) {
        if (this.orderSelectedAll.length === 0) {
          this.memoDisabled = true;
          this.$toastr.w(this.msgNotChooseOrderId);
          await this.sleep(2000);
          this.memoDisabled = false;
          return;
        }

        if (this.checkSelectedAll) {
          let allOrder = await this.getAllOrder()
          if (allOrder.length > 0) {
            this.orderSelectedAll = allOrder
          }
          this.checkSelectedAll = false
        }

        for (var i = 0; i < this.orderSelectedAll.length; i++) {
          if (this.orderSelectedAll[i].status === this.RSV_STATUS_COMPLETED_DELIVERY ||
            this.orderSelectedAll[i].status === this.RSV_STATUS_CANCEL)
          {
            this.memoDisabled = true;
            this.$toastr.w(this.msgMemoAddError);
            await this.sleep(2000);
            this.memoDisabled = false;
            return;
          }

          if (
            this.orderSelectedAll[i].memo !== null &&
            this.orderSelectedAll[i].memo !== ""
          ) {
            this.memoDisabled = true;
            this.$toastr.w(this.msgMemoExist);
            await this.sleep(2000);
            this.memoDisabled = false;
            return;
          }
        }

        this.memoDetail = "";
      } else {
        this.isCancel = false;
        if (orderItem.status === this.RSV_STATUS_COMPLETED_DELIVERY ||
            orderItem.status === this.RSV_STATUS_CANCEL)
        {
          this.isCancel = true;
        }

        this.memoDetail = orderItem.memo;
        this.orderDetail = { ...orderItem };
      }

      this.isAddMemo = isAddMemo;
      this.memoError = "";

      this.dialogMemo = !this.dialogMemo;
      this.loadingBtn = false;
    },
    // メモ確認ダイアログを表示する
    showDialogMemoConfirm() {
      if (!this.isAddMemo) {
        if (this.orderDetail.memo === this.memoDetail) {
          this.$toastr.w(this.msgNotthingChanged);
          return;
        }
      }
      this.checkValidMemo();

      if (this.memoError === "") {
        this.dialogMemoConfirm = true;
      } else if (this.memoError === "") {
        this.orderDetail.memo = this.memoDetail;
        this.loadingBtn = false;
        this.dialogMemo = !this.dialogMemo;
      }
    },
    // メモ登録・更新
    async submitMemo() {
      this.loading = true;
      this.dialogMemo = false;
      this.dialogMemoConfirm = false;
      this.loadingBtn = true;

      let memoSubmit = [];

      if (this.isAddMemo) {
        let lstOrderSelect = [...this.orderSelectedAll];
        for (var i = 0; i < lstOrderSelect.length; i++) {
          lstOrderSelect[i].memo = this.memoDetail;
          lstOrderSelect[i].updateUserCd = this.updateUserCd;
        }
        memoSubmit = lstOrderSelect;
      } else {
        this.orderDetail.memo = this.memoDetail;
        this.orderDetail.updateUserCd = this.updateUserCd;
        memoSubmit.push(this.orderDetail);
      }

      const config = {
          method: "post",
          url: "api/Order/SubmitMemo",
          data: memoSubmit,
        };
      try {
        const responSubmitMemo = await require("axios")(config);
        if (responSubmitMemo.data) {
          if (this.isAddMemo) {
            this.$toastr.s(this.msgAddMemoSucces);
          } else {
            this.$toastr.s(this.msgUpdMemoSucces);
          }

          await this.getListOrder(this.PAGE_START_DEFAULT);
        } else {
          if (this.isAddMemo) {
            this.$toastr.e(this.msgAddMemoFailed);
          } else {
            this.$toastr.e(this.msgUpdMemoFailed);
          }
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.clearSelected()
        this.checkSelectedAll = false;
        this.memoDisabled = false;
        this.loadingBtn = false;
        this.loading = false;
      }
    },
    // 並び替える
    changeSort({ sortBy, sortDesc }) {
      if (sortBy[0] != undefined && sortDesc[0] != undefined) {
        if (sortBy[0] == "orderId") {
          this.sortColumnName = "ord.OrderId";
        } else if (sortBy[0] === "shopName") {
          this.sortColumnName = "ms.ShopName";
        } else if (sortBy[0] === "status") {
          this.sortColumnName = "ord.Status";
        } else if (sortBy[0] === "orderMailStatus") {
          this.sortColumnName = "sm_order.MailStatus";
        } else if (sortBy[0] === "remindMailStatus") {
          this.sortColumnName = "sm_remind.MailStatus";
        } else if (sortBy[0] === "finishedMailStatus") {
          this.sortColumnName = "sm_finished.MailStatus";
        } else {
          this.sortColumnName = "ord.UpdateDtTm";
        }

        this.isDesc = sortDesc[0];
        this.clearSelected()

        this.getListOrder(this.PAGE_START_DEFAULT);
      } else {
        this.sortColumnName = "ord.UpdateDtTm";
      }
    },
    // 検索
    search() {
      this.clearSelected()
      this.checkSelectedAll = false;

      this.getListOrder(this.PAGE_START_DEFAULT);
    },
    // 検索クリア
    clearSearch() {
      this.searchOrder = {}
      this.clearSelected()
      this.checkSelectedAll = false;

      this.getListOrder(this.PAGE_START_DEFAULT);
    },
    // ページ変更
    async changePage(page) {
      await this.getListOrder(page);

      if (this.checkSelectedAll) {
        if (this.orderUnSelected.length > 0) {
          let index = -1;
          this.lstOrder.map((order) => {
            index = this.orderUnSelected.findIndex((x) => x.orderId === order.orderId);

            if (index === -1) {
              this.orderSelected.push(order)
              this.orderSelectedAll.push(order)
            }
          })
        } else {
          this.lstOrder.map((order) => {
             this.orderSelected.push(order)
            this.orderSelectedAll.push(order)
          })
        }
      } else {
        // その前の選択された注文をチェックする
        let index = -1;
        if (this.lstOrder.length > 0) {
          this.lstOrder.map((order) => {
            index = this.orderSelectedAll.findIndex((x) => x.orderId == order.orderId);
            if (index !== -1) {
              this.orderSelected.push(order)
            }
          })
        }
      }
    },
    // ブランド一覧
    async getListBranch() {
      const config = {
        method: "get",
        url: "api/MBrand/GetListBrand",
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      try {
        const lstBrand = await this.axios(config);
        if (lstBrand.data != null && lstBrand.data.length > 0) {
          for (var i = 0; i < lstBrand.data.length; i++) {
            this.brandOptions.push({
              value: lstBrand.data[i].brandCd,
              text: lstBrand.data[i].brandName,
            });
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    // 注文番号による注文詳細
    async getOrderDetail(orderId) {
      const config = {
        method: "get",
        url: "api/Order/GetOrderDetail",
        params: {
          orderId: orderId,
        },
      };
      try {
        const lstOrderDetail = await require("axios")(config);
        if (lstOrderDetail.data != null && lstOrderDetail.data.length > 0) {
          for (var i = 0; i < lstOrderDetail.data.length; i++) {
            lstOrderDetail.data[i].lstSizeCd = lstOrderDetail.data[i].sizeCdArr !== null? lstOrderDetail.data[i].sizeCdArr.split(","):
             [lstOrderDetail.data[i].sizeCd];
            lstOrderDetail.data[i].lstSizeName =  lstOrderDetail.data[i].sizeNameArr !== null? lstOrderDetail.data[i].sizeNameArr.split(","):
              [lstOrderDetail.data[i].sizeName];
            lstOrderDetail.data[i].lstColorCd =  lstOrderDetail.data[i].colorCdArr !== null? lstOrderDetail.data[i].colorCdArr.split(","): 
              [lstOrderDetail.data[i].colorCd];
            lstOrderDetail.data[i].lstColorName =  lstOrderDetail.data[i].colorNameArr !== null? lstOrderDetail.data[i].colorNameArr.split(","): 
              [lstOrderDetail.data[i].colorName];

            lstOrderDetail.data[i].lstProductInventory = [...Array(lstOrderDetail.data[i].maxSizeCanOrder + 1).keys()]
            lstOrderDetail.data[i].lstProductInventory.shift()
          }

          this.lstOrderDetail = lstOrderDetail.data;
          this.oldLstOrderDetail = JSON.parse(
            JSON.stringify(this.lstOrderDetail)
          );
        }
      } catch (error) {
        console.log(error);
      }
    },
    // 注文履歴
    async getOrderHistory(orderId) {
      const config = {
        method: "get",
        url: "api/OrderHistory/GetOrderHistory",
        params: {
          orderId: orderId,
        },
      };
      try {
         const lstOrderHistory = await require("axios")(config);
        if (lstOrderHistory.data != null && lstOrderHistory.data.length > 0) {
          this.lstOrderHistory = lstOrderHistory.data;
        }
      } catch (error) {
        console.log(error);
      }
    },
    // カラー変更
    changeColor(ordItem) {
      let index = ordItem.lstColorName.indexOf(ordItem.colorName);
      ordItem.colorCd = ordItem.lstColorCd[index]
      ordItem.sku = ordItem.productCd + ordItem.colorCd + ordItem.sizeCd;
      ordItem.updateUserCd = this.updateUserCd;
      ordItem.backgroundErr = undefined;
      this.sizeOrColorError = "";
    },
    // サイズ変更
    changeSize(ordItem) {
      let index = ordItem.lstSizeName.indexOf(ordItem.sizeName);
      ordItem.sizeCd = ordItem.lstSizeCd[index]
      ordItem.sku = ordItem.productCd + ordItem.colorCd + ordItem.sizeCd;
      this.changeListColor(ordItem)
      ordItem.updateUserCd = this.updateUserCd;
      ordItem.backgroundErr = undefined;
      this.sizeOrColorError = "";
    },
      // カラー一覧変更
    async changeListColor(ordItem) {
      const config = {
        method: "get",
        url: "api/Order/GetLstColorByPrdSize",
        params: {
          productCd: ordItem.productCd,
          sizeCd: ordItem.sizeCd
        }
      };
      try {
        const colorLstRespone = await require("axios")(config);
        if (colorLstRespone.data !== null && colorLstRespone.data[0].colorCdArr != null) {
          ordItem.lstColorCd = colorLstRespone.data[0].colorCdArr.split(",");
          ordItem.lstColorName =  colorLstRespone.data[0].colorNameArr.split(",");
          ordItem.colorCdArr = colorLstRespone.data[0].colorCdArr
          ordItem.colorNameArr =  colorLstRespone.data[0].colorNameArr
          let index = colorLstRespone.data[0].colorCdArr.indexOf(ordItem.colorCd)
          if (index < 0) {
            ordItem.colorCd = ordItem.lstColorCd[0]
            ordItem.colorName = ordItem.lstColorName[0]
            ordItem.sku = ordItem.productCd + ordItem.colorCd + ordItem.sizeCd;
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    // 商品価格が数量に該当する
    changePrice(ordItem) {
      ordItem.subTotal = ordItem.price * ordItem.quantity;
      ordItem.updateUserCd = this.updateUserCd;
      ordItem.backgroundErr = undefined;
      this.listDataOrder(this.lstOrderDetail, ordItem);
    },
    // 商品受取人の情報変更を確認する
    isChangeCustReceiveInfo() {
      if (
        this.orderDetail.kanaFamilyNameMCustReceive !==
          this.oldOrderDetail.kanaFamilyNameMCustReceive ||
        this.orderDetail.kanaFirstNameMCustReceive !==
          this.oldOrderDetail.kanaFirstNameMCustReceive ||
        this.orderDetail.kanjiFamilyNameMCustReceive !==
          this.oldOrderDetail.kanjiFamilyNameMCustReceive ||
        this.orderDetail.kanjiFirstNameMCustReceive !==
          this.oldOrderDetail.kanjiFirstNameMCustReceive ||
        this.orderDetail.zipCdMCustReceive !==
          this.oldOrderDetail.zipCdMCustReceive ||
        this.orderDetail.provinceMCustReceive !==
          this.oldOrderDetail.provinceMCustReceive ||
        this.orderDetail.adrCd1MCustReceive !==
          this.oldOrderDetail.adrCd1MCustReceive ||
        this.orderDetail.adrCd2MCustReceive !==
          this.oldOrderDetail.adrCd2MCustReceive ||
        this.orderDetail.adrCd3MCustReceive !==
          this.oldOrderDetail.adrCd3MCustReceive ||
        this.orderDetail.phoneNumberMCustReceive !==
          this.oldOrderDetail.phoneNumberMCustReceive
      ) {
        return true;
      } else {
        return false;
      }
    },
    // 商品項目を削除する
    async removeOrdDetailItem(ordItem) {
      this.loadingBtn = true;
      if (this.lstOrderDetail.length < 2) {
        this.$toastr.e(this.msgNotRemoveAll);
        await this.sleep(2000);
        this.loadingBtn = false;
        return;
      }
      this.loadingBtn = false;

      const index = this.lstOrderDetail.indexOf(ordItem);
      this.lstOrderDetail.splice(index, 1);
      this.lstOrderDetailRemove.push(ordItem);
      this.listDataOrder(this.lstOrderDetail, ordItem);
    },
    // 注文商品一覧
    listDataOrder(lstProductOrder, ordItem) {
      let totalQuantity = 0;
      let totalAmount = 0;
      let totalAmountByProductCd = 0;

      for (var i = 0; i < lstProductOrder.length; i++) {
        totalQuantity += lstProductOrder[i].quantity;
        totalAmount += lstProductOrder[i].quantity * lstProductOrder[i].price;

        if (lstProductOrder[i].productCd == ordItem.productCd) {
          totalAmountByProductCd += lstProductOrder[i].quantity * lstProductOrder[i].price;
        }
      }

      this.orderDetail.totalQuantity = totalQuantity;
      this.orderDetail.total = totalAmount;
      if (totalAmountByProductCd > ordItem.maxAmountCanOrder) {
        this.maxAmountCanOrderError = this.msgOutOfPrice;
      } else {
        this.maxAmountCanOrderError = "";
      }
    },
    // お客様情報バリデーションテキストを削除する
    clearCustInfoErr() {
      this.kanjiFamilyError = "";
      this.kanjiFirstError = "";
      this.kanaFamilyError = "";
      this.kanaFirstError = "";
      this.zipCodeError = "";
      this.provinceError = "";
      this.adrCd1Error = "";
      this.adrCd2Error = "";
      this.adrCd3Error = "";
      this.phoneNumberError = "";
      this.mailAddressError = "";
    },
    // 商品受取人情報バリデーションテキストを削除する
    clearCustReceiveInfoErr() {
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
    async callGetKbnValue() {
      this.histTypeValue = await this.getParameterByCd(this.KBN_HIST_TYPE);
      this.orderStatusKbn = await this.getParameterByCd(this.KBN_ORDER_STATUS);
      this.mailType = await this.getParameterByCd(this.KBN_MAIL_TYPE);
    },
    toggleOrder(ordItem) {
      const index = this.orderSelected.findIndex((x) => x.orderId === ordItem.orderId)

      if (index === -1) {
        this.orderSelected.push(ordItem)
        this.orderSelectedAll.push(ordItem)

        const indexUnCheck = this.orderUnSelected.findIndex((x) => x.orderId === ordItem.orderId);
        this.orderUnSelected.splice(indexUnCheck, 1)
      } else {
        this.orderSelected.splice(index, 1)
        this.orderSelectedAll.splice(this.orderSelectedAll.findIndex((x) => x.orderId === ordItem.orderId), 1)
        this.orderUnSelected.push(ordItem)
      }
    },
    selectAllOrder(orders) {
      // 全てチェック
      if (orders.value) {
        this.orderSelected = [...orders.items];
        this.orderSelectedAll = [...orders.items];
        this.checkSelectedAll = true;
      } else {
        // 全てチェックを解除する
        this.clearSelected()
        this.checkSelectedAll = false;
      }
    },
    async getAllOrder() {
      this.loading = true;
      let orders = {
        order: this.searchOrder,
        orderUnSelected: this.orderUnSelected
      }

      const config = {
        method: "post",
        url: "api/Order/GetAllSub",
        data: orders
      };
      try {
        const lstAllOrder = await require("axios")(config);

        if (lstAllOrder.data.length > 0) {
          return lstAllOrder.data;
        } else {
          this.$toastr.e(this.msgNoData);
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loading = false;
      }
    },
    clearSelected() {
      this.orderSelected = []
      this.orderSelectedAll = []
      this.orderUnSelected = []
    },
    openBrandNameDialog(item) {
      this.selectedBrandName = item.brandName
      this.dialogBrandName = true
    }
  },
};
</script>

<style scoped>
.btn-danger {
  color: #fff;
  background-color: #d92550 !important;
  border-color: #d92550;
}

.btn-danger.disabled,
.btn-danger:disabled {
  background-color: #d92550 !important;
}

.theme--light.v-btn.v-btn--disabled:not(.v-btn--icon):not(.v-btn--flat):not(.v-btn--outline) {
  background-color: #d92550 !important;
}

.theme--light.v-btn.v-btn--disabled {
  color: #fff !important;
}

.padding-price {
  padding-left: 13px;
}

.button-group .button-item {
  margin-right: 90px;
}

.button-group .button-width {
  min-width: 140px;
}

.btn-update:disabled,
.button-disabled:disabled {
  background-color: #3f6ad8 !important;
}

.text-error {
  padding-top: unset;
  padding-bottom: unset;
}

.text-error span {
  padding-left: 15px;
}

.fix-padding-lable {
  padding-bottom: unset;
  padding-top: unset;
  margin-bottom: unset;
  padding-left: unset;
}

#title-shipping {
  font-size: 17px !important;
  color: #040404;
}

.fix-lable-shop {
  padding-bottom: unset;
  margin-bottom: unset;
  padding-left: unset;
}

.fix-padding-search {
  padding-top: 7px;
  padding-bottom: 7px;
}

.label-search,
.title-mailbouce  {
  color: #040404;
  font-weight: bold;
}

#padding-readonly {
  padding-left: 0;
}
</style>
