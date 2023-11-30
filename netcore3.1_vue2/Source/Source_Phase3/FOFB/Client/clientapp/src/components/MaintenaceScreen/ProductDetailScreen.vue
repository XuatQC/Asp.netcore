<template>
  <div>
    <!--- 商品詳細情報 -->
    <b-row class="mb-4">
      <b-col lg="1" md="12" xl="2">
        <label class="col-md-12"
          ><strong>商品詳細情報</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="6">
        <ckeditor
          :readOnly="isUserReadOnly"
          type="classic"
          id="txtContentArea1"
          name="txtContentArea1"
          v-validate="'required'"
          v-model="textContent.textArea1"
          :config="editorConfig"
        ></ckeditor>
        <span
          class="invalid-feedback d-block"
          v-if="errors.has('txtContentArea1') == true"
          >{{ errors.first("txtContentArea1") }}</span
        >
      </b-col>
    </b-row>
    <!--- 注意事項 -->
    <b-row class="mb-4">
      <b-col lg="1" md="12" xl="2">
        <label class="col-md-12"
          ><strong>注意事項</strong><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="6">
        <ckeditor
          :readOnly="isUserReadOnly"
          type="classic"
          id="txtArea2"
          name="txtArea2"
          v-validate="'required'"
          v-model="textContent.textArea2"
          :config="editorConfig"
        ></ckeditor>
        <span
          class="invalid-feedback d-block"
          v-if="errors.has('txtArea2') == true"
          >{{ errors.first("txtArea2") }}</span
        >
      </b-col>
    </b-row>
    <!--- 入力画面に進むボタン名-->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>入力画面に進むボタン名</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="4" md="12">
        <b-form-group>
          <b-form-input
            :readonly="isUserReadOnly"
            type="text"
            name="txtBtn1"
            v-validate="'required'"
            v-model="textContent.textButton1"
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtBtn1') == true"
            >{{ errors.first("txtBtn1") }}</span
          >
        </b-form-group>
      </b-col>
    </b-row>
    <!--- ボタンの注意事項 -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>ボタンの注意事項</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="4" md="12">
        <b-form-group>
          <b-form-input
            :readonly="isUserReadOnly"
            type="text"
            name="txtNortify2"
            v-validate="'required'"
            v-model="textContent.textNortify2"
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtNortify2') == true"
            >{{ errors.first("txtNortify2") }}</span
          >
        </b-form-group>
      </b-col>
    </b-row>
    <!---  前画面に戻るボタン名（数量リセット) -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong> 前画面に戻るボタン名（数量リセット)</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="4" md="12">
        <b-form-group>
          <b-form-input
            :readonly="isUserReadOnly"
            type="text"
            name="txtBtn2"
            v-validate="'required'"
            v-model="textContent.textButton2"
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtBtn2') == true"
            >{{ errors.first("txtBtn2") }}</span
          >
        </b-form-group>
      </b-col>
    </b-row>
    <!--- 前画面に戻るボタン名（数量保存) -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>前画面に戻るボタン名（数量保存)</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="4" md="12">
        <b-form-group>
          <b-form-input
            :readonly="isUserReadOnly"
            type="text"
            name="txtTitle2"
            v-validate="'required'"
            v-model="textContent.textTitle2"
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtTitle2') == true"
            >{{ errors.first("txtTitle2") }}</span
          >
        </b-form-group>
      </b-col>
    </b-row>
  </div>
</template>

<script>
export default {
  name: "screenDetail",
  props: {
    textContent: null,
    lstImage: null,
  },
  data() {
    return {};
  },
  created() {},
  methods: {
    openImageDialog() {
      document.getElementById("btn-dialog-productDetail").click();
    },
  },
};
</script>

<style scoped>
.static-area .static-area-wrap {
  font-weight: 350 !important;
}
</style>
