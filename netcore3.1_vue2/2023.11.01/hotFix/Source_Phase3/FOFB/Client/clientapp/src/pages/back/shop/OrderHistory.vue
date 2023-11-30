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
              <label class="col-md-4 label-search fix-padding-search">お客様名前</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-input v-model="searchOrderHistory.custName" placeholder="" single-line hide-details />
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search">メールアドレス</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-input v-model="searchOrderHistory.mailAddress" placeholder="" single-line hide-details />
              </div>
            </div>
          </div>
          <div class="col-md-4">
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search">予約受付番号</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-input v-model="searchOrderHistory.orderId" placeholder="" single-line hide-details />
              </div>
            </div>
            <div class="row col-md-12 fix-padding-search">
              <label class="col-md-4 label-search fix-padding-search">お電話番号</label>
              <div class="col-md-7 fix-padding-search">
                <b-form-input v-model="searchOrderHistory.phoneNumber" placeholder="" single-line hide-details />
              </div>
            </div>
          </div>
          <div class="col-md-4">
            <div class="row btn-grp-search">
              <div class="col-md-4" style="text-align: right;">
                <button type="button" class="btn btn-primary btn-search" @click="search()">
                  検索
                </button>
              </div>
              <div class="col-md-4" style="padding-left: 20px;">
                <button type="button" class="btn btn-secondary btn-clear" @click="clearSearch()">
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
        <v-data-table
          :headers="headersLstOrderHistory"
          :items="lstOrder"
          item-key="orderId"
          hide-default-footer
          :items-per-page="itemsPerPage"
          :no-results-text="msgNoData"
          :no-data-text="msgNoData"
          @update:options="changeSort"
        >
          <template v-slot:item="{ item }">
            <tr>
              <td class="text-left">
                <a @click="openOrderDialog(item)" class="router">{{
                  item.orderId
                }}</a>
              </td>
              <td>{{ item.kanjiFamilyName + item.kanjiFirstName }}</td>
              <td>
                {{ maskPhone(item.phoneNumber) }}
              </td>
              <td class="text-left">{{ maskMail(item.mailAddress) }}</td>
              <td class="text-left">{{ item.statusName }}</td>
              <td class="text-left">
                {{ item.orderMailStatusName }}
              </td>
              <td class="text-left">
                <a @click="memoDialog(item)" class="router"
                  >{{ item.memo }}
                </a>
              </td>
            </tr>
          </template>
        </v-data-table>
        <!-- order dialog-->
        <v-dialog v-model="dialogOrderDetail" max-width="1000px">
          <v-card>
            <v-card-text>
              <div class="main-card">
                <!-- cust receive info -->
                <div v-if="orderDetail.receiveWay === this.SHIPPING && orderDetail.paymentWay === this.PAY_IN_SHOP">
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
                              ></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <span>{{orderDetail.kanjiFamilyNameMCustReceive}}</span>
                          </b-col>
                        </b-row>
                        <!-- Kanji First Name -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>名（漢字）</strong
                              ></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <span>{{ orderDetail.kanjiFirstNameMCustReceive }}</span>
                          </b-col>
                        </b-row>
                        <!-- Kana family Name -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>姓（かな）</strong
                              ></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <span>{{ orderDetail.kanaFamilyNameMCustReceive }}</span>
                          </b-col>
                        </b-row>
                        <!-- Kana First Name -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>名（かな）</strong
                              ></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <span>{{ orderDetail.kanaFirstNameMCustReceive }}</span>
                          </b-col>
                        </b-row>
                        <!-- zipCd -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>郵便番号 </strong
                              ></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <span>{{ orderDetail.zipCodeDspMCustReceive }}</span>
                          </b-col>
                        </b-row>
                        <!-- Province -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>都道府県 </strong
                              ></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <span>{{ orderDetail.provinceNameMCustReceive }}</span>
                          </b-col>
                        </b-row>
                        <!-- address 1 -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>住所１</strong
                              ></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <span>{{ orderDetail.adrCd1MCustReceive }}</span>
                          </b-col>
                        </b-row>
                        <!-- address 2 -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>住所2 </strong
                              ></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <span>{{ orderDetail.adrCd2MCustReceive }}</span>
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
                            <span>{{ orderDetail.adrCd3MCustReceive }}</span>
                          </b-col>
                        </b-row>
                        <!-- phone number -->
                        <b-row>
                          <b-col lg="2" md="12">
                            <label class="color-black"
                              ><strong>電話番号 </strong
                              ></label
                            >
                          </b-col>
                          <b-col lg="8" md="12">
                            <span>{{ orderDetail.phoneNumberMCustReceive }}</span>
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
                        <div class="col-md-10" style="margin-bottom: 7px;">
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
                        <!-- Shop Name-->
                        <div
                          class="col-md-6"
                        >
                          <div class="row">
                            <label class="col-md-4 fix-lable-shop color-black">
                              <strong>入金・受取店舗</strong>
                            </label>
                            <span class="col-md-8" style="padding-left: 7px;">{{ orderDetail.shopName }}</span>
                          </div>
                          <br />
                        </div>
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
                              <tr>
                                <td class="product-group">
                                  <span class="product-name">{{ item.productName }}</span>
                                  <br/>
                                  <span class="product-code">{{ item.productCd }}</span>
                                </td>
                                <td class="text-left">
                                  <span>{{ item.colorName }}</span>
                                </td>
                                <td class="text-left">
                                  <span>{{ item.sizeName }}</span>
                                </td>
                                <td class="text-left">
                                  <span>{{ item.quantity }}</span>
                                </td>
                                <td class="text-left">
                                  ¥{{ formatPrice(item.subTotal) }}
                                </td>
                              </tr>
                            </template>
                          </v-data-table>
                        </div>
                        <div class="row col-md-8 offset-md-4" style="padding-left: 10px;">
                          <div class="col-md-6 color-black" style="padding-left: unset;">
                            <strong>ご予約合計金額</strong>
                          </div>
                          <td class="col-md-2 color-black" style="padding-left: 10px;">
                            <strong>{{ orderDetail.totalQuantity }}</strong>
                          </td>
                          <div class="col-md-4 color-black">
                            <span class="price"
                              ><strong
                                >¥{{ formatPrice(orderDetail.total) }}</strong
                              ></span
                            >
                            <span class="tax"><strong>(税込)</strong></span>
                          </div>
                        </div>
                        <br />
                      </div>
                      <div style="margin-top: 10px;">
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
                      readonly
                      solo
                      rows="10"
                    ></v-textarea>
                  </v-col>
                </div>
              </div>
            </v-card-text>
          </v-card>
        </v-dialog>
      </div>
    </div>
  </div>
