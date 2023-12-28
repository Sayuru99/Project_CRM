import { AxiosInstance } from "axios";

import { UserModel } from "../domain/model/user";

interface CreateUserRepository {
  (form: FormData): Promise<UserModel>;
}

const createUserRepository =
  (axios: AxiosInstance): CreateUserRepository =>
  async (form: FormData) => {
    // TODO: improve this implementation
    const response = await axios.post("/users", form, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return new UserModel(response?.data);
  };

export { createUserRepository, CreateUserRepository };
