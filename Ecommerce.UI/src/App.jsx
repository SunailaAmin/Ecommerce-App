import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Register";
import AdminRegister from "./pages/AdminRegister";
import Products from "./pages/Products";
import ProtectedRoute from "./components/ProtectedRoute";
import RegisterOptions from "./pages/RegisterOptions";
import Cart from "./pages/Cart";

function App() {
  return (
    <BrowserRouter>
      <Routes>

        <Route
          path="/"
          element={<Login />}
        />

        <Route
          path="/register-options"
          element={<RegisterOptions />}
        />

        <Route
          path="/register"
          element={<Register />}
        />

        <Route
          path="/admin-register"
          element={<AdminRegister />}
        />

        <Route
          path="/products"
          element={
            <ProtectedRoute>
              <Products />
            </ProtectedRoute>
        }
        />

        <Route
          path="/cart"
          element={
            <ProtectedRoute>
              <Cart />
            </ProtectedRoute>
          }
        />

      </Routes>
    </BrowserRouter>
  );
}

export default App;