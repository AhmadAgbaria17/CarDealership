export interface User {
  id? : string;
  userName? : string;
  email? : string;
  password? : string;
  token? : string;
}

export interface CarDealerShip {
  id: number;
  name: string;
  city: string;
  address: string;
  coordinates?: number[];
  phone: string;
  createdBy?: string;
  personId?: string;
  cars?: Car[];
}

export interface CreateCarDealerShips {
  name: string;
  city: string;
  address: string;
  phone: string;
}

export interface Car {
  id: number;
  company: string;
  modelName: string;
  year: number;
  color: string;
  images? : string[];
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

export interface CreateCar {
  company: string;
  modelName: string;
  year: number;
  color: string;
  images? : string[];
  description: string;
  price: string;
  fuel: string;
  transmission: string;
  mileage: string;
  engine: string;
  horsePower: string;
  type: string;
}

export interface LikedCar {
  id: number;
  company: string | undefined;
  modelName: string | undefined;
  year: number | undefined;
}


export interface AuthState {
  user : User | null;
  registerMessage: string | null;
  likedCars: LikedCar[];
}

export interface CarDealerShipsState {
  carDealerShips: CarDealerShip[] | null;
  loading: boolean;
  selectedCarDealerShip: CarDealerShip | null;
  car: Car | null;
}
