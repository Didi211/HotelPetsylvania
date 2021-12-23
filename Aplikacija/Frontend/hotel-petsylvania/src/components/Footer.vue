<template>
  <footer class="footer text-center">
    <div class="container">
      <div class="row">
        <!-- Footer Location-->
        <div class="col-lg-6 mb-5 mb-lg-0">
          <h4 class="text-uppercase mb-4">Lokacija</h4>
          <p class="lead mb-0">
            ul. Vojvode Tankosića 16
            <br />
            Niš Medijana 18000
          </p>
        </div>
        <!-- Footer Social Icons-->
        <!-- <div class="col-lg-4 mb-5 mb-lg-0">
          <h4 class="text-uppercase mb-4">Around the Web</h4>
          <a class="btn btn-outline-light btn-social mx-1" href="#!"><i class="fab fa-fw fa-facebook-f"></i></a>
          <a class="btn btn-outline-light btn-social mx-1" href="#!"><i class="fab fa-fw fa-twitter"></i></a>
          <a class="btn btn-outline-light btn-social mx-1" href="#!"><i class="fab fa-fw fa-linkedin-in"></i></a>
          <a class="btn btn-outline-light btn-social mx-1" href="#!"><i class="fab fa-fw fa-dribbble"></i></a>
        </div> -->
        <!-- Footer About Text-->
        <div class="col-lg-6">
          <h4 class="text-uppercase mb-4">Kontakt Informacije:</h4>
          <p class="lead mb-0">
            Tel.: +381652770489
           
            <!-- <a href="http://startbootstrap.com">Start Bootstrap</a>
            . -->
          </p>
          <p class="lead mb-0">Email: hotel.petsylvania@gmail.com</p>
        </div>
      </div>
      <div class="row pt-3" >
        <p class="lead mb-0"><font-awesome-icon :icon="['fas', 'copyright']" />2021 Copyright: HotelPetsylvania by 101Dalmatinac </p>
      </div>
    </div>
  </footer>
</template>

<script>
import { defineComponent } from '@vue/composition-api'
import { mapGetters } from 'vuex'

export default defineComponent({
    setup() {
        
    },
    computed:{
      ...mapGetters({
        'korisnik': 'authKorisnik'
      })
    }
})
</script>

<style scoped>
.dodatak{
  font-size: 1rem;
}

</style>