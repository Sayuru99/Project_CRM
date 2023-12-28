import { AxiosInstance } from "axios";
import { AuthenticateRequest, AuthenticateResponse } from "../domain/types";

interface LogoutRepository {
  (token: string): Promise<AuthenticateResponse>;
}

const logoutRepository =
  (axios: AxiosInstance): LogoutRepository =>
  async (token: string) => {
    const response = await axios.delete("/auth/logout", {
      headers: {
        Authorization: token,
      },
    });

    return response.data;
  };

export { logoutRepository, LogoutRepository };
