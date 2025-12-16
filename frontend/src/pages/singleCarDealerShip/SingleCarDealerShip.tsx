import React from 'react'
import { useParams } from 'react-router-dom';

const SingleCarDealerShip: React.FC = () => {
  const {id} = useParams();
    return (
        <div>
            <h2>Single Car Dealer Ship</h2>
            <p>{id}</p>
        </div>
    )
}

export default SingleCarDealerShip
