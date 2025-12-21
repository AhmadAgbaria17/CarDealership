import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import type { CarDealerShip, CarDealerShipsState } from "../../interfaces/types";


const initialState: CarDealerShipsState ={
  carDealerShips: [],
  loading: false,
  selectedCarDealerShip: null,
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
    createCarDealerShip(state, action: PayloadAction<CarDealerShip>){
      if(state.carDealerShips){
        state.carDealerShips = [...state.carDealerShips, action.payload];
      }
    },
    deleteCarDealerShip(state, action: PayloadAction<number>){
      if(state.carDealerShips){
         state.carDealerShips = state.carDealerShips.filter(c => c.id !== action.payload);
      }
    },
    updateCarDealerShip(state, action: PayloadAction<CarDealerShip>){
      if(state.carDealerShips){
        state.carDealerShips = state.carDealerShips.map(c => 
          c.id === action.payload.id ? action.payload : c
        )
      }
    },
    setSelectedCarDealerShip(state, action: PayloadAction<CarDealerShip | null>){
      state.selectedCarDealerShip = action.payload;
    },
    
  }})


  const carDealerShipsReducer = carDealerShipsSlice.reducer;
  const carDealerShipsActions = carDealerShipsSlice.actions;
  export {carDealerShipsReducer, carDealerShipsActions};