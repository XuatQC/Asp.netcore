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
    <!--- 内容 -->
    <b-row class="mb-4">
      <b-col lg="1" md="12" xl="2">
        <label class="col-md-12"
          ><strong>内容</strong><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="6">
        <span hidden="true">
          {{ (editorConfig.height = 500) }}
        </span>
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
    <!--- TOP画面に戻るボタン名 -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>TOP画面に戻るボタン名</strong
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
  </div>
</template>

<script>
export default {
  name: "screenRules",
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
