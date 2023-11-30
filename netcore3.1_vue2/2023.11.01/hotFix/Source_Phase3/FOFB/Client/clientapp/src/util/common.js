var common = {
  data: function () {
    return {
      msgQualityWrong: '数量を選択してください。',
      msgOutOfPrice: '一度に予約できる金額を超えています',
      msgQualityBiggerInventory: '在庫数が足りません。数量を再度ご入力ください。',
      msgProductOutOfStock:'在庫切れ',
      msgPleaseSelect: '*を選択してください。',
      msgAreaOrShopPleaseSelect: 'エリアと店舗を選択してください',
      msgPleaseInput: '*を入力してください。',
      msgInputFormat: '*のフォーマットが正しくありません。',
      msgLengthTooLong: '*$length文字以内で入力してください。',
      msgTextInputFailed: '特殊文字は禁止です。',
      msgPhoneNumberFormat: '*は半角数字を入力してください。',
      msgMailAddressMatched: '*が一致しません。',
      msgZipCodeExist: '郵便番号が存在しません。',
      msgShopCdExist: '店舗コードが既に存在しています。',
      msgShopNotSelected: '店舗がまだ選択されていません。',
      msgNotthingChanged: '変更がありません。',
      msgInsertSucces: '新規追加に成功しました。',
      msgInsertFailed: '新規追加に失敗しました。',
      msgErrURL: '英数字を入力してください。',
      //______________ Login err msg _____________________
      msgSessionExpire: 'セッションが切れています。',
      msgRelogin: '再度ログインしてください。',
      msgUserInforChanged: '個人情報が変更されましたので',
      //______________ End Login err msg __________________
      msgNotChooseOrderId: '対象レコードを選択してください。',
      msgSendMailSucces: 'メール送信に成功しました。',
      msgSendMailFailed: 'メール送信に失敗しました。',
      msgCancelSucces: 'キャンセルに成功しました。',
      msgCancelFailed: 'キャンセルに失敗しました。',
      msgAddMemoSucces: 'メモ登録が成功しました。',
      msgAddMemoFailed: 'メモ登録に失敗しました。',
      msgUpdMemoSucces: 'メモ更新が成功しました。',
      msgUpdMemoFailed: 'メモ更新に失敗しました。',
      msgMemoExist: 'メモある注文が存在しています。',
      msgUpdateSucces: '更新に成功しました。',
      msgUpdateFailed: '更新に失敗しました。',
      msgUpdtedBefore: '既に更新されました。',
      msgDeleteSucces: '削除に成功しました。',
      msgDeleteFailed: '削除に失敗しました。',
      msgRequired: '必須項目です。',
      msgSameBrandCd: 'ブランドコードが重複しています。',
      msgSameBrandName: 'ブランド名が重複しています。',
      msgSameBrandSub: 'サブブランドが重複しています。',
      msgNotChooseImage: '画像を選択してください。',
      msgNotChooseImageMax2: '最小2画像が必要です。',
      msgNotChooseImageMax1: '最小1画像が必要です。',
      msgNotChooseSame: 'カラー又はサイズが重複しています。再度選択してください。',
      msgProductNotExit: '商品がまだ選択されていません。',
      msgProductExited:'品番が既に存在しています。',
      msgDownloadFailed: 'CSVダウンロードに失敗しました。',
      msgResendOrderMailError: '予約ステータスが「予約（入金待ち）」以外の予約があります。',
      msgSendRemindMailError: '予約ステータスが「予約（入金待ち）」以外、又は、予約完了メール送信エラーの予約があります。',
      msgCheckStatusCancel: '予約ステータスが「キャンセル」の予約はキャンセルできません。',
      msgCheckStatusCompleteDelivery: '予約ステータスが「引渡完了」の予約はキャンセルできません。',
      msgCheckStatusCompleteDeliveryAndCancel: '予約ステータスが「引渡完了」又は「キャンセル」の予約はキャンセルできません。',
      msgMemoAddError: '[引渡し完了]又は[キャンセル]の予約状態のある注文が存在する場合、メモを追加できません。',
      msgNotRemoveAll: '全て削除できません。',
      msgNoData: 'データがありません。',
      msgDeletedSize: 'を削除しました。',
      msgSendMailComplete: '*メールを送信しました。',
      msgSendMailErr: '*メール送信に失敗しました。',
      msgMailResend: '*メールを再送しました。',
      msgUserNotExit:'対象ユーザーを選択してください。',
      msgResendReserveMailError: '予約ステータスが「予約（入金待ち）」以外の予約があります。',
      msgCannotChangeVar: '{{}} 中の内容を編集しないでください。',
      msgErrPrice: '価格＜1品番の予約可能金額で入力してください。',
      msgPasswordSameBefore: '新しいパスワードには現在のパスワードとは異なるパスワードを設定してください',
      msgConfirmPasswordNotMatch: '新しいパスワードと新しいパスワードの確認が一致していません。',
      msgPasswordLengthNotValid:'新しいパスワードは8文字以上入力してください。',
      msgProductSameCode:'重複している品番があります。',
      msgUserSameCode:'重複しているユーザーコードがあります。',
      msgDataDuplicateInCsv: '*が重複しています。',
      msgUserDeleted:'システムで削除されたユーザーコードが存在します。',
      msgUserExited:'ユーザーコードが既に存在しています。',
      msgStatusDelivery: '配送しました',
      msgStatusPaid: '振り込みました',
      msgStatusOrder: '予約しました。',
      msgMinTextProductCd: '英数字の7桁で入力してください。',
      msgMinTextUserCd: '英数字の8桁で入力してください。',
      msgMinTextPassWord: '英数字の8桁で入力してください。',
      msgSameRoleName: '権限名が既に存在しています。',
      msgErrYear: '年はYYYY形式で入力してください。',
      msgErrWrongQuantity: '０以外の半角数字を入力してください。 ',
      msgErrShowItemProduct: '製品はせめて1個表示する必要があります。',
      msgErrWrongInventory: '在庫数は０以外の半角数字を入力してください ',
      editorConfig: {
        toolbar: [
          { name: 'document', items: ['Preview'] },
          {
            name: 'clipboard',
            items: [
              'Cut',
              'Copy',
              'Paste',
              '-',
              'Undo',
              'Redo'
            ]
          },
          {
            name: 'basicstyles',
            items: [
              'Bold',
              'Italic',
              'Underline',
              'Strike',
              '-',
              'CopyFormatting'
            ]
          },
          {
            name: 'paragraph',
            items: [
              'JustifyLeft',
              'JustifyCenter',
              'JustifyRight',
              'JustifyBlock'
            ]
          },
          { name: 'links', items: ['Link'] },
          {
            name: 'insert',
            items: ['Table', 'HorizontalRule']
          },
          { name: 'colors', items: ['TextColor', 'BGColor'] }
        ]
      },
      editorConfigMail: {
        toolbar: [
          { name: 'document', items: ['Preview'] },
          {
            name: 'clipboard',
            items: [
              'Cut',
              'Copy',
              'Paste',
              '-',
              'Undo',
              'Redo'
            ]
          },
          {
            name: 'basicstyles',
            items: [
              'Bold',
              'Italic',
              'Underline',
              'Strike',
              '-',
              'CopyFormatting'
            ]
          },
          {
            name: 'paragraph',
            items: [
              'JustifyLeft',
              'JustifyCenter',
              'JustifyRight',
              'JustifyBlock'
            ]
          },
          { name: 'links', items: ['Link'] },
          {
            name: 'insert',
            items: ['Table', 'HorizontalRule']
          },
          { name: 'styles', items: ['Format', 'FontSize'] },
          { name: 'colors', items: ['TextColor', 'BGColor'] }
        ],
        height: 420,
        uiColor: '#eaebed',
        toolbarCanCollapse: true,
      },
      sizeName: '',
      receiveWayOptions: [
        { text: "", value: "" },
        { text: "配送", value: "02" },
        { text: "店頭受取", value: "01" },
      ],
      discountOptions: [
        { text: "", value: null },
        { text: "あり", value: true },
        { text: "なし", value: false },
      ],
      paymentWayOptions: [
        { text: "振込", value: "02" },
        { text: "店舗で支払い", value: "01" },
      ],
    }
  },
  methods: {
      formatPrice(value) {
        if (value !== undefined) {
          return Intl.NumberFormat().format(value);
        }
    },
    deFormatPrice(value) {
      const val = value.replace(',', '').replace('.', '')
      return val
    },
    formatPostCode(value) {
      if (value !== undefined) {
        return value.substr(0, 3) + '-' + value.substr(value.length - 4, 4)
      }
    },
    formatDate(value) {
      if (value !== undefined && value != null && value != '') {
        return value.substr(0, 4) + '-' + value.substr(value.length - 4, 2) + '-' + value.substr(value.length - 2, 2)
      }
    },
    sleep(ms) {
      return new Promise(resolve => setTimeout(resolve, ms))
    },
    isNumber: function (evt) {
      evt = evt || window.event
      var charCode = evt.which ? evt.which : evt.keyCode
      if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        evt.preventDefault()
      } else {
        return true
      }
    },
    isNumberAndDot: function (evt) {
      evt = evt || window.event
      var charCode = evt.which ? evt.which : evt.keyCode
      if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode !== 46) {
        evt.preventDefault();
      } else {
        return true;
      }
    },
    isNumberAndDash: function (evt) {
      evt = evt || window.event
      var charCode = evt.which ? evt.which : evt.keyCode
      if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode !== 45) {
        evt.preventDefault();
      } else {
        return true;
      }
    },
    validateKanjiName(txtNameKanji, txtLength, txtReplaceMsg) {
      if (
        txtNameKanji === '' ||
        txtNameKanji === undefined ||
        (txtNameKanji.length > 0 &&
          txtNameKanji.trim() === '')
      ) {
        return this.msgPleaseInput.replace('*', txtReplaceMsg)
      } else if (txtNameKanji.length > txtLength) {
        return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
      } else if (!new RegExp(this.REGEX_SPECIAL).test(txtNameKanji)) {
        return this.msgTextInputFailed
      } else {
        return ''
      }
    },
    validateKanaName(txtNameKana, txtLength, txtReplaceMsg) {
      if (
        txtNameKana === '' ||
        txtNameKana === undefined ||
        (txtNameKana.length > 0 &&
          txtNameKana.trim() === '')
      ) {
        return this.msgPleaseInput.replace('*', txtReplaceMsg)
      } else if (txtNameKana.length > txtLength) {
        return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
      } else {
        txtNameKana = txtNameKana.replace(/\s/g, '')
        if (!new RegExp(this.REGEX_KANA).test(txtNameKana)) {
          return this.msgPleaseInput.replace('*', txtReplaceMsg)
        } else {
          return ''
        }
      }
    },
    validateKanaNameForAdmin(txtNameKana, txtLength, txtReplaceMsg) {
      if (
        txtNameKana === '' ||
        txtNameKana === undefined ||
        (txtNameKana.length > 0 &&
          txtNameKana.trim() === '')
      ) {
        return this.msgRequired
      } else if (txtNameKana.length > txtLength) {
        return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
      } else {
        txtNameKana = txtNameKana.replace(/\s/g, '')
        if (!new RegExp(this.REGEX_KANA).test(txtNameKana)) {
          return this.msgPleaseInput.replace('*', txtReplaceMsg)
        } else {
          return ''
        }
      }
    },
    validateZipCode(txtZipCode, txtLength, txtReplaceMsg) {
      if (
        txtZipCode === '' ||
        txtZipCode === undefined ||
        (txtZipCode.length > 0 &&
          txtZipCode.trim() === '')
      ) {
        return this.msgPleaseInput.replace('*', txtReplaceMsg)
      } else if (txtZipCode.length !== txtLength) {
        return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
      } else if (!new RegExp(this.REGEX_NUMBER).test(txtZipCode)) {
        return this.msgInputFormat.replace('*', txtReplaceMsg)
      } else {
        return ''
      }
    },
    validateProvince(txtProvince, txtReplaceMsg) {
      if (
        txtProvince === null ||
        txtProvince === '' ||
        txtProvince === undefined
      ) {
        return this.msgPleaseSelect.replace('*', txtReplaceMsg)
      } else {
        return ''
      }
    },
    validateAdrCdRequired(txtAdrCd, txtLength, txtReplaceMsg) {
      if (
        txtAdrCd === '' ||
        txtAdrCd === undefined ||
        (txtAdrCd.length > 0 && txtAdrCd.trim() === '')
      ) {
        return this.msgPleaseInput.replace('*', txtReplaceMsg)
      } else if (txtAdrCd.length > txtLength) {
        return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
      } else if (!new RegExp(this.REGEX_SPECIAL_ADDRESS).test(txtAdrCd)) {
        return this.msgTextInputFailed
      } else {
        return ''
      }
    },
    validateAdrCdUnRequired(txtAdrCd, txtLength, txtReplaceMsg) {
      if (txtAdrCd !== undefined && txtAdrCd !== null) {
        if (txtAdrCd.length > txtLength) {
          return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
        } else if (!new RegExp(this.REGEX_SPECIAL_ADDRESS).test(txtAdrCd) && txtAdrCd !== '') {
          return this.msgTextInputFailed
        } else {
          return ''
        }
      } else {
        return ''
      }
    },
    validatePhoneNumber(txtPhoneNumber, txtLength, txtReplaceMsg) {
      if (
        txtPhoneNumber === '' ||
        txtPhoneNumber === undefined ||
        txtPhoneNumber.length < 9 ||
        (txtPhoneNumber.length > 0 &&
          txtPhoneNumber.trim() === '')
      ) {
        return this.msgPleaseInput.replace('*', txtReplaceMsg)
      } else if (
        !new RegExp(this.REGEX_NUMBER).test(txtPhoneNumber)
      ) {
        return this.msgPhoneNumberFormat.replace('*', txtReplaceMsg)
      } else if (txtPhoneNumber.length > txtLength) {
        return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
      } else {
        return ''
      }
    },
    validateMailAddress(txtMailAddress, txtLength, txtReplaceMsg) {
      if (
        txtMailAddress === '' ||
        txtMailAddress === undefined ||
        (txtMailAddress.length > 0 &&
          txtMailAddress.trim() === '')
      ) {
        return this.msgPleaseInput.replace('*', txtReplaceMsg)
      } else if (txtMailAddress.length > txtLength) {
        return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
      } else if (!new RegExp(this.REGEX_MAIL).test(txtMailAddress)) {
        return this.msgInputFormat.replace('*', txtReplaceMsg)
      } else {
        return ''
      }
    },
    validateMailAddressForAdmin(txtMailAddress, txtLength, txtReplaceMsg) {
      if (
        txtMailAddress === '' ||
        txtMailAddress === undefined ||
        (txtMailAddress.length > 0 &&
          txtMailAddress.trim() === '')
      ) {
        return this.msgRequired
      } else if (txtMailAddress.length > txtLength) {
        return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
      } else if (!new RegExp(this.REGEX_MAIL).test(txtMailAddress)) {
        return this.msgInputFormat.replace('*', txtReplaceMsg)
      } else {
        return ''
      }
    },
    validateMailAddressConfirm(txtMailAddressConfirm, txtMailAddress, txtReplaceMsg) {
      if (
        txtMailAddressConfirm === '' ||
        txtMailAddressConfirm === undefined ||
        (txtMailAddressConfirm.length > 0 &&
          txtMailAddressConfirm.trim() === '')
      ) {
        return this.msgPleaseInput.replace('*', txtReplaceMsg)
      } else if (
        !new RegExp(this.REGEX_MAIL).test(txtMailAddressConfirm)
      ) {
        return this.msgInputFormat.replace('*', txtReplaceMsg)
      } else if (
        txtMailAddressConfirm !== txtMailAddress &&
        txtMailAddress !== '' &&
        txtMailAddress !== undefined
      ) {
        return this.msgMailAddressMatched.replace('*', txtReplaceMsg)
      } else {
        return ''
      }
    },
    validateShopSelect(txtAreaSelect, txtShopSelect, txtAreaReplaceMsg, txtShopReplaceMsg) {
      if (txtAreaSelect === '' || txtAreaSelect === undefined) {
        return this.msgPleaseSelect.replace('*', txtAreaReplaceMsg)
      } else if (txtShopSelect === '' || txtShopSelect === undefined) {
        return this.msgPleaseSelect.replace('*', txtShopReplaceMsg)
      } else {
        return ''
      }
    },
    validateTextRequired(txtValue, txtReplaceMsg) {
      if (
        txtValue === '' ||
        txtValue === undefined ||
        (txtValue.length > 0 && txtValue.trim() === '')
      ) {
        return this.msgPleaseInput.replace('*', txtReplaceMsg)
      } else {
        return ''
      }
    },
    validateTextLength(txtValue, txtLength, txtReplaceMsg) {
      if (
        txtValue !== null &&
        txtValue !== '' &&
        txtValue !== undefined &&
        (txtValue.length > 0 &&
          txtValue.trim() !== '')
      ) {
        if (
          txtValue.length > txtLength
        ) {
          return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
        } else {
          return ''
        }
      } else {
        return ''
      }
    },
    validateTextRequiredAndLength(txtValue, txtLength, txtReplaceMsg) {
      if (
        txtValue === '' ||
        txtValue === undefined ||
        (txtValue.length > 0 && txtValue.trim() === '')
      ) {
        return this.msgPleaseInput.replace('*', txtReplaceMsg)
      } else if (txtValue.length > txtLength) {
        return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
      } else {
        return ''
      }
    },
    validateArea(txtArea, txtReplaceMsg) {
      if (
        txtArea === null ||
        txtArea === '' ||
        txtArea === undefined ||
        txtArea === 0
      ) {
        return this.msgPleaseSelect.replace('*', txtReplaceMsg)
      } else {
        return ''
      }
    },
    validateTelOrFaxUnRequired(txtValue, txtLength, txtReplaceMsg) {
      if (
        txtValue !== null &&
        txtValue !== '' &&
        txtValue !== undefined &&
        (txtValue.length > 0 &&
          txtValue.trim() !== '')
      ) {
        if (txtValue.length < 9) {
          return this.msgPleaseInput.replace('*', txtReplaceMsg)
        } else if (
          !new RegExp(this.REGEX_TEL_FAX).test(txtValue)
        ) {
          return this.msgPhoneNumberFormat.replace('*', txtReplaceMsg)
        } else if (txtValue.length > txtLength) {
          return this.msgLengthTooLong.replace('*', txtReplaceMsg).replace('$length', txtLength)
        } else {
          return ''
        }
      } else {
        return ''
      }
    },
    validateSelected(txtValue, txtReplaceMsg) {
      if (
        txtValue === null ||
        txtValue === '' ||
        txtValue === undefined
      ) {
        return this.msgPleaseSelect.replace('*', txtReplaceMsg)
      } else {
        return ''
      }
    },
    maskPhone(textPhone) {
      if (textPhone === '' || textPhone === undefined) {
        return ''
      }
      var mask = ''
      for (var i = 0; i < textPhone.length - 4; i++) {
        mask += '*'
      }
      return mask + textPhone.substring(textPhone.length - 4)
    },
    maskMail(textMail) {
      if (textMail === '' || textMail === undefined) {
        return ''
      }
      var maskCheck = textMail.search('@')
      var maskPhone = ''
      if (maskCheck < 4) {
        for (var i = 0; i < maskCheck; i++) {
          maskPhone += '*'
        }
        return (
          maskPhone +
          textMail.substring(maskCheck, textMail.length - maskCheck + 3)
        )
      }
      return textMail.replace(/(.{4})(?=@)/, '****')
    },
    getTextFromOptions(objOptions, txtValue, isParse) {
      if (objOptions === null || txtValue === null ||
        objOptions === undefined || txtValue === undefined ||
        objOptions.length === 0 || txtValue === '') {
        return ''
      }

      let obj = []
      if (isParse) {
        obj = objOptions.filter((x) => parseInt(x.value) === parseInt(txtValue))
      } else {
        obj = objOptions.filter((x) => x.value === txtValue)
      }

      return obj.length > 0 ? obj[0].text : txtValue;
    },
    getTextFromOptionsBool(objOptions, txtValue) {
      if (objOptions === null || txtValue === null ||
        objOptions === undefined || txtValue === undefined ||
        objOptions.length === 0 || txtValue === '') {
        return ''
      }

      let obj = objOptions.filter((x) => x.value === txtValue)
      return obj.length > 0 ? obj[0].text : txtValue;
    },
    getUserData() {
      return this.$cookies.get('userData')
    },
    getTextFromKbnValue(objKbn, kbnValue) {
      if (objKbn === null || kbnValue === null ||
        objKbn === undefined || kbnValue === undefined ||
        objKbn.length === 0 || kbnValue === '') {
        return ''
      }
      return objKbn.filter(x => x.kbnValue === kbnValue)[0].kbnValueName
    },
    logOut() {
      // remove all
      try {
        this.clearData()
        this.$router.push('/admin/login')
      } catch (e) {
        this.$toastr.e(e.message)
      }
    },
    clearData() {
      const cookies = this.$cookies.keys()
      for (let index = 0; index < cookies.length; index++) {
        const key = cookies[index]
        this.$cookies.remove(key)
      }
      localStorage.clear()
      sessionStorage.clear()
    },
    setDisplayStrHist(histType, itemHist) {
      var histText = ''
      if (itemHist.department === this.DEPART_ADMIN_TYPE) {
        histText += this.DEPART_ADMIN
      }
      if (itemHist.department === this.DEPART_SHOP_TYPE) {
        histText += this.DEPART_SHOP
      }
      switch (histType) {
        case this.HIST_TYPE_CHANGE_STATUS:
          if (itemHist.updatedStatus === this.RSV_STATUS_ORDER) {
            histText = this.msgStatusOrder
            break
          }
          if (itemHist.updatedStatus === this.RSV_STATUS_PAID) {
            if(itemHist.receiveWay === this.SHIPPING && 
              itemHist.paymentWay === this.TRANSFER)
            {
              histText = this.msgStatusPaid
            }else{
                histText += 'にて入金・引渡しました。'
            }
            break
          }
          //if (itemHist.updatedStatus === this.RSV_STATUS_COMPLETED_DELIVERY) {
          //  if(itemHist.receiveWay === this.SHIPPING && 
          //    itemHist.paymentWay === this.TRANSFER)
          //  {
          //    histText = this.msgStatusDelivery
          //  }else{
          //    histText += 'にて引渡しました。'
          //  }
          //  break
          //}
          if (itemHist.updatedStatus === this.RSV_STATUS_CANCEL) {
            histText += 'が予約をキャンセルしました。'
            break
          }
          histText +=
            this.getTextFromKbnValue(this.histTypeValue, histType) +
            ' ' +
            this.getTextFromKbnValue(this.rsvStatusKbn, itemHist.lastStatus) +
            ' -> ' +
            this.getTextFromKbnValue(this.rsvStatusKbn, itemHist.updatedStatus)
          break
        case this.HIST_TYPE_SEND_MAIL:
          // histText += ''
          // histText += this.getSentMailReserveHist(itemHist.mailId)
          switch (itemHist.mailType) {
            case this.MAIL_TYPE_ORDER:
              if (itemHist.mailStatus === this.MAIL_STATUS_SUCCESS) {
                histText = this.msgSendMailComplete.replace('*', this.TITLE_MAIL_TYPE_ORDER)
              } else if (itemHist.mailStatus === this.MAIL_STATUS_RESEND) {
                histText = this.msgMailResend.replace('*', this.TITLE_MAIL_TYPE_ORDER)
              } else {
                histText = this.msgSendMailErr.replace('*', this.TITLE_MAIL_TYPE_ORDER)
              }
              break
            case this.MAIL_TYPE_ORDER_CHANGED:
              if (itemHist.mailStatus === this.MAIL_STATUS_SUCCESS) {
                histText = this.msgSendMailComplete.replace('*', this.TITLE_MAIL_TYPE_ORDER_CHANGED)
              } else if (itemHist.mailStatus === this.MAIL_STATUS_RESEND) {
                histText = this.msgMailResend.replace('*', this.TITLE_MAIL_TYPE_ORDER_CHANGED)
              } else if (itemHist.mailStatus === this.MAIL_STATUS_ERROR) {
                histText = this.msgSendMailErr.replace('*', this.TITLE_MAIL_TYPE_ORDER_CHANGED)
              }
              break
            case this.MAIL_TYPE_ORDER_CANCEL:
              if (itemHist.mailStatus === this.MAIL_STATUS_SUCCESS) {
                histText = this.msgSendMailComplete.replace('*', this.TITLE_MAIL_TYPE_ORDER_CANCEL)
              } else if (itemHist.mailStatus === this.MAIL_STATUS_RESEND) {
                histText = this.msgMailResend.replace('*', this.TITLE_MAIL_TYPE_ORDER_CANCEL)
              } else if (itemHist.mailStatus === this.MAIL_STATUS_ERROR) {
                histText = this.msgSendMailErr.replace('*', this.TITLE_MAIL_TYPE_ORDER_CANCEL)
              }
              break
            case this.MAIL_TYPE_REMIND_PAYMENT:
              if (itemHist.mailStatus === this.MAIL_STATUS_SUCCESS) {
                histText = this.msgSendMailComplete.replace('*', this.TITLE_MAIL_TYPE_REMIND_PAYMENT)
              } else if (itemHist.mailStatus === this.MAIL_STATUS_RESEND) {
                histText = this.msgMailResend.replace('*', this.TITLE_MAIL_TYPE_REMIND_PAYMENT)
              } else if (itemHist.mailStatus === this.MAIL_STATUS_ERROR) {
                histText = this.msgSendMailErr.replace('*', this.TITLE_MAIL_TYPE_REMIND_PAYMENT)
              }
              break
            case this.MAIL_TYPE_COMPLETE_PAYMENT:
              if (itemHist.mailStatus === this.MAIL_STATUS_SUCCESS) {
                histText = this.msgSendMailComplete.replace('*', this.TITLE_MAIL_TYPE_COMPLETE_PAYMENT)
              } else if (itemHist.mailStatus === this.MAIL_STATUS_RESEND) {
                histText = this.msgMailResend.replace('*', this.TITLE_MAIL_TYPE_COMPLETE_PAYMENT)
              } else if (itemHist.mailStatus === this.MAIL_STATUS_ERROR) {
                histText = this.msgSendMailErr.replace('*', this.TITLE_MAIL_TYPE_COMPLETE_PAYMENT)
              }
              break
          }
          break
        case this.HIST_TYPE_CHANGE_QUANTITY:
          histText =
            this.getSku(itemHist) + '、' +
            this.QUANTITY_TEXT +
            itemHist.lastQuantity +
            ' -> ' +
            this.QUANTITY_TEXT +
            itemHist.updatedlQuantity
          break
        case this.HIST_TYPE_CHANGE_SIZE:
          histText =
            this.getProductCdWithText(itemHist) + '、' +
            itemHist.lastSize +
            this.SIZE_TEXT + '、' +
            itemHist.color + ' -> ' +
            itemHist.updatedSize +
            this.SIZE_TEXT
          break
        case this.HIST_TYPE_DELETE_ORDER:
          histText = this.getSku(itemHist) +
            this.msgDeletedSize
          break
        case this.HIST_TYPE_CHANGE_SHOP:
            histText = itemHist.lastShopName +
              ' -> ' +
              itemHist.updatedShopName
            break
        case this.HIST_TYPE_CHANGE_COLOR:
            histText =
            this.getProductCdWithText(itemHist) + '、' +
            itemHist.size +
            this.SIZE_TEXT + '、' +
            itemHist.lastColor +' -> ' +
            itemHist.updatedColor
            break
        case this.HIST_TYPE_CHANGE_SIZE_AND_QUANTITY:
            histText =
            this.getProductCdWithText(itemHist) + '、' +
            itemHist.lastSize +
            this.SIZE_TEXT + '、' +
            itemHist.color + '、' +
            this.QUANTITY_TEXT +
            itemHist.lastQuantity +
            ' -> ' +
            itemHist.updatedSize +
            this.SIZE_TEXT + '、' +
            this.QUANTITY_TEXT +
            itemHist.updatedlQuantity
            break
        case this.HIST_TYPE_CHANGE_COLOR_AND_QUANTITY:
            histText =
            this.getProductCdWithText(itemHist) + '、' +
            itemHist.size +
            this.SIZE_TEXT + '、' +
            itemHist.lastColor + '、' +
            this.QUANTITY_TEXT +
            itemHist.lastQuantity +
            ' -> ' +
            itemHist.updatedColor + '、' +
            this.QUANTITY_TEXT +
            itemHist.updatedlQuantity
            break
        case this.HIST_TYPE_CHANGE_SIZE_AND_COLOR:
            histText =
            this.getProductCdWithText(itemHist) + '、' +
            itemHist.lastSize + 
            this.SIZE_TEXT +'、' +
            itemHist.lastColor +
            ' -> ' +
            itemHist.updatedSize +
            this.SIZE_TEXT + '、' +
            itemHist.updatedColor 
            break
        case this.HIST_TYPE_CHANGE_SIZE_AND_COLOR_AND_QUANTITY:
            histText =
            this.getProductCdWithText(itemHist) + '、' +
            itemHist.lastSize + 
            this.SIZE_TEXT +'、' +
            itemHist.lastColor + '、' +
            this.QUANTITY_TEXT +
            itemHist.lastQuantity +
            ' -> ' +
            itemHist.updatedSize + 
            this.SIZE_TEXT +'、' +
            itemHist.updatedColor + '、' +
            this.QUANTITY_TEXT +
            itemHist.updatedlQuantity
            break
        case this.HIST_TYPE_CHANGE_RECIVE_INFO:
             histText = '配送情報を変更しました'
            break
        default:
      }
      return histText
    },
    getSku(skuObj){
      return skuObj.productCd +
        this.TEXT_PRODUCT + '、' +
        skuObj.size + 
        this.SIZE_TEXT + '、' +
        skuObj.color 
    },
    getProductCdWithText(skuObj){
      return skuObj.productCd +
        this.TEXT_PRODUCT
    },
    gotoChangePw() {
      this.$router.push('/admin/change-password')
    },
    checkEndOrder() {
      if (localStorage.getItem("orderFinish") !== null || localStorage.getItem("openShop") === null) {
        if (localStorage.getItem("routerTop") != null) {
          this.$router.push({ path: localStorage.getItem("routerTop") });
        } else {
          this.$router.push({ name: "404" });
        }
      }
    },
    clearSelectedProduct(){
      sessionStorage.removeItem("selectedProducts")
    },
    checkDefaultPassword(){
      var isAdminDefaultPw = sessionStorage.getItem('isAdminDefaultPw')
      var isShowPopup = sessionStorage.getItem('isShowPopup')
      var isChangePass = sessionStorage.getItem('isChangePass')
      
      if (this.$route.params.oldPassword !== undefined &&
        this.$route.params.oldPassword === true &&
        isShowPopup == this.SHOW_POPUP &&
        isChangePass == this.CHANGE_PASS) {
        this.dialogChangePwConfirm = true
      }
      else if (isAdminDefaultPw !== null && 
        isAdminDefaultPw !== undefined && 
        isAdminDefaultPw !== 'null' &&
        isAdminDefaultPw !== 'undefined' &&
        isShowPopup == this.SHOW_POPUP &&
        isChangePass == this.CHANGE_PASS) {
        this.dialogChangePwConfirm = true
        sessionStorage.removeItem("isAdminDefaultPw")
        sessionStorage.removeItem("isShowPopup")
      }
    }
  },
  computed: {
    listPermission: function () {
      var userData = this.getUserData();
      if (userData !== null && userData.permissions !== undefined) {
        if (userData.permissions !== null  && userData.permissions !== undefined) {
          return userData.permissions.split(",")
        }
      }
      return []
    },
    isUserReadOnly: function () {
      return (this.listPermission.length > 0
              && (
                  (this.listPermission.length === 1
                    && this.listPermission.find(x => x === this.VIEW) !== undefined)
                  || (this.listPermission.find(x => x === this.VIEW) !== undefined
                      && this.listPermission.find(x => x === this.UPD) === undefined) 
                  )
              )
    },
    isUserCanDownCsv: function () {
        return  (this.listPermission.length > 0 
          && this.listPermission.find(x => x === this.DOWN_CSV) !== undefined)
    }
  }
}
export default common
