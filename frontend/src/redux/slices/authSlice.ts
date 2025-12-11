import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import type { AuthState, User } from "../../interfaces/types";

const getUserFromStorage = () => {
  try {
    const userFromStorage = localStorage.getItem("user");
    return userFromStorage ? JSON.parse(userFromStorage) : null;
  } catch (error) {
    return null;
  }
}

const initialState: AuthState = {
  user: getUserFromStorage(),
  registerMessage: null
}


const authSlice = createSlice({
  name: "auth",
  initialState, 
  reducers:{
    login(state, action: PayloadAction<User>){
      state.user = action.payload;
      state.registerMessage = null;
    },
    logout(state){
      state.user = null;
    },
    register(state, action: PayloadAction<string>){
      state.registerMessage = action.payload;
    },
  }
})

const authReducer = authSlice.reducer;
const authActions = authSlice.actions;
export {authReducer, authActions};