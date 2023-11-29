import Axios from 'axios';
import toastUtil from 'utilities/toast';
import { useAuthStore } from 'stores/auth-store';
import { useAppStore } from 'stores/app-store';
import { useLoadingStore } from 'stores/loading-store';

// merge params to axios config
function mergeAxiosConfig(data, config) {
  const axiosConfig = Object.assign(config || {}, {
    params: data,
  });
  return axiosConfig;
}

export default class RestClient {
  constructor(servicePath) {
    this.client = this.createAxiosClient();
    this.servicePath = servicePath;
  }

  createAxiosClient() {
    const axiosInstance = Axios.create({
      // set server url by environment variable
      baseURL: `${process.env.API_URL}api`,
      headers: {
        'Content-type': 'application/json',
      },
      // allow cookies
      withCredentials: true,
    });
    axiosInstance.interceptors.request.use(this.onRequest, this.onRequestError);
    return axiosInstance;
  }

  createCustomHeader() {
    const { initial } = useAuthStore();
    const { plantName } = useAppStore();
    return JSON.stringify({ plantName, userInitial: initial });
  }

  onRequest = async (config) => {
    const authStore = useAuthStore();
    // set token from store to header
    if (config.headers) {
      config.headers.Authorization = `Bearer ${authStore.token}`;
      config.headers.RequestHeaderDto = this.createCustomHeader();
    }
    // do something here before request
    return config;
  };

  onRequestError(error) {
    return Promise.reject(error);
  }

  #processThen(response, reject, resolve) {
    const result = response.data;
    // reject if status code abnormal
    if (result.code !== 0 && result.code !== 200) {
      toastUtil.error(result.message);
      reject(result);
    }
    resolve(result);
  }

  #processError(error, reject) {
    if (Axios.isAxiosError(error)) {
      if (error.response) {
        // The request was made and the server responded with a status code
        // that falls out of the range of 2xx
        if (error.response.data) {
          // error data from server
          const serviceErrorResponse = error.response.data;
          if (serviceErrorResponse.message) {
            toastUtil.error(serviceErrorResponse.message);
          }
          reject(serviceErrorResponse);
        } else {
          reject(error.response);
        }
      } else {
        toastUtil.exception(error);
        reject(error);
      }
    } else {
      reject(error);
    }
  }

  request(method, url, data, config) {
    const loading = useLoadingStore();
    return new Promise((resolve, reject) => {
      // show loading
      loading.show();
      const axiosConfig = Object.assign(config, {
        url: `${this.servicePath}${url}`,
        method,
        data,
      });
      this.client
        .request(axiosConfig)
        .then((response) => {
          // add a rawValue: if it's true, there are not to check response code
          const result = response.data;
          if (!config.rawValue) {
            this.#processThen(response, reject, resolve);
          } else {
            resolve(result);
          }
        })
        .catch((error) => {
          // process when error
          this.#processError(error, reject);
        })
        .finally(() => {
          // hide if not multi loading
          if (!loading.isMultiLoading) {
            loading.hide();
          }
        });
    });
  }

  get(url, data, config) {
    // merge params to axios config
    const axiosConfig = mergeAxiosConfig(data, config);
    return this.request('GET', url, null, axiosConfig);
  }

  post(url, data, config) {
    // merge params to axios config
    const axiosConfig = mergeAxiosConfig(data, config);
    delete axiosConfig.params;
    return this.request('POST', url, data, axiosConfig);
  }

  put(url, data, config) {
    // merge params to axios config
    const axiosConfig = mergeAxiosConfig(data, config);
    delete axiosConfig.params;
    return this.request('PUT', url, data, axiosConfig);
  }

  delete(url, data, config) {
    // merge params to axios config
    const axiosConfig = mergeAxiosConfig(data, config);
    return this.request('DELETE', url, null, axiosConfig);
  }
}
