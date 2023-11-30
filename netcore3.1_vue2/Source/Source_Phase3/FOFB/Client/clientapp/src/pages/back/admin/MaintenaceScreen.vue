<template>
  <div>
    <VueElementLoading
      :active="loading"
      spinner="ring"
      color="var(--success)"
    />
    <div hidden>
      <button
        id="imgThumbnail"
        @click="openImgThumbnail()"
        ref="imgThumbnail"
      ></button>
    </div>
    <div hidden>
      <b-form-file
        id="imgThumbnailNew"
        multiple
        :accept="this.IMAGE_FILE"
        @change="imgThumbnailNewChange"
        ref="imgThumbnailNew"
      ></b-form-file>
    </div>
    <div hidden>
      <b-form-file
        id="imgThumbnailNewSingle"
        :accept="this.IMAGE_FILE"
        @change="imgThumbnailNewSingleChange"
        ref="imgThumbnailNewSingle"
      ></b-form-file>
    </div>
    <div hidden>
      <b-form-file
        id="imgStep"
        :accept="this.IMAGE_FILE"
        @change="imgStepChange"
        ref="imgStep"
      ></b-form-file>
      <b-form-file
        id="imgTop"
        :accept="this.IMAGE_FILE"
        @change="imgTopChange"
        ref="imgTop"
      ></b-form-file>
      <b-form-file
        id="imgBottom1"
        :accept="this.IMAGE_FILE"
        @change="imgBottom1Change"
        ref="imgBottom1"
      ></b-form-file>
      <b-form-file
        id="imgBottom2"
        :accept="this.IMAGE_FILE"
        @change="imgBottom2Change"
        ref="imgBottom2"
      ></b-form-file>
    </div>
    <div class="main-card">
      <div class="card card-body">
        <!-- pagination for table Screen start  -->
        <br />
        <div class="row mb-1">
          <div class="col-md-12 col-lg-12 text-xs-right">
            <div class="row" style="float: right; padding-right: 6px">
              <p
                style="
                  margin-bottom: unset;
                  line-height: 3;
                  padding-right: 10px;
                "
              >
                全 {{ itemsLength }} 件中 {{ pageStart }} 件 〜
                {{ pageStop }} 件を表示
              </p>
              <v-pagination
                v-if="this.lstScreen.length > this.itemsPerPage"
                :total-visible="7"
                v-model="page"
                :length="pageCount"
              ></v-pagination>
            </div>
          </div>
        </div>
        <!-- pagination for table Screen end  -->

        <!-- table data Screen start  -->
        <v-data-table
          :headers="headersImage"
          :items="lstScreen"
          hide-default-footer
          :page.sync="page"
          :items-per-page="itemsPerPage"
          @page-count="pageCount = $event"
          no-results-text="データがありません。"
          no-data-text="データがありません。"
        >
          <template v-slot:item="{ item }">
            <tr>
              <td class="text-left">{{ item.no }}</td>
              <td class="text-left">
                <strong
                  ><a @click="openScreenDialog(item)" class="router">{{
                    item.kbnValueName
                  }}</a></strong
                >
              </td>
            </tr>
          </template>
        </v-data-table>
        <!-- table data Screen end  -->

        <!-- Dialog Screen -->
        <v-dialog v-model="dialogScreen" fullscreen hide-overlay persistent>
          <v-card>
            <v-toolbar dark color="primary">
              <v-btn icon dark @click="dialogScreen = false">
                <v-icon>mdi-close</v-icon>
              </v-btn>
              <v-toolbar-title>{{
                this.currentScreen.kbnValueName
              }}</v-toolbar-title>
              <v-spacer></v-spacer>
              <v-toolbar-items> </v-toolbar-items>
            </v-toolbar>
            <VueElementLoading
              :active="popupLoading"
              spinner="ring"
              color="var(--success)"
            />
            <v-card-text>
              <div class="main-card">
                <!-- screen top -->
                <screenTop
                  v-if="currentScreen.kbnValue === this.SCREEN_TOP"
                  :textContent="textContent"
                  :urlImgMid="urlImgMid"
                  :lstImage="lstImage"
                ></screenTop>
                <!-- screen  product list -->
                <screenProductList
                  v-if="currentScreen.kbnValue === this.SCREEN_LIST"
                  :textContent="textContent"
                  :urlImgTop="urlImgTop"
                  :urlImgBottom1="urlImgBottom1"
                  :urlImgBottom2="urlImgBottom2"
                ></screenProductList>
                <!-- screen  product Detail -->
                <screenProductDetail
                  v-if="currentScreen.kbnValue === this.SCREEN_DETAIL"
                  :textContent="textContent"
                ></screenProductDetail>
                <!-- screen  order -->
                <screenOrder
                  v-if="currentScreen.kbnValue === this.SCREEN_ORDER"
                  :textContent="textContent"
                ></screenOrder>
                <!-- screen  orderConfirm -->
                <screenOrderConfirm
                  v-if="currentScreen.kbnValue === this.SCREEN_ORDER_CONFIRM"
                  :textContent="textContent"
                ></screenOrderConfirm>
                <!-- screen orderFinish -->
                <screenOrderFinish
                  v-if="currentScreen.kbnValue === this.SCREEN_ORDER_FINISH"
                  :textContent="textContent"
                ></screenOrderFinish>
                <!-- screen rule -->
                <screenRule
                  v-if="currentScreen.kbnValue === this.SCREEN_RULES"
                  :textContent="textContent"
                ></screenRule>
              </div>
            </v-card-text>
            <!-- bottom button -->
            <div style="text-align: center; padding-bottom: 50px">
              <v-btn
                color="primary"
                class="btn_confirm_yes"
                @click="dialogScreen = false"
              >
                キャンセル
              </v-btn>
              <v-btn @click="showDialogPreview()" class="btn_confirm_yes"
                >プレビュー</v-btn
              >
              <v-btn
                color="error"
                class="btn_confirm_yes"
                v-if="!isUserReadOnly"
                @click="showDialogConfirmUpdate()"
              >
                保存
              </v-btn>
            </div>
          </v-card>
        </v-dialog>
        <!-- Dialog Preview Screen -->

        <!-- Dialog update img thumbnail start -->
        <v-dialog v-model="dialogImage" max-width="1000px">
          <v-card>
            <VueElementLoading spinner="ring" color="var(--success)" />
            <div class="row">
              <div class="col" style="text-align: left">
                <button
                  @click="deleteManyImage"
                  style="margin:20px"
                  class="btn btn-danger"
                >
                  削除
                </button>
              </div>
              <div class="col" style="text-align: right">
                <button
                  @click="openFileImage"
                  style="margin:20px"
                  class="btn btn-success"
                >
                  追加
                </button>
              </div>
            </div>
            <v-data-table
              v-model="selectedImage"
              :headers="headersImageThumnail"
              :items="imgThumbnailNew"
              item-key="no"
              hide-default-footer
              show-select
              class="elevation-1"
              no-results-text="データがありません。"
              no-data-text="データがありません。"
            >
              <template v-slot:item="{ item }">
                <tr>
                  <td>
                    <v-checkbox
                      :ripple="false"
                      v-model="selectedImage"
                      :value="item"
                      hide-details
                    >
                    </v-checkbox>
                  </td>
                  <td class="text-center">
                    {{ item.no }}
                  </td>
                  <td class="text-center">
                    <img
                      width="150px"
                      style="margin: 10px"
                      :src="item.imageUrl"
                    />
                  </td>
                  <td class="text-center">
                    <strong>
                      <button
                        @click="openFileUpdate(item)"
                        class="btn btn-success mr-2"
                      >
                        編集
                      </button>
                      <button @click="deleteImage(item)" class="btn btn-danger">
                        削除
                      </button>
                    </strong>
                  </td>
                </tr>
              </template>
            </v-data-table>
            <!-- bottm button-->
            <div>
              <v-card-actions>
                <v-spacer></v-spacer>
                <button class="btn btn-primary" @click="dialogImage = false">
                  キャンセル
                </button>
                <v-spacer></v-spacer>
                <button
                  @click="changeImage()"
                  style="width: 90px"
                  class="btn btn-danger"
                >
                  保存
                </button>
                <v-spacer></v-spacer>
              </v-card-actions>
            </div>
          </v-card>
        </v-dialog>
        <!-- Dialog update img thumbnail end -->

        <!--  dialog confirm Update Screen start -->
        <v-dialog
          v-model="dialogConfirmUpdateScreen"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="text-center">変更します。よろしいですか。</div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn
                class="btn_confirm_no"
                @click="dialogConfirmUpdateScreen = false"
                >キャンセル</v-btn
              >
              <v-btn class="btn_confirm_yes" @click="updateScreen()"
                >保存</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!--  dialog confirm Update Screen end -->

        <!-- Dialog Preview Screen start-->
        <v-dialog v-model="dialogPreviewScreen" max-width="1100px">
          <v-card>
            <VueElementLoading
              :active="popupLoading"
              spinner="ring"
              color="var(--success)"
            />
            <v-card-title>
              <span class="headline"></span>
            </v-card-title>
            <v-card-text>
              <div class="main-card">
                <!-- screen Preview top -->
                <screenPreviewTop
                  v-if="currentScreen.kbnValue === this.SCREEN_TOP"
                  :textContent="textContent"
                  :urlImgMid="urlImgMid"
                  :lstImage="lstImage"
                ></screenPreviewTop>
                <!-- screen Preview product list -->
                <screenPreviewProductList
                  v-if="currentScreen.kbnValue === this.SCREEN_LIST"
                  :textContent="textContent"
                  :urlImgTop="urlImgTop"
                  :urlImgProducts="lstImageProductWithDspFlg"
                  :urlImgBottom1="urlImgBottom1"
                  :urlImgBottom2="urlImgBottom2"
                ></screenPreviewProductList>
                <!-- screen Preview product Detail -->
                <screenPreviewProductDetail
                  v-if="currentScreen.kbnValue === this.SCREEN_DETAIL"
                  :textContent="textContent"
                ></screenPreviewProductDetail>
                <!-- screen Preview order -->
                <screenPreviewOrder
                  v-if="currentScreen.kbnValue === this.SCREEN_ORDER"
                  :textContent="textContent"
                ></screenPreviewOrder>
                <!-- screen Preview orderConfirm -->
                <screenPreviewOrderConfirm
                  v-if="currentScreen.kbnValue === this.SCREEN_ORDER_CONFIRM"
                  :textContent="textContent"
                ></screenPreviewOrderConfirm>
                <!-- screen Preview orderFinish -->
                <screenPreviewOrderFinish
                  v-if="currentScreen.kbnValue === this.SCREEN_ORDER_FINISH"
                  :textContent="textContent"
                ></screenPreviewOrderFinish>
                <!-- screen Preview rule -->
                <screenPreviewRules
                  v-if="currentScreen.kbnValue === this.SCREEN_RULES"
                  :textContent="textContent"
                ></screenPreviewRules>
              </div>
            </v-card-text>
            <!-- bottom button -->
            <div style="text-align: center; padding-top: 20px">
              <v-btn
                color="primary"
                class="btn_confirm_yes"
                @click="dialogPreviewScreen = false"
              >
                戻る
              </v-btn>
            </div>
          </v-card>
        </v-dialog>
        <!-- Dialog Preview Screen end-->
        <v-dialog
          v-model="dialogProductCd"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card v-if="lstProductCd.length > 0">
            <p>品番を選択する</p>
            <b-form-select
              class="province-area"
              name="province"
              v-model="selectedProduct"
              :options="lstProductCd"
              @change="changeProductCd(selectedProduct)"
            ></b-form-select>
          </v-card>
          <v-card v-else>
            <p>まず在庫管理で商品を追加してください。</p>
          </v-card>
        </v-dialog>
        <!-- Dialog Preview Screen end-->
        <v-dialog
          v-model="dialogBussinessCd"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <p>データをコピーしたい業態を選択してください。</p>
            <b-form-select
              class="province-area"
              name="province"
              v-model="selectedBussinessCd"
              :options="lstBussiness"
              @change="changeBussiness(selectedBussinessCd)"
            ></b-form-select>
          </v-card>
        </v-dialog>
      </div>
    </div>
  </div>
