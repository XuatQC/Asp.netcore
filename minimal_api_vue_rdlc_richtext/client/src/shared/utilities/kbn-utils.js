/* eslint-disable no-restricted-syntax */
export function displayKbnKey(kbn, selectedValue) {
  for (const [key, value] of Object.entries(kbn)) {
    if (selectedValue === value) {
      return key;
    }
  }

  return '';
}
