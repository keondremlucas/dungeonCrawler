using System;
using lib;
namespace app
{ 
    class Program
    {
        static void Main(string[] args)
        {   var db = new Database();
            bool run = true;
            while(run)
            {
                Console.WriteLine("You wake up in your house... Today is the day you begin your adventure as the Chosen One!");
                Console.WriteLine("You hear alot of commotion outside.....What would you like to do?");
                Console.WriteLine("1. Go outside. 2.Check your Items");
                var Input = Int32.Parse(Console.ReadLine());
                switch(Input)
                {
                    case 1:
                    Console.WriteLine("The FINAL BOSS IS HERE????!!!!!");
                    Console.WriteLine("HELLO CHOSEN ONE I HAVE STOLEN YOUR FAMILY IF YOU WISH TO EVER SEE THEM AGAIN YOU MUST DEFEAT ME HAHAHAHA!!!!!!! ");
                    Console.WriteLine("I WILL BE WAITING FOR IN MY CASTLE!!!! DONT MAKE ME WAIT TOO LONG I HAVENT EATEN IN A WHILE!!!!");
                    run = false;
                    break;

                    case 2:
                    Console.WriteLine("You dont have any items!"); 
                    break;

                }
                
                
            }

            

        }
    }
}
