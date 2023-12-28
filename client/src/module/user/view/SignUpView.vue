<template>
  <v-container>
    <snack-bar></snack-bar>
    <v-row justify="center">
      <v-col cols="10" xl="4">
        <sign-up-form ref="userForm" :controller="controller"></sign-up-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { ComponentCustomProperties } from "vue";
import { usuarioController } from "../../di/di";
import SnackBar from "@/module/shared/components/SnackBar.vue";
import SignUpForm from "../components/SignUpForm.vue";

export default {
  name: "SignUpView",
  components: {
    SnackBar,
    SignUpForm,
  },
  data: (context: ComponentCustomProperties) => ({
    controller: usuarioController(context),
  }),
};
</script>
