import { AxiosInstance } from "axios";
import { AuthenticateRequest, AuthenticateResponse } from "../domain/types";

interface AuthenticateRepository {
  (auth: AuthenticateRequest): Promise<AuthenticateResponse>;
}

const authenticateRepository =
  (axios: AxiosInstance): AuthenticateRepository =>
  async (auth: AuthenticateRequest) => {
    const formData = new FormData();
    formData.append("email", auth.email);
    formData.append("password", auth.password);

    const response = await axios.post("/auth/token", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });

    return response.data;
  };

export { authenticateRepository, AuthenticateRepository };
