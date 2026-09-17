/*
    Por enquanto nn tenho a mínima ideia do que e pra que vou usar
    
    O código a baixo, é um exemplo do Chat GPT
*/


export interface LoginRequest {
    email: string
    password: string
}

export interface LoginResponse {
    accessToken: string
    refreshToken: string
    expiresIn: number
}