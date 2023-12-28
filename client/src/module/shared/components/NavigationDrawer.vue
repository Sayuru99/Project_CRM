<template>
  <v-card height="100%" v-if="isSignedIn">
    <v-navigation-drawer
      v-model="drawer"
      :mini-variant.sync="mini"
      permanent
      abosolute
    >
      <v-list-item class="px-2">
        <v-list-item-avatar>
          <v-img :src="photo" alt="User image" />
        </v-list-item-avatar>

        <v-list-item-title>{{ user.name }}</v-list-item-title>

        <v-btn icon @click.stop="mini = !mini">
          <v-icon>mdi-chevron-left</v-icon>
        </v-btn>
      </v-list-item>

      <v-divider></v-divider>

      <v-list dense>
        <v-list-item v-for="item in items" :key="item.title" link>
          <v-list-item-icon>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
  </v-card>
</template>

<script lang="ts">
import store from "@/store";
import { UserModel } from "@/module/user/domain/model/user";

export default {
  name: "NavigationDrawer",
  data() {
    return {
      drawer: true,
      items: [
        { title: "Dashboard", icon: "mdi-view-dashboard-outline" },
        { title: "Order", icon: "mdi-point-of-sale" },
        { title: "Customer", icon: "mdi-account-group-outline" },
        { title: "Product", icon: "mdi-book-multiple-outline" },
      ],
      mini: true,
    };
  },
  computed: {
    isSignedIn(): boolean {
      return store.state.user.isSignedIn;
    },
    user(): UserModel {
      return store.state.user.current;
    },
    photo(): string {
      return `data:image/png;base64, ${store.state.user.current.image.content}`;
    },
  },
};
</script>
