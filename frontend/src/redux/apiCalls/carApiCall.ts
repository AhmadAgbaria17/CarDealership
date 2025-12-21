import type { AppDispatch } from "../stores";
import request from "../../utils/request";
import { toast } from "react-toastify";
import { carActions } from "../slices/carSlice";

const user = localStorage.getItem("user");
const token = user ? JSON.parse(user).token : null;

export function getCarById(id: number){
  return async (dispatch: AppDispatch): Promise<void>=>{
    try{
      dispatch(carActions.setLoading(true));
      const response = await request.get(`car/${id}`);
      dispatch(carActions.setCar(response.data));
    }catch(err){
      if(err instanceof Error){
        toast.error(err.message);
      } else {
         toast.error("Failed to get car");
      }
    }finally{
      dispatch(carActions.setLoading(false));
    }
  }
}

export function updateCar(id: number, carData: Partial<any>){
   return async (dispatch: AppDispatch): Promise<void>=>{
    try{
      const {data} = await request.put(`/car/${id}`, carData,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      dispatch(carActions.updateCar(data));
      toast.success("Car updated successfully");
    }catch(err){
       if(err instanceof Error){
        toast.error(err.message);
      } else {
         toast.error("Failed to update car");
      }
    }
   }
}

export function deleteCar(id: number){
   return async (dispatch: AppDispatch): Promise<void>=>{
    try{
      await request.delete(`/car/${id}`,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      dispatch(carActions.deleteCar());
      toast.success("Car deleted successfully");
    }catch(err){
       if(err instanceof Error){
        toast.error(err.message);
      } else {
         toast.error("Failed to delete car");
      }
    }
   }
}

export function createCar(carData: Partial<any>){
   return async (dispatch: AppDispatch): Promise<void>=>{
    try{
      const {data} = await request.post("/car", carData,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      dispatch(carActions.createCar(data));
      toast.success("Car created successfully");
    }catch(err){
       if(err instanceof Error){
        toast.error(err.message);
      } else {
         toast.error("Failed to create car");
      }
    }
   }
}

