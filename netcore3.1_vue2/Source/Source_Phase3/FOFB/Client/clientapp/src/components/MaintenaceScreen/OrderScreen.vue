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
    <!--- 商品受け取り方法(非表示/表示) -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>商品受け取り方法(非表示/表示)</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="6" md="12">
        <v-switch
          :readonly="isUserReadOnly"
          style="margin: unset"
          v-model="textContent.checkBoxFlg1"
          :label="
            textContent.checkBoxFlg1 === true
              ? this.ITEM_SHOW_TEXT
              : this.ITEM_HIDDEN_TEXT
          "
        ></v-switch>
      </b-col>
    </b-row>
    <!--- 配送タイトル -->
    <b-row v-if="this.textContent.checkBoxFlg1">
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>配送タイトル</strong
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
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtTitle3') == true"
            >{{ errors.first("txtTitle3") }}</span
          >
        </b-form-group>
      </b-col>
    </b-row>
    <!--- 支払店舗選択欄タイトル -->
    <b-row v-if="this.textContent.checkBoxFlg1">
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>支払店舗選択欄タイトル</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="6" md="12">
        <b-form-group>
          <b-form-input
            :readonly="isUserReadOnly"
            type="text"
            name="txtArea1"
            v-validate="'required'"
            v-model="textContent.textArea1"
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtArea1') == true"
            >{{ errors.first("txtArea1") }}</span
          >
        </b-form-group>
      </b-col>
    </b-row>
    <!--- 支払店舗選択の注意事項 -->
    <b-row class="mb-4" v-if="this.textContent.checkBoxFlg1">
      <b-col lg="1" md="12" xl="2">
        <label class="col-md-12"
          ><strong>支払店舗選択の注意事項</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="6">
        <ckeditor
          :readOnly="isUserReadOnly"
          type="classic"
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
    <!--- 店舗選択欄 タイトル -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>店舗選択欄 タイトル</strong
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
          />
          <span
            class="invalid-feedback d-block"
            v-if="errors.has('txtTitle2') == true"
            >{{ errors.first("txtTitle2") }}</span
          >
        </b-form-group>
      </b-col>
    </b-row>
        <!--- ご入金・お受取 店舗選択の注意事項 -->
    <b-row class="mb-4">
      <b-col lg="1" md="12" xl="2">
        <label class="col-md-12"
          ><strong>ご入金・お受取 店舗選択の注意事項</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="6">
        <ckeditor
          :readOnly="isUserReadOnly"
          type="classic"
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
    <!--- 注意事項１ -->
    <b-row class="mb-4">
      <b-col lg="1" md="12" xl="2">
        <label class="col-md-12"
          ><strong>注意事項１</strong><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col lg="6">
        <ckeditor
          :readOnly="isUserReadOnly"
          type="classic"
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
    <!--- 確認画面に進むボタン名 -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>確認画面に進むボタン名</strong
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
    <!--- 早期割引特典(非表示/表示) -->
    <b-row>
      <b-col lg="3" md="12" xl="2">
        <label class="col-md-12"
          ><strong>早期割引特典(非表示/表示)</strong
          ><span class="text-danger">*</span></label
        >
      </b-col>
      <b-col xl="4" lg="6" md="12">
        <v-switch
          :readonly="isUserReadOnly"
          style="margin: unset"
          v-model="textContent.checkBoxFlg2"
          :label="
            textContent.checkBoxFlg2 === true
              ? this.ITEM_SHOW_TEXT
              : this.ITEM_HIDDEN_TEXT
          "
        ></v-switch>
      </b-col>
    </b-row>
  </div>
</template>

<script>
export default {
  name: "screenOrder",
  props: {
    textContent: null,
  },
};
</script>

<style scoped></style>
