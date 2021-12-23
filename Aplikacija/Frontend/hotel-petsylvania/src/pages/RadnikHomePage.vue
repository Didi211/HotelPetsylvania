<template>
  <div>
    <div class="container-fluid">
      <div class="row">
        <HeaderRadnik />
      </div>
      <div v-if="isDataLoaded">
        <div class="container SrednjiRow">
          <div class="row">
            <div class="col-xl-12 KolDodavanje">
              <div class="row">
                <h3>Zahtevi za čuvanje:</h3>
              </div>
              <div class="row mestoIzmedju">
                <VueSlickCarousel v-bind="settings">
                  <RadnikZahtevCuvanje v-for="zahtev in zahteviCuvanje" :key="zahtev.id" :zahtev="zahtev"/>
                </VueSlickCarousel>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-xl-4 KolDodavanje">
              <h3>Neobrađeni zahtevi:</h3>
              <VueDraggable class="list-group-one drag-board" :list="zahteviNeobradjeni" :group="{name: 'grupa'}" :move="pomeri">
                <RadnikZahtev class="list-group-item" :key="zahtev.id" v-for="zahtev in zahteviNeobradjeni" :zahtev="zahtev"/>          
              </VueDraggable>
            </div>
            <div class="col-xl-4 KolDodavanje">
              <h3>Zahtevi u obradi</h3>
              <VueDraggable class="list-group-two drag-board" :list="zahteviUObradi" :group="{name: 'grupa'}" :move="pomeri">
                <RadnikZahtev class="list-group-item" :key="zahtev.id" v-for="zahtev in zahteviUObradi" :zahtev="zahtev"/>          
              </VueDraggable>
            </div>
            <div class="col-xl-4 KolDodavanje">
              <h3>Završeni zahtevi</h3>
              <VueDraggable class="list-group-three drag-board" :list="zahteviObradjeni" :group="{ name: 'grupa', pull: false}" :move="pomeri">
                <RadnikZahtev class="list-group-item" :key="zahtev.id" v-for="zahtev in zahteviObradjeni" :zahtev="zahtev"/>          
              </VueDraggable>
            </div>
          </div>
        </div>
        <div class="row">
          <Footer />
        </div>
      </div>
      <div v-else>
        <AppSpinner />
      </div>
    </div>
  </div>
</template>

<script>
import HeaderRadnik from "@/components/HeaderRadnik.vue";
import Footer from "@/components/Footer.vue";
import RadnikZahtevCuvanje from "@/components/RadnikZahtevCuvanje.vue"
import RadnikZahtev from "@/components/RadnikZahtev.vue"

import "vue-slick-carousel/dist/vue-slick-carousel.css";
// optional style for arrows & dots
import "vue-slick-carousel/dist/vue-slick-carousel-theme.css";

export default {
  name: "RadnikHomePage",
  components: {
    HeaderRadnik,
    Footer,
    RadnikZahtevCuvanje,
    RadnikZahtev
  },
  props: {},
  data() {
    return {
      isDataLoaded: false,
      settings: {
        dots: true,
        infinite: true,
        speed: 500,
        slidesToShow: 4,
        slidesToScroll: 4,
        initialSlide: 0,
        responsive: [
          {
            breakpoint: 1024,
            settings: {
              slidesToShow: 3,
              slidesToScroll: 3,
              infinite: true,
              dots: true,
            },
          },
          {
            breakpoint: 600,
            settings: {
              slidesToShow: 2,
              slidesToScroll: 2,
              initialSlide: 2,
            },
          },
          {
            breakpoint: 480,
            settings: {
              slidesToShow: 1,
              slidesToScroll: 1,
            },
          },
        ],
      },
    };
  },
  computed:{
    zahteviCuvanje() {
      return this.$store.getters['getZahteviCuvanjeRadnik']
    },
    zahteviNeobradjeni(){
      return this.$store.getters['getZahteviNeobradjeniRadnik']
    },
    zahteviUObradi(){
      return this.$store.getters['getZahteviUObradiRadnik']
    },
    zahteviObradjeni(){
      return this.$store.getters['getZahteviObradjeniRadnik']
    },
    radnikID(){
      return this.$store.getters['getosobaid']
    }
  },
  async created(){
    await this.$store.dispatch("postaviOsobaID", this.$cookies.get("id"))
    await this.$store.dispatch('getZahteviCuvanjeRadnik',this.radnikID).then(()=>{
      this.isDataLoaded = true
    }
    )
  },
  methods:{
    pomeri(evt){
      if(evt.from == document.getElementsByClassName("list-group-one")[0] && evt.to == document.getElementsByClassName("list-group-two")[0]){
        evt.draggedContext.element.obradjen = "U_OBRADI";
        let obj={
          RezervacioniBroj:evt.draggedContext.element.zahtevID,
          UslugaID:evt.draggedContext.element.uslugaID,
          RadnikID:this.radnikID
        }
        this.$store.dispatch('postaviUslugaZaObradu', obj)
      }
      else if(evt.from == document.getElementsByClassName("list-group-two")[0] && evt.to == document.getElementsByClassName("list-group-three")[0]){
        evt.draggedContext.element.obradjen = "OBRADJEN";
        let obj={
          rezervacioniBroj:evt.draggedContext.element.zahtevID,
          uslugaID:evt.draggedContext.element.uslugaID,
          radnikID:this.radnikID
        }
        this.$store.dispatch('postaviUslugaObradjena', obj)
      }
      else{
        return false
      }
    }
  }
};
</script>

<style scoped>
.SrednjiRow {
  padding-top: 6rem;
}

.KolDodavanje {
  /* background-color: rgba(255, 255, 255, 0.7); */
  background-color: rgba(242, 175, 88, 0.5);
  border-radius: 25px;
  border: 4px solid rgb(242, 175, 88);
  margin-top: 20px;
  margin-bottom: 20px;
}

.KolPIzmeniKap {
  justify-content: center;
  display: flex;
  align-items: center;
}

.labele {
  font-weight: bold;
}

.SrednjiRow {
  padding-top: 6rem;
}

.KolZahtevi {
  /* background-color: rgba(255, 255, 255, 0.7); */
  background-color: rgba(242, 175, 88, 0.5);
  border-radius: 25px;
  border: 4px solid rgb(242, 175, 88);
  margin-top: 20px;
  margin-bottom: 20px;
}
.mestoIzmedju{
  display:flex;
  justify-content: space-between;
}
.drag-board{
  min-height: 230px;
}
</style>