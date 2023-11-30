<template>
    <div>
      <VueElementLoading
              :active="loading"
              spinner="ring"
              color="var(--success)"
            />
     <!-- main contain -->
    <div class="main-card main_content" style="font-size: 16px !important;">
      <div class="card-body" v-bind:class="[isModeNoData ? 'nodata_mode' : '']">
          <!-- Cust info
        <b-row>
          <b-col md="6" lg="6">
            <h5><strong class="label_title">ご予約受付番号: </strong></h5>
          </b-col>
        </b-row>
        <br> -->
        <b-row>
          <b-col  xl="5" lg="5" md="5">
          </b-col>
           <b-col style="text-align: right;">
               <b-form-input
                    @keyup.enter="getReserveDetail()"
                    ref="barcode"
                    id="txtBarCode"
                    placeholder=" バーコード/ 予約番号"
                    name="txtBarCode"
                    v-model="txtBarCode"
                    class="barCode-border"
                  ></b-form-input>
              <button @click="getReserveDetail()" style="margin-left: -50px;" class="btn-search-pg"></button>
          </b-col>
        </b-row>
        <b-row>
           <b-col  lg="4" md="4" xl="4">
              <b-row>
                <b-col><strong  v-bind:class="[isModeNoData ? 'nodata_mode' : 'label_title']">■ご予約受付番号: </strong> <span><strong>{{this.reservation.orderId}}</strong></span></b-col>
              </b-row>
           </b-col>
          </b-row>
          <br/>
          <b-row>
            <b-col  lg="4" md="4" xl="4">
              <b-row>
                  <b-col><strong v-bind:class="[isModeNoData ? 'nodata_mode' : 'label_title']">■お客様情報</strong> </b-col>
                </b-row>
            </b-col>
          </b-row>
          <br/>
          <b-row class="content_text text_near_title">
            <b-col  lg="4" md="4" xl="4">
              <b-row>
                  <b-col><strong>お名前: {{this.reservation.kanjiFamilyName == undefined? '': this.reservation.kanjiFamilyName
                      +  reservation.kanjiFirstName}}</strong> </b-col>
                </b-row>
            </b-col>
            <b-col  lg="4" md="4" xl="4">
              <b-row>
                  <b-col><strong style="margin-left: 15px;">お電話番号: {{this.maskPhone(this.reservation.phoneNumber)}} </strong> </b-col>
                </b-row>
            </b-col>
          </b-row>
        <br>
        <v-divider/>
        <br>
        <!-- Reserve info -->
        <b-row>
          <b-col md="6" lg="6">
            <strong v-bind:class="[isModeNoData ? 'nodata_mode' : 'label_title']">■ご予約内容</strong>
          </b-col>
        </b-row>
        <br>
        <b-row class="content_text text_near_title">
          <b-col lg="4" md="4" xl="4"><strong>商品名</strong></b-col>
          <b-col lg="2" md="2" xl="2"><strong>カラー</strong></b-col>
          <b-col lg="2" md="2" xl="2"><strong>サイズ</strong></b-col>
          <b-col lg="2" md="2" xl="2"><strong>数量</strong></b-col>
          <b-col lg="2" md="2" xl="2"><strong>⼩計</strong></b-col>
        </b-row>
        <v-divider/>
        <div v-for="(orderDetail, index) in this.reserveDetails" :key="index">
        <b-row class="content_text">
          <b-col lg="4" md="4" xl="4">
            <strong>{{orderDetail.productName}}</strong>
            <br>
            <strong>{{orderDetail.productCd}}</strong>
          </b-col>
          <b-col lg="2" md="2" xl="2">{{orderDetail.colorName}}</b-col>
          <b-col lg="2" md="2" xl="2">{{orderDetail.sizeName}}</b-col>
          <b-col lg="2" md="2" xl="2"><span class="align_right_number">{{orderDetail.quantity}}</span></b-col>
          <b-col lg="2" md="2" xl="2"><span><strong>¥{{formatPrice(orderDetail.subTotal)}}</strong></span></b-col>
        </b-row>
        <v-divider/>
        </div>
        <!-- display line when no data -->
        <div v-if="isModeNoData">
          <br>
        <v-divider/>
        </div>
        <!-- Total -->
        <b-row  class="content_text">
          <b-col lg="1" md="1" xl="1"></b-col>
          <b-col lg="7" md="7" xl="7"><strong>ご予約合計⾦額</strong></b-col>
          <b-col lg="2" md="2" xl="2"><span class="align_right_number" v-if="this.reservation.orderId !== undefined">{{this.reservation.totalQuantity}}</span></b-col>
          <b-col lg="2" md="2" xl="2"><span v-if="this.reservation.orderId !== undefined"><strong>¥{{formatPrice(this.reservation.total)}}</strong></span></b-col>
        </b-row>
         <b-row>
          <b-col lg="1" md="1" xl="1"></b-col>
          <b-col lg="11" md="11" xl="11"><v-divider/></b-col>
        </b-row>
        <br>
        <!-- Reserve info -->
        <b-row>
          <b-col md="6" lg="6">
            <strong v-bind:class="[isModeNoData ? 'nodata_mode' : 'label_title']">■メモ</strong>
          </b-col>
        </b-row>
        <br>
        <b-row class="content_text text_near_title">
            <b-col style="word-break: break-all;"><strong>{{this.reservation.memo}}</strong></b-col>
        </b-row>
        <!-- Status -->
        <b-row>
          <b-col lg="1" md="1" xl="1"></b-col>
          <b-col lg="7" md="7" xl="7">
            <b-row style="color:#DB8383">
               <b-col><strong>ステータス :</strong>
                <span v-bind:class="[this.reservation.status === RSV_STATUS_PAID  ? 'text-success' : '']" v-if="this.reservation.orderId !== undefined">{{this.reservation.statusName}}</span>
                <span v-if="this.reservation.reserveMailStatus !== undefined &&
                        this.reservation.reserveMailStatus !== null && isModeNoData === false" style="padding-left:20px">
                  ({{ getTextFromKbnValue(mailStatusKbn, this.reservation.reserveMailStatus)}})
                </span>
                <span v-else style="padding-left:20px">
                    <span v-if="isModeNoData === false">({{  getTextFromKbnValue(mailStatusKbn, MAIL_STATUS_NOTSEND) }})</span>
                </span>
                </b-col>
            </b-row>
          </b-col>
          <b-col lg="4" md="4" xl="4" v-if="(getUserData().department === this.DEPART_SHOP_TYPE) ||
                                            (getUserData().department === this.DEPART_ADMIN_TYPE && !isUserReadOnly) ">
              <v-btn v-if="isModeNoData"
                class="btn btn_purple"
                :disabled="!isPaymentAble"
                style="width: 50%; margin-left: 80px;"
                variant="success"
              >入金・引渡</v-btn>
              <v-btn v-if="this.reservation.status === RSV_STATUS_ORDER && isModeNoData == false"
                class="btn btn_purple"
                :disabled="!isPaymentAble"
                @click="showPopupConfirm(RSV_STATUS_PAID)"
                style="width: 50%; margin-left: 80px;"
                variant="success"
              >入金・引渡</v-btn>
              <!-- <v-btn v-if="this.reservation.status !== RSV_STATUS_ORDER && isModeNoData == false"
                class="btn btn_purple"
                :disabled="!isDeliveryAble"
                @click="showPopupConfirm(RSV_STATUS_COMPLETED_DELIVERY)"
                  style="width: 50%; margin-left: 80px;"
                variant="success"
              >引渡</v-btn> -->
          </b-col>
        </b-row>
         <!-- Confirm dialog-->
         <v-dialog v-model="dialogConfirm" max-width="401px"  content-class="v-dialog-border" >
          <v-card>
            <v-card-text>
              <div class="text-center">{{this.confirmContent}}。よろしいですか。</div>
            </v-card-text>
            <v-card-actions style="text-align: center;display: block;">
              <v-btn
                class="btn_confirm_no"
                text
                @click="dialogConfirm = false;"
              >いいえ</v-btn>
              <v-btn class="btn_confirm_yes"
                :loading="buttonConfirm"
                text @click="updateReserve()">はい</v-btn>
            </v-card-actions>
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
      </div>
    </div>
    </div>
