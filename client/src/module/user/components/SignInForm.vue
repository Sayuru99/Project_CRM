<!-- eslint-disable vue/no-mutating-props -->
<template>
  <v-card elevation="2" class="pa-4">
    <v-form @submit.prevent="controller.signIn()" ref="form">
      <v-text-field
        v-model="controller.form.email"
        label="Email"
        required
      ></v-text-field>
      <v-text-field
        v-model="controller.form.password"
        label="Password"
        :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
        :type="showPassword ? 'text' : 'password'"
        required
        @click:append="togglePasswordVisibility()"
      ></v-text-field>
      <v-btn
        :disabled="loading"
        :loading="loading"
        width="100%"
        color="primary"
        type="submit"
        >Sign in</v-btn
      >
    </v-form>
  </v-card>
</template>

<script lang="ts">
import Vue from "vue";
import store from "@/store";
import { UserController } from "../controller/UserController";

export default Vue.extend({
  name: "SignInForm",
  props: {
    controller: {
      type: UserController,
      required: true,
    },
  },
  data() {
    return {
      showPassword: false,
    };
  },
  computed: {
    loading() {
      return store.state.app.loading;
    },
  },
  methods: {
    togglePasswordVisibility() {
      this.showPassword = !this.showPassword;
    },
  },
});
</script>
