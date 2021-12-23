<template>
  <div class="form-group">
    <!-- <label class="labele" for="cuvanje">Kapacitet za {{ usluga.naziv }}:</label> -->
    <!-- <input
      type="number"
      class="form-control"
      :id="usluga.naziv"
      :value="usluga.kapacitet"
      :name="usluga.naziv"
      required
    />
    <button type="submit" class="btn btn-primary marstil">
      Izmeni kapacitet za {{ usluga.naziv }}
    </button> -->

    <div class="container">
      <form action="/action_page.php" @submit.prevent>
        <div class="row">
          <div class="col-xl-6">
            <label class="labele" for="cuvanje">Kapacitet za {{ usluga.naziv }}:</label
            >
            <input
              type="number"
              class="form-control"
              :id="usluga.naziv"
              :value="usluga.kapacitet"
              :name="usluga.naziv"
              required
            />
          </div>
          <div class="col-xl-6">
            <br />
            <button
              @click="izmeniKapacitet"
              type="submit"
              class="btn btn-primary marstil"
            >
              <font-awesome-icon :icon="['fas', 'edit']" />&nbsp;&nbsp; Izmeni kapacitet za {{ usluga.naziv }}
            </button>
          </div>
        </div>
      </form>
    </div>

    <!-- <div class="valid-feedback">Valid.</div> -->
    <!-- <div class="invalid-feedback">Popunite ovo polje.</div> -->
  </div>
</template>
<script>
export default {
  name: "AdminKapacitetUsluge",
  props: {
    usluga: {
      required: true,
      type: Object,
    },
  },
  data() {
    return {
      noviKap: null,
    };
  },
  methods: {
    izmeniKapacitet() {
      this.noviKap = parseInt(document.getElementById(this.usluga.naziv).value);
      if(this.noviKap<=0){
        this.$toasted.show("Kapacitet ne sme biti manji od 0!", {
          theme: "bubble",
          position: "top-center",
          duration: 2000,
        });
      }
      else if (this.usluga.kapacitet != this.noviKap) {
        let payload ={
          id: this.usluga.id,
          novaVr: this.noviKap
        }
        this.$store.dispatch("izmeniKapacitetAdmin",payload);
      } else {
        this.$toasted.show("Niste promenili vrednost kapaciteta!", {
          theme: "bubble",
          position: "top-center",
          duration: 2000,
        });
      }
    },
  },
};
</script>

<style scoped>
.labele {
  font-weight: bold;
}
</style>