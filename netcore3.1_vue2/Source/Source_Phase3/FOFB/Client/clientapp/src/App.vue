<template>
  <div id="app">
    <main :class="[pageStyle]">
      <div v-if="isFullPage">
          <router-view/>
      </div>
      <div v-else-if="isAdminPage">
           <AminBase/>
      </div>
      <div v-else>
           <ClientBase/>
      </div>
    </main>
  </div>
</template>

<script>
import ClientBase from "@/components/ClientBase"
import AminBase from "@/components/AdminBase"

export default {
  name: "App",
  components: {
    ClientBase,
    AminBase
  },
   data(){
     return {
     }
   },
  computed: {
    isAdminPage: function () {
        return this.$route.meta.forAdmin
    },
    isFullPage: function () {
      if(this.$route.meta.fullPage !== undefined){
         return this.$route.meta.fullPage
      }
      return false
    },
    pageStyle: function () {
        return this.isAdminPage ? '': 'main-fix'
    }
  },
  methods: {
    toggleBodyClass(addRemoveClass, className) {
      const el = document.body;

      if (addRemoveClass === 'addClass') {
        el.classList.add(className);
      } else {
        el.classList.remove(className);
      }
    },
  },
  mounted() {
    if(this.isAdminPage){
      this.toggleBodyClass('addClass', 'admin-bg');
    }else{
      this.toggleBodyClass('addClass', 'front-bg');
    }
  },
  destroyed() {
    this.toggleBodyClass('removeClass', 'front-bg');
    this.toggleBodyClass('removeClass', 'admin-bg');
  },
};
</script>
<style lang="scss">
@import "assets/css/base.scss";
.admin-bg{
  background-color: white;
}
</style>
