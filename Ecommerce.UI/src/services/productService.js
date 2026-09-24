import axios from "axios";
import api from "./api";

const API = "https://localhost:7064/api/Product";

export const getProducts = async () => {
  const response = await api.get("/Product");
  return response.data;
};

export const getProductById = async (id) => {
  return await axios.get(`${API}/${id}`);
};

export const createProduct = async (product, token) => {
  return await axios.post(API, product, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
};
