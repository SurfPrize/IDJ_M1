using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateDB
{
    class Program
    {
        
        static void Main(string[] args)
        {
            ThroneOfHeroes.Loadfromfile();
            
            Master teste = new Master(ThroneOfHeroes.Summon_by_Class(Servant_Class.AVENGER,5));
            Console.WriteLine(teste.ToString());
            Console.Write("Enter to Exit");
            Console.ReadLine();
        }
    }
}
