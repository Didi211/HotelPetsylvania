<template>
  <nav id="mainNav" class=" fixed-top navbar navbar-expand-lg bg-secondary text-uppercase">
    <div class="container d-flex justify-content-center">     
      <img class="img-responsive" src="../assets/logo.png" alt="">
      <div v-if="Musterija">
        <router-link :to="{name: 'MusterijaHomePage'}"><a class="navbar-brand js-scroll-trigger fixed" href="#page-top">Hotel Petsylvania</a></router-link>
      </div>
      <div v-else-if="Radnik">
        <router-link :to="{name: 'RadnikHomePage'}"><a class="navbar-brand js-scroll-trigger fixed" href="#page-top">Hotel Petsylvania</a></router-link>
      </div>  
      <div v-else>
        <router-link :to="{name: 'PosetilacHomePage'}"><a class="navbar-brand js-scroll-trigger fixed" href="#page-top">Hotel Petsylvania</a></router-link>
      </div>    
    </div>
  </nav>
</template>

<script>
import { defineComponent } from '@vue/composition-api'
import Vue from 'vue'

export default defineComponent({
  setup() {
    
  },
  computed: {
    Musterija() {
      return Vue.$cookies.get("tip")== 'M'
    },
    Radnik(){
      return Vue.$cookies.get("tip")== 'R'
    },
  }
})
</script>