<template>
  <div class="col-xl-4 Usluga">
    <!-- <div v-if="izmena">
    </div> -->
    <h4>{{ usluga.naziv }}</h4>
    <div v-if="izmena">
      <h4><s>{{ usluga.cenaUsluge }} dinara</s></h4>
    </div>
    <div v-else>
      <h5>Cena: {{ usluga.cenaUsluge }} dinara</h5>
      <h5>Kapacitet: {{ usluga.kapacitet }} </h5>
    </div>
    <div v-if="izmena==true">
      <form action="/action_page.php" @submit.prevent>
        <div class="form-group">
          <label class="labele" for="novacena">Unesite novu cenu usluge:</label>
          <input
            :value="usluga.cenaUsluge"
            type="number"
            class="form-control"
            id="novacena"
            name="novacena"
            required
          />
        </div>
        <div class="row">
          <a @click="toggleIzmena">Odustani od izmene <font-awesome-icon :icon="['fas', 'chevron-up']" /></a>
          <div class="col-xl-12 dugme">
            <button type="submit" class="btn btn-primary marstil dugme" @click="izmeniCenu"><font-awesome-icon :icon="['fas', 'arrow-circle-up']" /> &nbsp;&nbsp;Primeni izmenu</button>
          </div>
        </div>
      </form>
    </div>
    <div v-if="!izmena" class="row">
      <div class="col-xl-12  malo-manje">
        <button @click="toggleIzmena" type="button" class="btn btn-primary dugme">  <font-awesome-icon :icon="['fas', 'edit']" /> &nbsp;&nbsp;Izmeni uslugu</button>
      </div>
    </div>
    <div class="row">
      <div class="col-xl-7  malo-manje">
        <button type="submit" class="btn btn-primary marstil dugme" @click="deaktivirajUslugu"> <font-awesome-icon :icon="['fas', 'lock']" />  &nbsp;&nbsp;Deaktiviraj uslugu</button>
      </div>
      <div class="col-xl-5  malo-manje">
        <button type="button" class="btn btn-danger dugme" @click="izbrisiUslugu"> <font-awesome-icon :icon="['fas', 'times']" /> &nbsp;&nbsp; Izbriši uslugu</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AdminUsluga",
  props: {
    usluga: {
      required: true,
      type: Object,
    }
  },
  data(){
    return{
      novaCena:null,
      izmena: false
    }
  },
  methods:{
    izbrisiUslugu(){
          this.$store.dispatch('izbrisiUsluguAdmin', this.usluga.id);
    },
    toggleIzmena(){
      this.izmena = !this.izmena;
    },
    izmeniCenu(){
      this.novaCena = parseInt(document.getElementById("novacena").value);
      if(this.novaCena<=0){
        this.$toasted.show("Cena ne sme biti manja od 0!", {
          theme: "bubble",
          position: "top-center",
          duration: 2000,
        });
      }
      else if(this.novaCena != this.usluga.cenaUsluge){
        let payload = {
          id:this.usluga.id,
          nCena: this.novaCena
        }
        this.$store.dispatch('izmeniCenuUslugeAdmin', payload).then(()=>{
          this.izmena = false
        })
      }
      else{
        this.$toasted.show("Niste promenili vrednost kapaciteta!", {
          theme: "bubble",
          position: "top-center",
          duration: 2000,
        });
      }

    },
    deaktivirajUslugu(){
      this.$store.dispatch('deaktivirajUsluguAdmin', this.usluga.id);
    }
  }
};
</script>

<style scoped>
.Usluga {
  background-color: rgba(255, 255, 255, 0.7);
  /* background-color: rgba(242, 175, 88,0.5); */
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 4px solid rgb(255, 255, 255);

  margin-top: 20px;
  margin-bottom: 20px;
  margin-left: 47px; /*ovo bi trebalo na lepsi nacin da se resi*/

  padding-bottom: 15px;
  padding-top: 15px;
}
.dugme{
  margin-top:5px;
  margin-bottom:5px;
}
.btn:focus {
  outline: 0;
  box-shadow: 0;
}
.malo-manje{
  padding:0;
}
</style>