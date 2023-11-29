<template>
  <div class="container">
    <div class="tw-flex items-stretch justify-between">
      <div class="tw-mr-8 tw-flex-1">
        <div
          class="tw-h-full tw-border main-image-container tw-flex tw-items-center tw-overflow-hidden tw-justify-center">
          <label
            for="custom-file-import"
            :class="{ 'drag-area': isDragImage }"
            class="tw-h-full tw-border tw-flex tw-items-center tw-overflow-hidden tw-justify-center tw-absolute tw-top-0 tw-right-0 tw-left-0 tw-bottom-0 tw-z-20"
            @drop.prevent="onDropImage"
            @click.prevent
            @dragenter="isDragImage = true"
            @dragleave="isDragImage = false"
          >
          </label>
          <Transition name="fade">
            <div
              class="tw-absolute tw-top-0 tw-right-0 tw-left-0 tw-bottom-0 tw-z-10 tw-transition-all"
              v-if="isDragImage"
            >
              <div class="tw-h-full tw-w-full tw-bg-[#1d1d1d87] tw-flex tw-items-center tw-justify-center">
                <i
                  class="q-icon notranslate material-icons tw-text-2xl tw-text-white"
                  aria-hidden="true"
                  role="presentation"
                >upload_file</i>
                <span class="tw-text-lg tw-text-white">drop your image here!</span>
              </div>
            </div>
          </Transition>
          <img
            v-if="selectedImage && selectedImage.src !== ''"
            :src="!selectedImage?.src ? `${photoPath}${selectedImage?.fileName}` : `${selectedImage?.prefix},${selectedImage?.src}`"
            :alt="selectedImage.alt"
            class="tw-bg-white main-image"
          />
        </div>
        <div class="tw-mb-4 tw-mt-6 tw-flex tw-justify-between tw-items-center">
          <div class="tw-flex tw-px-1 tw-ml-[auto]">
            <q-checkbox v-model="selectedRepresentImageCheckBox" dense :label="$t('photograph.mainPhotoLabel')" :disable="props.isReadOnly" name=""  id="representImage"/>
          </div>
          <div class="tw-flex justify-between">
            <input
              type="file"
              class="tw-hidden"
              name="custom-file-import"
              id="custom-file-import"
              accept=".jpg, .jpeg, .bmp"
              @input="(event) => inputImageHandler(event)"
              ref="uploadFileInput"
              :disabled="props.isReadOnly"
              multiple
            />
            <label
              for="custom-file-import"
              class="btn tw-mr-1 tw-text-center add"
              :class="{ disabled: props.isReadOnly }"
            >{{ $t('add') }}</label>
            <button
              class="btn tw-mr-1 delete"
              @click="removeImage"
              :class="{ disabled: props.isReadOnly || imageList?.length === 0}"
            >{{ $t('delete') }}</button>
            <button
              class="btn close"
              @click="closePopup"
            >{{ $t('close') }}</button>
          </div>
        </div>
        <div class="tw-bg-slate-100">
          {{ $t('photograph.maxSizeFileWarningMessage') }}
        </div>
      </div>
      <div class="tw-w-full tw-flex-1 ">
        <div
          class="tw-grid tw-grid-cols-3 tw-gap-2 tw-overflow-y-auto"
          style="width: 530px;height:490px"
        >
          <template v-if="imageList.length != 0 || imageList != null">
            <div
              v-for="(image, index) in imageList"
              :key="index"
              :ref="(el) => { if (image === selectedImage) latestImageItem = el }"
            >
              <div class="tw-my-1 ">
                <span
                  class="tw-whitespace-pre-line"
                  v-if="image"
                > {{ index + 1 }}
                  &ensp;
                  {{ image == selectedRepresentImage ? $t('photograph.mainPhotoLabel') : '' }}
                </span>
                <span
                  v-else
                  class="tw-whitespace-pre-line"
                >
                  &ensp;
                </span>
              </div>
              <div
                class="image-select-box tw-border tw-overflow-hidden"
                :class="{
                  'image-select-box-selected':
                    image === selectedImage && imageList.length !== 0
                }"
                @click="clickChooseImageHandler(image)"
              >
                <img
                  class="tw-bg-white"
                  :src="!image?.src ? `${photoPath}${image?.fileName}` : `${image?.prefix},${image?.src}`"
                  :alt="image?.alt"
                />
              </div>
            </div>
            <div
              v-for="index in numberOfImagePlaceHolder"
              :key="index"
            >
              <div class="tw-my-1 ">
                <span class="tw-whitespace-pre-line">
                  &emsp;
                </span>
                <span class="tw-whitespace-pre-line">
                  &emsp;
                </span>
              </div>
              <div class="image-select-box tw-border tw-overflow-hidden">
              </div>
            </div>
          </template>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import dialog from 'utilities/dialog';
