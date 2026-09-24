/*
    Apenas um exemplo, implementar de uma forma correta futuramente.

    src/
├── pages/
│   └── Auth.tsx
│
├── services/
│   └── AuthService.ts
│
├── types/
│   └── AuthTypes.ts
│
└── ...


Auth.tsx
   ↓
captura email/senha
   ↓
AuthService.ts
   ↓
faz requisição para API
   ↓
AuthTypes.ts
define o formato dos dados ↗




types/
├── AuthTypes.ts
├── ContaTypes.ts
├── TransacaoTypes.ts
├── CategoriaTypes.ts
└── UsuarioTypes.ts
*/


import type { LoginRequest, LoginResponse, RefreshTokenRequest, RegisterRequest, RegisterResponse } from "../types/AuthTypes"
import api from "../../../api/api"

export async function Register(credentials:RegisterRequest) : Promise<RegisterResponse> {
  const response = await api.post(`auth/register`,{
    Nome:credentials.Nome,
    Email:credentials.Email,
    Password:credentials.Password
  })
  if(!response.data[0]){
    throw new Error("Erro ao registrar")
  }
  return response.data
}

export async function login(credentials: LoginRequest): Promise<LoginResponse> {
  const response = await api.post(`auth/login`, {
    Email: credentials.email,
    Password: credentials.password,
  })

  if (!response.data[0]) {
    console.error(response)
    throw new Error("Erro ao realizar login")
  }
  return response.data()
}

export async function Refreshtoken(params: RefreshTokenRequest) {
  const response = await api.post(`auth/refresh`,{
    UserId: params.userid,
    RefreshToken: params.refreshtoken
  })

  if(!response.data[0]){
    throw new Error("Erro ao realizar atualizar Token")
  }
  return response.data
}

export async function Revoke(UserId:string) {
  const response = await api.post(`auth/rovoke/${UserId}`)
  if(!response.data[0]){
    throw new Error("Erro ao realizar rovoke")
  }
  return response.data
}