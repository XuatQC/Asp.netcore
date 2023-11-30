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
    <div hidden>
      <b-form-file
        id="imgMain"
        :accept="this.IMAGE_FILE"
        @change="imgMainChange"
        ref="imgMain"
      ></b-form-file>
    </div>
    <div hidden>
      <b-form-file
        id="imgDetail"
        :accept="this.IMAGE_FILE"
        @change="imgDetailChange"
        ref="imgDetail"
      ></b-form-file>
    </div>
    <div hidden>
      <b-form-file
        id="imgThumbnailNew"
        multiple
        :accept="this.IMAGE_FILE"
        @change="imgThumbnailNewChange"
        ref="imgThumbnailNew"
      ></b-form-file>
    </div>
    <div hidden>
      <b-form-file
        id="imgThumbnailNewSingle"
        multiple
        :accept="this.IMAGE_FILE"
        @change="imgThumbnailNewSingleChange"
        ref="imgThumbnailNewSingle"
      ></b-form-file>
    </div>
    <div class="main-card">
      <!-- Inventory symbol start -->
      <div class="card card-body">
        <div class="row" style="padding-bottom: 8px">
          <div class="btn-grp-search col-md-6">
            <p>在庫数表示</p>
            <br />
            <div class=" row btn-grp-search lg-12">
              <p class="col-lg-2">○ : ≧ {{ this.inventoryNumber }}</p>
              <p class="col-lg-2">△: ＜ {{ this.inventoryNumber }}</p>
              <p class="col-lg-2">×: 0</p>
              <button
                v-if="!isUserReadOnly"
                class="mb-2 btn btn-primary btn-search"
                @click="showDialogQuantity()"
              >
                変更
              </button>
            </div>
          </div>
          <div
            class="col-md-2 offset-md-2 btn-grp-search mt-3"
            style="text-align: right"
          >
            <br />
            <button
              type="button"
              v-if="!isUserReadOnly"
              class="btn btn-primary btn-search"
              @click="showDialogCsv()"
            >
              CSV登録
            </button>
          </div>
          <div
            class="col-md-2 btn-grp-search mt-3"
            style="padding-right: 61px; text-align: right"
          >
            <br />

            <button
              type="button"
              v-if="!isUserReadOnly"
              class="btn btn-primary btn-search"
              @click="showDialoginsertProduct()"
            >
              登録
            </button>
          </div>
        </div>
      </div>
      <!-- Inventory symbol end -->
      <!-- Search bar start -->
      <div class="main-card">
        <!-- Search bar -->
        <div class="card card-body">
          <div class="row">
            <div class="col-md-4">
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >品番</label
                >
                <div class="col-md-7 fix-padding-search">
                  <b-form-input
                    v-model="txtProductCode"
                    placeholder=""
                    single-line
                    hide-details
                  />
                </div>
              </div>
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >商品名</label
                >
                <div class="col-md-7 fix-padding-search">
                  <b-form-input
                    v-model="txtProductName"
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
                  >ブランド</label
                >
                <div class="col-md-7 fix-padding-search">
                  <b-form-select
                    v-model="cbbBrandName"
                    :options="brandNameOptionSearch"
                  ></b-form-select>
                </div>
              </div>
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >年</label
                >
                <div class="col-md-7 fix-padding-search">
                  <b-form-select
                    :options="lstYear"
                    placeholder=""
                    v-model="cbbYear"
                  ></b-form-select>
                </div>
              </div>
            </div>
            <div class="col-md-4">
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-5 label-search fix-padding-search"
                  >表示ステータス</label
                >
                <div class="col-md-7 fix-padding-search">
                  <b-form-select
                    :options="lstDisPlayFlg"
                    placeholder=""
                    v-model="cbbDisplay"
                  ></b-form-select>
                </div>
              </div>
              <div class="row col-md-12 btn-grp-search">
                <div
                  class="col-md-4 offset-md-4"
                  style="text-align: right; padding-right: unset;"
                >
                  <button
                    type="button"
                    @click="search()"
                    class="btn btn-primary btn-search"
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
      <!-- Search bar end -->
      <div class="card card-body">
        <!-- pagination for table product start  -->
        <div class="row mb-2 mt-3">
          <div class="col-md-12 col-lg-12 text-xs-right">
            <div class="row" style="float: right; padding-right: 6px">
              <p
                style="
                  margin-bottom: unset;
                  line-height: 3;
                  padding-right: 10px;
                "
              >
                全 {{ itemsLength }} 件中 {{ pageStart }} 件 〜
                {{ pageStop }} 件を表示
              </p>
              <v-pagination
                v-if="this.lstProduct.length > this.itemsPerPage"
                :total-visible="7"
                v-model="page"
                :length="pageCount"
              ></v-pagination>
            </div>
          </div>
        </div>
        <!-- pagination for table product end  -->

        <!-- table data product start  -->
        <v-data-table
          :headers="headers"
          :items="lstProduct"
          hide-default-footer
          :page.sync="page"
          :items-per-page="itemsPerPage"
          @page-count="pageCount = $event"
          class="elevation-1"
          :loading="tblProductLoading"
          :no-results-text="msgNoData"
          :no-data-text="msgNoData"
        >
          <template v-slot:item="{ item }">
            <tr>
              <td class="text-left">
                <strong
                  ><a @click="showDialogUpdateProduct(item)" class="router">{{
                    item.productCd
                  }}</a></strong
                >
              </td>
              <td class="text-left">{{ item.productName }}</td>
              <td class="text-left">
                {{ formatPrice(item.totalInventoryNumber) }}
              </td>
              <td class="text-left">{{ item.year }}</td>
              <td class="text-left">{{ item.brandName }}</td>
              <td class="text-left">
                {{ formatProductStatus(item.displayFlg) }}
              </td>
            </tr>
          </template>
        </v-data-table>
        <!-- table data product end  -->
      </div>

      <!-- Add, update, detail dialog start -->
      <v-dialog v-model="dialogProduct" max-width="950px">
        <v-card>
          <VueElementLoading
            :active="displayDialogProduct"
            spinner="ring"
            color="var(--success)"
          />
          <v-card-title>
            <span class="headline">商品情報</span>
          </v-card-title>
          <!-- content detail-->
          <v-card-text>
            <div class="main-card">
              <div>
                <!-- product detail-->
                <!--- Product code -->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>品番</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col xl="2" lg="5" md="12">
                    <b-form-input
                      name="txtProductCode"
                      type="text"
                      v-validate="'required'"
                      v-model="currentProduct.productCd"
                      placeholder=""
                      maxlength="7"
                      autocomplete="off"
                      :disabled="!isModeInsert"
                    />
                    <span class="invalid-feedback d-block">{{
                      errors.first("txtProductCode")
                    }}</span>
                    <span
                      v-if="
                        currentProduct.productCd != undefined &&
                          currentProduct.productCd.length < 7 &&
                          currentProduct.productCd.length > 0
                      "
                      class="invalid-feedback d-block"
                      >{{ msgMinTextProductCd }}</span
                    >
                  </b-col>
                </b-row>
                <!--- Product name -->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>商品名</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col xl="5" lg="5" md="12">
                    <b-form-input
                      :readonly="isUserReadOnly"
                      name="txtProductName"
                      type="text"
                      v-validate="'required'"
                      v-model="currentProduct.productName"
                      placeholder=""
                      maxlength="50"
                      autocomplete="off"
                    />
                    <span class="invalid-feedback d-block">{{
                      errors.first("txtProductName")
                    }}</span>
                  </b-col>
                </b-row>
                <!--- Product name 1-->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>商品名（略称）</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col xl="5" lg="5" md="12">
                    <b-form-input
                      :readonly="isUserReadOnly"
                      name="txtProductName1"
                      type="text"
                      v-validate="'required'"
                      v-model="currentProduct.productName1"
                      placeholder=""
                      maxlength="50"
                      autocomplete="off"
                    />
                    <span class="invalid-feedback d-block">{{
                      errors.first("txtProductName1")
                    }}</span>
                  </b-col>
                </b-row>
                <!--- Year-->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>年</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col xl="4" lg="5" md="12">
                    <b-form-input
                      name="txtYear"
                      type="text"
                      style="max-width: 120px"
                      :readonly="isUserReadOnly"
                      v-validate="'required'"
                      v-model="currentProduct.year"
                      maxlength="4"
                      v-on:keypress="isNumber()"
                      placeholder=""
                      autocomplete="off"
                    />
                    <span class="invalid-feedback d-block">{{
                      errors.first("txtYear")
                    }}</span>
                    <span
                      v-if="
                        currentProduct.year != undefined &&
                          currentProduct.year.length > 0 &&
                          currentProduct.year.length != 4
                      "
                      class="invalid-feedback d-block"
                      >{{ msgErrYear }}</span
                    >
                  </b-col>
                </b-row>
                <!--- Brand name-->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>ブランド名</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col xl="5" lg="5" md="12">
                    <b-form-select
                      :disabled="isUserReadOnly"
                      v-model="currentProduct.brandRltId"
                      @change="changeBrand(currentProduct.brandRltId)"
                      :options="brandNameOption"
                    ></b-form-select>
                    <span class="invalid-feedback d-block">
                      {{ this.msgErrNotChooseBranch }}
                    </span>
                  </b-col>
                  <b-col xl="3" lg="5" md="12">
                    <button
                      v-if="isModeInsert"
                      class="offset-lg-5 btn btn-primary"
                      @click="showDialogInsertBrand()"
                    >
                      追加
                    </button>
                  </b-col>
                </b-row>
                <!--- Sub Brand -->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>サブブランド</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col xl="2" lg="5" md="12">
                    <b-form-select
                      :disabled="isUserReadOnly"
                      v-model="currentProduct.subBrandId"
                      
                      :options="brandSubNameOption"
                    ></b-form-select>
                    <span class="invalid-feedback d-block">
                      {{ this.msgErrNotChooseSubBranch }}
                    </span>
                  </b-col>
                  <b-col xl="3" lg="5" md="12" offset-xl="3">
                    <button
                      v-if="isModeInsert"
                      class="offset-lg-5 btn btn-primary"
                      @click="showDialogInsertBrandSub()"
                    >
                      追加
                    </button>
                  </b-col>
                </b-row>
                <!--- Price -->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>価格（税抜き）</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col xl="6" lg="5" md="12">
                    <b-form-input
                      :readonly="isUserReadOnly"
                      name="txtPrice"
                      style="max-width: 120px;"
                      v-validate="'required'"
                      v-model="currentProduct.price"
                      v-money="money"
                      maxlength="10"
                      v-on:keypress="isNumber()"
                      v-on:keydown="checkPrice = checkWrongPrice = false"
                      placeholder=""
                      autocomplete="off"
                    />
                    <span class="invalid-feedback d-block">{{
                      errors.first("txtPrice")
                    }}</span>
                    <span v-if="checkPrice" class="invalid-feedback d-block">{{
                      msgErrPrice
                    }}</span>
                    <span
                      v-if="checkWrongPrice"
                      class="invalid-feedback d-block"
                      >{{ msgErrWrongQuantity }}</span
                    >
                  </b-col>
                </b-row>
                <!--- SKU -->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>サイズ/色/数量</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col xl="9" lg="5" md="12">
                    <div>
                      <div
                        v-for="(product, index) in this.lstProductSameCode"
                        :key="product.no"
                      >
                        <b-row>
                          <b-col xl="2" lg="4" md="4">
                            <b-form-select
                              :disabled="isUserReadOnly"
                              v-model="product.sizeCd"
                              @change="changeSize(index)"
                              :options="sizeNameOption"
                            ></b-form-select>
                          </b-col>
                          <b-col xl="3" lg="4" md="4">
                            <b-form-select
                              :disabled="isUserReadOnly"
                              v-model="product.colorCd"
                              @change="changeColor(index)"
                              :options="colorNameOption"
                            ></b-form-select>
                          </b-col>
                          <b-col xl="2" lg="4" md="4">
                            <b-form-input
                              :readonly="isUserReadOnly"
                              :name="'txtInventory_' + index"
                              type="text"
                              v-validate="'required'"
                              v-model="product.inventoryNumber"
                              maxlength="4"
                              v-on:keypress="isNumber()"
                              v-on:keydown="inventoryNumberError = ''"
                              v-money="money"
                              placeholder=""
                              autocomplete="off"
                            />
                          </b-col>
                          <b-col v-if="!isUserReadOnly" xl="3" lg="5" md="5">
                            <b-row>
                              <b-col>
                                <button
                                  @click="addProduct(index)"
                                  type="submit"
                                  class="btn btn-primary button-width"
                                >
                                  +
                                </button>
                              </b-col>
                              <b-col>
                                <button
                                  @click="delProduct(index)"
                                  type="submit"
                                  class="btn btn-danger button-width"
                                >
                                  -
                                </button>
                              </b-col>
                            </b-row>
                          </b-col>
                        </b-row>
                      </div>
                      <span
                        class="invalid-feedback d-block"
                        v-if="inventoryNumberError != ''"
                        >{{ this.inventoryNumberError }}</span
                      >
                      <div>
                        <span
                          class="invalid-feedback d-block"
                          v-if="sizeOrColorError != ''"
                          >{{ this.sizeOrColorError }}</span
                        >
                      </div>
                    </div>
                  </b-col>
                </b-row>
                <!--- MaxSizeCanOrder -->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>1サイズの予約可能点数</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col xl="6" lg="5" md="12">
                    <b-form-input
                      :readonly="isUserReadOnly"
                      name="txtMaxSizeCanOrder"
                      type="text"
                      style="max-width: 120px;"
                      v-validate="'required'"
                      v-model="currentProduct.maxSizeCanOrder"
                      maxlength="4"
                      v-on:keypress="isNumber()"
                      v-on:keydown="checkWrongMaxSizeCanOrder = false"
                      v-money="money"
                      placeholder=""
                      autocomplete="off"
                    />
                    <span class="invalid-feedback d-block">{{
                      errors.first("txtMaxSizeCanOrder")
                    }}</span>
                    <span
                      v-if="checkWrongMaxSizeCanOrder"
                      class="invalid-feedback d-block"
                      >{{ msgErrWrongQuantity }}</span
                    >
                  </b-col>
                </b-row>
                <!--- MaxAmountCanOrder -->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>1品番の予約可能金額</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col xl="6" lg="5" md="12">
                    <b-form-input
                      :readonly="isUserReadOnly"
                      name="txtMaxAmountCanOrder"
                      type="text"
                      style="max-width: 120px;"
                      v-validate="'required'"
                      v-model="currentProduct.maxAmountCanOrder"
                      maxlength="10"
                      v-on:keypress="isNumber()"
                      v-on:keydown="
                        checkPrice = checkWrongMaxAmountCanOrder = false
                      "
                      v-money="money"
                      placeholder=""
                      autocomplete="off"
                    />
                    <span class="invalid-feedback d-block">{{
                      errors.first("txtMaxAmountCanOrder")
                    }}</span>
                    <span
                      v-if="checkWrongMaxAmountCanOrder"
                      class="invalid-feedback d-block"
                      >{{ msgErrWrongQuantity }}</span
                    >
                  </b-col>
                </b-row>
                <!--- Show/Hide Product -->
                <b-row v-if="lstProduct.length > 1 || (isModeInsert && lstProduct.length === 1)">
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong
                        >{{ ITEM_SHOW_TEXT }}/{{ ITEM_HIDDEN_TEXT }}</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col xl="6" lg="7" md="12">
                    <v-switch
                      :readonly="isUserReadOnly"
                      style="margin: unset"
                      v-model="currentProduct.displayFlg"
                      @change="changeDisplayFlg(currentProduct)"
                      :label="
                        currentProduct.displayFlg === true
                          ? ITEM_SHOW_TEXT
                          : ITEM_HIDDEN_TEXT
                      "
                    ></v-switch>
                    <span
                      v-if="errChangeDisplayFlg"
                      class="invalid-feedback d-block"
                      >{{ msgErrShowItemProduct }}</span
                    >
                  </b-col>
                </b-row>
                <!--- Main Image -->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>メイン画像</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col v-if="imgMain != null" xl="3" lg="5" md="12">
                    <img width="150px" :src="imgMain" />
                  </b-col>
                  <b-col xl="2" lg="5" md="12">
                    <button
                      v-if="!isUserReadOnly"
                      @click="openImgMain"
                      class="mb-4 btn btn-success"
                    >
                      追加
                    </button>
                  </b-col>
                  <b-col
                    v-if="imgMain == null && isCheckValidate"
                    xl="3"
                    lg="5"
                    md="12"
                  >
                    <span class="invalid-feedback d-block">{{
                      this.msgRequired
                    }}</span>
                  </b-col>
                </b-row>
                <br />
                <!--- detail Image -->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>商品詳細画像</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col v-if="imgDetail != null" xl="3" lg="5" md="12">
                    <img width="150px" :src="imgDetail" />
                  </b-col>
                  <b-col xl="2" lg="5" md="12">
                    <button
                      @click="openImgDetail"
                      v-if="!isUserReadOnly"
                      class="mb-4 btn btn-success"
                    >
                      追加
                    </button>
                  </b-col>
                  <b-col
                    v-if="imgDetail == null && isCheckValidate"
                    xl="3"
                    lg="5"
                    md="12"
                  >
                    <span class="invalid-feedback d-block">{{
                      this.msgRequired
                    }}</span>
                  </b-col>
                </b-row>
                <br />
                <!---Thumbnail Image -->
                <b-row>
                  <b-col lg="3" md="12" xl="3">
                    <label class="col-md-12"
                      ><strong>サムネイル画像</strong
                      ><span class="text-danger">*</span></label
                    >
                  </b-col>
                  <b-col
                    v-if="imgThumbnail.length < 4 && imgThumbnail.length > 0"
                    xl="6"
                    lg="5"
                    md="12"
                  >
                    <img
                      width="100px"
                      style="margin-right: 30px"
                      v-for="image in this.imgThumbnail"
                      :src="image.imageUrl"
                      :key="image.productImgUrlId"
                    />
                  </b-col>
                  <b-col v-if="imgThumbnail.length > 3" xl="6" lg="5" md="12">
                    <div class="group-image" style="margin: unset">
                      <div hidden class="Large-image">
                        <div class="img-active">
                          <VueSlickCarousel
                            v-if="imgThumbnail.length > 0"
                            ref="c1"
                            :asNavFor="$refs.c2"
                          >
                            <img
                              v-for="image in this.imgThumbnail"
                              :src="image.imageUrl"
                              :key="image.productImgUrlId"
                            />
                          </VueSlickCarousel>
                        </div>
                      </div>
                      <div class="Thumbnail-image">
                        <div class="thumbnail-img-group">
                          <VueSlickCarousel
                            v-if="imgThumbnail.length > 0"
                            ref="c2"
                            :asNavFor="$refs.c1"
                            :slidesToShow="3"
                            :focusOnSelect="true"
                          >
                            <img
                              v-for="image in this.imgThumbnail"
                              :src="image.imageUrl"
                              :key="image.no"
                            />
                          </VueSlickCarousel>
                        </div>
                      </div>
                    </div>
                  </b-col>
                  <b-col xl="2" lg="5" md="12">
                    <button
                      @click="openImgThumbnail"
                      v-if="!isUserReadOnly"
                      class="mb-4 btn btn-success"
                    >
                      追加
                    </button>
                  </b-col>
                  <b-col
                    v-if="imgThumbnail.length == 0 && isCheckValidate"
                    xl="3"
                    lg="5"
                    md="12"
                  >
                    <span class="invalid-feedback d-block">{{
                      this.msgRequired
                    }}</span>
                  </b-col>
                </b-row>
                <br />
                <!-- bottm button-->
                <div>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <button
                      class="btn btn-primary"
                      @click="dialogProduct = false"
                    >
                      キャンセル
                    </button>
                    <v-spacer></v-spacer>
                    <button
                      v-if="!isUserReadOnly"
                      @click="showDialogInsertUpdate()"
                      style="width: 90px"
                      class="btn btn-danger"
                    >
                      保存
                    </button>
                    <v-spacer></v-spacer>
                  </v-card-actions>
                </div>
              </div>
            </div>
          </v-card-text>
        </v-card>
      </v-dialog>
      <!-- Add, update, detail dialog end -->

      <!--  dialog insert brand start -->
      <v-dialog
        v-model="dialogInsertBrand"
        max-width="600px"
        content-class="v-dialog-border"
      >
        <VueElementLoading
          :active="loadingPopupBrand"
          spinner="ring"
          color="var(--success)"
        />
        <v-card>
          <v-card-title>
            <span class="headline">ブランド追加</span>
          </v-card-title>
          <v-card-text>
            <!--- Brand code-->
            <b-row>
              <b-col lg="3" md="12" xl="4">
                <label class="col-md-12"
                  ><strong>ブランドコード</strong
                  ><span class="text-danger">*</span></label
                >
              </b-col>
              <b-col xl="6" lg="5" md="12">
                <b-form-input
                  name="txtBrand"
                  type="text"
                  v-validate="'required'"
                  v-model="currentProduct.brandCd"
                  maxlength="1"
                  style="max-width: 108px;"
                  @input="checkBrandCd = chkBrandSameCode = false"
                  placeholder=""
                  autocomplete="off"
                />
                <span v-if="checkBrandCd" class="invalid-feedback d-block">{{
                  this.msgRequired
                }}</span>
                <span
                  v-if="chkBrandSameCode"
                  class="invalid-feedback d-block"
                  >{{ this.msgSameBrandCd }}</span
                >
              </b-col>
            </b-row>
            <!--- Brand name-->
            <b-row>
              <b-col lg="3" md="12" xl="4">
                <label class="col-md-12"
                  ><strong>ブランド名</strong
                  ><span class="text-danger">*</span></label
                >
              </b-col>
              <b-col xl="5" lg="5" md="12">
                <b-form-input
                  name="txtBrandName"
                  type="text"
                  v-validate="'required'"
                  v-model="currentProduct.brandName"
                  style="width: 300px;"
                  placeholder=""
                  @input="checkBrandName = chkBrandSameName = false"
                  maxlength="30"
                  autocomplete="off"
                />
                <span v-if="checkBrandName" class="invalid-feedback d-block">{{
                  this.msgRequired
                }}</span>
                <span
                  v-if="chkBrandSameName"
                  class="invalid-feedback d-block"
                  >{{ this.msgSameBrandName }}</span
                >
              </b-col>
            </b-row>
            <!--- Sub brand-->
            <b-row>
              <b-col lg="3" md="12" xl="4">
                <label class="col-md-12"><strong>サブブランド</strong>
                <span class="text-danger">*</span></label>
              </b-col>
              <b-col xl="6" lg="6" md="12">
                <b-form-input
                  name="txtSubBrand"
                  type="text"
                  v-model="currentProduct.subBrand"
                  style="max-width: 108px;"
                  placeholder=""
                  @input="checkSubBrand = false"
                  maxlength="30"
                  autocomplete="off"
                />
                <span v-if="checkSubBrand" class="invalid-feedback d-block">{{
                  this.msgRequired
                }}</span>
              </b-col>
            </b-row>
          </v-card-text>
          <v-card-actions style="text-align: center; display: block">
            <v-btn class="btn_confirm_no" @click="dialogInsertBrand = false"
              >キャンセル</v-btn
            >
            <v-btn
              class="btn_confirm_yes"
              @click="showDialogConfirmInsertBrand()"
              >保存</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-dialog>
      <!--  dialog insert brand end -->

      <!--  dialog insert brand sub start -->
      <v-dialog
        v-model="dialogInsertBrandSub"
        max-width="600px"
        content-class="v-dialog-border"
      >
        <VueElementLoading
          :active="loadingPopupBrandSub"
          spinner="ring"
          color="var(--success)"
        />
        <v-card>
          <v-card-title>
            <span class="headline">サブブランド追加</span>
          </v-card-title>
          <v-card-text>
            <!--- Brand code-->
            <b-row>
              <b-col lg="3" md="12" xl="4">
                <label class="col-md-12"
                  ><strong>ブランドコード</strong
                  ><span class="text-danger">*</span></label
                >
              </b-col>
              <b-col xl="3" lg="5" md="12">
                <b-form-input readonly v-model="selectedBrandCd" />
              </b-col>
            </b-row>
            <!--- Brand name-->
            <b-row>
              <b-col lg="3" md="12" xl="4">
                <label class="col-md-12"
                  ><strong>ブランド名</strong
                  ><span class="text-danger">*</span></label
                >
              </b-col>
              <b-col xl="3" lg="5" md="12">
                <b-form-input v-model="selectedBrandName" readonly />
              </b-col>
            </b-row>
            <!--- Sub brand-->
            <b-row>
              <b-col lg="3" md="12" xl="4">
                <label class="col-md-12"><strong>サブブランド</strong>
                <span class="text-danger">*</span></label>
              </b-col>
              <b-col xl="6" lg="5" md="12">
                <b-form-input
                  name="txtSubBrand"
                  type="text"
                  style="max-width: 110px;"
                  v-model="currentProduct.subBrand"
                  @input="checkSubBrand = chkBrandSameBrandSub = false"
                  placeholder=""
                  maxlength="30"
                  autocomplete="off"
                />
                <span v-if="checkSubBrand" class="invalid-feedback d-block">{{
                  this.msgRequired
                }}</span>
                <span
                  v-if="chkBrandSameBrandSub"
                  class="invalid-feedback d-block"
                  >{{ msgSameBrandSub }}</span
                >
              </b-col>
            </b-row>
          </v-card-text>
          <v-card-actions style="text-align: center; display: block">
            <v-btn class="btn_confirm_no" @click="dialogInsertBrandSub = false"
              >キャンセル</v-btn
            >
            <v-btn
              class="btn_confirm_yes"
              @click="showDialogConfirmInsertBrandSub()"
              >保存</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-dialog>
      <!--  dialog insert brand end -->

      <!--  dialog confirm insert brand start -->
      <v-dialog
        v-model="dialogConfirmInsertBrand"
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
              @click="dialogConfirmInsertBrand = false"
              >キャンセル</v-btn
            >
            <v-btn class="btn_confirm_yes" @click="InsertBrand()">保存</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <!--  dialog confirm insert brand end -->

      <!--  dialog confirm insert brand sub start -->
      <v-dialog
        v-model="dialogConfirmInsertBrandSub"
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
              @click="dialogConfirmInsertBrandSub = false"
              >キャンセル</v-btn
            >
            <v-btn class="btn_confirm_yes" @click="InsertBrandSub()"
              >保存</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-dialog>
      <!--  dialog confirm insert brand sub end -->

      <!--  dialog confirm insert product start -->
      <v-dialog
        v-model="dialogConfirmProduct"
        max-width="401px"
        content-class="v-dialog-border"
      >
        <v-card>
          <v-card-text>
            <div v-if="isModeInsert" class="text-center">
              登録します。よろしいですか。
            </div>
            <div v-else class="text-center">変更します。よろしいですか。</div>
          </v-card-text>
          <v-card-actions style="text-align: center; display: block">
            <v-btn class="btn_confirm_no" @click="dialogConfirmProduct = false"
              >キャンセル</v-btn
            >
            <v-btn class="btn_confirm_yes" @click="insertUpdateProduct()"
              >保存</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-dialog>
      <!--  dialog confirm insert product end -->

      <!--  dialog Quantity start -->
      <v-dialog v-model="dialogQuantity" max-width="600px">
        <v-card>
          <v-card-title>
            <span class="headline">在庫数表示</span>
          </v-card-title>
          <v-card-text>
            <!---  quantity -->
            <b-row>
              <b-col lg="2" md="12" xl="2">
                <label class="col-md-12"><strong>○ </strong></label>
              </b-col>
              <b-col lg="2" md="12" xl="2">
                <label class="col-md-12"><strong>≧</strong></label>
              </b-col>
              <b-col xl="3" lg="6" md="12">
                <b-form-group>
                  <b-form-input
                    type="text"
                    name="txtInventoryNumber"
                    id="txtInventoryNumber"
                    v-validate="'required'"
                    v-model="inventoryNumberUpd"
                    v-on:keypress="isNumber()"
                    maxlength="10"
                    autocomplete="off"
                  />
                  <span
                    class="invalid-feedback d-block"
                    v-if="errors.has('txtInventoryNumber') == true"
                    >{{ errors.first("txtInventoryNumber") }}</span
                  >
                </b-form-group>
              </b-col>
            </b-row>
            <b-row>
              <b-col lg="2" md="12" xl="2">
                <label class="col-md-12"><strong>△ </strong></label>
              </b-col>
              <b-col lg="2" md="12" xl="2">
                <label class="col-md-12"><strong>＜</strong></label>
              </b-col>
              <b-col xl="3" lg="6" md="12">
                <label class="col-md-12">{{ inventoryNumberUpd }}</label>
              </b-col>
            </b-row>
            <b-row>
              <b-col lg="2" md="12" xl="2">
                <label class="col-md-12"><strong>× </strong></label>
              </b-col>
              <b-col lg="2" md="12" xl="2">
                <label class="col-md-12"><strong>=</strong></label>
              </b-col>
              <b-col xl="3" lg="6" md="12">
                <label class="col-md-12">0</label>
              </b-col>
            </b-row>
          </v-card-text>
          <v-card-actions style="text-align: center; display: block">
            <div>
              <v-card-actions>
                <v-spacer></v-spacer>
                <button class="btn btn-primary" @click="dialogQuantity = false">
                  キャンセル
                </button>
                <v-spacer></v-spacer>
                <button
                  @click="showdialogConfirmQuantity()"
                  style="width: 90px"
                  class="btn btn-danger"
                >
                  保存
                </button>
                <v-spacer></v-spacer>
              </v-card-actions>
            </div>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <!--  dialog quantity end -->

      <!--  dialog confirm quantity start -->
      <v-dialog
        v-model="dialogConfirmQuantity"
        max-width="401px"
        content-class="v-dialog-border"
      >
        <v-card>
          <v-card-text>
            <div class="text-center">変更します。よろしいですか。</div>
          </v-card-text>
          <v-card-actions style="text-align: center; display: block">
            <v-btn class="btn_confirm_no" @click="dialogConfirmQuantity = false"
              >キャンセル</v-btn
            >
            <v-btn class="btn_confirm_yes" @click="updateInventoryNumber()"
              >保存</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-dialog>
      <!--  dialog confirm quantity end -->

      <!-- Dialog update img thumbnail start -->
      <v-dialog v-model="dialogImage" max-width="1000px">
        <v-card>
          <VueElementLoading
            :active="popupImageLoading"
            spinner="ring"
            color="var(--success)"
          />
          <div class="row">
            <div class="col" style="text-align: left">
              <button
                @click="deleteManyImage"
                style="margin:20px"
                class="btn btn-danger"
              >
                削除
              </button>
            </div>
            <div class="col" style="text-align: right">
              <button
                @click="openFileImage"
                style="margin:20px"
                class="btn btn-success"
              >
                追加
              </button>
            </div>
          </div>
          <v-data-table
            v-model="selected"
            :headers="headersImage"
            :items="imgThumbnailNew"
            item-key="no"
            hide-default-footer
            show-select
            class="elevation-1"
            :no-results-text="msgNoData"
            :no-data-text="msgNoData"
          >
            <template v-slot:item="{ item }">
              <tr>
                <td>
                  <v-checkbox
                    class="pa-0 ma-0"
                    :ripple="false"
                    v-model="selected"
                    :value="item"
                    hide-details
                  >
                  </v-checkbox>
                </td>
                <td class="text-center">
                  {{ item.no }}
                </td>
                <td class="text-center">
                  <img
                    width="150px"
                    style="margin: 10px"
                    :src="item.imageUrl"
                  />
                </td>
                <td class="text-center">
                  <strong>
                    <button
                      @click="openFileUpdate(item)"
                      class="btn btn-success mr-2"
                    >
                      編集
                    </button>
                    <button @click="deleteImage(item)" class="btn btn-danger">
                      削除
                    </button>
                  </strong>
                </td>
              </tr>
            </template>
          </v-data-table>
          <!-- bottm button-->
          <div>
            <v-card-actions>
              <v-spacer></v-spacer>
              <button class="btn btn-primary" @click="dialogImage = false">
                キャンセル
              </button>
              <v-spacer></v-spacer>
              <button
                @click="changeImage()"
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
      <!-- Dialog update img thumbnail end -->

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
                style="margin:20px"
                class="btn btn-danger"
              >
                CSVファイル選択
              </button>
            </div>
          </div>
          <span class="text-danger ml-5"
            >※選択している業態のデータを読み込むので、注意してください。</span
          >
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
                  :length="pageCountCsv"
                ></v-pagination>
              </div>
            </div>
            <div v-else style="height: 42px"></div>
          </div>
          <v-data-table
            class="table-csv"
            v-model="selectedCsv"
            :headers="headersCsv"
            :items="dataCsv"
            :page.sync="pageCsv"
            :items-per-page="itemsPerPage"
            @page-count="pageCountCsv = $event"
            item-key="sku"
            hide-default-footer
            show-select
            no-results-text=""
            no-data-text=""
            @toggle-select-all="selectAllCsv"
            @update:options="changeSortCsv"
          >
            <template v-slot:item="{ item }">
              <tr>
                <td class="text-center" v-if="item.txtError === null">
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
                    @click="ShowError(item.txtError)"
                    class="btn btn-danger"
                  >
                    エラー
                  </button>
                </td>
                <td class="text-center">
                  {{ item.productCd }}
                </td>
                <td class="text-center">
                  {{ item.sizeName }}
                </td>
                <td class="text-center">
                  {{ item.colorName }}
                </td>
                <td class="text-center">
                  {{ item.txtYear }}
                </td>
                <td class="text-center">
                  {{ item.productName }}
                </td>
                <td class="text-center">
                  {{ item.productName1 }}
                </td>
                <td class="text-center">
                  {{ item.brandName }}
                </td>
                <td class="text-center">
                  {{ item.subBrand }}
                </td>
                <td class="text-center">
                  {{ item.inventoryNumber }}
                </td>
                <td class="text-center">
                  {{ item.txtPrice }}
                </td>
                <td class="text-center">
                  {{ item.txtDisplay }}
                </td>
              </tr>
            </template>
          </v-data-table>
          <!-- bottm button-->
          <div style="margin-top: 10px;">
            <v-card-actions>
              <v-spacer></v-spacer>
              <button class="btn btn-primary" @click="dialogCsv = false">
                キャンセル
              </button>
              <v-spacer></v-spacer>
              <button
                @click="showDialogConfirmInsertCsv()"
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

      <!--  dialog confirm insert Csv start -->
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
            <v-btn class="btn_confirm_yes" @click="insertCsv()">保存</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <!--  dialog confirm insert Csv end -->

      <!--  dialog show Message Error start -->
      <v-dialog v-model="dialogError" max-width="700px">
        <v-card>
          <v-card-title>
            <span class="headline">エラー一覧</span>
          </v-card-title>
          <v-card-text>
            <h6 class="memo_newLine text-danger">{{ currentMessError }}</h6>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <button class="btn btn-primary" @click="dialogError = false">
              キャンセル
            </button>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </div>
  </div>
