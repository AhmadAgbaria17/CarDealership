import React from 'react'
import type { Car } from '../../interfaces/types'
import './carItem.css'

interface CarItemProps {
    car: Car;
}

const CarItem: React.FC<CarItemProps> = ({car}) => {
    return (
        <div className="car-item-card">
            <div className="car-item-image-container">
                {car.images && car.images.length > 0 ? (
                    <img src={car.images[0]} alt={`${car.company} ${car.modelName}`} className="car-item-image" />
                ) : (
                    <div className="car-item-placeholder">No Image Available</div>
                )}
            </div>
            
            <div className="car-item-header">
                <div>
                    <h3 className="car-item-title">{car.company} {car.modelName}</h3>
                    <p className="car-item-subtitle">{car.year} • {car.type}</p>
                </div>
                <div className="car-item-color" style={{
                    width: '20px', 
                    height: '20px', 
                    borderRadius: '50%', 
                    backgroundColor: car.color, 
                    border: '1px solid #e5e7eb',
                    
                }} />
            </div>

            <p className="car-item-price">${car.price}</p>

            <p className="car-item-description">{car.description}</p>

            <div className="car-item-details-grid">
                <div className="car-item-detail">
                    <span>Fuel:</span> <strong>{car.fuel}</strong>
                </div>
                <div className="car-item-detail">
                    <span>Trans:</span> <strong>{car.transmission}</strong>
                </div>
                <div className="car-item-detail">
                    <span>Mileage:</span> <strong>{car.mileage}</strong>
                </div>
                <div className="car-item-detail">
                    <span>Engine:</span> <strong>{car.engine}</strong>
                </div>
                <div className="car-item-detail">
                    <span>HP:</span> <strong>{car.horsePower}</strong>
                </div>
            </div>
        </div>
    )
}

export default CarItem