import {
  computed, ref, onMounted, onUnmounted, nextTick,
} from 'vue';
import {
  EVENTS, MAX_IMAGE_SIZE,
} from 'helpers/enum';
import {
  CONFIRM_DIALOG,
  MODAL, MSG,
} from 'helpers/constant';
import Compressor from 'compressorjs';
import { useAppStore } from 'stores/app-store';

// 1) ======= INITIALIZATION ========
const app = useAppStore();
const props = defineProps({
  imageList: {
    type: Array,
    required: true,
  },
  isReadOnly: {
    type: Boolean,
    default: false,
  },
  photoNumSelected: {
    type: Number,
    default: 0,
  },
  photoSelect: Function,
  obs: {
    type: Object,
    required: true,
  },
  fact: {
    type: Object,
    required: true,
  },
});

// 2) ======= VARIABLE REF ========

const imageList = ref([]);
const latestImageItem = ref();
const selectedImage = ref(null);
const selectedRepresentImage = ref(null);
const uploadFileInput = ref();
const isDragImage = ref(false);
const photoPath = computed(() => {
  let apiPath = process.env.API_URL;
  if (apiPath === '/') {
    apiPath = `${window.location.origin}/`;
  }
  return `${apiPath}${app.folderStore.photoLiveFolder}/`;
});
const selectedRepresentImageCheckBox = computed({
  get() {
    if (imageList.value.length === 0) return false;
    return selectedImage.value && selectedImage.value === selectedRepresentImage.value;
  },
  set(value) {
    if (value) selectedRepresentImage.value = selectedImage.value;
    else selectedRepresentImage.value = null;
  },
});

// 3) ======= METHOD/FUNCTION ========
// プレース保持用の枚数
const numberOfImagePlaceHolder = computed(() => {
  let theNumber = imageList.value.length;
  while (theNumber % 3 !== 0) {
    theNumber++;
  }
  if (imageList.value.length < 9) {
    return 9 - imageList.value.length;
  }
  return theNumber - imageList.value.length;
});

//  写真一枚を選択するイベント
const clickChooseImageHandler = (value) => {
  selectedImage.value = value;
};

// 写真ファイルのBase64を取得する
function getBase64(file) {
  return new Promise((done, fail) => {
    const reader = new FileReader();
    // 渡されているファイルを読み込む
    reader.readAsDataURL(file);
    reader.onload = () => {
      // 写真オブジェクトの作成
      const currentImage = {
        alt: file.name,
        prefix: reader.result.split(',')[0],
        src: reader.result.split(',')[1],
        extension: reader.result.split(';')[0].split('/')[1],
        factNum: props.fact.factNum,
        serialNum: 0,
        numNoRevisions: props.obs.numNoRevisions,
        representPhotoFlag: false,
        fileName: '',
        deleteFlag: false,
        size: `${file.size}`,
      };
      // 作成した写真を写真一覧に追加する
      imageList.value.push(currentImage);
      done(currentImage);
    };
    // 写真読込にエラーが発生した場合
    reader.onerror = (error) => {
      fail(error);
    };
  });
}

// 選択中の写真までスクロールする
const scrollToSelectedImage = () => {
  if (latestImageItem.value) {
    latestImageItem.value.scrollIntoView({ behavior: 'smooth' });
  }
};

// main photo、selected imageの値を変更する
const afterAddImageHandler = (newestImage) => {
  if (newestImage) {
    // 追加したばかりの写真を選択中の状態にする
    selectedImage.value = newestImage;
    uploadFileInput.value.value = null;
    nextTick(() => {
      scrollToSelectedImage();
    });
  }
  if (imageList.value.length === 1) {
    selectedRepresentImageCheckBox.value = true;
  }
};

// 写真を一覧に入れる
const pushToImageList = async (imageBlob) => {
  const currentImage = await getBase64(imageBlob);
  afterAddImageHandler(currentImage);
};

// 写真サイズをMAX_IMAGE_SIZE以下になるまでリサイズする
const resizeImage = (file, quality = 0.9) => {
  const compressorPromise = new Compressor(
    file,
    {
      minWidth: 640,
      minHeight: 480,
      width: 640,
      height: 480,
      quality,
      convertSize: MAX_IMAGE_SIZE,
      resize: 'none',
      mimeType: 'png',
      convertTypes: ['image/png'],
      success(result) {
        if (result.size > MAX_IMAGE_SIZE) {
          resizeImage(result, quality - 0.1);
        } else {
          pushToImageList(result);
        }
      },
    },
  );
  return compressorPromise;
};

