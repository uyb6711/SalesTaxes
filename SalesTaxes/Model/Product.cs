using SalesTaxes.Model.Utils;
using SalesTaxes.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SalesTaxes.Model
{

    /*
     * Product object organizes product properties
     */
    public abstract class Product
    {
        private string _name;
        private bool _isImported;
        private decimal _price;
        private int _taxRate;

        public Product(string description, bool isImported, decimal price)
        {
            Name = description;
            IsImported = isImported;
            Price = price;
            TaxRate = Tax.ComputeTaxRate(this);
        }

        public string Name { get => _name; set => _name = StringUtils.CapitalizeFirstLetter(value); }
        public bool IsImported { get => _isImported; set => _isImported = value; }
        public decimal Price { get => _price; set => _price = value; }
        public int TaxRate { get => _taxRate; set => _taxRate = value; }
        
    }

    public class ExemptedProduct : Product
    {
        public ExemptedProduct(string name, bool isImported, decimal price) :
            base(name, isImported, price)
        {
        }
    }

    public class NonexemptedProduct : Product
    {
        public NonexemptedProduct(string name, bool isImported, decimal price) :
            base(name, isImported, price)
        {
        }
    }
}
