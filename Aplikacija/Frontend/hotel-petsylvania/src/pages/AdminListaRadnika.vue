<template>
  <div>
    <div class="container-fluid">
      <div class="row">
        <HeaderAdmin />
      </div>
      <div v-if="!isDataLoaded">
        <AppSpinner />
      </div>
      <div v-else>
        <div class="container SrednjiRow">
          <h3>Lista svih radnika:</h3>
          <div class="row radniciprikaz">
            <AdminRadnikUListi v-for="radnik in radnici" :key="radnik.id" :radnik="radnik"/>
          </div>
        </div>
        <div class="row">
          <Footer />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import HeaderAdmin from "@/components/HeaderAdmin.vue";
import Footer from "@/components/Footer.vue";
import AdminRadnikUListi from "@/components/AdminRadnikUListi.vue"

export default {
  name: "AdminListaRadnika",
  components: {
    HeaderAdmin,
    Footer,
    AdminRadnikUListi
  },
  props: {

  },
  data() {
    return {
      isDataLoaded:false
    };
  },
  computed:{
    radnici() {
      return this.$store.getters['getRadnikeAdmin']
    }
  },
  async created(){
    await this.$store.dispatch("postaviOsobaID", this.$cookies.get("id"))
    await this.$store.dispatch('getRadnikeAdmin').then(()=>{
      this.isDataLoaded=true;
    })
  }
};
</script>

<style scoped>
.SrednjiRow {
  padding-top: 6rem;
}
.radniciprikaz{
  display:flex;
  justify-content:space-around;
}
.KolDodavanje {
  background-color: rgba(255, 255, 255, 0.7);
  /* background-color: rgba(242, 175, 88,0.5); */
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 4px solid rgb(255, 255, 255, 1);
  margin-top: 20px;
  margin-bottom: 20px;
}
</style>