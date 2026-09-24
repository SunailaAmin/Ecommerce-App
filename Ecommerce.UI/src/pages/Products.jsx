import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getProducts } from "../services/productService";
import ProductCard from "../components/ProductCard";

function Products() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  const navigate = useNavigate();

  useEffect(() => {
    let cancelled = false;

    getProducts()
      .then((data) => {
        if (!cancelled) {
          setProducts(data);
        }
      })
      .catch((error) => {
        console.error(error);
      })
      .finally(() => {
        if (!cancelled) {
          setLoading(false);
        }
      });

    return () => {
      cancelled = true;
    };
  }, []);

  const addToCart = (product) => {
    const cart =
      JSON.parse(
        localStorage.getItem("cart")
      ) || [];

    cart.push(product);

    localStorage.setItem(
      "cart",
      JSON.stringify(cart)
    );

    alert(`${product.name} added to cart`);
  };

  if (loading) {
    return <h2>Loading...</h2>;
  }

  return (
    <div style={{ padding: "20px" }}>
      <h1>Products</h1>

      <button
        onClick={() => navigate("/cart")}
      >
        Go To Cart
      </button>

      <br />
      <br />

      <div
        style={{
          display: "flex",
          gap: "20px",
          flexWrap: "wrap"
        }}
      >
        {products.map((product) => (
          <ProductCard
            key={product.id}
            product={product}
            onAddToCart={addToCart}
          />
        ))}
      </div>
    </div>
  );
}

export default Products;