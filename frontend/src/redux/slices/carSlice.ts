import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { Car, CarState } from "../../interfaces/types";


const initialState: CarState ={
  car: null,
  loading: false,
}


const carSlice = createSlice({
  name: "car",
  initialState,
  reducers:{
    setLoading(state, action: PayloadAction<boolean>){
      state.loading = action.payload;
    },
    setCar(state, action: PayloadAction<Car>){
      state.car = action.payload;
    },
    updateCar(state, action: PayloadAction<Car>){
      state.car = action.payload;
    },
    deleteCar(state){
      state.car = null;
    },
    createCar(state, action: PayloadAction<Car>){
      state.car = action.payload;
    }


  }
})

const carReducer = carSlice.reducer;
const carActions = carSlice.actions;

export {carReducer, carActions};