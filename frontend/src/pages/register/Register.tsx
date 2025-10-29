import { toast } from "react-toastify";
import "../login/Login.css"
  import { useEffect , useState } from "react";



const Register: React.FC = () => {


  const [username, setUsername] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  useEffect(()=>{
    setEmail("");
    setPassword("");
    setUsername("");
  },[])

  const submitHandler = (e:React.FormEvent<HTMLFormElement>)=>{
    e.preventDefault();

    if(username.trim() === "" || email.trim() === "" || password.trim() === ""){
     toast.error("All fields are required");
     return
    }



  }
  
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
           value={username}
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
