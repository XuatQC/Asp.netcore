<template>
  <!-- Header Start -->
  <div class="title-header sticky-top">
    <div class="card-body" style="height: 85px; background: white">
      <b-row>
        <b-col xl="11" lg="11" md="11">
          <b-row>
            <b-col xl="4" lg="4" md="4" style="text-align: left">
              <div class="app-header__logo__text"></div>
            </b-col>
            <b-col xl="7" lg="7" md="7" style="text-align: left"> </b-col>
          </b-row>
        </b-col>
        <b-col xl="1" lg="1" md="1" style="text-align: center">
          <button
            type="button"
            tabindex="0"
            class="btn-logout"
            @click.stop.prevent="logOut()"
          ></button>
        </b-col>
      </b-row>
    </div>
    <div class="menu-nav">
      <v-app-bar-title class="username-title">
        <v-menu
          bottom
          origin="center center"
          transition="scale-transition"
        >
        <template v-slot:activator="{ on, attrs }">
          <span v-if="isChangePass == CHANGE_PASS"
           v-bind="attrs"
            v-on="on">{{currUserName}}</span>
          <span v-else>{{currUserName}}</span>
          <v-icon v-if="isChangePass == CHANGE_PASS"
            class="text-white"
            v-bind="attrs"
            v-on="on">mdi-dots-vertical</v-icon>
        </template>
        <v-list>
          <v-list-item
            v-for="(item, i) in items"
            :key="i"
            :to="item.href"
          >
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
      </v-app-bar-title>
      <!-- <v-app-bar-nav-icon></v-app-bar-nav-icon> -->
      <!-- <v-app-bar-nav-icon @click.stop="toggleMini = !toggleMini"></v-app-bar-nav-icon> -->
      <!-- <v-app-bar-nav-icon @click.stop="sidebarMenu = !sidebarMenu"></v-app-bar-nav-icon> -->
    </div>
  </div>
  <!--  Header Area End -->
</template>

<script>
export default {
  name: "AdminHeader",
  data: () => ({
    userData: null,
    isUserNameKanji: true,
    currUserName: '',
     items: [
      { title: 'パスワード変更', href:'/admin/change-password'  },
    ],
    isChangePass: null
  }),
  async created() {
    this.isChangePass = sessionStorage.getItem('isChangePass')
    if (this.userData === null) {
      this.userData = this.getUserData();
      this.currUserName = (this.userData.userNameKanji !== null && this.userData.userNameKanji !== '') ? 
                  this.userData.userNameKanji : this.userData.userNameKana 
    }
  },
};
</script>

<style>
.btn-logout {
  background: url("../assets/images/logout.png");
  height: 38px;
  width: 36px;
  background-repeat: no-repeat;
  background-clip: border-box;
  background-size: cover;
  margin-top: 7px;
}
.title-header {
  position: sticky;
  top: 0;
  left: 0;
  align-items: center;
  color: white;
  font-size: 20px;
  font-weight: 500;
  z-index: 3001;
  margin-right: 0;
}
.app-header__logo__text {
  background: url("../assets/images/logo/logo-FO-text-color.png");
  height: 42px;
  width: 300px;
  background-repeat: no-repeat;
  background-clip: border-box;
  background-size: cover;
}

.username-title {
  margin-top: 20px;
  /* position: fixed; */
  font-weight: bold;
  display: inline-block;
  margin-left: 20px;
}
.username-title span {
  font-size: 16px !important;
  font-weight: bold;
}
.hide-userName{
  display: none
}
.show-userName{
  display: block
}
.v-app-bar-title__placeholder{
  display: none !important;
}
.v-app-bar-title__content{
  display: block !important;
  visibility: visible !important;
  position: unset !important;
}
</style>
