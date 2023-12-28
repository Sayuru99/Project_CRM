import { CreateUserRepository } from "../../repository/createUserRepository";
import { UserModel } from "../model/user";

interface CreateUserUseCase {
  (product: UserModel): Promise<UserModel>;
}

const createUserUseCase =
  (repository: CreateUserRepository): CreateUserUseCase =>
  async (user: UserModel) => {
    // TODO: improve this ifs
    const formData = new FormData();
    if (user.email) formData.append("email", user.email);
    if (user.name) formData.append("name", user.name);
    if (user.password) formData.append("password", user.password);
    if (user.image?.content) formData.append("image.content", user.image.content);

    const created = await repository(formData);
    return created;
  };

export { createUserUseCase, CreateUserUseCase };
