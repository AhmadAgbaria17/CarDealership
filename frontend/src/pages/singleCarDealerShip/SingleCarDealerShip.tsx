import "./singleCarDealerShip.css";
import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useParams } from 'react-router-dom';
import type { AppDispatch,RootState } from '../../redux/stores';
import { deleteCar, getCarDealerShipById } from '../../redux/apiCalls/carDealerShipsApiCall';
import CarItem from "../../components/carItem/CarItem";
import type { Car } from "../../interfaces/types";
import EditCarModal from "../../components/modals/EditCarModal";
import { FaArrowLeft } from "react-icons/fa";

const SingleCarDealerShip: React.FC = () => {
  const dispatch = useDispatch<AppDispatch>();
  const {id} = useParams();
  const {selectedCarDealerShip , loading} = useSelector((state:RootState) => state.carDealerShips);
  const {user} = useSelector((state:RootState) => state.auth);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [selectedCar, setSelectedCar] = useState<Car | null>(null);
  
  useEffect(()=>{
    dispatch(getCarDealerShipById(Number(id)));
  },[dispatch])

  const navigate = useNavigate();

  const navigateToCarDetails = (id: number, carId: number) => {
    navigate(`/car-dealer-ships/${id}/${carId}`);
  }

  const handleEditClick = (e: React.MouseEvent, car: Car) => {
    e.stopPropagation();
    setSelectedCar(car);
    setEditModalOpen(true);
  }


  const handleDeleteClick = (e: React.MouseEvent, carId: number) => {
    e.stopPropagation();
    swal({
      title: "Are you sure?",
      text: "Once deleted, you will not be able to recover this car!",
      icon: "warning",
      buttons: ["Cancel", "Delete"],
      dangerMode: true,
    })
    .then((willDelete) => {
      if (willDelete) {
        dispatch(deleteCar(carId));
        navigate(`/car-dealer-ships/${id}`);
      }
    });


  }

  if(loading){
    return (
      <div className="car-dealer-ships-spinner-container">
        <div className="car-dealer-ships-spinner" role="status" aria-label="Loading" />
      </div>
    )
  }




  return (
    <div className="single-car-dealer-ship-container">
      {editModalOpen && selectedCar && (
        <EditCarModal 
          car={selectedCar} 
          onClose={() => setEditModalOpen(false)} 
        />
      )}

      <div className="single-car-dealer-ship-information-container">
        <p><span>name :</span> {selectedCarDealerShip?.name}</p>
        <p><span>city :</span> {selectedCarDealerShip?.city}</p>
        <p><span>address :</span> {selectedCarDealerShip?.address}</p>
        <p><span>phone :</span> {selectedCarDealerShip?.phone}</p>
      </div>

      <div className="single-car-dealer-ship-cars-header">
        
            <h2><span className='car-title-arrow'><FaArrowLeft size={18} onClick={() => navigate(`/car-dealer-ships`)}/></span> cars ({selectedCarDealerShip?.cars?.length})</h2>
            {user?.id === selectedCarDealerShip?.personId &&(
              <button className="action-btn add-btn" onClick={() => navigate(`/car-dealer-ships/${id}/add-car`)}>Add Car</button>
            )}
      </div>
      
      <div className="single-car-dealer-ship-cars-grid">
        {selectedCarDealerShip?.cars?.length === 0 && (
          <p className="single-car-dealer-ship-cars-grid-no-cars">No cars found</p>
        )}
        {selectedCarDealerShip?.cars?.map((car)=>(
          <div key={car.id} className="car-card-wrapper" onClick={() => navigateToCarDetails(Number(id), car.id)}>
          {user?.id === selectedCarDealerShip?.personId &&(
            <div className="car-card-actions">
              <button 
              className="action-btn edit-btn"
              onClick={(e) => handleEditClick(e, car)}
              >Edit</button>
              <button 
              className="action-btn delete-btn"
              onClick={(e) => handleDeleteClick(e, car.id)}
              >Delete</button>
            </div>
          )}
            <CarItem car={car}/>
          </div>
        ))}

      </div>
    </div>
  
)
}

export default SingleCarDealerShip
