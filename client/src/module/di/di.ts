import axios from "axios";

import store from "@/store";

import { fetchUsersRepository, fetchUserByIdRepository } from "../user/repository/fetchUsersRepository";
import { fetchUsersUseCase, fetchUserByIDUseCase } from "../user/domain/useCase/fetchUsersUseCase";
import { createUserRepository } from "../user/repository/createUserRepository";
import { createUserUseCase } from "../user/domain/useCase/createUserUseCase";
import { UserController } from "../user/controller/UserController";

import { authenticateRepository } from "../auth/repository/authenticateRepository";
import { authenticateUseCase } from "../auth/domain/useCase/authenticateUseCase";
import { logoutUseCase } from "../auth/domain/useCase/logoutUseCase";
import { logoutRepository } from "../auth/repository/logoutRepository";

axios.defaults.headers.common["Accept"] = "*/*";
axios.defaults.headers.common["Content-Type"] = "application/json;charset=UTF-8";
axios.defaults.baseURL = "http://localhost:5166/api";
const axiosInstance = axios.create();

axiosInstance.interceptors.response.use(
  (response) => response,
  async (err) => {
    const status = err.response ? err.response.status : null;

    if (status === 500) {
      // TODO: something here or on any status code return
    }

    return Promise.reject(err);
  }
);

const authenticateUseCaseImpl = authenticateUseCase(
  authenticateRepository(axiosInstance)
);

const logoutUseCaseImpl = logoutUseCase(logoutRepository(axiosInstance));

const fetchUsersUseCaseImpl = fetchUsersUseCase(
  fetchUsersRepository(axiosInstance)
);

const fetchUserByIDUseCaseImpl = fetchUserByIDUseCase(
  fetchUserByIdRepository(axiosInstance)
);
const createUserUseCaseImpl = createUserUseCase(
  createUserRepository(axiosInstance)
);

const usuarioController = (context: any) => {
  return new UserController(context, store, fetchUsersUseCaseImpl);
};

export {
  usuarioController,
  authenticateUseCaseImpl,
  createUserUseCaseImpl,
  logoutUseCaseImpl,
  fetchUserByIDUseCaseImpl
};
