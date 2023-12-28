<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12">
        <h1 class="text-center">CRM System</h1>
      </v-col>
    </v-row>
    <v-row justify="center">
      <v-col cols="12" sm="3">
        <router-link to="/users/signin">
          <v-btn color="primary" width="100%"> Sign In </v-btn>
        </router-link>
      </v-col>
      <v-col cols="12" sm="3">
        <router-link to="/users/signup">
          <v-btn color="primary" width="100%">Sign Up</v-btn>
        </router-link>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
export default {
  name: "HomeView",
};
</script>
