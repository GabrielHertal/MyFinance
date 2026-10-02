import type { LoginRequest, LoginResponse, RefreshTokenRequest, RegisterRequest, RegisterResponse } from "../types/AuthTypes"
import api from "../../../api/api"

export async function Register(credentials:RegisterRequest) : Promise<RegisterResponse> {
  const response = await api.post(`auth/register`,{
    Nome:credentials.Nome,
    Email:credentials.Email,
    Password:credentials.Password
  })
  return response.data
}

export async function login(credentials: LoginRequest): Promise<LoginResponse> {
  const response = await api.post(`auth/login`, {
    Email: credentials.email,
    Password: credentials.password,
  })
  return response.data
}

export async function Refreshtoken(params: RefreshTokenRequest) {
  const response = await api.post(`auth/refresh`,{
    UserId: params.userid,
    RefreshToken: params.refreshtoken
  })
  return response.data
}

export async function Revoke(UserId:string) {
  const response = await api.post(`auth/revoke/${UserId}`)
  return response.data
}