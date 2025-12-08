import { toast } from "react-toastify";
import type { AppDispatch } from "../stores";
import request from "../../utils/request";
import { carDealerShipsActions } from "../slices/carDealerShipsSlice";


export function getCarDealerShips(){
  return async (dispatch : AppDispatch): Promise<void>=>{
    try{
      dispatch(carDealerShipsActions.setLoading(true));
      const response = await request.get("/car-dealer-ships");
      console.log(response.data)
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

export function deleteCarDealerShip(id: number){
  return async (dispatch: AppDispatch)=>{
    try{
      await request.delete(`api/car-dealer-ships/${id}`);
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

export function updateCarDealerShip(id: number, dealerShipData: Partial<any>){
   return async (dispatch: AppDispatch)=>{
    try{
      const {data} = await request.put(`api/car-dealer-ships/${id}`, dealerShipData);
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