<template>
  <div class="Zahtev">
    <div >
      <p>Rezervacioni broj {{ zahtev.zahtevID }}</p>
      <p>Usluga: {{ zahtev.nazivUsluge }}</p>
      <p>Mušterija: {{ zahtev.username }}</p>
      <p>Ime životinje: {{ zahtev.imeLjubimca }}</p>
      <p>Vrsta životinje: {{ tipZ }}</p>
      <p>Termin: {{ zahtev.termin | formatirajTermin }}</p>
    </div>
    <div v-if="uObradi">
      <form action="/action_page.php" @submit.prevent>
        <div class="form-group">
          <label class="labele" for="komentar">Napiši komentar:</label>
          <textarea style="min-width:50px"
                    v-model="komentar.Sadrzaj"
                    class="form-control mb-2"
                    id="komentar"
                    name="komentar"
                    rows="3"
          ></textarea>
        </div>
        <button
          type="submit"
          class="btn btn-primary marstil"
          @click="dodajKomentar"
        >
          <font-awesome-icon :icon="['fas', 'share']" /> &nbsp;&nbsp;Pošalji obaveštenje mušteriji
        </button>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "RadnikZahtev",
  props: {
    zahtev: {
      required: true,
      type: Object,
    }
  },
    data(){
    return{
      komentar:{
        Sadrzaj:"",
        ID:0,
        radnikID:""
      }
    }
  },
  computed:{
    uObradi(){
      return this.zahtev.obradjen ==="U_OBRADI"
    },
    tipZ(){
      if(this.zahtev.tipZivotinje == null){
        return "/"
      }
      else{
        return this.zahtev.tipZivotinje
      }
    },
    getRadnikID(){
      return this.$store.getters['getOsobaID']
    }
  },
  methods:{
    dodajKomentar(){
      this.komentar.radnikID = this.getRadnikID
      let payload={
        rez:this.zahtev.zahtevID,
        kom:this.komentar
      }
      this.$store.dispatch('obavestiMusteriju',payload).then(()=>{
        document.getElementById("komentar").value="";
        document.getElementById("komentar").disabled = true;
      })
    }
  }
};
</script>

<style scoped>
.Zahtev {
  background-color: rgba(255, 255, 255, 0.7);
  /* background-color: rgba(242, 175, 88,0.5); */
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 4px solid rgb(255, 255, 255);
  margin-bottom: 20px;
  padding-bottom: 15px;
  padding-top: 15px;
}
</style>