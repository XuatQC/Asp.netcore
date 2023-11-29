import { ACCOUNT_STATUS_KBN } from 'helpers/enum';

const accountSchema = {
  tenant_code: {
    type: 'string',
    minLength: 1,
    maxLength: 10,
    errorMessage: {
      minLength: 'コードを入力してください。',
      maxLength: '10文以内で入力してください。',
      _: 'コードを正しく入力してください。',
    },
  },
  name: {
    type: 'string',
    minLength: 1,
    maxLength: 80,
    errorMessage: {
      minLength: '氏名を入力してください。',
      maxLength: '80文以内で入力してください。',
      _: '氏名を正しく入力してください。',
    },
  },
  password: {
    type: 'string',
    minLength: 8,
    maxLength: 50,
    pattern: '^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$',
    errorMessage: {
      pattern: 'パスワードのパターンが違いです。',
      minLength: '8文字以上',
      maxLength: '50文以内で入力してください。',
      _: 'パスワードを正しく入力してください。',
    },
  },
  email: {
    type: 'string',
    minLength: 1,
    maxLength: 50,
    pattern: '^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$',
    errorMessage: {
      minLength: 'メールアドレスを入力してください。',
      maxLength: '50文以内で入力してください。',
      pattern: 'メールのパターンが違いです。',
      _: 'メールアドレスを正しく入力してください。',
    },
  },
  status_kbn: {
    type: 'string',
    enum: Object.values(ACCOUNT_STATUS_KBN), // 01: Active, 02: Inactive
    errorMessage: {
      _: '許可された値のいずれかに等しい必要があります。',
    },
  },
};
export default accountSchema;
