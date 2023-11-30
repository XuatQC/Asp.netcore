<template>
  <div>
    <table class="table-cart table">
      <thead>
        <tr>
          <th width="49%" class="product">商品名</th>
          <th width="16%" class="color">カラー</th>
          <th width="10%" class="size">サイズ</th>
          <th width="10%" class="quantity">数量</th>
          <th width="15%" class="subtotal">小計</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(itemProduct, index) in this.dataCart" :key="index">
          <td class="product">
            <div class="product-name fix-product-name">
              <span>{{ itemProduct.productName }}</span>
            </div>
            <div class="product-code">
              <span>{{ itemProduct.productCd }}</span>
            </div>
          </td>
          <td class="color">{{ itemProduct.colorName }}</td>
          <td class="size">{{ itemProduct.sizeName }}</td>
          <td class="quantity">{{ itemProduct.quantity }}</td>
          <td class="subtotal">¥{{ formatPrice(itemProduct.subTotal) }}</td>
        </tr>
      </tbody>
    </table>
    <div class="row order-total">
      <div class="total-title">
        <label>ご予約合計金額</label>
      </div>
      <div class="total-quantity">
        <span>{{ quantityTotal }}</span>
      </div>
      <div class="total-subtotal">
        ¥{{  formatPrice(amountTotal) + '(税込)'}}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Cart",
  props: {
    dataCart: null,
    quantityTotal: null,
    amountTotal: null,
  }
};
</script>