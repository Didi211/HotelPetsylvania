<template>
  <div>
    <div class="container-fluid">
      <div class="row">
        <div v-if="korisnik=='M'">
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
          <h3>Ocene i recenzije</h3>
          <div class="row Odsek">
            <Recenzija v-for="recenzija in recenzije" :key="recenzija.id" :recenzija="recenzija"/>
            <div v-if="korisnik=='M'">
              <div class="pt-4" v-if="!prikaziDodavanje">
                <button style="margin-bottom:15px" type="submit" class="btn btn-primary marstil dugme" @click="togglePrikaziDodavanje">Oceni hotel</button>
              </div>
              <div v-else>
                <a @click="togglePrikaziDodavanje">Odustani <font-awesome-icon :icon="['fas', 'chevron-up']" /></a>
                <div class="stilZaZvezde mt-2 mb-2">
                  <star-rating v-model="ocena" 
                               :star-size="30"
                               inactive-color="#f2af58"
                               active-color="#289b86"
                               :show-rating="true"
                               :glow="2"
                               glow-color="#ffffff"/>
                </div>
                <form action="/action_page.php" @submit.prevent>
                  <div class="form-group">
                    <!-- <label class="labele" for="odgovor">Napisi novo pitanje:</label> -->
                    <textarea
                      v-model="novaRecenzija"
                      class="form-control rounded-0"
                      id="novaRecenzija"
                      name="novaRecenzija"
                      rows="3"
                      placeholder="Dodajte komentar..."
                    ></textarea>
                  </div>
                  <button style="margin-bottom:15px" type="submit" class="btn btn-primary marstil" @click="postaviRecenziju">  Postavi ocenu! </button>
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
import Recenzija from "@/components/Recenzija.vue"

export default {
  name: "OceneIRecenzije",
  components: {
    Header,
    Footer,
    Recenzija,
    HeaderRadnik,
    HeaderMusterija
  },
  data() {
    return {
      isDataLoaded:false,
      prikaziDodavanje:false,
      novaRecenzija:"",
      ocena:0
    };
  },
  computed:{
    recenzije() {
      return this.$store.getters['getRecenzije']
    },
    korisnik(){
      return this.$cookies.get("tip")
    },
    musterijaID(){
      return this.$store.getters['getOsobaID']
    }
  },
  async created(){
    await this.$store.dispatch('getRecenzije').then(()=>{
      this.isDataLoaded=true;
    })
  },
  methods:{
    togglePrikaziDodavanje(){
      this.prikaziDodavanje = !this.prikaziDodavanje
    },
    postaviRecenziju(){
      if(this.novaRecenzija==""){
        this.$toasted.show("Ne možete poslati praznu poruku!", { 
                    theme: "bubble", 
                    position: "top-center", 
                    duration : 2000
               })
      }
      else if(this.novaRecenzija.length >256){
        this.$toasted.show("Poruka ne sme biti veća od 256 karaktera!", { 
                    theme: "bubble", 
                    position: "top-center", 
                    duration : 2000
               })
      }
      else{
        let obj = {
        tekst: this.novaRecenzija,
        ocena: this.ocena,
        musterijaID: this.musterijaID
      }
      this.$store.dispatch("postaviRecenzijuMusterija", obj).then(()=>{
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
.stilZaZvezde{
  display:flex;
  justify-content: center;
}
</style>