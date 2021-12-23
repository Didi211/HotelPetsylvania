<template>
  <div class="Zahtev">
    <p>Rezervacioni broj {{ zahtev.zahtevID }}</p>
    <p>Mušterija: {{ zahtev.username }}</p>
    <p>Ime životinje: {{ zahtev.imeLjubimca }}</p>
    <p>Vrsta životinje: {{ tipZ }}</p>
    <p>Početak boravka: {{ zahtev.datumPocetka | formatirajDatume }}</p>
    <p>Kraj boravka: {{ zahtev.datumZavrsetka | formatirajDatume }}</p>
  </div>
</template>

<script>
export default {
  name: "ZahtevCuvanje",
  props: {
    zahtev: {
      required: true,
      type: Object,
    }
  },
  computed:{
    tipZ(){
      if(this.zahtev.tipZivotinje == null){
        return "/"
      }
      else{
        return this.zahtev.tipZivotinje
      }
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