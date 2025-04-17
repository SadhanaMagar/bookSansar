// src/services/authService.js
import axios from 'axios';
import api from './api';

const register = async (firstName, lastName, email, password) => {
    try {
        const response = await api.post('/auth/register', {
            firstName,
            lastName,
            email,
            password
        });
        return response.data;
    } catch (error) {
        throw error;
    }
};

const login = async (email, password) => {
    try {
        const response = await api.post('/auth/login', {
            email,
            password
        });

        if (response.data.accessToken) {
            localStorage.setItem('user', JSON.stringify(response.data));
        }
        return response.data;
    } catch (error) {
        throw error;
    }
};

const logout = () => {
    localStorage.removeItem('user');
};

const getCurrentUser = () => {
    return JSON.parse(localStorage.getItem('user'));
};

export default {
    register,
    login,
    logout,
    getCurrentUser
};