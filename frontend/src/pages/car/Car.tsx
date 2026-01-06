import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { useDispatch } from 'react-redux'
import type { AppDispatch } from '../../redux/stores'
import './car.css'
import { FaArrowLeft ,FaHeart, FaRegHeart } from "react-icons/fa";
import { useSelector } from 'react-redux'
import type { RootState } from '../../redux/stores'
import { getCarById } from '../../redux/apiCalls/carDealerShipsApiCall'
import type { LikedCar } from '../../interfaces/types'
import { addLikedCar, removeLikedCar } from '../../redux/apiCalls/authApiCall'
import { useNavigate } from 'react-router-dom'

const Car: React.FC = () =>{
  const navigate = useNavigate();
  const {carId} = useParams();
  const dispatch = useDispatch<AppDispatch>();
  const {loading, car} = useSelector((state:RootState) => state.carDealerShips);
  const [liked, setLiked] = useState(false);
  const [selectedImage, setSelectedImage] = useState<string | null>(null);
  const {user , likedCars } = useSelector((state: RootState) => state.auth);  

  useEffect(()=>{
    if(carId){
      dispatch(getCarById(Number(carId)));
    }
  },[dispatch, carId])

  useEffect(() => {
    if (car?.images && car.images.length > 0) {
      setSelectedImage(car.images[0]);
    }
  }, [car]);

  useEffect(() => {
    if (car) {
      const isLiked = likedCars.some((likedCar) => likedCar.id === Number(carId));
      setLiked(isLiked);
    }
  }, [likedCars, car]);

  const handleToggleLike = () => {
    
      if (liked) {
        dispatch(removeLikedCar(Number(carId)));
      } else {


        const likedCar: LikedCar = {
          id: Number(carId),
          company: car?.company,
          modelName: car?.modelName,
          year: car?.year,
        };
        dispatch(addLikedCar(likedCar));
      }
      setLiked(!liked);
  
  };



  if(loading){
    return (
      <div className="car-dealer-ships-spinner-container">
        <div className="car-dealer-ships-spinner" role="status" aria-label="Loading" />
      </div>
    )
  }

  return (
    <div className='car-container'>
      
      <div className='car-header'>
        <div className='car-title-container'>
         <h1 className='car-title'><span className='car-title-arrow'><FaArrowLeft size={20} onClick={() => navigate(-1)} /></span> {car?.company} {car?.modelName} <span className='car-year'>({car?.year})</span></h1>
          {user && (
            <span onClick={() => handleToggleLike()} style={{ cursor: "pointer" }}>
             {liked ? (
              <FaHeart color="red" size={30} />
            ) : (
              <FaRegHeart size={30} />
            )}
          </span>
          )}
        </div>
       <div className="car-info-grid">
          <div className='info-item'>
             <span className="label">Price</span>
             <span className="value price">${car?.price}</span>
          </div>
           <div className='info-item'>
             <span className="label">Color</span>
             <span className="value" style={{display: 'flex', alignItems: 'center', gap: '5px'}}>
               {car?.color}
               <span style={{backgroundColor: car?.color, width: '15px', height: '15px', borderRadius: '50%', display: 'inline-block', border: '1px solid #ddd'}}></span>
             </span>
          </div>
           <div className='info-item'>
             <span className="label">Mileage</span>
             <span className="value">{car?.mileage} km</span>
          </div>
          <div className='info-item'>
             <span className="label">Fuel Type</span>
             <span className="value">{car?.fuel}</span>
          </div>
          <div className='info-item'>
             <span className="label">Transmission</span>
             <span className="value">{car?.transmission}</span>
          </div>
          <div className='info-item'>
             <span className="label">Engine</span>
             <span className="value">{car?.engine}</span>
          </div>
          <div className='info-item'>
             <span className="label">Horsepower</span>
             <span className="value">{car?.horsePower} HP</span>
          </div>
           <div className='info-item'>
             <span className="label">Type</span>
             <span className="value">{car?.type}</span>
          </div>
       </div>

        <div className='car-description'>
          <h3>Description</h3>
          <p>{car?.description}</p>
        </div>
      </div>

      <div className='car-main-image-container'>
        {selectedImage ? (
           <img src={selectedImage} alt={`${car?.company} ${car?.modelName}`} className="main-image" />
        ) : (
           <div className="no-image-placeholder">No Image Available</div>
        )}
      </div>

      <div className='car-images-gallery'>
        {car?.images?.map((img, index) => (
          <div 
            key={index} 
            className={`gallery-item ${selectedImage === img ? 'active' : ''}`}
            onClick={() => setSelectedImage(img)}
          >
            <img src={img} alt={`Thumbnail ${index + 1}`} />
          </div>
        ))}
      </div>
    </div>
  )
}

export default Car