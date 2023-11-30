<template>
  <div class="customer-info-confirm-area">
    <div class="container">
      <h3 class="title-cust-info-confirm">■配送</h3>
      <div class="cust-info-confirm-detail">
        <div class="row cust-info-confirm">
          <div class="title">
            <label>お名前(漢字)</label>
          </div>
          <div class="cust-info-value">
            <span>
              {{ custRecieveInfo.kanjiFamilyName }}
              {{ custRecieveInfo.kanjiFirstName }}</span
            >
          </div>
        </div>
        <hr class="line-info-confirm" />
        <div class="row cust-info-confirm">
          <div class="title">
            <label>お名前(かな)</label>
          </div>
          <div class="cust-info-value">
            <span
              >{{ custRecieveInfo.kanaFamilyName }}
              {{ custRecieveInfo.kanaFirstName }}</span
            >
          </div>
        </div>
        <hr class="line-info-confirm" />
        <div class="row cust-info-confirm">
          <div class="title">
            <label>住所</label>
          </div>
          <div class="cust-info-value">
            <span>〒{{ this.formatPostCode(custRecieveInfo.zipCd) }}</span
            ><br />
            <span
              >{{ custRecieveInfo.provinceName+custRecieveInfo.adrCd1+custRecieveInfo.adrCd2 }} {{ custRecieveInfo.adrCd3 }}</span
            >
          </div>
        </div>
        <hr class="line-info-confirm" />
        <div class="row cust-info-confirm">
          <div class="title">
            <label>お電話番号</label>
          </div>
          <div class="cust-info-value">
            <span>{{ custRecieveInfo.phoneNumber }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "CusReceiveInfo",
  props: {
    custRecieveInfo: null,
  },
  data() {
    return {};
  },
  created() {
  },
  methods: {},
};
</script>

<style scoped>
</style>
