import Ajv from 'ajv';
import ajvErrors from 'ajv-errors';
import ajvKeywords from 'ajv-keywords';
import * as ajvFormat from 'ajv-formats';

const $ajv = new Ajv({
  allErrors: true,
  removeAdditional: true,
  useDefaults: true,
  coerceTypes: true,
  $data: true,
});
// enhance the ajv instance
ajvErrors($ajv);
ajvKeywords($ajv);
ajvFormat($ajv);

export default $ajv;
