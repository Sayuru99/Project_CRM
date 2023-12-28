<template>
  <v-app id="app">
    <v-main id="container">
      <v-layout fluid fill-height align-center justify-center>
        <navigation-drawer></navigation-drawer>
        <router-view />
      </v-layout>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import NavigationDrawer from "./module/shared/components/NavigationDrawer.vue";

export default Vue.extend({
  name: "App",
  components: { NavigationDrawer },
});
</script>
