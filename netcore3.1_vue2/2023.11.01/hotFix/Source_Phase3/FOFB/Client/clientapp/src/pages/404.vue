<template>
  <div id="container">
    <div class="child">
      <span class="text-404">404</span>
      <p class="text-bellow">PAGE NOT FOUND</p>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style>
#container {
  position: relative;
  height: 400px;
}
.child {
  /* Center vertically and horizontally */
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.text-404 {
  font-family: fredoka one, cursive;
  font-size: 168px !important;
  margin: 0;
  color: #ff508e;
  text-transform: uppercase;
}
.text-bellow {
  padding-top: 15px;
  padding-left: 90px;
  font-weight: bold;
}
</style>
