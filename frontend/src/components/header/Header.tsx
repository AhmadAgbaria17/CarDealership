import { useEffect, useRef, useState } from 'react';
import './header.css';
import { useDispatch, useSelector} from "react-redux";
import type { AppDispatch, RootState } from '../../redux/stores';
import { getlikedCars, logoutUser, removeLikedCar } from '../../redux/apiCalls/authApiCall';


const Header = () => {
  const dispatch = useDispatch<AppDispatch>();
  const {user , likedCars} = useSelector((state:RootState)=>state.auth);
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const dropdownRef = useRef<HTMLDivElement | null>(null);

  const toggleDropdown = () => {
    setIsDropdownOpen((prevState) => !prevState);
  };

  const handleLogout = (e: React.MouseEvent) => {
    e.preventDefault();
    e.stopPropagation();
    dispatch(logoutUser());
    setIsDropdownOpen(false);
  };

  useEffect(() => {
    if (!isDropdownOpen) return;

    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setIsDropdownOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, [isDropdownOpen]);

  useEffect(() => {
    if(user){
      dispatch(getlikedCars());
    }
  }, [user,dispatch]);

  const handleDeleteClick = (e: React.MouseEvent, carId: number) => {
    e.preventDefault();
    e.stopPropagation();
    dispatch(removeLikedCar(carId));
  }

  return (
    <header className="header">
      <div >
        <h1><a className='nav-label' href='/'>CarDealerShip</a></h1>
      </div>

      {user ? 
      <div className='user-section' ref={dropdownRef}>
        <button 
          className='user-menu-toggle' 
          onClick={toggleDropdown} 
          aria-haspopup="true" 
          aria-expanded={isDropdownOpen}
          type='button'
        >
          <span className='welcome-text'>Welcome {user.userName}</span>
          <span className={`arrow-icon ${isDropdownOpen ? 'open' : ''}`} aria-hidden="true">⌄</span>
        </button>
        {isDropdownOpen && (
          <div className='user-dropdown' role='menu'>
            <div className='dropdown-section'>
              <p className='dropdown-title'>Liked Cars</p>
              {likedCars.length ? (
                <ul className='liked-cars-list'>
                  {likedCars.map((likedcar, index) => (
                    <a href={`/car-dealer-ships/${likedcar.carDealerShipId}/${likedcar.id}`}><li  key={likedcar.id ?? `${likedcar}-${index}`}><span className='liked-car-delete-btn' onClick={(e) => handleDeleteClick(e, likedcar.id)}>X</span>{likedcar.company} {likedcar.modelName} {likedcar.year}</li></a>
                  ))}
                </ul>
              ) : (
                <p className='empty-message'>No liked cars yet</p>
              )}
            </div>
            <button className='logout-btn' type='button' onClick={handleLogout}>
              Logout
            </button>
          </div>
        )}
      </div> 
      :
       <div >
        <nav className='nav-section'>
          <ul><a className='nav-btn' href='/login'>Login</a></ul>
          <ul><a className='nav-btn' href='/register'>Register</a></ul>
        </nav>
      </div>          } 
    </header>
  )
}

export default Header


