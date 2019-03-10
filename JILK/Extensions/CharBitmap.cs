using System;

namespace JILK.Extensions
{
    public static class CharBitmapExtension
    {
        /// <summary>
        /// Declares extension method for copy one char matrix into one another.
        /// Ignores out of range values.
        /// </summary>
        /// <param name="bitmap">Main char matrix for placing another one </param>
        /// <param name="innerBitmap">Char matrix which will be placed over main char matrix</param>
        /// <param name="xOffset">Horizontal offset determines distance between innerBitmap and left border of main bitmap</param>
        /// <param name="yOffset">Vertical offset determines distance between innerBitmap and upper border of main bitmap</param>
        public static void AddInnerBitmap(this char[,] bitmap, char[,] innerBitmap, int xOffset, int yOffset)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");
            if (innerBitmap == null)
                throw new ArgumentNullException("innerBitmap");

            for (int i = 0; i < innerBitmap.GetLength(0); i++)
            {
                for (int j = 0; j < innerBitmap.GetLength(1); j++)
                {
                    if (i + yOffset >= bitmap.GetLength(0))
                        continue;
                    if (j + xOffset >= bitmap.GetLength(1))
                        continue;
                    if (i + yOffset < 0)
                        continue;
                    if (j + xOffset < 0)
                        continue;
                    bitmap[i + yOffset, j + xOffset] = innerBitmap[i, j];
                }
            }
        }
    }
}
