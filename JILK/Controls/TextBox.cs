using JILK.Delegates;
using JILK.Interfaces;
using JILK.Primitives;
using System;
using System.Text;

namespace JILK.Controls
{
    public class TextBox : IGUIelement, ISelfRedrawableControl
    {
        private int height;
        private int width;
        private string text;

        /// <summary>
        /// Text for drawing, with replaced control symbols
        /// </summary>
        private string formattedText;

        /// <summary>
        /// First row number drawed on the screen
        /// </summary>
        private int currentRow;

        /// <summary>
        /// True if charmap redrawing required
        /// </summary>
        private bool isStateChanged = true;


        /// <summary>
        /// Gets or sets border height, validates incoming value and sets minimal possible value if required
        /// </summary>
        public int Height
        {
            get { return height; }
            set
            {
                isStateChanged = true;
                height = value;
                if (height < 1)
                    height = 1;
                Redraw();
            }
        }

        /// <summary>
        /// Gets or sets border height, validates incoming value and sets minimal possible value if required
        /// </summary>
        public int Width
        {
            get { return width; }
            set
            {
                isStateChanged = true;
                width = value;
                if (width < 1)
                    width = 1;
                Redraw();
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                formattedText = ReformateString(text);
                if (text == null)
                    text = string.Empty;
                Redraw();
            }
        }
        public Point Position { get; set; }
        public char[,] Charmap { get; private set; }
        private int RowsNumber
        {
            get { return formattedText.Length / Width; }
        }


        // TODO: make TextBox selfRedrawable
        public event SelfRedrawEventHandler SelfRedrawEventHandler;

        public TextBox(int height, int width, string initialText, Point position)
        {
            Position = position;
            this.height = height;
            this.width = width;
            Text = initialText;
        }

        public void ScrollDown()
        {
            if (currentRow < RowsNumber)
            {
                currentRow += 1;
                isStateChanged = true;
                Redraw();
            }
        }
        public void ScrollUp()
        {
            if (currentRow > 0)
            {
                currentRow -= 1;
                isStateChanged = true;
                Redraw();
            }
        }

        private void Redraw()
        {
            if (isStateChanged)
            {
                Charmap = new char[Height, Width];
                formattedText = ReformateString(Text);
                isStateChanged = false;
            }
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Charmap[i, j] = GetChar(currentRow + i, j);
                }
            }
        }

        /// <summary>
        /// Returns char in the text by row and offset from row start
        /// </summary>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private char GetChar(int row, int index)
        {
            // If requested symbol is out of text length range - returns whitespace
            if ((row * Width) + index >= formattedText.Length)
                return ' ';
            return formattedText[(row * Width) + index];
        }

        /// <summary> 
        /// Replaces control symbols for correct console view
        /// </summary>
        private string ReformateString(string innerText)
        {
            var textBuilder = new StringBuilder();
            int charsInLine = 0;

            for (int i = 0; i < innerText.Length; i++)
            {
                if (charsInLine >= Width)
                    charsInLine = 0;

                if (!char.IsControl(innerText[i]))
                    textBuilder.Append(innerText[i]);
                else if (innerText[i] == '\n')
                {
                    textBuilder.Append(new string(' ', Width - charsInLine));
                    charsInLine = 0;
                    continue;
                }
                else if (innerText[i] == '\t')
                {
                    textBuilder.Append(new string(' ', 4));
                    charsInLine = 4;
                    continue;
                }
                charsInLine++;
            }

            return textBuilder.ToString();
        }
    }
}