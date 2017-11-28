using SalesTaxes.Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SalesTaxes.Model
{
    /*
     * Shopping cart object organizes current total sales tax, price, and items
     */
    public class ShoppingCart
    {
        private HashSet<ShoppingCartItem> _shoppingItems;
        private decimal _totalSalesTax;
        private decimal _totalPrice;

        public ShoppingCart()
        {
            _shoppingItems = new HashSet<ShoppingCartItem>();
            _totalSalesTax = 0;
            _totalPrice = 0;
        }

        public HashSet<ShoppingCartItem> ShoppingItems { get => _shoppingItems; set => _shoppingItems = value; }
        public decimal TotalSalesTax { get => _totalSalesTax; set => _totalSalesTax = value; }
        public decimal TotalPrice { get => _totalPrice; set => _totalPrice = value; }
        
        /*
         * Add new item to shopping cart.
         * Update total price and sales tax.
         */
        public void AddItem(Product product, int quantity)
        {
            bool isExisted = false;
            decimal tempTotalSaleTax = 0;
            decimal tempTotalPrice = 0;
            foreach(ShoppingCartItem sci in ShoppingItems)
            {
                //if customer alreadys buys existing prodcut, then add additional quantity to existing one.
                if (sci.Product.Name.Equals(product.Name))
                {
                    sci.Quantity += quantity;
                    sci.ItemTotalSalesTax = Tax.ComputeTotalSalesTax(sci.ItemSalesTax, sci.Quantity);
                    sci.ItemTotalPrice = sci.ComputeTotalPrice(sci.Product.Price, sci.Quantity, sci.ItemTotalSalesTax);
                    
                    tempTotalSaleTax = Tax.ComputeTotalSalesTax(sci.ItemSalesTax, quantity);
                    tempTotalPrice = sci.ComputeTotalPrice(sci.Product.Price, quantity, tempTotalSaleTax);
                    isExisted = true;
                    break;
                }
            }
            if (!isExisted)
            {
                ShoppingCartItem newItem = new ShoppingCartItem(product, quantity);
                ShoppingItems.Add(newItem);

                tempTotalPrice = newItem.ItemTotalPrice;
                tempTotalSaleTax = newItem.ItemTotalSalesTax;
            }
            TotalSalesTax += tempTotalSaleTax;
            TotalPrice += tempTotalPrice;
        }

        public bool IsExisted(string str)
        {
            str = StringUtils.CapitalizeFirstLetter(str);
            foreach (ShoppingCartItem sci in ShoppingItems)
            {
                if (sci.Product.Name.Equals(str))
                {
                    return true;
                }
            }
            return false;
        }

        /*
         * Returns Product object that matches with a given product name
         */ 
        public Product GetProduct(string str)
        {
            str = StringUtils.CapitalizeFirstLetter(str);
            foreach (ShoppingCartItem sci in ShoppingItems)
            {
                if (sci.Product.Name.Equals(str))
                {
                    return sci.Product;
                }
            }
            return null;
        }
    }

    /*
     * Shopping cart item to organize its individual quantity, total price, and sales tax 
     */
    public class ShoppingCartItem
    {
        private Product _product;
        private int _quantity;
        private decimal _itemTotalPrice;
        private decimal _itemSalesTax;
        private decimal _itemTotalSalesTax;

        public ShoppingCartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            
            ItemSalesTax = Tax.ComputeSalesTax(Product.Price, Product.TaxRate);
            ItemTotalSalesTax = Tax.ComputeTotalSalesTax(ItemSalesTax, quantity);
            ItemTotalPrice = ComputeTotalPrice(Product.Price, quantity, ItemTotalSalesTax);
        }

        public Product Product { get => _product; set => _product = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public decimal ItemTotalPrice { get => _itemTotalPrice; set => _itemTotalPrice = value; }
        public decimal ItemSalesTax { get => _itemSalesTax; set => _itemSalesTax = value; }
        public decimal ItemTotalSalesTax { get => _itemTotalSalesTax; set => _itemTotalSalesTax = value; }

        public decimal ComputeTotalPrice(decimal price, int quantity, decimal totalSalesTax)
        {
            return price * quantity + totalSalesTax;
        }
    }
}
