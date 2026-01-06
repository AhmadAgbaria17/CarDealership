import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import type { Car, CarDealerShip, CarDealerShipsState } from "../../interfaces/types";


const initialState: CarDealerShipsState ={
  carDealerShips: [],
  loading: false,
  selectedCarDealerShip: null,
  car: null,
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

    

    setCar(state, action: PayloadAction<Car | null>){
      state.car = action.payload;
    },
    deleteCar(state, action: PayloadAction<number>){
      if(state.carDealerShips){
        state.carDealerShips = state.carDealerShips.map(dealership => {
          if(dealership.cars){
             return {
              ...dealership,
              cars: dealership.cars.filter(c => c.id !== action.payload)
             }
          }
          return dealership;
        })
      }
      if(state.selectedCarDealerShip && state.selectedCarDealerShip.cars){
         state.selectedCarDealerShip.cars = state.selectedCarDealerShip.cars.filter(c => c.id !== action.payload);
      }
    },
    createCar(state, action: PayloadAction<Car>){
      if(state.carDealerShips){
        state.carDealerShips = state.carDealerShips.map(dealership => {
          if(dealership.cars){
             return {
              ...dealership,
              cars: dealership.cars.map(c => 
                c.id === action.payload.id ? action.payload : c
              )
             }
          }
          return dealership;
        })
      }
      if(state.selectedCarDealerShip && state.selectedCarDealerShip.cars){
         state.selectedCarDealerShip.cars = state.selectedCarDealerShip.cars.map(c => 
           c.id === action.payload.id ? action.payload : c
         )
      }
    },
    updateCar(state, action: PayloadAction<Car>){
      if(state.carDealerShips){
        state.carDealerShips = state.carDealerShips.map(dealership => {
          if(dealership.cars){
             return {
              ...dealership,
              cars: dealership.cars.map(c => 
                c.id === action.payload.id ? action.payload : c
              )
             }
          }
          return dealership;
        })
      }
      if(state.selectedCarDealerShip && state.selectedCarDealerShip.cars){
         state.selectedCarDealerShip.cars = state.selectedCarDealerShip.cars.map(c => 
           c.id === action.payload.id ? action.payload : c
         )
      }
    },
  
  

    
  }})


  const carDealerShipsReducer = carDealerShipsSlice.reducer;
  const carDealerShipsActions = carDealerShipsSlice.actions;
  export {carDealerShipsReducer, carDealerShipsActions};