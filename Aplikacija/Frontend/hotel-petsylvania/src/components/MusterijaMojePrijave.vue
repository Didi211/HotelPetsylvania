<template>
  <div class="Zahtev">
    <div class="row">
      <h4>Rezervacija: {{ zahtev.rezervacioniBroj }}</h4>
      <div v-for="i in zahtev.izabraneUsluge" :key="i.id">
        <h4>{{ i.naziv }}</h4>
        <div v-if="i.tipUsluge =='BORAVAK'">
          <p class="lead mb-0">od: {{ i.datumPocetka | formatirajDatume }}  do: {{ i.datumZavrsetka | formatirajDatume }}</p>
        </div>
        <div v-else-if="i.tipUsluge == 'JEDNOKRATNA_USLUGA'">
          <p class="lead mb-0">Datum: {{ i.termin | formatirajDatume }}</p>
        </div>
        <div v-else>
          <p class="lead mb-0">Datum: {{ i.termin | formatirajDatume }}</p>
        </div>
      </div>
      <h4>Cena: {{ zahtev.cena }} RSD</h4>
      <h4>Ime ljubimca: {{ zahtev.imeLjubimca }}</h4>
      <!-- <div v-for="i in zahtev.izabraneUsluge" :key="i.id">
        <div v-if="i.tipUsluge =='BORAVAK'">
          <h4>od: {{ i.datumPocetka | formatirajDatume }}  do: {{ i.datumZavrsetka | formatirajDatume }}</h4>
        </div>
        <div v-else-if="i.tipUsluge == 'JEDNOKRATNA_USLUGA'">
          <h4>Datum: {{ i.termin | formatirajDatume }}</h4>
        </div>
        <div v-else>
          <h4>Datum: {{ i.termin | formatirajDatume }}</h4>
        </div>
      </div>       -->
    </div>
    <!-- <p>Popust: ne</p> -->
    <div class="row">
      <div class="col-xl-4 offset-xl-1">
        <button type="button" class=" pt-2 btn btn-primary" @click.prevent="show"><font-awesome-icon :icon="['fas', 'edit']" />&nbsp;&nbsp;Izmeni rezervaciju</button>
      </div>
      <div class="col-xl-4 offset-xl-2">
        <button type="button" class="pt-2 btn btn-danger" @click="obrisiRezervaciju"><font-awesome-icon :icon="['fas', 'times']" />&nbsp;&nbsp;Obriši rezervaciju</button>
      </div>
    </div>
    <transition name="fade" appear>
      <div class="modal-overlay" id="myModal" v-if="isFormShown">
        <div class="ModalStyle">
          <div class="container fluid">
            <div class="row PrviRow">
              <div >
                <h3>Izaberite usluge: </h3>
                <UslugaZaCekiranje v-for="usluga in usluge" 
                                   :key="usluga.id" 
                                   :usluga="usluga"
                                   @childToParentYes="onChildClickYes"
                                   @childToParentNo="onChildClickNo"/>    
              </div>
          
              <!-- ovde treba kroz for petlju da se izlistajuu
              sve usluge hotela i da budu checkbox-ovi -->
            </div>
          </div>
          <br>
          <div class="row DrugoRow">
            <div class="col-lg-5 col-sm-5 offset-lg-1">
              <label for="ime">Ime ljubimca: </label><br>
              <input type="text" class="form-control" v-model="zahtev.imeLjubimca" id="ime" name="ime" ref="ime" >
              <!-- textbox input -->
            </div>
            <div class="col-lg-5 col-sm-5">
              <label for="tip" style="Labele">Tip ljubimca: </label><br>
              <input type="text" class="form-control" v-model="zahtev.tipLjubimca" id="tip" name="tip" >
              <!-- textbox input -->
            </div>
          </div>
          <!-- <div class="row TreciRow">
                <h3>Datum: </h3>
                <div class="col-lg-6 offset-lg-4">
                  <datepicker id="endDate" :value="date" format="dd-MM-yyyy" :disabled-dates="disabledDates" ></datepicker>
                </div>
          -->
          <!-- NE IDE OVDE -->
          <!-- kalendar za datume -->
          <!-- </div> -->
          <div class="row">
            <div class="col-lg-12 sredina">
              <button class=" pt-2 btn btn-lg btn-primary btn-stil" type="submit" @click.prevent="izmeniRezervaciju" @keydown.tab.exact.prevent="">Izmeni</button>
            </div>
            <div class="col-lg-12 sredina">
              <a @click="toggleIzmena">Odustani od rezervacije <font-awesome-icon :icon="['fas', 'times']" /></a>            
            </div>
          </div>
          <!-- </div>      -->
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
import { defineComponent } from '@vue/composition-api'
import Datepicker from 'vuejs-datepicker'
import UslugaZaCekiranje from '@/components/UslugaZaCekiranje.vue'

