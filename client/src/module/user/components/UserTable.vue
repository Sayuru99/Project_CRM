<!-- eslint-disable vue/no-mutating-props -->
<template>
  <v-card>
    <v-card-title>
      Users
      <v-spacer />
      <v-btn color="primary" @click="controller.formDialog = true">
        <v-icon left>mdi-plus</v-icon>new
      </v-btn>
    </v-card-title>
    <v-card-text class="pa-0">
      <v-data-table
        dense
        class="elevation-1"
        :items="controller.userPagination.items"
        :headers="controller.headers"
        @pagination="controller.paginate()"
        :options.sync="controller.options"
        :server-items-length="controller.userPagination.count"
      ></v-data-table>
    </v-card-text>
  </v-card>
</template>

<script>
import { UserController } from "../controller/UserController";

export default {
  props: {
    controller: {
      type: UserController,
      required: true,
    },
  },
};
</script>
