<template>
    <v-app>
      <div>
         <!-- <div class="title-header sticky-top">
           <div style="height: 85px; background: white"> </div>
         </div> -->
        <HeadNav/>
         <!-- <v-app-bar app class="menu-nav" elevation="0"> -->
            <!--humberger-->
            <!-- <v-app-bar-nav-icon @click.stop="sidebarMenu = !sidebarMenu"></v-app-bar-nav-icon> -->
        <!-- </v-app-bar> -->
        
        <!--menu-->
        <!-- <SideBar :sidebarMenu="isToggleSideBar"/> -->
        <SideBar/> 
        <!--Template container-->
        <v-content>
              <router-view/>
        </v-content>
      </div>
    </v-app>
</template>

<script>
 import HeadNav from "@/components/AdminHeader"
import SideBar from "@/components/SideBar"

export default {
  name: 'AdminBase',
  components:{
    HeadNav,
    SideBar
  },
  // computed: {
  //   isToggleSideBar() {
  //     return this.sidebarMenu;
  //   },
  // },
  // data: () => ({
  //     sidebarMenu: true
  // }),

}
</script>

<style>
.menu-nav{
  background: #5bc1d8 !important;
  height: 64px;
}
</style>
