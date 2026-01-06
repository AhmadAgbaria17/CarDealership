import './addCar.css';
import { useState } from "react";
import { useDispatch } from "react-redux"
import type { AppDispatch } from "../../redux/stores";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { createCar } from '../../redux/apiCalls/carDealerShipsApiCall';
import type {  CreateCar } from '../../interfaces/types';
import { useParams } from 'react-router-dom';

const AddCar:React.FC = ()=>{

  const CarDealerShipId = useParams().id;
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [formData , setFormData] = useState({
    company: '',
    modelName: '',
    year: '',
    color: '',
    description: '',
    price: '',
    fuel:'',
    transmission:'',
    mileage:'',
    engine:'',
    horsepower:'',
    type:''
  })

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    })
  }

  const handleSubmit = async (e: React.FormEvent)=>{
    e.preventDefault();
    try{
      if(!formData.company || !formData.modelName || !formData.year || !formData.color || !formData.description || !formData.price || !formData.fuel || !formData.transmission || !formData.mileage || !formData.engine || !formData.horsepower || !formData.type){
        toast.error("All fields are required");
        return;
      }
      setLoading(true);
      const carData: CreateCar= {
        company: formData.company,
        modelName: formData.modelName,
        year: Number(formData.year),
        color: formData.color,
        description: formData.description,
        price: formData.price,
        fuel: formData.fuel,
        transmission: formData.transmission,
        mileage: formData.mileage,
        engine: formData.engine,
        horsePower: formData.horsepower,
        type: formData.type
      }
      await dispatch(createCar(carData, CarDealerShipId));
      navigate(`/car-dealer-ships/${CarDealerShipId}`);
    }catch(error){
      toast.error("Failed to add car");
    }finally{
      setLoading(false);
    }
  }



  return(
    <div className="add-car-container">
      <div className="add-car-header">
        <h2>Add Car</h2>
      </div>
      <form className="add-car-form" onSubmit={handleSubmit}>
        <div className='two-groups'>
            <div>
            <div className="add-car-form-group">
          <label htmlFor="company">Company</label>
          <input type="text" id="company" name="company" value={formData.company} onChange={handleChange} />
        </div>
        <div className="add-car-form-group">
          <label htmlFor="modelName">Model Name</label>
          <input type="text" id="modelName" name="modelName" value={formData.modelName} onChange={handleChange} />
        </div>
        <div className="add-car-form-group">
          <label htmlFor="year">Year</label>
          <input type="text" id="year" name="year" value={formData.year} onChange={handleChange} />
        </div>
        <div className="add-car-form-group">
          <label htmlFor="color">Color</label>
          <input type="text" id="color" name="color" value={formData.color} onChange={handleChange} />
        </div>
        <div className="add-car-form-group">
          <label htmlFor="description">Description</label>
          <input type="text" id="description" name="description" value={formData.description} onChange={handleChange} />
        </div>
        <div className="add-car-form-group">
          <label htmlFor="price">Price</label>
          <input type="text" id="price" name="price" value={formData.price} onChange={handleChange} />
        </div>
        </div>
        <div>
          <div className="add-car-form-group">
          <label htmlFor="fuel">Fuel</label>
          <input type="text" id="fuel" name="fuel" value={formData.fuel} onChange={handleChange} />
        </div>
        <div className="add-car-form-group">
          <label htmlFor="transmission">Transmission</label>
          <input type="text" id="transmission" name="transmission" value={formData.transmission} onChange={handleChange} />
        </div>
        <div className="add-car-form-group">
          <label htmlFor="mileage">Mileage</label>
          <input type="text" id="mileage" name="mileage" value={formData.mileage} onChange={handleChange} />
        </div>
        <div className="add-car-form-group">
          <label htmlFor="engine">Engine</label>
          <input type="text" id="engine" name="engine" value={formData.engine} onChange={handleChange} />
        </div>
        <div className="add-car-form-group">
          <label htmlFor="horsepower">Horsepower</label>
          <input type="text" id="horsepower" name="horsepower" value={formData.horsepower} onChange={handleChange} />
        </div>
        <div className="add-car-form-group">
          <label htmlFor="type">Type</label>
          <input type="text" id="type" name="type" value={formData.type} onChange={handleChange} />
        </div>
        </div>
        </div>
        <button type="submit" className="add-car-button" onClick={handleSubmit} disabled={loading}>Add Car</button>
      </form>
      

    </div>
  )
}

export default AddCar