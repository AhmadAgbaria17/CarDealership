export interface LikedCar {
  id?: string;
  name: string;
}

export interface User {
  id? : string;
  userName? : string;
  email? : string;
  password? : string;
  token? : string;
  likedCars?: LikedCar[];
}

export interface CarDealerShip {
  id: number;
  name: string;
  city: string;
  address: string;
  coordinates: number[];
  phone: string;
  createdBy?: string;
  personId?: string;
  cars: Car[];
}

export interface Car {
  id: number;
  company: string;
  modelName: string;
  year: number;
  color: string;
  image: string;
  description: string;
  price: string;
  fuel: string;
  transmission: string;
  mileage: string;
  engine: string;
  horsePower: string;
  type: string;
  carDealerShipId?: number;
}


export interface AuthState {
  user : User | null;
  registerMessage: string | null;
}

export interface CarDealerShipsState {
  carDealerShips: CarDealerShip[] | null;
  loading: boolean;
}