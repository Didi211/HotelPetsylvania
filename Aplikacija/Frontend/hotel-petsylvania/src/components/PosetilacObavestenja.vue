<template>
  <div class="Obavestenje ml-3 mr-3">
    <p>{{ obavestenje.sadrzaj }}</p>
    <hr>
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
/* .Obavestenje {
  background-color: #5bd4bc94;
  border-radius: 25px;
  border: 3px solid #3bb9a2;
  margin-top: 20px;
  margin-bottom: 20px;
  margin-left: 45px; 
  margin-right: 45px;
  padding-bottom: 15px;
  padding-top: 15px;
  display: flex;
  justify-content: center;
 
} */
p{
  font-size: x-large;
  font-family:Arial
}
hr{
  color: white;
  opacity: 100%;
}
</style>