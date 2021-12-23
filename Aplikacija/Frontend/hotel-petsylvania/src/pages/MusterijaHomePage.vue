<template>
  <div class="cenovnik">
    <div class="row">
      <HeaderMusterija />
    </div>
    <div v-if="!isDataLoaded">
      <AppSpinner />
    </div>
    <div v-else>
      <div class="wrapper MusterijaRow">
        <!-- <h1>MusterijaHomePage</h1> -->
        <div class="container">
          <div class=" row PrviRow ml-1 mr-1">
            <div class="NovaUsluga form-group col-lg-6 offset-lg-3">
              <h3>Rezervišite novu uslugu</h3>
              <MusterijaModalNoviZahtev />
            <!-- nova usluga -->
            </div>          
          </div>
          <div class=" row DrugiRow no-gutters">
            <div class="col-lg-5 NovoObavestenje">
              <!-- <p>Obavestenja o hotelu</p> -->
              <h4>Obaveštenja o hotelu</h4>
              <MusterijaObavestenja v-for="obavestenje in obavestenja" 
                                    :key="obavestenje.id" 
                                    :obavestenje="obavestenje" />
            </div>
            <div class="col-lg-6 NovaUsluga">
              <h4>Lista rezervacija</h4>
              <MusterijaMojePrijave v-for="zahtev in zahtevi" 
                                    :key="zahtev.id" 
                                    :zahtev="zahtev" />
            <!-- Moji zahtevi --> 
            </div>
            <!-- <Notifikacije /> -->
          </div>
        </div>
      </div>
      <div class="row">
        <div class="footer">
          <Footer />
        </div>
      </div>
    </div>
  </div>
</template>



<script>
import { defineComponent } from '@vue/composition-api'
import HeaderMusterija from '@/components/HeaderMusterija.vue'
import Footer from '@/components/Footer.vue'
import MusterijaObavestenja from '@/components/MusterijaObavestenja.vue'
import MusterijaMojePrijave from '@/components/MusterijaMojePrijave.vue'
import MusterijaModalNoviZahtev from '@/components/MusterijaModalNoviZahtev.vue'
import {mapGetters} from 'vuex';

export default defineComponent({
    name: "MusterijaHomePage",
    components: {
      HeaderMusterija,
      Footer,
      MusterijaObavestenja,
      MusterijaMojePrijave,
      MusterijaModalNoviZahtev,
   
    },
     data () {
      return {
        isDataLoaded:false,
      }
    },
    computed: {
      obavestenja(){
        return this.$store.getters['getObavestenjaMusterija']
      },
      zahtevi(){
        return this.$store.getters['getZahteveMusterije']
      },
     
    },
    async created(){
      Promise.all([await this.$store.dispatch("postaviOsobaID", this.$cookies.get("id")),
       await this.$store.dispatch('getObavestenjaMusterija'), await this.$store.dispatch('getZahteveMusterija'),
       await this.$store.dispatch("getNotifikacijeMusterije")]).then(() => {
          this.isDataLoaded = true
      });
    
    }
})
</script>

<style scoped>
.footer{
    position:relative;
}
.MusterijaRow{
    padding-top:6rem;
    /* padding-bottom: 15%; */
}
.DrugiRow{
  padding: 0;
  padding-top: 1rem;
  justify-content: space-between;
}
.PrviRow{
  display: flex;
}
.btn-primary{
    background-color: #267a6b;
    border-color:#267a6b;

}
h4{
  color: #174b42;
}
h3{
  color:#174b42;
  font-weight: bold;
}
.NovaUsluga{
  background-color: rgba(242, 175, 88, 0.5);
  border-radius: 25px;
  border: 4px solid rgb(242, 175, 88);
  margin-top: 20px;
  margin-bottom: 20px;
  padding-bottom: 15px;
  padding-top: 15px;
  justify-content: center;

}

.NovoObavestenje{
  background-color: rgba(242, 175, 88, 0.5);
  border-radius: 25px;
  border: 4px solid rgb(242, 175, 88);
  margin-top: 20px;
  margin-bottom: 20px;
  padding-bottom: 15px;
  padding-top: 15px;
  justify-content: center;
}

</style>