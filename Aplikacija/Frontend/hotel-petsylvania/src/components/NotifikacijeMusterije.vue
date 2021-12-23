<template>
  <div>
    <div class="row dropdown">
      <a class="nav-link mr-5 py-3 px-0 px-lg-3 rounded dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" href="#registracija" @click="isNotifClicked=!isNotifClicked" >Notifikacije</a>
      <!-- <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
        Notifikacije
      </button> -->
      <div v-if="isNotifClicked" class="dropdown-menu navbar-collapse navbar " aria-labelledby="dropdownMenuButton" >
        <div v-if="notifikacije.length>0 ">        
          <!-- <a class="dropdown-item">Trenutno nemate novih notifikacija</a> -->
          <!-- <a class="dropdown-item">{{ notifikacije[1].sadrzaj }}</a> -->
          <!-- <div class="Notif  mr-5 py-3 px-0 px-lg-3" v-for="n in notifikacije" :key="n.id" >
            <a class="dropdown-item">{{ n.sadrzaj }}</a>
          </div> -->
          <!-- ovde ide div sa v-for i onda se komponenti notifikacijaPopUpMusterija salje taj jedan obj -->
          <NotifikacijaPopUpMusterija v-for="(notifikacija, index) in notifikacije" 
                                      :key="notifikacija.id" 
                                      :notifikacija="notifikacija"
                                      :index="index"/>
          <!-- <div role="alert" aria-live="assertive" aria-atomic="true" class="toast" data-autohide="false">
              <h4>{{ n.sadrzaj }}</h4>
            </div> -->
        </div>
        <div v-else>
          <p class="ml-2">Trenutno nemate novih notifikacija</p>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { defineComponent } from '@vue/composition-api'
import Vue from 'vue'
import NotifikacijaPopUpMusterija from '@/components/NotifikacijaPopUpMusterija.vue'

export default defineComponent({
    name: "Notifikacije",
    setup() {
        
    },
    props:{
        // notifikacije:{
        //     required: true,
        //     type: Object, 
        // }
    },
    components:{
      NotifikacijaPopUpMusterija
    },
    data(){
      return{
        isNotifClicked: false,
        notifShow:false,
      }
    },
    methods:{
      doMath: function (index) {
        return index+1
      },
     
    },
    computed: {
      notifikacije(){
        
        return this.$store.getters['getterNotifikacijeMusterije']
      }
    },
    async created(){
      await this.$store.dispatch("postaviOsobaID", this.$cookies.get("id"))
      await this.$store.dispatch("getNotifikacijeMusterije")
     
    }

})
</script>

<style scoped>
.Notif{
  background-color: #c2eee6;
  /* background-color: rgba(242, 175, 88,0.5); */
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 4px solid #39b89e;
  margin: auto;
  width: auto;
  display: fixed;
  justify-content: center
}


h4{
    color: #174b42;
}
.toast{
  background-color:azure;
}
.navbar-collapse, .dropdown-menu{
  background-color: #c2eee6;
  overflow: visible !important;
  position: relative;
}
.dropdown-toggle{
  color: #2b9b86;
  font: 1.5rem;
}
.dropdown{
 margin-left: 0;
 margin-right: 0;
}
/* .dropdown-menu {
  
    
    top: 100%;
    left: 0;
    z-index: 1000;
    display: none;
    float: center;
    min-width: 160px;
    padding: 5px 0;
    margin: 2px 0 0;
    font-size: 14px;
    text-align: left;
    list-style: none; */
    /* background-color: #fff; */
    /* background-clip: padding-box; */
    /* border: 1px solid #ccc; */
    /* border: 1px solid rgba(0,0,0,.15); */
    /* border-radius: 4px; */
    /* -webkit-box-shadow: 0 6px 12px rgb(0 0 0 / 18%); */
    /* box-shadow: 0 6px 12px rgb(0 0 0 / 18%); */
/* } */
a{
  margin-left: 5%;
    font: 1.5rem;
     font-weight: lighter;
}
</style>

