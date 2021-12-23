import Vue from 'vue'
import App from './App.vue'
import router from './router/index.js'
import store from './store/data'
import Axios from 'axios'
import VueCookies from 'vue-cookies';
import Toasted from 'vue-toasted'
import VModal from 'vue-js-modal'
import VueSlickCarousel from 'vue-slick-carousel'
import moment from 'moment'
import VueDraggable from 'vuedraggable'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faCheck, faCopyright, faInfo, faRemoveFormat  } from '@fortawesome/free-solid-svg-icons'
import { faChevronUp  } from '@fortawesome/free-solid-svg-icons'
import { faPlusCircle } from '@fortawesome/free-solid-svg-icons'
import { faTimes } from '@fortawesome/free-solid-svg-icons'
import { faEdit } from '@fortawesome/free-solid-svg-icons'
import { faLock } from '@fortawesome/free-solid-svg-icons'
import { faLockOpen } from '@fortawesome/free-solid-svg-icons'
import { faShare } from '@fortawesome/free-solid-svg-icons'
import { faRedo } from '@fortawesome/free-solid-svg-icons'
import { faSearch } from '@fortawesome/free-solid-svg-icons'
import { faArrowAltCircleLeft } from '@fortawesome/free-solid-svg-icons'
import { faSave } from '@fortawesome/free-solid-svg-icons'
import { faArrowCircleUp } from '@fortawesome/free-solid-svg-icons'
import { faCog } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faFontAwesome } from '@fortawesome/free-brands-svg-icons'
import AppSpinner from './components/AppSpinner'
import vuelidate from 'vuelidate'
import StarRating from 'vue-star-rating'

library.add(faCheck )
library.add(faChevronUp)
library.add(faFontAwesome)
library.add(faPlusCircle)
library.add(faTimes)
library.add(faEdit)
library.add(faLock)
library.add(faLockOpen)
library.add(faShare)
library.add(faRedo)
library.add(faSearch)
library.add(faArrowAltCircleLeft)
library.add(faSave)
library.add(faArrowCircleUp)
library.add(faCog)
library.add(faInfo)
library.add(faCopyright)

Vue.component('font-awesome-icon', FontAwesomeIcon)

Vue.config.productionTip = false;
//The process is a global Node.js variable through which we can access our environment variables.
Axios.defaults.baseURL = process.env.API_ENDPOINT;

Vue.component('AppSpinner', AppSpinner);
Vue.component('VueSlickCarousel',VueSlickCarousel);
Vue.component('VueDraggable', VueDraggable);
Vue.component('StarRating', StarRating)
Vue.use(VueCookies);
Vue.use(Toasted);
Vue.use(VModal);
Vue.use(vuelidate)

// Vue.use(VueSlickCarousel);

Vue.filter('formatirajDatume', function(value){
  if(!value) return '/'
  return moment(value).format('LL')
})

Vue.filter('formatirajVreme', function(value){
  if(!value) return '/'
  return moment(value).format('LLL')
})

Vue.filter('formatirajTermin', function(value){
  if(!value) return '/'
  return moment(value).format('LT')
})

new Vue({
  router,
  store,
  vuelidate,
  render: h => h(App),
}).$mount('#app')
