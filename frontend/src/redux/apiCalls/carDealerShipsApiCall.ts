import { toast } from "react-toastify";
import type { AppDispatch } from "../stores";
import request from "../../utils/request";
import { carDealerShipsActions } from "../slices/carDealerShipsSlice";





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