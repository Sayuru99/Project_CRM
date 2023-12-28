<template>
  <v-container>
    <snack-bar></snack-bar>
    <v-row justify="center">
      <v-col cols="10" sm="6" md="6" xl="4">
        <sign-in-form ref="authForm" :controller="controller"></sign-in-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { ComponentCustomProperties } from "vue";
import { usuarioController } from "../../di/di";
import SnackBar from "@/module/shared/components/SnackBar.vue";
import SignInForm from "../components/SignInForm.vue";

export default {
  name: "SignInView",
  components: {
    SnackBar,
    SignInForm,
  },
  data: (context: ComponentCustomProperties) => ({
    controller: usuarioController(context),
  }),
};
</script>
