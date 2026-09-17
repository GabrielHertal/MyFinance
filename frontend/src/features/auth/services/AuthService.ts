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


import type { LoginRequest, LoginResponse } from "../types/AuthTypes"

export async function login(
  credentials: LoginRequest
): Promise<LoginResponse> {
  const response = await fetch(`${import.meta.env.VITE_API_URL}/auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(credentials)
  })

  if (!response.ok) {
    throw new Error("Erro ao realizar login")
  }

  return response.json()
}