export default defineComponent({
   name: "MusterijaMojePrijave",
   props:{
       zahtev: {
           required:true,
           type: Object,
       },
   },
   components:{
     Datepicker,
     UslugaZaCekiranje
   },
   data(){
     return{
      //  date: new Date(), 
        isFormShown: false,
        imeLjub: "",
        tipLjub: "",
        checkedUsluge: [],
      //  nPrijava:{
      //    Mid: "",
      //    usluge: null
      //  },
      //  nUsluga:{
      //    id:"",
      //    tipU: "",
      //    termin:"",
      //    dp:"",
      //    dz:""
      //  },
      //  nUsluge:[],
       
      
       disabledDates:{
         customPredictor: function (date){
           const today = new Date()
           today.setDate(today.getDate()-1) //yesterday
           return date < today
         },
        //  from: this.boravakOgranicenje(this.date)
       }

     }
   },
  
   methods: {
     
     obrisiRezervaciju(){
       if(this.zahtev.obradjen == 'NEOBRADJEN'){
          this.$store.dispatch('obrisiRezervacijuMusterija', this.zahtev.rezervacioniBroj);
       }
       else{
          this.$toasted.show("Nije moguće obrisati rezervaciju, jer je u procesu izvršenja", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 5000
                   })
       }
      
     },
     izmeniRezervaciju(){
        if(this.zahtev.obradjen == 'NEOBRADJEN'){
          let Mid=this.$store.getters['getOsobaID']
          this.imeLjub = document.getElementById('ime').value
          this.tipLjub = document.getElementById('tip').value
          let rezz={
            MusterijaID: Mid,
            ImeLjubimca: this.imeLjub,
            TipZivotinje: this.tipLjub,
            Usluge: this.checkedUsluge
          }
          let payload={
            rezBr: this.zahtev.rezervacioniBroj,
            rez: rezz           
          }
          // for(u in usluge)
          // {
          //    this.usluga.id=z.UslugaID
          //    this.usluga.tipU=z.TipUsluge
          //    this.usluga.termin =z.Termin
          //    this.usluga.dp=z.DatumPocetka
          //    this.usluga.dz=z.DatumZavrsetka
          // }
          this.$store.dispatch('izmeniRezervacijuMusterija', payload);
       }
       else{
          this.$toasted.show("Nije moguce izmeniti rezervaciju, jer je u procesu izvrsenja", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 5000
                   })
       }
        this.isFormShown=!this.isFormShown
        this.checkedUsluge = []
     },
      show(){
            this.isFormShown=!this.isFormShown
        },
        toggleIzmena(){
           this.isFormShown=false; 
        },
          onChildClickYes(value){
          this.checkedUsluge.push(value)
        },
        onChildClickNo(value){
          this.isChecked=false
            let index=this.checkedUsluge.indexOf(value)
            if(index > -1){
              this.checkedUsluge.splice(index,1)
            }
        }
        //  boravakOgranicenje(date){
        //    if(document.getElementById("flexCheckDefault").value == 'Boravak'){
        //      const today = new Date(document.getElementById("endDate").value)
        //      return date>today
        //    }            
        // }
     
   },
    computed:{
      usluge(){
        return this.$store.getters['getUslugeZaRezervaciju']
      }
    },
    async created(){
      await this.$store.dispatch('getUslugeZaRezervaciju');

    },
    // function(){
    //    $("#myModal").draggable({
    //   handle: ".modal-overlay"
    //   });
    // }
      
})
</script>

<style scoped>
.Zahtev{
     background-color: rgba(255, 255, 255, 0.7);
  /* background-color: rgba(242, 175, 88,0.5); */
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 4px solid rgb(255, 255, 255);

  /* margin-top: 20px; */
  margin-bottom: 20px;
  margin-left: 45px; /*ovo bi trebalo na lepsi nacin da se resi*/
  margin-right: 45px;
  padding-bottom: 15px;
  padding-top: 15px;
  /* display: flex; */
  /* justify-content: center; */
}
/* .container{
   width: 100%;
    height: 100%;
    top: 0;
    position: absolute;
    visibility: hidden;
    display: none;
} */
.sredina{
  display: flex;
  justify-content: center;
}
datepicker{
  justify-content: center;
  display: flex;
  margin:auto;
  position: center;
}
.btn-primary{
    background-color: #2b9b86;
    border-color:#2b9b86;
}
.btn-stil{
  margin-top:5%;
    margin-bottom: 5%;
    position: right,
}
.modal-overlay{
  position:absolute;
  z-index: 10;
  top:0%;
  left: 0%;
  right: 0;
  bottom:0%;

}
.ModalStyle{
  /* background-color: rgba(242, 175, 88, 0.5); */
  background-color: #c2eee6;
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 5px solid #39b89e;
  margin-top: 10%;
  margin-bottom: 20px;
  padding-bottom: 15px;
  padding-top: 15px;
  justify-content: center;
  position:relative;
  /* display:grid; */
  width:100%;
  /* top: 50%; */
  margin-left:0;
  /* margin-right:250%; */
   overflow-x: hidden;
   overflow-y: auto;

}

label{
  margin-top: 0;
  margin-bottom: 0.5rem;
  font-family: "Montserrat", -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, "Noto Sans", sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
  font-weight: 700;
  line-height: 1.2;
  font-size: 1.75rem;
  orphans: 3;
  widows: 3;
  page-break-after: avoid;
}
.fade-enter-active, 
.fade-leave-active{
  transition: opacity 0.5s;
}
.fade-enter,
.fade-leave-to{
  opacity: 0;
}

</style>
