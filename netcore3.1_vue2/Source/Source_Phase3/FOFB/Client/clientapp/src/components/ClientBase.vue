<template>
  <div>
    <Header/>
    <router-view/>
    <Footer/>
  </div>
</template>

<script>
import Header from "@/components/Header"
import Footer from "@/components/Footer"



export default {
  name: "ClientBase",
  components: {
    Header,
    Footer
  },
   data(){
     return {
     }
   },
  computed: {
  },
  methods: {
  },
  mounted() {
  },
  destroyed() {
  },
};
</script>
<style lang="scss">
</style>
