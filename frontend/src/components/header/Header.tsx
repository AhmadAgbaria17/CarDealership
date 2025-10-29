import './header.css'

const Header = () => {
  return (
    <header className="header">
      <div >
        <h1><a className='nav-label' href='/'>CarDealerShip</a></h1>
      </div>

      <div >
        <nav className='nav-section'>
          <ul><a className='nav-btn' href='login'>Login</a></ul>
          <ul><a className='nav-btn' href='register'>Register</a></ul>
        </nav>
      </div>           
    </header>
  )
}

export default Header


