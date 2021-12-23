<template>
  <div class="mojProfil">
    <div class="row">
      <div v-if="Musterija">
        <HeaderMusterija />
      </div>
      <div v-else-if="Radnik">
        <HeaderRadnik />
      </div>
      <div v-else>
        <HeaderMojProfil />
      </div>     
    </div>
    <div v-if="isDataLoaded">
      <div class="container">
        <div class="ProfilRow">
          <div v-if="Musterija">
            <img class="img-centered img-lg img1" src="../assets/musterijaLogo2.png" alt="">
          </div>
          <div v-else-if="Radnik">
            <img class="img-centered img-lg img2" src="../assets/radnik.png" alt="">
          </div>
          <div v-else>
            <router-link :to="{name:'Login'}">Niste ulogovani</router-link>
          </div>
        </div>
        <div class="row InfoRow">
          <div v-if="Musterija">
            <label for="username">Username: </label>
            <h5>{{ MusterijaObj.username }} </h5>
            <div v-if="isIzmeniDugme">
              <label for="ime">Ime: </label>
              <input type="text" class="form-control" id="ime" name="ime" v-model="MusterijaObj.ime">
              <label for="prezime">Prezime: </label>
              <input type="text" class="form-control" id="prezime" name="prezime" v-model="MusterijaObj.prezime">
              <div class="row ">
                <div class="col-xl-12 pt-4">
                  <button class="btn btn-primary" name="sacuvaj" @click="sacuvajInfoMusterija" ><font-awesome-icon :icon="['fas', 'save']" />&nbsp;&nbsp;Sačuvaj</button>
                </div>
                <div class="col-xl-12 pt-4">
                  <a @click="izmeniPodatke">Odustani od izmene <font-awesome-icon :icon="['fas', 'times']" /></a>            
                </div>
              </div>
            </div>
            <div v-else>
              <label for="ime">Ime: </label>
              <h5>{{ MusterijaObj.ime }} </h5>
              <label for="prezime">Prezime: </label>
              <h5>{{ MusterijaObj.prezime }} </h5>
            </div>
          </div> 
          <div v-if="Radnik">
            <label for="username">Username: </label>
            <h5>{{ RadnikObj.username }}</h5>
            <label for="jmbg">Jmbg: </label><br>
            <h5>{{ RadnikObj.jmbg }}</h5>
            <div v-if="isIzmeniDugme">
              <label for="ime">Ime: </label>
              <input type="text" class="form-control" id="ime" name="ime" v-model="RadnikObj.ime" >
              <label for="prezime">Prezime: </label>
              <input type="text" class="form-control" id="prezime" name="prezime" v-model="RadnikObj.prezime">
              <div class="row">
                <div class="col-xl-12 pt-4">
                  <button class="btn btn-primary" name="sacuvaj" @click="sacuvajInfoRadnik" ><font-awesome-icon :icon="['fas', 'save']" />&nbsp;&nbsp;Sačuvaj</button>
                </div>
                <div class="col-xl-12 pt-4">
                  <a @click="izmeniPodatke">Odustani od izmene <font-awesome-icon :icon="['fas', 'times']" /></a>            
                </div>
              </div>
            </div>
            <div v-else>
              <label for="ime">Ime: </label>
              <h5>{{ RadnikObj.ime }}</h5>
              <label for="prezime">Prezime: </label>
              <h5>{{ RadnikObj.prezime }}</h5>
            </div>
          </div>        
          <div v-if="Musterija">
            <div class="pt-4" v-if="!isIzmeniDugme">
              <button class="btn btn-primary" name="musterija" @click="izmeniPodatke"><font-awesome-icon :icon="['fas', 'edit']" />&nbsp;&nbsp;Izmeni podatke</button>
            </div>
            <div v-if="!isIzmeniSifru" class="pt-4">
              <button class="btn btn-primary" name="musterija" @click="toggleIsIzmeniSifru"><font-awesome-icon :icon="['fas', 'cog']" />&nbsp;&nbsp;Promeni šifru</button>
            </div>
            <div v-else>
              <label for="password">Stara šifra: </label>
              <input v-model="sifraObj.staraSifra" type="password" class="form-control" name="password" placeholder="Stara šifra" required="" /> 
              <label for="password">Nova šifra: </label>
              <input v-model="sifraObj.novaSifra" type="password" class="form-control" name="password" placeholder="Nova šifra" required="" />
              <div class="row">
                <div class="col-xl-12 pt-4">
                  <button class="btn btn-primary" name="sacuvaj" @click="sacuvajSifruMusterije" ><font-awesome-icon :icon="['fas', 'save']" />&nbsp;&nbsp;Sačuvaj šifru</button>
                </div>
                <div class="col-xl-12 pt-4">
                  <a @click="toggleIsIzmeniSifru">Odustani od izmene šifre <font-awesome-icon :icon="['fas', 'times']" /></a>            
                </div>
              </div>
            </div>
          </div>
          <div v-else-if="Radnik">
            <div class="pt-4" v-if="!isIzmeniDugme">
              <button class="btn btn-primary" name="radnik" @click="izmeniPodatke"><font-awesome-icon :icon="['fas', 'edit']" />&nbsp;&nbsp;Izmeni podatke</button>
            </div>
            <div v-if="!isIzmeniSifru" class="pt-4">
              <button class="btn btn-primary" name="radnik" @click="toggleIsIzmeniSifru"><font-awesome-icon :icon="['fas', 'cog']" />&nbsp;&nbsp;Promeni šifru</button>
            </div>   
            <div class="pt-4" v-else-if="isIzmeniSifru">
              <label for="password">Stara šifra: </label>
              <input v-model="sifraObj.staraSifra" type="password" class="form-control" name="password" placeholder="Stara šifra" required="" /> 
              <label for="password">Nova šifra: </label>
              <input v-model="sifraObj.novaSifra" type="password" class="form-control" name="password" placeholder="Nova šifra" required="" />
              <div class="row">
                <div class="col-xl-12 pt-4">
                  <button class="btn btn-primary" name="sacuvaj" @click="sacuvajSifruRadnika" ><font-awesome-icon :icon="['fas', 'save']" />&nbsp;&nbsp;Sačuvaj šifru</button>
                </div>
                <div class="col-xl-12 pt-4">
                  <a @click="toggleIsIzmeniSifru">Odustani od izmene šifre <font-awesome-icon :icon="['fas', 'times']" /></a>            
                </div>
              </div>
            </div>  
          </div>
        </div>
      </div>
    </div>
    <div v-else>
      <AppSpinner />
    </div>
    <div class="row">
      <Footer />
    </div>
  </div>
