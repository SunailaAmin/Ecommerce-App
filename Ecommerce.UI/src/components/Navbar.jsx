import { useNavigate } from "react-router-dom";

function Navbar() {
  const navigate = useNavigate();

  const logout = () => {
    localStorage.removeItem("token");
    navigate("/");
  };

  return (
    <div>
      <button onClick={logout}>
        Logout
      </button>
    </div>
  );
}

export default Navbar;