</template>

<script>
import OrderHistory from "@/components/OrderHistory";
export default {
  data() {
    return {
      loading: false,
      bussinessCd: "",
      shopCd: "",
      itemsPerPage: 0,
      itemsPerPageDetail: 0,
      totalData: 0,
      page: 1,
      pageCount: 0,
      pageOrdDetail: 1,
      sortColumnName: "",
      isDesc: true,
      searchOrderHistory: {},
      lstOrder: [],
      lstOrderDetail: [],
      lstOrderHistory: {},
      headersLstOrderHistory: [
        {
          text: "予約番号",
          sortable: true,
          align: "center",
          value: "orderId",
          width: "10%",
        },
        {
          text: "お客様名前",
          align: "center",
          sortable: false,
          width: "15%",
        },
        {
          text: "お電話番号",
          align: "center",
          sortable: false,
          width: "10%",
        },
        {
          text: "メールアドレス",
          align: "center",
          sortable: false,
          width: "15%",
        },
        {
          text: "予約ステータス",
          align: "center",
          sortable: true,
          value: "status",
          width: "15%",
        },
        {
          text: "メール送信ステータス",
          align: "center",
          sortable: true,
          value: "orderMailStatus",
          width: "15%",
        },
        {
          text: "メモ",
          align: "center",
          sortable: false,
          width: "20%",
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
        { text: "サイズ", width: "15%", sortable: false },
        {
          text: "数量",
          sortable: false,
          width: "10%",
          align: "left",
        },
        { text: "小計", width: "20%", sortable: false },
      ],
      orderDetail: {},
      memoDetail: "",
      dialogMemo: false,
      dialogOrderDetail: false
    };
  },
  components: {
    OrderHistory,
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
    this.shopCd = this.getUserData().userCd;
    this.itemsPerPage = this.PAGE_SIZE_DEFAULT;
    this.itemsPerPageDetail = this.PAGE_SIZE_DETAIL_DEFAULT;
    this.sortColumnName = "ord.UpdateDtTm";

    await this.callGetKbnValue();

    await this.getListOrderHistory(this.PAGE_START_DEFAULT);
  },
  methods: {
    // 検索条件による予約履歴（店舗）
    async getListOrderHistory(page) {
      this.loading = true;
      this.searchOrderHistory.bussinessCd = this.bussinessCd;
      this.searchOrderHistory.shopCd = this.shopCd;
      const config = {
        method: "post",
        url: "api/MShop/GetDataOrdersHistoryPagination",
        data: this.searchOrderHistory,
        params: {
          currentpage: page,
          pageSize: this.PAGE_SIZE_DEFAULT,
          sortColumnName: this.sortColumnName,
          isDesc: this.isDesc
        },
      };
      try {
        const resultGetLstOrderHistory = await require("axios")(config);
        if (resultGetLstOrderHistory.data !== null) {
          this.lstOrder = resultGetLstOrderHistory.data.data;
          this.totalData = resultGetLstOrderHistory.data.totalData;
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

      this.orderDetail = { ...orderItem };

      await this.getOrderDetail(orderItem.orderId);
      
      await this.getOrderHistory(orderItem.orderId);

      this.dialogOrderDetail = true;
      this.loading = false;
    },
    // メモダイアログを表示する
    memoDialog(orderItem) {
      this.memoDetail = orderItem.memo;
      this.dialogMemo = !this.dialogMemo;
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
    // 検索
    search() {
      this.getListOrderHistory(this.PAGE_START_DEFAULT);
    },
    // 検索クリア
    clearSearch() {
      this.searchOrderHistory = {}
      this.getListOrderHistory(this.PAGE_START_DEFAULT);
    },
    // ページ変更
    async changePage(page) {
      await this.getListOrderHistory(page);
    },
    // 並び替える
    async changeSort({ sortBy, sortDesc }) {
      if (sortBy[0] != undefined && sortDesc[0] != undefined) {
        if (sortBy[0] == "orderId") {
          this.sortColumnName = "ord.OrderId";
        } else if (sortBy[0] === "status") {
          this.sortColumnName = "ord.Status";
        } else if (sortBy[0] === "orderMailStatus") {
          this.sortColumnName = "sm_order.MailStatus";
        } else {
          this.sortColumnName = "ord.UpdateDtTm";
        }

        this.isDesc = sortDesc[0];
        
        await this.getListOrderHistory(this.PAGE_START_DEFAULT);
      } else {
        this.sortColumnName = "ord.UpdateDtTm";
      }
    },
    async callGetKbnValue() {
      this.histTypeValue = await this.getParameterByCd(this.KBN_HIST_TYPE);
      this.orderStatusKbn = await this.getParameterByCd(this.KBN_ORDER_STATUS);
      this.mailType = await this.getParameterByCd(this.KBN_MAIL_TYPE);
    },
  },
};
</script>
<style scoped>
.fix-padding-lable {
  padding-bottom: unset;
  padding-top: unset;
  margin-bottom: unset;
  padding-left: unset;
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

.label-search {
  color: #040404;
  font-weight: bold;
}

#title-shipping {
  font-size: 17px !important;
  color: #040404;
}
</style>