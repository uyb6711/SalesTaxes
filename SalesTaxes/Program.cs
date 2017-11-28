using SalesTaxes.Model;
using SalesTaxes.Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxes
{
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            Product product;
            string input;
            decimal productPrice;
            int productQuantity;
            string productName, isImported, isExempted, answer;
            
            do
            {
                Console.WriteLine("Please enter the name of product");
                productName = Console.ReadLine();

                if (shoppingCart.IsExisted(productName))
                {
                    Console.WriteLine("This product is already in shopping cart");
                    Product p = shoppingCart.GetProduct(productName);
                    isImported = p.IsImported? "y" : "n";
                    productPrice = p.Price;
                    isExempted = p is ExemptedProduct ? "y" : "n";
                }
                else
                {
                    Console.WriteLine("Is this product imported? (y/n)");
                    isImported = Console.ReadLine();
                    isImported = StringUtils.ValidateYesOrNo(isImported);
                    Console.WriteLine("Please enter the price of product");
                    input = Console.ReadLine();
                    productPrice = StringUtils.ValidateCurrency(input);
                    Console.WriteLine("Is this product a type of book, food, or medical produt? (y/n)");
                    isExempted = Console.ReadLine();
                    isExempted = StringUtils.ValidateYesOrNo(isExempted);
                }
                
                Console.WriteLine("Please enter the quantity of product");
                input = Console.ReadLine();
                productQuantity = StringUtils.ValidateNumber(input);
                

                if (isExempted.Equals("y"))
                {
                    product = new ExemptedProduct(productName, StringUtils.IsImported(isImported), Convert.ToDecimal(productPrice));
                }
                else
                {
                    product = new NonexemptedProduct(productName, StringUtils.IsImported(isImported), Convert.ToDecimal(productPrice));
                }

                shoppingCart.AddItem(product, productQuantity);


                Console.WriteLine("Do you want to add more product? (y/n)");
                answer = Console.ReadLine();
                answer = StringUtils.ValidateYesOrNo(answer);
            } while (answer.Equals("y"));
            
            Receipt receipt = new Receipt
            {
                ShoppingCart = shoppingCart
            };
            string response = receipt.ToString();
            Console.WriteLine(response);
            Console.ReadKey();
        }
    }
}
