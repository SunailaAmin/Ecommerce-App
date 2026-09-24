import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { register } from "../services/authService";

function Register() {
  const navigate = useNavigate();

  const [name, setName] =
    useState("");

  const [email, setEmail] =
    useState("");

  const [password, setPassword] =
    useState("");

  const [loading, setLoading] =
    useState(false);

  const [error, setError] =
    useState("");

  const [message, setMessage] =
    useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    setError("");
    setMessage("");

    if (
      !name ||
      !email ||
      !password
    ) {
      setError(
        "Please fill all fields"
      );
      return;
    }

    try {
      setLoading(true);

      await register({
        name,
        email,
        password,
      });

      setMessage(
        "Registration Successful. Redirecting..."
      );

      setName("");
      setEmail("");
      setPassword("");

      setTimeout(() => {
        navigate("/");
      }, 1500);
    }
    catch (error) {
      console.error(error);

      setError(
        error.response?.data ||
        "Registration Failed"
      );
    }
    finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h2>Customer Registration</h2>

      {error && (
        <p style={{ color: "red" }}>
          {error}
        </p>
      )}

      {message && (
        <p style={{ color: "green" }}>
          {message}
        </p>
      )}

      <form onSubmit={handleSubmit}>
        <input
          placeholder="Name"
          value={name}
          onChange={(e) =>
            setName(e.target.value)
          }
        />

        <br />
        <br />

        <input
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
          type="submit"
          disabled={loading}
        >
          {loading
            ? "Registering..."
            : "Register"}
        </button>
      </form>
    </div>
  );
}

export default Register;