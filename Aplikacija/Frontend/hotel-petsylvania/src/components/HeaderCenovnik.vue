<template>
  <nav id="mainNav" class=" fixed-top navbar navbar-expand-lg bg-secondary text-uppercase">
    <div class="container d-flex justify-content-center">     
      <img class="img-responsive" src="../assets/logo.png" alt="">
      <div v-if="Musterija">
        <router-link :to="{name: 'MusterijaHomePage'}"><a class="navbar-brand js-scroll-trigger fixed" href="#page-top">Hotel Petsylvania</a></router-link>
      </div>
      <div v-else>
        <router-link :to="{name: 'PosetilacHomePage'}"><a class="navbar-brand js-scroll-trigger fixed" href="#page-top">Hotel Petsylvania</a></router-link>
      </div>
      <!-- <div class="dropdown"> -->   
      <button class="navbar-toggler navbar-toggler-right text-uppercase font-weight-bold bg-primary text-white rounded" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
        Menu 
      <!-- <span class="navbar-toggler-icon"></span> -->
      <!-- <i class="fas fa-bars"></i> -->
      </button>      
      <div id="navbarResponsive" class="collapse navbar-collapse" >
        <ul class="navbar-nav ml-auto">
          <router-link :to="{name: 'Login'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#prijava">Prijava</a></li></router-link>
          <router-link :to="{name: 'Register'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#registracija">Registracija</a></li></router-link>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
import { defineComponent } from '@vue/composition-api'

export default defineComponent({
  setup() {
    
  },
  computed: {
    Musterija() {
      return Vue.$cookies.get("tip")== 'M'
    }
  }
})
</script>
