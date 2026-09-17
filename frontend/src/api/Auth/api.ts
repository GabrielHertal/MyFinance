import api from "../api";

export const Register = async (Nome:string, Email:string, Password:string) => {
    try
    {
        const response = await api.post("auth/register",{
            Nome: Nome,
            Email: Email,
            Password: Password,
        });
        return response.data;
    }
    catch(error){
        console.log(error);
        return error;
    }
}

export const Login = async (Email:string, Password:string) => {
    try
    {
        const response = await api.post("auth/login",{
            Email: Email,
            Password: Password,
        }); 
        return response.data;
    }
    catch(error)
    {
        console.error(error);
        return error;
    }
}

export const RefreshToken = async (UserId:string, RefreshToken:string) => {
    try
    {
        const response = await api.post("auth/refresh",{
            UserId: UserId,
            RefreshToken: RefreshToken,
        });
        return response.data;
    }

    catch(error)
    {
        console.error(error)
        return error;
    }
}

export const Revoke = async (UserId:string) => {
    try
    {
        const response = await api.post("auth/revoke",{
            UserId: UserId,
        });
        return response.data;
    }
    catch(error)
    {
        console.error(error);
        return error;
    }
}