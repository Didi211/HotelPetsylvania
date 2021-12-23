<template>
  <div class="Obavestenje">
    <h4>{{ obavestenje.sadrzaj }}</h4>
   
    <!-- <p>Popust: ne</p> -->
  </div>
</template>

<script>
import { defineComponent } from '@vue/composition-api'

export default defineComponent({
   name: "MusterijaObavestenja",
   props:{
       obavestenje:{
           required: true,
           type: Object,
       },
   }
})
</script>

<style scoped>
.Obavestenje {
   background-color: rgba(255, 255, 255, 0.7);
  /* background-color: rgba(242, 175, 88,0.5); */
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 4px solid rgb(255, 255, 255);

  margin-top: 20px;
  margin-bottom: 20px;
  margin-left: 40px; /*ovo bi trebalo na lepsi nacin da se resi */
  margin-right: 40px;
  padding-bottom: 15px;
  padding-top: 15px;
 
}
/* p{
  font-size: x-large;
  font-family:Arial
} */

</style>