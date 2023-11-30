<template>
  <div>
    <VueElementLoading
      :active="loading"
      spinner="ring"
      color="var(--success)"
    />
    <br />
    <!---  ordStartDtTm -->
    <b-row>
      <b-col lg="2">
        <label class="col-md-12"
          ><strong>予約開始日時</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="3">
        <div class="row col-md-11">
          <v-menu v-model="menuOrdStartDate" :close-on-content-click="false">
            <template v-slot:activator="{ on, attrs }">
              <b-form-input
                name="txtOrdStartDate"
                class="col-lg-6 mr-5 item"
                v-validate="'required'"
                v-model="ordStartDate"
                readonly
                v-bind="attrs"
                v-on="on"
              ></b-form-input>
            </template>
            <v-date-picker
              class="startDate"
              locale="ja-JA"
              :min="moment().format()"
              :max="ordEndDate"
              v-validate="'required'"
              v-model="ordStartDate"
              @input="menuOrdStartDate = false"
            ></v-date-picker>
          </v-menu>
          <v-menu
            ref="menuOrdStartTime"
            v-model="menuOrdStartTime"
            :close-on-content-click="false"
            max-width="290px"
            min-width="290px"
          >
            <template v-slot:activator="{ on, attrs }">
              <b-form-input
                :class="`col-md-3 item ${ordStartDate < moment().format('yyyy-MM-DD') ? 'disable-input' : ''}`"
                v-model="ordStartTime"
                readonly
                :disabled="ordStartDate < moment().format('yyyy-MM-DD')"
                v-bind="attrs"
                v-on="on"
              ></b-form-input>
            </template>
            <v-time-picker
              v-if="menuOrdStartTime"
              v-model="ordStartTime"
              format="24hr"
              no-title
              :min="
                ordStartDate < moment().format() ? moment().format('HH:mm') : ''
              "
              :max="ordStartDate == ordEndDate ? ordEndTime : ''"
              @click:minute="$refs.menuOrdStartTime.save(ordStartTime)"
            ></v-time-picker>
          </v-menu>
        </div>
        <span class="invalid-feedback d-block">{{
                    errors.first("txtOrdStartDate")}}
        </span>
      </b-col>
    </b-row>
    <!---  ordEndDtTm -->
    <b-row>
      <b-col lg="2">
        <label class="col-md-12"
          ><strong>予約終了日時</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="3">
        <div class="row col-md-11">
          <v-menu v-model="menuOrdEndDate" :close-on-content-click="false">
            <template v-slot:activator="{ on, attrs }">
              <b-form-input
                name="txtOrdEndDate"
                class="col-lg-6 mr-5 item"
                v-validate="'required'"
                v-model="ordEndDate"
                readonly
                v-bind="attrs"
                v-on="on"
              ></b-form-input>
            </template>
            <v-date-picker
              class="startDate"
              locale="ja-JA"
              :min="
                ordStartDate < moment().format('yyyy-MM-DD')
                  ? moment().format()
                  : ordStartDate
              "
              v-model="ordEndDate"
              @input="menuOrdEndDate = false"
            ></v-date-picker>
          </v-menu>
          <v-menu
            ref="menuOrdEndTime"
            v-model="menuOrdEndTime"
            :close-on-content-click="false"
            max-width="290px"
            min-width="290px"
          >
            <template v-slot:activator="{ on, attrs }">
              <b-form-input
                :class="`col-md-3 item ${ordEndDate < moment().format('yyyy-MM-DD') ? 'disable-input' : ''}`"
                v-model="ordEndTime"
                :disabled="ordEndDate < moment().format('yyyy-MM-DD')"
                readonly
                v-bind="attrs"
                v-on="on"
              ></b-form-input>
            </template>
            <v-time-picker
              v-if="menuOrdEndTime"
              v-model="ordEndTime"
              format="24hr"
              no-title
              :min="
                ordStartDate == ordEndDate
                  ? ordStartTime
                  : ordEndDate < moment().format()
                  ? moment().format('HH:mm')
                  : ''
              "
              @click:minute="$refs.menuOrdEndTime.save(ordEndTime)"
            ></v-time-picker>
          </v-menu>
        </div>
        <span class="invalid-feedback d-block">{{
                    errors.first("txtOrdEndDate")}}
        </span>
      </b-col>
    </b-row>
    <!---  RcvMoneyStartDtTm -->
    <b-row>
      <b-col lg="2">
        <label class="col-md-12"
          ><strong>入金開始日時</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="3">
        <div class="row col-md-11">
          <v-menu
            v-model="menuRcvMoneyStartDate"
            :close-on-content-click="false"
          >
            <template v-slot:activator="{ on, attrs }">
              <b-form-input
                name="txtRcvMoneyStartDate"
                class="col-lg-6 mr-5 item"
                v-validate="'required'"
                v-model="rcvMoneyStartDate"
                readonly
                v-bind="attrs"
                v-on="on"
              ></b-form-input>
            </template>
            <v-date-picker
              class="startDate"
              locale="ja-JA"
              :min="moment().format()"
              :max="rcvMoneyEndDate"
              v-model="rcvMoneyStartDate"
              @input="menuRcvMoneyStartDate = false"
            ></v-date-picker>
          </v-menu>
          <v-menu
            ref="menuRcvMoneyStartTime"
            v-model="menuRcvMoneyStartTime"
            :close-on-content-click="false"
            max-width="290px"
            min-width="290px"
          >
            <template v-slot:activator="{ on, attrs }">
              <b-form-input
                :class="`col-md-3 item ${rcvMoneyStartDate < moment().format('yyyy-MM-DD') ? 'disable-input' : ''}`"
                v-model="rcvMoneyStartTime"
                :disabled="rcvMoneyStartDate < moment().format('yyyy-MM-DD')"
                readonly
                v-bind="attrs"
                v-on="on"
              ></b-form-input>
            </template>
            <v-time-picker
              v-if="menuRcvMoneyStartTime"
              v-model="rcvMoneyStartTime"
              format="24hr"
              no-title
              :min="
                rcvMoneyStartDate < moment().format()
                  ? moment().format('HH:mm')
                  : ''
              "
              :max="rcvMoneyStartDate == rcvMoneyEndDate ? rcvMoneyEndTime : ''"
              @click:minute="
                $refs.menuRcvMoneyStartTime.save(rcvMoneyStartTime)
              "
            ></v-time-picker>
          </v-menu>
        </div>
        <span class="invalid-feedback d-block">{{
                    errors.first("txtRcvMoneyStartDate")}}
        </span>
      </b-col>
    </b-row>
    <!---  RcvMoneyEndtDtTm -->
    <b-row>
      <b-col lg="2">
        <label class="col-md-12"
          ><strong>入金終了日時</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="3">
        <div class="row col-md-11">
          <v-menu v-model="menuRcvMoneyEndDate" :close-on-content-click="false">
            <template v-slot:activator="{ on, attrs }">
              <b-form-input
                name="txtRcvMoneyEndDate"
                class="col-lg-6 mr-5 item"
                v-validate="'required'"
                v-model="rcvMoneyEndDate"
                readonly
                v-bind="attrs"
                v-on="on"
              ></b-form-input>
            </template>
            <v-date-picker
              class="startDate"
              locale="ja-JA"
              :min="
                rcvMoneyStartDate < moment().format('yyyy-MM-DD')
                  ? moment().format()
                  : rcvMoneyStartDate
              "
              v-model="rcvMoneyEndDate"
              @input="menuRcvMoneyEndDate = false"
            ></v-date-picker>
          </v-menu>
          <v-menu
            ref="menuRcvMoneyEndTime"
            v-model="menuRcvMoneyEndTime"
            :close-on-content-click="false"
            max-width="290px"
            min-width="290px"
          >
            <template v-slot:activator="{ on, attrs }">
              <b-form-input
                :class="`col-md-3 item ${rcvMoneyEndDate < moment().format('yyyy-MM-DD') ? 'disable-input' : ''}`"
                v-model="rcvMoneyEndTime"
                readonly
                :disabled="rcvMoneyEndDate < moment().format('yyyy-MM-DD')"
                v-bind="attrs"
                v-on="on"
              ></b-form-input>
            </template>
            <v-time-picker
              v-if="menuRcvMoneyEndTime"
              v-model="rcvMoneyEndTime"
              format="24hr"
              no-title
              :min="
                rcvMoneyStartDate == rcvMoneyEndDate
                  ? rcvMoneyStartTime
                  : rcvMoneyEndDate < moment().format()
                  ? moment().format('HH:mm')
                  : ''
              "
              @click:minute="$refs.menuRcvMoneyEndTime.save(rcvMoneyEndTime)"
            ></v-time-picker>
          </v-menu>
          <span class="invalid-feedback d-block">{{
                    errors.first("txtRcvMoneyEndDate")}}
        </span>
        </div>
      </b-col>
    </b-row>
    <!---  DeliveryStartDtTm -->
    <!--<b-row>
      <b-col lg="2">
        <label class="col-md-12"
          ><strong>引渡開始日時</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="3">
        <div class="row col-md-11">
          <v-menu
            v-model="menuDeliveryStartDate"
            :close-on-content-click="false"
          >
            <template v-slot:activator="{ on, attrs }">
              <b-form-input
                name="txtDeliveryStartDate"
                class="col-lg-6 mr-5 item"
                v-validate="'required'"
                v-model="deliveryStartDate"
                readonly
                v-bind="attrs"
                v-on="on"
              ></b-form-input>
            </template>
            <v-date-picker
              class="startDate"
              locale="ja-JA"
              :min="moment().format()"
              v-model="deliveryStartDate"
              @input="menuDeliveryStartDate = false"
            ></v-date-picker>
          </v-menu>
          <v-menu
            ref="menuDeliveryStartTime"
            v-model="menuDeliveryStartTime"
            :close-on-content-click="false"
            max-width="290px"
            min-width="290px"
          >
            <template v-slot:activator="{ on, attrs }">
              <b-form-input
                :class="`col-md-3 item ${deliveryStartDate < moment().format('yyyy-MM-DD') ? 'disable-input' : ''}`"
                v-model="deliveryStartTime"
                :disabled="deliveryStartDate < moment().format('yyyy-MM-DD')"
                readonly
                v-bind="attrs"
                v-on="on"
              ></b-form-input>
            </template>
            <v-time-picker
              v-if="menuDeliveryStartTime"
              v-model="deliveryStartTime"
              format="24hr"
              no-title
              :min="
                deliveryStartDate < moment().format()
                  ? moment().format('HH:mm')
                  : ''
              "
              @click:minute="
                $refs.menuDeliveryStartTime.save(deliveryStartTime)
              "
            ></v-time-picker>
          </v-menu>
        </div>
        <span class="invalid-feedback d-block">{{errors.first("txtDeliveryStartDate")}}
        </span>
      </b-col>
    </b-row>-->
    <b-row>
      <b-col offset-lg="3">
        <button
          @click="showdialogConfirmUpdate()"
          style="width: 90px"
          class="btn btn-danger"
        >
          保存
        </button>
      </b-col>
    </b-row>
    <!--  dialog confirm update Url start -->
    <v-dialog
      v-model="dialogConfirmUpdateDateTime"
      max-width="401px"
      content-class="v-dialog-border"
    >
      <v-card>
        <v-card-text>
          <div class="text-center">変更します。よろしいですか。</div>
        </v-card-text>
        <v-card-actions style="text-align: center; display: block">
          <v-btn
            class="btn_confirm_no"
            @click="dialogConfirmUpdateDateTime = false"
            >キャンセル</v-btn
          >
          <v-btn class="btn_confirm_yes" @click="updateDateTime()">保存</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!--  dialog confirm update Url end -->
  </div>
