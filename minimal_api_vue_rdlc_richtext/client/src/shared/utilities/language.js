import { useAppStore } from 'src/stores/app-store';
import { i18n } from 'boot/i18n';

export const langs = [
  {
    id: 0, value: 'or-og', name: 'オリジナル版／Original', code: 'O',
  },
  {
    id: 1, value: 'ja-JP', name: '日本語版／Japanese', code: 'J',
  },
  {
    id: 2, value: 'en-US', name: '英語版／English', code: 'E',
  },
];

// システム言語の変更
export const changeLanguage = (langObject) => {
  let language;
  const langObjectKeys = Object.keys(langObject);
  // langの全てpropをループする
  for (const langProp of langObjectKeys) {
    // propがfalsyじゃないなら、そのpropと比較する
    if (langProp) {
      language = langs.find((langItem) => langItem[langProp] === langObject[langProp]);
      break;
    }
  }
  if (!language) [, language] = langs;
  const { setLanguage } = useAppStore();
  setLanguage(language);
  const { locale } = i18n.global;
  locale.value = language.value;
};
