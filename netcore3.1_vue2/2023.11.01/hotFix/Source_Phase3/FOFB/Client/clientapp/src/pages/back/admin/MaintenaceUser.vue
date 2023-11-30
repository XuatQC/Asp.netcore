<template>
  <div>
    <VueElementLoading
      :active="loading"
      spinner="ring"
      color="var(--success)"
    />
    <div hidden>
      <b-form-file
        id="fileCsv"
        accept=".csv"
        @change="fileCsvChange"
        ref="fileCsv"
      ></b-form-file>
    </div>
    <div>
      <div class="card card-body">
        <div class="row" style="padding-bottom: 8px">
          <div
            class="col-md-2 offset-md-8 btn-grp-search"
            style="text-align: right"
          >
            <button
              type="button"
              class="btn btn-primary btn-search"
              @click="showDialogCsv()"
            >
              CSV登録
            </button>
          </div>
          <div
            class="col-md-2 btn-grp-search"
            style="padding-right: 61px; text-align: right"
          >
            <button
              type="button"
              class="btn btn-primary btn-search"
              @click="showDialogInsertUser()"
            >
              登録
            </button>
          </div>
        </div>
      </div>
      <div>
        <!-- Search bar start -->
        <div class="main-card">
          <div class="card card-body">
            <div class="row">
              <div class="col-md-4">
                <div class="row col-md-12 fix-padding-search">
                  <label class="col-md-4 label-search fix-padding-search"
                    >ユーザーコード</label
                  >
                  <div class="col-md-7 fix-padding-search">
                    <b-form-input
                      v-model="txtuserCode"
                      placeholder=""
                      single-line
                      maxlength="9"
                      hide-details
                    />
                  </div>
                </div>
                <div class="row col-md-12 fix-padding-search">
                  <label class="col-md-4 label-search fix-padding-search"
                    >名前</label
                  >
                  <div class="col-md-7 fix-padding-search">
                    <b-form-input
                      v-model="txtuserName"
                      placeholder=""
                      single-line
                      maxlength="40"
                      hide-details
                    />
                  </div>
                </div>
              </div>
              <div class="col-md-4">
                <div class="row col-md-12 fix-padding-search">
                  <label class="col-md-4 label-search fix-padding-search"
                    >メールアドレス</label
                  >
                  <div class="col-md-7 fix-padding-search">
                    <b-form-input
                      v-model="txtMail"
                      placeholder=""
                      single-line
                      maxlength="50"
                      hide-details
                    />
                  </div>
                </div>
                <div class="row col-md-12 fix-padding-search">
                  <label class="col-md-4 label-search fix-padding-search"
                    >権限名</label
                  >
                  <div class="col-md-7 fix-padding-search">
                    <b-form-select
                      v-model="searchRole"
                      :options="roleOptionSearch"
                    ></b-form-select>
                  </div>
                </div>
              </div>
              <div class="col-md-4">
                <div class="row col-md-12 btn-grp-search">
                  <div
                    class="col-md-4 offset-md-4"
                    style="text-align: right; padding-right: unset;"
                  >
                    <button
                      type="button"
                      @click="search()"
                      class="btn btn-primary btn-search"
                    >
                      検索
                    </button>
                  </div>
                  <div class="col-md-4" style="text-align: right;">
                    <button
                      type="button"
                      class="btn btn-secondary btn-clear"
                      @click="clearSearch()"
                    >
                      クリア
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- Search bar end -->
        <div class="main-card">
          <div class="card card-body">
            <!-- pagination for table user start  -->
            <div class="row mb-2 mt-3">
              <div class="col-md-12 col-lg-12 text-xs-right">
                <div class="row" style="float: left; padding-left: 15px">
                  <button
                    @click="showDialogConfirmDeleteUser()"
                    class="btn btn-danger"
                  >
                    削除
                  </button>
                </div>
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
                    v-if="this.lstUser.length > this.itemsPerPage"
                    :total-visible="7"
                    v-model="page"
                    :length="pageCount"
                  ></v-pagination>
                </div>
              </div>
            </div>
            <!-- pagination for table user end  -->

            <!-- table data user start  -->
            <v-data-table
              :headers="headersTblUser"
              :items="lstUser"
              hide-default-footer
              :page.sync="page"
              show-select
              item-key="userCd"
              v-model="selectedUser"
              :items-per-page="itemsPerPage"
              @page-count="pageCount = $event"
              :loading="tbluserLoading"
              no-results-text="データがありません。"
              no-data-text="データがありません。"
              @update:options="changeSort"
            >
              <template v-slot:item="{ item }">
                <tr>
                  <td>
                    <v-checkbox
                      class="pa-0 ma-0"
                      :disabled="currentUser == item.userCd"
                      :ripple="true"
                      v-model="selectedUser"
                      :value="item"
                      hide-details
                    >
                    </v-checkbox>
                  </td>
                  <td class="text-left">
                    <strong
                      ><a @click="showDialogUpdateUser(item)" class="router">{{
                        item.userCd
                      }}</a></strong
                    >
                  </td>
                  <td class="text-left">{{ item.userNameKanji }}</td>
                  <td class="text-left">{{ item.userNameKana }}</td>
                  <td class="text-left">{{ item.emailAddress }}</td>
                  <td class="text-left">{{ item.roleName }}</td>
                </tr>
              </template>
            </v-data-table>
            <!-- table data user end  -->
          </div>
        </div>

        <!--  dialog insert update user start -->
        <v-dialog v-model="dialogInsertUpdateUser" max-width="600px">
          <v-card>
            <v-card-title>
              <span class="headline">ユーザー情報</span>
            </v-card-title>
            <v-card-text>
              <!--- User code-->
              <b-row>
                <b-col lg="3" md="12" xl="4">
                  <label class="col-md-12"
                    ><strong>ユーザーコード</strong
                    ><span class="text-danger"> *</span></label
                  >
                </b-col>
                <b-col xl="6" lg="5" md="12">
                  <b-form-input
                    name="txtUserCd"
                    type="text"
                    v-validate="'required'"
                    v-model="dataUser.userCd"
                    maxlength="8"
                    placeholder=""
                    autocomplete="off"
                    style="max-width: 100px;"
                    :disabled="!isModeInsert"
                  />
                  <span class="invalid-feedback d-block">{{
                    errors.first("txtUserCd")
                  }}</span>
                  <span
                    v-if="
                      (dataUser.userCd != undefined &&
                        dataUser.userCd.length < 8 &&
                        dataUser.userCd.length > 0) ||
                        (!new RegExp(this.REGEX_URL).test(dataUser.userCd) &&
                          dataUser.userCd.length > 0)
                    "
                    class="invalid-feedback d-block"
                    >{{ msgMinTextUserCd }}</span
                  >
                </b-col>
              </b-row>
              <!--- Kanji name-->
              <b-row>
                <b-col lg="3" md="12" xl="4">
                  <label class="col-md-12"
                    ><strong>名前（漢字）</strong
                    ><span class="text-danger">*</span></label
                  >
                </b-col>
                <b-col xl="4" lg="5" md="12">
                  <b-form-input
                    name="txtUserNameKanji"
                    type="text"
                    v-validate="'required'"
                    v-model="dataUser.userNameKanji"
                    placeholder=""
                    maxlength="40"
                    autocomplete="off"
                  />
                  <span class="invalid-feedback d-block">{{
                    errors.first("txtUserNameKanji")
                  }}</span>
                </b-col>
              </b-row>
              <!--- Kana name-->
              <b-row>
                <b-col lg="3" md="12" xl="4">
                  <label class="col-md-12"
                    ><strong>名前（かな）</strong
                    ><span class="text-danger">*</span></label
                  >
                </b-col>
                <b-col xl="8" lg="5" md="12">
                  <b-form-input
                    name="txtUserNameKana"
                    type="text"
                    v-model="dataUser.userNameKana"
                    @keyup="checkValidKana()"
                    placeholder=""
                    style="max-width: 150px;"
                    maxlength="40"
                    autocomplete="off"
                  />
                  <span
                    class="invalid-feedback d-block"
                    v-if="kanaFirstError != ''"
                    >{{ this.kanaFirstError }}</span
                  >
                </b-col>
              </b-row>
              <!--- PassWord-->
              <b-row>
                <b-col lg="3" md="12" xl="4">
                  <label class="col-md-12"
                    ><strong>パスワード</strong
                    ><span class="text-danger">*</span></label
                  >
                </b-col>
                <b-col v-if="isModeInsert" xl="8" lg="5" md="12">
                  <b-input-group>
                    <template v-slot:append>
                      <b-input-group-text>
                        <i
                          style="margin-top:-3px"
                          id="pass-status"
                          aria-hidden="true"
                          @click="viewPassword"
                        >
                          <v-icon>{{ showPassWord }}</v-icon>
                        </i>
                      </b-input-group-text>
                    </template>
                    <b-form-input
                      type="password"
                      name="txtPassWord"
                      id="txtPassWord"
                      maxlength="32"
                      v-model="dataUser.password"
                      v-validate="'required'"
                      style="max-width: 150px;"
                      placeholder="********"
                      ref="password"
                    />
                  </b-input-group>
                  <span class="invalid-feedback d-block">{{
                    errors.first("txtPassWord")
                  }}</span>
                  <span
                    v-if="
                      (dataUser.password != undefined &&
                        dataUser.password.length < 8 &&
                        dataUser.password.length > 0) ||
                        (!new RegExp(this.REGEX_URL).test(dataUser.password) &&
                          dataUser.password.length > 0)
                    "
                    class="invalid-feedback d-block"
                    >{{ msgMinTextPassWord }}</span
                  >
                </b-col>
                <b-col v-else xl="6" lg="6" md="12">
                  <button
                    class="btn btn-primary btn-reset-pass mb-3"
                    :disabled="loadingBtn"
                    @click="dialogConfirmResetPassWord = true"
                  >
                    パスワードリセット
                    <p>(ユーザーコードと同じにする）</p>
                  </button>
                </b-col>
              </b-row>
              <!--- Role -->
              <b-row>
                <b-col lg="3" md="12" xl="4">
                  <label class="col-md-12"
                    ><strong>権限名</strong
                    ><span class="text-danger"> *</span></label
                  >
                </b-col>
                <b-col xl="4" lg="5" md="12">
                  <b-form-select
                    v-model="dataUser.roleId"
                    :options="roleOption"
                  ></b-form-select>
                </b-col>
                <b-col xl="4" lg="5" md="12">
                  <strong
                    ><a @click="showDialogRoleDetail()" class="router"
                      >権限詳細</a
                    ></strong
                  >
                </b-col>
              </b-row>
              <!--- Mail address -->
              <b-row>
                <b-col lg="3" md="12" xl="4">
                  <label class="col-md-12"
                    ><strong>メールアドレス</strong
                    ><span class="text-danger"> *</span></label
                  >
                </b-col>
                <b-col xl="8" lg="5" md="12">
                  <b-form-input
                    name="txtEmailAddress"
                    type="text"
                    v-model="dataUser.emailAddress"
                    placeholder=""
                    @keyup="checkValidMailAddress()"
                    maxlength="50"
                    style="max-width: 230px;"
                    autocomplete="off"
                  />
                  <span
                    class="invalid-feedback d-block mb-3"
                    v-if="mailAddressError != ''"
                    >{{ this.mailAddressError }}</span
                  >
                </b-col>
              </b-row>
              <div>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <button
                    class="btn btn-primary"
                    @click="dialogInsertUpdateUser = false"
                  >
                    キャンセル
                  </button>
                  <v-spacer></v-spacer>
                  <button
                    @click="showDialogConfirmUpdateInsertUser()"
                    style="width: 90px"
                    class="btn btn-danger"
                  >
                    保存
                  </button>
                  <v-spacer></v-spacer>
                </v-card-actions>
              </div>
            </v-card-text>
          </v-card>
        </v-dialog>
        <!--  dialog insert update user end -->

        <!--  dialog confirm insert update user start -->
        <v-dialog
          v-model="dialogConfirmUpdateInsertUser"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div v-if="isModeInsert" class="text-center">
                登録します。よろしいですか。
              </div>
              <div v-else class="text-center">変更します。よろしいですか。</div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn
                class="btn_confirm_no"
                @click="dialogConfirmUpdateInsertUser = false"
                >キャンセル</v-btn
              >
              <v-btn class="btn_confirm_yes" @click="insertUpdateUser()"
                >保存</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!--  dialog confirm insert update user end -->

        <!--  dialog confirm delete update user start -->
        <v-dialog
          v-model="dialogConfirmDeleteUser"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="text-center">
                ユーザが削除されます。よろしいですか。
              </div>
              <div class="text-center">
                この操作後は元に戻すことはできません。
              </div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn
                class="btn_confirm_no"
                @click="dialogConfirmDeleteUser = false"
                >キャンセル</v-btn
              >
              <v-btn class="btn_confirm_yes" @click="deleteUser()">削除</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!--  dialog confirm delete update user end -->

        <!--  dialog confirm reset password user start -->
        <v-dialog
          v-model="dialogConfirmResetPassWord"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="text-center">パスワードをリセットします。よろしいですか。</div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn
                class="btn_confirm_no"
                @click="dialogConfirmResetPassWord = false"
                >キャンセル</v-btn
              >
              <v-btn class="btn_confirm_yes" @click="resetPass()">保存</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!--  dialog confirm reset password user user end -->

        <!-- Dialog csv start -->
        <v-dialog v-model="dialogCsv" max-width="1500px">
          <VueElementLoading
            :active="loadingPopup"
            spinner="ring"
            color="var(--success)"
          />
          <v-card>
            <div class="row">
              <div class="col" style="text-align: left;margin-bottom: -15px;">
                <button
                  @click="importCsv()"
                  style="margin:20px"
                  class="btn btn-danger"
                >
                  CSVファイル選択
                </button>
              </div>
            </div>
            <span class="text-danger ml-5"
              >※選択している業態のデータを読み込むので、注意してください。</span
            >
            <div class="row mb-1">
              <div
                class="col-md-12 col-lg-12 text-xs-right"
                v-if="dataCsv.length > 0"
              >
                <div class="row" style="float: right; padding-right: 6px">
                  <p
                    style="
                      margin-bottom: unset;
                      line-height: 3;
                      padding-right: 10px;
                    "
                  >
                    全 {{ itemsLengthCsv }} 件中 {{ pageStartCsv }} 件 〜
                    {{ pageStopCsv }} 件を表示
                  </p>
                  <v-pagination
                    v-if="dataCsv.length > this.itemsPerPage"
                    :total-visible="7"
                    @input="changePageCsv()"
                    v-model="pageCsv"
                    :length="pageCountCsv"
                  ></v-pagination>
                </div>
              </div>
              <div v-else style="height: 42px"></div>
            </div>
            <!-- table data user start  -->
            <v-data-table
              class="table-csv"
              :headers="headersTblUser"
              :items="dataCsv"
              hide-default-footer
              show-select
              :page.sync="pageCsv"
              :items-per-page="itemsPerPage"
              @page-count="pageCountCsv = $event"
              item-key="userCd"
              v-model="selectedCsv"
              no-results-text=""
              no-data-text=""
              @toggle-select-all="selectAllCsv"
              @update:options="changeSortCsv"
            >
              <template v-slot:item="{ item }">
                <tr>
                  <td v-if="item.txtError === null" class="text-center">
                    <input
                      class="item-checkbox"
                      type="checkbox"
                      :ripple="false"
                      v-model="selectedCsv"
                      :value="item"
                      hide-details
                    />
                  </td>
                  <td class="text-center" v-else>
                    <button
                      @click="showError(item.txtError)"
                      class="btn btn-danger"
                    >
                      エラー
                    </button>
                  </td>
                  <td class="text-left">{{ item.userCd }}</td>
                  <td class="text-left">{{ item.userNameKanji }}</td>
                  <td class="text-left">{{ item.userNameKana }}</td>
                  <td class="text-left">{{ item.emailAddress }}</td>
                  <td class="text-left">{{ item.roleName }}</td>
                </tr>
              </template>
            </v-data-table>
            <!-- table data user end  -->
            <!-- bottm button-->
            <div style="margin-top: 10px;">
              <v-card-actions>
                <v-spacer></v-spacer>
                <button class="btn btn-primary" @click="dialogCsv = false">
                  キャンセル
                </button>
                <v-spacer></v-spacer>
                <button
                  @click="showDialogConfirmInsertCsv()"
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
        <!-- Dialog csv end -->

        <!--  dialog confirm import csv start -->
        <v-dialog
          v-model="dialogConfirmInsertCsv"
          max-width="401px"
          content-class="v-dialog-border"
        >
          <v-card>
            <v-card-text>
              <div class="text-center">登録します。よろしいですか。</div>
            </v-card-text>
            <v-card-actions style="text-align: center; display: block">
              <v-btn
                class="btn_confirm_no"
                @click="dialogConfirmInsertCsv = false"
                >キャンセル</v-btn
              >
              <v-btn class="btn_confirm_yes" @click="insertCsv()">保存</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!--  dialog confirm import csv end -->

        <!--  dialog show Message Error start -->
        <v-dialog v-model="dialogError" max-width="700px">
          <v-card>
            <v-card-title>
              <span class="headline">エラー一覧</span>
            </v-card-title>
            <v-card-text>
              <h6 class="memo_newLine text-danger">{{ currentMessError }}</h6>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <button class="btn btn-primary" @click="dialogError = false">
                キャンセル
              </button>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!--  dialog show Message Error end -->

        <!--  dialog RoleDetail start -->
        <v-dialog v-model="dialogRoleDetail" max-width="700px">
          <v-card>
            <v-card-title>
              <span class="headline">{{ selectedRole }}</span>
            </v-card-title>
            <v-card-text>
              <div
                v-for="roleDetail in this.lstRoleDetail"
                :key="roleDetail.kbnValue"
                class="action-type-group"
              >
                <v-checkbox
                  readonly
                  v-model="selectedRoleDetail"
                  :label="roleDetail.stringValue"
                  :value="roleDetail.kbnValueName"
                ></v-checkbox>
              </div>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <button class="btn btn-primary" @click="dialogRoleDetail = false">
                キャンセル
              </button>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <!--  dialog RoleDetail end -->
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data: () => ({
    selectedRoleDetail: [],
    selectedRole: "",
    currentMessError: "",
    dialogError: false,
    dataUser: {},
    oldDataUser: {},
    txtuserCode: "",
    txtuserName: "",
    txtMail: "",
    searchRole: 0,
    pageCsv: 1,
    dialogConfirmResetPassWord: false,
    dialogCsv: false,
    dialogConfirmUpdateInsertUser: false,
    dialogConfirmDeleteUser: false,
    dialogInsertUpdateUser: false,
    loading: false,
    tbluserLoading: true,
    showPassWord: "mdi-eye-off",
    page: 1,
    pageCount: 0,
    pageCountCsv: 0,
    itemsPerPage: 10,
    lstUser: [],
    lstUserAll: [],
    lstUserSearch: [],
    selectedUser: [],
    roleOption: [],
    selectedCsv: [],
    mailAddressError: "",
    kanaFirstError: "",
    roleOptionSearch: [{ text: "", value: 0 }],
    dataCsv: [],
    isModeInsert: false,
    loadingPopup: false,
    loadingBtn: false,
    dialogConfirmInsertCsv: false,
    headersTblUser: [
      {
        text: "ユーザーコード",
        width: "10%",
        value: "userCd",
        align: "left",
      },
      {
        text: "名前（漢字）",
        value: "userNameKanji",
        width: "20%",
        align: "left",
      },
      {
        text: "名前（かな）",
        sortable: true,
        width: "20%",
        align: "left",
        value: "userNameKana",
      },
      {
        text: "メールアドレス",
        value: "emailAddress",
        sortable: false,
        width: "20%",
        align: "left",
      },
      {
        text: "権限名",
        sortable: true,
        width: "20%",
        align: "left",
        value: "roleName",
      },
    ],
    lstRoleDetail: [],
    dialogRoleDetail: false,
    isChangePage: true,
  }),
  async created() {
    this.currentUser = this.getUserData().userCd;
    this.bussinessCd = this.getUserData().bussinessCd;
    await this.getAllUsers();
    await this.getRolesOptions();
    await this.getRolesDetail();
    this.loading = false;
  },
  methods: {
    //権限詳細一覧
    async getRolesDetail() {
      const config = {
        method: "get",
        url: "api/Common/GetCbbByKbnCd",
        params: {
          kbnCd: this.KBN_ROLE_DETAIL,
        },
      };
      try {
        const lstRoleDetail = await this.axios(config);
        if (lstRoleDetail.data != null) {
          this.lstRoleDetail = lstRoleDetail.data;
        }
      } catch (error) {
        console.log(error);
      }
    },
    //権限名フォーマット
    formatRoleName(roleId) {
      try {
        return this.roleOption.find((x) => x.value == roleId).text;
      } catch {
        return roleId;
      }
    },
    // メールアドレスバリデーション
    checkValidMailAddress() {
      this.mailAddressError = this.validateMailAddressForAdmin(
        this.dataUser.emailAddress,
        50,
        this.MAIL_ADDRESS
      );
    },
    // 名（ひらがな）バリデーション
    checkValidKana() {
      this.kanaFirstError = this.validateKanaNameForAdmin(
        this.dataUser.userNameKana,
        40,
        this.KANA_NAME
      );
    },
    //ソート順
    changeSort() {
      let select = [...this.selectedUser];
      this.selectedUser = [];
      this.selectedUser = select;
    },
    //パスワードを見る
    viewPassword() {
      var passwordInput = document.getElementById("txtPassWord");
      if (passwordInput.type === "password") {
        this.showPassWord = "mdi-eye";
        passwordInput.type = "text";
      } else {
        passwordInput.type = "password";
        this.showPassWord = "mdi-eye-off";
      }
    },
    //ユーザー追加・編集の確認ダイアログを表示する
    async showDialogConfirmUpdateInsertUser() {
      if (!this.isModeInsert && JSON.stringify(this.dataUser) === JSON.stringify(this.oldDataUser)) {
        this.$toastr.w(this.msgNotthingChanged);
        return;
      }

      const error = await this.$validator.validateAll();
      this.checkValidMailAddress();
      this.checkValidKana();
      if (this.mailAddressError !== "" || this.kanaFirstError !== "") {
        return;
        }
        if (this.dataUser.userCd.length < 8) {
          return;
      }
      if (error) {
        this.dialogConfirmUpdateInsertUser = true;
      }
    },
    //ユーザー削除
    showDialogConfirmDeleteUser() {
      if (this.selectedUser.length === 0) {
        this.$toastr.w(this.msgUserNotExit);
        return;
      }
      this.dialogConfirmDeleteUser = true;
    },
    //条件によるRoles一覧を取得する
    async getRolesOptions() {
      const config = {
        method: "get",
        url: "api/Role/GetAll",
      };
      const res = await require("axios")(config);

      if (res.data !== null) {
        res.data.forEach((role) => {
          this.roleOption.push({
            text: role.roleName,
            value: role.roleId,
          });
          this.roleOptionSearch.push({
            text: role.roleName,
            value: role.roleId,
          });
        });
      } else {
        this.$toastr.e(res.data.error);
      }
    },
    //ユーザー編集のダイアログを表示する
    async showDialogUpdateUser(user) {
      this.isModeInsert = false;
      this.dataUser = { ...user };
      this.oldDataUser = { ...user };
      this.$validator.reset();
      this.mailAddressError = "";
      (this.kanaFirstError = ""), (this.dialogInsertUpdateUser = true);
    },
    //ユーザー追加・編集
    async insertUpdateUser() {
      let apiUrl = "AddAsync";
      this.dialogConfirmUpdateInsertUser = false;
      if (this.isModeInsert) {
        let userCheck = this.lstUserAll.filter(
          (x) => x.userCd == this.dataUser.userCd
        );
        if (userCheck.length != 0) {
          if (userCheck[0].delFlg) {
            this.$toastr.e(this.msgUserDeleted);
          } else {
            this.$toastr.e(this.msgUserExited);
          }
          return;
        }
        this.dataUser.createUserCd = this.currentUser;
      } else {
        this.dataUser.updateUserCd = this.currentUser;
        this.dataUser.bussinessCd = this.bussinessCd;
        apiUrl = "UpdateAsync";
      }
      this.loading = true;
      this.displayDialogProduct = true;
      this.dialogInsertUpdateUser = false;
      const config = {
        method: "post",
        url: "api/MAdminUser/" + apiUrl,
        data: this.dataUser,
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      try {
        const resInsUpd = await require("axios")(config);
        if (this.isModeInsert) {
          if (resInsUpd.data !== 0) {
            this.$toastr.s(this.msgInsertSucces);
            await this.getAllUsers();
            this.search();
          } else {
            this.$toastr.e(this.msgInsertFailed);
          }
        } else {
          if (resInsUpd.data) {
            this.$toastr.s(this.msgUpdateSucces);
            await this.getAllUsers();
            this.search();
          } else {
            this.$toastr.e(this.msgUpdateFailed);
          }
        }
      } catch (error) {
        console.log(error);
      }
      this.loading = false;
    },
    //ユーザー削除
    async deleteUser() {
      this.dialogConfirmDeleteUser = false;
      this.loading = true;
      const config = {
        method: "post",
        url: "api/MAdminUser/DeleteAsync",
        data: this.selectedUser,
      };
      try {
        const resDelUser = await require("axios")(config);
        if (resDelUser.data) {
          this.$toastr.s(this.msgDeleteSucces);
          await this.getAllUsers();
          this.search();
        } else {
          this.$toastr.e(this.msgDeleteFailed);
        }
      } catch (error) {
        console.log(error);
      }
      this.loading = false;
    },
    //条件によるUsers一覧を取得する
    async getAllUsers() {
      this.loading = true;
      const config = {
        method: "get",
        url: "api/MAdminUser/GetListUserMaintain",
        params: {
          bussinessCd: this.bussinessCd,
        },
      };
      const res = await require("axios")(config);
      if (res.data !== null) {
        this.lstUserAll = [...res.data];
        res.data = res.data.filter(
          (x) =>
            x.delFlg == 0 &&
            (x.bussinessCd == this.bussinessCd || x.bussinessCd == null)
        );
        this.lstUser = res.data;
        this.lstUserSearch = res.data;
        this.tbluserLoading = false;
      } else {
        this.$toastr.e(res.data.error);
      }
      this.selectedUser = [];
    },
    //ユーザー追加のダイアログを表示する
    showDialogInsertUser() {
      this.dataUser = {};
      this.mailAddressError = "";
      this.kanaFirstError = "";
      this.$validator.reset();
      this.dataUser.roleId = this.roleOption[0].value;
      this.isModeInsert = true;
      this.dialogInsertUpdateUser = true;
    },
    //検索クリア
    clearSearch() {
      this.txtuserCode = "";
      this.txtuserName = "";
      this.txtMail = "";
      this.searchRole = 0;
      this.search();
    },
    //検索
    search() {
      this.selectedUser = [];
      this.lstUser = [...this.lstUserSearch];
      this.lstUser = this.lstUser.filter(
        (x) =>
          x.userCd.includes(this.txtuserCode) &&
          (x.userNameKanji.includes(this.txtuserName) ||
            x.userNameKana.includes(this.txtuserName)) &&
          x.emailAddress.includes(this.txtMail)
      );
      if (this.searchRole != 0) {
        this.lstUser = this.lstUser.filter((x) => x.roleId == this.searchRole);
      }
    },
    //CSVファイルからユーザーを追加する
    importCsv() {
      document.getElementById("fileCsv").click();
    },
    fileCsvChange(e) {
      if (e.target.files[0] != null) {
        this.fileCsv = e.target.files[0];
        this.readDataCsv();
      }
      this.$refs["fileCsv"].reset();
    },
    //csvファイルデータを読み込む
    async readDataCsv() {
      try {
        this.dataCsv = [];
        this.loadingPopup = true;
        const formData = new FormData();
        formData.append("file", this.fileCsv);
        formData.append("bussinessCd", this.bussinessCd);
        const resDataCsv = await this.axios.post(
          "api/MAdminUser/CheckDataCsv",
          formData,
          {
            headers: {
              "Content-type": "multipart/form-data",
            },
          }
        );
        if (resDataCsv.data.error === undefined) {
          if (resDataCsv.data != null) {
            resDataCsv.data = resDataCsv.data.filter(
              (x) => x.bussinessCd == this.bussinessCd || x.bussinessCd == null
            );
            this.dataCsv = resDataCsv.data;
            this.dataCsv.map((data) => {
              let dataExist = this.dataCsv.filter(
                (x) => x.userCd == data.userCd
              );
              if (dataExist.length > 1) {
                data.txtError =
                  data.txtError == null
                    ? this.msgUserSameCode
                    : (data.txtError += this.msgUserSameCode);
              }
            });
          }
        } else {
          this.$toastr.e(resDataCsv.data.error);
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loadingPopup = false;
      }
    },
    //csv取込ダイアログを表示する
    showDialogCsv() {
      this.selectedCsv = [];
      this.dialogCsv = true;
      this.dataCsv = [];
      this.loading = false;
      this.loadingPopup = false;
    },
    //csv取込ダイアログを表示する
    showDialogConfirmInsertCsv() {
      if (this.dataCsv.length <= 0 || this.selectedCsv.length == 0) {
        this.$toastr.w(this.msgUserNotExit);
        return;
      }
      this.dialogConfirmInsertCsv = true;
    },
    //CSVファイルからユーザーを追加する
    async insertCsv() {
      this.dialogConfirmInsertCsv = false;
      this.loadingPopup = true;
      this.selectedCsv = this.selectedCsv.filter((x) => x.txtError == null)
      let lstUserCdUpd = "";
      this.selectedCsv.map((dataCsv) => {
        if (!dataCsv.isAdd) {
          lstUserCdUpd += "'" + dataCsv.userCd + "',";
        }
        dataCsv.createUserCd = this.currentUser;
      })

      if (lstUserCdUpd !== "") {
        lstUserCdUpd = lstUserCdUpd.substring(0 , lstUserCdUpd.length -1);
      }

      let csvSubmit = {
        mUserSubmit: this.selectedCsv,
        listUserCd: lstUserCdUpd
      };

      const config = {
        method: "post",
        url: "api/MAdminUser/InsertCsv",
        data: csvSubmit,
      };
      try {
        const res = await require("axios")(config);
        if (res.data > 0) {
          this.$toastr.s(this.msgInsertSucces);
        } else {
          this.$toastr.e(this.msgInsertFailed);
        }
        this.dialogCsv = false;
      } catch (error) {
        this.$toastr.e(this.msgInsertFailed);
      } finally {
        await this.getAllUsers();
        this.search();
        this.loading = false;
        this.loadingPopup = false;
      }
    },
    //エラーを表示する
    showError(error) {
      this.dialogError = true;
      this.currentMessError = error;
    },
    //パスワードリセット
    async resetPass() {
      this.dialogConfirmResetPassWord = false;
      this.loadingBtn = true;
      try {
        const resUpdatePass = await this.resetPassword(this.dataUser.userCd, this.currentUser);
        if (resUpdatePass == this.RESULT_SUCCESS) {
          this.$toastr.s(this.msgUpdateSucces);
        } else {
          this.$toastr.e(this.msgUpdateFailed);
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loadingBtn = false;
      }
    },
    //権限詳細ダイアログを表示する
    async showDialogRoleDetail() {
      this.loading = true;
      this.selectedRoleDetail = [];
      this.selectedRole = this.roleOption.find(
        (x) => x.value == this.dataUser.roleId
      ).text;
      try {
        const config = {
          method: "get",
          url: "api/RoleDetail/GetAll",
          params: {
            roleId: this.dataUser.roleId,
          },
        };
        const res = await require("axios")(config);

        if (res.data !== null) {
          this.selectedRoleDetail = res.data;
          this.selectedRoleDetail = this.selectedRoleDetail.map(
            (x) => x.actionType
          );
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loading = false;
        this.dialogRoleDetail = true;
      }
    },
    selectAllCsv() {
      if (this.selectedCsv.length !== this.dataCsv.length) {
        this.selectedCsv = this.dataCsv;
      } else {
        this.selectedCsv = [];
      }
    },
    // ページ変更
    changePageCsv() {
      this.isChangePage = false;
    },
    //Csvソート順
    changeSortCsv() {
      if (this.isChangePage) {
        this.selectedCsv = [];
      } else {
        this.selectedCsv = [...this.selectedCsv];
        this.isChangePage = true;
      }
    },
  },
  computed: {
    pageStop() {
      if (this.page < Math.ceil(this.lstUser.length / this.itemsPerPage)) {
        return this.page * this.itemsPerPage;
      } else {
        return this.lstUser.length;
      }
    },
    pageStart() {
      if (this.lstUser.length == 0) {
        return 0;
      } else {
        return (this.page - 1) * this.itemsPerPage + 1;
      }
    },
    itemsLength() {
      return this.lstUser.length;
    },
    pageStopCsv() {
      if (this.pageCsv < Math.ceil(this.dataCsv.length / this.itemsPerPage)) {
        return this.pageCsv * this.itemsPerPage;
      } else {
        return this.dataCsv.length;
      }
    },
    pageStartCsv() {
      if (this.dataCsv.length == 0) {
        return 0;
      } else {
        return (this.pageCsv - 1) * this.itemsPerPage + 1;
      }
    },
    itemsLengthCsv() {
      return this.dataCsv.length;
    },
  },
};
</script>
<style>
.memo_newLine {
  white-space: pre-line;
  word-wrap: break-word;
}
.fix-padding-search {
  padding-top: 7px;
  padding-bottom: 7px;
}

.btn-reset-pass:disabled {
  background-color: #007bff !important;
}
</style>
