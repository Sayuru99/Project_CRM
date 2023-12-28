<!-- eslint-disable vue/no-mutating-props -->
<template>
  <v-card tile elevation="2">
    <v-card-title>
      <h1>New user</h1>
    </v-card-title>
    <v-card-text>
      <v-form @submit.prevent="controller.signUp()" ref="form">
        <v-text-field
          label="Name"
          v-model="controller.user.name"
          :rules="[isRequired]"
        />
        <v-text-field
          label="Email"
          v-model="controller.user.email"
          type="email"
          :rules="[isRequired]"
        />
        <v-text-field
          label="Password"
          v-model="controller.user.password"
          :rules="[isRequired]"
        />
        <v-file-input
          label="Photo"
          accept="image/*"
          prepend-icon="mdi-camera"
          v-model="controller.user.image.content"
        ></v-file-input>
        <v-btn
          :disabled="loading"
          :loading="loading"
          color="primary"
          type="submit"
          >Sign Up</v-btn
        >
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import Vue from "vue";
import store from "@/store";
import { UserController } from "../controller/UserController";

export default Vue.extend({
  name: "SignUpForm",
  props: {
    controller: {
      type: UserController,
      required: true,
    },
  },
  computed: {
    loading() {
      return store.state.app.loading;
    },
  },
  methods: {
    isRequired(value: string) {
      return !!value || "Required";
    },
  },
});
</script>
