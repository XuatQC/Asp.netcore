<template>
  <div>
    <VueElementLoading
      :active="loading"
      spinner="ring"
      color="var(--success)"
    />
    <!-- Title Start -->
    <div class="title-rules">
      <div class="container">
        <!-- Text for title page rules -->
        <h5 class="title-detail">{{ pageTitle }}</h5>
      </div>
    </div>
    <!-- Title End -->
    <!-- Rules Content Start -->
    <div class="rules-content">
      <!-- Text for content page rules -->
      <div class="container" v-html="pageContent"></div>
    </div>
    <!-- Rules Content End -->
    <!-- Button Group Area Start -->
    <div class="button-back-top-area">
      <div class="container">
        <div class="button-back-top">
          <!-- Text for button back to Top -->
          <button @click="routerToTop()" type="submit" class="btn btn-back-top">
            {{ textBtnRouterTop }}
          </button>
        </div>
      </div>
    </div>
    <!-- Button Group Area End -->
  </div>
</template>

<script>
export default {
  data: () => ({
    loading: null,
    bussinessCd: "",
    routerTop: "",
    pageTitle: "",
    pageContent: "",
    textBtnRouterTop: "",
  }),
  async created() {
    this.loading = true;
    this.bussinessCd = localStorage.getItem("bussinessCd");
    if (this.bussinessCd != null && this.bussinessCd != undefined) {
      // 画面のConfigデータを取得する
      await this.getConfig(this.bussinessCd);

      // Topへのルーターを設定する
      this.setRouterTop();
    } else {
      this.$router.push({ name: "top" });
    }

    this.loading = false;
  },
  methods: {
    // 画面のConfigデータを取得する
    async getConfig(bussinessCd) {
      try {
        var configScreen = await this.getConfigScreen(
          this.SCREEN_RULES,
          bussinessCd
        );

        if (configScreen != null && configScreen.length > 0) {
          this.pageTitle = configScreen[0].textTitle1;
          this.pageContent = configScreen[0].textArea1;
          this.textBtnRouterTop = configScreen[0].textButton1;
        }
      } catch (error) {
        console.log(error);
      }
    },
    // Topへのルーターを設定する
    setRouterTop() {
      if (localStorage.getItem("routerTop") != null) {
        this.routerTop = localStorage.getItem("routerTop");
      } else {
        this.$router.push({ name: "top" });
      }
    },
    // TOP画面に戻る
    routerToTop() {
      this.$router.push({ path: this.routerTop });
    },
  },
};
</script>

<style></style>
