import { configureStore } from "@reduxjs/toolkit";
import { authReducer } from "./slices/authSlice";
import { carDealerShipsReducer } from "./slices/carDealerShipsSlice";


const store = configureStore({
  reducer: {
    auth: authReducer,
    carDealerShips: carDealerShipsReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;
