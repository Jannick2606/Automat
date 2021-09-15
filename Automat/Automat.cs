using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat
{
    //This class is what I would consider the logic layer
    class Automat
    {
        Product p = new Product();

        Product cola = new Product("Cola", 20, 10);
        Product pepsi = new Product("Pepsi", 20, 10);
        Product yankie = new Product("Yankie", 10, 15);
        Product juice = new Product("Juice", 15, 12);
        Product skittles = new Product("Skittles", 25, 8);

        //Constructor. It doesn't need to take any arguments
        public Automat()
        {

        }
        //The method that is called when someone wants to buy something. Takes the productnumber as an argument
        //It calls methods in the menu class when it needs to write something to the user and take input from the user
        public void Buy(int productNumber)
        {
            Menu m = new Menu();
            Product product = null;
            int amountPaid=0;
            do
            {

            switch (productNumber)
            {
                case 1:
                        product = cola;
                    break;
                case 2:
                        product = pepsi;
                    break;
                case 3:
                        product = yankie;
                    break;
                case 4:
                        product = juice;
                    break;
                case 5:
                        product = skittles;
                    break;
                default:
                    break;
            }
                amountPaid += m.PutMoneyIn(product.Price-amountPaid);
            } while (product.Price>amountPaid);
            m.SaleFinished(amountPaid - product.Price);
            moneyInMachine += product.Price;
        }
        //I use this method when I need to know how long a for loop in menu needs to run
        public int ProductListCount()
        {
            return p.Products.Count();
        }
        //It returns the list of products in the product class
        //I use it as a bridge between menu and product
        //When I need to print the name, price or stock in the menu class I call this method so I don't go directly from menu to product
        public List<Product> ReturnProductList()
        {
            return p.Products;
        }
        //This methods gives the admin an option to withdraw money from the vending machine
        //If I had more time I would make sure that you can't withdraw more money than what's in the machine
        public void WithdrawMoney(int withdraw)
        {
            moneyInMachine -= withdraw;
        }
        //This method fills up the machine at the start
        public void FillUp()
        {  
            p.Products.Add(cola);
            p.Products.Add(pepsi);
            p.Products.Add(yankie);
            p.Products.Add(juice);
            p.Products.Add(skittles);
        }
        //Gives the option to add extra stock in the admin menu
        //With more time I would make sure that if you write a negative number, it can't be less than what's currently in the machine
        public void AddStock(string product, int addedStock)
        {
            switch (product.ToLower())
            {
                case "cola":
                    cola.Stock += addedStock;
                    break;
                case "pepsi":
                    pepsi.Stock += addedStock;
                    break;
                case "yankie":
                    yankie.Stock += addedStock;
                    break;
                case "juice":
                    juice.Stock += addedStock;
                    break;
                case "skittles":
                    skittles.Stock += addedStock;
                    break;
                default:
                    break;
            }
        }
        //Changes the price on the item I want
        public void ChangePrice( string name, int price )
        {
            switch (name.ToLower())
            {
                case "cola":
                    cola.Price = price;
                break;
                case "pepsi":
                    pepsi.Price = price;
                    break;
                case "yankie":
                    yankie.Price = price;
                    break;
                case "juice":
                    juice.Price = price;
                    break;
                case "skittles":
                    skittles.Price = price;
                    break;
                default:
                    break;
            }
        }
        //This is what I use to check if the password is correct
        public string CheckPassword()
        {
            return password;
        }
        //This is what I use to change the password
        public string SetPassword(string newPassword)
        {
            password = newPassword;
            return "Password has been set";
        }

        //I'm gonna have password as a string even though its a number cause I don't need to do any calculations etc
        //so it doesn't need to be able to do the things an int could do
        //I decided to put a variable called moneyInMachine in this class and it counts up and down depending if the admin withdraws money or if a user buys something
        private string password = "1";
        private int moneyInMachine = 50;
        public int MoneyInMachine
        {
            get
            {
                return moneyInMachine;
            }
            set
            {
                moneyInMachine = value;
            }
        }
    }
}
