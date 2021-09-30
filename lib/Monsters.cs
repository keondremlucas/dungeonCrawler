using System;
using System.Collections.Generic; 

namespace lib
{
    public  class Monster
    {
       public int Id {get; set;}
       public string Name {get; set;}
       public string Description  {get; set;}
       public string Type  {get; set;}

       public List<Item> Items {get; set;}  = new List<Item>();
       public decimal Level {get; set;}
       public decimal Health  {get; set;}
       public decimal Energy {get; set;}
       public string Location {get; set;}


    }
}