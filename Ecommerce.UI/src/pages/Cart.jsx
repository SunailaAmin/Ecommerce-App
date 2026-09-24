import { useState } from "react";

function Cart() {
  const [cart] = useState(() =>
    JSON.parse(localStorage.getItem("cart")) || []
  );

  return (
    <div>
      <h1>My Cart</h1>

      {cart.map((item, index) => (
        <div key={index}>
          <h3>{item.name}</h3>
          <p>₹ {item.price}</p>
        </div>
      ))}
    </div>
  );
}

export default Cart;