</template>

<script>
import screenTop from "@/components/MaintenaceScreen/TopScreen.vue";
import screenRule from "@/components/MaintenaceScreen/RulesScreen.vue";
import screenPreviewRules from "@/components/MaintenaceScreen/RulesPreview.vue";
import screenPreviewTop from "@/components/MaintenaceScreen/TopPreview.vue";
import screenOrderFinish from "@/components/MaintenaceScreen/OrderFinishScreen.vue";
import screenPreviewOrderFinish from "@/components/MaintenaceScreen/OrderFinishPreview.vue";
import screenOrderConfirm from "@/components/MaintenaceScreen/OrderConfirmScreen.vue";
import screenPreviewOrderConfirm from "@/components/MaintenaceScreen/OrderConfirmPreview.vue";
import screenOrder from "@/components/MaintenaceScreen/OrderScreen.vue";
import screenPreviewOrder from "@/components/MaintenaceScreen/OrderPreview.vue";
import screenProductDetail from "@/components/MaintenaceScreen/ProductDetailScreen.vue";
import screenPreviewProductDetail from "@/components/MaintenaceScreen/productDetailPreview.vue";
import screenProductList from "@/components/MaintenaceScreen/ProductListScreen.vue";
import screenPreviewProductList from "@/components/MaintenaceScreen/ProductListPreview.vue";

