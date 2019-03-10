using JILK.Delegates;
using System;
using System.Collections.Generic;
using System.Text;

namespace JILK.Interfaces
{
    interface ISelfRedrawableControl
    {
        event SelfRedrawEventHandler SelfRedrawEventHandler;
    }
}
