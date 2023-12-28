<template>
  <v-snackbar
    :top="true"
    :right="true"
    :timeout="3000"
    :color="mode"
    v-model="snackbar"
  >
    <v-row justify="space-around" align="center">
      {{ notice }}
      <v-btn text @click.native="closeSnackbar">
        <v-icon small>mdi-close</v-icon>
      </v-btn>
    </v-row>
  </v-snackbar>
</template>

<script lang="ts">
import store from "@/store";

export default {
  name: "SnackBar",
  computed: {
    mode() {
      return store.state.app.mode;
    },
    snackbar() {
      return store.state.app.snackbar;
    },
    notice() {
      return store.state.app.notice;
    },
  },
  methods: {
    async closeSnackbar() {
      await store.dispatch("closeNotice");
    },
  },
};
</script>
