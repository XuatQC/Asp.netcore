// import moment from 'moment'

var commonCallApi = {
  data: function () {
    return {

    }
  },
  methods: {
    async getConfigScreen(screenType, bussinessCd) {
      const config = {
        method: 'get',
        url: 'api/FrontScreen/GetListDetailScreen',
        params: {
          screenType: screenType,
          bussinessCd: bussinessCd
        }
      }
      try {
        const res = await require('axios')(config)
        return res.data
      } catch (error) {
        console.log(error)
      }
    },
    async getListBussiness() {
      const config = {
        method: 'get',
        url: 'api/MBussiness/GetAll'
      }
      try {
        const lstBussiness = await require("axios")(config)
        return lstBussiness.data
      } catch (error) {
        console.log(error);
      }
    },
    async getConfigScreenWithProductCd(screenType, bussinessCd,productCd) {
      const config = {
        method: 'get',
        url: 'api/FrontScreen/GetListDetailScreenWithProductCd',
        params: {
          screenType: screenType,
          bussinessCd: bussinessCd,
          productCd: productCd
        }
      }
      try {
        const res = await require('axios')(config)
        return res.data
      } catch (error) {
        console.log(error)
      }
    },
    async chkTimeOpenShop(bussinessCd) {
      const config = {
        method: 'get',
        url: 'api/DatetimeSetting/IsValidOrderDateTime',
        params: {
          bussinessCd: bussinessCd
        }
      }
      try {
        const resPoneChkDateTime = await require('axios')(config)
        if (resPoneChkDateTime.data !== null) {
          return resPoneChkDateTime.data
        }
      } catch (error) {
        console.log(error)
      }
    },
    async getParameterByCd (paraCd) {
      const getParam = await this.axios.get(
        'api/Common/GetCbbByKbnCd?kbnCd=' + paraCd
      )
      return getParam.data
    },
    async getSizeByCd (sizeCd) {
      const getParam = await this.axios.get(
        'api/MSize/GetSizeByCd?sizeCd=' + sizeCd
      )
      return getParam.data
    },
    async resetPassword(password, updateUserCd) {
      const config = {
        method: "post",
        url: "api/MuserLogin/ResetPassword",
        params: {
          password: password,
          updateUserCd: updateUserCd
        },
      };
      try {
        const resUpdatePass = await require('axios')(config)
        return resUpdatePass.data
      } catch (error) {
        console.log(error)
      }
    },
    // 郵便番号による郵便一覧
    async getListMPostCodeByZipcode(zipCode) {
      const config = {
        method: "get",
        url: "api/Order/GetListMPostCode",
        params: {
          zipCode: zipCode,
        },
      };
      try {
        const lstMPostCode = await require("axios")(config);
        if (lstMPostCode.data !== null) {
          return lstMPostCode.data;
        } else {
          return null;
        }
      } catch (error) {
        console.log(error);
      }
    }
  }
}
export default commonCallApi