// 写真をbase64に変換し保存する
const uploadImageHandler = async (files) => {
  let newestImage = null;
  const allowedExtensions = /(\.jpg|\.jpeg|\.bmp)$/i;
  const base64ConversionPromises = [];
  const notAllowFileExtensions = [];
  for (const file of files) {
    // ファイルは許可拡張子を満たすか確認
    if (allowedExtensions.exec(file.name)) {
      if (file.size > MAX_IMAGE_SIZE) {
        resizeImage(file);
      } else {
        base64ConversionPromises.push(getBase64(file));
      }
    } else {
      notAllowFileExtensions.push(file);
    }
  }
  if (notAllowFileExtensions.length > 0) {
    await dialog.showMessage(MODAL.TITLE_INFO, MSG.PHOTO_FORMAT_INCORRECT);
  }
  const recentAddedImage = await Promise.all(base64ConversionPromises);

  newestImage = recentAddedImage[recentAddedImage.length - 1];
  // 写真一覧に追加された最新写真の値を処理する
  afterAddImageHandler(newestImage);
};

// input:fileの値を変更するイベント
const inputImageHandler = async (event) => {
  await uploadImageHandler(event.srcElement.files);
};

// 写真削除ボタン押下イベント
const removeImage = async () => {
  // 確認ポップアップの表示
  const result = await dialog.showConfirm(MODAL.TITLE_INFO, MSG.DELETE_PHOTO);
  if (result === CONFIRM_DIALOG.YES) {
    const index = imageList.value.indexOf(selectedImage.value);
    let reAssignSelectedRepresentImage = false;
    // 削除された写真が代表写真なら、代表写真の値を変更する
    if (selectedRepresentImageCheckBox.value) {
      reAssignSelectedRepresentImage = true;
    }
    // 写真がimageListに存在するなら、削除する
    if (index > -1) {
      imageList.value.splice(index, 1);
    }
    // 選択中写真を隣の写真に変更する
    const theNextChooseImage = imageList.value[index] ? imageList.value[index] : imageList.value[index - 1];
    selectedImage.value = theNextChooseImage;
    // 代表写真なら値をtrueにする
    if (reAssignSelectedRepresentImage) {
      selectedRepresentImageCheckBox.value = true;
    }
  }
};

// ポップアップを閉じるイベント
const closePopup = () => {
  if (selectedRepresentImage.value) {
    imageList.value = imageList.value.map((x) => {
      x.representPhotoFlag = false;
      return x;
    });
    selectedRepresentImage.value.representPhotoFlag = true;
  }
  dialog.hide(imageList.value);
};

// 写真をドラッグ＆ドロップイベント
const onDropImage = (inputFileDrag) => {
  isDragImage.value = false;
  uploadImageHandler(inputFileDrag.dataTransfer.files);
};

// 4) ======= VUEJS LIFECYCLE ========

// コンポーネント初期後
onMounted(() => {
  const events = [EVENTS.DRAG_ENTER, EVENTS.DRAG_LEAVE, EVENTS.DRAG_OVER, EVENTS.DROP];

  // dragEnter,dragLeaveなどのイベントのデフォルトイベントをブロックする
  for (const event of events) {
    document.body.addEventListener(event, (e) => { e.preventDefault(); });
  }
  imageList.value = props.imageList;
  selectedRepresentImage.value = imageList.value.find((x) => x.representPhotoFlag);
  selectedImage.value = selectedRepresentImage.value;
  nextTick(() => {
    scrollToSelectedImage();
  });
});

// コンポーネントをキャンセル後
onUnmounted(() => {
  const events = [EVENTS.DRAG_ENTER, EVENTS.DRAG_LEAVE, EVENTS.DRAG_OVER, EVENTS.DROP];
  // dragEnter,dragLeaveなどのイベントのデフォルトイベントをブロックすることを解除する
  for (const event of events) {
    document.body.removeEventListener(event, (e) => { e.preventDefault(); });
  }
});

</script>

<style scoped>
.main-image-container {
  height: 386px;
  width: 546px;
  background-color: var(--vt-c-text-dark-2);
  position: relative;
}

.image-select-box {
  height: 125px;
  display: flex;
  background-color: var(--vt-c-text-dark-2);
  align-items: center;
}

.image-select-box-selected {
  border-color: red;
}

.drag-area {
  opacity: 0.5;

}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
