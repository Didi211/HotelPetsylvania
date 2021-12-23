<template>
  <div class="container-fluid">
    <div>
      <HeaderRadnik />
    </div>
    <div v-if="isDataLoaded">
      <div class="SrednjiRow">
        <div class="container">
          <div class="row">
            <h3>Svi zahtevi</h3>
          </div>
          <div class="row">
            <div class="col-xl-12 KolDodavanje">
              <h4 class="pt-2">Izaberite koji filter želite da koristite:</h4>
              <div class="row pb-3 ">
                <div class="col-xl-4">
                  <button type="submit" class="lepodugme btn btn-primary marstil" @click="togglePoTipuUsluge">Tip usluge</button>
                </div>
                <div class="col-xl-4">
                  <button type="submit" class="lepodugme btn btn-primary marstil" @click="togglePoTipuZivotinje">Tip životinje</button>
                </div>
                <div class="col-xl-4">
                  <button type="submit" class="lepodugme btn btn-primary marstil" @click="togglePoRezBroju">Rezervacioni broj</button>
                </div>
              </div>
              <div v-if="poTipuZivotinje">
                <div class="row pb-3">
                  <div class="col-xl-10" style="padding-right:0px">
                    <select
                      v-model="ftipZivotinje"
                      class="form-control"
                      id="ZivotinjaTip"
                      name="tipzivotinje"
                      required
                    >
                      <option value="" disabled>
                        Izaberite tip životinje
                      </option>
                      <option v-for="s in sviTipoviZivotinja" :key="s.id" :value="s">{{ s }}</option>
                    </select>
                  </div>
                  <div class="col-xl-2" style="padding-left:0px">
                    <div v-if="!pretraga">
                      <button type="submit" class="btn btn-primary marstil" @click="pretraziPoTipuZivotinje">
                        <font-awesome-icon :icon="['fas', 'search']" />&nbsp;&nbsp;<span>Pretraži</span>
                      </button>
                    </div>
                    <div v-else>
                      <button type="submit" class="btn btn-primary marstil" @click="osveziPoTipuZivotinje">
                        <font-awesome-icon :icon="['fas', 'redo']" />&nbsp;&nbsp;<span>Osveži</span>
                      </button>
                    </div>
                  </div>
                </div>
              </div>
              <div v-if="poTipuUsluge"> 
                <div class="row pb-3">
                  <div class="col-xl-10" style="padding-right:0px">
                    <select
                      v-model="ftipUsluge"
                      class="form-control rounded"
                      id="UslugaTip"
                      name="tipusluge"
                      required
                    >
                      <option value="" disabled>
                        Izaberite tip usluge
                      </option>
                      <option v-for="s in sviTipoviUsluga" :key="s.id" :value="s">{{ s }}</option>
                    </select>
                  </div>
                  <div class="col-xl-2" style="padding-left:0px">
                    <div v-if="!pretraga">
                      <button type="submit" class="btn btn-primary marstil" @click="pretraziPoTipuUsluge">
                        <font-awesome-icon :icon="['fas', 'search']" />&nbsp;&nbsp;<span>Pretraži</span>
                      </button>
                    </div>
                    <div v-else>
                      <button type="submit" class="btn btn-primary marstil" @click="osveziPoTipuUsluge">
                        <font-awesome-icon :icon="['fas', 'redo']" />&nbsp;&nbsp;<span>Osveži</span>
                      </button>
                    </div>
                  </div>
                </div>
              </div>
              <div v-if="poRezBroju" >
                <div class="row pb-3">
                  <div class="col-xl-10" style="padding-right:0px">
                    <input type="search" v-model="search" id="src" class="form-control rounded" placeholder="Rezervacioni broj..." aria-label="Search"
                           aria-describedby="search-addon" @keyup.enter="pretrazi"/>
                  </div>
                  <div class="col-xl-2" style="padding-left:0px">
                    <div v-if="!pretraga">
                      <button type="submit" class="btn btn-primary marstil" @click="pretrazi">
                        <font-awesome-icon :icon="['fas', 'search']" />&nbsp;&nbsp;<span>Pretraži</span>
                      </button>
                    </div>
                    <div v-else>
                      <button type="submit" class="btn btn-primary marstil" @click="osvezi">
                        <font-awesome-icon :icon="['fas', 'redo']" />&nbsp;&nbsp;<span>Osveži</span>
                      </button>
                    </div>
                  </div>
                </div>
              </div>
              <!-- <input type="text" v-model="search" placeholder="nadjite rezervacioni broj"/> -->
              <div v-if="pretraga">
                <div v-if="poTipuZivotinje">
                  <div v-if="filtriraniZahteviPoTipuZivotinje.length>0">
                    <div v-for="zahtev in filtriraniZahteviPoTipuZivotinje" :key="zahtev.id" :zahtev="zahtev">
                      <div v-if="zahtev.tipUsluge=='BORAVAK'">
                        <RadnikZahtevCuvanje :zahtev="zahtev"/>
                      </div>
                      <div v-if="zahtev.tipUsluge=='JEDNOKRATNA_USLUGA'">
                        <RadnikZahtevZaFilt :zahtev="zahtev"/>
                      </div>
                    </div>
                  </div>
                  <div v-else>
                    <h4>Nema zahteva!</h4>
                  </div>
                </div>
                <div v-if="poTipuUsluge">
                  <div v-if="filtriraniZahteviPoTipuUsluge.length>0">
                    <div v-for="zahtev in filtriraniZahteviPoTipuUsluge" :key="zahtev.id" :zahtev="zahtev">
                      <div v-if="zahtev.tipUsluge=='BORAVAK'">
                        <RadnikZahtevCuvanje :zahtev="zahtev"/>
                      </div>
                      <div v-if="zahtev.tipUsluge=='JEDNOKRATNA_USLUGA'">
                        <RadnikZahtevZaFilt :zahtev="zahtev"/>
                      </div>
                    </div>
                  </div>
                  <div v-else>
                    <h4>Nema zahteva!</h4>
                  </div>
                </div>
                <div v-if="poRezBroju">
                  <div v-if="filtriraniZahteviPoRezBroju.length>0">
                    <div v-for="zahtev in filtriraniZahteviPoRezBroju" :key="zahtev.id" :zahtev="zahtev">
                      <div v-if="zahtev.tipUsluge=='BORAVAK'">
                        <RadnikZahtevCuvanje :zahtev="zahtev"/>
                      </div>
                      <div v-if="zahtev.tipUsluge=='JEDNOKRATNA_USLUGA'">
                        <RadnikZahtevZaFilt :zahtev="zahtev"/>
                      </div>
                    </div>
                  </div>
                  <div v-else>
                    <h4>Nema zahteva!</h4>
                  </div>
                </div>
              </div>
              <div v-else>
                <div v-for="zahtev in sviZahtevi" :key="zahtev.id" :zahtev="zahtev">
                  <div v-if="zahtev.tipUsluge=='BORAVAK'">
                    <RadnikZahtevCuvanje :zahtev="zahtev"/>
                  </div>
                  <div v-if="zahtev.tipUsluge=='JEDNOKRATNA_USLUGA'">
                    <RadnikZahtevZaFilt :zahtev="zahtev"/>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div>
        <Footer />
      </div>
    </div>
    <div v-else>
      <AppSpinner />
    </div>
  </div>
