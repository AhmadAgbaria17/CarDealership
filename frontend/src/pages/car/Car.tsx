import React, { useEffect } from 'react'
import { useParams } from 'react-router-dom'
import { useDispatch } from 'react-redux'
import type { AppDispatch } from '../../redux/stores'
import { getCarById } from '../../redux/apiCalls/carApiCall'
import { useSelector } from 'react-redux'
import type { RootState } from '../../redux/stores'

const Car: React.FC = () =>{
  const {carId} = useParams();
  const dispatch = useDispatch<AppDispatch>();
  const {car, loading} = useSelector((state:RootState) => state.car);

  useEffect(()=>{
    dispatch(getCarById(Number(carId)));
  },[dispatch])


  if(loading){
    return (
      <div className="car-dealer-ships-spinner-container">
        <div className="car-dealer-ships-spinner" role="status" aria-label="Loading" />
      </div>
    )
  }

  return (
    <div>
      <h2>{car?.company}</h2>
      <p>{car?.color}</p>
        
    </div>
  )
}


export default Car