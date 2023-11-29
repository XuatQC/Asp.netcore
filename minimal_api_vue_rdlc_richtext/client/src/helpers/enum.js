export const PAGINATION = {
  PAGE_SIZE: 30,
  MAX_DISPLAY_PAGES: 6,
};

export const TOKEN = {
  Name: '__TOKEN',
};

export const ROLE_KBN = {
  管理者: '01',
  一般ユーザー: '02',
  システムオーナー: '99',
};

export const ACCOUNT_STATUS_KBN = {
  アクティブ: '01',
  インアクティブ: '02',
};

export const SELECT_BOX_PLACEHOLDER = {
  ROWS_OF_PLACEHOLDER: 12,
};

export const IMAGE_PAGE = {
  MAX_IMAGE_IN_A_PAGE: 9,
};

export const NAVIGATE = {
  PREV: -1,
  NEXT: 1,
};
export const NAVIGATE_VERTICAL = {
  DOWN: -1,
  UP: 1,
};

export const OBS_INPUT = {
  TITLE: 'title',
  SCOPE: 'scope',
  FACT: 'fact',
  CONCLUSION: 'conclusion',
};

export const OBS_INPUT_LABEL = {
  TITLE: 'タイトル／Title',
  SCOPE: '範囲／Scope',
  FACT: '観察事実／Fact',
  CONCLUSION: '結論／Conclusions',
};

export const INPUT_DISPLAY_MODE = {
  NORMAL: 'normal',
  CONTEXT: 'context',
  LANGUAGE: 'language',
  COMMENT: 'comment',
  ENTRY: 'entry',
};

export const FIELD_ERROR_LOGIN = {
  INITIAL: 'Initial',
  PASSWORD: 'Password',
};

export const PLUS_NEUTRAL_DELTA = {
  OTHER: 0,
  PLUS: 1,
  NEUTRAL: 2,
  DELTA: 3,
};

export const TAG = {
  GFA: 'GFA',
  PFA: 'PFA',
  AFI: 'AFI',
  BP: 'BP',
  OBS: 'OBS',
  PD: 'PD',
  STR: 'STR',
  SOI: 'SOI',
};

export const TYPE_INPUT_AS_FIRST_LETTER = {
  TITLE: 'T',
  SCOPE: 'S',
  FACT: 'F',
  CONCLUSION: 'C',
  GENERAL_COMMENT: 'G',
};

export const SORT_ORDER = {
  ASC: 'asc',
  DESC: 'desc',
};

export const DIVISION = {
  AREA: 'A',
  POC: 'P',
};

export const NUMBER_AS_BOOL = {
  TRUE: 1,
  FALSE: 0,
};

export const EVENTS = {
  DRAG_ENTER: 'dragenter',
  DRAG_LEAVE: 'dragleave',
  DRAG_OVER: 'dragover',
  DROP: 'drop',
};

export const DIALOG_RETURN_VAL = {
  CANCEL: 'cancel',
  OK: 'OK',
};

export const REQUEST_FROM_SCREEN = {
  DEFAULT: 1,
  OBS_LIST: 21,
  OBS_INPUT: 22,
};

export const FORM_NAME = {
  OBS: 'frm_OBS',
  OBS_REFER: 'frm_OBS_Refer',
  WC: 'frm_WC',
  WC_EDIT: 'frm_WC_Edit',
  WC_REFER: 'frm_WC_Refer',
  WC_LIST: 'frm_WC_List',
  OUTPUT_WC: 'frm_Output_WC',
  SOI: 'frm_SOI',
  SOI_REFER: 'frm_SOI_Refer',
  ANALYSIS_WC: 'frm_Analysis_WC',
  AFI_REFER: 'frm_AFI_Refer',
  AFI: 'frm_AFI',
  BP: 'frm_BP',
  BP_REFER: 'frm_BP_Refer',
  GFA: 'frm_GFA',
  GFA_REFER: 'frm_GFA_Refer',
  PD: 'frm_PD',
  PD_REFER: 'frm_PD_Refer',
  PFA: 'frm_PFA',
  PFA_REFER: 'frm_PFA_Refer',
  STR: 'frm_STR',
  STR_REFER: 'frm_STR_Refer',
};

export const TRANS_HIST_ITEM = {
  TITLE: 0,
  SCOPE: 1,
  FACT: 2,
  CONCLUSION: 3,
  EXAMPLE: 4,
};

export const OBS_INPUT_TAG = {
  ADD: 'Add',
  EDIT: 'Edit',
  EDIT_OLD: 'Edit_Old',
  EDIT_TL: 'Edit_TL',
};

export const REQUEST_TIME_LIMITATION = {
  1: '通常／Normal',
  2: '至急／Rush',
  3: '＜1Hr',
  4: '＜2Hr',
  5: '～10:00',
  6: '～13:00',
  7: '～15:00',
  8: '～18:00',
  9: '～T.Meeting',
};

export const LANG_TRANSFER = {
  EN_JAPANESE_TO_ENGLISH: 'Japanese ーー＞ English',
  EN_ENGLISH_TO_JAPANESE: 'English ーー＞ Japanese',
  JP_JAPANESE_TO_ENGLISH: '日本語 ーー＞ 英語',
  JP_ENGLISH_TO_JAPANESE: '英語 ーー＞ 日本語',
};

export const CONNECTION_PLACE = {
  三田オフィス: 0,
  BOXサーバ1: 1,
  BOXサーバ2: 2,
};

export const DM_DIVISION = {
  MAKER: 'M',
  F: 'F',
};

export const SHEET_TYPE_NAME = {
  SOI: '改善状況評価シート／SOI',
  GFA: 'フォーカスエリア分析シート／GFA',
  PFA: 'プラスフォーカスエリア分析シート／PFA',
  AFI: '要改善事項／AFI',
  PD: '軽微な問題点／PD',
  STR: '良好事例／STR',
  BP: '有益事例／BP',
};

export const PRINT_MODE = {
  PRINT: 'Prt',
  WORD: 'Word',
  EXCEL: 'Excel',
};

export const EDITOR_SELECTION_DISPLAY = {
  DEFAULT: 'default',
  ALL: 'all',
};

export const MAX_IMAGE_SIZE = 70000;

export const SHARE_FOLDER_NAME = {
  WORD: '%(connection)s Word保管先',
  PHOTO_LIVE: '%(connection)s 写真保管先ライブ',
  PHOTO_PAST: '%(connection)s 写真保管先過去',
  PUT_TRANS: '%(connection)s 翻訳出力先',
  IN_TRANS: '%(connection)s 翻訳取込先',
  IN_TRANS_FINAL: '%(connection)s 翻訳取込先翻訳済',
};

export const SHARE_FOLDER_PATH = {
  WORD: '%(plantName)s\\DocInWord',
  PHOTO_LIVE: '%(plantName)s\\Photo',
  PHOTO_PAST: '%(plantName)s\\PastData\\Photo',
  PUT_TRANS: '%(plantName)s\\翻訳関係\\翻訳対象ファイル\\Waiting',
  IN_TRANS: '%(plantName)s\\翻訳関係\\翻訳対象ファイル\\Done',
  IN_TRANS_FINAL: '%(plantName)s\\翻訳関係\\翻訳対象ファイル\\Done\\翻訳取込済',
  PUT_LOCAL: '%(directory)sMSWord Templates\\temp\\',
};
