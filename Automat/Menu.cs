using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Automat
{
    class Menu
    {
        //The constructor fills up the machine so it does not have to be done manually
        //This way you can go straight to buying instead of having to wait for someone with admin access to fill it up first
        //I originally had it as something you had to do manually but for the sake of testing it's just easier this way
        public Menu()
        {
            a.FillUp();
        }
        Automat a = new Automat();

        //This is the usermenu. Gives you a few options including switching to the admin menu.It has a loop that repeats until you exit the menu
        public void UserMenu()
        {
            bool repeatUserMenu = true;
            while (repeatUserMenu)
            {
                Console.Clear();
                Console.WriteLine("User Menu\n");
                Console.WriteLine("1. Buy");
                Console.WriteLine("2. Enter admin menu");
                Console.WriteLine("3. Exit");

                int choice = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        bool goOn = true;
                        do
                        {
                            Console.Clear();
                            for (int i = 0; i < a.ProductListCount(); i++)
                            {
                                Console.WriteLine($"{i + 1}. {a.ReturnProductList()[i].Name} {a.ReturnProductList()[i].Price}kr");
                            }
                            Console.WriteLine("Enter the number on the products you want to buy, press # to go back to the menu");
                            string productChoice = Console.ReadLine();
                            if (productChoice == "#")
                                goOn = false;
                            else if (int.Parse(productChoice) > 0 && int.Parse(productChoice) < 6)
                                a.Buy(int.Parse(productChoice));
                            else InvalidEntry();
                        } while (goOn == true);
                        break;
                    case 2:
                        Console.WriteLine("Write password to enter: ");
                        string passwordAttempt = Console.ReadLine();
                        AdminMenu(passwordAttempt);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        InvalidEntry();
                        break;
                }
            }
        }
        //This is the admin menu. It also has a loop that keeps going until you decide to exit the menu
        public void AdminMenu(string password)
        {
            bool repeat = true;
            while (repeat == true )
            {
                if (password == a.CheckPassword())
                {
                    Console.Clear();
                    Console.WriteLine("Admin Menu\n");
                    Console.WriteLine("1. Withdraw money");
                    Console.WriteLine("2. See prices on products/amount of products left");
                    Console.WriteLine("3. Change prices on products");
                    Console.WriteLine("4. Add stock");
                    Console.WriteLine("5. Change admin password");
                    Console.WriteLine("6. Go back to user menu");
                    Console.WriteLine("7. Exit all menus");

                    int choice = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine($"There's currently {a.MoneyInMachine} kr in the vending machine");
                            Console.WriteLine("How much do you want to withdraw?");
                            int withdraw = int.Parse(Console.ReadLine());
                            a.WithdrawMoney(withdraw);
                            break;
                        case 2:
                            for (int i = 0; i < a.ProductListCount(); i++)
                            {
                                Console.WriteLine($"{a.ReturnProductList()[i].Name} Price: {a.ReturnProductList()[i].Price} Amount: {a.ReturnProductList()[i].Stock}");
                            }
                            Console.WriteLine("Press enter to continue");
                            Console.ReadKey();
                            break;
                        case 3:
                            Console.WriteLine("What product do you want to change the price on?");
                            string product = Console.ReadLine();
                            Console.WriteLine("Enter the new price");
                            int newPrice = int.Parse(Console.ReadLine());
                            a.ChangePrice(product, newPrice);
                            break;
                        case 4:
                            Console.WriteLine("What product do you want to fill up?");
                            string name = Console.ReadLine();
                            Console.WriteLine("How much do you want to add?");
                            int extraStock = int.Parse(Console.ReadLine());
                            a.AddStock(name, extraStock);
                            break;
                        case 5:
                            Console.Write("Enter the current password: ");
                            string currentPassword = Console.ReadLine();
                            if (currentPassword != a.CheckPassword())
                            {
                                Console.WriteLine("Invalid password");
                            }
                            else
                            {
                                Console.WriteLine("Enter your new password");
                                Console.WriteLine(a.SetPassword(Console.ReadLine()));
                                Thread.Sleep(2000);
                                UserMenu();
                            }
                            break;
                        case 6:
                            UserMenu();
                            break;
                        case 7:
                            Environment.Exit(0);
                            break;
                        default:
                            InvalidEntry();
                            break;
                    }
                }
                //This part of the code sends people back to the usermenu if they write the wrong admin password
                else
                {
                    InvalidEntry();
                    UserMenu();
                }
            }
        }
        //This is the method that taskes money from the user and tells them how much they need to pay
        public int PutMoneyIn( int remaining)
        {
            Console.Clear();
            Console.WriteLine($"Please put {remaining}kr in the machine");
            return int.Parse(Console.ReadLine());
        }
        //I wanted to have the cancel option but I don't have time to figure out how to make it work right now
        //My idea was to give the user the option to press # to cancel but the system is expecting an int
        //I could probably figure it out by parsing from int to string and the other way around but it'll just be an idea for now
        public void SaleCancelled(int returnMoney)
        {
            Console.WriteLine("The sale was cancelled");
            Console.WriteLine($"{returnMoney}kr was returned to you");
        }
        //This tells the user how much money they're getting in change
        public void SaleFinished(int change)
        {
            Console.WriteLine($"Here's {change}kr in change");
            Thread.Sleep(2000);
        }
        //I found myself needing to write the same 2 lines of code a couple of times so I decided to just throw those lines into a method
        //I need the thread sleep to allow the user to read what happened before it clears the console
        static void InvalidEntry()
        {
            Console.WriteLine("Invalid Entry");
            Thread.Sleep(2000);
        }
    }
}