</template>

<script>
import moment from "moment";

export default {
  data() {
    return {
      moment: moment,
      ordStartDate: null,
      ordStartTime: null,
      ordEndDate: null,
      ordEndTime: null,
      currentUser: null,
      rcvMoneyStartDate: null,
      rcvMoneyStartTime: null,
      rcvMoneyEndDate: null,
      rcvMoneyEndTime: null,
      //deliveryStartDate: null,
      deliveryStartTime: null,
      oldAllDateTime: null,
      bussinessCd: null,
      dialogConfirmUpdateDateTime: false,
      loading: false,
      menuOrdStartTime: false,
      menuOrdStartDate: false,
      menuOrdEndTime: false,
      menuOrdEndDate: false,
      menuRcvMoneyStartTime: null,
      menuRcvMoneyStartDate: null,
      menuRcvMoneyEndTime: null,
      menuRcvMoneyEndDate: null,
      menuDeliveryStartDate: null,
      menuDeliveryStartTime: null,
    };
  },
  async created() {
    this.loading = true;
    this.currentUser = this.getUserData().userCd;
    this.bussinessCd = this.getUserData().bussinessCd;
    await this.getDateTime();
    this.loading = false;
  },
  methods: {
    // show Dialog Confirm Update DateTime
    async showdialogConfirmUpdate() {
      if(this.oldAllDateTime === this.ordStartDate + this.ordStartTime + this.ordEndDate + this.ordEndTime 
                              + this.rcvMoneyStartDate + this.rcvMoneyStartTime + this.rcvMoneyEndDate + this.rcvMoneyEndTime)
                              //+ this.deliveryStartDate + this.deliveryStartTime)
      {
        this.$toastr.w(this.msgNotthingChanged);
        return;
      }

      const error = await this.$validator.validateAll();
      if (error) {
        this.dialogConfirmUpdateDateTime = true;
      }
    },
    // 条件による予約日時設定一覧を取得する
    async getDateTime() {
      const config = {
        method: "get",
        url: "api/DatetimeSetting/GetAll",
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      try {
        const resUrl = await require("axios")(config);
        if (Object.keys(resUrl.data).length > 0) {
          this.ordStartDate = resUrl.data[0].ordStartDtTm.substr(0, 10);
          this.ordStartTime = resUrl.data[0].ordStartDtTm.substr(11, 5);
          this.ordEndDate = resUrl.data[0].ordEndDtTm.substr(0, 10);
          this.ordEndTime = resUrl.data[0].ordEndDtTm.substr(11, 5);
          this.rcvMoneyStartDate = resUrl.data[0].rcvMoneyStartDtTm.substr(
            0,
            10
          );
          this.rcvMoneyStartTime = resUrl.data[0].rcvMoneyStartDtTm.substr(
            11,
            5
          );
          this.rcvMoneyEndDate = resUrl.data[0].rcvMoneyEndDtTm.substr(0, 10);
          this.rcvMoneyEndTime = resUrl.data[0].rcvMoneyEndDtTm.substr(11, 5);
          //this.deliveryStartDate = resUrl.data[0].deliveryStartDtTm.substr(
          //  0,
          //  10
          //);
          //this.deliveryStartTime = resUrl.data[0].deliveryStartDtTm.substr(
          //  11,
          //  5
          //);
          this.oldAllDateTime = this.ordStartDate + this.ordStartTime + this.ordEndDate + this.ordEndTime 
                              + this.rcvMoneyStartDate + this.rcvMoneyStartTime + this.rcvMoneyEndDate + this.rcvMoneyEndTime
                              /*+ this.deliveryStartDate + this.deliveryStartTime;*/
        }
      } catch (error) {
        console.log(error);
      }
    },
    // DateTimeを更新する
    async updateDateTime() {
      this.dialogConfirmUpdateDateTime = false;
      this.loading = true;

      this.ordStartTime = this.ordStartTime?this.ordStartTime:"";
      this.ordEndTime = this.ordEndTime?this.ordEndTime:"";
      this.rcvMoneyStartTime = this.rcvMoneyStartTime?this.rcvMoneyStartTime:"";
      this.rcvMoneyEndTime = this.rcvMoneyEndTime?this.rcvMoneyEndTime:"";
     /* this.deliveryStartTime = this.deliveryStartTime?this.deliveryStartTime:"";*/

      let dateTime = {
        ordStartDtTm: moment(
          this.ordStartDate + " " + this.ordStartTime
        ).format(),
        ordEndDtTm: moment(this.ordEndDate + " " + this.ordEndTime).format(),
        rcvMoneyStartDtTm: moment(
          this.rcvMoneyStartDate + " " + this.rcvMoneyStartTime
        ).format(),
        rcvMoneyEndDtTm: moment(
          this.rcvMoneyEndDate + " " + this.rcvMoneyEndTime
          ).format(),
        //deliveryStartDtTm: moment(
        //  this.deliveryStartDate + " " + this.deliveryStartTime
        //).format(),
        bussinessCd: this.bussinessCd,
        updateUserCd: this.currentUser,
        createUserCd: this.currentUser,
      };
      const config = {
        method: "post",
        url: "api/DatetimeSetting/updateDateTime",
        data: dateTime,
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
        await this.getDateTime();
        this.loading = false;
      }
    },
  },
};
</script>
<style scoped>
.item {
  background-color: white !important;
}
.disable-input {
  cursor: default;
  background-color: #e9ecef !important;
}
</style>
