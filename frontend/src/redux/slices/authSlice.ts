import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

interface User {
  id?: string;
  username?: string;
  email?: string;
}

interface AuthState {
  user : User | null;
  registerMessage: string | null;
}

const userFromStorage = localStorage.getItem("user");
const initialState: AuthState = {
  user : userFromStorage ? JSON.parse(userFromStorage) : null,
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