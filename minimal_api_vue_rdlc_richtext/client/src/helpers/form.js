import { ref } from 'vue';
import cloneDeep from 'lodash/cloneDeep';
import isEqual from 'lodash/isEqual';

const formData = ref({});
const originalFormData = ref({});

// init data before changing
const init = (data) => {
  formData.value = data;
  originalFormData.value = cloneDeep(data);
};

// check data changes
const hasChange = () => {
  // Check original data is not empty
  if (Object.keys(formData.value).length === 0) {
    return false;
  }

  // check data changes
  return !isEqual(formData.value, originalFormData.value);
};

const getOriginalForm = () => originalFormData;

// reset data
const reset = () => {
  formData.value = {};
  originalFormData.value = {};
};

export default {
  init,
  hasChange,
  getOriginalForm,
  reset,
};
