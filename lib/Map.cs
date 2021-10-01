using System;
using System.Collections.Generic; 

namespace lib
{
    public  class Location
    {
       public int Id {get; set;}
       public string Name {get; set;}
       public string Description {get; set;}
       public string Type {get; set;}
        public int IsDiscovered {get; set;} = 0;
    }
}