using JILK.Primitives;
using System;

namespace JILK.Interfaces
{
    public interface IGUIelement
    {
        int Height { get; set; }
        int Width { get; set; }
        Point Position { get; set; }
        char[,] Charmap { get; }
    }
}