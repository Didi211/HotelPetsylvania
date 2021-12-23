<template>
  <nav id="mainNav" class=" fixed-top navbar navbar-expand-lg bg-secondary text-uppercase">
    <div class="container d-flex justify-content-center">     
      <img class="img-responsive" src="../assets/logo.png" alt="">
      <router-link :to="{name: 'PosetilacHomePage'}"><a class="navbar-brand js-scroll-trigger fixed" href="#page-top">Hotel Petsylvania</a></router-link>
    </div>
  </nav>
</template>

<script>
import { defineComponent } from '@vue/composition-api'

export default {
  computed:{
    tipKorisnika(){
      return this.$store.state.trenutniKorisnik.tip
    }
  },
  methods:{
    compare(a,b){
      return a === b
      }
  }
};
</script>
<style scoped>

</style>
