using System;
using System.Collections.Generic;
using System.Text;

namespace HLAServer
{
    class GameLogic
    {
        public static void Update ()
        {
            ThreadManager.UpdateMain();
        }
    }
}
