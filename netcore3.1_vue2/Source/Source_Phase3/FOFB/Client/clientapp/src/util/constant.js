var constant = {
  data: function () {
    return {
      // ________________ Regex _________________________
      REGEX_NUMBER: '^[0-9]*$',
      REGEX_TEL_FAX: '^[0-9-]*$',
      REGEX_URL: '^[a-zA-Z0-9]+$',
      REGEX_KANA: '^[ぁ-んァ-ンｱ-ﾝ]+$',
      REGEX_MAIL: /^[a-zA-Z0-9.!#$%&'*+\\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/,
      REGEX_SPECIAL: '^[ｧ-ﾝﾞﾟ一-龠ぁ-ゔァ-ヴーa-zA-Z0-9ａ-ｚＡ-Ｚ０-９々〆〤ァ-ヶぁ-ゞｦ-ﾟ\\ \\　]+$',
      REGEX_SPECIAL_ADDRESS: '^[ｧ-ﾝﾞﾟ一-龠ぁ-ゔァ-ヴーa-zA-Z0-9ａ-ｚＡ-Ｚ０-９々〆〤ァ-ヶぁ-ゞｦ-ﾟ/\\-\\－（）()／\\ \\　]+$',
      // ________________ MkbnValue ______________________
      KBN_PROVINCE: '0001',
      KBN_AREA: '0002',
      KBN_ROLE_DETAIL: '0004',
      KBN_LIST_SCREEN: '0012',
      KBN_AREA_1: '0010',
      KBN_CONTRACT_TYPE: '0011',
      // ________________ Image Position _________________
      POSITION_TOP: '01',
      POSITION_MID: '02',
      POSITION_BOTTOM: '03',
      // ________________ Screen Code ____________________
      SCREEN_TOP: '01',
      SCREEN_LIST: '02',
      SCREEN_DETAIL: '03',
      SCREEN_ORDER: '04',
      SCREEN_ORDER_CONFIRM: '05',
      SCREEN_ORDER_FINISH: '06',
      SCREEN_RULES: '07',
      // ________________ Item Name ______________________
      KANJI_FAMILY: '姓（漢字）',
      KANJI_FIRST: '名（漢字）',
      KANA_FAMILY: '姓（ひらがな）',
      KANA_FIRST: '名（ひらがな）',
      KANA_NAME: '名前（ひらがな）',
      ZIPCODE: '郵便番号',
      PROVINCE: '都道府県',
      ADR_CD1: '住所１',
      ADR_CD2: '住所２',
      ADR_CD3: '住所３',
      PHONE_NUMBER: '電話番号',
      MAIL_ADDRESS: 'メールアドレス',
      MAIL_ADDRESS_CONFIRM: 'メールアドレス（確認）',
      AREA: 'エリア',
      SHOP: '店舗',
      DISCOUNT: '早期割引特典',
      PAYMENT_WAY: '決済方法',
      MEMO: 'メモ',
      MAIL_TITLE: '件名',
      MAIL_CONTENT: '内容',
      MAIL_FROM: '送信元',
      MAIL_FROM_NAME: '送信元名',
      MAIL_TO: '送信先',
      ACCESS_KEY_ID: 'AccessKeyID(バウンスメール取得)',
      SECRET_ACCESS_KEY: 'SecretAccessKey(バウンスメール取得)',
      QUEUE_URL: 'QueueUrl(バウンスメール取得)',
      SMTP_SERVER: 'SMTP Server',
      SMTP_ACCOUNT: 'SMTP Account',
      SMTP_PASS: 'SMTP Pass',
      SHOP_CD: '店舗コード',
      SHOP_NAME: '店舗名',
      ADR_CD1_SHOP: '所在地１',
      ADR_CD2_SHOP: '所在地２',
      AREA2: 'エリア２',
      TEL: 'TEL',
      FAX: 'FAX',
      STORE_STATUS: '出退店区分',
      START_DATE: '開店日',
      END_DATE: '閉店日',
      AFFILIATE_DEPARTMENT_CD: '所属部門',
      AFFILIATE_DEPARTMENT_NAME: '所属部門名',
      OPEN_TIME: '営業時間',
      SQUARE: '坪数',
      SALE_PERSON_CD: '営業担当者',
      SALE_PERSON_NAME: '営業担当者名',
      ROLE_NAME: '権限名',
      // ________________ ReceiveWay _____________________
      IN_SHOP: '01',
      SHIPPING: '02',
      // ________________ paymentWay _____________________
      PAY_IN_SHOP: '01',
      TRANSFER: '02',
      // ________________ Discount _______________________
      HAVE_DISCOUNT: 'あり',
      NOT_DISCOUNT: 'なし',
      // ________________ Reservation ____________________
      RSV_STATUS_ORDER: '01',
      RSV_STATUS_PAID: '02',
      //RSV_STATUS_COMPLETED_DELIVERY: '04',
      RSV_STATUS_CANCEL: '05',
      // ________________ End Reservation _________________
      // ________________  Result Orders __________________
      ADD_PRODUCT_FAIL: 0,
      PRODUCT_OUT_QUANTITY: -1,
      // ________________ Item Show/Hidden ________________
      ITEM_SHOW: 0,
      ITEM_SHOW_TEXT: '表示',
      ITEM_HIDDEN: 1,
      ITEM_HIDDEN_TEXT: '非表示',
      //_________________ Perrmission _____________________
      VIEW: 'VIEW',
      CRUD: 'CRUD',
      UPD: 'UPD',
      DOWN_CSV: 'DOWN_CSV',
      // ________________ Product Show/Hidden ________________
      PRODUCT_SHOW: false,
      PRODUCT_HIDDEN: true,
      // ________________ Insert brand ____________________
      INSERT_BRAND_FAIL: -1,
            // ________________ Insert brand ____________________
      INSERT_PRODUCT_FAIL: -1,
      // ________________ Mode ____________________
      MODE_ADD: 'Add',
      MODE_UPDATE: 'Update',
      MODE_DETAIL: 'Detail',
      //_________________ PageSize ________________________
      PAGE_SIZE_DEFAULT: 10,
      PAGE_SIZE_DETAIL_DEFAULT: 5,
      PAGE_START_DEFAULT: 1,
      // ________________ Mail Status _____________________
      MAIL_STATUS_SUCCESS: '01',
      MAIL_STATUS_ERROR: '02',
      MAIL_STATUS_RESEND: '03',
      MAIL_STATUS_NOTSEND: '04',
      // ________________ Result Type _____________________
      RESULT_ERROR: 0,
      RESULT_SUCCESS: 1,
      RESULT_UPDATED: 2,
      // ________________ Title Add/Update ________________
      TEXT_ADD: '登録',
      TEXT_UPD: '変更',
       // ________________ Size / Color / Product___________________
      TEXT_SIZE: 'サイズ',
      TEXT_COLOR: '色',
      TEXT_PRODUCT: '品番',
      // ________________ Reserve history type __________________
      HIST_TYPE_CHANGE_STATUS: '01',
      HIST_TYPE_SEND_MAIL: '02',
      HIST_TYPE_CHANGE_QUANTITY: '03',
      HIST_TYPE_CHANGE_SIZE: '04',
      HIST_TYPE_CHANGE_COLOR: '05',
      HIST_TYPE_CHANGE_SIZE_AND_QUANTITY: '06',
      HIST_TYPE_CHANGE_COLOR_AND_QUANTITY: '07',
      HIST_TYPE_CHANGE_SIZE_AND_COLOR: '08',
      HIST_TYPE_CHANGE_SIZE_AND_COLOR_AND_QUANTITY: '09',
      HIST_TYPE_DELETE_ORDER: '10',
      HIST_TYPE_CHANGE_SHOP: '11',
      HIST_TYPE_CHANGE_RECIVE_INFO: '12',
      // ________________ End reserve history type _______________
      KBN_ORDER_STATUS: '0003',
      KBN_HIST_TYPE: '0004',
      KBN_MAIL_TYPE: '0005',
      KBN_MAIL_STATUS: '0009',
       // ________________ Mail Type _____________________________
       MAIL_TYPE_ORDER: '10',
       MAIL_TYPE_ORDER_CHANGED: '11',
       MAIL_TYPE_ORDER_CANCEL: '12',
       MAIL_TYPE_REMIND_PAYMENT: '20',
       MAIL_TYPE_COMPLETE_PAYMENT: '30',
        // ________________ Mail Type _____________________________
       TITLE_MAIL_TYPE_ORDER: '予約完了',
       TITLE_MAIL_TYPE_ORDER_CHANGED: '予約変更',
       TITLE_MAIL_TYPE_ORDER_CANCEL: '予約キャンセル',
       TITLE_MAIL_TYPE_REMIND_PAYMENT: '入金リマインド',
       TITLE_MAIL_TYPE_COMPLETE_PAYMENT: '入金完了',
      // ________________ End Mail Type __________________________
      // ________________  User define ___________________________
      DEPART_ADMIN_TYPE: 1,
      DEPART_ADMIN: '本部',
      DEPART_SHOP_TYPE: 2,
      DEPART_SHOP: '店舗',
      // ________________  End User define _______________________
      SEND_TO_CUST: '01',
      SEND_TO_ADMIN: '02',
      CUST_TEXT: 'お客様',
      QUANTITY_TEXT: '数量',
      SIZE_TEXT: 'サイズ',
      // ________________ Auto login/ Not auto login ____________
       NOT_AUTO_LOGIN: 0,
       AUTO_LOGIN: 1,
      // ________________ Role __________________________________
      ROLE_SUPER_ADMIN: 1,
      // ________________ File Img ______________________________
      IMAGE_FILE: '.jpg, .png, .jpeg, .gif, .BMP, .TIFF',
      PRODUCT_DETAIL_PATH:'/Images/product-detail/',
      PRODUCT_LIST_PATH:'/Images/product-list/',
      IMAGE_TOP_PATH:'/Images/top/',
      IMAGE_BAR_CODE_BASE_PATH:'/Images/bar-code/algy/BarCodeBase.jpg',
      // ________________ PassWord ______________________________
      SHOW_POPUP: 1,
      CHANGE_PASS: 1,
    }
  },
  methods: {

  }
}
export default constant
