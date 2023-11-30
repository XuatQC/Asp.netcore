<template>
  <v-app>
    <div v-if="isHiddenPagination">
      <v-pagination
        @input="ChangePage()"
        v-model="page"
        :total-visible="7"
        :length="totalpage"
      ></v-pagination>
    </div>
  </v-app>
</template>

<script>
export default {
  props: {
    apiUrl: String,
    pageSize: Number,
    bussinessCd: String,
    dataSearch: Object,
  },
  data() {
    return {
      totalpage: 0,
      page: 1,
      dataTable: [],
      isHiddenPagination: true,
    };
  },
  async created() {
    this.ChangePage();
  },
  methods: {
    async ChangePage() {
      const url = this.apiUrl;
      // call api to get total page
      const config = {
        method: "get",
        url: url,
        data:this.dataSearch,
        params: {
          currentpage: this.page,
          pageSize: this.pageSize,
          bussinessCd: this.bussinessCd,
        },
      };
      const listDataRespone = await require("axios")(config);
      this.dataTable = listDataRespone.data.data;

      this.checkHiddenPagination(listDataRespone);
      this.totalpage = Math.ceil(listDataRespone.data.count / this.pageSize);
      
      this.$emit("dataWasUpdated", listDataRespone.data.data);
    },
    checkHiddenPagination(listDataRespone) {
      this.isHiddenPagination =
        this.pageSize >= listDataRespone.data.count ? false : true;
    },
  },
};
</script>