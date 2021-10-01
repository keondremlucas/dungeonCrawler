using System;
using System.Collections.Generic; 
using System.Linq;

namespace lib
{   
    public static class Scenario
    {
        

            public static void Explore(Database Database)
            {
                Random Random = new Random();
                var Player = Database.Characters.First();
                switch (Player.Location)
                {
                    case "Home":
                    {
                        
                        Console.WriteLine("You wake up in your house... Today is the day you begin your adventure as the Chosen One!");
                        Console.WriteLine("You Grab your Dads Sword");
                        Console.WriteLine("You hear alot of commotion outside.....What would you like to do?");

                        Console.WriteLine("1. Go outside. 2.Check your Items");
                        Player.Items.Add(Database.Items.ToList()[1]);
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
                            Console.WriteLine($"You have 1 {Database.Items.ToList()[1].Name}. {Database.Items.ToList()[1].Description}"); 
                            break;

                        }
                        break;
                    }
                    case "Starting Town":
                    {
                        Console.WriteLine("Now You must Begin Your Journey to defeat FinalBoss");
                        Console.WriteLine("You must Explore the world to Become strong enough to Eventually");
                        Console.WriteLine("Defeat FinalBoss. what will you do next ?");
                        Console.Write("1.Explore World ");
                        if(Database.Map.Where(map=> map.IsDiscovered == 1).Count() > 1){Console.Write("2.Change Location");}
                        var Input = Int32.Parse(Console.ReadLine());
                        switch (Input)
                        {
                            
                            case 1:
                            ExploreWorld(Database);
                            break;

                            case 2:
                            ChangeLocations(Database);
                            break;
                        }
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("1.Explore World 2.Change Location");
                        var Input = Int32.Parse(Console.ReadLine());

                        switch (Input)
                        {
                            
                            case 1:
                            ExploreWorld(Database);
                            break;

                            case 2:
                            ChangeLocations(Database);
                            break;
                        }
                        break;
                    }

                }
            }  
            public static void Fight(Database Database)
            {
                var Player = Database.Characters.First();
                
                //var Weapon = Database.Items.Where(item => Player.Id == item);
                Random Random = new Random();
                var monster = Database.Characters.Where(mon => mon.Type == "Monster").ToList();
                var selectedMonster = monster[Random.Next(0,monster.Count()-1)];
                Console.WriteLine($"YOUVE ENCOUNTERED A LEVEL {selectedMonster.Level} {selectedMonster.Name} WITH HP {selectedMonster.Health} AND ENERGY {selectedMonster.Energy}. {selectedMonster.Description} ");
                bool fighting = true;
                var weapon = Player.Items.First();
                var health = selectedMonster.Health;
                var level = selectedMonster.Level;
                var energy = selectedMonster.Energy;
                while(fighting)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{selectedMonster.Name}: Level: {selectedMonster.Level}  Health: {health} Energy:{energy}");
                    Console.WriteLine();
                    Console.WriteLine($"Level: {Player.Level} Hero  Health: {Player.Health}  Energy:{Player.Energy}   Weapon Level: {weapon.Level}");
                    Console.WriteLine();
                    Console.WriteLine("What will you do?");
                    Console.WriteLine("1.Attack 2.Use an Item/Spell 3.Try to Run");
                    var Input = Int32.Parse(Console.ReadLine());
                    Console.WriteLine();
                    switch (Input)
                    { 
                        case 1:
                        if(Random.Next(0,100)>20)
                        {
                            Console.WriteLine($"You attacked the {selectedMonster.Name} for {(int) Player.Level + (int) weapon.Level} damage");
                            health -= (int) Player.Level + (int) weapon.Level;
                            if(health <=0)
                            {
                                fighting=false;
                                Console.WriteLine($"You Defeated {selectedMonster.Name} and Gained {level} experience");
                                LevelUp(Database,level,player,weapon);
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine($"YOU MISSED!!!!");
                        }
                        if(Random.Next(0,100)>20 && health > 0){
                            Console.WriteLine($"{selectedMonster.Name} attacked the for {level} damage");
                            Player.Health -= level;
                            Database.SaveChanges();

                            if(Player.Health <=0)
                            {
                                fighting=false;
                                Death(Database);
                            }
                        }
                        else if(health > 0)
                        {
                            Console.WriteLine($"YOU MANAGED TO DODGE THE {selectedMonster.Name}'S ATTACK !!!!");
                        }
                        break;
                        case 2:
                        break;
                        case 3:
                        break;
                    }
                }
            }
            public static void ChangeLocations(Database Database)
            {


            }
            public static void ExploreWorld(Database Database)
            {
                Random Random = new Random();
                var locations = Database.Map.Where(map => map.IsDiscovered == 0).ToList();
                var locationOption = locations[Random.Next(0,locations.Count()-1)];
                var Player = Database.Characters.First();
                Player.Location = locationOption.Name;
                locationOption.IsDiscovered = 1;
                Database.SaveChanges();
                Console.WriteLine($"You Have Discovered {locationOption.Name}.{locationOption.Description}");
                if(Random.Next(0,100)>50)
                {
                    Fight(Database);
                }
            }
            public static void Death(Database Database)
            {
                Console.WriteLine("YOU DIED GAME OVER");
                var Player = Database.Characters.First();
                Player.Location = "Home";
                Player.Level = 1;
                Player.Items = new List<Item>();
                Player.Health = 100;
                Player.Energy = 100;


              foreach (var item in Database.Map.Where(map => map.Name != "Starting Town"))
              {
                  item.IsDiscovered =0;
              };
              Database.SaveChanges();
                

            }
            public static void LevelUp(Database Database,int experience,Character player,Item weapon)
            {
                
            }




        
                
    }
}

