import axios from "axios";

const API = "https://localhost:7064/api/Auth";

export const login = async (data) => {
  return await axios.post(`${API}/login`, data);
};

export const register = async (data) => {
  return await axios.post(`${API}/register`, data);
};

export const adminRegister = async (data) => {
  return await axios.post(`${API}/admin/register`, data);
};
