using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    internal class MenuSearch : Subsystem
    {
        static MenuSearch instance;
        public static MenuSearch Instance
        {
            get { return instance; }
        }

        List<MenuItem> runtimemenu = new List<MenuItem>();
        public List<MenuItem> RuntimeMenu
        {
            get { return runtimemenu; }
        }

        public override void Init()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                return;
            }
        }

    }
}
