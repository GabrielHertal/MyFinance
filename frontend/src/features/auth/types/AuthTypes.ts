export interface RegisterRequest{
    Nome:string
    Email:string
    Password:string
}

export interface RegisterResponse{
    UserId:string
    Token:string
}

export interface LoginRequest {
    email: string
    password: string
}

export interface LoginResponse {
    sucess: boolean
    status_code: number
    accessToken: string
    refreshToken: string
    expiresIn: number
}

export interface RefreshTokenRequest{
    userid: string
    refreshtoken: string
}