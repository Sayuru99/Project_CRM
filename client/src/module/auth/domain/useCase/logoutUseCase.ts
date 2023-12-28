import { LogoutRepository } from "../../repository/logoutRepository";
import { AuthenticateResponse } from "../types";

interface LogoutUseCase {
  (token: string): Promise<AuthenticateResponse>;
}

const logoutUseCase =
  (repository: LogoutRepository): LogoutUseCase =>
  async (token: string) => {
    return await repository(token);
  };

export { logoutUseCase, LogoutUseCase };
