import axios from "axios";
export const api = axios.create({
    baseURL: import.meta.env.BaseUrlAPI,
});
api.interceptors.request.use((config) => {
    const token = localStorage.getItem('AuthToken');
    if(token){
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config 
});

export default api;