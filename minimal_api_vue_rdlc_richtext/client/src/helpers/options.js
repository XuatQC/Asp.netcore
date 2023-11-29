import {
  ROLE_KBN, ACCOUNT_STATUS_KBN, CONNECTION_PLACE, TRANS_HIST_ITEM, OBS_INPUT_LABEL, REQUEST_TIME_LIMITATION,
} from './enum';

const generateOptions = (enumKbn) => {
  const options = [];
  for (const [key, value] of Object.entries(enumKbn)) {
    options.push({
      label: key,
      value,
    });
  }
  return options;
};

const generateOptionsKeyValueEnum = (keyEnum, valueEnum) => {
  const options = [];
  for (let i = 0; i < keyEnum.length; i++) {
    options.push({
      label: keyEnum[i],
      value: valueEnum[i],
    });
  }
  return options;
};

// it is used for select box ( object to array)
export const ROLE_OPTIONS = generateOptions(ROLE_KBN);
export const ACCOUNT_STATUS_OPTIONS = generateOptions(ACCOUNT_STATUS_KBN);
export const CONNECTION_PLACE_OPTION = generateOptions(CONNECTION_PLACE);
export const TOPIC_TRANS_HIST_OPTION = generateOptionsKeyValueEnum(Object.values(OBS_INPUT_LABEL), Object.values(TRANS_HIST_ITEM));
export const REQUEST_TIME_LIMITATION_OPTION = generateOptionsKeyValueEnum(Object.values(REQUEST_TIME_LIMITATION), Object.keys(REQUEST_TIME_LIMITATION));
