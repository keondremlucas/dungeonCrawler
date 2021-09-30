using System;
using lib;
namespace app
{ 
    class Program
    {
        static void Main(string[] args)
        {   
            var db = new Database();

            bool run = true;
            while(run)
            {
                Scenario.Explore(db);
                
            }

        }
    }
}
