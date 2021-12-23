<template>
  <div>
    <div class="row">
      <HeaderAdmin />
    </div>
    <div v-if="isDataLoaded==false">
      <AppSpinner />
    </div>
    <div v-else>
      <div class="container-fluid">
        <div class="row">
          <HeaderAdmin />
        </div>
        <div class="container">
          <div class="row SrednjiRow">
            <div class="col-xl-4 KolDodavanje">
              <h3>Dodaj radnika</h3>
              <form id="forma" action="/action_page.php" @submit.prevent>
                <div class="form-group">
                  <label class="labele" for="name">Ime</label>
                  <input
                    v-model="noviRadnik.ime"
                    type="text"
                    class="form-control"
                    id="imeRadnika"
                    placeholder="Upišite ime"
                    name="name"
                    
                  />
                  <!-- <div class="valid-feedback">Valid.</div> -->
                  <div class="invalid-feedback">Popunite ovo polje.</div>
                </div>
                <div class="form-group">
                  <label class="labele" for="surname">Prezime</label>
                  <input
                    v-model="noviRadnik.prezime"
                    type="text"
                    class="form-control"
                    id="prezimeRadnika"
                    placeholder="Upišite prezime"
                    name="surname"
                    
                  />
                  <!-- <div class="valid-feedback">Valid.</div> -->
                  <div class="invalid-feedback">Popunite ovo polje.</div>
                </div>
                <div class="form-group">
                  <label class="labele" for="email">Username:</label>
                  <input
                    v-model="noviRadnik.username"
                    type="text"
                    class="form-control"
                    id="usernameRadnika"
                    placeholder="Upišite username"
                    name="email"
                    
                  />
                  <!-- <div class="valid-feedback">Valid.</div> -->
                  <div class="invalid-feedback">Popunite ovo polje.</div>
                </div>

                <div class="form-group">
                  <label class="labele" for="pwd">JMBG:</label>
                  <input
                    v-model="noviRadnik.jmbg"
                    type="text"
                    maxlength="13"
                    class="form-control"
                    id="jmbg"
                    placeholder="Upišite JMBG"
                    name="pswd"
                    
                  />
                  <!-- <div class="valid-feedback">Valid.</div> -->
                  <div class="invalid-feedback">Popunite ovo polje.</div>
                </div>

                <button
                  @click="dodajRadnika"
                  type="submit"
                  class="btn btn-primary marstil"
                >
                  <font-awesome-icon :icon="['fas', 'plus-circle']" />&nbsp;&nbsp;
                  Dodaj radnika
                </button>
              </form>
            </div>
            <div class="col-xl-4 KolDodavanje">
              <h3>Dodaj uslugu</h3>
              <form id="forma" action="/action_page.php" @submit.prevent>
                <div class="form-group">
                  <label class="labele" for="usluga">Naziv usluge:</label>
                  <input
                    v-model="novaUsluga.Naziv"
                    type="text"
                    class="form-control"
                    id="UslugaNaziv"
                    placeholder="Upišite naziv usluge"
                    name="nazivusluge"
                  />
                  <!-- <div class="valid-feedback">Valid.</div> -->
                  <div class="invalid-feedback">Popunite ovo polje.</div>
                </div>
                <div class="form-group">
                  <label class="labele" for="cena">Cena usluge:</label>
                  <input
                    v-model="novaUsluga.Cena"
                    type="number"
                    class="form-control"
                    id="UslugaCena"
                    placeholder="Upišite cenu usluge"
                    name="cenausluge"
                    
                  />
                  <!-- <div class="valid-feedback">Valid.</div> -->
                  <div class="invalid-feedback">Popunite ovo polje.</div>
                </div>
                <div class="form-group">
                  <label class="labele" for="kapacitet">Kapacitet usluge:</label>
                  <input
                    v-model="novaUsluga.Kapacitet"
                    type="number"
                    class="form-control"
                    id="UslugaKapacitet"
                    placeholder="Upišite kapacitet usluge"
                    name="kapacitetusluge"
                    
                  />
                  <!-- <div class="valid-feedback">Valid.</div> -->
                  <div class="invalid-feedback">Popunite ovo polje.</div>
                </div>
                <div class="form-group">
                  <label class="labele" for="tipUsluge">Tip usluge:</label>
                  <select
                    v-model="novaUsluga.Tip"
                    class="form-control"
                    id="UslugaTip"
                    name="tipusluge"
                    
                  >
                    <option value="JEDNOKRATNA_USLUGA">Jednokratna usluga</option>
                    <option value="BORAVAK">Boravak</option>
                  </select>
                  <!-- <div class="valid-feedback">Valid.</div> -->
                  <div class="invalid-feedback">Popunite ovo polje.</div>
                </div>
                <button
                  type="submit"
                  class="btn btn-primary marstil"
                  @click="dodajUslugu"
                >
                  <font-awesome-icon :icon="['fas', 'plus-circle']" />&nbsp;&nbsp; Dodaj uslugu
                </button>
              </form>
            </div>
            <div class="col-xl-4 KolDodavanje">
              <h3>Dodaj obaveštenje</h3>
              <form action="/action_page.php" @submit.prevent>
                <div class="form-group">
                  <label class="labele" for="obavestenje">Obaveštenje:</label>
                  <textarea
                    v-model="obavestenje.Sadrzaj"
                    class="form-control rounded-0"
                    id="obavestenjetext"
                    name="obavestenje"
                    rows="10"
                  ></textarea>
                </div>
                <button
                  type="submit"
                  class="btn btn-primary marstil"
                  @click="dodajObavestenje"
                >
                  <font-awesome-icon :icon="['fas', 'plus-circle']" />&nbsp;&nbsp; Dodaj obaveštenje
                </button>
              </form>
            </div>
          </div>
          <div class="row AzurKapRow">
            <div class="col-xl-4 KolPIzmeniKap">
              <h3>Izmeni kapacitete hotela:</h3>
            </div>
            <div class="col-xl-8 KolDodavanje">
              <AdminKapacitetUsluge
                v-for="usluga in usluge"
                :key="usluga.id"
                :usluga="usluga"
              />
            </div>
          </div>
          <div class="row KolDodavanje">
            <div class="row">
              <div class="col-xl-12">
                <div class="container">
                  <div class="row">
                    <h3>Trenutna ponuda svih usluga:</h3>
                  </div>
                  <div class="row Rasporedjivanje">
                    <AdminUsluga
                      v-for="usluga in usluge"
                      :key="usluga.id"
                      :usluga="usluga"
                    />
                  </div>
                </div>
              </div>
            </div>
            <div class="row"></div>
          </div>
          <div class="row KolDodavanje">
            <div class="row">
              <div class="col-xl-12">
                <div class="container">
                  <div class="row">
                    <h3>Deaktivirane usluge:</h3>
                  </div>
                  <div class="row Rasporedjivanje">
                    <AdminDeaktUsluga
                      v-for="usluga in deaktiviraneUsluge"
                      :key="usluga.id"
                      :usluga="usluga"
                    />
                  </div>
                </div>
              </div>
            </div>
            <div class="row"></div>
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
import AdminUsluga from "@/components/AdminUsluga.vue";
import AdminKapacitetUsluge from "@/components/AdminKapacitetUsluge.vue";
import AdminDeaktUsluga from "@/components/AdminDeaktUsluga.vue";
import { mapGetters } from "vuex";

