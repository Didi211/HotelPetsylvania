<template>
  <div class="register">
    <div class="row">
      <Header />
    </div>
    <div v-if="!isDataLoaded">
      <AppSpinner />
    </div>
    <div v-else>
      <div class="wrapper RegistracijaRow">
        <!-- <div class="row">
        <HeaderLoginRegister />
      </div> -->
        <div class="col-lg-8">
          <form class="form-signin" @submit.prevent>
            <h2 class="form-signin-heading text-center ml-2 mr-2">Registracija</h2>
            <input v-model="form.ime" type="text" class="form-control" name="name" placeholder="Ime" required="" autofocus="">
            <input v-model="form.prezime" type="text" class="form-control" name="surname" placeholder="Prezime" required="" autofocus="">
            <input v-model="form.username" type="text" class="form-control" name="username" placeholder="Username" required="" autofocus="">
            <input v-model="form.sifra" type="password" class="form-control" name="password" placeholder="Šifra" required="" /> 
            <input v-model="form.potvrdiSifru" type="password" class="form-control" name="confirm" placeholder="Potvrdi šifru" required="" /> 
            <button @click.prevent="register" class="btn btn-lg btn-primary btn-block">Registruj se</button>
          </form>
          <div class="row">
            <router-link style="color:#267a6b" :to="{name:'Login'}">Već imate nalog?</router-link>
          </div>
        </div>
      </div>
      <div class="row">
        <Footer />
      </div>
    </div>
  </div>
</template>

<style scoped>
body{
   background: #eee; 
}
.wrapper{
    margin: 80px;
}
.form-signin{
    /* max-width: 40%;
    min-width: 380px;    */
    margin-left: 10%;
    margin-right: 10%;
    background-color: #c2eee6;
    padding: 15px 40px 50px;
    border:4px solid rgba(80, 182, 163, 0.644);
    /* border: 1px solid #267a6b; */
    border-radius: 5%;
}
.btn-primary{
    background-color: #267a6b;
    border-color:#267a6b;
}
h2{
    color: #267a6b;
    word-wrap: break-word;
     -webkit-hyphens: auto;
     -moz-hyphens: auto;
     -ms-hyphens: auto;
     -o-hyphens: auto;
     hyphens: auto;
    /* font-size:2.5vw; */
}
.form-signin .form-signin-heading, .form-signin, .checkbox{
     margin-bottom: 6%;
     justify-content: center;
}
.form-signin input[type="text"], .form-signin input[type="password"]{
    margin-bottom: 5%;
}
.RegistracijaRow{
  padding-top: 6rem;
  justify-content: center;
  display: flex;
}
@media screen and (max-width: 380px) {
  .form-signin-heading, .form-control, .checkbox, h2,
  .form-signin input[type="text"], .form-signin input[type="password"] {
    width: 100%;
    margin-top: 0;
  }
}
/* @media screen and (max-width: 768px) {
    h2{
        font-size:14px;
    }
} */
</style>

<script>
import { defineComponent } from '@vue/composition-api'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'

export default defineComponent({
    setup() {
        
    },
    components:{
      Header,
      Footer
    },
    data () {
      return {
        isDataLoaded:true,
        form: {
          ime: "",
          prezime: "",
          username: "",
          sifra: "",
          potvrdiSifru: ""
        }
      }
    },
    methods:{
      async register(){
        if(this.form.ime==""){
           this.$toasted.show("Polje za ime mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
        }
        else if(this.form.prezime==""){
           this.$toasted.show("Polje za prezime mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
        }
        else if(this.form.username==""){
           this.$toasted.show("Polje za username mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
        }
        else if(this.form.sifra==""){
           this.$toasted.show("Polje za šifru mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
        }
        else if(this.form.potvrdiSifru==""){
           this.$toasted.show("Morate potvrditi šifru!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
        }
        else if(this.form.potvrdiSifru!=this.form.sifra){
           this.$toasted.show("Šifre se ne poklapaju, probajte ponovo!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
        }
        else{
          this.isDataLoaded = false
          await this.$store.dispatch('registerKorisnik', this.form).then(()=>{
            this.isDataLoaded = true
          })
        }
      }
    }
})
</script>
