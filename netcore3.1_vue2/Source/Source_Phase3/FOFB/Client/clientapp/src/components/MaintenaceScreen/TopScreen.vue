<template>
  <div>
    <!--- メインビジュアル画像 -->
    <b-row class="mb-4">
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>メインビジュアル画像</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col v-if="lstImage.length < 4" xl="10" lg="2" md="6">
        <img
          width="200px"
          style="margin-right: 30px"
          v-for="image in this.lstImage"
          :src="image.imageUrl"
          :key="image.dspIndex"
        />
        <button
          v-if="!isUserReadOnly"
          @click="openImageDialog()"
          class="btn btn-success"
        >
          変更
        </button>
      </b-col>
      <b-col v-else xl="10" lg="2" md="6">
        <div class="row">
          <div class="col-lg-4">
            <section style="float: left; margin: unset" class="part-select">
              <div class="container" style="margin: unset">
                <div class="row">
                  <div class="group-image" style="margin: unset">
                    <div hidden class="Large-image">
                      <div class="img-active">
                        <VueSlickCarousel
                          ref="c1"
                          :asNavFor="$refs.c2"
                          v-if="this.lstImage !== null"
                        >
                          <img
                            v-for="image in this.lstImage"
                            :src="image.imageUrl"
                            :key="image.dspIndex"
                          />
                        </VueSlickCarousel>
                      </div>
                    </div>
                    <div class="Thumbnail-image">
                      <div class="thumbnail-img-group">
                        <VueSlickCarousel
                          ref="c2"
                          :asNavFor="$refs.c1"
                          :slidesToShow="3"
                          v-if="this.lstImage !== null"
                          :focusOnSelect="true"
                        >
                          <img
                            v-for="image in this.lstImage"
                            :src="image.imageUrl"
                            :key="image.scrId"
                          />
                        </VueSlickCarousel>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </section>
          </div>
          <div class="col-lag-2">
            <button
              v-if="!isUserReadOnly"
              @click="openImageDialog()"
              class="btn btn-success"
            >
              変更
            </button>
          </div>
        </div>
      </b-col>
    </b-row>
    <!--- コンテンツ画像 -->
    <b-row class="mb-4" v-if="urlImgMid.imageUrl !== ''">
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>コンテンツ画像</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="4" md="6">
        <img width="500px" :src="urlImgMid.imageUrl" />
      </b-col>
      <b-col xl="2" lg="2" md="6">
        <button
          v-if="!isUserReadOnly"
          @click="openFile()"
          class="btn btn-success"
        >
          変更
        </button>
      </b-col>
    </b-row>
    <!--- 記事タイトル -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>記事タイトル</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="6" md="12">
        <b-form-group>
          <b-form-input
            type="text"
            name="txtTitle"
            :readonly="isUserReadOnly"
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
        <span hidden="true">
          {{ (editorConfig.height = 500) }}
        </span>
        <ckeditor
          type="classic"
          id="txtContentArea1"
          name="txtContentArea1"
          v-model="textContent.textArea1"
          :config="editorConfig"
          :readOnly="isUserReadOnly"
          v-validate="'required'"
        ></ckeditor>
        <span
          class="invalid-feedback d-block"
          v-if="errors.has('txtContentArea1') == true"
          >{{ errors.first("txtContentArea1") }}</span
        >
      </b-col>
    </b-row>
    <!--- 同意するボタン名 -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>同意するボタン名</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="4" md="12">
        <b-form-group>
          <b-form-input
            type="text"
            name="txtBtn"
            :readonly="isUserReadOnly"
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
  name: "screenTop",
  props: {
    textContent: null,
    urlImgMid: Object,
    lstImage: Array,
  },
  data() {
    return {};
  },
  async created() {},
  methods: {
    openFile() {
      document.getElementById("imgStep").click();
    },
    openImageDialog() {
      document.getElementById("imgThumbnail").click();
    },
  },
};
</script>

<style scoped>
.static-area .static-area-wrap {
  font-weight: 350 !important;
}
</style>
