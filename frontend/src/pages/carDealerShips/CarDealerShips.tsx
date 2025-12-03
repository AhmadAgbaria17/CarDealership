import { useEffect } from "react";
import { getCarDealerShips } from "../../redux/apiCalls/carDealerShipsApiCall";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../redux/stores";
import "./CarDealerShips.css";
import CarDealerShipItem from "../../components/carDealerShipItem/CarDealerShipItem";



const CarDealerShips: React.FC = () => {


  const dispatch = useDispatch<AppDispatch>();
  const { carDealerShips,loading } = useSelector((state:RootState)=>state.carDealerShips);

  useEffect(()=>{
    dispatch(getCarDealerShips());
  },[dispatch])
  if(loading){
    return (
      <div className="car-dealer-ships-spinner-container">
        <div className="car-dealer-ships-spinner" role="status" aria-label="Loading" />
      </div>
    )
  }

  if(!carDealerShips || carDealerShips.length === 0){
    return <p className="car-dealer-ships-empty-state">No car DealerShips found.</p>
  }


  return (
    <div className="car-dealer-ships-grid">
      {carDealerShips.map((carDealerShip)=>(
        <a key={carDealerShip.id} href={`/car-dealer-ships/${carDealerShip.id}`} className="car-dealer-ships-link">
          <CarDealerShipItem carDealerShip={carDealerShip}/>
        </a>
      ))}
    </div>
  )
}

export default CarDealerShips
