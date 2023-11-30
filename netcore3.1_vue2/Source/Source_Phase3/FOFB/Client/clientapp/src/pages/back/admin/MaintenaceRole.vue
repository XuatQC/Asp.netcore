<template>
  <div>
    <VueElementLoading
      :active="loading"
      spinner="ring"
      color="var(--success)"
    />
    <div class="main-card">
      <div class="card card-body role">
        <br />
        <div class="row mb-1">
          <div
              class="col-md-9"
              style="padding-right: 8px; text-align: right; padding-bottom: 20px;"
          >
              <button
                type="button"
                class="btn btn-primary btn-search"
                @click="openRoleDialog(true, null)"
              >
                権限追加
              </button>
          </div>
          <!-- pagination for table Screen start  -->
          <div class="col-md-9 col-lg-9 text-xs-right">
            <div class="row" style="float: right;">
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
                v-if="this.lstRole.length > this.itemsPerPage"
                :total-visible="7"
                v-model="page"
                :length="pageCount"
              ></v-pagination>
            </div>
          </div>
          <!-- pagination for table Screen end  -->
        </div>       
        
        <div class="table-role">
          <v-data-table
            height="580"
            :headers="headersLstRole"
            :items="lstRole"
            item-key="roleId"
            hide-default-footer
            :no-results-text="msgNoData"
            :no-data-text="msgNoData"
            :page.sync="page"
            :items-per-page="itemsPerPage"
            @page-count="pageCount = $event"
            
          >
            <template v-slot:item="{ item }">
              <tr>
                <td>
                  <a
                    v-if="item.roleId !== ROLE_SUPER_ADMIN"
                    @click="openRoleDialog(false, item)"
                    class="router"
                  >
                    {{ item.roleName }}
                  </a>
                  <span v-else>{{ item.roleName }}</span>
                </td>
                <td class="text-left">
                  {{ item.roleDescript }}
                </td>
              </tr>
            </template>
          </v-data-table>
          <!-- add/upd role dialog -->
          <v-dialog v-model="dialogRole" max-width="670px">
            <v-card>
              <v-card-text>
                <div class="main-card">
                  <div>
                    <b-row>
                      <b-col>
                        <div>
                          <!-- RoleName -->
                          <b-row>
                            <b-col lg="2" md="12">
                              <label class="color-black"
                                ><strong>権限名</strong
                                ><span class="text-danger">*</span></label
                              >
                            </b-col>
                            <b-col lg="10" md="12">
                              <b-form-input
                                class="col-lg-9 col-md-12"
                                type="text"
                                v-model="roleDetail.roleName"
                                name="roleName"
                                @keyup="checkValidRoleName()"
                                autocomplete="off"
                              />
                              <span
                                class="invalid-feedback d-block"
                                v-if="roleNameError != ''"
                                >{{ this.roleNameError }}</span
                              >
                            </b-col>
                          </b-row>
                          <!-- roleDescript -->
                          <b-row>
                            <b-col lg="2" md="12">
                              <label class="color-black"
                                ><strong>記述</strong></label
                              >
                            </b-col>
                            <b-col lg="10" md="12">
                              <b-form-input
                                class="col-lg-9 col-md-12"
                                type="text"
                                v-model="roleDetail.roleDescript"
                                name="roleDescript"
                                autocomplete="off"
                              />
                            </b-col>
                          </b-row>
                          <!-- ActionType -->
                          <b-row>
                            <b-col lg="2" md="12">
                              <label class="color-black"
                                ><strong>機能</strong></label
                              >
                            </b-col>
                            <b-col lg="10" md="12" class="action-type-group">
                              <div
                                v-for="(actionType, index) in actionTypeOptions"
                                :key="index"
                              >
                                <v-checkbox
                                  v-model="actionTypeArr"
                                  :value="actionType.value"
                                  :label="actionType.text"
                                  :readonly="
                                    actionType.value == VIEW ? true : false
                                  "
                                ></v-checkbox>
                              </div>
                            </b-col>
                          </b-row>
                        </div>
                      </b-col>
                    </b-row>
                  </div>
                  <!-- bottom button -->
                  <div style="margin-top: 15px">
                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <button
                        class="btn btn-primary"
                        @click="dialogRole = false"
                      >
                        キャンセル
                      </button>
                      <v-spacer></v-spacer>
                      <button
                        @click="showDialogRoleConfirm()"
                        style="width: 90px"
                        class="btn btn-danger"
                      >
                        保存
                      </button>
                      <v-spacer></v-spacer>
                    </v-card-actions>
                  </div>
                </div>
              </v-card-text>
            </v-card>
          </v-dialog>
          <!-- Confirm add/upd role dialog-->
          <v-dialog
            v-model="dialogRoleConfirm"
            max-width="401px"
            content-class="v-dialog-border"
          >
            <v-card>
              <v-card-text>
                <div class="text-center">
                  {{
                    isAddRole ? this.TEXT_ADD : this.TEXT_UPD
                  }}します。よろしいですか。
                </div>
              </v-card-text>
              <v-card-actions style="text-align: center; display: block">
                <v-btn class="btn_confirm_no" @click="dialogRoleConfirm = false"
                  >キャンセル</v-btn
                >
                <v-btn
                  class="btn_confirm_yes"
                  :loading="loadingBtn"
                  @click="submitRole()"
                  >保存</v-btn
                >
              </v-card-actions>
            </v-card>
          </v-dialog>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      loading: false,
      loadingBtn: false,
      isSuperAdmin: null,
      updateUserCd: "",
      isAddRole: null,
      actionTypeArr: [],
      actionTypeOptions: [],
      lstRole: [],
      headersLstRole: [
        {
          text: "権限名",
          sortable: false,
          align: "left",
          width: "10%",
        },
        {
          text: "記述",
          align: "left",
          sortable: false,
          width: "30%",
        },
      ],
      roleDetail: {},
      oldRoleDetail: {},
      dialogRole: false,
      dialogRoleConfirm: false,
      roleNameError: "",
      itemsPerPage: 10,
      page: 1,
      pageCount: 0,
    };
  },
  async created() {
    this.loading = true;
    this.isSuperAdmin =
      this.getUserData().roleId == this.ROLE_SUPER_ADMIN ? true : false;
    if (!this.isSuperAdmin) {
      this.$router.push({ name: "404" });
    }
    this.updateUserCd = this.getUserData().userCd;

    await this.getListRoleDetail();

    await this.getListRole();
  },
  methods: {
    // 権限名バリデーション
    checkValidRoleName() {
      this.roleNameError = this.validateTextRequiredAndLength(
        this.roleDetail.roleName,
        40,
        this.ROLE_NAME
      );
    },
    async getListRoleDetail() {
      let lstRoleDetail = await this.getParameterByCd(this.KBN_ROLE_DETAIL);
      if (lstRoleDetail.length > 0) {
        lstRoleDetail.sort((a, b) => (a.numberValue > b.numberValue ? 1 : -1));
        lstRoleDetail.map((role) => {
          this.actionTypeOptions.push({
            text: role.stringValue,
            value: role.kbnValueName,
          });
        });
      }
    },
    // 権限一覧
    async getListRole() {
      this.loading = true;
      const config = {
        method: "get",
        url: "api/Role/GetAllSub",
      };
      try {
        const resultGetLstRole = await require("axios")(config);
        if (resultGetLstRole.data !== null) {
          this.lstRole = resultGetLstRole.data;
        } else {
          this.$toastr.e(this.msgNoData);
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.loading = false;
      }
    },
    // 権限詳細ダイアログを表示する
    openRoleDialog(isAddRole, itemRole) {
      this.isAddRole = isAddRole;
      this.clearMessageError();

      if (isAddRole) {
        this.roleDetail = {};
        this.actionTypeArr = [this.VIEW];
      } else {
        itemRole.actionTypeArr = itemRole.actionType.split(",");
        itemRole.oldActionTypeArr = itemRole.actionTypeArr;
        this.actionTypeArr = itemRole.actionTypeArr;
        this.roleDetail = { ...itemRole };
        this.oldRoleDetail = { ...itemRole };
      }

      this.dialogRole = true;
    },
    // 確認ダイアログを表示する
    showDialogRoleConfirm() {
      if (
        this.lstRole.filter((x) => x.roleName == this.roleDetail.roleName)
          .length != 0 &&
        this.isAddRole
      ) {
        this.roleNameError = this.msgSameRoleName;
        return;
      }
      this.roleDetail.actionTypeArr = this.actionTypeArr;

      if (!this.isAddRole) {
        if (
          JSON.stringify(this.roleDetail) === JSON.stringify(this.oldRoleDetail)
        ) {
          this.$toastr.w(this.msgNotthingChanged);
          return;
        }
      }

      if (this.isValidateAll()) {
        this.dialogRoleConfirm = true;
        this.loadingBtn = false;
      }
    },
    // 権限の登録・更新
    async submitRole() {
      this.loading = true;
      this.loadingBtn = true;
      this.dialogRole = false;
      this.dialogRoleConfirm = false;
      this.roleDetail.updateUserCd = this.updateUserCd;

      if (this.isAddRole) {
        this.roleDetail.createUserCd = this.updateUserCd;
        const config = {
          method: "post",
          url: "api/Role/InsertRoles",
          data: this.roleDetail,
        };
        try {
          const resultInsertRole = await require("axios")(config);
          if (resultInsertRole.data) {
            this.$toastr.s(this.msgInsertSucces);
          } else {
            this.$toastr.e(this.msgInsertFailed);
          }
          await this.getListRole();
        } catch (error) {
          console.log(error);
        } finally {
          this.loading = false;
          this.loadingBtn = false;
        }
      } else {
        const config = {
          method: "post",
          url: "api/Role/UpdateRoles",
          data: this.roleDetail,
        };
        try {
          const resultUpdRole = await require("axios")(config);
          if (resultUpdRole.data !== this.RESULT_ERROR) {
            if (resultUpdRole.data === this.RESULT_SUCCESS) {
              this.$toastr.s(this.msgUpdateSucces);
            } else {
              this.$toastr.w(this.msgUpdtedBefore);
            }
            await this.getListRole();
          } else {
            this.$toastr.e(this.msgUpdateFailed);
          }
        } catch (error) {
          console.log(error);
        } finally {
          this.loading = false;
          this.loadingBtn = false;
        }
      }
    },
    isValidateAll() {
      this.checkValidRoleName();

      if (this.roleNameError === "") {
        return true;
      } else {
        return false;
      }
    },
    clearMessageError() {
      this.roleNameError = "";
    },
  },
  computed: {
    pageStop() {
      if (this.page < Math.ceil(this.lstRole.length / this.itemsPerPage)) {
        return this.page * this.itemsPerPage;
      } else {
        return this.lstRole.length;
      }
    },
    pageStart() {
      return (this.page - 1) * this.itemsPerPage + 1;
    },
    itemsLength() {
      return this.lstRole.length;
    },
  },
};
</script>

<style scoped>
.role {
  border: unset;
}

.table-role {
  width: 50%;
  margin: auto;
}
</style>
