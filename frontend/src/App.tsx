import {BrowserRouter , Route, Routes} from 'react-router-dom';
import {ToastContainer} from 'react-toastify'
import Header from './components/header/Header';
import Home from './pages/home/Home';
import Login from './pages/login/Login';
import Register from './pages/register/Register';

function App() {


  return (
    <BrowserRouter>
      <ToastContainer theme="colored" position="top-center" />
      <Header />
      <Routes>
        <Route path='/' element={<Home/>} />
        <Route path='/login' element={<Login/>} />
        <Route path='/register' element={<Register/>} />

      </Routes>

    
    </BrowserRouter>
  )
}

export default App
