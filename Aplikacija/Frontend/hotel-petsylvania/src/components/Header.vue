<template>
  <nav id="mainNav" class=" fixed-top navbar navbar-expand-lg bg-secondary text-uppercase">
    <div class="container d-flex justify-content-center">     
      <img class="img-responsive" src="../assets/logo.png" alt="">
      <router-link :to="{name: 'PosetilacHomePage'}"><a class="navbar-brand js-scroll-trigger fixed" href="#page-top">Hotel Petsylvania</a></router-link>
      <!-- <div class="dropdown"> -->   
      <button class="navbar-toggler navbar-toggler-right text-uppercase font-weight-bold bg-primary text-white rounded" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
        Menu 
      <!-- <span class="navbar-toggler-icon"></span> -->
      <!-- <i class="fas fa-bars"></i> -->
      </button>
      <!-- <div id="navbarResponsive" class="dropdown-menu ili collapse navbar-collapse" aria-labelledby="dropdownMenuButton">
          <a class="nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger" href="#portfolio">Portfolio</a>
          <a class=" nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger" href="#about">About</a>
          <a class=" nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger" href="#contact">Contact</a>
          <a class=" nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger" href="#registracija">Registracija</a>
        </div>  -->
      
      <div id="navbarResponsive" class="collapse navbar-collapse" >
        <ul class="navbar-nav ml-auto">
          <!-- <li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#portfolio">Galerija</a></li>
          <li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#about">O hotelu</a></li>
          <li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#contact">Kontakt</a></li> -->
          <!-- <li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#prijava">Prijava</a></li> -->
          <router-link :to="{name: 'Cenovnik'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#cenovnik">Cenovnik</a></li></router-link>
          <router-link :to="{name: 'OceneIRecenzije'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#registracija">Recenzije</a></li></router-link>
          <router-link :to="{name: 'OdsekZaPitanja'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#registracija">Pitanja</a></li></router-link>
          <router-link :to="{name: 'Login'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#prijava">Prijava</a></li></router-link>
          <!-- <li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#registracija">Registracija</a></li> -->
          <router-link :to="{name: 'Register'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#registracija">Registracija</a></li></router-link>
        </ul>
      </div>
    </div>
    <!-- <div class="btn-group">
        <button class="btn btn-primary text-uppercase font-weight-bold bg-primary text-white rounded dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false" aria-haspopup="true">
          Menu
        </button>
        <div class="dropdown-menu">
          <a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#portfolio">Portfolio</a>
          <a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#about">About</a>
          <a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#contact">Contact</a>
          <a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#prijava">Prijava</a>
          <a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#registracija">Registracija</a>
        </div>
      </div> -->
    <!-- </div> -->
    <!-- </div> -->
  </nav>
</template>

<script>
import { defineComponent } from '@vue/composition-api'

export default defineComponent({
    setup() {
        
    },
})
</script>

<style scoped>

</style>
