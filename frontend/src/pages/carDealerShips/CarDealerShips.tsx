import { useEffect, useState } from "react";
import { getCarDealerShips, deleteCarDealerShip } from "../../redux/apiCalls/carDealerShipsApiCall";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../redux/stores";
import "./CarDealerShips.css";
import CarDealerShipItem from "../../components/carDealerShipItem/CarDealerShipItem";
import type { CarDealerShip } from "../../interfaces/types";
import EditCarDealerShipModal from "../../components/modals/EditCarDealerShipModal";
import swal from "sweetalert";
import { useNavigate } from "react-router-dom";

const CarDealerShips: React.FC = () => {

  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const { carDealerShips, loading } = useSelector((state:RootState) => state.carDealerShips);
  const { user } = useSelector((state:RootState) => state.auth);

  const [editModalOpen, setEditModalOpen] = useState(false);
  const [selectedDealership, setSelectedDealership] = useState<CarDealerShip | null>(null);

  useEffect(()=>{
    dispatch(getCarDealerShips());
  },[dispatch])

  const handleEditClick = (e: React.MouseEvent, dealership: CarDealerShip) => {
    e.stopPropagation();
    setSelectedDealership(dealership);
    setEditModalOpen(true);
  };

  const handleDeleteClick = (e: React.MouseEvent, id: number) => {
    e.stopPropagation();
    swal({
      title: "Are you sure?",
      text: "Once deleted, you will not be able to recover this dealership!",
      icon: "warning",
      buttons: ["Cancel", "Delete"],
      dangerMode: true,
    })
    .then((willDelete) => {
      if (willDelete) {
        dispatch(deleteCarDealerShip(id));
      }
    });
  };

  const navigateToDetails = (id: number) => {
    navigate(`/car-dealer-ships/${id}`);
  }

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
  const myDealerShips = carDealerShips.filter(ship => ship.personId === user?.id);
  const otherDealerShips = carDealerShips.filter(ship => ship.personId !== user?.id);



  return (
    <div className="car-dealer-ships-container">
      {editModalOpen && selectedDealership && (
        <EditCarDealerShipModal 
          dealership={selectedDealership} 
          onClose={() => setEditModalOpen(false)} 
        />
      )}
      
      {myDealerShips.length > 0 && (
        <>
          <h2>My Dealerships</h2>
          <div className="car-dealer-ships-grid">
          {myDealerShips.map((carDealerShip)=>(
              <div key={carDealerShip.id} className="dealership-card-wrapper" onClick={() => navigateToDetails(carDealerShip.id)}>
                <div className="dealership-actions">
                  <button 
                    className="action-btn edit-btn"
                    onClick={(e) => handleEditClick(e, carDealerShip)}
                  >
                    Edit
                  </button>
                  <button 
                    className="action-btn delete-btn"
                    onClick={(e) => handleDeleteClick(e, carDealerShip.id)}
                  >
                    Delete
                  </button>
                </div>
                <CarDealerShipItem carDealerShip={carDealerShip}/>
              </div>
          ))}
        </div>
      </>
      )}

      {otherDealerShips.length > 0 && (
        <>
          <h2>Other Dealerships</h2>
          <div className="car-dealer-ships-grid">
            {otherDealerShips.map((carDealerShip)=>(
               <div key={carDealerShip.id} className="dealership-card-wrapper" onClick={() => navigateToDetails(carDealerShip.id)}>
                <CarDealerShipItem carDealerShip={carDealerShip}/>
              </div>
            ))}
          </div>
        </>
      )}
    </div>
  )
}

export default CarDealerShips

