<template>
  <div class="container">
    <div class="row">
      <HeaderNot />
    </div>
    <div class="container">
      <div class="row SrednjiRow">
        <div class="col-xl-12">
          <h1>Ups, stranica koju tražite ne postoji!</h1>
          <!-- <router-link :to="{ name: 'PosetilacHomePage'}"><button type="submit" class="btn btn-primary marstil">Vrati se na početnu stranicu</button></router-link> -->
          <button @click="vratiRutu" type="submit" class="btn btn-primary marstil"><font-awesome-icon :icon="['fas', 'arrow-alt-circle-left']" />&nbsp;&nbsp;Vrati se na početnu stranicu</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import HeaderNot from "@/components/HeaderNot.vue";

export default {
  name: "PageNotFound",
  components: {
    HeaderNot
  },
  computed:{
    tipKorisnika(){
      return this.$cookies.get("tip")
    }
  },
  methods:{
      vratiRutu(){
          if(this.tipKorisnika == "A"){
              this.$router.push('/AdminHomePage')
          }
          else if(this.tipKorisnika == "R"){
              this.$router.push('/RadnikHomePage')
          }
          else if(this.tipKorisnika == "M"){
              this.$router.push('/MusterijaHomePage')
          }
          else if(this.tipKorisnika == null){
              this.$router.push('/')
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
</style>