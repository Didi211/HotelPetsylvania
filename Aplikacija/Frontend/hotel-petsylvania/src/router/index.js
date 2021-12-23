import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store/data.js'

import PosetilacHomePage from '@/pages/PosetilacHomePage.vue'
import AdminHomePage from '@/pages/AdminHomePage.vue'
import AdminListaRadnika from '@/pages/AdminListaRadnika.vue'
import AdminObavestenja from '@/pages/AdminListaObavestenja.vue'
import Cenovnik from '@/pages/Cenovnik.vue'
import Login from '@/pages/Login.vue'
import MojProfil from '@/pages/MojProfil.vue'
import MusterijaHomePage from '@/pages/MusterijaHomePage.vue'
import OceneIRecenzije from '@/pages/OceneIRecenzije.vue'
import OdsekZaPitanja from '@/pages/OdsekZaPitanja.vue'
import RadnikFiltriranjeZahteva from '@/pages/RadnikFiltriranjeZahteva.vue'
import RadnikHomePage from '@/pages/RadnikHomePage.vue'
import Register from '@/pages/Register.vue'
import PageNotAuthenticated from '@/pages/NotAuthenticated.vue'
import PageNotFound from '@/pages/PageNotFound.vue'

Vue.use(Router)

const router = new Router({
    routes:[
        {
            path:'/',
            name: 'PosetilacHomePage',
            component: PosetilacHomePage,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip === 'A'){
                    next({name: 'AdminHomePage'});
                }
                else if(tip === 'M'){
                    next({name: 'MusterijaHomePage'})
                }
                else if(tip === 'R'){
                    next({name: 'RadnikHomePage'})
                }
                else{
                    next();
                }
            }
        },
        {
            path:'/AdminHomePage',
            name: 'AdminHomePage',
            component: AdminHomePage,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip == 'A'){
                    next();
                }
                else{
                    next({name: 'PageNotAuthenticated'})
                }
            }
        },
        {
            path:'/AdminListaRadnika',
            name: 'AdminListaRadnika',
            component: AdminListaRadnika,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip == 'A'){
                    next();
                }
                else{
                    next({name: 'PageNotAuthenticated'})
                }
            }
        },
        {
            path:'/AdminObavestenja',
            name: 'AdminObavestenja',
            component: AdminObavestenja,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip == 'A'){
                    next();
                }
                else{
                    next({name: 'PageNotAuthenticated'})
                }
            }
        },
        {
            path:'/Cenovnik',
            name: 'Cenovnik',
            component: Cenovnik,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip == 'R' || tip == 'A'){
                    next({name: 'PageNotAuthenticated'})
                }
                else{
                    next();
                }
            }
        },
        {
            path:'/Login',
            name: 'Login',
            component: Login,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip === 'A'){
                    next({name: 'AdminHomePage'});
                }
                else if(tip === 'M'){
                    next({name: 'MusterijaHomePage'})
                }
                else if(tip === 'R'){
                    next({name: 'RadnikHomePage'})
                }
                else{
                    next();
                }
            }
        },
        {
            path:'/MojProfil',
            name: 'MojProfil',
            component: MojProfil,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip == 'M' || tip == 'R'){
                    next();
                }
                else{
                    next({name: 'PageNotAuthenticated'})
                }
            }
        },
        {
            path:'/MusterijaHomePage',
            name: 'MusterijaHomePage',
            component: MusterijaHomePage,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip == 'M'){
                    next();
                }
                else{
                    next({name: 'PageNotAuthenticated'})
                }
            }
        },
        {
            path:'/OceneIRecenzije',
            name: 'OceneIRecenzije',
            component: OceneIRecenzije,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip == 'R' || tip == 'A'){
                    next({name: 'PageNotAuthenticated'})
                }
                else{
                    next();
                }
            }
        },
        {
            path:'/OdsekZaPitanja',
            name: 'OdsekZaPitanja',
            component: OdsekZaPitanja,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip == 'A'){
                    next({name: 'PageNotAuthenticated'})
                }
                else{
                    next();
                }
            }
        },
        {
            path:'/RadnikFiltriranjeZahteva',
            name: 'RadnikFiltriranjeZahteva',
            component: RadnikFiltriranjeZahteva,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip == 'R'){
                    next();
                }
                else{
                    next({name: 'PageNotAuthenticated'})
                }
            }
        },
        {
            path:'/RadnikHomePage',
            name: 'RadnikHomePage',
            component: RadnikHomePage,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip == 'R'){
                    next();
                }
                else{
                    next({name: 'PageNotAuthenticated'})
                }
            }
        },
        {
            path:'/Register',
            name: 'Register',
            component: Register,
            beforeEnter(to, from, next){
                let tip = Vue.$cookies.get("tip")
                if(tip === 'A'){
                    next({name: 'AdminHomePage'});
                }
                else if(tip === 'M'){
                    next({name: 'MusterijaHomePage'})
                }
                else if(tip === 'R'){
                    next({name: 'RadnikHomePage'})
                }
                else{
                    next();
                }
            }
        },
        {
            path: '/401',
            name: 'PageNotAuthenticated',
            component: PageNotAuthenticated
        },
        {
            path: '*',
            name: 'PageNotFound',
            component: PageNotFound
        }
    ],
    mode: 'history'
})

export default router