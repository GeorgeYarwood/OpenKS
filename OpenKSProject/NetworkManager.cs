using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    internal class NetworkManager : Subsystem
    {
        KS_MODE ksMode;
        public KS_MODE KsMode
        {
            get { return ksMode; }
            set { ksMode = value; }
        }

        HOST_DISCOVERY_MODE hostDiscoveryMode;
        public HOST_DISCOVERY_MODE HostDiscoveryMode
        {
            get { return hostDiscoveryMode; }
            set { hostDiscoveryMode = value; }
        }

        static NetworkManager instance;
        public static NetworkManager Instance
        {
            get { return instance; }
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

        public override void SlowUpdate()
        {
            //if(!isMasterKs)
            //{
            //    SyncWithMaster();
            //}
        }

        void SyncWithMaster()
        {

        }

        public override void FastUpdate()
        {

        }
    }
}
