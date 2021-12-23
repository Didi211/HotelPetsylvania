<template>
  <div class="col-xl-12 Obavestenje">
    <h4>{{ obavestenje.sadrzaj }}</h4>
    <div class="row">
      <div class="col-xl-12">
        <button type="button" class="btn btn-danger" @click="izbrisiObavestenje"><font-awesome-icon :icon="['fas', 'times']" />&nbsp;&nbsp;  Izbriši obaveštenje</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AdminObavestenje",
  props: {
    obavestenje: {
      required: true,
      type: Object,
    },
  },
  methods:{
    izbrisiObavestenje(){
          this.$store.dispatch('izbrisiObavestenjeAdmin', this.obavestenje.id);
    }
  }
};
</script>

<style scoped>
.Obavestenje {
  background-color: rgba(255, 255, 255, 0.7);
  /* background-color: rgba(242, 175, 88,0.5); */
  border-radius: 25px;
  /* border: 4px solid rgb(242, 175, 88); */
  border: 4px solid rgb(255, 255, 255);

  margin-top: 20px;
  margin-bottom: 20px;
  /* margin-left: 47px; ovo bi trebalo na lepsi nacin da se resi */

  padding-bottom: 15px;
  padding-top: 15px;
}
</style>