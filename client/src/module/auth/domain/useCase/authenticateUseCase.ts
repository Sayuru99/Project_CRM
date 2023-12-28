import { AuthenticateRepository } from "../../repository/authenticateRepository";
import { AuthenticateRequest, AuthenticateResponse } from "../types";

interface AuthenticateUseCase {
  (auth: AuthenticateRequest): Promise<AuthenticateResponse>;
}

const authenticateUseCase =
  (repository: AuthenticateRepository): AuthenticateUseCase =>
  async (auth: AuthenticateRequest) => {
    return await repository(auth);
  };

export { authenticateUseCase, AuthenticateUseCase };
