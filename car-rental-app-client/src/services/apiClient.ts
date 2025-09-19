import axios from "axios";

const api = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
});

// (இப்போ login இல்லை; token attach தேவையில்லை. பின்னாடி சேர்க்கலாம்)
export default api;
