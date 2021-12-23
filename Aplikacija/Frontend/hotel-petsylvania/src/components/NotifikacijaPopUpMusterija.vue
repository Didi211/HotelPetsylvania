<template>
  <div>
    <a class="dropdown-item" @click="showNotification">notifikacija {{ doMath(index) }} </a>
    <div >
      <div role="status" :id="notifikacija.id" aria-live="polite" aria-atomic="true" class="toast" style="position: inline;" data-autohide="false">
        <div class="toast-header">
          <font-awesome-icon :icon="['fas', 'info']" />
          <!-- <svg class=" rounded mr-2" width="20" height="20" xmlns="http://www.w3.org/2000/svg"
                     preserveAspectRatio="xMidYMid slice" focusable="false" role="img">
                  <rect fill="#007aff" width="100%" height="100%" /></svg> -->
          <strong class="mr-auto ml-2">notifikacija {{ doMath(index) }}</strong>                
          <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="toast-body">
          {{ notifikacija.sadrzaj }}
        </div>
      </div>
    </div>
  </div>
  <!-- role="alert" aria-live="assertive" -->
</template>

<script>
import { defineComponent } from '@vue/composition-api'

export default defineComponent({
    setup() {
        
    },
    props:{
        notifikacija:{
            required:true,
           type: Object,
        },
        index:{
            required: true,
            type: Number,
        },
    },
    data: ()=>{
      return{
        isToast: true, 
      }
    },
     methods:{
      doMath: function (index) {
        return index+1
      },
      showNotification(){
      //  debugger
      var id= this.notifikacija.id
      var vr ='#'+id.toString()
        $(vr).toast('show');
        // this.isToast=!this.isToast
        // this.notifShow=!this.notifShow
      }
    },

})
</script>

<style scoped>
.toast{
  background-color:azure;
}
#myToast .alert {
    margin-bottom: 0;
}
</style>