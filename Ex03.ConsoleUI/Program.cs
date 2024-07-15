using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Enums;
// $G$ RUL-005 (-20) The zip file should contain a single folder.
// $G$ RUL-004 (-20) Wrong zip name format / folder name format
namespace Ex03.ConsoleUI
{
    // $G$ CSS-999 (-3) The Class must have an access modifier.
    // $G$ CSS-999 (-3) The method must have an access modifier.
    class Program
    {
        static void Main()
        {
            ConsoleUIManager cuim = new ConsoleUIManager();
            cuim.RunProgram();
        }
    }
}
