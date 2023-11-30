<template>
  <div>
    <!--- TOP画像 -->
    <b-row class="mb-4" v-if="urlImgTop.imageUrl !== ''">
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>TOP画像</strong><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="4" md="6">
        <img width="500px" :src="urlImgTop.imageUrl" />
      </b-col>
      <b-col xl="2" lg="2" md="6">
        <button
          v-if="!isUserReadOnly"
          @click="openFileImgTop()"
          class="btn btn-success"
        >
          変更
        </button>
      </b-col>
    </b-row>
    <!--- 注意事項の表示方法(画像/テキスト) -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>注意事項の表示方法(画像/テキスト)</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="6" md="12">
        <v-switch
          style="margin: unset"
          v-model="textContent.buttonFlg"
          :label="textContent.buttonFlg === false ? '画像' : 'テキスト'"
        ></v-switch>
      </b-col>
    </b-row>
    <!--- 注意事項の画像 -->
    <b-row
      class="mb-4"
      v-if="urlImgBottom1.imageUrl !== '' && !textContent.buttonFlg"
    >
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>注意事項の画像1</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="4" md="6">
        <img width="500px" :src="urlImgBottom1.imageUrl" />
      </b-col>
      <b-col xl="2" lg="2" md="6">
        <button
          v-if="!isUserReadOnly"
          @click="openFileImgBottom1()"
          class="btn btn-success"
        >
          変更
        </button>
      </b-col>
    </b-row>
    <!--- 注意事項の画像 -->
    <b-row
      class="mb-4"
      v-if="urlImgBottom2.imageUrl !== '' && !textContent.buttonFlg"
    >
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>注意事項の画像2</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="4" md="6">
        <img width="500px" :src="urlImgBottom2.imageUrl" />
      </b-col>
      <b-col xl="2" lg="2" md="6">
        <button
          v-if="!isUserReadOnly"
          @click="openFileImgBottom2()"
          class="btn btn-success"
        >
          変更
        </button>
      </b-col>
    </b-row>
    <!--- 注意事項 -->
    <b-row class="mb-4" v-if="textContent.buttonFlg">
      <b-col lg="1" md="12" xl="2">
        <label class="col-md-12"
          ><strong>注意事項</strong><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="6">
        <span hidden="true">
          {{ (editorConfig.height = 500) }}
        </span>
        <ckeditor
          :readOnly="isUserReadOnly"
          type="classic"
          id="txtContentArea1"
          name="txtContentArea1"
          v-validate="'required'"
          v-model="textContent.textNortify1"
          :config="editorConfig"
        ></ckeditor>
        <span
          class="invalid-feedback d-block"
          v-if="errors.has('txtContentArea1') == true"
          >{{ errors.first("txtContentArea1") }}</span
        >
      </b-col>
    </b-row>
  </div>
</template>

<script>
export default {
  name: "screenTop",
  props: {
    textContent: null,
    urlImgTop: Object,
    urlImgBottom1: Object,
    urlImgBottom2: Object,
  },
  data() {
    return {};
  },
  async created() {},
  methods: {
    openFileImgTop() {
      document.getElementById("imgTop").click();
    },
    openFileImgBottom1() {
      document.getElementById("imgBottom1").click();
    },
    openFileImgBottom2() {
      document.getElementById("imgBottom2").click();
    },
  },
};
</script>

<style scoped>
.static-area .static-area-wrap {
  font-weight: 350 !important;
}
</style>
