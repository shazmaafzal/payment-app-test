import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:44340/api',
});

export default apiClient;