export default {
  name: "AdminHomePage",
  components: {
    HeaderAdmin,
    Footer,
    AdminUsluga,
    AdminKapacitetUsluge,
    AdminDeaktUsluga
  },
  props: {},
  data() {
    return {
      isDataLoaded:false,
      obavestenje: {Sadrzaj: "", BroadCastFlag: true, KomeNamenjeno: null, RadnikID: null},
      novaUsluga: { Naziv: "", Cena: null, Kapacitet: null, Tip: null },
      noviRadnik: {ime: "", prezime: "", username: "", jmbg: "", sifra:"sifra", slika: null}
    };
  },
  computed: {
    usluge() {
      return this.$store.getters["getUslugeAdmin"];
    },
    deaktiviraneUsluge(){
      return this.$store.getters["getUslugeDeaktiviraneAdmin"];
    }
  },
  async created() {
    await this.$store.dispatch("postaviOsobaID", this.$cookies.get("id"))
    await this.$store.dispatch("getUslugeAdmin").then(()=>{
      this.isDataLoaded=true;
    })
  },
  methods: {
    dodajObavestenje() {
      if(this.obavestenje.Sadrzaj==""){
        this.$toasted.show("Niste napisali novo obaveštenje!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else{
        this.$store.dispatch("dodajObavestenjeAdmin", this.obavestenje).then(()=>{
          this.obavestenje.Sadrzaj = ""
          document.getElementById("obavestenjetext").value = "";
      })
      }
      
    },
    dodajUslugu() {
      if(this.novaUsluga.Naziv==""){
        this.$toasted.show("Polje za naziv mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else if(this.novaUsluga.Cena==null || this.novaUsluga.Cena==""){
        this.$toasted.show("Polje za cenu mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else if(this.novaUsluga.Cena<0){
        this.$toasted.show("Cena ne sme biti negativan broj!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else if(this.novaUsluga.Kapacitet==null || this.novaUsluga.Kapacitet==""){
        this.$toasted.show("Polje za kapacitet mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else if(this.novaUsluga.Kapacitet <= 0){
        this.$toasted.show("Kapacitet ne sme biti manji od 0!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else if(this.novaUsluga.Tip==null){
        this.$toasted.show("Tip usluge mora biti izabren!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else{
        this.$store.dispatch("dodajUsluguAdmin", this.novaUsluga).then(()=>{
                this.novaUsluga.Naziv= ""
                this.novaUsluga.Cena=null
                this.novaUsluga.Kapacitet=null
                this.novaUsluga.Tip=null
                document.getElementById("UslugaNaziv").value = "";
                document.getElementById("UslugaCena").value = "";
                document.getElementById("UslugaKapacitet").value = "";
                document.getElementById("UslugaTip").value = "";
        })
      }
    },
    dodajRadnika() {
      if(this.noviRadnik.ime==""){
        this.$toasted.show("Polje za ime mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else if(this.noviRadnik.prezime==""){
        this.$toasted.show("Polje za prezime mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else if(this.noviRadnik.username==""){
        this.$toasted.show("Polje za username mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else if(this.noviRadnik.jmbg==""){
        this.$toasted.show("Polje za JMBG mora biti popunjeno!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else if(this.noviRadnik.jmbg.length !=13){
        this.$toasted.show("JMBG mora da ima 13 cifara!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
      }
      else{
        this.$store.dispatch("dodajRadnikaAdmin", this.noviRadnik).then(()=>{
              this.noviRadnik.ime= ""
              this.noviRadnik.prezime=""
              this.noviRadnik.username=""
              this.noviRadnik.jmbg=""
              document.getElementById("imeRadnika").value = "";
              document.getElementById("prezimeRadnika").value = "";
              document.getElementById("usernameRadnika").value = "";
              document.getElementById("jmbg").value = "";
        })
      }
    },
  },
};
</script>

<style scoped>
.SrednjiRow {
  padding-top: 6rem;
  justify-content: space-between;
}

.KolDodavanje {
  /* background-color: rgba(255, 255, 255, 0.7); */
  background-color: rgba(242, 175, 88, 0.5);
  border-radius: 25px;
  border: 4px solid rgb(242, 175, 88);
  margin-top: 20px;
  margin-bottom: 20px;
  padding-bottom: 15px;
  padding-top: 15px;
}

.KolPIzmeniKap {
  justify-content: center;
  display: flex;
  align-items: center;
}

.labele {
  font-weight: bold;
}
.Rasporedjivanje {
  display: flex;
  justify-content: space-around;
  align-items: center;
}

.btn:focus {
  outline: none;
  box-shadow: none;
}

select:required:invalid {
  color: gray;
}
option[value=""][disabled] {
  display: none;
}
option {
  color: black;
}
</style>