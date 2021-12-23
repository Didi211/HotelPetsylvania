<template>
  <div class="col-xl-5 Radnik">
    <img class="img-sm img1 displej" src="../assets/radniksmall.png" alt="">
    <h4><span class="notbold">Ime: </span>{{ radnik.ime }}</h4>
    <h4><span class="notbold">Prezime:</span> {{ radnik.prezime }}</h4>
    <h4><span class="notbold">Username: </span>{{ radnik.username }}</h4>
    <h4><span class="notbold">JMBG:</span> {{ radnik.jmbg }}</h4>
    <h4> <span class="notbold">Datum početka rada:</span> {{ radnik.datumPocetkaRada | formatirajDatume }}</h4>
    <div class="row">
      <div class="col-xl-12">
        <button @click="otpustiRadnika" type="button" class="btn btn-danger"><font-awesome-icon :icon="['fas', 'times']" /> &nbsp;&nbsp; Otpusti radnika</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AdminRadnikUListi",
  props: {
    radnik: {
      required: true,
      type: Object,
    },
  },
  methods:{
    otpustiRadnika(){
      this.$store.dispatch('otpustiRadnikaAdmin', this.radnik.id);
    }
  }
};
</script>

<style scoped>
.Radnik {
  background-color: rgba(255, 255, 255, 0.7);
  /* background-color: rgba(242, 175, 88,0.5); */
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 4px solid rgb(255, 255, 255);

  margin-top: 20px;
  margin-bottom: 20px;
  /* margin-left: 47px; ovo bi trebalo na lepsi nacin da se resi */

  padding-bottom: 15px;
  padding-top: 15px;
}
.notbold{
  font-weight:normal;
}
</style>