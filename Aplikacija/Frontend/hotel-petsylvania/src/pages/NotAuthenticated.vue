<template>
  <div class="container">
    <div class="row">
      <HeaderNot />
    </div>
    <div class="container">
      <div class="row SrednjiRow">
        <div class="col-xl-12">
          <h1>Ups, nemate odgovarajuće privilegije da pristupite ovoj stranici!</h1>
          <div class="col-xl-12 margine">
            <router-link :to="{ name: 'PosetilacHomePage'}"><button type="submit" class="btn btn-primary marstil"> <font-awesome-icon :icon="['fas', 'arrow-alt-circle-left']" />&nbsp;&nbsp;Vrati se na početnu stranicu</button></router-link>
          </div>
          <!-- <div class="col-xl-12 margine">
            <router-link :to="{ name: 'Login'}"><button type="submit" class="btn btn-primary marstil">Prijavi se</button></router-link>
          </div> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import HeaderNot from "@/components/HeaderLoginRegister.vue";
import { mapGetters } from 'vuex'

export default {
  name: "PageNotAuthenticated",
  components: {
    HeaderNot
  },
  computed:{
      // ...mapGetters({
      //   'tipKorisnika': 'getTipKorisnika'
      // })
  },
  methods:{
      vratiRutu(){
          if(this.tipKorisnika == "A"){
              return 'AdminHomePage'
          }
          else if(this.tipKorisnika == "R"){
              return 'RadnikHomePage'
          }
          else if(this.tipKorisnika == "M"){
              return 'MusterijaHomePage'
          }
          else{
              return 'PosetilacHomePage'
          }
      }
  }

};
</script>

<style scoped>
.SrednjiRow {
  padding-top: 6rem;
  justify-content: space-between;
}
h1{
    margin:1.2rem;
}
.margine{
    margin-top:1.2rem;
    margin-bottom:1.2rem;
}
</style>