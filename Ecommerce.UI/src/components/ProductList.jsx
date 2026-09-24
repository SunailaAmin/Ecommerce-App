import { useEffect, useState } from "react";
import axios from "axios";

function ProductList() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let mounted = true;

    axios
      .get("https://localhost:7064/api/Product")
      .then((response) => {
        if (mounted) {
          setProducts(response.data);
        }
      })
      .catch((error) => {
        console.error("Error loading products", error);
      })
      .finally(() => {
        if (mounted) {
          setLoading(false);
        }
      });

    return () => {
      mounted = false;
    };
  }, []);

  if (loading) {
    return <h2>Loading Products...</h2>;
  }

  return (
    <div
      style={{
        display: "grid",
        gridTemplateColumns:
          "repeat(auto-fit,minmax(300px,1fr))",
        gap: "20px",
        padding: "20px",
      }}
    >
      {products.map((product) => (
        <div
          key={product.id}
          style={{
            border: "1px solid #ddd",
            borderRadius: "10px",
            padding: "15px",
            boxShadow: "0 2px 5px rgba(0,0,0,0.1)",
          }}
        >
          <h2>{product.name}</h2>

          <p>{product.description}</p>

          <h3>₹ {product.price}</h3>

          <p>
            <strong>Stock:</strong> {product.stock}
          </p>

          <button>
            Add To Cart
          </button>
        </div>
      ))}
    </div>
  );
}

export default ProductList;