<template>
  <nav id="mainNav" class=" fixed-top navbar navbar-expand-lg bg-secondary text-uppercase">
    <div class="container d-flex justify-content-center">     
      <img class="img-responsive" src="../assets/logo.png" alt="">
      <router-link :to="{name: 'AdminHomePage'}"><a class="navbar-brand js-scroll-trigger fixed" href="#page-top">Hotel Petsylvania</a></router-link>
      <button class="navbar-toggler navbar-toggler-right text-uppercase font-weight-bold bg-primary text-white rounded" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
        Menu
      </button>      
      <div id="navbarResponsive" class="collapse navbar-collapse" >
        <ul class="navbar-nav ml-auto">
          <router-link :to="{name: 'AdminObavestenja'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#cenovnik">Aktuelna obaveštenja</a></li></router-link>
          <router-link :to="{name: 'AdminListaRadnika'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#registracija">Radnici</a></li></router-link>
          <router-link :to="{name: 'PosetilacHomePage'}"><li class="nav-item mx-0 mx-lg-1"><button class="nav-link py-3 px-0 px-lg-3 rounded text-uppercase dropdown-item" type="button" @click="logoutKorisnik" href="#registracija">Odjavi se</button></li></router-link> <!--ovo treba da bude dugme -->
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
import { defineComponent } from '@vue/composition-api'
import { mapState } from 'vuex'

export default defineComponent({
    setup() {
        
    },
    computed: {
      ...mapState(['trenutniKorisnik'])
    },
    methods: {
      logoutKorisnik(){
        this.$store.dispatch("logoutKorisnik")
      }
    }
})
</script>
<style scoped>
button{
  color: #2b9b86;
}
</style>