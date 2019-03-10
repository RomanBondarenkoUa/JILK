using JILK.enums;
using JILK.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace JILK.Delegates
{
    public class CursorEventArgs : EventArgs
    {
        public Point CursorOnScreenPosition { get; set; }
        public CursorEventType EventType { get; set; }
        public CursorEventArgs(Point cursorPosition, CursorEventType eventType)
        {
            CursorOnScreenPosition = cursorPosition;
            EventType = eventType;
        }
    }
}
