import accountSchema from 'models/account';

const loginSchema = {
  type: 'object',
  required: ['email', 'password'],
  properties: {
    email: {
      ...accountSchema.email,
      errorMessage: { // it will override errorMessage in email
        minLength: 'Email is required',
        _: 'Email is invalid',
      },
    },
    password: {
      ...accountSchema.password,
      errorMessage: { // it will override errorMessage in email
        minLength: 'Password is required',
        _: 'Password is invalid',
      },
    },
  },
  additionalProperties: false,
};

export default loginSchema;
