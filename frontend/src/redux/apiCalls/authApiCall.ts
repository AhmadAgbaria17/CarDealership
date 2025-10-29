import {authActions} from "../slices/authSlice";
import {toast} from "react-toastify"
import type { AppDispatch } from "../stores";

interface User {
  id?: string;
  username?: string;
  email?: string;
  password?: string;
}

export function registerUser(user:User){
  return async (dispatch : AppDispatch): Promise<void> =>{
    try{

      console.log(user)
      dispatch(authActions.register("Register succeded")); 
    } catch (err) {
    if (err instanceof Error) {
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
      dispatch(authActions.login(user));
      
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