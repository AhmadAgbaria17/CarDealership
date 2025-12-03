import axios from "axios";

const request = axios.create({
  baseURL: "http://localhost:5125/api/",
})

export default request;