export default {
  components: {
    screenTop,
    screenPreviewTop,
    screenRule,
    screenPreviewRules,
    screenOrderFinish,
    screenPreviewOrderFinish,
    screenOrderConfirm,
    screenPreviewOrderConfirm,
    screenOrder,
    screenPreviewOrder,
    screenProductDetail,
    screenPreviewProductDetail,
    screenProductList,
    screenPreviewProductList,
  },
  data() {
    return {
      page: 1,
      popupLoading: false,
      dialogScreen: false,
      pageCount: 0,
      selectedProduct: "",
      selectedBussinessCd: "",
      dialogProductCd: false,
      dialogBussinessCd: false,
      itemsPerPage: 10,
      lstScreen: [],
      dialogConfirmUpdateScreen: false,
      currentScreen: {},
      urlImgMid: {},
      urlImgTop: {},
      urlImgBottom1: {},
      urlImgBottom2: {},
      lstImage: [],
      dataImage: [],
      dataImageDelete: [],
      textContent: {},
      textContentOld: {},
      loading: false,
      selectedImage: [],
      imgThumbnailNew: [],
      dialogImage: false,
      currentImgThumbnail: {},
      currentBussinessName: "",
      bussiness: {},
      lstBussiness: [],
      lstProductCd: [],
      urlImgProducts:{},
      lstImageProductWithDspFlg:[],
      screenSelect: {},
      isShowProductList: false,
      dialogPreviewScreen: false,
      isProductListScreen: false,
      isChangeText: false,
      isChangeImg: false,
      headersImageThumnail: [
        {
          text: "No",
          sortable: false,
          width: "10%",
          align: "center",
        },
        {
          text: "画像",
          sortable: false,
          width: "40%",
          align: "center",
        },
        {
          text: "操作",
          sortable: false,
          width: "40%",
          align: "center",
        },
      ],
      headersImage: [
        {
          text: "No",
          sortable: false,
          width: "10%",
          align: "left",
        },
        {
          text: "画面名",
          sortable: false,
          width: "40%",
          align: "left",
        },
      ],
      checkAddProduct: false
    };
  },
  async created() {
    this.loading = true;
    this.currentUser = this.getUserData().userCd;
    this.bussinessCd = this.getUserData().bussinessCd;
    this.getBussinessName();
    await this.getListScreen();
    this.lstProductCd = await this.getProductCd();
    this.checkShowProductList();
    this.lstProductCd = this.lstProductCd.map((x) => x.productCd);
    await this.checkShowListBussiness()
    this.loading = false;
  },
  methods: {
    //品番変更
    async changeProductCd(productCd) {
      this.textContent = {};
      try {
        const resConfig = await this.getConfigScreenWithProductCd(
          this.SCREEN_DETAIL,
          this.bussinessCd,
          productCd
        );
        if (resConfig !== null && resConfig.length != 0) {
          this.textContent = resConfig[0]
          this.textContent.isOneProduct = this.lstProductCd.length > 1;
          this.textContentOld = {...this.textContent};

          this.dialogScreen = true;
          this.dialogProductCd = false;
        } else {
          this.dialogScreen = false;
          return;
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.display = false;
      }
    },
    checkShowProductList(){
      if( this.urlImgProducts != null &&  this.urlImgProducts.length > 1){
        this.isShowProductList = false;
        this.lstImageProductWithDspFlg = this.urlImgProducts.filter(
          (x) => x.displayFlg == 0
        );
        if(this.lstImageProductWithDspFlg.length > 1){
            this.isShowProductList = true;
            this.lstImageProductWithDspFlg.sort((a, b) => b.createDtTm.localeCompare(a.createDtTm));
          }
      }
      if(!this.isShowProductList){
          const index = this.lstScreen.findIndex((x) => x.kbnValue === this.SCREEN_LIST);
          this.lstScreen.splice(index, 1);
          for (var i = 1; i <= this.lstScreen.length; i++) {
            this.lstScreen[i - 1].no = i;
          }
      }
      
    },
    //品番一覧を取得する
    async getProductCd() {
      const config = {
        method: "get",
        url: "api/MProduct/GetListProductMaintain",
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      try {
        const productCd = await require("axios")(config);
        if (productCd.data !== null) {
          this.urlImgProducts = productCd.data;
          return productCd.data;
        }
      } catch (error) {
        console.log(error);
      }
    },
    //業態名を取得する
    async getBussinessName() {
      try {
        const config = {
          method: "get",
          url: "api/MBussiness/GetAllByBussinessCd",
          params: {
            bussinessCd: this.bussinessCd,
          },
        };
        const getListBussinessRespone = await require("axios")(config);
        this.currentBussinessName = getListBussinessRespone.data[0].bussinessName.toLowerCase();
      } catch (error) {
        console.log(error);
      }
    },
    async checkShowListBussiness() {
      if (await this.getConfigScreenByScrType(this.SCREEN_TOP) == false) {
        await this.listBussiness()
        this.checkAddProduct = false;
      } else {
        this.checkAddProduct = true;
      }
    },
    //レイアウトメンテナンダイアログを表示する
    async openScreenDialog(screenTemp) {
      this.loading = true;
      this.selectedProduct = "";
      this.currentScreen = screenTemp;
      if (screenTemp.kbnValue === this.SCREEN_LIST) {
        this.isProductListScreen = true;
      } else {
        this.isProductListScreen = false;
      }
      if (screenTemp.kbnValue === this.SCREEN_DETAIL && this.checkAddProduct) {
        this.dialogProductCd = true;
        this.loading = false;
        return;
      }
      this.clearAll();
      this.isChangeText = false;
      this.isChangeImg = false;

      try {
        if (await this.getConfigScreenByScrType(this.currentScreen.kbnValue)) {
          this.dialogScreen = true;
        }
        else {
          await this.checkShowListBussiness()
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loading = false;
      }
    },
    async getConfigScreenByScrType(scrType) {
      const resConfig = await this.getConfigScreen(
        scrType,
        this.bussinessCd
      );
      if (resConfig !== null && resConfig.length > 0) {
        this.textContent = {...resConfig[0]};
        this.textContent.checkBoxFlg1 = !this.textContent.checkBoxFlg1;
        this.textContent.checkBoxFlg2 = !this.textContent.checkBoxFlg2;
        this.textContent.buttonFlg = this.textContent.buttonFlg === 0 ? false : true;

        this.textContentOld = {...this.textContent};

        this.lstImage = resConfig.filter(
          (x) => x.imagePosition === this.POSITION_TOP
        );
        if (this.lstImage != undefined && this.lstImage.length != 0) {
          this.urlImgTop = {
            ...this.lstImage[0],
          };
          this.urlImgTop.imageUrl =
            this.PRODUCT_LIST_PATH + this.lstImage[0].imageUrl;
          this.lstImage.map(
            (x) => (x.imageUrl = this.IMAGE_TOP_PATH + x.imageUrl)
          );
        }
        this.lstUrlImgBottom = resConfig.filter(
          (x) => x.imagePosition === this.POSITION_BOTTOM
        );
        if (
          this.lstUrlImgBottom != undefined &&
          this.lstUrlImgBottom.length != 0
        ) {
          this.urlImgBottom1 = { ...this.lstUrlImgBottom[0] };
          this.urlImgBottom2 = { ...this.lstUrlImgBottom[1] };
          this.urlImgBottom1.imageUrl =
            this.PRODUCT_LIST_PATH + this.lstUrlImgBottom[0].imageUrl;
          this.urlImgBottom2.imageUrl =
            this.PRODUCT_LIST_PATH + this.lstUrlImgBottom[1].imageUrl;
        }
        this.urlImgMid = resConfig.filter(
          (x) => x.imagePosition === this.POSITION_MID
        )[0];
        if (this.urlImgMid != undefined && this.urlImgMid.length != 0) {
          this.urlImgMid.imageUrl =
            this.IMAGE_TOP_PATH + this.urlImgMid.imageUrl;
        }
        return true;
      } else {
        return false;
      }
    },
    // 業態を選択
    async changeBussiness(bussinessCd) {
      this.loading = true;
      this.dialogBussinessCd = false;
      const config = {
        method: 'post',
        url: 'api/FrontScreen/CreateFrontScreenByBussinessCd',
        data: this.bussiness,
        params: {
          bussinessCdDuplicate: bussinessCd,
          bussinessNameDuplicate: this.lstBussiness.filter((x) => x.value === bussinessCd)[0].text,
          updateUserCd: this.currentUser
        },
      }
      try {
        const resFrontScreenByBussinessCd = await require("axios")(config)
        if (resFrontScreenByBussinessCd.data > 0) {
          this.$toastr.s(this.msgInsertSucces);
          this.checkAddProduct = true;
        } else {
          this.$toastr.e(this.msgInsertFailed);
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.selectedBussinessCd = "";
        this.loading = false;
      }
    },
    // 条件によるMBussiness一覧を取得する
    async listBussiness() {
      try {
        const lstBussiness = await this.getListBussiness();
        if (lstBussiness != null && lstBussiness.length > 0) {
          this.lstBussiness = []
          this.bussiness = lstBussiness.filter((x) => x.bussinessCd === this.bussinessCd)[0]
          let lstBussinessFilter = lstBussiness.filter((x) => x.bussinessCd !== this.bussinessCd)
          lstBussinessFilter.map((bussiness) => {
            this.lstBussiness.push({
              value: bussiness.bussinessCd,
              text: bussiness.bussinessName,
            })
          })
          this.dialogBussinessCd = true;
        }
      } catch (error) {
        console.log(error);
      }
    },
    //旧データをクリアする
    clearAll() {
      this.textContent = {};
      this.selectedImage = [];
      this.urlImgMid = {};
      this.urlImgTop = {};
      this.urlImgBottom1 = {};
      this.urlImgBottom2 = {};
      this.lstImage = [];
    },
    //画面名一覧
    async getListScreen() {
      const config = {
        method: "get",
        url: "api/Common/GetCbbByKbnCd",
        params: {
          kbnCd: this.KBN_LIST_SCREEN,
        },
      };
      try {
        const lstScreen = await this.axios(config);
        if (lstScreen.data != null) {
          this.lstScreen = lstScreen.data;
          for (var i = 1; i <= this.lstScreen.length; i++) {
            this.lstScreen[i - 1].no = i;
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    //サムネイル画像一覧を表示する
    openImgThumbnail() {
      this.imgThumbnailNew = JSON.parse(JSON.stringify(this.lstImage));
      if (
        this.imgThumbnailNew.filter((x) => x.isHaveFile === true).length > 0
      ) {
        this.imgThumbnailNew = [...this.lstImage];
      }
      for (var i = 1; i <= this.imgThumbnailNew.length; i++) {
        this.imgThumbnailNew[i - 1].no = i;
      }
      this.dataImage = [];
      this.selectedImage = [];
      this.dialogImage = true;
    },
    //複数画像を削除する
    async deleteManyImage() {
      if (this.selectedImage.length === 0) {
        this.$toastr.w(this.msgNotChooseImage);
      } else {
        if (this.selectedImage.length > this.imgThumbnailNew.length - 1) {
          this.$toastr.w(this.msgNotChooseImageMax1);
          return;
        }
      }
      for (var i = 0; i < this.selectedImage.length; i++) {
        const j = this.imgThumbnailNew.findIndex(
          (x) => x.no === this.selectedImage[i].no
        );
        this.imgThumbnailNew.splice(j, 1);
        this.selectedImage[i].imageUrl = this.selectedImage[i].imageUrl.replace(
          this.IMAGE_TOP_PATH,
          ""
        );
        this.selectedImage[i].delFlg = true;
      }
      this.selectedImage = [];
      this.imgThumbnailNew = this.imgThumbnailNew.map((x, idx) => {
        x.no = idx + 1;
        return x;
      });
    },
    //1枚の画像を削除する
    deleteImage(dataImg) {
      this.selectedImage = [];
      if (this.imgThumbnailNew.length < 2) {
        this.$toastr.w(this.msgNotChooseImageMax1);
        return;
      }
      const i = this.imgThumbnailNew.findIndex((x) => x.no === dataImg.no);
      this.imgThumbnailNew.splice(i, 1);
      this.imgThumbnailNew = this.imgThumbnailNew.map((x, idx) => {
        x.no = idx + 1;
        return x;
      });
      dataImg.delFlg = true;
      dataImg.imageUrl = dataImg.imageUrl.replace(this.IMAGE_TOP_PATH, "");
    },
    openFileImage() {
      document.getElementById("imgThumbnailNew").click();
    },
    //画像を変更する
    async changeImage() {
      this.dialogImage = false;
      for (let i = 0; i < this.lstImage.length; i++) {
        let imgDel = this.imgThumbnailNew.find(
          (x) => x.scrImgUrlId === this.lstImage[i].scrImgUrlId
        );
        if (imgDel == undefined) {
          this.lstImage[i].imageUrl = this.lstImage[i].imageUrl.replace(
            this.IMAGE_TOP_PATH,
            ""
          );
          this.lstImage[i].delFlg = true;
          this.dataImageDelete.push(this.lstImage[i]);
        }
      }
      for (let i = 0; i < this.imgThumbnailNew.length; i++) {
        if (this.imgThumbnailNew[i].fileImg !== undefined) {
          if (this.imgThumbnailNew[i].scrImgUrlId !== undefined) {
            let dataImg = { ...this.imgThumbnailNew[i] };
            dataImg.isHaveFile = true;
            dataImg.imageUrl = this.imgThumbnailNew[i].imageUrlName;
            dataImg.fileImg = this.imgThumbnailNew[i].fileImg;
            dataImg.updateUserCd = this.currentUser;
            this.dataImage.push(dataImg);
          } else {
            this.dataImage.push({
              imageUrl: this.imgThumbnailNew[i].imageUrlName,
              fileImg: this.imgThumbnailNew[i].fileImg,
              isHaveFile: true,
              imagePosition: this.POSITION_TOP,
              createUserCd: this.currentUser,
              scrId: this.textContent.scrId,
              dspIndex: this.imgThumbnailNew[i].no,
            });
          }
        } else {
          this.dataImage.push(this.imgThumbnailNew[i]);
        }
      }
      this.lstImage = [...this.imgThumbnailNew];
      this.isChangeImg = true;
    },
    //画像を変更する
    imgThumbnailNewChange(e) {
      this.selected = [];
      const imgThumbnailNew = e.target.files;
      const currentNo = this.imgThumbnailNew.length;
      if (imgThumbnailNew.length === 0) {
        return;
      }

      for (let i = 0; i < imgThumbnailNew.length; i++) {
        this.imgThumbnailNew.push({
          imageUrl: URL.createObjectURL(e.target.files[i]),
          no: i + currentNo + 1,
          fileImg: e.target.files[i],
          isHaveFile: true,
          imageUrlName:
            this.currentBussinessName + "/" + imgThumbnailNew[i].name,
        });
      }
      this.$refs["imgThumbnailNew"].reset();
    },
    //画像を変更する
    imgThumbnailNewSingleChange(e) {
      const imgThumbnail = e.target.files[0];
      if (imgThumbnail.length === 0) {
        return;
      }
      let imgChange = this.imgThumbnailNew.find(
        (x) => x.no === this.currentImgThumbnail.no
      );
      imgChange.imageUrl = URL.createObjectURL(imgThumbnail);
      imgChange.fileImg = imgThumbnail;
      imgChange.isHaveFile = true;
      imgChange.imageUrlName =
        this.currentBussinessName + "/" + imgThumbnail.name;

      this.$refs["imgThumbnailNewSingle"].reset();
    },
    //画像を変更する
    async imgStepChange(e) {
      const imgStep = e.target.files[0];
      if (imgStep.length === 0) {
        return;
      }
      this.urlImgMid.imageUrl = URL.createObjectURL(imgStep);
      this.urlImgMid.fileImg = imgStep;
      this.urlImgMid.isHaveFile = true;
      this.urlImgMid.updateUserCd = this.currentUser;
      this.urlImgMid.imageUrlName = this.currentBussinessName + "/" + imgStep.name;
      this.isChangeImg = true;
      this.$refs["imgStep"].reset();
    },
    //画像を変更する
    imgBottom1Change(e) {
      const imgBottom1 = e.target.files[0];
      if (imgBottom1.length === 0) {
        return;
      }
      this.urlImgBottom1.imageUrl = URL.createObjectURL(imgBottom1);
      this.urlImgBottom1.fileImg = imgBottom1;
      this.urlImgBottom1.isHaveFile = true;
      this.urlImgBottom1.updateUserCd = this.currentUser;
      this.urlImgBottom1.imageUrlName = this.currentBussinessName + "/" + imgBottom1.name;
      this.isChangeImg = true;
      this.$refs["imgBottom1"].reset();
    },
    //画像を変更する
    imgTopChange(e) {
      const imgTop = e.target.files[0];
      if (imgTop.length === 0) {
        return;
      }
      this.urlImgTop.imageUrl = URL.createObjectURL(imgTop);
      this.urlImgTop.fileImg = imgTop;
      this.urlImgTop.isHaveFile = true;
      this.urlImgTop.updateUserCd = this.currentUser;
      this.urlImgTop.imageUrlName = this.currentBussinessName + "/" + imgTop.name;
      this.isChangeImg = true;
      this.$refs["imgTop"].reset();
    },
    //画像を変更する
    imgBottom2Change(e) {
      const imgBottom2 = e.target.files[0];
      if (imgBottom2.length === 0) {
        return;
      }
      this.urlImgBottom2.imageUrl = URL.createObjectURL(imgBottom2);
      this.urlImgBottom2.fileImg = imgBottom2;
      this.urlImgBottom2.isHaveFile = true;
      this.urlImgBottom2.updateUserCd = this.currentUser;
      this.urlImgBottom2.imageUrlName = this.currentBussinessName + "/" + imgBottom2.name;
      this.isChangeImg = true;
      this.$refs["imgBottom2"].reset();
    },
    openFileUpdate(item) {
      this.currentImgThumbnail = { ...item };
      document.getElementById("imgThumbnailNewSingle").click();
    },
    //レイアウトを更新する
    async updateScreen() {
      this.dialogConfirmUpdateScreen = false;
      this.loading = true;
      this.dialogScreen = false;

      try {
        const formData = new FormData();
        this.textContent.updateUserCd = this.currentUser;
        this.textContent.checkBoxFlg1 = this.textContent.checkBoxFlg1 ? 0 : 1;
        this.textContent.checkBoxFlg2 = this.textContent.checkBoxFlg2 ? 0 : 1;
        this.textContent.buttonFlg = this.textContent.buttonFlg ? 1 : 0;
        formData.append("dataContent", JSON.stringify(this.textContent));
        let urlImgMid = { ...this.urlImgMid };
        urlImgMid.imageUrl = urlImgMid.imageUrlName;
        this.dataImage.push(urlImgMid);
        let urlImgTop = { ...this.urlImgTop };
        urlImgTop.imageUrl = urlImgTop.imageUrlName;
        this.dataImage.push(urlImgTop);
        let urlImgBottom1 = { ...this.urlImgBottom1 };
        urlImgBottom1.imageUrl = urlImgBottom1.imageUrlName;
        this.dataImage.push(urlImgBottom1);
        let urlImgBottom2 = { ...this.urlImgBottom2 };
        urlImgBottom2.imageUrl = urlImgBottom2.imageUrlName;
        this.dataImage.push(urlImgBottom2);
        this.dataImage = this.dataImage.filter((x) => x.isHaveFile == true);

        formData.append(
          "isProductListScreen",
          JSON.stringify(this.isProductListScreen)
        );

        for (let i = 0; i < this.dataImage.length; i++) {
          this.dataImage[i].imageUrl = this.dataImage[i].imageUrl.replace(
            this.IMAGE_TOP_PATH,
            ""
          );
          formData.append("file[" + i + "]", this.dataImage[i].fileImg);
        }
        formData.append("lstImage", JSON.stringify(this.dataImage));
        formData.append(
          "dataImageDelete",
          JSON.stringify(this.dataImageDelete)
        );
        const axios = require("axios");
        const resUpdate = await axios.post(
          "api/FrontScreen/UpdateAsyncSub",
          formData,
          {
            headers: {
              "Content-type": "multipart/form-data",
            },
          }
        );
        if (resUpdate.data) {
          await this.getListScreen();
          this.$toastr.s(this.msgUpdateSucces);
          this.loading = false;
        } else {
          this.$toastr.e(this.msgUpdateFailed);
        }
      } catch (error) {
        console.log(error);
      }
    },
    //更新確認ダイアログを表示する
    showDialogConfirmUpdate() {
      const error = this.$validator.validateAll();
      if (error) {
        const arrayItemErr = this.$validator.errors.items;
        if (arrayItemErr.length !== 0) {
          return;
        }
      }

      switch(this.currentScreen.kbnValue)
      {
        case this.SCREEN_TOP:
          if (JSON.stringify(this.textContent.textArea1) != JSON.stringify(this.textContentOld.textArea1 )
              || JSON.stringify(this.textContent.textButton1) != JSON.stringify(this.textContentOld.textButton1 )   
              || JSON.stringify(this.textContent.textTitle1) != JSON.stringify(this.textContentOld.textTitle1 )
              ) 
          {
            this.isChangeText = true;
          }
          else
          {
            this.isChangeText = false;
          }
          break;
        case this.SCREEN_LIST:
          if (JSON.stringify(this.textContent.buttonFlg) != JSON.stringify(this.textContentOld.buttonFlg )
              || (JSON.stringify(this.textContent.buttonFlg) === true
                    && JSON.stringify(this.textContent.textNortify1) != JSON.stringify(this.textContentOld.textNortify1 )
                )
              ) 
          {
            this.isChangeText = true;
          }
          else
          {
            this.isChangeText = false;
          }
          break;
        case this.SCREEN_ORDER:
          if (JSON.stringify(this.textContent.textTitle1) != JSON.stringify(this.textContentOld.textTitle1 )
              || JSON.stringify(this.textContent.checkBoxFlg1) != JSON.stringify(this.textContentOld.checkBoxFlg1 )
              || JSON.stringify(this.textContent.textTitle2) != JSON.stringify(this.textContentOld.textTitle2 )
              || JSON.stringify(this.textContent.textNortify1) != JSON.stringify(this.textContentOld.textNortify1 )
              || JSON.stringify(this.textContent.textNortify2) != JSON.stringify(this.textContentOld.textNortify2 )
              || JSON.stringify(this.textContent.textButton1) != JSON.stringify(this.textContentOld.textButton1 )
              || JSON.stringify(this.textContent.checkBoxFlg2) != JSON.stringify(this.textContentOld.checkBoxFlg2 )
              || (JSON.stringify(this.textContent.checkBoxFlg1) === true
                  && ( JSON.stringify(this.textContent.textTitle3) != JSON.stringify(this.textContentOld.textTitle3 )
                      || JSON.stringify(this.textContent.textArea1) != JSON.stringify(this.textContentOld.textArea1 )
                      || JSON.stringify(this.textContent.textArea2) != JSON.stringify(this.textContentOld.textArea2 )
                      )
                  )
              ) 
          {
            this.isChangeText = true;
          }
          else
          {
            this.isChangeText = false;
          }
          break;
        case this.SCREEN_DETAIL:
        case this.SCREEN_ORDER_CONFIRM:
        case this.SCREEN_ORDER_FINISH:
        case this.SCREEN_RULES:
          if (JSON.stringify(this.textContent) != JSON.stringify(this.textContentOld ))
          {
            this.isChangeText = true;
          }
          else
          {
            this.isChangeText = false;
          }
          break;
        default:
          this.isChangeText = false;
          break;
      }

      if (this.isChangeImg === false && this.isChangeText === false )
      {
        this.$toastr.w(this.msgNotthingChanged);
        return;
      }

      this.dialogConfirmUpdateScreen = true;
    },
    //プレビューダイアログを表示する
    showDialogPreview() {
      this.dialogPreviewScreen = true;
    },
  },
  computed: {
    pageStop() {
      if (this.page < Math.ceil(this.lstScreen.length / this.itemsPerPage)) {
        return this.page * this.itemsPerPage;
      } else {
        return this.lstScreen.length;
      }
    },
    pageStart() {
      return (this.page - 1) * this.itemsPerPage + 1;
    },
    itemsLength() {
      return this.lstScreen.length;
    },
  },
};
</script>
