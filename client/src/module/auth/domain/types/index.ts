interface AuthenticateRequest {
  email: string;
  password: string;
}

interface AuthenticateResponse {
  accessToken: string;
  refreshToken: string;
  expiresIn: number;
  tokenType: string;
}

export { AuthenticateRequest, AuthenticateResponse };
