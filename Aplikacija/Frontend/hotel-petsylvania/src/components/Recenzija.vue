<template>
  <div class="">
    <div class="container Recenzija">
      <div class="row">
        <div class="col-xl-12">
          <img class="img-sm img1 displej" src="../assets/musterijasmall.png" alt="">
          <h5 class="displej">{{ recenzija.musterijaIme }} {{ recenzija.musterijaPrezime }}</h5>
          <div class="stilZaZvezde mt-2 mb-2">
            <star-rating v-model="recenzija.ocena" 
                         :star-size="30"
                         inactive-color="#f2af58"
                         active-color="#289b86"
                         :show-rating="true"
                         :glow="2"
                         glow-color="#ffffff"
                         read-only/>
          </div>
          <h4 class="mb-2">{{ recenzija.tekst }}</h4>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Recenzija",
  props: {
    recenzija: {
      required: true,
      type: Object,
    }
  }
};
</script>

<style scoped>
.Recenzija{
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
.stilZaZvezde{
  display:flex;
  justify-content: center;
}
</style>