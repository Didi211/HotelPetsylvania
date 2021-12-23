<template>
  <nav id="mainNav" class=" fixed-top navbar navbar-expand-lg bg-secondary text-uppercase">
    <div class="container d-flex justify-content-center">     
      <img class="img-responsive" src="../assets/logo.png" alt="">
      <router-link :to="{name: 'MusterijaHomePage'}"><a class="navbar-brand js-scroll-trigger fixed" href="#page-top">Hotel Petsylvania</a></router-link>
      <button class="navbar-toggler navbar-toggler-right text-uppercase font-weight-bold bg-primary text-white rounded" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
        Menu
      </button>      
      <div id="navbarResponsive" class="collapse navbar-collapse" >
        <ul class="navbar-nav ml-auto">
          <Notifikacije />
          <router-link :to="{name: 'Cenovnik'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#cenovnik">Cenovnik</a></li></router-link>
          <router-link :to="{name: 'OceneIRecenzije'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#registracija">Recenzije</a></li></router-link>
          <router-link :to="{name: 'OdsekZaPitanja'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#registracija">Pitanja</a></li></router-link>
          <router-link :to="{name: 'MojProfil'}"><li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded dropdown-item" href="#registracija">Profil</a></li></router-link>
          <router-link :to="{name: 'PosetilacHomePage'}"><li class="nav-item mx-0 mx-lg-1"><button class="nav-link py-3 px-0 px-lg-3 rounded text-uppercase dropdown-item" type="button" @click="logoutKorisnik" href="#registracija">Odjavi se</button></li></router-link> <!--ovo treba da bude dugme -->
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
import { defineComponent } from '@vue/composition-api'
import Notifikacije from '@/components/NotifikacijeMusterije.vue'
import Vue from 'vue'
import { mapState } from 'vuex'
import {mapGetters} from 'vuex';

export default defineComponent({
    setup() {
        
    },
    components:{
      Notifikacije,
    },
    data(){
      return{
        isNotifClicked: false,
      }
    },
    computed: {
      ...mapState(['trenutniKorisnik']),
      // notifikacije(){
      //   return this.$store.dispatch.getters['getterNotifikacijeMusterije']
      // }
    },
    methods: {
      logoutKorisnik(){
        this.$store.dispatch("logoutKorisnik")
      }
    },
    async created(){
      // await this.$store.dispatch("postaviOsobaID", this.$cookies.get("id"))
    //   // await this.$store.dispatch("getNotifikacijeMusterije")
    }
})
</script>
<style scoped>
button{
  color: #2b9b86;
}
</style>