using JILK.enums;
using JILK.Extensions;
using JILK.Interfaces;
using JILK.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace JILK.Controls
{
    public class Border<T> : IGUIelement where T : IGUIelement
    {
        private int height;
        private int width;
        private BorderStyle style;

        /// <summary>
        /// True if charmap redrawing required ( size changed )
        /// </summary>
        private bool isStateChanged = true;
        private T innerElement;


        /// <summary>
        /// Gets or sets border height, validates incoming value and sets minimal possible value if required
        /// </summary>
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                isStateChanged = true;
                if (height < 3)
                    height = 3;
                Redraw();
            }
        }
        /// <summary>
        /// Gets or sets border width, validates incoming value and sets minimal possible value if required
        /// </summary>
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                isStateChanged = true;
                if (width < 3)
                    width = 3;
                Redraw();
            }
        }

        public char[,] Charmap { get; private set; }

        /// <summary>
        /// Represent element for framing
        /// </summary>
        public T InnerElement
        {
            get { return innerElement; }
            set
            {
                innerElement = value;
                Redraw();
            }
        }
        public BorderStyle Style
        {
            get { return this.style; }
            set
            {
                style = value;
                Redraw();
            }
        }
        public Point Position { get; set; }

        
        public Border(T innerControl, BorderStyle style, Point position)
        {
            Position = position;
            this.style = style;
            innerElement = innerControl;

            this.height = innerControl.Height + 2;
            this.width = innerControl.Width + 2;

            Redraw();
        }

        public void Redraw()
        {
            if (isStateChanged)
            {
                innerElement.Width = Width - 2;
                innerElement.Height = Height - 2;
                Charmap = new char[Height , Width];
                isStateChanged = false;
            }
            DrawBorders();
            DrawCornerBorders();
            DrawInnerBitmap();
        }

        private void DrawInnerBitmap()
        {
            Charmap.AddInnerBitmap(innerElement.Charmap, 1, 1);
        }

        private void DrawBorders()
        {
            for (int i = 1; i < Charmap.GetLength(0) - 1; i++)
            {
                Charmap[i, 0] = BorderDesign.GetBorderChar(Style, BorderPosition.Vertical);
                Charmap[i, Charmap.GetLength(1) - 1] = BorderDesign.GetBorderChar(Style, BorderPosition.Vertical);
            }

            for (int i = 1; i < Charmap.GetLength(1) - 1; i++)
            {
                Charmap[ 0, i ] = BorderDesign.GetBorderChar(Style, BorderPosition.Horizontal);
                Charmap[ Charmap.GetLength(0) - 1, i] = BorderDesign.GetBorderChar(Style, BorderPosition.Horizontal);
            }
        }

        private void DrawCornerBorders()
        {
            Charmap[0, 0] = BorderDesign.GetBorderChar(Style, BorderPosition.LeftUpper);
            Charmap[0, Charmap.GetLength(1) - 1] = BorderDesign.GetBorderChar(Style, BorderPosition.RightUpper);
            Charmap[Charmap.GetLength(0) - 1, 0] = BorderDesign.GetBorderChar(Style, BorderPosition.LeftLower);
            Charmap[Charmap.GetLength(0) - 1, Charmap.GetLength(1) - 1] = BorderDesign.GetBorderChar(Style, BorderPosition.RightLower);
        }
    }
}
