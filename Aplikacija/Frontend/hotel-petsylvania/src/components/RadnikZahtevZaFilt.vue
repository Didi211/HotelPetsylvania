<template>
  <div class="Zahtev">
    <div >
      <p>Rezervacioni broj {{ zahtev.zahtevID }}</p>
      <p>Usluga: {{ zahtev.nazivUsluge }}</p>
      <p>Mušterija: {{ zahtev.username }}</p>
      <p>Ime životinje: {{ zahtev.imeLjubimca }}</p>
      <p>Vrsta životinje: {{ tipZ }}</p>
      <p>Termin: {{ zahtev.termin | formatirajVreme }}</p>
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
        ID:null
      },
      slika:null
    }
  },
  computed:{
    uObradi(){
      return this.zahtev.obradjen ==="U_OBRADI"
    },
    tipZ(){
      if(this.zahtev.tipZivotinje == null){
        return "ispis"
      }
      else{
        return this.zahtev.tipZivotinje
      }
    }
  },
  methods:{
    onFileSelected(event){
      this.slika = event.target.files[0]
    },
    dodajKomentar(){
      this.$store.dispatch('obavestiMusteriju', this.zahtev.zahtevID, this.komentar)
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