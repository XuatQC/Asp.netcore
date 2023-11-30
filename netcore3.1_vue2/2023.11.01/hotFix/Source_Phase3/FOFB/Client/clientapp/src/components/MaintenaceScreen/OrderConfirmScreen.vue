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
    <!--- 前の画面に戻るボタン名 -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>前の画面に戻るボタン名</strong
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
    <!--- 確定ボタン名 -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>確定ボタン名</strong
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
  </div>
</template>

<script>
export default {
  name: "screenOrderConfirm",
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
