import {BrowserRouter , Route, Routes} from 'react-router-dom';
import {ToastContainer} from 'react-toastify'
import Header from './components/header/Header';
import Home from './pages/home/Home';
import Login from './pages/login/Login';
import Register from './pages/register/Register';
import CarDealerShips from './pages/carDealerShips/CarDealerShips';
import CreateCarDealerShip from './pages/createCarDealerShip/CreateCarDealerShip';
import SingleCarDealerShip from './pages/singleCarDealerShip/SingleCarDealerShip';
import Car from './pages/car/Car';
import AddCar from './pages/addCar/AddCar';

function App() {


  return (
    <BrowserRouter>
      <ToastContainer theme="colored" position="top-center" />
      <Header />
      <Routes>
        <Route path='/' element={<Home/>} />
        <Route path='/car-dealer-ships' element={<CarDealerShips/>}/>
        <Route path='/login' element={<Login/>} />
        <Route path='/register' element={<Register/>} />
        <Route path='/create-car-dealer-ship' element={<CreateCarDealerShip />}/>
        <Route path='/car-dealer-ships/:id' element={<SingleCarDealerShip />}/>
        <Route path='/car-dealer-ships/:id/:carId' element={<Car />}/>
        <Route path= 'car-dealer-ships/:id/add-car' element={<AddCar/>}/>
      </Routes>

    
    </BrowserRouter>
  )
}

export default App
