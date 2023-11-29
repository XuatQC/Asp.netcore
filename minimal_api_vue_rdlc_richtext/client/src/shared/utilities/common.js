import { LANGUAGE } from 'helpers/constant';
import translateService from 'services/translate.service';
import { useAppStore } from 'stores/app-store';
import printService from 'services/print.service';
import { useAuthStore } from 'stores/auth-store';
import { FORM_NAME } from 'helpers/enum';

// ============================ Common ===========================================
// OBSのプロパティを元にnum文字列を作成する
export const generateNum = ({
  plantName, kinds, fields, partId, serialNum, revisions, language,
}) => {
  let num = `${plantName}-${kinds}-${fields}-${partId}`;
  num += `-${serialNum.toString().padStart(2, '0')}-r${revisions}`;
  if (language === LANGUAGE.ENGLISH_FIRST_LETTER) num += '-E';
  return num;
};

// translateの値を作成する
export const createTranslateData = async ({
  parentKind, plantNameParam, kinds, fields, individual, serialNumber, revision, engEdition,
}) => {
  const appStore = useAppStore();
  const createTransResult = await translateService.createTranslateData(
    parentKind,
    appStore.plantName,
    {
      plantName: plantNameParam,
      kinds,
      fields,
      partId: individual,
      revisions: revision,
      branchOrSerialNum: serialNumber,
      transLang: engEdition,
    },
  );

  return createTransResult.payload;
};

// translateの値をTranslate一時テーブルに保存する
export const loadTranslateDataToWktTranslate = async (plantName) => {
  const result = await translateService.loadTranslateDataToWkt(plantName);
  return result;
};

// Wordファイルに出力する
export const outputWord = async ({
  parentKind, planNameParam, kind, fields, individual, serialNumber, revision, engEdition,
}) => {
  const result = await printService.getOutputWord({
    plantName: planNameParam,
    kinds: kind,
    fields,
    partId: individual,
    branchOrSerialNum: serialNumber,
    revisions: revision,
    language: engEdition,
  }, parentKind);
  return result;
};

export const outputWordProcessing = async (transData) => {
  const authStore = useAuthStore();
  const appStore = useAppStore();
  const printDataRequest = {
    preTranslateList: transData,
    language: authStore.user.language,
    obsPathsDto: {
      msworD_TEMPLATES: appStore.folderStore.wordFolder,
      trN_ES_DOT: '',
      docS_PUT_LOCAL: appStore.folderStore.docPutLocal,
      synC_PATH: '',
      docS_IN_TRANS: appStore.folderStore.docInTrans,
    },
  };

  const result = await printService.getOutputWordProcessing(printDataRequest);
  return result;
};

// 写真追加ウィンドウズを開いた親コンポーネントにより写真パスを取得する
export const getPhotoPathByParentComponent = (parentComponent) => {
  if (parentComponent === FORM_NAME.AFI || parentComponent === FORM_NAME.AFI_REFER) {
    return 'photo/AFI/';
  }
  if (parentComponent === FORM_NAME.BP || parentComponent === FORM_NAME.BP_REFER) {
    return 'photo/BP/';
  }
  if (parentComponent === FORM_NAME.GFA || parentComponent === FORM_NAME.GFA_REFER) {
    return 'photo/GFA/';
  }
  if (parentComponent === FORM_NAME.OBS || parentComponent === FORM_NAME.OBS_REFER) {
    return 'photo/OBS/';
  }
  if (parentComponent === FORM_NAME.PD || parentComponent === FORM_NAME.PD_REFER) {
    return 'photo/PD/';
  }
  if (parentComponent === FORM_NAME.PFA || parentComponent === FORM_NAME.PFA_REFER) {
    return 'photo/PFA/';
  }
  if (parentComponent === FORM_NAME.STR || parentComponent === FORM_NAME.STR_REFER) {
    return 'photo/STR/';
  }
  return '';
};
