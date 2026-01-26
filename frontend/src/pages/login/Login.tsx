import './login.css';
import { toast } from "react-toastify";
import { useEffect , useState } from "react";
import {useDispatch , useSelector} from "react-redux"
import { useNavigate } from 'react-router-dom';
import { getlikedCars, loginUser } from '../../redux/apiCalls/authApiCall';
import type { AppDispatch , RootState } from '../../redux/stores';




const Login : React.FC = () => {
  
  const dispatch = useDispatch<AppDispatch>();
  const {user} = useSelector((state:RootState)=>state.auth);

  const [userName, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  useEffect(()=>{
    setPassword("");
    setUsername("");
  },[])

  const submitHandler = (e:React.FormEvent<HTMLFormElement>)=>{
    e.preventDefault();

    if(userName.trim() === "" || password.trim() === ""){
      toast.error("All fields are required");
      return
    }

    dispatch(loginUser({userName,password}));

  }
  const navigate = useNavigate();
  
  useEffect(()=>{
    if(user){
      dispatch(getlikedCars());
      navigate("/");
    }
  },[dispatch,user])


  return (
    <div className="form-container">
      <form onSubmit={submitHandler}>
        <h2 className='form-label'>Login</h2>
        <div className='form-div'>
          <label htmlFor="username">Username:</label>
          <input 
          type="text" 
          id="username" 
          name="username" 
          value={userName}
          onChange={(e)=>setUsername(e.target.value)}
          />
        </div>

        <div className='form-div'>
          <label htmlFor="password">Password:</label>
          <input 
          type="password" 
          id="password" 
          name="password" 
          value={password}
          onChange={(e)=>setPassword(e.target.value)}
          />
        </div>
        <button className='form-btn' type="submit">Submit</button>

      </form>
      
    </div>
  )
}

export default Login
