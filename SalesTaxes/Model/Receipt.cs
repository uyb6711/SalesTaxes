using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxes.Model
{
    /*
     * Receipt object helps to visualize a shopping cart
     */ 
    public class Receipt
    {
        private ShoppingCart _shoppingCart;
        
        public Receipt()
        {
            ShoppingCart = new ShoppingCart();
        }

        public ShoppingCart ShoppingCart { get => _shoppingCart; set => _shoppingCart = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (ShoppingCartItem sci in ShoppingCart.ShoppingItems)
            {
                string name = sci.Product.Name;
                decimal price = sci.Product.Price;
                decimal sciTotalPrice = sci.ItemTotalPrice;
                int quantity = sci.Quantity;

                sb.Append(string.Format("{0}: {1}", name, sciTotalPrice.ToString("0.00")));
                if(quantity > 1)
                {
                    sb.Append(string.Format(" ({0} @ {1})", quantity, (price + sci.ItemSalesTax).ToString("0.00")));
                }
                sb.Append("\n");
            }
            sb.Append(string.Format("Sales Taxes: {0}\n", ShoppingCart.TotalSalesTax.ToString("0.00")));
            sb.Append(string.Format("Total: {0}\n", ShoppingCart.TotalPrice.ToString("0.00")));

            return sb.ToString();
        }

    }
}
