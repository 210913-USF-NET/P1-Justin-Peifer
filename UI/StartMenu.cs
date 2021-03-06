using System;
using Models;
using SBL;
using DL;
namespace UI
{
    public class StartMenu : IMenu
    {
        public void Start(){
            bool exit = false;
            string input = "";
            
            do{
                Console.WriteLine("Welcome to the Beelicious app!");
                Console.WriteLine("Press [1] to login.");
                Console.WriteLine("Press [2] to create a new account");
                Console.WriteLine("Press[x] to exit.");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        MenuFactory.GetMenu("login").Start();
                        exit = true;
                        break;

                    case "2":
                        MenuFactory.GetMenu("newuser").Start();
                        exit = true;
                        break;
                    case "3":
                        MenuFactory.GetMenu("manager").Start();
                        exit = true;
                        break;

                    case "x":
                        Console.WriteLine("Goodbye!");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (!exit);
            }
    }
}