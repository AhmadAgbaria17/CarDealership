import { useDispatch } from "react-redux";
import type { Car } from "../../interfaces/types";
import type { AppDispatch } from "../../redux/stores";
import { useState } from "react";
import { updateCar } from "../../redux/apiCalls/carDealerShipsApiCall";
import { toast } from "react-toastify";
import "./EditCarModal.css";

interface EditCarModalProps {
  car: Car;
  onClose: () => void;
}

const EditCarModal:React.FC<EditCarModalProps> = ({car,onClose}) => {

  const dispatch = useDispatch<AppDispatch>();
  const [formData , setFormData] = useState({
    company: car.company,
    modelName: car.modelName,
    year: car.year,
    color: car.color,
    description: car.description,
    price: car.price,
    fuel: car.fuel,
    transmission: car.transmission,
    mileage: car.mileage,
    engine: car.engine,
    horsePower: car.horsePower,
    type: car.type,
    images: car.images || [],
  })
  const [imagePreviews, setImagePreviews] = useState<string[]>(car.images || []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleImageChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    if (!files) return;

    const imageFiles = Array.from(files).filter(file => file.type.startsWith('image/'));
    const invalidFiles = Array.from(files).filter(file => !file.type.startsWith('image/'));

    if (invalidFiles.length > 0) {
      invalidFiles.forEach(file => {
        toast.error(`${file.name} is not an image file`);
      });
    }

    if (imageFiles.length === 0) return;

    try {
      const imagePromises = imageFiles.map((file) => {
        return new Promise<string>((resolve, reject) => {
          const reader = new FileReader();
          reader.onloadend = () => {
            const base64String = reader.result as string;
            resolve(base64String);
          };
          reader.onerror = reject;
          reader.readAsDataURL(file);
        });
      });

      const base64Images = await Promise.all(imagePromises);
      const updatedImages = [...formData.images, ...base64Images];
      setFormData({ ...formData, images: updatedImages });
      setImagePreviews([...imagePreviews, ...base64Images]);
    } catch {
      toast.error('Failed to process images');
    }
  };

  const removeImage = (index: number) => {
    const updatedImages = formData.images.filter((_, i) => i !== index);
    const updatedPreviews = imagePreviews.filter((_, i) => i !== index);
    setFormData({ ...formData, images: updatedImages });
    setImagePreviews(updatedPreviews);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    dispatch(updateCar(car.id, formData));
    onClose();
  };

  return (
    <div className="edit-modal-overlay" onClick={onClose}>
            <div className="edit-modal-content" onClick={e => e.stopPropagation()}>
                <div className="edit-modal-header">
                    <h2>Edit Car</h2>
                    <button className="close-button" onClick={onClose}>&times;</button>
                </div>
                <form className="edit-modal-form" onSubmit={handleSubmit}>
                  <div className="both-group">
                    <div>
                        <div className="form-group">
                        <label htmlFor="company">Company</label>
                        <input
                            type="text"
                            id="company"
                            name="company"
                            value={formData.company}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="modelName">Model Name</label>
                        <input
                            type="text"
                            id="modelName"
                            name="modelName"
                            value={formData.modelName}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="year">Year</label>
                        <input
                            type="text"
                            id="year"
                            name="year"
                            value={formData.year}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="color">Color</label>
                        <input
                            type="text"
                            id="color"
                            name="color"
                            value={formData.color}
                            onChange={handleChange}
                            required
                        />
                    </div>
                  
                    </div>
                    <div>
                    <div className="form-group">
                        <label htmlFor="description">Description</label>
                        <input
                            type="text"
                            id="description"
                            name="description"
                            value={formData.description}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="price">Price</label>
                        <input
                            type="text"
                            id="price"
                            name="price"
                            value={formData.price}
                            onChange={handleChange}
                            required
                        />
                    </div>
                        <div className="form-group">
                        <label htmlFor="fuel">Fuel</label>
                        <input
                            type="text"
                            id="fuel"
                            name="fuel"
                            value={formData.fuel}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="transmission">Transmission</label>
                        <input
                            type="text"
                            id="transmission"
                            name="transmission"
                            value={formData.transmission}
                            onChange={handleChange}
                            required
                        />
                    </div>
                  
                    </div>
                    <div>
                    <div className="form-group">
                        <label htmlFor="mileage">Mileage</label>
                        <input
                            type="text"
                            id="mileage"
                            name="mileage"
                            value={formData.mileage}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="engine">Engine</label>
                        <input
                            type="text"
                            id="engine"
                            name="engine"
                            value={formData.engine}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="horsePower">Horsepower</label>
                        <input
                            type="text"
                            id="horsePower"
                            name="horsePower"
                            value={formData.horsePower}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="type">Type</label>
                        <input
                            type="text"
                            id="type"
                            name="type"
                            value={formData.type}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    </div>

                  </div>
                
                  <div className="form-group">
                    <label htmlFor="images">Car Images</label>
                    <input 
                      type="file" 
                      id="images" 
                      name="images" 
                      accept="image/*" 
                      multiple 
                      onChange={handleImageChange}
                    />
                  </div>

                  {imagePreviews.length > 0 && (
                    <div className="image-preview-container">
                      <h3>Car Images ({imagePreviews.length})</h3>
                      <div className="image-preview-grid">
                        {imagePreviews.map((preview, index) => (
                          <div key={index} className="image-preview-item">
                            <img src={preview} alt={`Car image ${index + 1}`} className="image-preview" />
                            <button 
                              type="button" 
                              className="remove-image-button" 
                              onClick={() => removeImage(index)}
                            >
                              Remove
                            </button>
                          </div>
                        ))}
                      </div>
                    </div>
                  )}
                
                    <div className="modal-actions">
                        <button type="button" className="btn-cancel" onClick={onClose}>Cancel</button>
                        <button type="submit" className="btn-save">Save Changes</button>
                    </div>
                </form>
            </div>
        </div>
  )
}

export default EditCarModal