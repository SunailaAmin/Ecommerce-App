import { useEffect, useState } from "react";
import { getProducts } from "../services/productService";

function Home() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    const loadProducts = async () => {
      const data = await getProducts();
      setProducts(data);
    };

    loadProducts();
  }, []);

  return (
    <div>
      <h1>Products</h1>

      {products.map((product) => (
        <div key={product.id}>
          <h3>{product.name}</h3>
          <p>{product.description}</p>
          <p>₹ {product.price}</p>
        </div>
      ))}
    </div>
  );
}

export default Home;