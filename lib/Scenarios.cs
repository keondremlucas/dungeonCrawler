using System;
using System.Collections.Generic; 
using System.Linq;

namespace lib
{   
    public static class Scenario
    {
            public static void Explore(Database Database)
            {
                var Player = Database.Characters.First();
                switch (Player.Location)
                {
                    case "Home":
                    {
                        
                        Console.WriteLine("You wake up in your house... Today is the day you begin your adventure as the Chosen One!");
                        Console.WriteLine("You hear alot of commotion outside.....What would you like to do?");
                        Console.WriteLine("1. Go outside. 2.Check your Items");
                        var Input = Int32.Parse(Console.ReadLine());
                        switch(Input)
                        {
                            case 1:
                            Console.WriteLine("The FINALBOSS IS HERE????!!!!!");
                            Console.WriteLine("HELLO CHOSEN ONE I HAVE STOLEN YOUR FAMILY IF YOU WISH TO EVER SEE THEM AGAIN YOU MUST DEFEAT ME HAHAHAHA!!!!!!! ");
                            Console.WriteLine("I WILL BE WAITING FOR IN MY CASTLE!!!! DONT MAKE ME WAIT TOO LONG I HAVENT EATEN IN A WHILE!!!!");
                            Player.Location = "Starting Town";
                            break;
                            case 2:
                            Console.WriteLine("You dont have any items!"); 
                            break;

                        }
                        break;
                    }
                    case "Starting Town":
                    {
                        Console.WriteLine("Now You must Begin Your Journey to defeat FinalBoss");
                        Console.WriteLine("You must Explore the world to Become strong enough to Eventually");
                        Console.WriteLine("Defeat FinalBoss. what will you do next ?");
                        //ChangeLocation();
                        break;
                    }

                }
            }  
            public static void Fight(Database Database)
            {

            }




        
                
    }
}

