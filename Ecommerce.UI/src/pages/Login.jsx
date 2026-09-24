import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { login } from "../services/authService";

function Login() {
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleLogin = async () => {
    setError("");

    if (!email || !password) {
      setError("Please enter email and password");
      return;
    }

    try {
      setLoading(true);

      const response = await login({
        email,
        password
      });

      localStorage.setItem(
        "token",
        response.data.token
      );

      navigate("/products");
    }
    catch (error) {
      console.error(error);

      setError(
        error.response?.data ||
        "Invalid email or password"
      );
    }
    finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h2>Login</h2>

      {error && (
        <p style={{ color: "red" }}>
          {error}
        </p>
      )}

      <input
        type="email"
        placeholder="Email"
        value={email}
        onChange={(e) =>
          setEmail(e.target.value)
        }
      />

      <br />
      <br />

      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(e) =>
          setPassword(e.target.value)
        }
      />

      <br />
      <br />

      <button
        onClick={handleLogin}
        disabled={loading}
      >
        {loading
          ? "Logging in..."
          : "Login"}
      </button>

      <br />
      <br />

      <p>
        New User?{" "}
        <Link to="/register-options">
          Register Here
        </Link>
      </p>
    </div>
  );
}

export default Login;