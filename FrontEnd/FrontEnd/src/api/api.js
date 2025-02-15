// src/api/api.js
import axios from 'axios';

// Create an axios instance with your base URL
const API = axios.create({
  baseURL: 'https://localhost:44307/api/v1', // Adjust as needed
});

// REQUEST INTERCEPTOR
API.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// RESPONSE INTERCEPTOR
API.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response) {
      // If we get a 401, 403, or 404, remove the token and force login
      if ([401, 403, 404].includes(error.response.status)) {
        localStorage.removeItem('token');
        window.location.href = '/login'; // or use navigate('/login') in a React component
      }
    }
    return Promise.reject(error);
  }
);

export default API;
