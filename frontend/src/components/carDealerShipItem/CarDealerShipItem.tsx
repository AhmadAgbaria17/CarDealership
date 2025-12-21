import React from 'react'
import type { CarDealerShip } from '../../interfaces/types'
import "./carDealerSShipItem.css"

interface CarDealerShipItemProps {
  carDealerShip:CarDealerShip
}

const CarDealerShipItem: React.FC<CarDealerShipItemProps> = ({carDealerShip}) => {
  return (
    <div className="car-dealership-card">
      <div className="car-dealership-card__header">
        <h3>{carDealerShip.name}</h3>
        <span>{carDealerShip.city}</span>
      </div>
      <p>{carDealerShip.address}</p>
      <p className="car-dealership-card__contact">
        Phone: <strong>{carDealerShip.phone}</strong>
      </p>
      <p>amount: {carDealerShip.cars?.length}</p>
      <p className="car-dealership-card__created">Added by {carDealerShip.createdBy}</p>

    </div>
  )
}

export default CarDealerShipItem
