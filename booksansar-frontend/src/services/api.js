import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7104', // Your ASP.NET Core backend URL
    withCredentials: true // Important for cookies/sessions
});

// Add a request interceptor to include the auth token
api.interceptors.request.use((config) => {
    const user = JSON.parse(localStorage.getItem('user'));
    if (user?.token) {
        config.headers.Authorization = `Bearer ${user.token}`;
    }
    return config;
}, (error) => {
    return Promise.reject(error);
});

export default api;