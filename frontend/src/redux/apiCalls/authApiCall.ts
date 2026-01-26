import {authActions} from "../slices/authSlice";
import {toast} from "react-toastify"
import type { AppDispatch } from "../stores";
import type { LikedCar, User } from "../../interfaces/types";
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

  export function getlikedCars(){
    return async (dispatch : AppDispatch):Promise<void> =>{
      try{
        const usert = localStorage.getItem("user");
        const token = usert ? JSON.parse(usert).token : null;
        
        if (!token) {
          toast.error("You must be logged in to view liked cars");
          return;
        }
        
        const response = await request.get(`/likedcars`,
          {
            headers: {
              "Content-Type": "application/json",
              Authorization: `Bearer ${token}`,
            },
          }
        );
        dispatch(authActions.getlikedCars(response.data));
      } catch (err) {
    if (err instanceof Error) {
      toast.error(`Failed to fetch liked cars: ${err.message}`);
    } else {
      toast.error("Failed to fetch liked cars: Unknown error");
    }
  }
  }
}

export function addLikedCar(likedCar: LikedCar){
  return async (dispatch : AppDispatch):Promise<void> =>{
    try{
      const usert = localStorage.getItem("user");
      const token = usert ? JSON.parse(usert).token : null;
      
      if (!token) {
        toast.error("You must be logged in to like a car");
        return;
      }
      
      const response = await request.post(`/likedcars/${likedCar.id}`,null,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        } 
      );
      console.log(response.data);
      if(response.data){
        dispatch(authActions.addLikedCar(likedCar));
      }
    } catch (err) {
  if (err instanceof Error) {
    toast.error(`Failed to add liked car: ${err.message}`);
  } else {
    toast.error("Failed to add liked car: Unknown error");
  }
}
}
}

export function removeLikedCar(carId: number){
  return async (dispatch : AppDispatch):Promise<void> =>{
    try{
      const usert = localStorage.getItem("user");
      const token = usert ? JSON.parse(usert).token : null;
      
      if (!token) {
        toast.error("You must be logged in to unlike a car");
        return;
      }
      
      const response = await request.delete(`/likedcars/${carId}`,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      if(response.data){
        dispatch(authActions.removeLikedCar(carId));
      }
    } catch (err) {
  if (err instanceof Error) {
    toast.error(`Failed to remove liked car: ${err.message}`);
  } else {
    toast.error("Failed to remove liked car: Unknown error");
  }
}
}
}

