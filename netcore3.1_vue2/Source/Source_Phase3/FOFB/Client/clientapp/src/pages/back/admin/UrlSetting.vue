<template>
  <div>
    <VueElementLoading
      :active="loading"
      spinner="ring"
      color="var(--success)"
    />
    <br />
    <!--- Url -->
    <b-row>
      <b-col lg="2" md="2" xl="1">
        <label class="col-md-12"
          ><strong>URL</strong><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="2" lg="5" md="12">
        <b-form-input
          name="txtUrl"
          type="text"
          v-validate="'required'"
          @input="checkInput()"
          v-model="topUrl"
          placeholder=""
          maxlength="50"
          autocomplete="off"
        />
        <span class="invalid-feedback d-block">{{
          errors.first("txtUrl")
        }}</span>
        <span
          v-if="isWrongURL && topUrl !== ''"
          class="invalid-feedback d-block"
          >{{ this.msgErrURL }}</span
        >
      </b-col>
      <b-col>
        <button
          @click="showDialogConfirmUpdateUrl()"
          style="width: 90px"
          class="btn btn-danger"
        >
          保存
        </button>
      </b-col>
    </b-row>
    <b-row>
      <b-col lg="2" md="2" xl="1"> </b-col>
      <b-col>
        <label class="col-md-12">
          <span
            ><a :href="currUrl + '/' + updUrl" target="_blank">{{
              currUrl + "/" + updUrl
            }}</a></span
          >
        </label>
      </b-col>
    </b-row>
    <!--  dialog confirm update Url start -->
    <v-dialog
      v-model="dialogConfirmUpdateUrl"
      max-width="401px"
      content-class="v-dialog-border"
    >
      <v-card>
        <v-card-text>
          <div class="text-center">変更します。よろしいですか。</div>
        </v-card-text>
        <v-card-actions style="text-align: center; display: block">
          <v-btn class="btn_confirm_no" @click="dialogConfirmUpdateUrl = false"
            >キャンセル</v-btn
          >
          <v-btn
            class="btn_confirm_yes"
            :loading="loadingBtn"
            @click="saveUrl()"
            >保存</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!--  dialog confirm update Url end -->
  </div>
</template>

<script>
export default {
  data() {
    return {
      loading: false,
      isWrongURL: false,
      loadingBtn: false,
      dialogConfirmUpdateUrl: false,
      bussinessCd: "",
      topUrl: "",
      updUrl: "",
      currUrl: window.location.origin,
    };
  },
  components: {},
  computed: {},
  async created() {
    this.bussinessCd = this.getUserData().bussinessCd;

    await this.getUrl();
  },
  methods: {
    checkInput() {
      this.isWrongURL = !new RegExp(this.REGEX_URL).test(this.topUrl);
    },
    async getUrl() {
      this.loading = true;
      const config = {
        method: "get",
        url: "api/Urlsetting/GetUrlByBussinessCd",
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      try {
        const resUrl = await require("axios")(config);
        if (resUrl.data !== null && resUrl.data !== undefined) {
          this.topUrl = resUrl.data[0].url;
          this.updUrl = this.topUrl;
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loading = false;
      }
    },
    // show Dialog Confirm Update Url
    showDialogConfirmUpdateUrl() {
      const arrayItemErr = this.$validator.errors.items;
      if (arrayItemErr.length === 0) {
        if (this.topUrl == this.updUrl) {
          this.$toastr.w(this.msgNotthingChanged);
          return;
        } else if (this.isWrongURL) {
          return;
        }
        this.dialogConfirmUpdateUrl = true;
      }
    },
    async saveUrl() {
      this.loadingBtn = true;
      const urlSetting = {
        BussinessCd: this.bussinessCd,
        Url: this.topUrl,
        CreateUserCd: this.getUserData().userCd,
        UpdateUserCd: this.getUserData().userCd,
      };
      const config = {
        method: "post",
        url: "api/Urlsetting/SaveUrl",
        data: urlSetting,
      };
      try {
        const resUrl = await require("axios")(config);
        if (resUrl.data !== false && resUrl.data !== undefined) {
          if (resUrl.data.error === undefined) {
            this.$toastr.s(this.msgUpdateSucces);
            this.getUrl();
          } else {
            this.$toastr.e(resUrl.data.error);
          }
        } else {
          this.$toastr.e(this.msgUpdateFailed);
        }
      } catch (error) {
        this.$toastr.e(this.msgUpdateFailed);
        console.log(error);
      } finally {
        this.sleep(2000);
        this.dialogConfirmUpdateUrl = false;
        this.loadingBtn = false;
      }
    },
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
  text-align: center;
  padding-top: unset;
  padding-bottom: unset;
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
.title-mailbouce {
  color: #040404;
  font-weight: bold;
}

#padding-readonly {
  padding-left: 0;
}
</style>
