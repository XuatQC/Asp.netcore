const defaultTheme = require('tailwindcss/defaultTheme');
/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './index.html',
    './src/**/*.{vue,js,ts,jsx,tsx}',
  ],
  theme: {
    extend: {},
    fontSize: {
      ...defaultTheme.fontSize,
      xl: ['1.375rem'],
    },
  },
  plugins: [],
  prefix: 'tw-',
};
