<template>
  <div>
    <div class="checkboxes">
      <label class="checkbox" for="checkUsluga">
        <input :name="usluga.naziv" :value="usluga.naziv" class="checkbox" type="checkbox" id="checkUsluga" @click="pushUsluga">          
        {{ usluga.naziv }}
      </label>         
    </div>
    <div class="row">
      <div class="col-md-6 col-sm-6 col-xs-6 picker" v-if="isChecked">                    
        <datepicker class="datepicker" :value="dateStart" v-model="dateStart" format="dd/MM/yyyy" :disabled-dates="disabledDates" id="checkUsluga1" @click="datePickerOpen"></datepicker>
      </div>     
      <div class="col-md-6 col-sm-6 col-xs-6 picker" v-if="isChecked">  
        <div v-if="usluga.tip=='JEDNOKRATNA_USLUGA'" >   
          <div class="md-form">
            <input type="time" id="inputMDEx1" class="timepicker" v-model="timeU" format="hh/mm" @change="pushData" >
          <!-- <label for="inputMDEx1">Choose your time</label> -->
          </div> 
        </div>             
        <!-- <vue-timepicker class="timepicker" lazy manual-input hide-dropdown id="checkUsluga3" @change="pushData"></vue-timepicker> -->
      </div>
    </div>  
    <div class="row">
      <div class="col-md-6 col-sm-6 col-xs-4  picker" v-if="isChecked && usluga.naziv=='Boravak'">                    
        <datepicker class="datepicker" :value="dateEnd" v-model="dateEnd" format="dd/MM/yyyy" :disabled-dates="disabledDates" id="checkUsluga2" placeholder="endDate" @input="pushData"></datepicker>
      </div>
    </div>    
  </div>
</template>

<script>
import { defineComponent } from '@vue/composition-api'
import Datepicker from 'vuejs-datepicker'
import VueTimepicker from 'vue2-timepicker'
import Vue from 'vue'
import moment from 'moment'

export default defineComponent({
    setup() {
        
    },
    props:{
       usluga: {
           required:true,
           type: Object,
       },
   },
   components:{
    Datepicker,
    VueTimepicker
   },
   data(){
       return{
        dateStart: new Date(),
        dateEnd: new Date(),
        termin: null,
        timeU: "",
        boravakDate: new Date(),
        isChecked:false,
        isDatePickerOpen: false,
        checkedUsluga: {
            UslugaID:null,
            TipUsluge: "",
            Termin:"",
            DatumPocetka:"",
            DatumZavrsetka:""
        },
        disabledDates: {
          customPredictor: function(date){
            const today = new Date()
            today.setDate(today.getDate()-1) //yesterday
            return date < today
          },
           from: this.boravakDate,

          // customPredictorFrom: function(date){
          //   if(document.getElementById("flexCheckDefault").value == 'Boravak'){
          //     const today = new Date(document.getElementById("endDate").value)
          //     return date>today
          //   }            
          // }
          // to: new Date(),
          // from: new Date(document.getElementById("endDate").value)
        },
        
       }
   },
   methods:{
        pushUsluga(event){
          if(this.isChecked == false)
          {
            this.isChecked=true
            this.checkedUsluga.UslugaID=this.usluga.id,
            this.checkedUsluga.TipUsluge=this.usluga.tip
            // this.$emit('childToParentYes', this.usluga.naziv)

          }
          else{
            this.isChecked=false
            this.$emit('childToParentNo', this.checkedUsluga)
          }
        },
        datePickerOpen(event){
          // this.isDatePickerOpen=!this.isDatePickerOpen
          if(this.isDatePickerOpen==false){
            this.isDatePickerOpen=true
            document.getElementById("inputMDEx1").disabled = true
          }
          else{
            this.isDatePickerOpen=false
            document.getElementById("inputMDEx1").disabled = false
          }
        },
        boravakOgranicenje(){
          for(boravak in this.checkedUsluge)
          {
           if(boravak == 'Boravak'){
             const today = new Date(document.getElementById("endDate").value)
             this.boravakDate.setDate(today)
           }   
          }         
        },
        endDateBoravak(){
          if(this.usluga.naziv == 'Boravak')
          {
            this.isChecked ==true; 
          }

        },
        pushData({data}){
          let vreme = this.timeU.split(':')
          let hour = vreme[0]
          let min= vreme[1]
          let Nvreme = hour+'/'+min
          // this.termin=data.hh + '/' + data.mm          
          if(this.usluga.tip == 'JEDNOKRATNA_USLUGA')
          {
            let terminUsluge =this.format_date(this.dateStart)+ '/'+ Nvreme
            this.checkedUsluga.Termin=terminUsluge       
          }
             
          if(this.usluga.naziv == 'Boravak')
          {
            if(this.dateStart < this.dateEnd){
               this.checkedUsluga.DatumPocetka=this.format_date(this.dateStart)
              this.checkedUsluga.DatumZavrsetka=this.format_date(this.dateEnd)
            }
            else{
               Vue.toasted.show("Ispravno izaberite datume za boravak. Datum pocetka mora da bude manji od datuma zavrsetka", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   return;
            }           
          }
          else{
            this.checkedUsluga.DatumZavrsetka=null
            this.checkedUsluga.DatumPocetka=null
          }
          this.$emit('childToParentYes', this.checkedUsluga)
        },
         format_date(value){
         if (value) {
           return moment(String(value)).format('DD/MM/yyyy')
          }
      },

   }
})
</script>

<style scoped>
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
.picker{
    display: flex;
    justify-content: center;
}
.timepicker{
  padding: 1px 2px;
  width: 195px;
}

</style>
