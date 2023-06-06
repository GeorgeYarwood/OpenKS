using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    //Instead of constantly polling the database, we traverse this and update from database when nessacary
    internal class MenuSearch
    {
        public static List<MenuItem> runtimeMenu = new List<MenuItem>();
    }
}
