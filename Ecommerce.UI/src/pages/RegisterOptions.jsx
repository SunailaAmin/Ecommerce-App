import { useNavigate } from "react-router-dom";

function RegisterOptions() {
  const navigate = useNavigate();

  return (
    <div>
      <h2>Select Registration Type</h2>

      <button
        onClick={() =>
          navigate("/register")
        }
      >
        Register as Customer
      </button>

      <br />
      <br />

      <button
        onClick={() =>
          navigate("/admin-register")
        }
      >
        Register as Admin
      </button>
    </div>
  );
}

export default RegisterOptions;