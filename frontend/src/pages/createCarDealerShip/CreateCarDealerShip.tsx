import './CreateCarDealerShip.css';
import { useNavigate } from "react-router-dom";
import { useDispatch } from 'react-redux';
import { useState } from "react";
import { createCarDealerShip } from '../../redux/apiCalls/carDealerShipsApiCall';
import type { CreateCarDealerShips } from '../../interfaces/types';
import type { AppDispatch } from '../../redux/stores';
import { toast } from 'react-toastify';

const CreateCarDealerShip: React.FC = () => {

  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [formData, setFormData] = useState({
    name: "",
    city: "",
    address: "",
    phone: ""
  });

  const handleSubmit = async (e: React.FormEvent)=>{
    e.preventDefault();
    try{
      if(!formData.name || !formData.city || !formData.address || !formData.phone){
        toast.error("All fields are required");
        return;
      }
      setLoading(true);
      const dealerShipData : CreateCarDealerShips = {
        name: formData.name,
        city: formData.city,
        address: formData.address,
        phone: formData.phone,
      };
     await dispatch(createCarDealerShip(dealerShipData));
      navigate('/car-dealer-ships');
    }catch(error){
      toast.error("Failed to create dealership");
    }finally{
    setLoading(false);
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