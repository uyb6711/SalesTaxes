using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxes.Model
{
    /*
     * Tax object organizes tax rule and caculates tax
     */ 
    class Tax
    {
        private const int _importTax = 5; // fixed rate on imported product
        private const int _nonexemptedtax = 10; // fixed rate on nonexempted product

        public enum ExemptedProductType
        {
            Book,
            Food,
            Medical,
            Other
        };

        /*
         * Basic sales tax is on 10% on nonexempted product.
         * Basic sales tax is on 0% on exempted product.
         * Additional 5% tax on imported product.
         */ 
        public static int ComputeTaxRate(Product product)
        {
            int taxRate = 0;
            if (product is NonexemptedProduct)
            {
                taxRate += _nonexemptedtax;
            }
            if(product.IsImported == true)
            {
                taxRate += _importTax;
            }

            return taxRate;
        }
        
        public static decimal ComputeSalesTax(decimal price, int taxRate)
        {
            return RoundUp(price * taxRate / 100);
        }

        public static decimal ComputeTotalSalesTax(decimal salesTax, int quantity)
        {
            return salesTax * quantity;
        }
        
        /*
         * Round up to the nearest 0.05
         */ 
        private static decimal RoundUp(decimal price)
        {
            decimal rounded = Math.Ceiling(price * 20) / 20;
            return rounded;
        }
       
    }
}
