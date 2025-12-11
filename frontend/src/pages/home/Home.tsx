import "./Home.css";
import { useSelector } from "react-redux";
import type { RootState } from "../../redux/stores";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const Home: React.FC = () => {
  const navigate = useNavigate();
  const { user } = useSelector((state: RootState) => state.auth);

  
  

  const handleCreate = () => {
    if (!user) {
      toast.info("Please log in to create a CarDealerShip.");
      navigate("/login");
      return;
    }
    navigate("/create-car-dealer-ship");
  };

  const handleViewAll = () => {
    navigate("/car-dealer-ships");
  };

  return (
    <div className="home-wrapper">
      <section className="home-hero">
        <div className="hero-content">
          <h1 className="hero-title">Bring Great Cars to the Right People</h1>
          <p className="hero-sub">Create your CarDealerShip, show off collections, or explore all dealerships nearby.</p>

          <div className="hero-actions">
            <button className="btn btn-primary" onClick={handleCreate}>
              Create my Car DealerShip
            </button>

            <button className="btn btn-outline" onClick={handleViewAll}>
              Check out all Car DealerShips
            </button>
          </div>    
          

          {!user && (
            <p className="login-hint">Not logged in? <span className="link" onClick={() => navigate('/login')}>Log in</span> or <span className="link" onClick={() => navigate('/register')}>register</span> to create a dealership.</p>
          )}

        </div>

      </section>

      <section className="features">
        <div className="feature">
          <h3>Fast listings</h3>
          <p>Create and publish your CarDealerShip quickly and reach buyers fast.</p>
        </div>
        <div className="feature">
          <h3>Secure</h3>
          <p>Authenticated users can manage their dealerships and cars safely.</p>
        </div>
        <div className="feature">
          <h3>Discover</h3>
          <p>Explore curated dealerships and compare inventory in one place.</p>
        </div>
      </section>
    </div>
  );
};

export default Home;
