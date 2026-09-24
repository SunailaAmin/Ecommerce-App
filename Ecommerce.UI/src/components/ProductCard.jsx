function ProductCard({ product, onAddToCart }) {
  return (
    <div
      style={{
        border: "1px solid #ddd",
        borderRadius: "10px",
        padding: "20px",
        width: "250px",
        boxShadow: "0 2px 5px rgba(0,0,0,0.1)"
      }}
    >
      <h3>{product.name}</h3>

      <p>{product.description}</p>

      <h4>₹ {product.price}</h4>

      <p>Stock: {product.stock}</p>

      <button
        onClick={() => onAddToCart(product)}
      >
        Add To Cart
      </button>
    </div>
  );
}

export default ProductCard;