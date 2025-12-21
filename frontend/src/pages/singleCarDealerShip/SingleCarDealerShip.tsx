import "./singleCarDealerShip.css";
import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useParams } from 'react-router-dom';
import type { AppDispatch,RootState } from '../../redux/stores';
import { getCarDealerShipById } from '../../redux/apiCalls/carDealerShipsApiCall';
import CarItem from "../../components/carItem/CarItem";

const SingleCarDealerShip: React.FC = () => {
  const dispatch = useDispatch<AppDispatch>();
  const {id} = useParams();
  const {selectedCarDealerShip , loading} = useSelector((state:RootState) => state.carDealerShips);
  


  useEffect(()=>{
    dispatch(getCarDealerShipById(Number(id)));
  },[dispatch])

  const navigate = useNavigate();

  const navigateToCarDetails = (id: number, carId: number) => {
    navigate(`/car-dealer-ships/${id}/${carId}`);
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
      <div className="single-car-dealer-ship-information-container">
        <p><span>name :</span> {selectedCarDealerShip?.name}</p>
        <p><span>city :</span> {selectedCarDealerShip?.city}</p>
        <p><span>address :</span> {selectedCarDealerShip?.address}</p>
        <p><span>phone :</span> {selectedCarDealerShip?.phone}</p>
      </div>

      <h2>cars ({selectedCarDealerShip?.cars?.length})</h2>

      <div className="single-car-dealer-ship-cars-grid">
        {selectedCarDealerShip?.cars?.length === 0 && (
          <p className="single-car-dealer-ship-cars-grid-no-cars">No cars found</p>
        )}
        {selectedCarDealerShip?.cars?.map((car)=>(
          <div key={car.id} className="car-card-wrapper" onClick={() => navigateToCarDetails(Number(id), car.id)}>
            <CarItem car={car}/>
          </div>
        ))}

      </div>
    </div>
  
)
}

export default SingleCarDealerShip
