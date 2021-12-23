<template>
  <div class="">
    <div class="container Pitanje">
      <div class="row">
        <div class="col-xl-12">
          <img class="img-sm img1 displej" src="../assets/musterijasmall.png" alt="">
          <h5 class="displej">{{ pitanje.musterijaIme }} {{ pitanje.musterijaPrezime }}</h5>
          <p class="displej">   Postavljeno: {{ pitanje.datumPitanja | formatirajVreme }}</p>
          <h4>{{ pitanje.tekstPitanja }}</h4>
        </div>
      </div>
      <div class="col-xl-12" v-if="pitanje.odgovoreno">
        <Odgovor :key="pitanje.id" :tekst="pitanje.tekstOdgovora" :datum="pitanje.datumOdgovora" :radnikime="pitanje.radnikIme" :radnikprezime="pitanje.radnikPrezime"/>
      </div>
      <div v-else class="nemaOdg">
        <div v-if="korisnik=='R'">
          <div v-if="!prikaziDodavanje">
            <button type="submit" class="btn btn-primary marstil dugme" @click="togglePrikaziDodavanje"><font-awesome-icon :icon="['fas', 'plus-circle']" />&nbsp;&nbsp;Odgovori na pitanje</button>
          </div>
          <div v-else>
            <a @click="togglePrikaziDodavanje">Odustani <font-awesome-icon :icon="['fas', 'chevron-up']" /></a>
            <form action="/action_page.php" @submit.prevent>
              <div class="form-group">
                <!-- <label class="labele" for="odgovor">Odgovor na pitanje:</label> -->
                <textarea
                  v-model="odgovorNaPitanje"
                  class="form-control rounded-0"
                  id="odgovor"
                  name="odgovor"
                  rows="3"
                ></textarea>
              </div>
              <button type="submit" class="btn btn-primary marstil" @click="dodajOdgovor"> <font-awesome-icon :icon="['fas', 'arrow-circle-up']" />&nbsp;&nbsp;Postavi odgovor </button>
            </form>
          </div>
        </div>
        <div v-else>
          <h6>Trenutno nema odgovora...</h6>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Odgovor from '@/components/Odgovor';
export default {
  name: "Pitanje",
  components:{
      Odgovor
  },
  props: {
    pitanje: {
      required: true,
      type: Object,
    },
  },
  data(){
    return{
      prikaziDodavanje: false,
      odgovorNaPitanje:""
    }
  },
  computed:{
    korisnik(){
      return this.$cookies.get("tip")
    },
    radnikID(){
      return this.$store.getters['getOsobaID']
    }
  },
  methods:{
    togglePrikaziDodavanje(){
      this.prikaziDodavanje = !this.prikaziDodavanje
    },
    dodajOdgovor(){
      let obj = {
        pitanjeID: this.pitanje.id,
        radnikID: this.radnikID,
        tekstOdgovora:this.odgovorNaPitanje
      }
      this.$store.dispatch("odgovoriNaPitanjeRadnik", obj).then(()=>{
        this.prikaziDodavanje = false
      })
    }
  }
};
</script>

<style scoped>
.Pitanje{

  background-color: #c2eee6;
  /* background-color: rgba(242, 175, 88,0.5); */
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 4px solid rgba(80, 182, 163, 0.644);
  margin-top:15px;
  margin-bottom:15px;
}
.displej{
  display: inline;
  justify-content: start;
}
.nemaOdg{
  margin:10px;
  margin-bottom:20px;
}
</style>