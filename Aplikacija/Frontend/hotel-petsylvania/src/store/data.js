import Vue from 'vue'
import Vuex from 'vuex'
import Api from '@/api-services/proba.js'
import router from "@/router"
import cookies from 'vue-cookies'
import { faWindows } from '@fortawesome/free-brands-svg-icons'


Vue.use(Vuex)

export default new Vuex.Store({
    //cuvaju se podaci i dele se medju svim komponentama
    state:{
        // korisnici: [],
        // musterije: [],
        radnici:[],
        uslugeAdmin:[],
        uslugeZaRezervaciju:[],
        uslugeDeaktiviraneAdmin:[],
        zahteviCuvanjeRadnik:[],
        zahteviNeobradjeniRadnik:[],
        zahteviUObradiRadnik:[],
        zahteviObradjeniRadnik:[],
        zahteviMusterije:[],
        sviZahteviFiltriranje:[],
        obavestenja:[],
        notifikacije:[], //zasto imamo dve notifikacije?
        notifikacijeMusterije:[],
        cenovnik: [],
        trenutniKorisnik: null,
        osobaID: null,
        token: null,
        tip: null,
        musterijaProfil: null,
        radnikProfil: null,
        sviTipoviUsluga:[],
        sviTipoviZivotinja:[],
        pitanja:[],
        recenzije:[],
        
        // trenutniKorisnik: {
        //     userID: '',
        //     username: '',
        //     token: '',
        //     tip: '',
        //     osobaID: ''
        // },
        //statistika????


    },
    //computed properties za store - jednostavne funkcije koje se koriste da se dobiju podaci iz store
    getters:{
        authKorisnik(state){
            return state.trenutniKorisnik || null
        },
        isAuthenticated(state){
            return !!state.user
        },
        getTip(state){
            return state.tip
        },
        getUslugeAdmin(state){
            return state.uslugeAdmin
        },
        getObavestenjaMusterija(state){
            return state.obavestenja
        },
        getosobaid(state){
            return state.osobaID
        },
        getOsobaID(state){
            return state.trenutniKorisnik.osobaID;
        },
        getZahteveMusterije(state){
            return state.zahteviMusterije;
        },
        getObavestenjaAdmin(state){
            return state.obavestenja
        },
        getRadnikeAdmin(state){
            return state.radnici
        },
        isAdmin(state){
            return state.isAdmin
        },
        getMusterijaProfil(state){
            return state.musterijaProfil
        },
        getRadnikProfil(state){
            return state.radnikProfil
        },
        getZahteviCuvanjeRadnik(state){
            return state.zahteviCuvanjeRadnik
        },
        getZahteviNeobradjeniRadnik(state){
            return state.zahteviNeobradjeniRadnik
        },
        getZahteviUObradiRadnik(state){
            return state.zahteviUObradiRadnik
        },
        getZahteviObradjeniRadnik(state){
            return state.zahteviObradjeniRadnik
        },
        getSviZahteviFiltriranje(state){
            return state.sviZahteviFiltriranje
        },
        getSviTipoviUsluga(state){
            return state.sviTipoviUsluga
        },
        getUslugeDeaktiviraneAdmin(state){
            return state.uslugeDeaktiviraneAdmin
        },
        getSviTipoviZivotinja(state){
            return state.sviTipoviZivotinja
        },
        getPitanja(state){
            return state.pitanja
        },
        getUslugeZaRezervaciju(state){
            return state.uslugeZaRezervaciju
        },
        getterNotifikacijeMusterije(state){
            return state.notifikacijeMusterije
        },
        getRecenzije(state){
            return state.recenzije
        }
        // getTipKorisnika(state){
        //     if(state.trenutniKorisnik == null)
        //     {
        //         return null
        //     }
        //     else
        //     {
        //         return state.trenutniKorisnik.tip
        //     }
        // }
    },
    //methods za store - koriste se za asinhrone dogadjaje, npr pribavljanje podataka, valjda ovde ne bi trebalo da se azuriraju podaci, Sluze samo za pozivanje mutacija i sinhronizaciju baze sa mutacijama
    actions:{
        async loadCene({commit}){
            let response = await Api().get('/Hotel/CeneUsluga');
            commit('SET_CENOVNIK',response.data);
        },
        async registerKorisnik({commit}, registerInfo){
            return await Api().post('/Hotel/RegistrujSe', registerInfo).then((res)=>{
                const trenutniKorisnik = res.data
                commit('setKorisnik', trenutniKorisnik)
                Vue.$cookies.set("id",this.state.trenutniKorisnik.userID,"1h");
                Vue.$cookies.set("token",this.state.trenutniKorisnik.token,"1h");
                Vue.$cookies.set("tip",this.state.trenutniKorisnik.tip,"1h");
                commit('setToken', Vue.$cookies.get("token"))
                commit('setTip', Vue.$cookies.get("tip"))
                router.push("/MusterijaHomePage")
            }).catch((err)=>{
                Vue.toasted.show("Već postoji korisnik sa tim username-om!", { 
                    theme: "bubble", 
                    position: "top-center", 
                    duration : 2000
               })
            })
        },
        async loginKorisnik({commit},loginInfo){
            await Api().post('/Hotel/Login', loginInfo).then(res=>{
                const trenutniKorisnik = res.data
                commit('setKorisnik', trenutniKorisnik)
                Vue.$cookies.set("id",this.state.trenutniKorisnik.userID,"1h");
                Vue.$cookies.set("token",this.state.trenutniKorisnik.token,"1h");
                Vue.$cookies.set("tip",this.state.trenutniKorisnik.tip,"1h");
                commit('setToken', Vue.$cookies.get("token"))
                commit('setTip', Vue.$cookies.get("tip"))
                // commit('setOsobaID',this.state.trenutniKorisnik.userID)
                if(this.state.trenutniKorisnik.tip == "A")
                {
                    router.push("/AdminHomePage")
                }
                else if(this.state.trenutniKorisnik.tip == "M")
                {
                    router.push("/MusterijaHomePage")
                }
                else if(this.state.trenutniKorisnik.tip == "R")
                {
                    router.push("/RadnikHomePage")
                }
            }).catch((err)=>{
                if(err.response.status==403){
                    Vue.toasted.show("Zabranjen vam je pristup!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                }
                else{
                    Vue.toasted.show("Pogrešno uneseni podaci, pokušajte ponovo!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                }
                
            })
        },
        logoutKorisnik({commit}){
                commit('setKorisnik', null)
                commit('setToken', null);
                commit('setTip', null)
                Vue.$cookies.remove("id");
                Vue.$cookies.remove("token");
                Vue.$cookies.remove("tip");
                // commit('setToken', null);
                // commit('setTip', null)
            //commit('LOGOUT_KORISNIK')
        },

        postaviToken({commit}, tok){
            commit('setToken', tok)
        },
        postaviTip({commit}, tip){
            commit('setTip', tip)
        },
        async postaviOsobaID({commit}, id){
            await Api().get('/Hotel/GetSpecificID/'+ id).then(res=>{
                let osid= res.data
                commit('setOsobaID',osid)
            })
        },

        async getKorisnikById({commit}, id){
            await Api().get('/Hotel/GetUserByID/'+id).then(res=>{
                const trenutniKorisnik = res.data
                commit('setKorisnik', trenutniKorisnik)
                // debugger
                // const osid= trenutniKorisnik.osobaID
                // commit('setOsobaID', osid)
            }).catch(function (error) {
                // console.log("Nema ulogovanog korisnika");
           })

        },
        getUslugeAdmin({commit}){
            return Api().get('/Admin/PrikaziUsluge',{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                const usluge = res.data
                commit('setUslugeAdmin', usluge)
            })
        },
        getObavestenjaMusterija({commit}){
            return Api().get('/Hotel/Obavestenja').then(res=>{
                const obav=res.data
                commit('setObavestenjeMusterija', obav)
            })
        },
        async getZahteveMusterija({commit, dispatch}){
            // let id = this.getters['getOsobaId'];
            return await Api().get('/Musterija/Zahtevi/'+ this.state.osobaID,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
                }).then(res=>{
                const zah= res.data;
                commit('setZahteveMusterija', zah)
            })


            // return dispatch('postaviOsobaID', Vue.$cookies.get("id")).then(()=>{
            //     Api().get('/Musterija/Zahtevi/'+ this.state.osobaID,{
            //         headers: {
            //           'Authorization': `Basic ${this.state.token}`
            //         }
            //         }).then(res=>{
            //         const zah= res.data;
            //         commit('setZahteveMusterija', zah)
            //     })
            // })
        },
        obrisiRezervacijuMusterija({commit, dispatch}, id){
            Api().delete('/Musterija/ObrisiZahtev/'+id,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{ 
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste izbrisali obaveštenje!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getZahteveMusterija')
                }
                else{
                    Vue.toasted.show("Došlo je do greške, pokušajte ponovo"),{
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                    }
                }
            })

        },
        async getMusterijaProfil({commit, dispatch}){
            // dispatch('postaviOsobaID', this.state.trenutniKorisnik.userID)
            await Api().get('/Musterija/Profil/'+ this.state.osobaID,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                const profilInfo = res.data
                commit('setMusterijaProfil', profilInfo)
            })
        },
        async getRadnikProfil({commit}){
            await Api().get('/Radnik/Profil/'+ this.state.osobaID,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                const profilInfo = res.data
                commit('setRadnikProfil', profilInfo)
            })
        },
        async AzurirajProfilMusterije({commit, dispatch}, mInfo){
            let t = Vue.$cookies.get("token")
            await Api().put('/Musterija/AzurirajProfil/'+ this.state.osobaID, mInfo,{
                headers: {
                  'Authorization': `Basic ${t}`
                }
            }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Ažurirali ste podatke profila!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getMusterijaProfil')
                   window.location.reload();
                }
                else{
                    // console.log(res.data)
                }

            }).catch(function (error) {
                // console.log(res.statusText);
           })
        },
        async AzurirajProfilRadnika({commit, dispatch}, rInfo){
            await Api().put('/Radnik/AzurirajProfil/'+ this.state.osobaID, rInfo,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
            }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Ažurirali ste podatke profila!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getRadnikProfil')
                   window.location.reload();
                }
                // else{
                //     console.log(res.data)
                // }

            }).catch(({response})=>{
                Vue.toasted.show(response.data, { 
                    theme: "bubble", 
                    position: "top-center", 
                    duration : 2000
               })

            }
                )
        },
        async getUslugeZaRezervaciju({commit}){
            let t = Vue.$cookies.get("token")
            await Api().get('/Musterija/PrikaziUsluge',{
                headers: {
                  'Authorization': `Basic ${t}`
                }
            }).then(res=>{
                const usluge = res.data;
                commit('setUslugeZaReservaciju', usluge)
            })

        },
        async getNotifikacijeMusterije({commit}){
            let t = Vue.$cookies.get("token")
            await Api().get('/Musterija/Obavestenja/'+this.state.osobaID, {
                headers: {
                  'Authorization': `Basic ${t}`
                }
            }).then(res=>{
                const notif= res.data
                commit('setNotifikacijeMusterije', notif)
            }).catch(function (error) {
                // console.log("nije usao u api fju getNotifMusterije");
           })
        },
        async napraviNovuRezervaciju({commit, dispatch}, payload){
            // debugger
            let t = Vue.$cookies.get("token")
            await Api().post('/Musterija/KreirajZahtev',payload, {
                headers: {
                  'Authorization': `Basic ${t}`
                }
            }).then((res)=>{
                if(res.status == 200){
                    Vue.toasted.show("Uspesno ste napravili novu rezervaciju!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getZahteveMusterija')
                //    window.location.reload();
                }
                // else if(res.status == 400){
                //     Vue.toasted.show(res.status, { 
                //         theme: "bubble", 
                //         position: "top-center", 
                //         duration : 2000
                //    })
                // }
            }).catch(({response})=>{
                Vue.toasted.show(response.data, { 
                    theme: "bubble", 
                    position: "top-center", 
                    duration : 2000
               })

            }
                )
        },
        izmeniRezervacijuMusterija({commit, dispatch}, {rezBr, rez}){
            let t = Vue.$cookies.get("token")
            Api().put('/Musterija/AzurirajZahtev/'+rezBr, rez, {
                headers: {
                  'Authorization': `Basic ${t}`
                }
            }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspesno ste azurirali vasu rezervaciju!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getZahteveMusterija')
                   window.location.reload();
                }
            }).catch(({response})=>{
                Vue.toasted.show(response.data, { 
                    theme: "bubble", 
                    position: "top-center", 
                    duration : 2000
               })

            })

        },
        // getAuthKorisnik({commit}){
        //     return Api().get("").then((res)=>{
        //         const korisnik = res.data
        //         commit('setKorisnik', korisnik)
        //         return korisnik
        //     }).catch(err =>{
        //         commit('setKorisnik', null)
        //         return undefined
        //     })
        // }
        getObavestenjaAdmin({commit}){
            return Api().get('/Admin/PrikaziObavestenja',{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                const obavestenja = res.data
                commit('setObavestenjaAdmin', obavestenja)
            })
        },
        getRadnikeAdmin({commit}){
            return Api().get('/Admin/PrikaziRadnike',{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                const radnici = res.data
                commit('setRadnike', radnici)
            })
        },
        getZahteviCuvanjeRadnik({commit}, id){
            return Api().get('/Radnik/ZahteviUsluge/'+id,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                const zahteviCuvanjeRadnik = res.data
                commit('setZahteviCuvanjeRadnik', zahteviCuvanjeRadnik)
            })
        },
        izbrisiObavestenjeAdmin({commit, dispatch}, id){
            Api().delete('/Admin/ObrisiObavestenje/'+id,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{ 
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste izbrisali obaveštenje!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getObavestenjaAdmin')
                }
            })
        },
        dodajObavestenjeAdmin({commit}, ob){
            Api().post('/Admin/PostaviObavestenje', ob,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                  
                if(res.status == 204){
                    Vue.toasted.show("Postavili ste novo obaveštenje!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                }
            })
        },
        dodajUsluguAdmin({commit, dispatch}, usl){
            Api().post('/Admin/DodajUslugu', usl,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Dodali ste novu uslugu!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getUslugeAdmin')
                }
            }).catch(()=>{
                Vue.toasted.show("Usluga sa istim imenom već postoji!", { 
                    theme: "bubble", 
                    position: "top-center", 
                    duration : 2000
               })
            })
        },
        izbrisiUsluguAdmin({commit, dispatch}, id){
            Api().delete('/Admin/ObrisiUslugu/'+id,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{ 
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste izbrisali uslugu!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getUslugeAdmin')
                }
            }).catch(err=>{
                Vue.toasted.show("Uslugu možete samo arhivirati jer je korišćena!", { 
                    theme: "bubble", 
                    position: "top-center", 
                    duration : 2000
               })
            })
        },
        dodajRadnikaAdmin({commit}, rad){
            Api().post('/Admin/DodajRadnika', rad,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste dodali novog radnika!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                }
                else if(res.status == 400){
                    Vue.toasted.show("Korisnik sa tim username-om već postoji!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                }
            }).catch(err=>
                {
                    // console.log(err);
                })
        },
        izmeniKapacitetAdmin({commit, dispatch}, {id, novaVr}){
            return Api().put('/Admin/IzmeniKapacitet/'+id,novaVr,{
                headers:{
                    'Authorization': `Basic ${this.state.token}`
                }
            }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste izmenili kapacitet usluge!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                dispatch('getUslugeAdmin');
                }
            })
        },
        izmeniCenuUslugeAdmin({commit,dispatch}, {id, nCena}){
            return Api().put('/Admin/IzmeniCenu/'+id,nCena,{
                headers:{
                    'Authorization': `Basic ${this.state.token}`
                }
            }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste izmenili cenu usluge!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getUslugeAdmin')
                }
            })
            // .catch(err=>{
            //     console.log(err.status);
            // })
        },
        deaktivirajUsluguAdmin({commit, dispatch}, id){
            let t = Vue.$cookies.get("token")
            return Api().put('/Admin/DeaktivirajUslugu',id,{
                headers:{
                    'Authorization': `Basic ${this.state.token}`
                }
            }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste deaktivirali uslugu!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getUslugeAdmin')
                }
            })
        },
        aktivirajUsluguAdmin({commit, dispatch}, id){
            return Api().put('/Admin/AktivirajUslugu',id,{
                headers:{
                    'Authorization': `Basic ${this.state.token}`
                }
            }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste aktivirali uslugu!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getUslugeAdmin')
                }
            })
        },
        otpustiRadnikaAdmin({commit, dispatch}, id){
            Api().delete('/Admin/OtpustiRadnika/'+id,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{ 
                if(res.status == 200){

                    Vue.toasted.show("Uspešno ste otpustili radnika!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getRadnikeAdmin')
                }
            })
        },
        async getPitanja({commit}){
            await Api().get('/Hotel/Pitanja').then(res=>{
                if(res.status == 200){
                    const pitanja = res.data
                    commit('setPitanja', pitanja)
                }
            })
        },
        postaviPitanjeMusterija({commit, dispatch}, obj){
            Api().post('/Musterija/PostaviPitanje', obj,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste postavili pitanje!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getPitanja')
                }
            })
        },
        odgovoriNaPitanjeRadnik({commit, dispatch}, obj){
            Api().put('/Radnik/OdgovoriNaPitanje', obj,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste odgovorili na pitanje!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getPitanja')
                }
            })
        },
        obavestiMusteriju({commit}, {rez, kom}){
            Api().post('/Radnik/ObavestiMusteriju/'+rez, kom,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Poslali ste obaveštenje mušteriji!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                }
            })
        },
        async getRecenzije({commit}){
            await Api().get('/Hotel/Recenzije').then(res=>{
                if(res.status == 200){
                    const recenzije = res.data
                    commit('setRecenzije', recenzije)
                }
            })
        },
        postaviRecenzijuMusterija({commit, dispatch}, obj){
            Api().post('/Musterija/KreirajRecenziju', obj,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste postavili svoju recenziju!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                   dispatch('getRecenzije')
                }
            })
        },
        postaviUslugaZaObradu({commit, dispatch}, obj){
            Api().put('/Radnik/IzaberiUsluguZaObradu', obj,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste izabrali zahtev koji želite da obradite!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                }
                // dispatch('getZahteviCuvanjeRadnik')
            })
        },
        postaviUslugaObradjena({commit, dispatch}, obj){
            Api().put('/Radnik/UslugaZavrsena', obj,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste obradili zahtev!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                }
                // dispatch('getZahteviCuvanjeRadnik')
            })
        },
        promeniSifruRadnika({commit, dispatch}, {idRadnika, sifraObj}){
            Api().put('/Radnik/PromeniSifru/'+idRadnika, sifraObj,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste promenili šifru!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                }
            }).catch(()=>
                {
                    Vue.toasted.show("Pogrešna stara šifra!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                })
        },
        promeniSifruMusterije({commit, dispatch}, {idMusterije, sifraObj}){
            Api().put('/Musterija/PromeniSifru/'+idMusterije, sifraObj,{
                headers: {
                  'Authorization': `Basic ${this.state.token}`
                }
              }).then(res=>{
                if(res.status == 204){
                    Vue.toasted.show("Uspešno ste promenili šifru!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                }
            }).catch(()=>
                {
                    Vue.toasted.show("Pogrešna stara šifra!", { 
                        theme: "bubble", 
                        position: "top-center", 
                        duration : 2000
                   })
                })
        }
    },
    //jednostavne fje koje menjaju podatke ?? Da, mutacijama menjamo podatke na stranicama
    mutations:{
        SET_CENOVNIK(state, cenovnik){
            state.cenovnik = cenovnik;  
        },
        setKorisnik(state, korisnik){
            state.trenutniKorisnik = korisnik;
        },
        setUslugeAdmin(state, usluge){
            // state.uslugeAdmin = usluge
            state.uslugeAdmin = []
            state.uslugeDeaktiviraneAdmin = []
            usluge.forEach(element => {
                if(element.dostupnost=='1'){
                    state.uslugeAdmin.push(element)
                }
                else if(element.dostupnost=='0'){
                    state.uslugeDeaktiviraneAdmin.push(element)
                }
            });
        },
        setObavestenjeMusterija(state, obav){
            state.obavestenja =obav
        },
        setZahteveMusterija(state, zah){
            state.zahteviMusterije=zah
        },
        setToken(state, token){
            state.token = token;
        },
        setOsobaID(state, osobaid){
            state.osobaID = osobaid
        },
        setTip(state, tip){
            state.tip = tip;
        },
        setObavestenjaAdmin(state, obavestenja){
            state.obavestenja = obavestenja;
        },
        setRadnike(state, radnici){
            state.radnici = radnici;
        },
        setMusterijaProfil(state, profilinfo){
            state.musterijaProfil=profilinfo
        },
        setRadnikProfil(state, profilinfo){
            state.radnikProfil=profilinfo
        },
        setZahteviCuvanjeRadnik(state, glavnaLista){
            state.zahteviCuvanjeRadnik = glavnaLista[0]
            state.zahteviNeobradjeniRadnik = []
            state.sviZahteviFiltriranje = []
            state.zahteviUObradiRadnik = []
            state.zahteviObradjeniRadnik = []
            glavnaLista[1].forEach(element => {
                // state.zahteviNeobradjeniRadnik.push(element)
                if(element.obradjen=="NEOBRADJEN"){
                    state.zahteviNeobradjeniRadnik.push(element)
                }
                else if(element.obradjen=="U_OBRADI"){
                    state.zahteviUObradiRadnik.push(element)
                }
                else if(element.obradjen=="OBRADJEN"){
                    state.zahteviObradjeniRadnik.push(element)
                }
            });
            state.sviZahteviFiltriranje = glavnaLista[0].concat(glavnaLista[1])
            state.sviZahteviFiltriranje.forEach(element => {
                if(element.tipZivotinje !=null & !state.sviTipoviZivotinja.includes(element.tipZivotinje))
                state.sviTipoviZivotinja.push(element.tipZivotinje)
                if(!state.sviTipoviUsluga.includes(element.nazivUsluge))
                state.sviTipoviUsluga.push(element.nazivUsluge)
            });
        },
        setPitanja(state, pitanja){
            state.pitanja = pitanja
        },
        setUslugeZaReservaciju(state, usluge){
            state.uslugeZaRezervaciju=usluge
        },
        setNotifikacijeMusterije(state, notif){
            state.notifikacijeMusterije=notif
        },
        setRecenzije(state, recenzije){
            state.recenzije = recenzije
        }
    }
})