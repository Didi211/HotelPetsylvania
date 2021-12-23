<template>
  <div class="Odgovor">
    <div class="col-xl">
      <img class="img-sm img1 displej" src="../assets/radniksmall.png" alt="">
      <h5 class="displej">{{ radnikime }} {{ radnikprezime }} </h5>
      <p class="displej">  Postavljeno: {{ datum | formatirajVreme }}</p>
      <h4>{{ tekst }}</h4>
    </div>
  </div>
</template>

<script>
import Odgovor from '@/components/Odgovor';
export default {
  name: "Odgovor",
  props: {
    tekst: {
      required: true,
      type: String,
    },
    datum:{
      required: true,
      type: String,
    },
    radnikime:{
      required: true,
      type: String,
    },
    radnikprezime:{
      required: true,
      type: String,
    }
  }
};
</script>


<style scoped>
.Odgovor {
  background-color: #ebc38fbb;
  /* background-color: rgba(242, 175, 88,0.5); */
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 4px solid #f2af58da;
  margin-top: 15px;
  margin-bottom:15px;
}
.displej{
  display: inline;
  justify-content: start;
}
</style>