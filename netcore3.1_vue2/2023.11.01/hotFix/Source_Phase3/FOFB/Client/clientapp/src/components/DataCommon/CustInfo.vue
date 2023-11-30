<template>
  <div class="customer-info-confirm-area">
    <div class="container">
      <h3 class="title-cust-info-confirm">■お客様情報</h3>
      <div class="cust-info-confirm-detail">
        <div class="row cust-info-confirm">
          <div class="title">
            <label>お名前(漢字)</label>
          </div>
          <div class="cust-info-value">
            <span>
              {{ dataCustInfo.kanjiFamilyName }}
              {{ dataCustInfo.kanjiFirstName }}</span
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
              >{{ dataCustInfo.kanaFamilyName }}
              {{ dataCustInfo.kanaFirstName }}</span
            >
          </div>
        </div>
        <hr class="line-info-confirm" />
        <div class="row cust-info-confirm">
          <div class="title">
            <label>住所</label>
          </div>
          <div class="cust-info-value">
            <span>〒{{ this.formatPostCode(dataCustInfo.zipCd) }}</span
            ><br />
            <span
              >{{ dataCustInfo.provinceName+dataCustInfo.adrCd1+dataCustInfo.adrCd2 }} {{ dataCustInfo.adrCd3 }}</span
            >
          </div>
        </div>
        <hr class="line-info-confirm" />
        <div class="row cust-info-confirm">
          <div class="title">
            <label>お電話番号</label>
          </div>
          <div class="cust-info-value">
            <span>{{ dataCustInfo.phoneNumber }}</span>
          </div>
        </div>
        <hr class="line-info-confirm" />
        <div class="row cust-info-confirm">
          <div class="title">
            <label>メールアドレス</label>
          </div>
          <div class="cust-info-value">
            <span>{{ dataCustInfo.mailAddress }}</span
            ><br />
            <span
              >こちらのメールアドレスにご予約確認メールが送信されます。</span
            >
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "CusInfo",
  props: {
    dataCustInfo: null,
  },
  data() {
    return {};
  },
  created() {},
  methods: {},
};
</script>

<style scoped></style>
