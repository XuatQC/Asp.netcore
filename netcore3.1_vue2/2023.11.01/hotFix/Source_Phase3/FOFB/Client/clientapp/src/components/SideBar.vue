<template>
  <div>
    <v-navigation-drawer
      app
      floating
      :mini-variant.sync="mini"
      class="sidebar-area"
    >
      <v-list dense class="menu-nav" dark>
        <v-list-item>
          <v-list-item-action>
            <!-- <v-icon @click.stop="sidebarMenu = !sidebarMenu">mdi-chevron-left</v-icon> -->
            <!-- <v-app-bar-nav-icon @click.stop="toggleMini = !toggleMini"></v-app-bar-nav-icon> -->
          </v-list-item-action>
        </v-list-item>
      </v-list>
      <!-- Dashboard -->
      <div class="title-shop-dashboard">
        <span class="icon-dashboard"></span>
        <span class="title-dashboard" style="cursor: pointer;">Dashboard</span>
      </div>
      <!-- End Dashboard -->
      <v-list>
      <!-- List Bussiness -->
      <v-list-item v-if="this.$cookies.get('rootBussinessCd') === 'null'">
        <v-list-item-content>
                <!-- 業態管理一覧-->
                <v-autocomplete class="ccb-menu-bussiness"
                  v-model="selectedBussiness"
                  :items="lstBussiness"
                  :loading="(lstBussiness.length === 0) ? true: false"
                  :disabled="(lstBussiness.length === 0) ? true: false"
                  no-data-text="データがありません。"
                  :menu-props="{ bottom: true, offsetY: true }"
                  @change="changeBussiness()" return-object>
                </v-autocomplete>
        </v-list-item-content>
      </v-list-item>
      <!-- End List Bussiness -->
      <!-- List menu item -->
      <v-list-item
          v-for="item in menuItems"
          :key="item.title"
          link
          :to="item.href"
        >
          <!-- <v-list-item-icon>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-icon> -->
          <v-list-item-content>
            <v-list-item-title class="title-menu">{{
              item.title
            }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <!-- End List menu item -->
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script>
export default {
  name: "SideBar",
  props: {
    // sidebarMenu: Boolean,
  },
  computed: {
    mini() {
      return this.$vuetify.breakpoint.smAndDown || this.toggleMini;
    },
  },
  data: () => ({
    toggleMini: false,
    userData: null,
    // items: [{ title: "Home", href: "/", icon: "mdi-home-outline" }],
    menuItems: [],
    lstBussiness: [],
    selectedBussiness: {text: '', value: ''}
  }),
  async created(){
    if(this.userData === null){
      this.userData = this.getUserData();
      if(this.$cookies.get('rootBussinessCd') === 'null'){
        this.setListBussiness()
      }
    }
    await this.setMenu()
  },
  methods: {
    //_______________ Set Menu _________________________
    async getMenu() {
      try {
        const config = {
          method: "get",
          url: "api/Menu/GetMenusByUser",
          contentType:'application/json',
          params: {
            userCd: this.userData.userCd
          }
        };
        const getMenuResRespone = await require("axios")(config)
        return getMenuResRespone.data
      } catch (error) {
        console.log(JSON.stringify(error))
      }
    },
    async getShopMenu() {
      try {
        const config = {
          method: "get",
          url: "api/Menu/GetShopMenu",
          contentType:'application/json',
          params: {}
        };
        const getMenuResRespone = await require("axios")(config)
        return getMenuResRespone.data
      } catch (error) {
        console.log(JSON.stringify(error))
      }
    },
   async setMenu(){
      //   var menuStorage = sessionStorage.getItem('menuItems')
      //  if(menuStorage !== null && 
      //   menuStorage !== undefined && 
      //   menuStorage !== 'null' &&
      //   menuStorage !== 'undefined'){
      //    this.menuItems = JSON.parse(sessionStorage.getItem('menuItems'))
      //  }else{
      //    if(this.userData.department === this.DEPART_SHOP_TYPE){
      //      this.menuItems = await this.getShopMenu()
      //    }else{
      //      this.menuItems = await this.getMenu()
      //    }
      //    sessionStorage.setItem('menuItems', JSON.stringify(this.menuItems))
      //  }
      if(this.userData.department === this.DEPART_SHOP_TYPE){
          this.menuItems = await this.getShopMenu()
        }else{
          this.menuItems = await this.getMenu()
        }
    },
    //_______________ End set Menu _________________________
    //_______________ Set Commbobox bussiness _________________________
    async getListBussiness () {
      try {
        const config = {
          method: 'get',
          url: 'api/MBussiness/GetAll'
        }
        const getListBussinessRespone = await require("axios")(config)
        return getListBussinessRespone.data
      } catch {
        return null
      }
    },
    async setListBussiness(){
      if(sessionStorage.getItem('listBussiness') !== null){
          this.lstBussiness = JSON.parse(sessionStorage.getItem('listBussiness'))
      }else{
        var lstBussinessResopne = await this.getListBussiness()
        if(lstBussinessResopne !== null && lstBussinessResopne !== undefined){
          this.lstBussiness = []
          for (var i = 0; i < lstBussinessResopne.length; i++) {
              this.lstBussiness.push({
                value: lstBussinessResopne[i].bussinessCd,
                text: lstBussinessResopne[i].bussinessName
              })
          }
          sessionStorage.setItem('listBussiness', JSON.stringify(this.lstBussiness))
        }
      }
      if(this.lstBussiness !== undefined && this.lstBussiness.length > 0){
         this.setSelectedBussiness()
      }
    },
    setSelectedBussiness(){
      if(this.userData.bussinessCd !== null){
          this.selectedBussiness.value = this.userData.bussinessCd 
        }else{
          this.selectedBussiness.value = this.lstBussiness[0].value
          this.setBussinesToUser()
        }
    },
    //_______________ End Set Commbobox bussiness _________________________
    //_______________ Changed Commbobox bussiness event _________________________
    changeBussiness(){
    if (this.userData.bussinessCd !== this.selectedBussiness.value) {
        this.setBussinesToUser()
      }
    },
    setBussinesToUser(){
      this.userData.bussinessCd = this.selectedBussiness.value
      this.$cookies.remove('userData')
      this.$cookies.set('userData', this.userData)
      this.$router.go(this.$router.currentRoute)
    }
    //_______________ End changed Commbobox bussiness event _________________________
  },
  mounted() {},
  destroyed() {},
};
</script>
<style lang="scss">
.sidebar-area{
  margin-top: 85px;
  background: #5bc1d8 !important;
  color: white !important;
  // width: 280px !important;
}
a:hover{
  text-decoration: none !important;
}
.v-application--is-ltr .v-list-item__action:first-child, 
.v-application--is-ltr .v-list-item__icon:first-child {
    margin-right: 20px !important;
}
.title-menu{
    font-size: 16px !important;
    font-weight: bold;
    color: white !important;
}
</style>
