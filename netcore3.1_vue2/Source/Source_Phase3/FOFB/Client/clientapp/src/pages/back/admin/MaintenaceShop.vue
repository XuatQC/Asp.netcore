<template>
  <div>
    <VueElementLoading
      :active="loading"
      spinner="ring"
      color="var(--success)"
    />
    <div hidden>
      <b-form-file
        id="fileCsv"
        accept=".csv"
        @change="fileCsvChange"
        ref="fileCsv"
      ></b-form-file>
    </div>
    <div class="main-card">
      <div class="card card-body">
        <div class="row" style="padding-bottom: 8px">
          <div
            class="col-md-2 offset-md-8 btn-grp-search"
            style="text-align: right"
          >
            <button
              type="button"
              class="btn btn-primary btn-search"
              @click="showDialogCsv()"
            >
              CSV登録
            </button>
          </div>
          <div
            class="col-md-2 btn-grp-search"
            style="padding-right: 61px; text-align: right"
          >
            <button
              type="button"
              class="btn btn-primary btn-search"
              @click="openShopDetailDialog(true, null)"
            >
              登録
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="main-card">
      <!-- Search bar -->
      <div class="card card-body">
        <div class="row">
          <div class="col-md-4">
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search"
                >店舗コード</label
              >
              <div class="col-md-7 fix-padding-search">
                <b-form-input
                  v-model="searchShop.shopCd"
                  placeholder=""
                  single-line
                  hide-details
                />
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search"
                >店舗名</label
              >
              <div class="col-md-7 fix-padding-search">
                <b-form-input
                  v-model="searchShop.shopName"
                  placeholder=""
                  single-line
                  hide-details
                />
              </div>
            </div>
          </div>
          <div class="col-md-4">
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search"
                >住所</label
              >
              <div class="col-md-7 fix-padding-search">
                <b-form-input
                  v-model="searchShop.address"
                  placeholder=""
                  single-line
                  hide-details
                />
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search"
                >エリア２</label
              >
              <div class="col-md-7 fix-padding-search">
                <b-form-select
                  :options="area2Options"
                  v-model="searchShop.area2Cd"
                ></b-form-select>
              </div>
            </div>
          </div>
          <div class="col-md-4">
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-5 label-search fix-padding-search"
                >メールアドレス</label
              >
              <div class="col-md-7 fix-padding-search">
                <b-form-input
                  v-model="searchShop.mailAddress"
                  placeholder=""
                  single-line
                  hide-details
                />
              </div>
            </div>
             <div class="row col-md-12 fix-padding-search">
              <label class="col-md-5 label-search fix-padding-search"
                >表示ステータス</label
              >
              <div class="col-md-7 fix-padding-search">
                 <b-form-select
                    :options="disPlayFlgOptions"
                    placeholder=""
                    v-model="searchShop.displayFlg"
                  ></b-form-select>
              </div>
            </div>
            <div class="row col-md-12 btn-grp-search">
              <div class="col-md-4 offset-md-4" style="text-align: right; padding-right: unset;">
                <button
                  type="button"
                  class="btn btn-primary btn-search"
                  @click="search()"
                >
                  検索
                </button>
              </div>
              <div class="col-md-4" style="text-align: right;">
                <button
                  type="button"
                  class="btn btn-secondary btn-clear"
                  @click="clearSearch()"
                >
                  クリア
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="main-card">
      <div class="card card-body">
        <div class="pagination">
          <div class="col-md-12" v-if="totalItem > 0">
            <div
              class="row"
              :style="`float: right; ${
                totalItem > itemsPerPage ? 'margin-right: -22px;' : ''
              }`"
            >
              <p
                :class="`${
                  totalItem > itemsPerPage ? 'page-info' : 'no-pagination'
                }`"
              >
                全 {{ totalItem }} 件中 {{ pageStart }} 件 〜
                {{ pageStop }} 件を表示
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
          :headers="headersLstShop"
          :items="lstShop"
          item-key="shopCd"
          hide-default-footer
          :items-per-page="itemsPerPage"
          :no-results-text="msgNoData"
          :no-data-text="msgNoData"
          @update:options="changeSort"
        >
          <template v-slot:item="{ item }">
            <tr>
              <td class="text-left">
                <a @click="openShopDetailDialog(false, item)" class="router">{{
                  item.shopCd
                }}</a>
              </td>
              <td>
                {{ item.shopName }}
              </td>
              <td class="text-left">
                {{
                  "〒" +
                  item.zipCodeDsp +
                  item.provinceName +
                  item.adrCd1 +
                  item.adrCd2
                }}
              </td>
              <td class="text-left">
                {{ item.area2Name }}
              </td>
              <td class="text-left">
                {{ item.mailAddress }}
              </td>
              <td class="text-left">
                {{ item.displayFlg ? ITEM_HIDDEN_TEXT : ITEM_SHOW_TEXT }}
              </td>
            </tr>
          </template>
        </v-data-table>
        <!-- shop detail dialog -->
        <v-dialog v-model="dialogMShop" max-width="700px">
          <v-card>
            <VueElementLoading
              :active="loadingPopup"
              spinner="ring"
              color="var(--success)"
            />
            <v-card-title>
              <span class="headline">店舗情報</span>
            </v-card-title>
            <v-card-text>
              <div class="main-card">
                <!-- shop info -->
                <div>
                  <b-row>
                    <b-col>
                      <div>
                        <!-- shopCd -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>店舗コード</strong
                              ><span class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-5"
                              type="text"
                              name="shopCd"
                              v-if="isAddShop"
                              v-on:keypress="isNumber()"
                              v-model="shopInfo.shopCd"
                              @keyup="checkValidShopCd(true)"
                              autocomplete="off"
                            />
                            <span v-else>{{ shopInfo.shopCd }}</span>
                            <span
                              class="invalid-feedback d-block"
                              v-if="shopCdError != ''"
                              >{{ this.shopCdError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- shopName -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>店舗名</strong
                              ><span class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-10"
                              type="text"
                              name="shopName"
                              v-model="shopInfo.shopName"
                              @keyup="checkValidShopName()"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="shopNameError != ''"
                              >{{ this.shopNameError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- password -->
                        <b-row v-if="!isAddShop">
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>パスワード</strong
                              ></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <button
                              class="btn btn-primary btn-reset-pass"
                              :disabled="loadingBtn"
                              @click="dialogConfirmResetPassWord = true"
                            >
                              パスワードリセット
                              <p>(店舗コードと同じにする）</p>
                            </button>
                          </b-col>
                        </b-row>
                        <!-- zipCd -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>郵便番号</strong
                              ><span class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-5"
                              type="number"
                              v-on:keypress="isNumber()"
                              name="zipCd"
                              v-model="shopInfo.zipCd"
                              @keyup="checkValidZipCd(true)"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="zipCdError != ''"
                              >{{ this.zipCdError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- province -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>都道府県</strong
                              ><span class="text-danger"> *</span></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-select
                              class="col-lg-5"
                              name="province"
                              v-model="shopInfo.province"
                              :options="provinceOptions"
                              @change="checkValidProvince()"
                            ></b-form-select>
                            <span
                              class="invalid-feedback d-block"
                              v-if="provinceError != ''"
                              >{{ this.provinceError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- adrCd1 -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>所在地１</strong
                              ><span class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-10"
                              type="text"
                              name="AdrCd1"
                              v-model="shopInfo.adrCd1"
                              @keyup="checkValidAdrCd1()"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="adrCd1Error != ''"
                              >{{ this.adrCd1Error }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- adrCd2 -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>所在地２</strong
                              ><span class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-10"
                              type="text"
                              name="AdrCd2"
                              v-model="shopInfo.adrCd2"
                              @keyup="checkValidAdrCd2()"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="adrCd2Error != ''"
                              >{{ this.adrCd2Error }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- area2Cd -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>エリア２</strong
                              ><span class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-select
                              class="col-lg-5"
                              name="area2"
                              v-model="shopInfo.area2Cd"
                              :options="area2Options"
                              @change="checkValidArea2Cd()"
                            ></b-form-select>
                            <span
                              class="invalid-feedback d-block"
                              v-if="area2CdError != ''"
                              >{{ this.area2CdError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- areaCd -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>エリア名</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-select
                              class="col-lg-5"
                              name="areaCd"
                              v-model="shopInfo.areaCd"
                              :options="areaOptions"
                            ></b-form-select>
                          </b-col>
                        </b-row>
                        <!-- mailAddress -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>メールアドレス</strong
                              ><span class="text-danger">*</span></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-8"
                              type="text"
                              name="mailAddress"
                              v-model="shopInfo.mailAddress"
                              @keyup="checkValidMailAddress()"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="mailAddressError != ''"
                              >{{ this.mailAddressError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- phoneNumber -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>TEL</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-5"
                              type="text"
                              name="phoneNumber"
                              v-model="shopInfo.phoneNumber"
                              v-on:keypress="isNumberAndDash()"
                              @keyup="checkValidPhoneNumber()"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="phoneNumberError != ''"
                              >{{ this.phoneNumberError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- faxNumber -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>Fax</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-5"
                              type="text"
                              v-model="shopInfo.faxNumber"
                              v-on:keypress="isNumberAndDash()"
                              @keyup="checkValidFaxNumber()"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="faxNumberError != ''"
                              >{{ this.faxNumberError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- Status -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>出退店区分</strong
                              ><span class="text-danger"> *</span></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-select
                              class="col-lg-5"
                              name="status"
                              v-model="shopInfo.status"
                              :options="statusOptions"
                              @change="checkValidStatus()"
                            ></b-form-select>
                            <span
                              class="invalid-feedback d-block"
                              v-if="statusError != ''"
                              >{{ this.statusError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- startDate -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>開店日</strong>
                              <span
                                v-if="
                                  (isAddShop &&
                                    shopInfo.status != null &&
                                    !shopInfo.status) ||
                                  (shopInfo.status != null && !shopInfo.status)
                                "
                                class="text-danger"
                              >
                                *</span
                              ></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <div class="row col-md-11">
                              <v-menu
                                v-model="menuDateStart"
                                :close-on-content-click="false"
                                min-width="auto"
                              >
                                <template v-slot:activator="{ on, attrs }">
                                  <b-form-input
                                    class="col-lg-6"
                                    v-model="startDate"
                                    readonly
                                    v-bind="attrs"
                                    v-on="on"
                                  ></b-form-input>
                                </template>
                                <v-date-picker
                                  class="startDate"
                                  locale="ja-JA"
                                  ref="startDatePicker"
                                  v-model="startDate"
                                  @input="menuDateStart = false"
                                  @change="changeStartDate()"
                                  :max="endDate"
                                ></v-date-picker>
                              </v-menu>
                              <div class="col-md-6 button-remove">
                                <span
                                  class="event-row"
                                  @click="removeStartDate()"
                                >
                                  <b-icon-x-circle-fill />
                                </span>
                              </div>
                            </div>
                            <span
                              class="invalid-feedback d-block"
                              v-if="startDateError != ''"
                              >{{ this.startDateError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- endDate -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>閉店日</strong>
                              <span
                                v-if="
                                  (isAddShop &&
                                    shopInfo.status != null &&
                                    shopInfo.status) ||
                                  (shopInfo.status != null && shopInfo.status)
                                "
                                class="text-danger"
                              >
                                *</span
                              ></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <div class="row col-md-11">
                              <v-menu
                                v-model="menuDateEnd"
                                :close-on-content-click="false"
                                min-width="auto"
                              >
                                <template v-slot:activator="{ on, attrs }">
                                  <b-form-input
                                    class="col-lg-6"
                                    v-model="endDate"
                                    readonly
                                    v-bind="attrs"
                                    v-on="on"
                                  ></b-form-input>
                                </template>
                                <v-date-picker
                                  class="endDate"
                                  locale="ja-JA"
                                  ref="endDatePicker"
                                  v-model="endDate"
                                  @input="menuDateEnd = false"
                                  @change="changeEndDate()"
                                  :min="startDate"
                                ></v-date-picker>
                              </v-menu>
                              <div class="col-md-6 button-remove">
                                <span
                                  class="event-row"
                                  @click="removeEndDate()"
                                >
                                  <b-icon-x-circle-fill />
                                </span>
                              </div>
                            </div>
                            <span
                              class="invalid-feedback d-block"
                              v-if="endDateError != ''"
                              >{{ this.endDateError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- AffiliateDepartmentCd -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>所属部門</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-5"
                              type="text"
                              name="affiliateDepartmentCd"
                              v-on:keypress="isNumber()"
                              v-model="shopInfo.affiliateDepartmentCd"
                              @keyup="checkValidAffiliateDepartmentCd()"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="affiliateDepartmentCdError != ''"
                              >{{ this.affiliateDepartmentCdError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- AffiliateDepartmentName -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>所属部門名</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-8"
                              type="text"
                              name="affiliateDepartmentName"
                              v-model="shopInfo.affiliateDepartmentName"
                              @keyup="checkValidAffiliateDepartmentName()"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="affiliateDepartmentNameError != ''"
                              >{{ this.affiliateDepartmentNameError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- OpenTime -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>営業時間</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <div class="row col-md-12">
                              <v-menu
                                ref="menuTimeStart"
                                v-model="menuStart"
                                :close-on-content-click="false"
                                max-width="290px"
                                min-width="290px"
                              >
                                <template v-slot:activator="{ on, attrs }">
                                  <b-form-input
                                    class="col-md-3"
                                    v-model="openTimeStart"
                                    readonly
                                    v-bind="attrs"
                                    v-on="on"
                                  ></b-form-input>
                                </template>
                                <v-time-picker
                                  v-if="menuStart"
                                  v-model="openTimeStart"
                                  :max="openTimeEnd"
                                  format="24hr"
                                  no-title
                                  @click:minute="
                                    $refs.menuTimeStart.save(openTimeStart)
                                  "
                                  @change="checkValidOpenTimeStart()"
                                ></v-time-picker>
                              </v-menu>
                              <span
                                style="
                                  padding-left: 10px;
                                  padding-right: 10px;
                                  padding-top: 6px;
                                "
                              >
                                -
                              </span>
                              <v-menu
                                ref="menuTimeEnd"
                                v-model="menuEnd"
                                :close-on-content-click="false"
                                max-width="290px"
                                min-width="290px"
                              >
                                <template v-slot:activator="{ on, attrs }">
                                  <b-form-input
                                    class="col-md-3"
                                    v-model="openTimeEnd"
                                    readonly
                                    v-bind="attrs"
                                    v-on="on"
                                  ></b-form-input>
                                </template>
                                <v-time-picker
                                  v-if="menuEnd"
                                  v-model="openTimeEnd"
                                  :min="openTimeStart"
                                  format="24hr"
                                  no-title
                                  @click:minute="
                                    $refs.menuTimeEnd.save(openTimeEnd)
                                  "
                                  @change="checkValidOpenTimeEnd()"
                                ></v-time-picker>
                              </v-menu>
                              <div class="col-md-3 button-remove">
                                <span
                                  class="event-row"
                                  @click="removeOpenTime()"
                                >
                                  <b-icon-x-circle-fill />
                                </span>
                              </div>
                            </div>
                            <span
                              class="invalid-feedback d-block"
                              v-if="openTimeError != ''"
                              >{{ this.openTimeError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- square -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>坪数</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-5"
                              type="text"
                              name="square"
                              v-model="shopInfo.square"
                              @keyup="checkValidSquare()"
                              v-on:keypress="isNumberAndDot()"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="squareError != ''"
                              >{{ this.squareError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- SalePersonCd -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>営業担当者</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-5"
                              type="text"
                              name="salePersonCd"
                              v-on:keypress="isNumber()"
                              v-model="shopInfo.salePersonCd"
                              @keyup="checkValidSalePersonCd()"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="salePersonCdError != ''"
                              >{{ this.salePersonCdError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- salePersonName -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>営業担当者名</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-input
                              class="col-lg-8"
                              type="text"
                              name="salePersonName"
                              v-model="shopInfo.salePersonName"
                              @keyup="checkValidSalePersonName()"
                              autocomplete="off"
                            />
                            <span
                              class="invalid-feedback d-block"
                              v-if="salePersonNameError != ''"
                              >{{ this.salePersonNameError }}</span
                            >
                          </b-col>
                        </b-row>
                        <!-- UsedDutyFree -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>免税使用</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-select
                              class="col-lg-5"
                              name="usedDutyFree"
                              v-model="shopInfo.usedDutyFree"
                              :options="usedDutyFreeOptions"
                            ></b-form-select>
                          </b-col>
                        </b-row>
                        <!-- contractType -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>契約形態</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <b-form-select
                              class="col-lg-5"
                              name="contractType"
                              v-model="shopInfo.contractType"
                              :options="contractTypeOptions"
                            ></b-form-select>
                          </b-col>
                        </b-row>
                        <!-- DisplayFlg -->
                        <b-row>
                          <b-col lg="3">
                            <label class="color-black"
                              ><strong>表示/非表示</strong></label
                            >
                          </b-col>
                          <b-col lg="8">
                            <v-switch
                              style="margin: unset; padding-top: unset"
                              v-model="displayFlgShop"
                              :label="
                                displayFlgShop === true
                                  ? ITEM_SHOW_TEXT
                                  : ITEM_HIDDEN_TEXT
                              "
                            ></v-switch>
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
                      @click="dialogMShop = false"
                    >
                      キャンセル
                    </button>
                    <v-spacer></v-spacer>
                    <button
                      @click="showDialogMShopConfirm()"
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
        <!-- mshop confirm dialog-->
        <v-dialog
          v-model="dialogMShopConfirm"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="title-confirm">
                店舗情報を{{
                  isAddShop ? this.TEXT_ADD : this.TEXT_UPD
                }}します。よろしいですか。
              </div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn class="btn_confirm_no" @click="dialogMShopConfirm = false"
                >キャンセル</v-btn
              >
              <v-btn
                class="btn_confirm_yes"
                :loading="loadingBtn"
                @click="submitMShopInfo()"
                >保存</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- Dialog csv start -->
        <v-dialog v-model="dialogCsv" max-width="1500px">
           <VueElementLoading
            :active="loadingPopup"
            spinner="ring"
            color="var(--success)"
          />
          <v-card>
            <div class="row">
              <div class="col" style="text-align: left;margin-bottom: -15px;">
                <button
                  @click="importCsv()"
                  style="margin: 20px"
                  class="btn btn-danger"
                >
                  CSVファイル選択
                </button>
              </div>
            </div>
            <span class="text-danger ml-5">※選択している業態のデータを読み込むので、注意してください。</span>
            <div class="row mb-1">
              <div
                class="col-md-12 col-lg-12 text-xs-right"
                v-if="dataCsv.length > 0"
              >
                <div class="row" style="float: right; padding-right: 6px">
                  <p
                    style="
                      margin-bottom: unset;
                      line-height: 3;
                      padding-right: 10px;
                    "
                  >
                    全 {{ itemsLengthCsv }} 件中 {{ pageStartCsv }} 件 〜
                    {{ pageStopCsv }} 件を表示
                  </p>
                  <v-pagination
                    v-if="dataCsv.length > this.itemsPerPage"
                    :total-visible="7"
                    @input="changePageCsv()"
                    v-model="pageCsv"
                    :length="pageCount"
                  ></v-pagination>
                </div>
              </div>
              <div v-else style="height: 42px"></div>
            </div>
            <!-- table data user start -->
            <v-data-table
              class="table-csv"
              v-model="selectedCsv"
              :headers="headersTblShopCSV"
              :items="dataCsv"
              hide-default-footer
              :page.sync="pageCsv"
              :items-per-page="itemsPerPage"
              @page-count="pageCount = $event"
              show-select
              item-key="shopCd"
              no-results-text=""
              no-data-text=""
              @toggle-select-all="selectAllCsv"
              @update:options="changeSortCsv"
            >
              <template v-slot:item="{ item }">
                <tr>
                  <td v-if="item.txtError === null" class="text-center">
                    <input
                      class="item-checkbox"
                      type="checkbox"
                      :ripple="false"
                      v-model="selectedCsv"
                      :value="item"
                      hide-details
                    />
                  </td>
                  <td class="text-center" v-else>
                    <button
                      @click="showError(item.txtError)"
                      class="btn btn-danger"
                    >
                      エラー
                    </button>
                  </td>
                  <td class="text-left">{{ item.shopCd }}</td>
                  <td class="text-left">{{ item.shopName }}</td>
                  <td class="text-left">{{ item.zipCd }}</td>
                  <td class="text-left">{{ item.provinceName }}</td>
                  <td class="text-left">{{ item.adrCd1 }}</td>
                  <td class="text-left">{{ item.adrCd2 }}</td>
                  <td class="text-left">{{ item.txtArea2Cd }}</td>
                  <td class="text-left">{{ item.txtAreaCd }}</td>
                  <td class="text-left">{{ item.mailAddress }}</td>
                  <td class="text-left">{{ item.phoneNumber }}</td>
                  <td class="text-left">{{ item.faxNumber }}</td>
                  <td class="text-left">{{ item.textStatus }}</td>
                  <td class="text-left">{{ item.startDate }}</td>
                  <td class="text-left">{{ item.endDate }}</td>
                  <td class="text-left">{{ item.affiliateDepartmentCd }}</td>
                  <td class="text-left">{{ item.affiliateDepartmentName }}</td>
                  <td class="text-left">{{ item.openTime }}</td>
                  <td class="text-left">
                    {{ item.square != null ? item.square : item.textSquare }}
                  </td>
                  <td class="text-left">{{ item.salePersonCd }}</td>
                  <td class="text-left">{{ item.salePersonName }}</td>
                  <td class="text-left">{{ item.textUsedDutyFree }}</td>
                  <td class="text-left">{{ item.textContractType }}</td>
                  <td class="text-left">{{ item.textDisplayFlg }}</td>
                </tr>
              </template>
            </v-data-table>
            <!-- table data user end  -->
            <!-- bottm button-->
            <div style="margin-top: 10px;">
              <v-card-actions>
                <v-spacer></v-spacer>
                <button class="btn btn-primary" @click="dialogCsv = false">
                  キャンセル
                </button>
                <v-spacer></v-spacer>
                <button
                  @click="confirmInsertCsv()"
                  style="width: 90px"
                  class="btn btn-danger"
                >
                  保存
                </button>
                <v-spacer></v-spacer>
              </v-card-actions>
            </div>
          </v-card>
        </v-dialog>
        <!-- Dialog csv end -->
        <!-- Dialog confirm import csv start -->
        <v-dialog
          v-model="dialogConfirmInsertCsv"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="text-center">登録します。よろしいですか。</div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn
                class="btn_confirm_no"
                @click="dialogConfirmInsertCsv = false"
                >キャンセル</v-btn
              >
              <v-btn
                class="btn_confirm_yes"
                :loading="loadingBtn"
                @click="insertCsv()"
                >保存</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- Dialog confirm import csv end -->
        <!-- Dialog show Message Error start -->
        <v-dialog v-model="dialogError" max-width="700px">
          <v-card>
            <v-card-title>
              <span class="headline">エラー一覧</span>
            </v-card-title>
            <v-card-text>
              <h6 class="memo_newLine text-danger">{{ messError }}</h6>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <button class="btn btn-primary" @click="dialogError = false">
                キャンセル
              </button>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- Dialog show Message Error end -->
        <!-- Dialog confirm reset password shop start -->
        <v-dialog
          v-model="dialogConfirmResetPassWord"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="text-center">パスワードをリセットします。よろしいですか。</div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn
                class="btn_confirm_no"
                @click="dialogConfirmResetPassWord = false"
                >キャンセル</v-btn
              >
              <v-btn class="btn_confirm_yes" @click="resetPass()">保存</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!-- Dialog confirm reset password shop end -->
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      loading: false,
      loadingPopup: false,
      loadingBtn: false,
      isSuperAdmin: null,
      bussinessCd: "",
      updateUserCd: "",
      itemsPerPage: 0,
      totalData: 0,
      page: 1,
      isChangePage: true,
      sortColumnName: "",
      isDesc: true,
      pageCsv: 1,
      pageCount: 0,
      searchShop: {},
      lstShop: [],
      isAddShop: null,
      isShopCdExist: null,
      isZipCodeNotExist: null,
      areaOptions: [{ text: "", value: 0 }],
      area2Options: [{ text: "", value: 0 }],
      provinceOptions: [{ text: "", value: null, disabled: true }],
      statusOptions: [
        { text: "", value: null },
        { text: "出店", value: false },
        { text: "退店", value: true },
      ],
      usedDutyFreeOptions: [
        { text: "", value: null },
        { text: "あり", value: false },
        { text: "なし", value: true },
      ],
       disPlayFlgOptions: [
        { text: "", value: null},
        { text: "非表示", value: true },
        { text: "表示", value: false },
      ],
      contractTypeOptions: [{ text: "", value: null }],
      headersLstShop: [
        {
          text: "店舗コード",
          sortable: true,
          align: "center",
          value: "shopCd",
          width: "5%",
        },
        {
          text: "店舗名",
          align: "center",
          sortable: true,
          value: "shopName",
          width: "20%",
        },
        {
          text: "住所",
          align: "center",
          sortable: true,
          value: "zipCd",
          width: "35%",
        },
        {
          text: "エリア2",
          align: "center",
          sortable: true,
          value: "area2Cd",
          width: "10%",
        },
        {
          text: "メールアドレス",
          align: "center",
          sortable: false,
          width: "20%",
        },
        {
          text: "表示ステータス",
          align: "center",
          sortable: false,
          width: "10%",
        },
      ],
      headersTblShopCSV: [
        {
          text: "店舗コード",
          value: "shopCd",
          sortable: true,
          align: "left",
        },
        {
          text: "店舗名",
          value: "shopName",
          sortable: true,
          align: "left",
        },
        {
          text: "郵便番号",
          value: "zipCd",
          sortable: true,
          align: "left",
        },
        {
          text: "都道府県",
          sortable: false,
          align: "left",
        },
        {
          text: "所在地１",
          sortable: false,
          align: "left",
        },
        {
          text: "所在地２",
          sortable: false,
          align: "left",
        },
        {
          text: "エリア２",
          sortable: false,
          align: "left",
        },
        {
          text: "エリア名",
          sortable: true,
          value: "areaCd",
          align: "left",
        },
        {
          text: "メールアドレス",
          sortable: false,
          align: "left",
        },
        {
          text: "TEL",
          sortable: false,
          align: "left",
        },
        {
          text: "FAX",
          sortable: false,
          align: "left",
        },
        {
          text: "出退店区分",
          sortable: false,
          align: "left",
        },
        {
          text: "開店日",
          sortable: false,
          align: "left",
        },
        {
          text: "閉店日",
          sortable: false,
          align: "left",
        },
        {
          text: "所属部門",
          sortable: false,
          align: "left",
        },
        {
          text: "所属部門名",
          sortable: false,
          align: "left",
        },
        {
          text: "営業時間",
          sortable: false,
          align: "left",
        },
        {
          text: "坪数",
          sortable: false,
          align: "left",
        },
        {
          text: "営業担当者",
          sortable: false,
          align: "left",
        },
        {
          text: "営業担当者名",
          sortable: false,
          align: "left",
        },
        {
          text: "免税使用",
          sortable: false,
          align: "left",
        },
        {
          text: "契約形態",
          sortable: false,
          align: "left",
        },
        {
          text: "表示ステータス",
          sortable: false,
          align: "left",
        },
      ],
      startDate: null,
      endDate: null,
      oldStartDate: null,
      oldEndDate: null,
      menuDateStart: false,
      menuDateEnd: false,
      openTimeStart: null,
      openTimeEnd: null,
      oldOpenTimeStart: null,
      oldOpenTimeEnd: null,
      menuStart: false,
      menuEnd: false,
      displayFlgShop: null,
      oldDisplayFlgShop: null,
      shopInfo: {},
      oldShopInfo: {},
      dialogMShop: false,
      dialogMShopConfirm: false,
      dialogConfirmResetPassWord: false,
      shopCdError: "",
      shopNameError: "",
      zipCdError: "",
      provinceError: "",
      adrCd1Error: "",
      adrCd2Error: "",
      area2CdError: "",
      mailAddressError: "",
      phoneNumberError: "",
      faxNumberError: "",
      statusError: "",
      startDateError: "",
      endDateError: "",
      affiliateDepartmentCdError: "",
      affiliateDepartmentNameError: "",
      openTimeError: "",
      squareError: "",
      salePersonCdError: "",
      salePersonNameError: "",
      dialogCsv: false,
      dataCsv: [],
      selectedCsv: [],
      dialogConfirmInsertCsv: false,
      dialogInforShoplErr: false,
      dialogError: false,
      messError: "",
    };
  },
  computed: {
    totalPage() {
      if (this.itemsPerPage == null || this.totalData == null) {
        return 0;
      }
      return Math.ceil(this.totalData / this.itemsPerPage);
    },
    pageStop() {
      if (this.page < Math.ceil(this.totalData / this.itemsPerPage)) {
        return this.itemsPerPage * this.page;
      } else {
        return this.totalData;
      }
    },
    pageStart() {
      return (this.page - 1) * this.itemsPerPage + 1;
    },
    totalItem() {
      return this.totalData;
    },
    pageStopCsv() {
      if (this.pageCsv < Math.ceil(this.dataCsv.length / this.itemsPerPage)) {
        return this.pageCsv * this.itemsPerPage;
      } else {
        return this.dataCsv.length;
      }
    },
    pageStartCsv() {
      if (this.dataCsv.length == 0) {
        return 0;
      } else {
        return (this.pageCsv - 1) * this.itemsPerPage + 1;
      }
    },
    itemsLengthCsv() {
      return this.dataCsv.length;
    },
  },
  async created() {
    this.loading = true;
    this.isSuperAdmin =
      this.getUserData().roleId == this.ROLE_SUPER_ADMIN ? true : false;
    if (!this.isSuperAdmin) {
      this.$router.push({ name: "404" });
    }
    this.bussinessCd = this.getUserData().bussinessCd;
    this.updateUserCd = this.getUserData().userCd;
    this.itemsPerPage = this.PAGE_SIZE_DEFAULT;

    await this.getListArea2();

    await this.getListArea();

    await this.getListProvince();

    await this.getListContractType();

    await this.getListShop(this.PAGE_START_DEFAULT);
  },
  watch: {
    menuDateEnd () {
      this.resetInitDate(this.$refs.endDatePicker)
    },
    menuDateStart () {
     this.resetInitDate(this.$refs.startDatePicker)
    },
  },
  methods: {
    resetInitDate(dateRef){
        setTimeout(() => (dateRef.activePicker = 'DATE'))
    },
    // 店舗コードバリデーション
    async checkValidShopCd(isGetShopCd) {
      if (
        this.shopInfo.shopCd === "" ||
        this.shopInfo.shopCd === undefined ||
        (this.shopInfo.shopCd.length > 0 && this.shopInfo.shopCd.trim() === "")
      ) {
        this.shopCdError = this.msgPleaseInput.replace("*", this.SHOP_CD);
      } else if (this.shopInfo.shopCd.length !== 6) {
        this.shopCdError = this.msgLengthTooLong
          .replace("*", this.SHOP_CD)
          .replace("$length", 6);
      } else {
        this.shopCdError = "";
      }

      if (this.shopCdError == "") {
        if (isGetShopCd) {
          let shopCdInfo = await this.getListShopByShopCd(this.shopInfo.shopCd);
          if (shopCdInfo !== null && shopCdInfo.length > 0) {
            this.isShopCdExist = true;
          } else {
            this.isShopCdExist = false;
            this.shopCdError = "";
          }
        }

        if (this.isShopCdExist) {
          this.shopCdError = this.msgShopCdExist;
        }
      }
    },
    // 店舗名バリデーション
    checkValidShopName() {
      this.shopNameError = this.validateTextRequiredAndLength(
        this.shopInfo.shopName,
        60,
        this.SHOP_NAME
      );
    },
    // 郵便番号バリデーション
    async checkValidZipCd(isGetZipCode) {
      this.zipCdError = this.validateZipCode(
        this.shopInfo.zipCd,
        7,
        this.ZIPCODE
      );
      if (this.zipCdError == "") {
        if (isGetZipCode) {
          this.loadingPopup = true;
          let zipCodeInfo = await this.getListMPostCodeByZipcode(
            this.shopInfo.zipCd
          );
          if (zipCodeInfo === null || zipCodeInfo.length === 0) {
            this.isZipCodeNotExist = true;
            this.loadingPopup = false;
          } else {
            this.isZipCodeNotExist = false;
            this.zipCdError = "";
            this.shopInfo.zipCodeDsp = zipCodeInfo[0].zipCodeDsp;
            this.shopInfo.province = (zipCodeInfo[0].regionCd.toString().length < 2) ? "0" + zipCodeInfo[0].regionCd : zipCodeInfo[0].regionCd + "";
            this.shopInfo.provinceName = zipCodeInfo[0].province;
            this.shopInfo.adrCd1 = zipCodeInfo[0].city;
            this.shopInfo.adrCd2 = zipCodeInfo[0].nom;
            
            if(zipCodeInfo.length > 1)
            {
              zipCodeInfo.forEach(zipCode => {
                  if(zipCode.isPriority === true){
                    this.shopInfo.zipCodeDsp = zipCode.zipCodeDsp;
                    this.shopInfo.province = (zipCode.regionCd.toString().length < 2) ? "0" + zipCode.regionCd : zipCode.regionCd + "";
                    this.shopInfo.provinceName = zipCode.province;
                    this.shopInfo.adrCd1 = zipCode.city;
                    this.shopInfo.adrCd2 = zipCode.nom;
                  }
                });
            }

            if(this.shopInfo.provinceName)
            {
              this.provinceError = "";
            }

            if(this.shopInfo.adrCd1)
            {
              this.adrCd1Error = "";
            }
            
            if(this.shopInfo.adrCd2)
            {
              this.adrCd2Error = "";
            }
            
            this.loadingPopup = false;
          }
        }

        if (this.isZipCodeNotExist) {
          this.zipCdError = this.msgZipCodeExist;
        }
      }
      else
      {
          this.shopInfo.zipCodeDsp = "";
          this.shopInfo.province = "";
          this.shopInfo.provinceName = "";
          this.shopInfo.adrCd1 = "";
          this.shopInfo.adrCd2 = "";
      }
    },
    // 都道府県バリデーション
    checkValidProvince() {
      this.provinceError = this.validateProvince(
        this.shopInfo.province,
        this.PROVINCE
      );
    },
    // 所在地１入力バリデーション
    checkValidAdrCd1() {
      this.adrCd1Error = this.validateAdrCdRequired(
        this.shopInfo.adrCd1,
        200,
        this.ADR_CD1_SHOP
      );
    },
    // 所在地２入力バリデーション
    checkValidAdrCd2() {
      this.adrCd2Error = this.validateAdrCdRequired(
        this.shopInfo.adrCd2,
        200,
        this.ADR_CD2_SHOP
      );
    },
    // エリア２バリデーション
    checkValidArea2Cd() {
      this.area2CdError = this.validateArea(this.shopInfo.area2Cd, this.AREA2);
    },
    // メールアドレスバリデーション
    checkValidMailAddress() {
      this.mailAddressError = this.validateMailAddress(
        this.shopInfo.mailAddress,
        50,
        this.MAIL_ADDRESS
      );
    },
    // TELバリデーション
    checkValidPhoneNumber() {
      this.phoneNumberError = this.validateTelOrFaxUnRequired(
        this.shopInfo.phoneNumber,
        15,
        this.TEL
      );
    },
    // Faxバリデーション
    checkValidFaxNumber() {
      this.faxNumberError = this.validateTelOrFaxUnRequired(
        this.shopInfo.faxNumber,
        15,
        this.FAX
      );
    },
    // 出退店区分バリデーション
    checkValidStatus() {
      this.statusError = this.validateSelected(
        this.shopInfo.status,
        this.STORE_STATUS
      );

      if (this.shopInfo.status) {
        this.startDateError = "";
      } else if (!this.shopInfo.status) {
        this.endDateError = "";
      }
    },
    // 開店日バリデーション
    checkValidStartDate() {
      if (this.shopInfo.status != null && !this.shopInfo.status) {
        if (this.startDate == null) {
          this.startDateError = this.msgPleaseInput.replace(
            "*",
            this.START_DATE
          );
        } else {
          this.startDateError = "";
        }
      }
    },
    // 閉店日バリデーション
    checkValidEndDate() {
      if (this.shopInfo.status != null && this.shopInfo.status) {
        if (this.endDate == null) {
          this.endDateError = this.msgPleaseInput.replace("*", this.END_DATE);
        } else {
          this.endDateError = "";
        }
      }
    },
    // 所属部門バリデーション
    checkValidAffiliateDepartmentCd() {
      this.affiliateDepartmentCdError = this.validateTextLength(
        this.shopInfo.affiliateDepartmentCd,
        4,
        this.AFFILIATE_DEPARTMENT_CD
      );
    },
    // 所属部門名バリデーション
    checkValidAffiliateDepartmentName() {
      this.affiliateDepartmentNameError = this.validateTextLength(
        this.shopInfo.affiliateDepartmentName,
        30,
        this.AFFILIATE_DEPARTMENT_NAME
      );
    },
    // 営業時間バリデーション
    checkValidOpenTime() {
      if (
        (this.openTimeStart !== null || this.openTimeEnd !== null) &&
        ((this.openTimeStart === null && this.openTimeEnd !== null) ||
          (this.openTimeStart !== null && this.openTimeEnd === null))
      ) {
        this.openTimeError = this.msgPleaseInput.replace("*", this.OPEN_TIME);
      } else {
        this.openTimeError = "";
      }
    },
    // 営業時間バリデーション
    checkValidOpenTimeStart() {
      this.openTimeError = "";
    },
    // 営業時間バリデーション
    checkValidOpenTimeEnd() {
      this.openTimeError = "";
    },
    // 坪数バリデーション
    checkValidSquare() {
      if (
        this.shopInfo.square !== null &&
        this.shopInfo.square !== "" &&
        this.shopInfo.square !== undefined &&
        this.shopInfo.square.length > 0 &&
        this.shopInfo.square.trim() !== ""
      ) {
        if (this.shopInfo.square.split(".").length === 3) {
          this.squareError = this.msgInputFormat.replace("*", this.SQUARE);
        } else {
          this.squareError = "";
        }
      } else {
        if (this.shopInfo.square === "" || this.shopInfo.square === undefined) {
          this.shopInfo.square = null;
        }
        this.squareError = "";
      }
    },
    // 営業担当者バリデーション
    checkValidSalePersonCd() {
      this.salePersonCdError = this.validateTextLength(
        this.shopInfo.salePersonCd,
        3,
        this.SALE_PERSON_CD
      );
    },
    // 営業担当者名バリデーション
    checkValidSalePersonName() {
      this.salePersonNameError = this.validateTextLength(
        this.shopInfo.salePersonName,
        30,
        this.SALE_PERSON_NAME
      );
    },
    changeStartDate() {
      this.startDateError = "";
    },
    changeEndDate() {
      this.endDateError = "";
    },
    // 店舗一覧
    async getListShop(page) {
      this.loading = true;
      this.searchShop.bussinessCd = this.bussinessCd;
      const config = {
        method: "post",
        url: "api/MShop/GetDataMShopSubPagination",
        data: this.searchShop,
        params: {
          currentpage: page,
          pageSize: this.PAGE_SIZE_DEFAULT,
          sortColumnName: this.sortColumnName,
          isDesc: this.isDesc,
        },
      };
      try {
        const resultGetLstShop = await require("axios")(config);
        if (resultGetLstShop.data !== null) {
          this.lstShop = resultGetLstShop.data.data;
          this.totalData = resultGetLstShop.data.totalData;
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
    // 店舗詳細情報ダイアログを表示する
    async openShopDetailDialog(isAddShop, shopItem) {
      this.isAddShop = isAddShop;
      this.clearMessageError();
      this.startDate = null;
      this.endDate = null;
      this.openTimeStart = null;
      this.openTimeEnd = null;
      this.oldStartDate = null;
      this.oldEndDate = null;
      this.oldOpenTimeStart = null;
      this.oldOpenTimeEnd = null;

      if (isAddShop) {
        this.shopInfo = {};
        this.shopInfo.bussinessCd = this.bussinessCd;
        this.displayFlgShop = true;
      } else {
        this.startDate = this.formatDate(shopItem.startDate);
        this.endDate = this.formatDate(shopItem.endDate);
        this.oldStartDate = this.startDate;
        this.oldEndDate = this.endDate;
        this.shopInfo = { ...shopItem };
        this.oldShopInfo = { ...shopItem };
        if (shopItem.openTime !== null && shopItem.openTime !== '') {
          let openTime = shopItem.openTime.split('-');
          this.openTimeStart = openTime[0];
          this.openTimeEnd = openTime[1];
          this.oldOpenTimeStart = openTime[0];
          this.oldOpenTimeEnd = openTime[1];
        }
        this.displayFlgShop = !this.shopInfo.displayFlg;
        this.oldDisplayFlgShop = this.displayFlgShop;
      }

      this.dialogMShop = true;
    },
    // 店舗確認ダイアログを表示する
    showDialogMShopConfirm() {
      if (!this.isAddShop) {
        if (
          JSON.stringify(this.shopInfo) === JSON.stringify(this.oldShopInfo) &&
          this.startDate === this.oldStartDate &&
          this.endDate === this.oldEndDate &&
          this.openTimeStart === this.oldOpenTimeStart &&
          this.openTimeEnd === this.oldOpenTimeEnd &&
          this.displayFlgShop === this.oldDisplayFlgShop
        ) {
          this.$toastr.w(this.msgNotthingChanged);
          return;
        }
      }

      if (this.isValidateAll()) {
        this.dialogMShopConfirm = true;
        this.loadingBtn = false;
      }
    },
    // 店舗情報を登録・更新する
    async submitMShopInfo() {
      this.loading = true;
      this.dialogMShop = false;
      this.dialogMShopConfirm = false;
      this.loadingBtn = true;
      this.shopInfo.updateUserCd = this.updateUserCd;
      this.shopInfo.displayFlg = !this.displayFlgShop;

      if (this.startDate != null) {
        this.shopInfo.startDate = this.startDate.replaceAll("-", "");
      } else {
        this.shopInfo.startDate = null;
      }

      if (this.endDate != null) {
        this.shopInfo.endDate = this.endDate.replaceAll("-", "");
      } else {
        this.shopInfo.endDate = null;
      }

      if (this.openTimeStart != null && this.openTimeEnd != null) {
          this.shopInfo.openTime = this.openTimeStart + "-" + this.openTimeEnd;
      }
      else
      {
        this.shopInfo.openTime = null;
      }

      if (this.isAddShop) {
        const config = {
          method: "post",
          url: "api/MShop/InsertShopInfo",
          params: {},
          data: this.shopInfo,
        };
        try {
          const resultInsertShop = await require("axios")(config);
          if (resultInsertShop.data.error === undefined) {
            if (resultInsertShop.data.isInsertSuccess) {
              this.$toastr.s(this.msgInsertSucces);
            } else {
              this.$toastr.e(this.msgInsertFailed);
            }
            await this.getListShop(this.PAGE_START_DEFAULT);
          } else {
            this.dialogMShop = true;
            this.isShopCdExist = true;
            this.shopCdError = this.msgShopCdExist;
            this.$toastr.e(resultInsertShop.data.error);
          }
        } catch (error) {
          console.log(error);
        } finally {
          this.loading = false;
          this.loadingBtn = false;
        }
      } else {
        const config = {
          method: "post",
          url: "api/MShop/UpdateShopInfo",
          params: {},
          data: this.shopInfo,
        };
        try {
          const resultUpdShop = await require("axios")(config);
          if (resultUpdShop.data !== this.RESULT_ERROR) {
            if (resultUpdShop.data === this.RESULT_SUCCESS) {
              this.$toastr.s(this.msgUpdateSucces);
            } else {
              this.$toastr.w(this.msgUpdtedBefore);
            }
            await this.getListShop(this.PAGE_START_DEFAULT);
          } else {
            this.$toastr.e(this.msgUpdateFailed);
          }
        } catch (error) {
          console.log(error);
        } finally {
          this.shopInfo = {};
          this.loading = false;
          this.loadingBtn = false;
        }
      }
    },
    // パスワードリセット
    async resetPass() {
      this.dialogConfirmResetPassWord = false;
      this.loadingBtn = true;
      try {
        const resUpdatePass = await this.resetPassword(this.shopInfo.shopCd, this.updateUserCd);
        if (resUpdatePass == this.RESULT_SUCCESS) {
          this.$toastr.s(this.msgUpdateSucces);
        } else {
          this.$toastr.e(this.msgUpdateFailed);
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loadingBtn = false;
      }
    },
    // 検索
    search() {
      this.getListShop(this.PAGE_START_DEFAULT);
    },
    // 検索クリア
    clearSearch() {
      this.searchShop = {};
      this.getListShop(this.PAGE_START_DEFAULT);
    },
    // ページ変更
    async changePage(page) {
      await this.getListShop(page);
    },
    // 並び替える
    changeSort({ sortBy, sortDesc }) {
      if (sortBy[0] != undefined && sortDesc[0] != undefined) {
        if (sortBy[0] == "shopCd") {
          this.sortColumnName = "ms.shopCd";
        } else if (sortBy[0] === "shopName") {
          this.sortColumnName = "ms.ShopName";
        } else if (sortBy[0] === "zipCd") {
          this.sortColumnName = "adr.zipCd";
        } else if (sortBy[0] === "area2Cd") {
          this.sortColumnName = "ms.area2Cd";
        } else {
          this.sortColumnName = "ms.UpdateDtTm";
        }

        this.isDesc = sortDesc[0];

        this.getListShop(this.PAGE_START_DEFAULT);
      } else {
        this.sortColumnName = "ms.UpdateDtTm";
      }
    },
    // 店舗コードによる店舗
    async getListShopByShopCd(shopCd) {
      const config = {
        method: "get",
        url: "api/MShop/GetListShop",
        params: {
          shopCd: shopCd,
        },
      };

      try {
        const lstShopByShopCd = await require("axios")(config);
        if (lstShopByShopCd.data !== null) {
          return lstShopByShopCd.data;
        } else {
          return null;
        }
      } catch (error) {
        console.log(error);
      }
    },
    async getListArea() {
      let lstArea = await this.getParameterByCd(this.KBN_AREA_1);
      if (lstArea.length > 0) {
        for (var i = 0; i < lstArea.length; i++) {
          lstArea.sort((a, b) => (a.numberValue > b.numberValue) ? 1 : -1)
          this.areaOptions.push({
            text: lstArea[i].kbnValueName,
            value: parseInt(lstArea[i].kbnValue),
          });
        }
      }
    },
    async getListArea2() {
      let lstArea2 = await this.getParameterByCd(this.KBN_AREA);
      if (lstArea2.length > 0) {
        for (var i = 0; i < lstArea2.length; i++) {
          this.area2Options.push({
            text: lstArea2[i].kbnValueName,
            value: parseInt(lstArea2[i].kbnValue),
          });
        }
      }
    },
    async getListContractType() {
      let lstContractType = await this.getParameterByCd(this.KBN_CONTRACT_TYPE);
      if (lstContractType.length > 0) {
        for (var i = 0; i < lstContractType.length; i++) {
          this.contractTypeOptions.push({
            text: lstContractType[i].kbnValueName,
            value: parseInt(lstContractType[i].kbnValue),
          });
        }
      }
    },
    async getListProvince() {
      let lstProvince = await this.getParameterByCd(this.KBN_PROVINCE);
      if (lstProvince.length > 0) {
        for (var i = 0; i < lstProvince.length; i++) {
          this.provinceOptions.push({
            text: lstProvince[i].kbnValueName,
            value: lstProvince[i].kbnValue,
          });
        }
      }
    },
    isValidateAll() {
      this.checkValidShopCd(false);
      this.checkValidShopName();
      this.checkValidZipCd(false);
      this.checkValidProvince();
      this.checkValidAdrCd1();
      this.checkValidAdrCd2();
      this.checkValidArea2Cd();
      this.checkValidMailAddress();
      this.checkValidPhoneNumber();
      this.checkValidFaxNumber();
      this.checkValidStatus();
      this.checkValidStartDate();
      this.checkValidEndDate();
      this.checkValidAffiliateDepartmentCd();
      this.checkValidAffiliateDepartmentName();
      this.checkValidOpenTime();
      this.checkValidSquare();
      this.checkValidSalePersonCd();
      this.checkValidSalePersonName();

      if (
        this.shopCdError === "" &&
        this.shopNameError === "" &&
        this.zipCdError === "" &&
        this.provinceError === "" &&
        this.adrCd1Error === "" &&
        this.adrCd2Error === "" &&
        this.area2CdError === "" &&
        this.mailAddressError === "" &&
        this.phoneNumberError === "" &&
        this.faxNumberError === "" &&
        this.statusError === "" &&
        this.startDateError === "" &&
        this.endDateError === "" &&
        this.affiliateDepartmentCdError === "" &&
        this.affiliateDepartmentNameError === "" &&
        this.openTimeError === "" &&
        this.squareError === "" &&
        this.salePersonCdError === "" &&
        this.salePersonNameError === ""
      ) {
        return true;
      } else {
        return false;
      }
    },
    clearMessageError() {
      this.shopCdError = "";
      this.shopNameError = "";
      this.zipCdError = "";
      this.provinceError = "";
      this.adrCd1Error = "";
      this.adrCd2Error = "";
      this.area2CdError = "";
      this.mailAddressError = "";
      this.phoneNumberError = "";
      this.faxNumberError = "";
      this.statusError = "";
      this.startDateError = "";
      this.endDateError = "";
      this.affiliateDepartmentCdError = "";
      this.affiliateDepartmentNameError = "";
      this.openTimeError = "";
      this.squareError = "";
      this.salePersonCdError = "";
      this.salePersonNameError = "";
    },
    removeStartDate() {
      this.startDate = null;
      this.shopInfo.startDate = null;
      this.startDateError = "";
    },
    removeEndDate() {
      this.endDate = null;
      this.shopInfo.endDate = null;
      this.endDateError = "";
    },
    removeOpenTime() {
      this.openTimeStart = null;
      this.openTimeEnd = null;
      this.openTimeError = "";
    },
    //csv取込ダイアログを表示する
    showDialogCsv() {
      this.selectedCsv = [];
      this.dialogCsv = true;
      this.dataCsv = [];
    },
    //CSVファイルから店舗を追加する
    importCsv() {
      document.getElementById("fileCsv").click();
    },
    fileCsvChange(e) {
      if (e.target.files[0] != null) {
        this.fileCsv = e.target.files[0];
        this.readDataCsv();
      }
      this.$refs["fileCsv"].reset();
    },
    //csvファイルデータを読み込む
    async readDataCsv() {
      try {
        this.dataCsv = []
        this.selectedCsv = []
        this.loadingPopup = true;
        const formData = new FormData();
        formData.append("file", this.fileCsv);
        formData.append("bussinessCd", this.bussinessCd);
        const resDataCsv = await this.axios.post(
          "api/MShop/CheckDataCsv",
          formData,
          {
            headers: {
              "Content-type": "multipart/form-data",
            },
          }
        );
        if (resDataCsv.data.error === undefined) {
          if (resDataCsv.data.listShopCsv != null && resDataCsv.data.listShopCsv.length > 0) {
            this.dataCsv = resDataCsv.data.listShopCsv;
            this.dataCsv.map((data) => {
              let dataExist = this.dataCsv.filter((x) => x.shopCd == data.shopCd)
                if (dataExist.length > 1) {
                  data.txtError = data.txtError === null ? this.msgDataDuplicateInCsv.replace('*', this.SHOP_CD)
                                                          : data.txtError += this.msgDataDuplicateInCsv.replace('*', this.SHOP_CD);
                }
            })
          }
        } else {
          this.$toastr.e(resDataCsv.data.error);
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loadingPopup = false;
      }
    },
    //Csvソート順
    changeSortCsv() {
      if (this.isChangePage) {
        this.selectedCsv = [];
      } else {
        this.selectedCsv = [...this.selectedCsv];
        this.isChangePage = true;
      }
    },
    //csv取込ダイアログを表示する
    confirmInsertCsv() {
      if (this.dataCsv.length <= 0 || this.selectedCsv.length == 0) {
        this.$toastr.w(this.msgShopNotSelected);
        return;
      }

      let csvInsert = this.selectedCsv.filter((x) => x.txtError == null);
      if (csvInsert.length == 0) {
        this.$toastr.w(this.msgShopNotSelected);
        return;
      } else {
        this.selectedCsv = csvInsert;
        this.loadingBtn = false;
        this.dialogConfirmInsertCsv = true;
      }
    },
    //CSVファイルから店舗を追加する
    async insertCsv() {
      this.loading = true;
      this.loadingBtn = true;
      this.dialogConfirmInsertCsv = false;
      this.dialogCsv = false;
      let lstShopCdUpd = "";
      this.selectedCsv.map((dataCsv) => {
        if (!dataCsv.isAdd) {
          lstShopCdUpd += "'" + dataCsv.shopCd + "',";
        }
        dataCsv.updateUserCd = this.updateUserCd;
      })

      if (lstShopCdUpd !== "") {
        lstShopCdUpd = lstShopCdUpd.substring(0 , lstShopCdUpd.length -1);
      }

      let csvSubmit = {
        mShopSubmit: this.selectedCsv,
        listShopCd: lstShopCdUpd
      };

      const config = {
        method: "post",
        url: "api/MShop/InsertCsv",
        data: csvSubmit,
      };
      try {
        const resInsert = await require("axios")(config);
        if (resInsert.data.isInsertSuccess) {
          this.$toastr.s(this.msgInsertSucces);
        } else {
          this.$toastr.e(this.msgInsertFailed);
        }
      } catch (error) {
        this.$toastr.e(this.msgInsertFailed);
      } finally {
        await this.getListShop(this.PAGE_START_DEFAULT);
        this.loading = false;
        this.loadingBtn = false;
      }
    },
    //エラーを表示する
    showError(error) {
      this.dialogError = true;
      this.messError = error;
    },
    selectAllCsv() {
      this.selectedCsv = this.dataCsv;
    },
    // ページ変更
    changePageCsv() {
      this.isChangePage = false;
    },
  },
};
</script>

<style scoped>
.label-search {
  color: #040404;
  font-weight: bold;
}

.fix-padding-search {
  padding-top: 7px;
  padding-bottom: 7px;
}

.button-remove {
  padding-bottom: unset;
  padding-top: 7px;
  text-align: center;
}

.event-row {
  cursor: pointer;
  padding: 10px;
}

.form-control[readonly] {
  background-color: white;
}

.btn-reset-pass:disabled {
  background-color: #007bff !important;
}
</style>