</template>

<script>
import { defineComponent } from "@vue/composition-api";
import HeaderMojProfil from '@/components/HeaderMojProfil.vue'
import HeaderMusterija from '@/components/HeaderMusterija.vue'
import HeaderRadnik from '@/components/HeaderRadnik.vue'
import Footer from '@/components/Footer.vue'
import Vue from 'vue'

export default defineComponent({
  
  setup() {},
  components:{
    HeaderMojProfil,
    Footer,
    HeaderRadnik,
    HeaderMusterija,
  },
  data(){    
       return{
         sifraObj:{
          novaSifra:"",
          staraSifra:""
         },
         isDataLoaded:false,
         isIzmeniDugme: false, 
         isIzmeniSifru:false,
         Minfo:{
           Ime:"",
           Prezime:"",
           Username:"",
           Sifra:"",
           Slika:"",
         },
         Rinfo:{
           ime:"",
           prezime:"",
           username:"",
           jmbg:"",
           slika:null
         }
       } 
  },
  methods:{
    toggleIsIzmeniSifru(){
      this.isIzmeniSifru=!this.isIzmeniSifru
    },
    //  toggleIsIzmeniDugme(){
    //   this.isIzmeniDugme=!this.isIzmeniDugme
    // },
    izmeniPodatke(){
      // otkljuca sve input tagove da ne budu readonly
      // toggle metoda ide ovde
      this.isIzmeniDugme=!this.isIzmeniDugme
     
    },
    sacuvajSifruRadnika(){
      let payload={
        idRadnika:this.osobaID,
        sifraObj:this.sifraObj
        }
        this.isIzmeniSifru=!this.isIzmeniSifru
      this.$store.dispatch("promeniSifruRadnika", payload)
    },
    sacuvajSifruMusterije(){
     
      let payload={
        idMusterije:this.osobaID,
        sifraObj:this.sifraObj
        }
        this.isIzmeniSifru=!this.isIzmeniSifru
      this.$store.dispatch("promeniSifruMusterije", payload)
    },
    sacuvajInfoMusterija(){
      //toggle metoda ide ovde
      //zakljucaj sve input tagove na readonly
      //sacuvaj podatke
     
      this.isIzmeniDugme=!this.isIzmeniDugme
      this.Minfo.Ime=document.getElementById("ime").value;
      this.Minfo.Prezime=document.getElementById("prezime").value;
      this.Minfo.Username=this.MusterijaObj.username;
      // if(this.MusterijaObj.ime == this.Minfo.ime && this.MusterijaObj.prezime == this.Minfo.prezime){
      //       this.$toasted.show("Niste promenili nijedan podatak!", {
      //         theme: "bubble",
      //         position: "top-center",
      //         duration: 2000,
      //       });
      // }
      // else{
        this.$store.dispatch('AzurirajProfilMusterije', this.Minfo)
      // }
    },
    sacuvajInfoRadnik(){
      //toggle metoda ide ovde
      //zakljucaj sve input tagove na readonly
      //sacuvaj podatke
      
      this.isIzmeniDugme=!this.isIzmeniDugme
      this.Rinfo.ime=document.getElementById("ime").value;
      this.Rinfo.prezime=document.getElementById("prezime").value;
      this.Rinfo.username=this.RadnikObj.username;
      this.Rinfo.jmbg=this.RadnikObj.jmbg;
     
      this.$store.dispatch('AzurirajProfilRadnika', this.Rinfo)
    }
  },

  computed: {
    Musterija() {
      return Vue.$cookies.get("tip")== 'M'
    },
    Radnik(){
      return Vue.$cookies.get("tip")== 'R'
    },
    MusterijaObj(){
      return this.$store.getters['getMusterijaProfil']
    },
    RadnikObj(){
      return this.$store.getters['getRadnikProfil']
    },
    osobaID(){
      return this.$store.getters['getOsobaID']
    }
  },

  async created(){
    // Promise.all([await this.$store.dispatch("postaviOsobaID", this.$cookies.get("id")), await this.$store.dispatch('getMusterijaProfil')]).then(()=>{
    //   this.isDataLoaded = true
    // })
   
    await this.$store.dispatch("postaviOsobaID", this.$cookies.get("id"))
   
    if(Vue.$cookies.get("tip")== 'M'){
      await this.$store.dispatch('getMusterijaProfil').then(()=>{
      this.isDataLoaded = true
    })
    }
    else if(Vue.$cookies.get("tip")== 'R'){
      await this.$store.dispatch('getRadnikProfil').then(()=>{
      this.isDataLoaded = true
    })
    }
   
    // await this.$store.dispatch('getRadnikProfil')
  }
});
</script>

<style scoped>
/* .btn{
  margin-top: 5%;
} */
.ProfilRow{
  padding-top: 12%;
  /* padding-bottom: 10%; */
}
.InfoRow{
  padding-bottom: 10%;
}
.img1 {
  display: block;
  margin-left: auto;
  margin-right: auto;
  width: 30%;
}
.img2{
  padding-top: 1%;
  display: block;
  margin-left: auto;
  margin-right: auto;
  width: 25%;
}
input{
  margin-left: auto;
  margin-right: auto;
  width: 30%;
}
label{
  margin-top: 0;
  margin-bottom: 0.5rem;
  font-family: "Montserrat", -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, "Noto Sans", sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
  font-weight: 700;
  line-height: 1.2;
  font-size: 1.5rem;
  orphans: 3;
  widows: 3;
  page-break-after: avoid;
}
label{
  font-weight: lighter;
}
h5{
  font-size:1.7rem
}
</style>