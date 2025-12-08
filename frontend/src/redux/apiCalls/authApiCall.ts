import {authActions} from "../slices/authSlice";
import {toast} from "react-toastify"
import type { AppDispatch } from "../stores";
import type { User } from "../../interfaces/types";
import request from "../../utils/request";



export function registerUser(user:User){
  return async (dispatch : AppDispatch): Promise<void> =>{
    try{

      const response = await request.post("/person/register", user);
      dispatch(authActions.register(response.data.registerMessage)); 
    } catch (err) {
    if (err instanceof Error) {
      console.log(err)
      toast.error(`Registration failed: ${err.message}`);
    } else {
      toast.error("Registration failed: Unknown error");
    }
    dispatch(authActions.register("Registration failed"));
  }

  }
}

export function loginUser(user:User){
  return async (dispatch : AppDispatch):Promise<void> =>{
    try{
      const response = await request.post("/person/login", user);
      localStorage.setItem("user", JSON.stringify(response.data));
      dispatch(authActions.login(response.data));
      toast.success("Login successful");
      
    } catch (err) {
    if (err instanceof Error) {
      toast.error(`Login failed: ${err.message}`);
    } else {
      toast.error("Login failed: Unknown error");
    }
  }
  }
}


export function logoutUser(){
  return async (dispatch : AppDispatch):Promise<void> =>{
    try{
      localStorage.removeItem("user");
      dispatch(authActions.logout());
      toast.success("Logged out");
      } catch (err) {
    if (err instanceof Error) {
      toast.error(`Logout failed: ${err.message}`);
    } else {
      toast.error("Logout failed: Unknown error");
    }
  }
  }
}