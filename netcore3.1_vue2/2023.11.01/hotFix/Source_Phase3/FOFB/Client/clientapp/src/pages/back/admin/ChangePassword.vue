<template>
    <div>
     <div class="main-card">
      <div class="card card-body">
        <div style="max-width: 400px">
          <div class="row">
            <span style="padding-left: 12px; padding-bottom: 10px">
              <strong>ユーザーコード</strong>
            </span>
          </div>
          <div class="row">
            <b-form-group class="col" label="">
              <div class="input-group">
                <b-form-input
                  maxlength="16"
                  disabled="disabled"
                  class="input_style"
                  v-model="userCode"
                  name="userCode"
                  id="userCode"
                ></b-form-input>
              </div>
            </b-form-group>
          </div>
          <div class="row">
            <span style="padding-left: 12px; padding-bottom: 10px">
              <strong>現在のパスワード</strong>
              <strong class="text-danger">*</strong>
            </span>
          </div>
          <div class="row">
          <b-form-group class="col" label="">
            <div class="input-group">
              <b-form-input
                maxlength="16"
                @keyup.enter="changePassword()"
                type="password"
                :class="errors.has('oldPassword') ? 'is-invalid' : '' "
                v-model="oldPassword"
                v-validate="'required'"
                name="oldPassword"
                id="oldPassword"
              ></b-form-input>
          </div>
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('oldPassword') === true"
          >{{errors.first('oldPassword')}}</span>
        </b-form-group>
          </div>
          <div class="row">
            <span style="padding-left: 12px; padding-bottom: 10px">
              <strong>新しいパスワード</strong>
              <strong class="text-danger">*</strong>
            </span>
          </div>
          <div class="row">
            <b-form-group class="col" label="">
              <div class="input-group">
                <b-form-input
                  maxlength="16"
                  @keyup.enter="changePassword()"
                  type="password"
                  :class="errors.has('newPassword') ? 'is-invalid' : ''"
                  v-model="newPassword"
                  name="newPassword"
                  v-validate="'required'"
                  ref="newPassword"
                  id="newPassword"
                ></b-form-input>
              </div>
              <span
                class="invalid-feedback d-block"
                v-if="errors.has('newPassword') === true"
              >{{errors.first('newPassword')}}</span>
            </b-form-group>
          </div>
          <div class="row">
            <span style="padding-left: 12px; padding-bottom: 10px">
              <strong>新しいパスワード（確認）</strong>
              <strong class="text-danger">*</strong>
            </span>
          </div>
          <div class="row">
            <b-form-group class="col" label="">
              <div class="input-group">
                <b-form-input
                  maxlength="16"
                  @keyup.enter="showDialogConfirmChangePassword()"
                  type="password"
                  :class="errors.has('reNewPassword') ? 'is-invalid' : ''"
                  v-model="reNewPassword"
                  name="reNewPassword"
                  id="newPasswordConfirm"
                  v-validate="'required'"
                ></b-form-input>
              </div>
              <span
                class="invalid-feedback d-block"
                v-if="errors.has('reNewPassword') === true"
              >{{errors.first('reNewPassword')}}</span>
            </b-form-group>
          </div>
          <div class="row">
            <div class="col">
              <b-button @click="showDialogConfirmChangePassword()" variant="success" class="ml-auto d-block">保存</b-button>
            </div>
        </div>
      </div>
      </div>
     </div>
      <!--  dialog confirm change Password start -->
    <v-dialog
      v-model="dialogConfirmUpdatePass"
      max-width="420px"
      content-class="v-dialog-border"
    >
      <v-card>
        <v-card-text>
          <div class="text-center">
            パスワードを更新します。ログイン画面に戻ります。
          </div>
          <div>よろしいですか。</div>
        </v-card-text>
        <v-card-actions style="text-align: center; display: block">
          <v-btn class="btn_confirm_no" @click="dialogConfirmUpdatePass = false"
            >キャンセル</v-btn
          >
          <v-btn class="btn_confirm_yes" @click="changePassword()">保存</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!--  dialog confirm change Password end -->
    </div>
</template>

<script>
import moment from 'moment'

export default {
  data: () => ({
    moment: moment,
    oldPassword: '',
    newPassword: '',
    reNewPassword: '',
    mUser: {},
    userCode: '',
    dialogConfirmUpdatePass: false,

  }),
  methods: {
  // ----------------------------Function  call API---------------------------------------------
  // _______________________________ change Password _________________________________________________
    async showDialogConfirmChangePassword() {
      const isValid = await this.$validator.validateAll();
      if (isValid) {
        if (this.oldPassword === this.newPassword) {
          this.$toastr.e(this.msgPasswordSameBefore);
          await this.sleep(1000);
        } else if (this.newPassword !== this.reNewPassword) {
          this.$toastr.e(this.msgConfirmPasswordNotMatch);
          await this.sleep(1000);
        } else if (this.newPassword.length < 8) {
          this.$toastr.e(this.msgPasswordLengthNotValid);
          await this.sleep(1000);
        } else {
          this.dialogConfirmUpdatePass = true;
        }
      }
    },
    async changePassword () {
      this.mUser = this.getUserData();
      this.mUser.updateUserCd = this.getUserData().userCd;
      this.mUser.password = this.newPassword;
          const config = {
            method: 'post',
            url: 'api/MuserLogin/ChangePassword',
            params: { oldPassword: this.oldPassword },
            data: this.mUser
          }
          try {
            const res = await require('axios')(config)
            if (res.data !== null) {
              if (res.data.error === undefined) {
                this.$toastr.s(this.msgUpdateSucces);
                this.$router.push({
                  name: "manage-stock",
                });
              } else {
                this.$toastr.e(res.data.error)
              }
            } else {
              this.$toastr.e(this.msgUpdateFailed)
            }
          } catch (error) {
            this.$toastr.e(this.msgUpdateFailed)
          }
        }
  },
  created () {
    this.userCode = this.getUserData().userCd
  },
  computed: {

  }
}
</script>
<style scoped>
.input_style{
  max-width: 250px;
}
</style>
