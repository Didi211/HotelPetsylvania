<template>
  <div class="login">
    <div class="row">
      <Header />
    </div>
    <div v-if="!isDataLoaded">
      <AppSpinner />
    </div>
    <div v-else>
      <div class="wrapper PrijavaRow">
        <div class="col-lg-8">
          <form class="form-signin" @submit.prevent>
            <h2 class="form-signin-heading text-center">Prijava</h2>
            <input type="text" class="form-control" v-model="loginInfo.username" name="username" placeholder="Username" autofocus="">           
            <input type="password" class="form-control" v-model="loginInfo.sifra" name="sifra" placeholder="password"/>
            <button class="btn btn-lg btn-primary btn-block" @click="login">Prijavi se</button>
          </form>
          <div class="row">
            <router-link style="color:#267a6b" :to="{name:'Register'}">Nemate nalog?</router-link>
          </div>
        </div>
      </div>
      <div class="row">
        <Footer />
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent } from '@vue/composition-api'
import { mapState } from 'vuex'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'
// import { required } from 'vuelidate/lib/validators'

export default defineComponent({
    setup() {
        
    },
    data(){
      return{
        isDataLoaded:true,
        loginInfo: {
          username: "",
          sifra: ""
        }
      }
    },
    // validations:{
    //   loginInfo: {
    //       username: {
    //         required
    //       },
    //       sifra: {
    //         required
    //       }
    //     }
    // },
    components: {
      Header,
      Footer
    },
    computed: {
      ...mapState(['korisnici'])
    },
    mounted(){
      // this.$store.dispatch('loadKorisnika'); //nigde se ne koristi i nije tacno
    },
    methods: {
      async login(){
        if(this.loginInfo.username==""){
           this.$toasted.show("Polje za username mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
        }
        else if(this.loginInfo.sifra==""){
           this.$toasted.show("Polje za šifru mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
        }
        else{
          this.isDataLoaded = false
          await this.$store.dispatch('loginKorisnik', this.loginInfo).then(()=>
          {
            this.isDataLoaded=true
          })
        }
        // this.isDataLoaded = false
      }
    }

})
</script>


<style scoped>
body{
   background: #eee; 
}
.wrapper{
    margin: 80px;
}
.form-signin{
    /* max-width: 40%;   */
    margin-left: 10%;
    margin-right: 10%;
    background-color: #c2eee6;
    padding: 15px 40px 50px;
    border: 4px solid rgba(80, 182, 163, 0.644);;
    border-radius: 5%;
    /* justify-content: center; */
}
/* .form-control{
  max-width: 40%;
} */
.btn-primary{
    background-color: #267a6b;
    border-color:#267a6b;
}
h2{
    color: #267a6b;
}
/* .checkbox{
  min-width: 40%;
} */
.form-signin .form-signin-heading, .form-signin, .checkbox{
     margin-bottom: 6%;
}
.form-signin input[type="text"], .form-signin input[type="password"]{
    margin-bottom: 5%;
}
.PrijavaRow{
  padding-top: 6rem;
  display: flex;
  justify-content: center;
}

/* .forma{
  justify-content: center;
} */
.footer{
  position:relative;
}
@media screen and (max-width: 380px) {
  .form-signin-heading, .form-control, .checkbox, h2,
  .form-signin input[type="text"], .form-signin input[type="password"] {
    width: 100%;
    margin-top: 0;
  }
}
</style>
