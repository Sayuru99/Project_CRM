import { ActionContext } from "vuex";

import {
  authenticateUseCaseImpl,
  createUserUseCaseImpl,
  logoutUseCaseImpl,
  fetchUserByIDUseCaseImpl,
} from "@/module/di/di";

import { UserModel } from "@/module/user/domain/model/user";
import {
  AuthenticateRequest,
  AuthenticateResponse,
} from "@/module/auth/domain/types";

import app from "./app";

interface UserState {
  token: AuthenticateResponse;
  isSignedIn: boolean;
  current: UserModel;
}

const state: UserState = {
  token: {
    accessToken: "",
    refreshToken: "",
    expiresIn: 0,
    tokenType: "",
  },
  isSignedIn: false,
  current: new UserModel({}),
};

const mutations = {
  setToken(
    state: { token: AuthenticateResponse },
    token: AuthenticateResponse
  ) {
    state.token = token;
  },
  setIsSignedIn(state: { isSignedIn: boolean }, signed: boolean) {
    state.isSignedIn = signed;
  },
  setUser(state: { current: UserModel }, user: UserModel) {
    state.current = user;
  },
};

const getters = {
  getToken(state: { token: AuthenticateResponse }) {
    return state.token;
  },
  getIsSignedIn(state: { isSignedIn: boolean }) {
    return state.isSignedIn;
  },
  getUser(state: { current: UserModel }) {
    return state.current;
  },
};

const actions = {
  isSignedIn(): boolean {
    return state.isSignedIn;
  },
  async signIn(
    context: ActionContext<any, any>,
    request: AuthenticateRequest
  ): Promise<boolean> {
    app.mutations.toggleLoading(app.state);

    try {
      const token = await authenticateUseCaseImpl(request);
      mutations.setToken(context.state, token);
      mutations.setIsSignedIn(context.state, !state.isSignedIn);
      const user = await fetchUserByIDUseCaseImpl(request.email);
      mutations.setUser(context.state, user);
      return true;
    } catch (error: any) {
      app.actions.sendErrorNotice(context, error.response.data.message);
      return false;
    } finally {
      app.mutations.toggleLoading(app.state);
    }
  },
  async signUp(
    context: ActionContext<any, any>,
    request: UserModel
  ): Promise<boolean> {
    app.mutations.toggleLoading(app.state);

    try {
      const { id } = await createUserUseCaseImpl(request);
      mutations.setIsSignedIn(context.state, !state.isSignedIn);

      const user = await fetchUserByIDUseCaseImpl(id);
      mutations.setUser(context.state, user);

      return true;
    } catch (error: any) {
      app.actions.sendErrorNotice(
        context,
        error.response.data.errors
          .map((e: any) => `${e.propertyName} : ${e.errorMessage}`)
          .join(", ")
      );
      return false;
    } finally {
      app.mutations.toggleLoading(app.state);
    }
  },
  async signOut(context: ActionContext<any, any>): Promise<boolean> {
    app.mutations.toggleLoading(app.state);

    try {
      await logoutUseCaseImpl(context.getters.GET_TOKEN);
      mutations.setIsSignedIn(context.state, !state.isSignedIn);
      return true;
    } catch (error: any) {
      app.actions.sendErrorNotice(context, error.response.data.message);
      return false;
    } finally {
      app.mutations.toggleLoading(app.state);
    }
  },
  async getUserByID(
    context: ActionContext<any, any>,
    id: string
  ): Promise<boolean> {
    try {
      const user = await fetchUserByIDUseCaseImpl(id);
      mutations.setUser(context.state, user);
      return true;
    } catch (error: any) {
      app.actions.sendErrorNotice(context, error.response.data.message);
      return false;
    } finally {
      app.mutations.toggleLoading(app.state);
    }
  },
};

export default {
  state,
  mutations,
  actions,
  getters,
};
