<template>
  <div class="hello">
    <div class="row">
      <Header />
    </div>
    <div v-if="isDataLoaded">
      <header class="masthead bg-primary text-white text-center">
        <div class="container d-flex align-items-center flex-column">
          <!-- Masthead Avatar Image-->
          <img class="masthead-avatar mb-5" src="assets/img/avataaars.svg" alt="" />
          <!-- Masthead Heading-->
          <h1 class="masthead-heading text-uppercase mb-0">Dobrodošli u Hotel Petsylvania</h1>
          <!-- Icon Divider-->
          <div class="divider-custom divider-light">
            <div class="divider-custom-line"></div>
            <div class="divider-custom-icon"><i class="fas fa-star"></i></div>
            <div class="divider-custom-line"></div>
          </div>
          <!-- Masthead Subheading-->
          <h2 class="masthead-subheading font-weight-light mb-0">Hotel za sve vaše kućne ljubimce!</h2>
        </div>
      </header>
      <!-- Portfolio Section-->
      <section id="portfolio" class="page-section portfolio" >
        <div class="container">
          <!-- Portfolio Section Heading-->
          <h2 class="page-section-heading text-center text-uppercase text-secondary mb-0">GALERIJA</h2>
          <!-- Icon Divider-->
          <div class="divider-custom">
            <div class="divider-custom-line"></div>
            <div class="divider-custom-icon"><i class="fas fa-star"></i></div>
            <div class="divider-custom-line"></div>
          </div>
          <div id="slides" class="carousel slide" data-ride="carousel">
            <ul class="carousel-indicators">
              <li data-target="#slides" data-slide-to="0" class="active"></li>
              <li data-target="#slides" data-slide-to="1"></li>
              <li data-target="#slides" data-slide-to="2"></li>
              <li data-target="#slides" data-slide-to="3"></li>
            </ul>
            <div class="carousel-inner">
              <div class="carousel-item active">
                <img class="img-responsive img-fluid" src="../assets/sobe.jpg">
              </div>
              <div class="carousel-item">
                <img class="img-responsive img-fluid" src="../assets/koktel.jpg">
              </div>
              <div class="carousel-item">
                <img class="img-responsive img-fluid" src="../assets/restoran1.jpg">
              </div>
              <div class="carousel-item">
                <img class="img-responsive img-fluid" src="../assets/uzivancija.jpg">
              </div>
            </div>
          </div>
        </div>
      </section>
      <!-- About Section-->
      <section id="about" class="page-section bg-primary text-white mb-0" >
        <div class="container">
          <!-- About Section Heading-->
          <h2 class="page-section-heading text-center text-uppercase text-white">O Hotelu</h2>
          <!-- Icon Divider-->
          <div class="divider-custom divider-light">
            <div class="divider-custom-line"></div>
            <div class="divider-custom-icon"><i class="fas fa-star"></i></div>
            <div class="divider-custom-line"></div>
          </div>
          <!-- About Section Content-->
          <div class="row">
            <div class="col-lg-4 ml-auto"><p class="lead">Naš udoban smeštaj i sertifikovano osoblje pružaju dobrodošlicu za vaše kućne ljubimce! Gosti će se osećati kao kod kuće uz neprekidnu brigu našeg osoblja. </p></div>
            <div class="col-lg-4 mr-auto"><p class="lead">Nudimo sigurno, čisto, okruženje sa kotrolisanom temperaturom, kao i vreme za igru, vežbanje, zdrave obroke i još mnogo toga!</p></div>
            <div class="col-lg-4 mr-auto"><p class="lead">Celodnevno maženje, paženje i igranje samo za Vaše najbolje prijatelje! Rezervišite najbolju destinaciju za boravak za Vašeg kućnog ljubimca sada!</p></div>
          </div>
          <div class="row ObavestenjaRow ml-auto mr-auto">
            <h1 class="page-section-heading text-center text-uppercase text-white ml-auto mr-auto">Aktuelna obaveštenja</h1>
            <div class="divider-custom divider-light">
              <div class="divider-custom-line"></div>
              <div class="divider-custom-icon"><i class="fas fa-star"></i></div>
              <div class="divider-custom-line"></div>           
            </div>
          </div>
          <div class="row">
            <div class="ml-auto mr-auto">
              <PosetilacObavestenja v-for="obavestenje in obavestenja" 
                                    :key="obavestenje.id" 
                                    :obavestenje="obavestenje"/>
            </div>            
          </div>
        </div>
      </section>
      <!-- Contact Section-->
      <section id="contact" class="page-section" >
        <div class="container">
          <!-- Contact Section Heading-->
          <h2 class="page-section-heading text-center text-uppercase text-secondary mb-0">Kontakt</h2>
          <!-- Icon Divider-->
          <div class="divider-custom">
            <div class="divider-custom-line"></div>
            <div class="divider-custom-icon"><i class="fas fa-star"></i></div>
            <div class="divider-custom-line"></div>
          </div>
          <!-- Contact Section Form-->
          <div class="row">
            <div class="col-lg-8 mx-auto">
              <!-- To configure the contact form email address, go to mail/contact_me.php and update the email address in the PHP file on line 19.-->
              <form id="contactForm" name="sentMessage" novalidate="novalidate">
                <div class="control-group">
                  <div class="form-group floating-label-form-group controls mb-0 pb-2">
                    <div class="form-group floating-label-form-group controls mb-0 pb-2" style="display: inline">
                      <p>Kontakt telefon:</p>
                      <p>+381654890489</p>
                    </div>
                    <!-- <input id="name" class="form-control" type="text" placeholder="Name" required="required" data-validation-required-message="Please enter your name." /> -->
                    <p class="help-block text-danger"></p>
                  </div>
                </div>
                <div class="control-group">
                  <div class="form-group floating-label-form-group controls mb-0 pb-2">
                    <div class="form-group floating-label-form-group controls mb-0 pb-2" style="display: inline">
                      <p>Email:</p>
                      <p>hotel.petsylvania@gmail.com</p>
                    </div>
                    <!-- <label>Email Address</label>
                  <input id="email" class="form-control" type="email" placeholder="Email Address" required="required" data-validation-required-message="Please enter your email address." /> -->
                    <p class="help-block text-danger"></p>
                  </div>
                </div>
                <div class="control-group">
                  <div class="form-group floating-label-form-group controls mb-0 pb-2">
                    <div class="form-group floating-label-form-group controls mb-0 pb-2" style="display: inline">
                      <p>Adresa:</p>
                      <p>Vojvode Tankosića 16</p>
                    </div>
                    <!-- <label>Phone Number</label>
                  <input id="phone" class="form-control" type="tel" placeholder="Phone Number" required="required" data-validation-required-message="Please enter your phone number." /> -->
                    <p class="help-block text-danger"></p>
                  </div>
                </div>               
                <br />             
              </form>
            </div>
          </div>
        </div>
      </section>
      <Footer />
    </div>
    <div v-else>
      <AppSpinner />
    </div>
  </div>
</template>

<script>
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'
import PosetilacObavestenja from '@/components/PosetilacObavestenja.vue'
export default {
  name: 'HelloWorld',
  components: {
    Header,
    Footer,
    PosetilacObavestenja
  },
  props: {
    
  },
  data(){
    return{
      isDataLoaded:false
    }
  },
  computed:{
    obavestenja(){
        return this.$store.getters['getObavestenjaMusterija']
    }
  },
  
  async created(){
     await this.$store.dispatch('getObavestenjaMusterija').then(()=>{
       this.isDataLoaded=true
     })
  }
 
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
.lead{
  color: white;
}
.ObavestenjaRow{
  margin-top: 5%;
  justify-content: center;
}

p{
  font-size: 1.5em;
  color:#2b9b86; 
    position: relative;
    z-index: 1;
    padding-right: 1rem;
    padding-left: 0;
    resize: none;
    border: none;
    border-radius: 0;
    background: none;
    box-shadow: none !important;
}
/* #about {
  margin-top: 10%;
}
#contact{
  margin-top: 10%;
} */
</style>
