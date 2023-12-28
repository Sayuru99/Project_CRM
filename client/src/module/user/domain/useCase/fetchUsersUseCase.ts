import {
  FetchUsersRepository,
  FetchUserByIdRepository,
} from "../../repository/fetchUsersRepository";
import { UserModel, UserPagination } from "../model/user";
import { DataOptions } from "vuetify";
import { TablePagination } from "@/module/pagination/domain/model/pagination";

interface FetchUsersUseCase {
  (options: DataOptions, search: string): Promise<UserPagination>;
}

const fetchUsersUseCase =
  (repository: FetchUsersRepository): FetchUsersUseCase =>
  async (options: DataOptions, search: string) => {
    const pagination = new TablePagination({
      descending: options.sortDesc.join(","),
      sort: options.sortBy.join(","),
      page: options.page,
      itemsPerPage: options.itemsPerPage,
      search: search,
    });

    const userPagination = await repository(pagination);
    return userPagination;
  };

  // TODO: Change this method name. Api finds by id or email
interface FetchUserByIDUseCase {
  (id?: string | null): Promise<UserModel>;
}

const fetchUserByIDUseCase =
  (repository: FetchUserByIdRepository): FetchUserByIDUseCase =>
  async (id?: string | null) => {
    const user = await repository(id);
    return user;
  };

export {
  fetchUsersUseCase,
  FetchUsersUseCase,
  fetchUserByIDUseCase,
  FetchUserByIDUseCase,
};
