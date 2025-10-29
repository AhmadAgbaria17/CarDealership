
import './Login.css'



const Login = () => {
  return (
    <div className="form-container">
      <form>
        <h2 className='form-label'>Login</h2>
        <div className='form-div'>
          <label htmlFor="username">Username:</label>
          <input type="text" id="username" name="username" />
        </div>

        <div className='form-div'>
          <label htmlFor="password">Password:</label>
          <input type="password" id="password" name="password" />
        </div>
        <button className='form-btn' type="submit">Submit</button>

      </form>
      
    </div>
  )
}

export default Login
