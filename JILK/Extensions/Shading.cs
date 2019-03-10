using System;
using System.Collections.Generic;
using System.Text;

namespace JILK.enums
{
    /// <summary>
    /// For upcoming 3d objects rasterization
    /// </summary>
    public static class ShadingSymbol
    {
        private static Dictionary<int, char> ShadingDictionary = new Dictionary<int, char>()
         {
            { 0, ' ' },
            { 1, '`' },
            { 2, '.' },
            { 3, ',' },
            { 4, '-' },
            { 4, '^' },
            { 7, '~' },
            { 8, '+' },
            { 5, ':' },
            { 6, ';' },
            { 9, '=' },
            { 10, 'i' },
            { 11, '%' },
            { 12, 'w' },
            { 13, '#' },
            { 14, '@' },
            { 15, '█' }
        };

        public static char GetShadingSymol(int index)
        {
            if (index < 0)
                return ' ';
            else if (index > 15)
                return ShadingDictionary[15];
            else
            {
                return ShadingDictionary[index];
            }
        }
    }
}
