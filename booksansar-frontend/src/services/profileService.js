import axios from 'axios';

const API_URL = 'https://localhost:7104/api/Profile';

const getProfile = () => {
    const user = JSON.parse(localStorage.getItem('user'));
    return axios.get(API_URL, {
        headers: {
            'Authorization': `Bearer ${user?.token}`
        }
    });
};

const updateProfile = (profileData) => {
    const user = JSON.parse(localStorage.getItem('user'));
    return axios.put(API_URL, profileData, {
        headers: {
            'Authorization': `Bearer ${user?.token}`
        }
    });
};

export default {
    getProfile,
    updateProfile
};