import axios from "axios";
const UrlAPI = import.meta.env.VITE_BASE_URL_API;
export const api = axios.create({
    baseURL: UrlAPI,
});
api.interceptors.request.use((config) => {
    const token = localStorage.getItem('AuthToken');
    if(token){
        config.headers.Authorization = `Bearer ${token}`;
        config.headers["Content-Type"] = "application/json";
    }
    return config 
});
export default api;