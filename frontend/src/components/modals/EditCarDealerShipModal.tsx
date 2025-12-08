
import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { updateCarDealerShip } from '../../redux/apiCalls/carDealerShipsApiCall';
import type { CarDealerShip } from '../../interfaces/types';
import './EditCarDealerShipModal.css';
import type { AppDispatch } from '../../redux/stores';

interface EditCarDealerShipModalProps {
    dealership: CarDealerShip;
    onClose: () => void;
}

const EditCarDealerShipModal: React.FC<EditCarDealerShipModalProps> = ({ dealership, onClose }) => {
    const dispatch = useDispatch<AppDispatch>();
    const [formData, setFormData] = useState({
        name: dealership.name,
        city: dealership.city,
        address: dealership.address,
        phone: dealership.phone
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await dispatch(updateCarDealerShip(dealership.id, formData));
        onClose();
    };

    return (
        <div className="edit-modal-overlay" onClick={onClose}>
            <div className="edit-modal-content" onClick={e => e.stopPropagation()}>
                <div className="edit-modal-header">
                    <h2>Edit Dealership</h2>
                    <button className="close-button" onClick={onClose}>&times;</button>
                </div>
                <form className="edit-modal-form" onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="name">Dealership Name</label>
                        <input
                            type="text"
                            id="name"
                            name="name"
                            value={formData.name}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="city">City</label>
                        <input
                            type="text"
                            id="city"
                            name="city"
                            value={formData.city}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="address">Address</label>
                        <input
                            type="text"
                            id="address"
                            name="address"
                            value={formData.address}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="phone">Phone</label>
                        <input
                            type="text"
                            id="phone"
                            name="phone"
                            value={formData.phone}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="modal-actions">
                        <button type="button" className="btn-cancel" onClick={onClose}>Cancel</button>
                        <button type="submit" className="btn-save">Save Changes</button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default EditCarDealerShipModal;
