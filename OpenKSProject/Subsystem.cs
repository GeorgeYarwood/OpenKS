using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    internal class Subsystem
    {
        public virtual void Init()
        {

        }

        public virtual void FastUpdate()    //Every second
        {

        }

        public virtual void SlowUpdate()    //Every 5 seconds
        {

        }
    }
}
