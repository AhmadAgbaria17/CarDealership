import './CreateCarDealerShip.css';
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { createCarDealerShip } from '../../redux/apiCalls/carDealerShipsApiCall';
import type { CreateCarDealerShips } from '../../interfaces/types';


const CreateCarDealerShip: React.FC = () => {

  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [formData, setFormData] = useState({
    name: "",
    city: "",
    address: "",
    phone: ""
  });

  const handleSubmit = (e: React.FormEvent)=>{
    e.preventDefault();
    try{
      setLoading(true);
      const dealerShipData : CreateCarDealerShips = {
        name: formData.name,
        city: formData.city,
        address: formData.address,
        phone: formData.phone,
      };
     createCarDealerShip(dealerShipData);
    }catch(error){
      console.log(error);
    }finally{
      setLoading(false);
      navigate("/car-dealer-ships");
    }
  }
  

    return (
        <div className="create-car-dealer-ship-container">
            <div className="create-car-dealer-ship-header">
              <h2>Create a car DealerShip</h2>
            </div>
            <form className="create-car-dealer-ship-form" onSubmit={handleSubmit}>
              <div className="create-car-dealer-ship-form-group">
                <label htmlFor="name">Name</label>
                <input type="text" id="name" name="name" value={formData.name} onChange={(e)=>setFormData({...formData, name: e.target.value})} />
              </div>
              <div className="create-car-dealer-ship-form-group">
                <label htmlFor="city">City</label>
                <input type="text" id="city" name="city" value={formData.city} onChange={(e)=>setFormData({...formData, city: e.target.value})} />
              </div>
              <div className="create-car-dealer-ship-form-group">
                <label htmlFor="address">Address</label>
                <input type="text" id="address" name="address" value={formData.address} onChange={(e)=>setFormData({...formData, address: e.target.value})} />
              </div>
              <div className="create-car-dealer-ship-form-group">
                <label htmlFor="phone">Phone</label>
                <input type="text" id="phone" name="phone" value={formData.phone} onChange={(e)=>setFormData({...formData, phone: e.target.value})} />
              </div>
              <button type="submit" className="create-car-dealer-ship-button" onClick={handleSubmit} disabled={loading}>Create</button>
            </form>
        </div>
    )
}

export default CreateCarDealerShip