</template>

<script>
export default {
  data: () => ({
    money: {},
    loadingPopup: false,
    pageCsv: 1,
    isChange: false,
    isChangePage: true,
    txtProductCode: "",
    txtProductName: "",
    cbbDisplay: -1,
    lstDisPlayFlg: [
      { text: "", value: -1 },
      { text: "非表示", value: 0 },
      { text: "表示", value: 1 },
    ],
    checkWrongMaxSizeCanOrder: false,
    checkWrongMaxAmountCanOrder: false,
    checkWrongPrice: false,
    lstYear: null,
    cbbBrandName: 0,
    cbbYear: "",
    page: 1,
    pageCount: 0,
    pageCountCsv: 0,
    itemsPerPage: 10,
    currentMessError: "",
    selected: [],
    selectedCsv: [],
    fileCsv: null,
    sizeOrColorError: "",
    inventoryNumberError: "",
    dialogImage: false,
    isCheckValidate: false,
    displayFlgProduct: true,
    brandNameOption: [],
    brandSubNameOption: [],
    brandNameOptionSearch: [],
    colorNameOption: [],
    sizeNameOption: [],
    lstUpdatedProduct: [],
    dialogConfirmQuantity: false,
    dialogQuantity: false,
    inventoryNumberUpd: 0,
    bussinessCd: null,
    dialogProduct: false,
    oldProduct: {},
    currentProduct: {},
    dialogInsertBrand: false,
    dialogInsertBrandSub: false,
    dialogConfirmInsertBrand: false,
    dialogConfirmInsertBrandSub: false,
    checkBrandName: false,
    checkBrandCd: false,
    checkSubBrand: false,
    dialogError: false,
    currentImgThumbnail: {},
    checkPrice: false,
    selectedBrandName: "",
    selectedBrandCd: "",
    chkBrandSameBrandSub: false,
    msgErrNotChooseSubBranch: "",
    msgErrNotChooseBranch: "",
    errChangeDisplayFlg: false,
    headersImage: [
      {
        text: "No",
        sortable: false,
        width: "10%",
        align: "center",
      },
      {
        text: "画像",
        sortable: false,
        width: "40%",
        align: "center",
      },
      {
        text: "操作",
        sortable: false,
        width: "40%",
        align: "center",
      },
    ],
    dataCsv: [],
    headersCsv: [
      {
        text: "品番",
        sortable: false,
        align: "center",
        value: "productCd",
      },
      {
        text: "サイズ名",
        sortable: false,
        align: "center",
        value: "sizeName",
      },
      {
        text: "色名",
        sortable: false,
        align: "center",
        value: "colorName",
      },
      {
        text: "年",
        sortable: false,
        align: "center",
        value: "txtYear",
      },
      {
        text: "商品名",
        sortable: false,
        align: "center",
        value: "productName",
      },
      {
        text: "商品名（略称）",
        sortable: false,
        align: "center",
        value: "productName1",
      },
      {
        text: "ブランド名",
        sortable: false,
        align: "center",
        value: "brandName",
      },
      {
        text: "サブブランド",
        sortable: false,
        align: "center",
        value: "subBrand",
      },
      {
        text: "在庫数",
        sortable: false,
        align: "center",
        value: "inventoryNumber",
      },
      {
        text: "価格（税抜き）",
        sortable: false,
        align: "center",
        value: "txtPrice",
      },
      {
        text: "表示ステータス",
        sortable: false,
        align: "center",
        value: "txtDisplay",
      },
    ],
    headers: [
      {
        text: "品番",
        sortable: true,
        width: "10%",
        value: "productCd",
        align: "left",
      },
      {
        text: "商品名",
        sortable: false,
        value: "productName",
        width: "20%",
        align: "left",
      },
      {
        text: "在庫数合計",
        sortable: true,
        width: "10%",
        align: "left",
        value: "totalInventoryNumber",
      },
      {
        text: "年",
        value: "year",
        sortable: true,
        width: "10%",
        align: "left",
      },
      {
        text: "ブランド名",
        sortable: true,
        width: "10%",
        align: "left",
        value: "brandName",
      },
      {
        text: "表示ステータス",
        sortable: true,
        width: "10%",
        align: "left",
        value: "displayFlg",
      },
    ],
    chkBrandSameCode: false,
    chkBrandSameName: false,
    dialogCsv: false,
    dataImageDelete: [],
    isModeInsert: true,
    imgMain: null,
    imgDetail: null,
    imgThumbnail: [],
    imgThumbnailNew: {},
    lstProductSameCode: [],
    lstOldProductSameCode: [],
    lstProduct: [],
    lstProductSearch: [],
    tblProductLoading: true,
    inventoryNumber: 0,
    loading: true,
    //seletedBrand: 0,
    //seletedBrandSub: 0,
    displayDialogProduct: false,
    dialogConfirmProduct: false,
    dataImage: [],
    popupImageLoading: false,
    lstProductDelete: [],
    dialogConfirmInsertCsv: false,
    currentUser: null,
    loadingPopupBrand: false,
    loadingPopupBrandSub: false,
  }),
  async created() {
    this.currentProduct.brandName = 1;
    this.currentUser = this.getUserData().userCd;
    this.bussinessCd = this.getUserData().bussinessCd;
    await this.getSizeOptions();
    await this.getColorOptions();
    await this.getAllMProducts();
    await this.getBrandOptions(null);
    await this.listInventoryNumber();
    this.loading = false;
  },
  methods: {
    //条件によるMProducts一覧を取得する
    async getAllMProducts() {
      this.loading = true;
      const config = {
        method: "get",
        url: "api/MProduct/GetListProductMaintain",
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      const res = await require("axios")(config);
      this.lstYear = [...new Set(res.data.map((a) => a.year))];
      this.lstYear.unshift("");
      if (res.data !== null) {
        if (res.data.error === undefined) {
          this.lstProduct = res.data;
          this.lstProductSearch = [...this.lstProduct];
          this.tblProductLoading = false;
        }
      } else {
        this.$toastr.e(res.data.error);
      }
    },
    //条件によるBrand一覧を取得する
    async getBrandOptions(brandCd) {
      this.brandNameOption = [];
      this.brandNameOptionSearch = [{ text: "", value: 0 }];
      const config = {
        method: "get",
        url: "api/MBrand/GetBrandWithBussinessCd",
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      const res = await require("axios")(config);

      if (res.data !== null && res.data.length > 0) {
        res.data.forEach((brand) => {
          this.brandNameOption.push({
            text: brand.brandName,
            value: brand.brandRltId,
            brandCd: brand.brandCd,
          });
          this.brandNameOptionSearch.push({
            text: brand.brandName,
            value: brand.brandRltId,
          });
        });
        if (brandCd == null) {
          brandCd = this.brandNameOption[0].value;
        }
        await this.changeBrand(brandCd);
      } else {
        this.loading = false;
        this.$toastr.e(res.data.error);
      }
    },
    async changeBrand(seletedBrand) {
      this.displayDialogProduct = true;
      //this.isChange = true;
      this.brandSubNameOption = [];
      try {
        const config = {
          method: "get",
          url: "api/MSubBrand/GetAllByBrandCd",
          params: {
            brandCd: this.brandNameOption.find((x) => x.value == seletedBrand).brandCd
          },
        };
        const res = await require("axios")(config);

        if (res.data !== null) {
          res.data.forEach((subBrand) => {
            this.brandSubNameOption.push({
              text: subBrand.subBrand != null ? subBrand.subBrand : '',
              value: subBrand.subBrandId,
            });
          });
          this.currentProduct.subBrandId = this.brandSubNameOption[0].value;
          this.msgErrNotChooseBranch = "";
          this.msgErrNotChooseSubBranch = "";
        } else {
          this.$toastr.e(res.data.error);
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.displayDialogProduct = false;
      }
    },
    //条件によるSize一覧を取得する
    async getSizeOptions() {
      const config = {
        method: "get",
        url: "api/MSize/GetAll",
      };
      const res = await require("axios")(config);

      if (res.data !== null) {
        res.data.forEach((size) => {
          this.sizeNameOption.push({
            text: size.sizeName,
            value: size.sizeCd,
          });
        });
      } else {
        this.$toastr.e(res.data.error);
      }
    },
    //条件によるMColor一覧を取得する
    async getColorOptions() {
      const config = {
        method: "get",
        url: "api/MColor/GetAll",
      };
      const res = await require("axios")(config);

      if (res.data !== null) {
        res.data.forEach((color) => {
          this.colorNameOption.push({
            text: color.colorName,
            value: color.colorCd,
          });
        });
      } else {
        this.$toastr.e(res.data.error);
      }
    },
    //条件によるInventorySymbol一覧を取得する
    async listInventoryNumber() {
      const config = {
        method: "get",
        url: "api/InventorySymbol/GetInventorySymbol",
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      try {
        const inventorySymbol = await require("axios")(config);
        if (inventorySymbol.data !== null && inventorySymbol.data.length > 0) {
          this.inventoryNumber = Number(
            inventorySymbol.data[0].inventoryNumCheck
          );
        }
      } catch (error) {
        console.log(error);
      }
    },
    //InventoryNumberを更新する
    async updateInventoryNumber() {
      this.dialogConfirmQuantity = false;
      this.dialogQuantity = false;
      this.loading = true;
      const config = {
        method: "post",
        url: "api/InventorySymbol/UpdateInventorySymbol",
        params: {
          inventoryNumber: this.inventoryNumberUpd,
          bussinessCd: this.bussinessCd,
          updateUserCd: this.currentUser,
        },
      };
      try {
        const res = await require("axios")(config);
        if (res.data !== false) {
          this.$toastr.s(this.msgUpdateSucces);
        } else {
          this.$toastr.e(this.msgUpdateFailed);
        }
      } catch (error) {
        this.$toastr.e(this.msgUpdateFailed);
      } finally {
        await this.listInventoryNumber();
        this.loading = false;
      }
    },
    //数量確認のダイアログを表示する
    showdialogConfirmQuantity() {
      const arrayItemErr = this.$validator.errors.items;
      if (arrayItemErr.length === 0) {
        this.inventoryNumber = this.inventoryNumber.toString();
        this.inventoryNumberUpd = this.inventoryNumberUpd.toString();
        if (this.inventoryNumberUpd === this.inventoryNumber) {
          this.$toastr.w(this.msgNotthingChanged);
          return;
        }
        this.dialogConfirmQuantity = true;
      }
    },
    //ブランド追加確認のダイアログを表示する
    async showDialogConfirmInsertBrand() {
      if (this.currentProduct.brandName == "") {
        this.checkBrandName = true;
      }
      if (this.currentProduct.brandCd == "") {
        this.checkBrandCd = true;
      }
      if (this.currentProduct.subBrand == "") {
        this.checkSubBrand = true;
      }
      if (this.currentProduct.brandCd == "" || this.currentProduct.brandCd == "" || this.currentProduct.subBrand == "") {
        return;
      }
      this.loadingPopupBrand = true;
      const lstBrand = await this.getAllBrand();
      if (lstBrand.map((x) => x.brandCd.toLowerCase()).includes(this.currentProduct.brandCd.toLowerCase())) {
        this.chkBrandSameCode = true;
      }
      if (lstBrand.map((x) => x.brandName.toLowerCase()).includes(this.currentProduct.brandName.toLowerCase())) {
        this.chkBrandSameName = true;
      }
      if (this.chkBrandSameCode || this.chkBrandSameName) {
        this.loadingPopupBrand = false;
        return;
      }
      if (!this.checkBrandCd && !this.checkBrandName) {
        this.dialogConfirmInsertBrand = true;
      }
      this.loadingPopupBrand = false;
    },
    async showDialogConfirmInsertBrandSub() {
      if (this.currentProduct.subBrand == "") {
        this.checkSubBrand = true;
        return;
      }
      if (
        this.brandSubNameOption.find(
          (x) => x.text.toLowerCase() == this.currentProduct.subBrand.toLowerCase()
        ) != undefined
      ) {
        this.chkBrandSameBrandSub = true;
        return;
      }

      this.dialogConfirmInsertBrandSub = true;
    },
    //数量通知ダイアログを表示する
    async showDialogQuantity() {
      this.loading = true;
      await this.listInventoryNumber();
      this.inventoryNumberUpd = this.inventoryNumber;
      this.dialogQuantity = true;
      this.loading = false;
    },
    //商品状態を表示する
    formatProductStatus(ProductStatus) {
      return ProductStatus !== this.PRODUCT_SHOW
        ? this.ITEM_HIDDEN_TEXT
        : this.ITEM_SHOW_TEXT;
    },
    //ブランド追加のダイアログを表示する
    showDialogInsertBrand() {
      this.dialogInsertBrand = true;
      this.currentProduct.brandName = "";
      this.checkBrandCd = false;
      this.checkBrandName = false;
      this.chkBrandSameName = false;
      this.chkBrandSameCode = false;
      this.checkSubBrand = false;
      this.currentProduct.brandCd = "";
      this.currentProduct.subBrand = "";
    },
    showDialogInsertBrandSub() {
      this.currentProduct.subBrand = "";
      this.chkBrandSameBrandSub = false;
      this.dialogInsertBrandSub = true;
      this.checkSubBrand = false;
      this.selectedBrandCd = this.brandNameOption.find(
        (x) => x.value == this.currentProduct.brandRltId
      ).brandCd;
      this.selectedBrandName = this.brandNameOption.find(
        (x) => x.value == this.currentProduct.brandRltId
      ).text;
    },
    //Brandを追加する
    async InsertBrand() {
      this.dialogConfirmInsertBrand = false;
      this.dialogInsertBrand = false;
      let dataBrand = {
        brandName: this.currentProduct.brandName,
        brandCd: this.currentProduct.brandCd,
        subBrand: this.currentProduct.subBrand,
        createUserCd: this.currentUser,
      };
      this.displayDialogProduct = true;
      const config = {
        method: "post",
        url: "api/MBrand/AddAsync",
        data: dataBrand,
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      try {
        const resIns = await require("axios")(config);
        if (resIns.data !== this.INSERT_BRAND_FAIL) {
          this.currentProduct.brandRltId = resIns.data;
          await this.getBrandOptions(resIns.data);
          this.msgErrNotChooseBranch = "";
          this.msgErrNotChooseSubBranch = "";
          this.$toastr.s(this.msgInsertSucces);
        } else {
          this.$toastr.e(this.msgInsertFailed);
        }
      } catch (error) {
        console.log(error);
      }
    },
    //サブブランド追加
    async InsertBrandSub() {
      this.dialogConfirmInsertBrandSub = false;
      this.dialogInsertBrandSub = false;
      let dataBrandSub = {
        brandCd: this.selectedBrandCd,
        subBrand: this.currentProduct.subBrand,
        createUserCd: this.currentUser,
      };
      this.displayDialogProduct = true;
      const config = {
        method: "post",
        url: "api/MBrand/AddBrandSubAsync",
        data: dataBrandSub,
      };
      try {
        const resIns = await require("axios")(config);
        if (resIns.data !== this.INSERT_BRAND_FAIL) {
          await this.changeBrand(this.currentProduct.brandRltId);
          this.currentProduct.subBrandId = this.brandSubNameOption.find(
            (x) => x.text == this.currentProduct.subBrand
          ).value;
          this.msgErrNotChooseSubBranch = "";
          this.$toastr.s(this.msgInsertSucces);
        } else {
          this.$toastr.e(this.msgInsertFailed);
        }
      } catch (error) {
        console.log(error);
      }
    },
    //条件によるBrand一覧を取得する
    async getAllBrand() {
      const config = {
        method: "get",
        url: "api/MBrand/GetAll",
      };
      try {
        const lstBrand = await require("axios")(config);
        if (lstBrand.data !== null) {
          return lstBrand.data;
        }
      } catch (error) {
        console.log(error);
      }
    },
    //条件によるProductDetail一覧を取得する
    async getProductDetail(productCd) {
      const config = {
        method: "get",
        url: "api/MProductDetail/GetAll",
        params: {
          productCd: productCd,
        },
      };
      try {
        const lstProductDetail = await require("axios")(config);
        if (lstProductDetail.data !== null) {
          this.lstProductSameCode = lstProductDetail.data;
          this.lstOldProductSameCode = JSON.parse(JSON.stringify(this.lstProductSameCode))

          for (var i in this.lstOldProductSameCode) {
            if(this.lstOldProductSameCode[i].inventoryNumber && this.lstOldProductSameCode[i].inventoryNumber.toString().length > 3)
            {
              this.lstOldProductSameCode[i].inventoryNumber = this.lstOldProductSameCode[i].inventoryNumber.toLocaleString();
            }
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    //条件によるProductImgUrl一覧を取得する
    async getProductImgUrl(productCd) {
      const config = {
        method: "get",
        url: "api/MProductImgUrl/GetAll",
        params: {
          productCd: productCd,
        },
      };
      try {
        const lstProductImgUrl = await require("axios")(config);
        if (lstProductImgUrl.data.length !== 0) {
          this.dataImage = lstProductImgUrl.data;
          this.imgMain =
            this.PRODUCT_LIST_PATH +
            this.dataImage.find(
              (x) => x.isMainInListPage && !x.isMainInDetailPage
            ).imageUrl;
          this.imgDetail =
            this.PRODUCT_DETAIL_PATH +
            this.dataImage.find(
              (x) => !x.isMainInListPage && x.isMainInDetailPage
            ).imageUrl;
          this.imgThumbnail = JSON.parse(
            JSON.stringify(
              this.dataImage.filter(
                (x) => !x.isMainInListPage && !x.isMainInDetailPage
              )
            )
          );
          this.imgThumbnail.map(
            (x) => (x.imageUrl = this.PRODUCT_DETAIL_PATH + x.imageUrl)
          );
        }
      } catch (error) {
        console.log(error);
      }
    },
    //商品更新のダイアログを表示する
    async showDialogUpdateProduct(dataProduct) {
      this.errChangeDisplayFlg = false;
      this.sizeOrColorError = "";
      this.inventoryNumberError = "";
      this.msgErrNotChooseBranch = "";
      this.msgErrNotChooseSubBranch = "";
      this.loading = true;
      this.isCheckValidate = false;
      this.checkWrongMaxSizeCanOrder = false;
      this.checkWrongMaxAmountCanOrder = false;
      this.checkWrongPrice = false;
      this.imgMain = null;
      this.imgDetail = null;
      this.imgThumbnail = [];
      this.dataImageDelete = [];
      this.checkPrice = false;
      this.dataImage = [];
      this.lstProductDelete = [];
      this.oldProduct = { ...dataProduct };
      this.currentProduct = { ...dataProduct };
      this.lstProductSameCode = [{}];
      await this.getProductDetail(dataProduct.productCd);
      await this.getProductImgUrl(dataProduct.productCd);
      this.isModeInsert = false;
      //this.seletedBrand = this.currentProduct.brandRltId;
      await this.changeBrand(this.currentProduct.brandRltId);
      //this.seletedBrandSub = this.currentProduct.subBrandId;
      this.currentProduct.displayFlg = !this.currentProduct.displayFlg;
      this.oldProduct.displayFlg = !this.oldProduct.displayFlg;
      if(this.oldProduct.price && this.oldProduct.price.toString().length > 3)
      {
        this.oldProduct.price = this.oldProduct.price.toLocaleString();
      }

      if(this.oldProduct.maxAmountCanOrder && this.oldProduct.maxAmountCanOrder.toString().length > 3)
      {
        this.oldProduct.maxAmountCanOrder = this.oldProduct.maxAmountCanOrder.toLocaleString();
      }

      if(this.oldProduct.maxSizeCanOrder && this.oldProduct.maxSizeCanOrder.toString().length > 3)
      {
        this.oldProduct.maxSizeCanOrder = this.oldProduct.maxSizeCanOrder.toLocaleString();
      }
      this.dialogProduct = true;
      this.loading = false;
      this.isChange = false;
    },
    //商品新規追加のダイアログを表示する
    async showDialoginsertProduct() {
      this.errChangeDisplayFlg = false;
      this.sizeOrColorError = "";
      this.inventoryNumberError = "";
      this.msgErrNotChooseBranch = "";
      this.msgErrNotChooseSubBranch = "";
      this.loading = true;
      this.isCheckValidate = false;
      this.isModeInsert = true;
      this.checkPrice = false;
      this.displayDialogProduct = true;
      this.checkWrongMaxSizeCanOrder = false;
      this.checkWrongMaxAmountCanOrder = false;
      this.checkWrongPrice = false;
      this.lstProductSameCode = [{}];
      if(this.sizeNameOption.length > 0) this.lstProductSameCode[0].sizeCd = this.sizeNameOption[0].value;
      if(this.colorNameOption.length > 0) this.lstProductSameCode[0].colorCd = this.colorNameOption[0].value;
      this.currentProduct = {};
      if(this.brandNameOption.length > 0) this.currentProduct.brandRltId = this.brandNameOption[0].value;
      if(this.brandNameOption.length > 0) await this.changeBrand(this.currentProduct.brandRltId);
      if (this.brandSubNameOption.length > 0) {
        this.currentProduct.subBrandId = this.brandSubNameOption[0].value;
      }
      this.imgMain = null;
      this.imgDetail = null;
      this.imgThumbnail = [];
      this.dataImage = [];
      this.currentProduct.displayFlg = true;
      this.dialogProduct = true;
      this.displayDialogProduct = false;
      this.$validator.reset();
      this.loading = false;
    },
    setDataUpdateProduct() {
      this.displayDialogProduct = true;
      this.dialogConfirmProduct = false;
      this.currentProduct.displayFlg = !this.currentProduct.displayFlg;
      this.oldProduct.displayFlg = !this.oldProduct.displayFlg;
      //this.currentProduct.brandRltId = this.seletedBrand;
      //this.currentProduct.subBrandId = this.seletedBrandSub;
      if (this.currentProduct.price.length > 3) {
        this.currentProduct.price = this.currentProduct.price.replace(",", "");
      }
      if (this.currentProduct.maxAmountCanOrder.length > 3) {
        this.currentProduct.maxAmountCanOrder = this.currentProduct.maxAmountCanOrder.replace(
          /,/g,
          ""
        );
      }
      if (this.currentProduct.maxSizeCanOrder.length > 3) {
        this.currentProduct.maxSizeCanOrder = this.currentProduct.maxSizeCanOrder.replace(
            /,/g,
          ""
          );
      }

      for (var i = 0; i < this.lstProductSameCode.length; i++) {
        this.lstProductSameCode[i].productCd = this.currentProduct.productCd;
        this.lstProductSameCode[i].sku =
          this.lstProductSameCode[i].productCd +
          this.lstProductSameCode[i].colorCd +
          this.lstProductSameCode[i].sizeCd;
        if (this.lstProductSameCode[i].inventoryNumber.length > 3) {
          this.lstProductSameCode[i].inventoryNumber = this.lstProductSameCode[
            i
          ].inventoryNumber.replace(/,/g, "");
        }
      }

      let dataProductSub = {
        dataProduct: this.currentProduct,
        lstMProductDetail: this.lstProductSameCode,
        lstProductDelete: this.lstProductDelete,
      };
      if (this.isModeInsert) {
        dataProductSub.dataProduct.createUserCd = this.currentUser;
        this.dataImage.map((x) => (x.productCd = this.currentProduct.productCd));
        this.dataImage.map(
          (x) =>
            (x.imageUrl =
              this.brandNameOption
                .find((x) => x.value == this.currentProduct.brandRltId)
                .text.toLowerCase() +
              "/" +
              x.productCd +
              "/" +
              x.imageUrl)
        );
      } else {
        dataProductSub.dataProduct.updateUserCd = this.currentUser;
      }
      return dataProductSub;
    },
    // Productを追加する,Productを更新する
    async insertUpdateProduct() {
      try {
        const formData = new FormData();
        formData.append(
          "dataProductSub",
          JSON.stringify(this.setDataUpdateProduct())
        );
        formData.append("bussinessCd", JSON.stringify(this.bussinessCd));
        for (let i = 0; i < this.dataImage.length; i++) {
          this.dataImage[i].imageUrl = this.dataImage[i].imageUrl.replace(
            this.PRODUCT_DETAIL_PATH,
            ""
          );
          formData.append("file[" + i + "]", this.dataImage[i].fileImg);
        }
        formData.append("lstImage", JSON.stringify(this.dataImage));
        formData.append(
          "dataImageDelete",
          JSON.stringify(this.dataImageDelete)
        );

        const axios = require("axios");
        let apiUrl = "AddAsyncSub";
        if (!this.isModeInsert) {
          apiUrl = "UpdateAsyncSub";
        }
        const resUpdate = await axios.post("api/MProduct/" + apiUrl, formData, {
          headers: {
            "Content-type": "multipart/form-data",
          },
        });
        if (this.isModeInsert) {
          if (resUpdate.data != 0) {
            this.displayDialogProduct = false;
            this.dialogProduct = false;
            await this.getAllMProducts();
            this.search();
            this.$toastr.s(this.msgInsertSucces);
            this.loading = false;
          } else {
            this.$toastr.e(this.msgInsertFailed);
          }
        } else {
          if (resUpdate.data.error != undefined) {
            this.loading = false;
            this.dialogProduct = false;
            this.$toastr.e(resUpdate.data.error);
            return;
          }
          if (resUpdate.data) {
            this.displayDialogProduct = false;
            this.dialogProduct = false;
            await this.getAllMProducts();
            this.search();
            this.$toastr.s(this.msgUpdateSucces);
            this.loading = false;
          } else {
            this.displayDialogProduct = false;
            this.dialogProduct = false;
            this.$toastr.e(this.msgUpdateFailed);
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    //商品を追加する
    addProduct(index) {
      let data = { ...this.lstProductSameCode[index] };
      this.lstProductSameCode.splice(index, 0, data);
      this.lstProductSameCode[index + 1].newProduct = true;
    },
    //商品を削除する
    delProduct(index) {
      //this.isChange = true;
      if (this.lstProductSameCode.length <= 1) {
        return;
      }
      if (this.lstProductSameCode[index].newProduct != true) {
        this.lstProductDelete.push({ ...this.lstProductSameCode[index] });
      }
      this.lstProductSameCode.splice(index, 1);
    },
    //商品追加・更新のダイアログを表示する。
    async showDialogInsertUpdate() {
      this.msgErrNotChooseBranch = "";
      this.msgErrNotChooseSubBranch = "";
      if (this.errChangeDisplayFlg) return; 
      if(this.currentProduct.brandRltId == undefined)
      {
        this.msgErrNotChooseBranch = this.msgRequired;
        return;
      }

      if(this.brandSubNameOption.length == 0 || this.currentProduct.subBrandId == undefined)
      {
        this.msgErrNotChooseSubBranch = this.msgRequired;
        return;
      }

      if (!this.isModeInsert && this.isChange === false
           && JSON.stringify(this.oldProduct) === JSON.stringify(this.currentProduct)
           && JSON.stringify(this.lstProductSameCode) === JSON.stringify(this.lstOldProductSameCode)
           ) 
      {
        this.$toastr.w(this.msgNotthingChanged);
        return;
      }

      this.isCheckValidate = true;
      const hasError = this.$validator.validateAll();
      if (
        this.lstProductSameCode.filter((x) => x.inventoryNumber == 0).length >
          0 &&
        this.isModeInsert
      ) {
        this.inventoryNumberError = this.msgErrWrongInventory;
      }
      if (this.currentProduct.maxAmountCanOrder == 0) {
        this.checkWrongMaxAmountCanOrder = true;
      }
      if (this.currentProduct.maxSizeCanOrder == 0) {
        this.checkWrongMaxSizeCanOrder = true;
      }
      if (this.currentProduct.price == 0) {
        this.checkWrongPrice = true;
      }
      if (
        this.currentProduct.year != undefined &&
        this.currentProduct.year.length > 0 &&
        this.currentProduct.year.length != 4
      ) {
        return;
      }
      if (
        this.imgThumbnail.length == 0 ||
        this.imgMain.length == 0 ||
        this.imgDetail.length == 0
      ) {
        return;
      }

      let chkOrdDtlSame = 0;
      if (this.lstProductSameCode.length > 1) {
        for (var i = 0; i < this.lstProductSameCode.length; i++) {
          chkOrdDtlSame = this.lstProductSameCode.filter(
            (x) =>
              x.colorCd == this.lstProductSameCode[i].colorCd &&
              x.sizeCd == this.lstProductSameCode[i].sizeCd
          ).length;
          if (chkOrdDtlSame > 1) {
            this.sizeOrColorError = this.msgNotChooseSame;
            return;
          }
        }
      }

      if (
        this.currentProduct.maxSizeCanOrder == 0 ||
        this.currentProduct.price == 0 ||
        (this.lstProductSameCode.filter((x) => x.inventoryNumber == 0).length >
          0 &&
          this.isModeInsert)
      ) {
        return;
      }
      if (this.currentProduct.maxAmountCanOrder == 0) {
        this.checkPrice = true;
        return;
      }

      if (this.currentProduct.price.length > 3) {
        this.currentProduct.price = this.currentProduct.price.replace(",", "");
      }
      if (this.currentProduct.maxAmountCanOrder.length > 3) {
        this.currentProduct.maxAmountCanOrder = this.currentProduct.maxAmountCanOrder.replace(
          ",",
          ""
        );
      }

      if (
        Number(this.currentProduct.price) >
        Number(this.currentProduct.maxAmountCanOrder)
      ) {
        this.checkPrice = true;
        return;
      }
      if (chkOrdDtlSame == 0) {
        this.sizeOrColorError = "";
      }
      if (
        this.lstProduct.filter((x) => x.productCd == this.currentProduct.productCd)
          .length > 0 &&
        this.isModeInsert
      ) {
        this.$toastr.e(this.msgProductExited);
        return;
      } else if (this.isModeInsert) {
        this.displayDialogProduct = true;
        const config = {
          method: "get",
          url: "api/MProduct/CheckProductSameCode",
          params: {
            productCd: this.currentProduct.productCd,
          },
        };
        try {
          const resCountProduct = await require("axios")(config);
          if (resCountProduct.data !== null) {
            if (resCountProduct.data != 0) {
              this.$toastr.e(this.msgProductExited);
              this.displayDialogProduct = false;
              return;
            }
          }
        } catch (error) {
          console.log(error);
        } finally {
          this.displayDialogProduct = false;
        }
      }
      if (hasError !== undefined && hasError !== null) {
        const arrayItemErr = this.$validator.errors.items;
        if (arrayItemErr.length !== 0) {
          if(arrayItemErr.length !== 1 && arrayItemErr[0].field !== 'txtBrand'){
            return;
          }
         
        }
        // if (
        //   JSON.stringify(this.currentProduct) ===
        //     JSON.stringify(
        //       this.lstProduct.find(
        //         (x) => x.productCd == this.currentProduct.productCd
        //       )
        //     ) &&
        //   this.isChange === false
        // ) {
        //   this.$toastr.w(this.msgNotthingChanged);
        //   return;
        // }

        this.dialogConfirmProduct = true;
      }
    },
    openImgMain() {
      document.getElementById("imgMain").click();
    },
    changeDisplayFlg(currentProduct) {
      if (!currentProduct.displayFlg) {
        let lstProduct = this.lstProduct
        if (currentProduct.productCd !== undefined) {
          lstProduct = lstProduct.filter(
            (x) =>
              x.productCd !== currentProduct.productCd
          );
        }
        let countProduct = lstProduct.filter((x) => x.displayFlg === currentProduct.displayFlg).length
        if (countProduct > 0) {
          this.errChangeDisplayFlg = false
        } else {
          this.errChangeDisplayFlg = true
        }
      } else {
        this.errChangeDisplayFlg = false
      }
    },
    imgMainChange(e) {
     this.isChange = true;
      const imgMain = e.target.files[0];
      if (imgMain.length === 0) {
        return;
      }
      this.imgMain = URL.createObjectURL(imgMain);
      let img = this.dataImage.find(
        (x) => x.isMainInListPage && !x.isMainInDetailPage
      );
      if (this.isModeInsert || img === undefined) {
        this.dataImage = this.dataImage.filter(
          (x) => !x.isMainInListPage && x.isMainInDetailPage
        );
        this.dataImage.push({
          imageUrl: this.isModeInsert
            ? imgMain.name
            : this.brandNameOption
                .find((x) => x.value == this.currentProduct.brandRltId)
                .text.toLowerCase() +
              "/" +
              this.currentProduct.productCd +
              "/" +
              imgMain.name,
          fileImg: imgMain,
          isHaveFile: true,
          isMainInListPage: true,
          isMainInDetailPage: false,
          createUserCd: this.currentUser,
          productCd: this.isModeInsert ? null : this.currentProduct.productCd,
        });
      } else {
        img.imageUrl = this.setPathUrlImg(imgMain.name, img.productCd);
        img.fileImg = imgMain;
        img.isHaveFile = true;
      }

      this.$refs["imgMain"].reset();
    },
    openImgDetail() {
      document.getElementById("imgDetail").click();
    },
    setPathUrlImg(imgName, productCd) {
      return (
        this.brandNameOption
          .find((x) => x.value == this.currentProduct.brandRltId)
          .text.toLowerCase() +
        "/" +
        productCd +
        "/" +
        imgName
      );
    },
    imgDetailChange(e) {
      this.isChange = true;
      const imgDetail = e.target.files[0];
      if (imgDetail.length === 0) {
        return;
      }
      let img = this.dataImage.find(
        (x) => !x.isMainInListPage && x.isMainInDetailPage
      );
      this.imgDetail = URL.createObjectURL(imgDetail);
      if (this.isModeInsert || img === undefined) {
        this.dataImage = this.dataImage.filter(
          (x) => !x.isMainInDetailPage && x.isMainInListPage
        );
        this.dataImage.push({
          imageUrl: this.isModeInsert
            ? imgDetail.name
            : this.brandNameOption
                .find((x) => x.value == this.currentProduct.brandRltId)
                .text.toLowerCase() +
              "/" +
              this.currentProduct.productCd +
              "/" +
              imgDetail.name,
          fileImg: imgDetail,
          isHaveFile: true,
          isMainInListPage: false,
          isMainInDetailPage: true,
          createUserCd: this.currentUser,
          productCd: this.isModeInsert ? null : this.currentProduct.productCd,
        });
      } else {
        img.imageUrl = this.setPathUrlImg(imgDetail.name, img.productCd);
        img.fileImg = imgDetail;
        img.isHaveFile = true;
      }
      this.$refs["imgDetail"].reset();
    },
    openImgThumbnail() {
      this.imgThumbnailNew = JSON.parse(JSON.stringify(this.imgThumbnail));
      if (
        this.imgThumbnailNew.filter((x) => x.isHaveFile === true).length > 0
      ) {
        this.imgThumbnailNew = [...this.imgThumbnail];
      }
      for (var i = 1; i <= this.imgThumbnailNew.length; i++) {
        this.imgThumbnailNew[i - 1].no = i;
      }
      this.selected = [];
      this.dialogImage = true;
    },
    imgThumbnailNewChange(e) {
      this.selected = [];
      const imgThumbnailNew = e.target.files;
      const currentNo = this.imgThumbnailNew.length;
      if (imgThumbnailNew.length === 0) {
        return;
      }

      for (let i = 0; i < imgThumbnailNew.length; i++) {
        this.imgThumbnailNew.push({
          imageUrl: URL.createObjectURL(e.target.files[i]),
          no: i + currentNo + 1,
          fileImg: e.target.files[i],
          isHaveFile: true,
          imageUrlName: this.isModeInsert
            ? imgThumbnailNew[i].name
            : this.brandNameOption
                .find((x) => x.value == this.currentProduct.brandRltId)
                .text.toLowerCase() +
              "/" +
              this.currentProduct.productCd +
              "/" +
              imgThumbnailNew[i].name,
        });
      }
      this.$refs["imgThumbnailNew"].reset();
    },
    imgThumbnailNewSingleChange(e) {
      const imgThumbnail = e.target.files[0];
      if (imgThumbnail.length === 0) {
        return;
      }
      let imgChange = this.imgThumbnailNew.find(
        (x) => x.no === this.currentImgThumbnail.no
      );
      imgChange.imageUrl = URL.createObjectURL(imgThumbnail);
      imgChange.fileImg = imgThumbnail;
      imgChange.isHaveFile = true;
      imgChange.imageUrlName = this.isModeInsert
        ? imgThumbnail.name
        : this.brandNameOption
            .find((x) => x.value == this.currentProduct.brandRltId)
            .text.toLowerCase() +
          "/" +
          this.imgThumbnailNew[0].productCd +
          "/" +
          imgThumbnail.name;

      this.$refs["imgThumbnailNewSingle"].reset();
    },
    //複数画像を削除する
    async deleteManyImage() {
      if (this.selected.length === 0) {
        this.$toastr.w(this.msgNotChooseImage);
        return;
      }
      if (this.selected.length > this.imgThumbnailNew.length - 1) {
        this.$toastr.w(this.msgNotChooseImageMax2);
        return;
      }

      for (var i = 0; i < this.selected.length; i++) {
        const j = this.imgThumbnailNew.findIndex(
          (x) => x.no === this.selected[i].no
        );
        this.imgThumbnailNew.splice(j, 1);
        this.selected[i].imageUrl = this.selected[i].imageUrl.replace(
          this.PRODUCT_DETAIL_PATH,
          ""
        );
        this.selected[i].delFlg = true;
      }
      this.selected = [];
      this.imgThumbnailNew = this.imgThumbnailNew.map((x, idx) => {
        x.no = idx + 1;
        return x;
      });
    },
    //1枚の画像を削除する
    deleteImage(dataImg) {
      this.selected = [];
      if (this.imgThumbnailNew.length < 2) {
        return;
      }
      const i = this.imgThumbnailNew.findIndex((x) => x.no === dataImg.no);
      this.imgThumbnailNew.splice(i, 1);
      this.imgThumbnailNew = this.imgThumbnailNew.map((x, idx) => {
        x.no = idx + 1;
        return x;
      });
      dataImg.delFlg = true;
      dataImg.imageUrl = dataImg.imageUrl.replace(this.PRODUCT_DETAIL_PATH, "");
    },
    openFileImage() {
      document.getElementById("imgThumbnailNew").click();
    },
    async changeImage() {
      this.dialogImage = false;
      for (let i = 0; i < this.imgThumbnail.length; i++) {
        let imgDel = this.imgThumbnailNew.find(
          (x) => x.productImgUrlId === this.imgThumbnail[i].productImgUrlId
        );
        if (imgDel == undefined) {
          this.imgThumbnail[i].delFlg = true;
          this.imgThumbnail[i].imageUrl = this.imgThumbnail[i].imageUrl.replace(
            this.PRODUCT_DETAIL_PATH,
            ""
          );
          this.dataImageDelete.push(this.imgThumbnail[i]);
        }
      }

      this.dataImage = this.dataImage.filter(
        (x) =>
          (x.isMainInDetailPage && !x.isMainInListPage) ||
          (!x.isMainInDetailPage && x.isMainInListPage)
      );

      for (let i = 0; i < this.imgThumbnailNew.length; i++) {
        if (this.imgThumbnailNew[i].fileImg !== undefined) {
          this.dataImage.push({
            productCd: this.currentProduct.productCd,
            imageUrl: this.imgThumbnailNew[i].imageUrlName,
            fileImg: this.imgThumbnailNew[i].fileImg,
            isHaveFile: true,
            productImgUrlId: this.imgThumbnailNew[i].productImgUrlId,
            isMainInListPage: false,
            isMainInDetailPage: false,
            createUserCd: this.currentUser,
          });
        } else {
          this.dataImage.push(this.imgThumbnailNew[i]);
        }
      }
      this.imgThumbnail = [...this.imgThumbnailNew];
      this.isChange = true;
    },
    //サイズを変更する
    changeSize(index) {
      //this.isChange = true;
      this.sizeOrColorError = "";
      if (this.lstProductSameCode[index].newProduct != true) {
        this.lstProductDelete.push({ ...this.lstProductSameCode[index] });
      }
      this.lstProductSameCode[index].newProduct = true;
    },
    //カラーを変更する
    changeColor(index) {
      //this.isChange = true;
      this.sizeOrColorError = "";
      if (this.lstProductSameCode[index].newProduct != true) {
        this.lstProductDelete.push({ ...this.lstProductSameCode[index] });
      }
      this.lstProductSameCode[index].newProduct = true;
    },
    openFileUpdate(item) {
      this.currentImgThumbnail = { ...item };
      document.getElementById("imgThumbnailNewSingle").click();
    },
    importCsv() {
      document.getElementById("fileCsv").click();
    },
    async fileCsvChange(e) {
      if (e.target.files[0] != null) {
        this.fileCsv = e.target.files[0];
        await this.readDataCsv();
      }
      this.$refs["fileCsv"].reset();
    },
    //csvファイルデータを読み込む
    async readDataCsv() {
      try {
        this.dataCsv = [];
        this.loadingPopup = true;
        const formData = new FormData();
        formData.append("file", this.fileCsv);
        formData.append("bussinessCd", this.bussinessCd);
        const resDataCsv = await this.axios.post(
          "api/MProduct/CheckDataCsv",
          formData,
          {
            headers: {
              "Content-type": "multipart/form-data",
            },
          }
        );
        if (resDataCsv.data.error === undefined) {
          if (resDataCsv.data != null) {
            this.dataCsv = resDataCsv.data;
            this.dataCsv.map((data) => {
              let dataExist = this.dataCsv.filter((x) => x.sku == data.sku);
              if (dataExist.length > 1) {
                data.txtError =
                  data.txtError == null
                    ? this.msgProductSameCode
                    : (data.txtError += this.msgProductSameCode);
              }
            });
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
    //csv取込ダイアログを表示する
    showDialogCsv() {
      this.selectedCsv = [];
      this.dialogCsv = true;
      this.dataCsv = [];
      this.loadingPopup = false;
    },
    //csv取込ダイアログを表示する
    showDialogConfirmInsertCsv() {
      if (this.dataCsv.length <= 0 || this.selectedCsv.length == 0) {
        this.$toastr.w(this.msgProductNotExit);
        return;
      }
      this.dialogConfirmInsertCsv = true;
    },
    async insertCsv() {
      this.dialogConfirmInsertCsv = false;
      this.loadingPopup = true;
      this.selectedCsv.map((x) => (x.createUserCd = this.currentUser));
      const config = {
        method: "post",
        url: "api/MProduct/InsertCsv",
        data: this.selectedCsv.filter((x) => x.txtError == null),
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      try {
        const res = await require("axios")(config);
        if (res.data > 0) {
          this.$toastr.s(this.msgInsertSucces);
        } else {
          this.$toastr.e(this.msgInsertFailed);
        }
      } catch (error) {
        this.$toastr.e(this.msgInsertFailed);
      } finally {
        this.dialogCsv = false;
        await this.getAllMProducts();
        this.search();
        this.loading = false;
      }
    },
    //エラーを表示する
    ShowError(error) {
      this.dialogError = true;
      this.currentMessError = error;
    },
    //検索クリア
    clearSearch() {
      this.txtProductCode = "";
      this.txtProductName = "";
      this.cbbBrandName = 0;
      this.cbbDisplay = -1;
      this.cbbYear = 0;
      this.search();
    },
    //検索
    search() {
      this.loading = true;
      this.lstProduct = [...this.lstProductSearch];
      this.lstProduct = this.lstProduct.filter(
        (x) =>
          x.productCd.includes(this.txtProductCode) &&
          x.productName.includes(this.txtProductName)
      );
      if (this.cbbBrandName != 0) {
        this.lstProduct = this.lstProduct.filter(
          (x) => x.brandRltId == this.cbbBrandName
        );
      }
      if (this.cbbDisplay !== -1) {
        if (this.cbbDisplay == 1) {
          this.lstProduct = this.lstProduct.filter(
            (x) => x.displayFlg === false
          );
        }
        if (this.cbbDisplay == 0) {
          this.lstProduct = this.lstProduct.filter(
            (x) => x.displayFlg === true
          );
        }
      }
      if (this.cbbYear != 0) {
        this.lstProduct = this.lstProduct.filter((x) => x.year == this.cbbYear);
      }
      this.loading = false;
    },
    selectAllCsv() {
      if (this.selectedCsv.length !== this.dataCsv.length) {
        this.selectedCsv = this.dataCsv;
      } else {
        this.selectedCsv = [];
      }
    },
    // ページ変更
    changePageCsv() {
      this.isChangePage = false;
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
  },
  computed: {
    pageStop() {
      if (this.page < Math.ceil(this.lstProduct.length / this.itemsPerPage)) {
        return this.page * this.itemsPerPage;
      } else {
        return this.lstProduct.length;
      }
    },
    pageStart() {
      return (this.page - 1) * this.itemsPerPage + 1;
    },
    itemsLength() {
      return this.lstProduct.length;
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
};
</script>
<style>
.fix-padding-search {
  padding-top: 7px;
  padding-bottom: 7px;
}
</style>
