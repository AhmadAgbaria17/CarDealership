import { toast } from "react-toastify";
import "../login/Login.css"
import { useEffect , useState } from "react";
import { useNavigate } from "react-router-dom";
import {useDispatch, useSelector} from "react-redux"
import type { RootState , AppDispatch } from "../../redux/stores";
import { registerUser } from "../../redux/apiCalls/authApiCall";
import swal from "sweetalert";





const Register: React.FC = () => {

  const dispatch = useDispatch<AppDispatch>();
  const {registerMessage} = useSelector((state:RootState)=>state.auth);

  const [userName, setUsername] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  useEffect(()=>{
    setEmail("");
    setPassword("");
    setUsername("");
  },[])

  const submitHandler = (e:React.FormEvent<HTMLFormElement>)=>{
    e.preventDefault();

    if(userName.trim() === "" || email.trim() === "" || password.trim() === ""){
     toast.error("All fields are required");
     return
    }

    dispatch(registerUser({userName, email, password}));
  }
  const navigate = useNavigate();
  useEffect(() => {
    if (registerMessage) {
      swal({
        title: registerMessage,
        icon: registerMessage === "Register succeded" ? "success" : "error",
      }).then(() => {
        if (registerMessage === "Register succeded") {
          navigate("/login");
        }
      });
    }
  }, [registerMessage, navigate]);

  
  return (
    <div className='form-container'>
      <form onSubmit={submitHandler} >
        <h2 className='form-label'>Register</h2>
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
          <label htmlFor="email">Email:  </label>
          <input 
          type="email" 
          id="email" 
          name="email" 
          value={email}
          onChange={(e)=>setEmail(e.target.value)}
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

export default Register
