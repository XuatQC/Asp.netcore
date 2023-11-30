<template>
  <div>
    <VueElementLoading
      :active="loading"
      spinner="ring"
      color="var(--success)"
    />
    <div class="main-card" v-if="isSuperAdmin">
      <div class="card card-body">
          <div class="col-md-12 mail-setting-group">
            <b-form-radio-group
              v-model="isMailTemplate"
              :options="mailSettingOptions"
              @change="changeSetting()"
              name="radio-inline"
            ></b-form-radio-group>
          </div>
      </div>
    </div>
    <div class="main-card">
      <div class="card card-body">
        <div v-if="isMailTemplate">
          <div class="row">
            <div class="col-md-4">
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >メールタイプ</label
                >
                <div class="col-md-7 fix-padding-search">
                  <b-form-select
                    v-model="searchMailTemplate.type"
                    :options="mailTypeOptions"
                    class="select-search"
                  ></b-form-select>
                </div>
              </div>
            </div>
            <div class="col-md-4">
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-3 label-search fix-padding-search"
                  >件名</label
                >
                <div class="col-md-7 fix-padding-search">
                  <b-form-input
                    v-model="searchMailTemplate.title"
                    single-line
                    style="max-width: 250px"
                    hide-details
                  />
                </div>
              </div>
            </div>
            <div class="col-md-4">
              <div class="row col-md-12 btn-grp-search" style="padding-bottom: unset; padding-top: unset;">
                <div class="col-md-4">
                  <button type="button" class="btn btn-primary btn-search" @click="search()">
                    検索
                  </button>
                </div>
                <div class="col-md-3">
                  <button type="button" class="btn btn-secondary btn-clear" @click="clearSearch()">
                    クリア
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div v-else>
          <div class="row">
            <div class="col-md-8">
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >送信元</label
                >
                <div class="col-md-6 fix-padding-search">
                  <b-form-input
                    v-model="mailSetting.mailFrom"
                    @keyup="checkValidMailFrom()"
                    single-line
                    hide-details
                  />
                  <span
                    class="invalid-feedback d-block"
                    v-if="mailFromError != ''"
                    >{{ this.mailFromError }}</span
                  >
                </div>
              </div>
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >送信元名</label
                >
                <div class="col-md-6 fix-padding-search">
                  <b-form-input
                    v-model="mailSetting.mailFromName"
                    @keyup="checkValidMailFromName()"
                    single-line
                    hide-details
                  />
                  <span
                    class="invalid-feedback d-block"
                    v-if="mailFromNameError != ''"
                    >{{ this.mailFromNameError }}</span
                  >
                </div>
              </div>
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >AccessKeyID(バウンスメール取得)</label
                >
                <div class="col-md-6 fix-padding-search">
                  <b-form-input
                    v-model="mailSetting.mailBounceAccessKeyID"
                    @keyup="checkValidMailBounceAccessKeyID()"
                    single-line
                    hide-details
                  />
                  <span
                    class="invalid-feedback d-block"
                    v-if="mailBounceAccessKeyIDError != ''"
                    >{{ this.mailBounceAccessKeyIDError }}</span
                  >
                </div>
              </div>
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >SecretAccessKey(バウンスメール取得)</label
                >
                <div class="col-md-6 fix-padding-search">
                  <b-form-input
                    v-model="mailSetting.mailBounceSecretAccessKey"
                    @keyup="checkValidMailBounceSecretAccessKey()"
                    single-line
                    hide-details
                  />
                  <span
                    class="invalid-feedback d-block"
                    v-if="mailBounceSecretAccessKeyError != ''"
                    >{{ this.mailBounceSecretAccessKeyError }}</span
                  >
                </div>
              </div>
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >QueueUrl(バウンスメール取得)</label
                >
                <div class="col-md-8 fix-padding-search">
                  <b-form-input
                    v-model="mailSetting.mailBounceQueueUrl"
                    @keyup="checkValidMailBounceQueueUrl()"
                    single-line
                    hide-details
                  />
                  <span
                    class="invalid-feedback d-block"
                    v-if="mailBounceQueueUrlError != ''"
                    >{{ this.mailBounceQueueUrlError }}</span
                  >
                </div>
              </div>
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >SMTP Server</label
                >
                <div class="col-md-6 fix-padding-search">
                  <b-form-input
                    v-model="mailSetting.smtpServer"
                    @keyup="checkValidSmtpServer()"
                    single-line
                    hide-details
                  />
                  <span
                    class="invalid-feedback d-block"
                    v-if="smtpServerError != ''"
                    >{{ this.smtpServerError }}</span
                  >
                </div>
              </div>
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >SMTP Account</label
                >
                <div class="col-md-6 fix-padding-search">
                  <b-form-input
                    v-model="mailSetting.smtpAccount"
                    @keyup="checkValidSmtpAccount()"
                    single-line
                    hide-details
                  />
                  <span
                    class="invalid-feedback d-block"
                    v-if="smtpAccountError != ''"
                    >{{ this.smtpAccountError }}</span
                  >
                </div>
              </div>
              <div class="row col-md-12 fix-padding-search">
                <label class="col-md-4 label-search fix-padding-search"
                  >SMTP PASS</label
                >
                <div class="col-md-6 fix-padding-search">
                  <b-form-input
                    v-model="mailSetting.smtpPass"
                    @keyup="checkValidSmtpPass()"
                    single-line
                    hide-details
                  />
                  <span
                    class="invalid-feedback d-block"
                    v-if="smtpPassError != ''"
                    >{{ this.smtpPassError }}</span
                  >
                </div>
              </div>
              <div class="row col-md-12" style="margin-top: 15px;">
                <div class="offset-md-11">
                  <button
                    @click="showDialogMailSettingConfirm()"
                    style="width: 90px"
                    class="btn btn-danger"
                  >
                    保存
                  </button>
                </div>
              </div>
            </div>
          </div>
          <!-- Confirm update mailsetting dialog-->
          <v-dialog
            v-if="isSuperAdmin"
            v-model="dialogMailSettingConfirm"
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
                  @click="dialogMailSettingConfirm = false"
                  >キャンセル</v-btn
                >
                <v-btn class="btn_confirm_yes" :loading="loadingBtn" @click="updateMailSetting()"
                  >保存</v-btn
                >
              </v-card-actions>
            </v-card>
          </v-dialog>
        </div>
      </div>
    </div>
    <div class="main-card" v-if="isMailTemplate">
      <div class="card card-body">
        <div class="pagination">
          <div class="col-md-12" v-if="totalItem > 0">
            <div class="row" :style="`float: right; ${totalItem > itemsPerPage ? 'margin-right: -22px;' : ''}`">
              <p :class="`${totalItem > itemsPerPage ? 'page-info' : 'no-pagination'}`">
                全 {{ totalItem }} 件中 {{ pageStart }} 件 〜 {{ pageStop }} 件を表示
              </p>
              <v-pagination
                v-if="totalItem > itemsPerPage"
                @input="changePage(page)"
                v-model="page"
                :total-visible="7"
                :length="totalPage"
              ></v-pagination>
            </div>
          </div>
          <div v-else style="height: 42px"></div>
        </div>
        <v-data-table
          :headers="headers"
          :items="lstMailTemplate"
          item-key="mailTemplateId"
          hide-default-footer
          :items-per-page="itemsPerPage"
          :no-results-text="msgNoData"
          :no-data-text="msgNoData"
        >
          <template v-slot:item="{ item, index }">
            <tr>
              <td>{{ index + pageStart }}</td>
              <td class="text-left">
                 <a @click="openMailDialog(item)" class="router">
                    【{{
                      item.sendTo === SEND_TO_ADMIN
                        ? DEPART_ADMIN
                        : CUST_TEXT
                    }}】{{ item.typeName }}
                  </a>
              </td>
              <td class="text-left">{{ item.title }}</td>
            </tr>
          </template>
        </v-data-table>
        <!-- update mail template dialog -->
        <v-dialog
          v-model="dialogMail"
          max-width="1000px"
        >
          <v-card>
            <!-- title-->
            <v-card-title>
              <span class="headline"
                >【{{
                  this.mailTemplate.sendTo === SEND_TO_ADMIN
                    ? DEPART_ADMIN
                    : CUST_TEXT
                }}】{{ this.mailTemplate.typeName }}</span
              >
            </v-card-title>
            <!-- content detail-->
            <v-card-text>
              <div class="main-card">
                <!-- mail detail-->
                <div>
                  <b-row v-if="!isUserReadOnly" style="margin-bottom: 10px">
                    <b-col><span class="text-danger">{{this.msgCannotChangeVar}}</span></b-col>
                  </b-row>
                  <!--- Title -->
                  <div class="row">
                    <div class="col-md-1">
                      <label class="label-mail">件名 <span v-if="!isUserReadOnly" class="text-danger">*</span></label>
                    </div>
                    <div class="col-md-11">
                      <b-form-group>
                        <b-form-input
                          type="text"
                          v-model="mailTemplate.title"
                          @keyup="checkValidateMailTitle()"
                          :readonly="isUserReadOnly ? true : false"
                          placeholder=""
                          autocomplete="off"
                        />
                        <span
                          class="invalid-feedback d-block"
                          v-if="msgTitleError != ''"
                          >{{ this.msgTitleError }}</span
                        >
                      </b-form-group>
                    </div>
                  </div>
                  <!--- Content-->
                  <div class="row">
                    <div class="col-md-1">
                      <label class="label-mail">内容 <span v-if="!isUserReadOnly" class="text-danger">*</span></label>
                    </div>
                    <div class="col-md-11">
                      <ckeditor
                        type="classic"
                        v-model="mailTemplate.content"
                        :config="editorConfigMail"
                        @input="checkValidateMailContent()"
                      ></ckeditor>
                      <span
                        class="invalid-feedback d-block"
                        v-if="msgContentError != ''"
                        >{{ this.msgContentError }}</span
                      >
                    </div>
                  </div>
                </div>
              </div>
            </v-card-text>
            <!-- bottm button-->
            <div>
              <v-card-actions>
                <v-spacer></v-spacer>
                <button class="btn btn-primary" @click="dialogMail = false">
                  キャンセル
                </button>
                <v-spacer></v-spacer>
                <button
                  v-if="!isUserReadOnly"
                  @click="showDialogMailTemplateConfirm()"
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
        <!-- Confirm dialog-->
        <v-dialog
          v-model="dialogConfirm"
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
                @click="dialogConfirm = false"
                >キャンセル</v-btn
              >
              <v-btn class="btn_confirm_yes" :loading="loadingBtn" @click="updateMailTemplate()"
                >保存</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
      </div>
    </div>
    <!-- Dialog BussinessCd-->
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
</template>

