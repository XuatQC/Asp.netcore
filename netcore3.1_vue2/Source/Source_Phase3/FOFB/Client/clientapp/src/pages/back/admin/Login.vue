<template>
  <form @submit.prevent="login" @keydown.enter.prevent="">
    <div>
      <VueElementLoading
        :active="loadding"
        spinner="ring"
        color="var(--success)"
      />
      <div class="background-login">
        <div class="d-flex h-100 justify-content-center align-items-center">
          <b-col md="8" class="mx-auto app-login-box">
            <!--Logo-->
            <div class="d-flex justify-content-center"></div>
            <div class="modal-login-box w-100 mx-auto">
              <div class="modal-content">
                <div class="modal-body">
                  <div class="h5 logo-area modal-title text-center">
                    <h4 class="mt-2 img-logo-area">
                      <img
                        style="width: 200px"
                        src="../../../assets/images/logo/logo-FO-text-color.png"
                      />
                    </h4>
                  </div>
                  <!-- User code -->
                  <b-form-group id="userCodeGroup" label-for="userCode">
                    <b-form-input
                      id="userCode"
                      placeholder="ユーザコード"
                      name="userCode"
                      v-model="userCode"
                      class="input-control"
                      v-validate="'required'"
                    ></b-form-input>
                    <span
                      class="invalid-feedback d-block"
                      v-if="errors.has('userCode') == true"
                      >{{ errors.first("userCode") }}</span
                    >
                  </b-form-group>
                  <!-- Password -->
                  <!-- <font-awesome-icon :icon="['fas', 'lock']" /> -->
                  <b-form-group id="passwordGroup" label-for="password">
                    <b-form-input
                      @keyup.enter="login()"
                      id="password"
                      class="input-control"
                      type="password"
                      placeholder="パスワード"
                      name="password"
                      v-model="password"
                      v-validate="'required'"
                    ></b-form-input>
                    <span
                      class="invalid-feedback d-block"
                      v-if="errors.has('password') == true"
                      >{{ errors.first("password") }}</span
                    >
                  </b-form-group>
                  <b-form-group id="autoLoginGroup" label-for="autoLogin">
                    <!-- checkbox auto login -->
                    <b-form-checkbox
                      name="autoLogin"
                      id="autoLogin"
                      v-model="autoLogin"
                      value="1"
                      unchecked-value="0"
                      >次回から自動的にログイン</b-form-checkbox
                    >
                  </b-form-group>
                  <strong
                    ><span class="text-danger d-flex justify-content-center">{{
                      errorMsg
                    }}</span></strong
                  >
                  <b-form-group id="btnLoginGroup" label-for="btnLogin">
                    <b-button
                      class="btn-login"
                      type="submit"
                      style="width: 100%"
                      size="lg"
                      :loading="clickedBtlSave"
                      >ログイン</b-button
                    >
                  </b-form-group>
                </div>
              </div>
            </div>
          </b-col>
        </div>
      </div>
    </div>
  </form>
</template>

<script>
export default {
  name: "Login",
  components: {},
  data: () => ({
    userCode: "",
    password: "",
    errorMsg: "",
    autoLogin: 0,
    loadding: false,
    clickedBtlSave: false,
  }),
  methods: {
    // _________________________ Login ________________________________
    async login() {
      const validate = await this.$validator.validateAll()
      if (validate) {
        const muserLogin = {
          UserCd: this.userCode,
          Password: this.password,
        };

        const isAutoLogin = (this.autoLogin === this.AUTO_LOGIN);
        try {
          const config = {
            method: "post",
            url: "api/MuserLogin/Login",
            params: {
              autoLogin: isAutoLogin,
            },
            data: muserLogin,
          };
          this.loadding = true
          const loginDataRespone = await require("axios")(config);
          if (loginDataRespone.data !== null) {
            if (loginDataRespone.data.error === undefined) {
              this.setUserData(loginDataRespone.data.userData);
              this.goToNextRoute(loginDataRespone.data);
              sessionStorage.setItem("isShowPopup", loginDataRespone.data.isShowPopup);
              sessionStorage.setItem("isChangePass", loginDataRespone.data.isChangePass);
            } else {
              this.errorMsg = loginDataRespone.data.error;
            }
          }
        } catch (error) {
          console.log(error);
        } finally{
          this.loadding = false
        }
      }
    },
    // _________________________ End Login ________________________________
    // _________________________ Change route _____________________________
    goToNextRoute(loginDataRespone){
      if (loginDataRespone.userData.department === this.DEPART_ADMIN_TYPE) {
           // this.$router.push("/admin/manage-order").catch(() => {});
        if (
          loginDataRespone.isDefaultPw !== undefined &&
          loginDataRespone.isDefaultPw === true
        ) {
          sessionStorage.setItem("isAdminDefaultPw", true);
        }
           this.setNextRoute(loginDataRespone.isDefaultPw, 'manage-order')
        } else if (loginDataRespone.userData.department === this.DEPART_SHOP_TYPE) {
           //this.$router.push("/shop/home").catch(() => {});
           this.setNextRoute(loginDataRespone.isDefaultPw, 'shopHome')
      }
    },
    setNextRoute(isDefaultPw, routeName){
      if (isDefaultPw !== undefined && isDefaultPw === true) {
          // go to home page with alert to change password
          this.$router.push({
            name: routeName,
            params: {
              oldPassword: true
            }
          })
        } else {
          // go to home page
          this.$router.push({name: routeName}).catch(() => {})
      }
    },
    // _________________________ End Change route _________________________
    // _________________________ Set User infor ___________________________
    setUserData(ueserData){
      this.$cookies.set("userData", ueserData);
      this.$cookies.set("rootBussinessCd", ueserData.bussinessCd);
      this.$cookies.set("token", ueserData.accessToken);
    },
    // _________________________ End Set User infor _________________________
    // _________________________ Check error & show message error ___________
    CheckErrorMsg () {
      if (this.$route.params.errorType !== undefined) {
        switch (this.$route.params.errorType) {
          case 'USER_ERR':
            if (this.$route.params.errorMsg !== undefined) {
             this.errorMsg = this.$route.params.errorMsg
              this.sleep(2000)
            }
            break
          case 'SESSION_EXPIRED':
            this.errorMsg = this.msgSessionExpire + this.msgRelogin
            this.sleep(2000)
            break
          case 'USER_CHANGED':
            this.errorMsg = this.msgUserInforChanged + this.msgRelogin
              this.sleep(2000)
            break
          case 'AUTO_LOGIN_EXPIRED':
            this.errorMsg =  this.msgSessionExpire + this.msgRelogin
            this.sleep(2000)
            break
          case 'OUT_OF_PERIOD':
            if (this.$route.params.errorMsg !== undefined) {
              this.errorMsg = this.$route.params.errorMsg
              this.sleep(2000)
            }
            break
          case 'ACCESS_DENIED':
            if (this.$route.params.errorMsg !== undefined) {
              this.errorMsg = this.$route.params.errorMsg
              this.sleep(2000)
            }
            break
        }
        this.clearData()
      }
    }
    // ________________________ End Check error & show message error ___________
  },
  created() {
    this.CheckErrorMsg()
  },
};
</script>

<style lang="scss" scoped>

</style>
