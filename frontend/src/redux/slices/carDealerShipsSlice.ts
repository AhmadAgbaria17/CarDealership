import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import type { CarDealerShip, CarDealerShipsState } from "../../interfaces/types";


const initialState: CarDealerShipsState ={
  carDealerShips: [],
  loading: false,
}


const carDealerShipsSlice = createSlice({
  name: "carDealerShips",
  initialState,
  reducers: {
    setCarDealerShips(state, action: PayloadAction<CarDealerShip[]>){
      state.carDealerShips = action.payload;
    },
    setLoading(state, action: PayloadAction<boolean>){
      state.loading = action.payload;
    },
  }})


  const carDealerShipsReducer = carDealerShipsSlice.reducer;
  const carDealerShipsActions = carDealerShipsSlice.actions;
  export {carDealerShipsReducer, carDealerShipsActions};