<script>
export default {
  data() {
    return {
      loading: false,
      loadingBtn: false,
      isSuperAdmin: null,
      bussinessCd: "",
      updateUserCd: "",
      selectedBussinessCd: "",
      itemsPerPage: 0,
      totalData: 0,
      page: 1,
      isMailTemplate: true,
      searchMailTemplate: {},
      mailSetting: {},
      oldMailSetting: {},
      mailSettingOptions: [
        { text: "メールアドレス設定", value: false },
        { text: "メールテンプレート管理", value: true },
      ],
      mailTemplate: {},
      lstMailTemplate: [],
      headers: [
        {
          text: "No",
          sortable: false,
          align: "left",
          width: "10%",
        },
        {
          text: "タイプ",
          align: "left",
          sortable: false,
          width: "30%",
        },
        {
          text: "件名",
          align: "left",
          sortable: false,
          width: "60%",
        },
      ],
      mailTypeOptions: [
        { text: "", value: "" }
      ],
      dialogMailSettingConfirm: false,
      dialogMail: false,
      dialogConfirm: false,
      dialogBussinessCd: false,
      lstBussiness: [],
      msgTitleError: "",
      msgContentError: "",
      arrContentSave: [],
      arrTitleSave: [],
      arrContentDefault: [],
      arrTitleDefault: [],
      oldMailTemplate: {},
      mailFromError: "",
      mailFromNameError: "",
      mailBounceAccessKeyIDError: "",
      mailBounceSecretAccessKeyError: "",
      mailBounceQueueUrlError: "",
      smtpServerError: "",
      smtpAccountError: "",
      smtpPassError: ""
    };
  },
  computed: {
    totalPage() {
      if (this.itemsPerPage == null || this.totalData == null) {
        return 0;
      }
      return Math.ceil(this.totalData / this.itemsPerPage);
    },
    pageStop() {
      if (this.page < Math.ceil(this.totalData / this.itemsPerPage)) {
        return this.itemsPerPage * this.page;
      } else {
        return this.totalData;
      }
    },
    pageStart() {
      return (this.page - 1) * this.itemsPerPage + 1;
    },
    totalItem() {
      return this.totalData;
    },
  },
  async created() {
    this.loading = true;
    this.isSuperAdmin = this.getUserData().roleId == this.ROLE_SUPER_ADMIN ? true : false;
    this.bussinessCd = this.getUserData().bussinessCd;
    this.updateUserCd = this.getUserData().userCd;
    this.itemsPerPage = this.PAGE_SIZE_DEFAULT;

    if (this.isUserReadOnly) {
      this.editorConfigMail.readOnly = true;
    }

    await this.getListMailType();

    await this.listBussiness();

    await this.getListMailTemplate(this.PAGE_START_DEFAULT)
  },
  methods: {
    // 送信元バリデーション
    checkValidMailFrom() {
      this.mailFromError = this.validateMailAddress(
        this.mailSetting.mailFrom,
        100,
        this.MAIL_FROM
      );
    },
    // 送信元名バリデーション
    checkValidMailFromName() {
      this.mailFromNameError = this.validateTextRequiredAndLength(
        this.mailSetting.mailFromName,
        100,
        this.MAIL_FROM_NAME
      );
    },
    // AccessKeyID(バウンスメール取得)バリデーション
    checkValidMailBounceAccessKeyID() {
      this.mailBounceAccessKeyIDError = this.validateTextRequiredAndLength(
        this.mailSetting.mailBounceAccessKeyID,
        100,
        this.ACCESS_KEY_ID
      );
    },
    // SecretAccessKey(バウンスメール取得)バリデーション
    checkValidMailBounceSecretAccessKey() {
      this.mailBounceSecretAccessKeyError = this.validateTextRequiredAndLength(
        this.mailSetting.mailBounceSecretAccessKey,
        100,
        this.SECRET_ACCESS_KEY
      );
    },
    // QueueUrl(バウンスメール取得)バリデーション
    checkValidMailBounceQueueUrl() {
      this.mailBounceQueueUrlError = this.validateTextRequired(
        this.mailSetting.mailBounceQueueUrl,
        this.QUEUE_URL
      );
    },
    // SMTP Serverバリデーション
    checkValidSmtpServer() {
      this.smtpServerError = this.validateTextRequiredAndLength(
        this.mailSetting.smtpServer,
        100,
        this.SMTP_SERVER
      );
    },
    // SMTP Accountバリデーション
    checkValidSmtpAccount() {
      this.smtpAccountError = this.validateTextRequiredAndLength(
        this.mailSetting.smtpAccount,
        100,
        this.SMTP_ACCOUNT
      );
    },
    // SMTP Passバリデーション
    checkValidSmtpPass() {
      this.smtpPassError = this.validateTextRequiredAndLength(
        this.mailSetting.smtpPass,
        100,
        this.SMTP_PASS
      );
    },
    // メールタイトルをバリデーションする
    checkValidateMailTitle () {
      this.msgTitleError = this.validateTextRequiredAndLength(
        this.mailTemplate.title,
        100,
        this.MAIL_CONTENT
      );
    },
    // メール内容をバリデーションする
    checkValidateMailContent () {
      this.msgContentError = this.validateTextRequired(
        this.mailTemplate.content,
        this.MAIL_CONTENT
      );
    },
    // メールテンプレート一覧
    async getListMailTemplate (page) {
      this.loading = true;
      this.searchMailTemplate.bussinessCd = this.bussinessCd;
      const config = {
        method: "post",
        url: "api/MailTemplate/GetDataMailTemplatePagination",
        data: this.searchMailTemplate,
        params: {
          currentpage: page,
          pageSize: this.PAGE_SIZE_DEFAULT
        },
      };
      try {
        const resultGetLstMailTemplate = await require("axios")(config);
        if (resultGetLstMailTemplate.data !== null) {
          if (resultGetLstMailTemplate.data.data.length > 0) {
            this.lstMailTemplate = resultGetLstMailTemplate.data.data;
            this.totalData = resultGetLstMailTemplate.data.totalData;
          } else {
            this.dialogBussinessCd = true;
          }
        }
        this.page = page;
      } catch (error) {
        console.log(error);
      } finally {
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
        }
      } catch (error) {
        console.log(error);
      }
    },
    // 業態を選択
    async changeBussiness(bussinessCd) {
      this.loading = true;
      this.dialogBussinessCd = false;

      if (this.isMailTemplate) {
        const config = {
          method: 'post',
          url: 'api/MailTemplate/CreateMailTemplateByBussinessCd',
          data: this.bussiness,
          params: {
            bussinessCdDuplicate: bussinessCd,
            updateUserCd: this.updateUserCd
          },
        }
        try {
          const resMailTemplateByBussinessCd = await require("axios")(config)
          if (resMailTemplateByBussinessCd.data > 0) {
            this.$toastr.s(this.msgInsertSucces);
          } else {
            this.$toastr.e(this.msgInsertFailed);
          }
          await this.getListMailTemplate(this.PAGE_START_DEFAULT);
        } catch (error) {
          console.log(error);
        } finally {
          this.loading = false;
        }
      } else {
        await this.getMailSetting(bussinessCd)
      }
    },
    async getListMailType() {
      let lstMailType = await this.getParameterByCd(this.KBN_MAIL_TYPE);
      if (lstMailType.length > 0) {
        for (var i = 0; i < lstMailType.length; i++) {
          this.mailTypeOptions.push({ text: lstMailType[i].kbnValueName, value: lstMailType[i].kbnValue })
        }
      }
    },
    // 検索
    search() {
      this.getListMailTemplate(this.PAGE_START_DEFAULT);
    },
    // 検索クリア
    async clearSearch() {
      this.searchMailTemplate = {}
      this.getListMailTemplate(this.PAGE_START_DEFAULT);
    },
    // ページ変更
    changePage(page) {
      this.getListMailTemplate(page);
    },
    changeSetting() {
      this.selectedBussinessCd = ""
      if (this.isMailTemplate) {
        this.searchMailTemplate = {}
        this.getListMailTemplate(this.PAGE_START_DEFAULT);
      } else {
        this.clearMessageError()
        this.getMailSetting(this.bussinessCd)
      }
    },
    // メールテンプレート詳細
    openMailDialog (mailItem) {
      this.msgTitleError = "";
      this.msgContentError = "";
      this.mailTemplate = {...mailItem}

      this.arrContentDefault = this.mailTemplate.content
        .split('&nbsp;')
        .filter((x) => x.indexOf('{{') > -1)
      this.arrTitleDefault = this.mailTemplate.title
        .split(' ')
        .filter((x) => x.indexOf('{{') > -1)
      this.oldMailTemplate = {...mailItem}
      this.dialogMail = true
    },
    // メールテンプレート更新確認ダイアログを表示する
    showDialogMailTemplateConfirm () {
      if (this.isValidateMailTemplate()) {
        this.loadingBtn = false
        this.dialogConfirm = true
      }
    },
    // メールテンプレート更新
    async updateMailTemplate () {
      this.dialogConfirm = false
      this.loadingBtn = true
      this.mailTemplate.updateUserCd = this.updateUserCd
      const config = {
        method: 'post',
        url: 'api/MailTemplate/UpdateMailTemplate',
        data: this.mailTemplate
      }
      try {
        const resultUpdMailTemplate = await require("axios")(config);
        if (resultUpdMailTemplate.data !== this.RESULT_ERROR) {
          if (resultUpdMailTemplate.data === this.RESULT_SUCCESS) {
            this.$toastr.s(this.msgUpdateSucces);
          } else {
            this.$toastr.w(this.msgUpdtedBefore);
          }
          await this.getListMailTemplate(this.PAGE_START_DEFAULT);
        } else {
          this.$toastr.e(this.msgUpdateFailed);
        }
      } catch (error) {
        this.$toastr.e(this.msgUpdateFailed)
      } finally {
        this.dialogMail = false;
        this.loadingBtn = false;
      }
    },
    // メールテンプレートをバリデーションする
    isValidateMailTemplate () {
      if (JSON.stringify(this.mailTemplate) === JSON.stringify(this.oldMailTemplate)) {
        this.$toastr.w(this.msgNotthingChanged)
        return false
      }

      this.isValidateTitle()

      this.isValidateContent()

      if (this.msgTitleError === '' && this.msgContentError === '') {
        return true
      } else {
        return false
      }
    },
    // メールタイトルをバリデーションする
    isValidateTitle() {
      this.arrTitleSave = this.mailTemplate.title
        .split(' ')
        .filter((x) => x.indexOf('{{') > -1)

      const isCheckTitle =
        this.arrTitleSave.length === this.arrTitleDefault.length &&
        this.arrTitleSave
          .sort()
          .every(
            (value, index) => value === this.arrTitleDefault.sort()[index]
          )

      if (isCheckTitle === false) {
        this.msgTitleError = this.msgCannotChangeVar
      } else {
        this.msgTitleError = ''
      }
    },
    // メール内容をバリデーションする
    isValidateContent() {
      this.arrContentSave = this.mailTemplate.content
        .split('&nbsp;')
        .filter((x) => x.indexOf('{{') > -1)
      
      const isCheckContent =
        this.arrContentSave.length === this.arrContentDefault.length &&
        this.arrContentSave
          .sort()
          .every(
            (value, index) => value === this.arrContentDefault.sort()[index]
          )

      if (isCheckContent === false) {
        this.msgContentError = this.msgCannotChangeVar
      } else {
        this.msgContentError = ''
      }
    },
    // 金融機関情報
    async getMailSetting(bussinessCd) {
      this.mailSetting = {}
      this.oldMailSetting = {}
      this.loading = true;
      const config = {
        method: "get",
        url: "api/MailSetting/GetListMailSetting",
        params: {
          bussinessCd: bussinessCd,
        },
      };
      try {
        const resMailSetting = await require("axios")(config);
        if (resMailSetting.data !== null && resMailSetting.data.length > 0) {
          this.mailSetting = resMailSetting.data[0];

          if (this.selectedBussinessCd != "") {
            this.mailSetting.mailFrom = "";
            this.mailSetting.mailFromName = "";
          } else {
            this.oldMailSetting = {...resMailSetting.data[0]};
          }
        } else {
          this.dialogBussinessCd = true;
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.selectedBussinessCd = "";
        this.loading = false;
      }
    },
    // メール設定更新確認ダイアログを表示する
    showDialogMailSettingConfirm() {
      if (JSON.stringify(this.mailSetting) === JSON.stringify(this.oldMailSetting)) {
        this.$toastr.w(this.msgNotthingChanged);
      } else if (this.isValidateMailSetting()) {
        this.dialogMailSettingConfirm = true;
        this.loadingBtn = false;
      }
    },
    // メール設定更新
    async updateMailSetting() {
      this.loading = true;
      this.loadingBtn = true;
      this.dialogMailSettingConfirm = false;
      this.mailSetting.bussinessCd = this.bussinessCd;
      this.mailSetting.updateUserCd = this.updateUserCd
      const config = {
        method: "post",
        url: "api/MailSetting/UpdateMailSetting",
        data: this.mailSetting
      };
      try {
        const resultUpdMailSetting = await require("axios")(config);
        if (resultUpdMailSetting.data !== this.RESULT_ERROR) {
          if (resultUpdMailSetting.data === this.RESULT_SUCCESS) {
            this.$toastr.s(this.msgUpdateSucces);
          } else {
            this.$toastr.w(this.msgUpdtedBefore);
          }
          await this.getMailSetting(this.bussinessCd);
        } else {
          this.$toastr.e(this.msgUpdateFailed);
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loading = false;
        this.loadingBtn = false;
      }
    },
    // メール設定をバリデーションする
    isValidateMailSetting() {
      this.checkValidMailFrom();
      this.checkValidMailFromName();
      this.checkValidMailBounceAccessKeyID();
      this.checkValidMailBounceSecretAccessKey();
      this.checkValidMailBounceQueueUrl();
      this.checkValidSmtpServer();
      this.checkValidSmtpAccount();
      this.checkValidSmtpPass();

      if (
        this.mailFromError === "" &&
        this.mailFromNameError === "" &&
        this.mailBounceAccessKeyIDError === "" &&
        this.mailBounceSecretAccessKeyError === "" &&
        this.mailBounceQueueUrlError === "" &&
        this.smtpServerError === "" &&
        this.smtpAccountError === "" &&
        this.smtpPassError === ""
      ) {
        return true;
      } else {
        return false;
      }
    },
    clearMessageError() {
      this.mailFromError = "";
      this.mailFromNameError = "";
      this.mailBounceAccessKeyIDError = "";
      this.mailBounceSecretAccessKeyError = "";
      this.mailBounceQueueUrlError = "";
      this.smtpServerError = "";
      this.smtpAccountError = "";
      this.smtpPassError = "";
    }
  },
};
</script>

<style scoped>
.btn-danger {
  color: #fff;
  background-color: #d92550 !important;
  border-color: #d92550;
}

.mail-setting-group {
  padding-top: unset;
  padding-bottom: unset;
}

.label-search,
.label-mail {
  color: #040404;
  font-weight: bold;
}

.fix-padding-search {
  padding-top: 7px;
  padding-bottom: 7px;
}
</style>