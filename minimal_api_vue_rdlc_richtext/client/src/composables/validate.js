import { ref } from 'vue';
import ajv from 'boot/ajv';

export default function useValidate() {
  const errors = ref({});

  const errorHandler = (errorList) => {
    const lst = {};
    for (const error of errorList) {
      const obj = error;
      let dataPath = obj.instancePath
        .slice(1, obj.instancePath.length);

      if (!dataPath) {
        // this should be root schema
        if (obj.params.errors?.length > 0) {
          if (obj.params.errors[0].keyword === 'required') {
            dataPath = obj.params.errors[0].params.missingProperty;
          }
        }
      }
      if (!lst[dataPath]) {
        lst[dataPath] = obj.message;
      }
    }
    return lst;
  };

  const validate = (schema, data) => {
    errors.value = {};
    const validateFn = ajv.compile(schema);
    const valid = validateFn(data);
    if (validateFn.errors) {
      errors.value = errorHandler(validateFn.errors);
    }
    return valid;
  };

  return {
    errors,
    // methods
    validate,
  };
}
