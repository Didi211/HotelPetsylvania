<template>
  <div class="cenovnik">
    <div class="row">
      <div v-if="korisnik">
        <HeaderMusterija />
      </div>
      <div v-else>
        <HeaderCenovnik />
      </div>
    </div>
    <div v-if="isDataLoaded==false">
      <AppSpinner />
    </div>
    <div v-else>
      <div class="container CenovnikRow">
        <div class="row">
          <h1>Cenovnik</h1>
        </div>
        <div class="row centriraj">
          <div class="col-xl-10">
            <table class="table table-striped table-bordered table-sm">
              <thead>
                <tr>
                  <th scope="col">Naziv</th>
                  <th scope="col">Cena (RSD)</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="a in cenovnik" :key="a.naziv">
                  <td>{{ a.naziv }}</td>
                  <td>{{ a.cena }}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="footer">
          <Footer />
        </div>
      </div>
    </div>
  </div>
</template>



<script>
import { defineComponent } from "@vue/composition-api";
import HeaderCenovnik from "@/components/Header.vue";
import HeaderMusterija from "@/components/HeaderMusterija.vue"
import Footer from "@/components/Footer.vue";
import {mapGetters} from 'vuex'
// import Api from '@/api-services/proba.js'

export default defineComponent({
  setup() {},
  components: {
    HeaderCenovnik,
    HeaderMusterija,
    Footer,
  },
  data(){
    return{
      isDataLoaded:false
    }
  },
  mounted() {
    this.$store.dispatch("loadCene").then(()=>{
      this.isDataLoaded=true
    })
  },
  computed: {
    ...mapGetters({
      'korisnik': 'authKorisnik'
    }),
    cenovnik() {
      return this.$store.state.cenovnik;
    },
  },
});
</script>

<style scoped>
.footer {
  position: relative;
}
.CenovnikRow {
  padding-top: 6rem;
  padding-bottom: 15%;
}
.table {
  width: 100%;
  table-layout: fixed;
  padding-top: 4rem;
  padding-bottom: 15%;
  justify-content: center;
  border-color: black;
  margin-right: 0%;
  margin-left: 0%;
}
th,
tr {
  text-align: center;
}
.centriraj{
  justify-content: center;
}
</style>