</template>

<script>

export default {
  data: () => ({
    confirmContent: '',
    dialogConfirm: false,
    buttonConfirm: false,
    loading: false,
    // ---------------------------------------End data Reserve List ---------------------------
    reservation: {},
    reserveDetails: [],
    txtBarCode: '',
    updateStatus: '',
    mailStatusKbn: [],
    rsvStatusKbn: [],
    isPaymentAble: false,
    // isDeliveryAble: false,
    isModeNoData: true,
    userData: null,
    dialogChangePwConfirm: false
  }),
  methods: {
    resetInitMode(){
       this.isModeNoData = true
      this.isPaymentAble = false
      // this.isDeliveryAble = false
      this.reservation = {}
      this.reserveDetails = {}
      this.reservation.orderId = undefined
    },
    //  _______________________________ Function  call API  ___________________________________
    async getReserveDetail () {
      const isValid = await this.$validator.validateAll()
      if (this.txtBarCode === '') {
        this.$toastr.e(this.msgPleaseInput.replace('*', 'バーコードまたは予約番号'))
        this.resetInitMode()
        this.sleep(2000);
      } else if (this.txtBarCode !== '' && isValid) {
        const config = {
          method: 'post',
          url: 'api/MShop/GetReserveDetail',
          params: { barCode: this.txtBarCode },
          data: this.userData
        }
        this.loading = true
        try {
          const res = await require('axios')(config)
          if(res === undefined){
            this.$toastr.e(this.msgNoData)
            this.resetInitMode()
          }
          else if (res.data !== null) {
            if (res.data.error === undefined) {
              if (res.data.reservation !== null) {
                this.isModeNoData = false
                // Set data order
                this.reservation = res.data.reservation
                
                // if (this.reservation.status === this.RSV_STATUS_COMPLETED_DELIVERY ||
                // this.reservation.status === this.RSV_STATUS_CANCEL) {
                if (this.reservation.status === this.RSV_STATUS_CANCEL) {
                  this.isPaymentAble = false
                  // this.isDeliveryAble = false
                }
              } else {
                this.$toastr.e(this.msgNoData)
                this.reservation = {}
              }
              // set data order detail
              if (res.data.reservationDetails !== null) {
                this.reserveDetails = res.data.reservationDetails
              } else {
                this.reserveDetails = []
              }

              // set payment button status
              if (res.data.isPaymentAble !== null) {
                this.isPaymentAble = res.data.isPaymentAble
              } else {
                this.isPaymentAble = false
              }
              // set delivery button status
              // if (res.data.isDeliveryAble !== null) {
              //   this.isDeliveryAble = res.data.isDeliveryAble
              // } else {
              //   this.isDeliveryAble = false
              // }
            } else {
              this.$toastr.e(res.data.error)
              this.resetInitMode()
            }
          } else {
            this.$toastr.e(this.msgNoData)
            this.resetInitMode()
          }
        } finally {
          this.loading = false
        }
      }
    },
    // _______________________________ Update reserve status __________________________________
    async updateReserve () {
      this.buttonConfirm = true
      this.dialogConfirm = false
      // this.reservation.status = this.updateStatus
      var lastStatus = this.reservation.status
      this.reservation.status = this.updateStatus
      this.reservation.updateUserCd = this.userData.userCd
      const config = {
        method: 'post',
        url: 'api/MShop/UpdateReserve',
        params: { lastStatus: lastStatus },
        data: this.reservation
      }
      try {
        this.loading = true
        const res = await require('axios')(config)
        if (res.data !== null) {
          if (res.data.error === undefined) {
            if (res.data.isUpdated !== false) {
              this.$toastr.s(this.msgUpdateSucces)
            } else {
              this.$toastr.e(this.msgUpdateFailed)
            }
             this.getReserveDetail()
            if (res.data.isSendMailFinishedSucces !==null &&
                res.data.isSendMailFinishedSucces == false) {
              this.$toastr.e(this.msgSendMailFailed)
            }
          } else {
            if (res.data.isNeedGetNewData !== undefined) {
              if (res.data.isNeedGetNewData === true) {
                this.getReserveDetail()
              }
            }
            this.$toastr.e(res.data.error)
          }
        } else {
          this.$toastr.e(this.msgUpdateFailed)
        }
      } catch (error) {
        this.$toastr.e(this.msgUpdateFailed)
      } finally {
        this.loading = false
      }
    },
    async callGetKbnValue () {
      this.mailStatusKbn = await this.getParameterByCd(this.KBN_MAIL_STATUS)
      //this.rsvStatusKbn = await this.getParameterByCd(this.KBN_RSV_STATUS)
    },
    //  _______________________________ End Function  call API  _______________________________
    // _______________________________ common function __________________________________
    showPopupConfirm (status) {
      if (this.reservation.status === this.RSV_STATUS_ORDER && this.isPaymentAble === false) {
        return
      } else if (this.reservation.status === this.RSV_STATUS_CANCEL) {
        return
      }
      this.buttonConfirm = false
      this.dialogConfirm = true
      this.confirmContent = (status === this.RSV_STATUS_PAID) ? '⼊⾦・引き渡しします' : ''
      this.updateStatus = status
    }
    // _______________________________ End common function __________________________________
  },
  created () {
    this.$nextTick(() => {
      this.$refs.barcode.focus()
    })
    this.userData = this.getUserData()
    this.callGetKbnValue()
    this.checkDefaultPassword()
  },
  computed: {

  }
}
</script>

<style lang="scss" scoped>
.barCode-border {
    border-radius: 10px;
    border: 1px solid #008CC8;
    height: 50px;
    width: 350px;
    display: inline;
}
hr {
    margin-top: 1rem !important; 
    margin-bottom: 1rem !important;
}
</style>
