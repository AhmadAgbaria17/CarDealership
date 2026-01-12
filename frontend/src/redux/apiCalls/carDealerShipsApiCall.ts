import { toast } from "react-toastify";
import type { AppDispatch } from "../stores";
import request from "../../utils/request";
import { carDealerShipsActions } from "../slices/carDealerShipsSlice";
import type { CreateCar, CreateCarDealerShips } from "../../interfaces/types";

const user = localStorage.getItem("user");
const token = user ? JSON.parse(user).token : null;

export function getCarDealerShips(){

  return async (dispatch : AppDispatch): Promise<void>=>{
    try{
      dispatch(carDealerShipsActions.setLoading(true));
      const response = await request.get("/car-dealer-ships");
      dispatch(carDealerShipsActions.setCarDealerShips(response.data));
    }catch(err){
      if(err instanceof Error){
        console.log(err)
        toast.error(`Error fetching car dealer ships: ${err.message}`)
      }else{
        toast.error("Error fetching car dealer ships: Unknown error")
      }
    }finally{
      dispatch(carDealerShipsActions.setLoading(false));
    }
  }
}

export function createCarDealerShip(dealerShipData: CreateCarDealerShips){
  return async (dispatch: AppDispatch)=>{
    try{
      const {data} = await request.post("/car-dealer-ships", dealerShipData,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      console.log(data)
      dispatch(carDealerShipsActions.createCarDealerShip(data));
      toast.success("Dealership created successfully");
    }catch(err){
      if(err instanceof Error){
        toast.error(err.message);
      } else {
         toast.error("Failed to create dealership");
      }
    }
  }
}

export function deleteCarDealerShip(id: number){
  return async (dispatch: AppDispatch): Promise<void>=>{
    try{
      await request.delete(`/car-dealer-ships/${id}`,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      dispatch(carDealerShipsActions.deleteCarDealerShip(id));
      toast.success("Dealership deleted successfully");
    }catch(err){
      if(err instanceof Error){
        toast.error(err.message);
      } else {
         toast.error("Failed to delete dealership");
      }
    }
  }
}

export function updateCarDealerShip(id: number, dealerShipData: Partial<CreateCarDealerShips>){
   return async (dispatch: AppDispatch): Promise<void>=>{
    try{
      const {data} = await request.put(`/car-dealer-ships/${id}`, dealerShipData,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      dispatch(carDealerShipsActions.updateCarDealerShip(data));
      toast.success("Dealership updated successfully");
    }catch(err){
       if(err instanceof Error){
        toast.error(err.message);
      } else {
         toast.error("Failed to update dealership");
      }
    }
   }
}

export function getCarDealerShipById(id: number){
  return async (dispatch: AppDispatch): Promise<void>=>{
    try{
      dispatch(carDealerShipsActions.setLoading(true));
      const {data} = await request.get(`/car-dealer-ships/${id}`);
      dispatch(carDealerShipsActions.setSelectedCarDealerShip(data));
    }catch(err){
      if(err instanceof Error){
        toast.error(err.message);
      } else {
         toast.error("Failed to get dealership");
      }
    }finally{
      dispatch(carDealerShipsActions.setLoading(false));
    }
  }
}

export function getCarById(id: number){
  return async (dispatch: AppDispatch): Promise<void>=>{
    try{
      dispatch(carDealerShipsActions.setLoading(true));
      const {data} = await request.get(`/car/${id}`);
      dispatch(carDealerShipsActions.setCar(data));
    }catch(err){
      if(err instanceof Error){
        toast.error(err.message);
      } else {
         toast.error("Failed to get car");
      }
    }finally{
      dispatch(carDealerShipsActions.setLoading(false));
    }
  }
}

export function deleteCar(id: number){
  return async (dispatch: AppDispatch):Promise<void>=>{
    try{
      await request.delete(`/car/${id}`,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      dispatch(carDealerShipsActions.deleteCar(id));
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

export function createCar(carData : CreateCar , CarDealerShipId? : string){
  return async (dispatch : AppDispatch): Promise<void>=>{
    try{
      const {data} = await request.post(`/car/${CarDealerShipId}`, carData,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      dispatch(carDealerShipsActions.createCar(data));
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

export function updateCar(id: number, carData: Partial<CreateCar>){
  return async (dispatch: AppDispatch): Promise<void> => {
    try {
      const { data } = await request.put(`/car/${id}`, carData,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      dispatch(carDealerShipsActions.updateCar(data));
      toast.success("Car updated successfully");
    } catch (err) {
      if (err instanceof Error) {
        toast.error(err.message);
      } else {
        toast.error("Failed to update car");
      }
    }
  }
}

