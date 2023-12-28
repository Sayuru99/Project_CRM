import { AxiosResponse } from "axios";
import { ImageModel } from "./image";

interface UserI {
  id?: string | null;
  name?: string | null;
  email?: string | null;
  password?: string | null;
  isActive?: boolean | null;
  image?: ImageModel;
}

class UserModel {
  id?: string | null;
  name?: string | null;
  email?: string | null;
  password?: string | null;
  isActive?: boolean | null;
  image?: ImageModel;

  constructor(data: UserI) {
    this.id = data.id ?? null;
    this.name = data.name ?? null;
    this.email = data.email ?? null;
    this.isActive = data.isActive ?? null;
    this.password = data.password ?? null;
    this.image = data.image ?? new ImageModel({});
  }
}

class UserPagination {
  page: number;
  pageSize: number;
  items: UserI[];
  count: number;

  constructor(response?: AxiosResponse) {
    this.page = response?.data?.page ?? 0;
    this.pageSize = response?.data?.pageSize ?? 0;
    this.items =
      response?.data?.items?.map((u: UserI) => new UserModel(u)) ?? [];
    this.count = response?.data?.count ?? 0;
  }
}

export { UserModel, UserPagination };
