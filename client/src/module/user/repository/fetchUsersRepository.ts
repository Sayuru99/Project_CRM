import { TablePagination } from "@/module/pagination/domain/model/pagination";
import {
  UserModel,
  UserPagination,
} from "@/module/user/domain/model/user";
import { AxiosInstance } from "axios";

interface FetchUsersRepository {
  (pagination: TablePagination): Promise<UserPagination>;
}

const fetchUsersRepository =
  (axios: AxiosInstance): FetchUsersRepository =>
  async (pagination: TablePagination) => {
    const response = await axios.get("/users", {
      params: { page: pagination.page, pageSize: pagination.itemsPerPage },
    });

    return new UserPagination(response);
  };

interface FetchUserByIdRepository {
  (id?: string | null): Promise<UserModel>;
}

const fetchUserByIdRepository =
  (axios: AxiosInstance): FetchUserByIdRepository =>
  async (id?: string | null) => {
    const response = await axios.get(`/users/${id}`);

    return new UserModel(response.data);
  };

export {
  fetchUsersRepository,
  FetchUsersRepository,
  fetchUserByIdRepository,
  FetchUserByIdRepository,
};
