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
          <h3>Sva aktuelna obaveštenja:</h3>
          <div class="row">
            <AdminObavestenje v-for="obavestenje in obavestenja" :key="obavestenje.id" :obavestenje="obavestenje"/>
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
import AdminObavestenje from "@/components/AdminObavestenje.vue"

export default {
  name: "AdminListaObavestenja",
  components: {
    HeaderAdmin,
    Footer,
    AdminObavestenje
  },
  data() {
    return {
      isDataLoaded:false
    };
  },
  computed:{
    obavestenja() {
      return this.$store.getters['getObavestenjaAdmin']
    }
  },
  async created(){
    await this.$store.dispatch("postaviOsobaID", this.$cookies.get("id"))
    await this.$store.dispatch('getObavestenjaAdmin').then(()=>{
      this.isDataLoaded=true;
    })
  }
};
</script>

<style scoped>
.SrednjiRow {
  padding-top: 6rem;
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