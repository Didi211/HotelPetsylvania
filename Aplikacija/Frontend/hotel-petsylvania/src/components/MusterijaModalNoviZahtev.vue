<template>
  <div> 
    <button type="submit" class="btn btn-lg btn-primary" @click.prevent="showForm">Nova rezervacija</button>
    <transition name="fade" appear>
      <div class="modal-overlay" v-if="isFormShown">
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
            </div>
          
            <!-- ovde treba kroz for petlju da se izlistajuu
              sve usluge hotela i da budu checkbox-ovi -->
          </div>
          <br>
          <div class="row DrugoRow">
            <div class="col-lg-5 offset-lg-1">
              <label for="ime">Ime ljubimca: </label><br>
              <input type="text" class="form-control" v-model="imeLjubimca" id="ime" name="ime" ref="ime">
              <!-- textbox input -->
            </div>
            <div class="col-lg-5">
              <label for="tip" style="Labele">Tip ljubimca: </label><br>
              <input type="text" class="form-control" v-model="tipLjubimca" id="tip" name="tip">
              <!-- textbox input -->
            </div>
          </div>
          <div class="row">
            <div class="col-lg-12 sredina">
              <button class="btn btn-lg btn-primary btn-stil" type="submit" @click.prevent="rezervisi" @keydown.tab.exact.prevent="">Rezerviši</button>
            </div>
            <div class="col-lg-12 sredina">
              <a @click="toggleIzmena">Odustani od rezervacije <font-awesome-icon :icon="['fas', 'times']" /></a>            
            </div>
          </div>
        </div>     
      </div>
    </transition>
  </div>
  <!-- Komentari sta treba dodati:
        1.tab trap na prvom mestu @keydown.shift.tab.prevent=""
        i na poslednjem mestu isto -->
</template>

<script>
import { computed, defineComponent } from '@vue/composition-api'
import Datepicker from 'vuejs-datepicker'
import VueTimepicker from 'vue2-timepicker'
import UslugaZaCekiranje from '@/components/UslugaZaCekiranje.vue'

export default defineComponent({
    setup() {
        
    },
    components:{
      Datepicker,
      VueTimepicker,
      UslugaZaCekiranje
    },
    data(){
      return{
        checkedUsluge: [],
        isFormShown: false,
        imeLjubimca: "",
        tipLjubimca: "",
        
      }
      
    },
    methods:{
        showForm(){
          this.isFormShown=!this.isFormShown
            // this.$modal.show('NoviZahtev');
        },
        toggleIzmena(){
           this.isFormShown=false; 
        },
        rezervisi(){
          let mID=this.$store.getters['getOsobaID']
            // snimi podatke
          let payload={
            MusterijaID: mID,
            ImeLjubimca: this.imeLjubimca,
            TipZivotinje:this.tipLjubimca,
            Usluge: this.checkedUsluge
          }
          this.$store.dispatch("napraviNovuRezervaciju", payload)    
          this.isFormShown=!this.isFormShown
          this.checkedUsluge = []
        },
        setDate(date){
           
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
       
       
        
    },
    computed:{
      usluge(){
        return this.$store.getters['getUslugeZaRezervaciju']
      }
    },
    async created(){
      await this.$store.dispatch('postaviOsobaID',  this.$cookies.get("id"));
      await this.$store.dispatch('getUslugeZaRezervaciju');
    }
})
</script>

<style scoped>
.btn-stil{
  margin-top:5%;
    margin-bottom: 5%;
    position: right,
}
.btn-primary{
    background-color: #2b9b86;
    border-color:#2b9b86;

}
.modal-overlay{
  position:absolute;
  top:0%;
  left: 0%;
  right: 0;
  bottom:0%;
  z-index: 98;

}

.fade-enter-active, 
.fade-leave-active{
  transition: opacity 0.5s;
}
.fade-enter,
.fade-leave-to{
  opacity: 0;
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
/* .checkboxes label{
  display:inline-block;
}
.checkboxes label{
  display:inline-block;
} */
.datepicker{
  justify-content: center;
  display: flex;
  margin:auto;
  position: center;
}
/* .timepicker{
   justify-content: center;
  display: flex;
  margin:auto;
  position: center;
} */
.sredina{
  display: flex;
  justify-content: center;
}
.ModalStyle{
  /* background-color: rgba(242, 175, 88, 0.5); */
  background-color: #c2eee6;
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 5px solid #39b89e;
  margin-top: 20px;
  margin-bottom: 20px;
  padding-bottom: 15px;
  padding-top: 15px;
  justify-content: center;
  position: relative;
  /* top: 50%;
  left: 50%;
  right:50%; */

}

/* input, label{
  display:flex;
  position: absolute;
} */
/* .modal{
  background-color: rgba(242, 175, 88, 0.5);
  border-radius: 25px;
  border: 4px solid rgb(242, 175, 88);
  margin-top: 20px;
  margin-bottom: 20px;
  padding-bottom: 15px;
  padding-top: 15px;
  justify-content: center;
} */
/*.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, .5);
  display: table;
  transition: opacity .3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.modal-container {
  width: 300px;
  margin: 0px auto;
  padding: 20px 30px;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, .33);
  transition: all .3s ease;
  font-family: Helvetica, Arial, sans-serif;
  overflow-y: auto;
  max-height: 30px;
}

.modal-header h3 {
  margin-top: 0;
  color: #42b983;
}

.modal-body {
  margin: 20px 0;
}
.main {
  overflow: auto;
  background-color: #fff;
}

.main-text {
  height: 1000px;
}

.modal_open {
  position: fixed;
} */
/* .ModalStyle{
  background-color: rgba(242, 175, 88, 0.5);
  border-radius: 25px;
  border: 4px solid rgb(242, 175, 88);
  margin-top: 20px;
  margin-bottom: 20px;
  padding-bottom: 15px;
  padding-top: 15px;
  justify-content: center;
} */
</style>