</template>


<script>
import HeaderRadnik from "@/components/HeaderRadnik.vue";
import Footer from "@/components/Footer.vue";
import RadnikZahtevCuvanje from "@/components/RadnikZahtevCuvanje.vue"
import RadnikZahtevZaFilt from "@/components/RadnikZahtevZaFilt.vue"

export default {
  name: "RadnikFiltriranjeZahteva",
  components: {
    HeaderRadnik,
    Footer,
    RadnikZahtevCuvanje,
    RadnikZahtevZaFilt
  },
  props: {},
  data(){
    return {
      isDataLoaded:false,
      search:"",
      ftipUsluge: "",
      ftipZivotinje:"",
      pretraga:false,
      poRezBroju:false,
      poTipuZivotinje:false,
      poTipuUsluge:false,
    }
  },
  computed:{
    sviZahtevi() {
      return this.$store.getters['getSviZahteviFiltriranje']
    },
    sviTipoviUsluga() {
      return this.$store.getters['getSviTipoviUsluga']
    },
    sviTipoviZivotinja(){
      return this.$store.getters['getSviTipoviZivotinja']
    },
    filtriraniZahteviPoRezBroju(){
      return this.sviZahtevi.filter((zahtev)=>{
        return zahtev.zahtevID == this.search;
      })
    },
    filtriraniZahteviPoTipuUsluge(){
      return this.sviZahtevi.filter((zahtev)=>{
        return zahtev.nazivUsluge == this.ftipUsluge;
      })
    },
    filtriraniZahteviPoTipuZivotinje(){
      return this.sviZahtevi.filter((zahtev)=>{
        return zahtev.tipZivotinje == this.ftipZivotinje;
      })
    },
    radnikID(){
      return this.$store.getters['getosobaid']
    }
  },
  async created(){
    await this.$store.dispatch("postaviOsobaID", this.$cookies.get("id"))

    await this.$store.dispatch('getZahteviCuvanjeRadnik',this.radnikID ).then(()=>{
      this.isDataLoaded = true
    }
    )
  },
  methods:{
    osvezi(){
      this.pretraga = false
      this.search = ""
    },
    osveziPoTipuUsluge(){
      this.pretraga = false
      this.ftipUsluge = ""
    },
    osveziPoTipuZivotinje(){
      this.pretraga = false
      this.ftipZivotinje = ""
    },
    pretrazi(){
      if(this.search == ""){
        this.$toasted.show("Morate prvo da upišete traženi rezervacioni broj!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else{
        this.pretraga = true
        this.poTipuUsluge = false
        this.poTipuZivotinje = false
        this.poRezBroju = true
      }
    },
    pretraziPoTipuUsluge(){
      if(this.ftipUsluge == ""){
        this.$toasted.show("Morate prvo da izaberete traženi tip usluge!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else{
        this.pretraga = true
        this.poTipuUsluge = true
        this.poTipuZivotinje = false
        this.poRezBroju = false
      }
    },
    pretraziPoTipuZivotinje(){
      if(this.ftipZivotinje == ""){
        this.$toasted.show("Morate prvo da izaberete traženi tip životinje!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else{
        this.pretraga = true
        this.poTipuZivotinje = true
        this.poTipuUsluge = false
        this.poRezBroju = false
      }
    },
    togglePoTipuUsluge(){
      this.poTipuUsluge = !this.poTipuUsluge
      this.poRezBroju = false
      this.poTipuZivotinje = false
      this.search="",
      this.ftipUsluge= "",
      this.ftipZivotinje="",
      this.pretraga=false,
      this.poRezBroju=false,
      this.poTipuZivotinje=false
    },
    togglePoTipuZivotinje(){
      this.poTipuZivotinje = !this.poTipuZivotinje
      this.poRezBroju = false
      this.poTipuUsluge = false
      this.search="",
      this.ftipUsluge= "",
      this.ftipZivotinje="",
      this.pretraga=false,
      this.poRezBroju=false,
      this.poTipuUsluge=false
    },
    togglePoRezBroju(){
      this.poRezBroju = !this.poRezBroju
      this.poTipuZivotinje = false
      this.poTipuUsluge = false
      this.search="",
      this.ftipUsluge= "",
      this.ftipZivotinje="",
      this.pretraga=false,
      this.poTipuZivotinje=false,
      this.poTipuUsluge=false
    },
  }
};
</script>

<style scoped>
.SrednjiRow {
  padding-top: 6rem;
}

.KolDodavanje {
  background-color: rgba(242, 175, 88, 0.5);
  /* background-color: rgba(242, 175, 88,0.5); */
  border-radius: 25px;
  border: 4px solid rgb(242, 175, 88);
  margin-top: 20px;
  margin-bottom: 20px;
}
.lepodugme {
  display:inline-block;
  padding:0.5em 3em;
  border:0.16em solid #FFFFFF;
  background-color: rgba(26, 188, 156, 0.8);;
  margin: 0 0.3em 0.3em 0;
  border:0.16em solid #FFFFFF;
  box-sizing: border-box;
  text-decoration:none;
  text-transform:uppercase;
  font-family:'Roboto',sans-serif;
  font-weight:400;
  color:#FFFFFF;
  text-align:center;
  transition: all 0.15s;
}
.lepodugme:hover{
  color:#DDDDDD;
  border-color:#DDDDDD;
}
.lepodugme:active{
  color:#BBBBBB;
  border-color:#BBBBBB;
}
.peding{
  padding-left:0;
  padding-right:0;
}
</style>