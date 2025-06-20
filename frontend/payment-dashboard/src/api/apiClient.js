import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7257/api',
});

export default apiClient;
