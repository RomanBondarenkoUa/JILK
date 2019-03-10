using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace JILK.Interfaces
{
    public interface IGUIcontainer
    {
        List<IGUIelement> Chilren { get; }
    }
}
