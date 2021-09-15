using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat
{
    class Product
    {
        //Empty constructor when I want to access some of the variables but don't want to make a new product
        public Product()
        {

        }
        //Constructor for creating the products
        public Product(string name, int price, int stock)
        {
            this.name = name;
            this.price = price;
            this.stock = stock;
        }
        List<Product> products = new List<Product>() { };
        private string name;
        private int price;
        private int stock;

        #region GetSet
        public List<Product> Products
        {
            get
            {
                return products;
            }
            set
            {
                products = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
            }
        }
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public int Stock
        {
            get
            {
                return stock;
            }
            set
            {
                stock = value;
            }
        }
        #endregion
    }
}
