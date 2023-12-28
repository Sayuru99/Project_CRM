export interface AppState {
  loading: boolean;
  mode: string;
  snackbar: boolean;
  notice: string;
}

const SUCCESS = "success";
const ERROR = "error";

const state: AppState = {
  loading: false,
  mode: "",
  snackbar: false,
  notice: "",
};

const mutations = {
  toggleLoading(state: AppState) {
    state.loading = !state.loading;
  },
  setNotice(state: AppState, notice: string) {
    state.notice = notice;
  },
  setSnackbar(state: AppState, snackbar: boolean) {
    state.snackbar = snackbar;
  },
  setMode(state: AppState, mode: string) {
    state.mode = mode;
  },
};

const actions = {
  closeNotice(context: any) {
    context.commit("setSnackbar", false);
    context.commit("setNotice", "");
    context.commit("setMode", "");
  },
  closeNoticeWithDelay(context: any, timeout = 3000) {
    setTimeout(() => {
      context.commit("setSnackbar", false);
      context.commit("setNotice", "");
      context.commit("setMode", "");
    }, timeout);
  },
  sendSuccessNotice(context: any, notice: string) {
    context.commit("setNotice", notice);
    context.commit("setMode", SUCCESS);
    context.commit("setSnackbar", true);
    context.dispatch("closeNoticeWithDelay");
  },
  sendErrorNotice(context: any, notice: string) {
    context.commit("setNotice", notice);
    context.commit("setMode", ERROR);
    context.commit("setSnackbar", true);
    context.dispatch("closeNoticeWithDelay");
  },
};

export default {
  state,
  mutations,
  actions,
};
