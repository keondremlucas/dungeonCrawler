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
                        switch (Input)
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
                        if (Database.Map.Where(map => map.IsDiscovered == 1).Count() > 1) { Console.Write("2.Change Location"); }
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
            var selectedMonster = monster[Random.Next(0, monster.Count() - 1)];
            Console.WriteLine($"YOUVE ENCOUNTERED A LEVEL {selectedMonster.Level} {selectedMonster.Name} WITH HP {selectedMonster.Health} AND ENERGY {selectedMonster.Energy}. {selectedMonster.Description} ");
            bool fighting = true;
            var weapon = Player.Items.First();
            var health = selectedMonster.Health;
            var level = selectedMonster.Level;
            var energy = selectedMonster.Energy;

            while (fighting)
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
                        if (Random.Next(0, 100) > 20)
                        {
                            Console.WriteLine($"You attacked the {selectedMonster.Name} for {(int)Player.Level + (int)weapon.Level} damage");
                            health -= (int)Player.Level + (int)weapon.Level;
                            if (health <= 0)
                            {
                                fighting = false;
                                Console.WriteLine($"You Defeated {selectedMonster.Name} and Gained {level} experience");
                                LevelUp(Database, level, Player, weapon);

                                if (Random.Next(0, 100) > 50)
                                {
                                    Loot(Database, Player);
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine($"YOU MISSED!!!!");
                        }
                        if (Random.Next(0, 100) > 20 && health > 0)
                        {
                            Console.WriteLine($"{selectedMonster.Name} attacked the for {level} damage");
                            Player.Health -= level;
                            Database.SaveChanges();

                            if (Player.Health <= 0)
                            {
                                fighting = false;
                                Death(Database);
                            }
                        }
                        else if (health > 0)
                        {
                            Console.WriteLine($"YOU MANAGED TO DODGE THE {selectedMonster.Name}'S ATTACK !!!!");
                        }
                        break;

                    case 2:

                        var items = Player.Items.Where(item => item.Type != "Weapon" && item.Type != "Useless");

                        if (items.Count() > 0)
                        {


                            foreach (var item in items)
                            {

                                Console.WriteLine($"Item Id: {item.Id} Item Name: {item.Name} Description: {item.Description}");
                            }

                            Console.WriteLine("Enter the id of the item you want to use: ");
                            var itemChoice = Int32.Parse(Console.ReadLine());

                            var useItem = Player.Items.Where(item => item.Id == itemChoice).First();

                            if (useItem.Type == "Healing")
                            {

                                if (Player.Health + useItem.Level > ((int)Player.Level * 100))
                                {
                                    Player.Health = (int)Player.Level * 100;
                                    Player.Items.Remove(useItem);
                                    Database.SaveChanges();
                                }
                                else
                                {
                                    Player.Health += useItem.Level;
                                    Player.Items.Remove(useItem);
                                    Database.SaveChanges();
                                }

                                if (Random.Next(0, 100) > 20 && health > 0)
                                {
                                    Console.WriteLine($"{selectedMonster.Name} attacked the for {level / 10} damage");
                                    Player.Health -= level;
                                    Database.SaveChanges();

                                    if (Player.Health <= 0)
                                    {
                                        fighting = false;
                                        Death(Database);
                                    }
                                }
                                else if (health > 0)
                                {
                                    Console.WriteLine($"YOU MANAGED TO DODGE THE {selectedMonster.Name}'S ATTACK !!!!");
                                }


                            }
                            else if (useItem.Type == "Spell")
                            {

                                if (Player.Energy > useItem.Level)
                                {

                                    Console.WriteLine($"You attacked the {selectedMonster.Name} for {(int)Player.Level + (int)useItem.Level} damage");
                                    Player.Energy -= useItem.Level;
                                    health -= (int)Player.Level + (int)weapon.Level;
                                    Database.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine("You don't have enough energy to cast the SPELL!!!");
                                }

                                if (health <= 0)
                                {
                                    fighting = false;
                                    Console.WriteLine($"You Defeated {selectedMonster.Name} and Gained {level / 10} experience");
                                    LevelUp(Database, level, Player, weapon);

                                    if (Random.Next(0, 100) > 50)
                                    {
                                        Loot(Database, Player);
                                    }
                                }

                                if (Random.Next(0, 100) > 20 && health > 0)
                                {
                                    Console.WriteLine($"{selectedMonster.Name} attacked the for {level} damage with {useItem.Name}");
                                    Player.Health -= level;
                                    Database.SaveChanges();

                                    if (Player.Health <= 0)
                                    {
                                        fighting = false;
                                        Death(Database);
                                    }
                                }
                                else if (health > 0)
                                {
                                    Console.WriteLine($"YOU MANAGED TO DODGE THE {selectedMonster.Name}'S ATTACK !!!!");
                                }


                            }
                            else if (useItem.Type == "Damage")
                            {

                                if (Player.Energy > useItem.Level)
                                {

                                    Console.WriteLine($"You attacked the {selectedMonster.Name} for {(int)Player.Level + (int)useItem.Level} damage with {useItem.Name}.");
                                    Player.Energy -= useItem.Level;
                                    health -= (int)Player.Level + (int)weapon.Level;
                                    Database.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine("You don't have enough energy to use the BOMB!!!");
                                }

                                if (health <= 0)
                                {
                                    fighting = false;
                                    Console.WriteLine($"You Defeated {selectedMonster.Name} and Gained {level / 10} experience");
                                    LevelUp(Database, level, Player, weapon);

                                    if (Random.Next(0, 100) > 50)
                                    {
                                        Loot(Database, Player);
                                    }
                                }

                                if (Random.Next(0, 100) > 20 && health > 0)
                                {
                                    Console.WriteLine($"{selectedMonster.Name} attacked the for {level} damage");
                                    Player.Health -= level;
                                    Database.SaveChanges();

                                    if (Player.Health <= 0)
                                    {
                                        fighting = false;
                                        Death(Database);
                                    }
                                }
                                else if (health > 0)
                                {
                                    Console.WriteLine($"YOU MANAGED TO DODGE THE {selectedMonster.Name}'S ATTACK !!!!");
                                }

                            }
                            else if (useItem.Type == "Food")
                            {

                                if (Player.Energy + useItem.Level > ((int)Player.Level * 100))
                                {
                                    Player.Energy = (int)Player.Level * 100;
                                    Player.Items.Remove(useItem);
                                    Database.SaveChanges();
                                }
                                else
                                {
                                    Player.Energy += useItem.Level;
                                    Player.Items.Remove(useItem);
                                    Database.SaveChanges();
                                }

                                if (Random.Next(0, 100) > 20 && health > 0)
                                {
                                    Console.WriteLine($"{selectedMonster.Name} attacked the for {level} damage");
                                    Player.Health -= level;
                                    Database.SaveChanges();

                                    if (Player.Health <= 0)
                                    {
                                        fighting = false;
                                        Death(Database);
                                    }
                                }
                                else if (health > 0)
                                {
                                    Console.WriteLine($"YOU MANAGED TO DODGE THE {selectedMonster.Name}'S ATTACK !!!!");
                                }
                            }

                        }
                        else
                        {

                            Console.WriteLine("You don't have any useful items in inventory. You are unable to use any items.");
                        }

                        break;

                    case 3:
                        if (level < Player.Level)
                        {
                            fighting = false;
                            Console.WriteLine("You managed to escape!");
                            break;

                        }
                        else
                        {

                            if (Random.Next(0, 100) > 60)
                            {
                                fighting = false;
                                Console.WriteLine("You managed to escape!");
                                break;

                            }
                            else
                            {
                                Console.WriteLine("You were unable to escape.");
                            }

                        }

                        if (Random.Next(0, 100) > 20 && health > 0)
                        {
                            Console.WriteLine($"{selectedMonster.Name} attacked the for {level} damage");
                            Player.Health -= level;
                            Database.SaveChanges();

                            if (Player.Health <= 0)
                            {
                                fighting = false;
                                Death(Database);
                            }
                        }
                        else if (health > 0)
                        {
                            Console.WriteLine($"YOU MANAGED TO DODGE THE {selectedMonster.Name}'S ATTACK !!!!");
                        }
                        break;
                }
            }
        }
        public static void ChangeLocations(Database Database)
        {
            var discoveredLocations = Database.Map.Where(loc => loc.IsDiscovered == 1).ToList();

            Console.WriteLine("These are the locations you have discovered: ");
            Console.WriteLine();

            foreach (var loc in discoveredLocations)
            {
                Console.WriteLine($"Id: {loc.Id} Name: {loc.Name}");
            }

            Console.WriteLine("Please Select where you would like to go by id.");
            var goTo = Int32.Parse(Console.ReadLine());

            Database.Characters.First().Location = discoveredLocations.Where(loc => loc.Id == goTo).First().Name;

            Database.SaveChanges();

        }
        public static void ExploreWorld(Database Database)
        {
            Random Random = new Random();
            var locations = Database.Map.Where(map => map.IsDiscovered == 0).ToList();
            var locationOption = locations[Random.Next(0, locations.Count() - 1)];
            var Player = Database.Characters.First();
            Player.Location = locationOption.Name;
            locationOption.IsDiscovered = 1;
            Database.SaveChanges();
            Console.WriteLine($"You Have Discovered {locationOption.Name}.{locationOption.Description}");
            if (Random.Next(0, 100) > 50)
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
            var MagicSword = Player.Items.Where(item => item.Id == 2).First();
            MagicSword.Level = 1;
            Player.Items = new List<Item>();
            Player.Health = 100;
            Player.Energy = 100;


            foreach (var item in Database.Map.Where(map => map.Name != "Starting Town"))
            {
                item.IsDiscovered = 0;
            };
            Database.SaveChanges();


        }
        public static void LevelUp(Database Database, decimal experience, Character player, Item weapon)
        {
            var playerlvl = player.Level;

            playerlvl = playerlvl + (experience / 10);

            if ((int)playerlvl == (int)(player.Level + 1))
            {

                Console.WriteLine("Congrats! you leveled up!");
                player.Health = ((int)playerlvl * 100);
                player.Energy = ((int)playerlvl * 100);
                player.Level = playerlvl;
                weapon.Level = playerlvl;
                Database.SaveChanges();

            }
            else
            {
                player.Health = ((int)playerlvl * 100);
                player.Energy = ((int)playerlvl * 100);
                player.Level = playerlvl;
                weapon.Level = playerlvl;
                Database.SaveChanges();
            }


        }

        public static void Loot(Database database, Character Player)
        {
            Random Random = new Random();
            var loot = database.Items.Where(item => item.Type != "Weapon" && !Player.Items.Contains(item)).ToList();
            var lootDropped = loot[Random.Next(0, loot.Count() - 1)];
            Player.Items.Add(lootDropped);
            Console.WriteLine($"You found a {lootDropped.Name}. Description: {lootDropped.Description}");
            database.SaveChanges();
        }

        public static void BossFight(Database database)
        {


        }

        public static void CheckItems(Character Player)
        {
            var inventory = Player.Items.ToList();

            foreach (var item in inventory)
            {
                Console.WriteLine($"Name: {item.Name} Level: {item.Level} Type: {item.Type} Description: {item.Description}");
            }
        }






    }
}

