<template>
  <div>
    <!--- 画面タイトル -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>画面タイトル</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="6" md="12">
        <b-form-group>
          <b-form-input
            :readonly="isUserReadOnly"
            type="text"
            name="txtTitle"
            v-validate="'required'"
            v-model="textContent.textTitle1"
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtTitle') == true"
            >{{ errors.first("txtTitle") }}</span
          >
        </b-form-group>
      </b-col>
    </b-row>
    <!--- 完了画面説明 -->
    <b-row class="mb-4">
      <b-col lg="1" md="12" xl="2">
        <label class="col-md-12"
          ><strong>完了画面説明</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="6">
        <ckeditor
          :readOnly="isUserReadOnly"
          type="classic"
          name="txtContentArea1"
          v-validate="'required'"
          id="txtContentArea1"
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
    <!--- お知らせ項目１ タイトル -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>お知らせ項目１ タイトル</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="6" md="12">
        <b-form-group>
          <b-form-input
            :readonly="isUserReadOnly"
            type="text"
            name="txtTitle2"
            v-validate="'required'"
            v-model="textContent.textTitle2"
            :disabled="textContent.textTitle2 === null ? true : false"
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtTitle2') == true"
            >{{ errors.first("txtTitle2") }}</span
          >
        </b-form-group>
      </b-col>
    </b-row>
    <!--- お知らせ項目１ 内容 -->
    <b-row class="mb-4">
      <b-col lg="1" md="12" xl="2">
        <label class="col-md-12"
          ><strong>お知らせ項目１ 内容</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="6">
        <ckeditor
          :readOnly="isUserReadOnly"
          type="classic"
          id="txtNortify1"
          name="txtNortify1"
          v-validate="'required'"
          v-model="textContent.textNortify1"
          :config="editorConfig"
        ></ckeditor>
        <span
          class="invalid-feedback d-block"
          v-if="errors.has('txtNortify1') == true"
          >{{ errors.first("txtNortify1") }}</span
        >
      </b-col>
    </b-row>
    <!--- お知らせ項目２ タイトル -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>お知らせ項目２ タイトル</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="6" md="12">
        <b-form-group>
          <b-form-input
            :readonly="isUserReadOnly"
            type="text"
            name="txtTitle3"
            v-validate="'required'"
            v-model="textContent.textTitle3"
            :disabled="textContent.textTitle3 === null ? true : false"
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtTitle3') == true"
            >{{ errors.first("txtTitle3") }}</span
          >
        </b-form-group>
      </b-col>
    </b-row>
    <!--- お知らせ項目２ 内容 -->
    <b-row class="mb-4">
      <b-col lg="1" md="12" xl="2">
        <label class="col-md-12"
          ><strong>お知らせ項目２ 内容</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="6">
        <ckeditor
          :readOnly="isUserReadOnly"
          type="classic"
          id="txtNortify2"
          name="txtNortify2"
          v-validate="'required'"
          v-model="textContent.textNortify2"
          :config="editorConfig"
        ></ckeditor>
        <span
          class="invalid-feedback d-block"
          v-if="errors.has('txtNortify2') == true"
          >{{ errors.first("txtNortify2") }}</span
        >
      </b-col>
    </b-row>
    <!--- 閉じるボタン名 -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>閉じるボタン名</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="4" md="12">
        <b-form-group>
          <b-form-input
            :readonly="isUserReadOnly"
            type="text"
            name="txtBtn"
            v-validate="'required'"
            v-model="textContent.textButton1"
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtBtn') == true"
            >{{ errors.first("txtBtn") }}</span
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
            name="txtBtn2"
            v-validate="'required'"
            v-model="textContent.textButton2"
            :disabled="textContent.textButton2 === null ? true : false"
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtBtn2') == true"
            >{{ errors.first("txtBtn2") }}</span
          >
        </b-form-group>
      </b-col>
    </b-row>
  </div>
</template>

<script>
export default {
  name: "screenOrderFinish",
  props: {
    textContent: null,
  },
  data() {
    return {};
  },
  created() {},
  methods: {},
};
</script>

<style scoped></style>
