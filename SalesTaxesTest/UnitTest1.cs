using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxes.Model;

namespace SalesTaxesTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ShoppingCart shoppingCart = new ShoppingCart();

            ExemptedProduct book1 = new ExemptedProduct("book", false, 12.49m);
            ExemptedProduct book2 = new ExemptedProduct("book", false, 12.49m);
            NonexemptedProduct musicCD = new NonexemptedProduct("music CD", false, 14.99m);
            ExemptedProduct chocolateBar = new ExemptedProduct("chocolate bar", false, 0.85m);

            shoppingCart.AddItem(book1, 1);
            shoppingCart.AddItem(book2, 1);
            shoppingCart.AddItem(musicCD, 1);
            shoppingCart.AddItem(chocolateBar, 1);

            Receipt receipt = new Receipt
            {
                ShoppingCart = shoppingCart
            };

            string expectedResponse = "Book: 24.98 (2 @ 12.49)\nMusic CD: 16.49\nChocolate bar: 0.85\nSales Taxes: 1.50\nTotal: 42.32\n";
            string response = receipt.ToString();
            
            Assert.AreEqual(response, expectedResponse);
        }

        [TestMethod]
        public void TestMethod2()
        {
            ShoppingCart shoppingCart = new ShoppingCart();

            ExemptedProduct chocolate = new ExemptedProduct("imported box of chocolates", true, 10.00m);
            NonexemptedProduct perfume = new NonexemptedProduct("imported bottle of perfume", true, 47.50m);

            shoppingCart.AddItem(chocolate, 1);
            shoppingCart.AddItem(perfume, 1);

            Receipt receipt = new Receipt
            {
                ShoppingCart = shoppingCart
            };

            string expectedResponse = "Imported box of chocolates: 10.50\nImported bottle of perfume: 54.65\nSales Taxes: 7.65\nTotal: 65.15\n";
            string response = receipt.ToString();
            
            Assert.AreEqual(response, expectedResponse);
        }

        [TestMethod]
        public void TestMethod3()
        {
            ShoppingCart shoppingCart = new ShoppingCart();

            NonexemptedProduct perfume = new NonexemptedProduct("imported bottle of perfume", true, 27.99m);
            NonexemptedProduct perfume2 = new NonexemptedProduct("bottle of perfume", false, 18.99m);
            ExemptedProduct medical = new ExemptedProduct("packet of headache pills", false, 9.75m);
            ExemptedProduct chocolate = new ExemptedProduct("imported box of chocolates", true, 11.25m);
            ExemptedProduct chocolate2 = new ExemptedProduct("imported box of chocolates", true, 11.25m);
                        
            shoppingCart.AddItem(perfume, 1);
            shoppingCart.AddItem(perfume2, 1);
            shoppingCart.AddItem(medical, 1);
            shoppingCart.AddItem(chocolate, 1);
            shoppingCart.AddItem(chocolate2, 1);

            Receipt receipt = new Receipt
            {
                ShoppingCart = shoppingCart
            };

            string expectedResponse = "Imported bottle of perfume: 32.19\nBottle of perfume: 20.89\nPacket of headache pills: 9.75\n" +
                "Imported box of chocolates: 23.70 (2 @ 11.85)\nSales Taxes: 7.30\nTotal: 86.53\n";
            string response = receipt.ToString();
            
            Assert.AreEqual(response, expectedResponse);
        }
        
    }
}
