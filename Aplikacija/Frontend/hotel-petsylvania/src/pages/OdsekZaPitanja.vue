<template>
  <div>
    <div class="container-fluid">
      <div class="row">
        <div v-if="korisnik=='R'">
          <HeaderRadnik />
        </div>
        <div v-else-if="korisnik=='M'">
          <HeaderMusterija />
        </div>
        <div v-else>
          <Header />
        </div>
      </div>
      <div v-if="!isDataLoaded">
        <AppSpinner />
      </div>
      <div v-else>
        <div class="container SrednjiRow">
          <h3>Pitanja i odgovori</h3>
          <div class="row Odsek">
            <Pitanje v-for="pitanje in pitanja" :key="pitanje.id" :pitanje="pitanje"/>
            <div class="nemaOdg" v-if="korisnik==null">
              <h5>Morate biti ulogovani da biste postavili pitanje...</h5>
            </div>
            <div v-else-if="korisnik=='M'">
              <div v-if="!prikaziDodavanje">
                <button style="margin-bottom:15px" type="submit" class="btn btn-primary marstil dugme" @click="togglePrikaziDodavanje">Postavi novo pitanje</button>
              </div>
              <div v-else>
                <a @click="togglePrikaziDodavanje">Odustani <font-awesome-icon :icon="['fas', 'chevron-up']" /></a>
                <form action="/action_page.php" @submit.prevent>
                  <div class="form-group">
                    <!-- <label class="labele" for="odgovor">Napisi novo pitanje:</label> -->
                    <textarea
                      v-model="novoPitanje"
                      class="form-control rounded-0"
                      id="novoPitanje"
                      name="novoPitanje"
                      rows="3"
                    ></textarea>
                  </div>
                  <button style="margin-bottom:15px" type="submit" class="btn btn-primary marstil" @click="postaviNovoPitanje"> Pitaj! </button>
                </form>
              </div>
            </div>
          </div>
        </div>
        
        <div class="row">
          <Footer />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import Header from "@/components/Header.vue";
import HeaderRadnik from "@/components/HeaderRadnik.vue";
import HeaderMusterija from "@/components/HeaderMusterija.vue";

import Footer from "@/components/Footer.vue";
import Pitanje from "@/components/Pitanje.vue"

export default {
  name: "OdsekZaPitanja",
  components: {
    Header,
    Footer,
    Pitanje,
    HeaderRadnik,
    HeaderMusterija
  },
  data() {
    return {
      isDataLoaded:false,
      prikaziDodavanje:false,
      novoPitanje:""
    };
  },
  computed:{
    pitanja() {
      return this.$store.getters['getPitanja']
    },
    korisnik(){
      return this.$cookies.get("tip")
    },
    musterijaID(){
      return this.$store.getters['getOsobaID']
    }
  },
  async created(){
    await this.$store.dispatch('getPitanja').then(()=>{
      this.isDataLoaded=true;
    })
  },
  methods:{
    togglePrikaziDodavanje(){
      this.prikaziDodavanje = !this.prikaziDodavanje
    },
    postaviNovoPitanje(){
      if(this.novoPitanje==""){
        this.$toasted.show("Ne možete poslati prazno pitanje!", { 
                    theme: "bubble", 
                    position: "top-center", 
                    duration : 2000
               })
      }
      else if(this.novoPitanje.length >256){
        this.$toasted.show("Pitanje ne sme biti veće od 256 karaktera!", { 
                    theme: "bubble", 
                    position: "top-center", 
                    duration : 2000
               })
      }
      else{
        let obj = {
          tekstPitanja: this.novoPitanje,
          musterijaID: this.musterijaID
        }
        this.$store.dispatch("postaviPitanjeMusterija", obj).then(()=>{
          this.prikaziDodavanje = false
        })
      }
      
    }
  }
};
</script>

<style scoped>
.SrednjiRow {
  padding-top: 6rem;
}

.Odsek {
  background-color: rgba(255, 255, 255, 0.7);
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 4px solid rgb(255, 255, 255);
  margin-bottom:10px;
}

.nemaOdg{
  margin:10px;
}
</style>