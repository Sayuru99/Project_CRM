<template>
  <v-container>
    <v-row justify="center" class="text-center">
      <v-col cols="12" xl="4">
        Welcome to the CRM system. Coming soon, new features!
      </v-col>
      <v-col cols="10" xl="4" class="text-center">
        <v-btn color="red" @click="controller.signOut()" ref="logout"> Logout </v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
// import { ComponentCustomProperties } from "vue";
import { usuarioController } from "@/module/di/di";

export default {
  name: "DashboardView",
  data: (context: any) => ({
    controller: usuarioController(context),
  }),
};
</script>
