using JILK.Delegates;
using JILK.enums;
using JILK.Interfaces;
using JILK.Primitives;
using System;

namespace JILK.Controls
{
    /// <summary>
    /// Defines on-top window header with borders, window control buttons (maximize, minimize, close) and application title label
    /// </summary>
    public class WindowStatusbar : InteractiveControl, IGUIelement
    {
        private char[,] charmap;
        private readonly TextBox title;

        public int Width
        {
            get { return RelatedWindow.Width; }
            set
            {
                // Eah, 'set' is not changing object state
                // This object completely depends from its related window
                // There is no way to change internal state from foreign objects
                // I`m not removing setter for fully IGUIelement interface implementation

                throw new InvalidOperationException(
                    $"There is no way to change {this.GetType()} internal state from foreign objects");
            }
        }
        public int Height
        {
            get { return 3; }
            set
            {
                // Eah, 'set' is not changing object state
                // This object completely depends from its related window
                // There is no way to change internal state from foreign objects
                // I`m not removing setter for fully IGUIelement interface implementation

                throw new InvalidOperationException(
                    $"There is no way to change {this.GetType()} internal state from foreign objects");
            }
        }
        public Point Position
        {
            get { return new Point(0, 0, 0); }
            set
            {
                // Eah, 'set' is not changing object state
                // This object completely depends from its related window
                // There is no way to change internal state from foreign objects
                // I`m not removing setter for fully IGUIelement interface implementation

                throw new InvalidOperationException(
                    $"There is no way to change {this.GetType()} internal state from foreign objects");
            }
        }
        public char[,] Charmap
        {
            get
            {
                return charmap;
            }
        }
        public string Title
        {
            get { return title.Text; }
            set
            {
                title.Text = value;
                Redraw(false);
            }
        }

        public Window RelatedWindow { get; private set; }

        public WindowStatusbar(Window relatedWindow, string titleText)
        {
            RelatedWindow = relatedWindow;
            title = new TextBox(1, Width - 15, titleText, new Point(1, 12, 0));
            Redraw(true);
            relatedWindow.SizeChangedEventHandler += Redraw;
        }

        public override void OnPressedCursorMoveLeft(object sender, CursorEventArgs eventArgs)
        {
            RelatedWindow.Position = new Point(
                RelatedWindow.Position.X - 1,
                RelatedWindow.Position.Y,
                RelatedWindow.Position.Z);
            base.OnPressedCursorMoveLeft(sender, eventArgs);
        }

        public override void OnPressedCursorMoveUp(object sender, CursorEventArgs eventArgs)
        {
            RelatedWindow.Position = new Point(
                RelatedWindow.Position.X,
                RelatedWindow.Position.Y - 1,
                RelatedWindow.Position.Z);
            base.OnPressedCursorMoveUp(sender, eventArgs);
        }

        public override void OnPressedCursorMoveRight(object sender, CursorEventArgs eventArgs)
        {
            RelatedWindow.Position = new Point(
                RelatedWindow.Position.X + 1,
                RelatedWindow.Position.Y,
                RelatedWindow.Position.Z);
            base.OnPressedCursorMoveRight(sender, eventArgs);
        }

        public override void OnPressedCursorMoveDown(object sender, CursorEventArgs eventArgs)
        {
            RelatedWindow.Position = new Point(
                RelatedWindow.Position.X,
                RelatedWindow.Position.Y + 1,
                RelatedWindow.Position.Z);
            base.OnPressedCursorMoveDown(sender, eventArgs);
        }

        public override void OnForcedCursorMoveLeft(object sender, CursorEventArgs eventArgs)
        {
            RelatedWindow.Width--;
            base.OnPressedCursorMoveDown(sender, eventArgs);
        }

        public override void OnForcedCursorMoveUp(object sender, CursorEventArgs eventArgs)
        {
            RelatedWindow.Height--;
            base.OnPressedCursorMoveDown(sender, eventArgs);
        }

        public override void OnForcedCursorMoveRight(object sender, CursorEventArgs eventArgs)
        {
            RelatedWindow.Width++;
            base.OnPressedCursorMoveDown(sender, eventArgs);
        }

        public override void OnForcedCursorMoveDown(object sender, CursorEventArgs eventArgs)
        {
            RelatedWindow.Height++;
            base.OnPressedCursorMoveDown(sender, eventArgs);
        }


        public override void OnCursorClick(object sender, CursorEventArgs eventArgs)
        {
            var relativePosition = new Point(
                eventArgs.CursorOnScreenPosition.X - RelatedWindow.Position.X,
                eventArgs.CursorOnScreenPosition.Y - RelatedWindow.Position.Y,
                0);
            if (relativePosition.Y == 1)
            {
                if (relativePosition.X == 2)
                {
                    RelatedWindow.OnClose();
                    // After OnClose invocation related window shall be closed, so user will see the following text 
                    // only in case when window is not closed
                    RelatedWindow.Title += " At least one window should be on the surface";
                }
                if (relativePosition.X == 5)
                {
                    // TODO: Minimizing
                    RelatedWindow.Title += " Maximizing is unavailable at this moment.";
                }
                if (relativePosition.X == 8)
                {
                    // TODO: Maximizing
                    RelatedWindow.Title += " Minimizing is unavailable at this moment.";
                }
            }
        }

        private void Redraw(object sender, EventArgs e)
        {
            Redraw(true);
        }

        private void Redraw(bool isSizeChanged)
        {
            ResizeHeader(isSizeChanged);
            GenerateTopBorder();
            GenerateStatusRow();
            GenerateLowerBorder();
        }

        /// <summary>
        /// Resizes charmap if required
        /// </summary>
        /// <param name="sizeChanged">if 'true' resizes charmap</param>
        private void ResizeHeader(bool sizeChanged)
        {
            if (sizeChanged)
                charmap = new char[Height, Width];
        }

        /// <summary>
        /// Setting higher window border
        /// </summary>
        private void GenerateTopBorder()
        {
            // Setting left upper window corner char
            charmap[0, 0] = BorderDesign.GetBorderChar(BorderStyle.Rounded, BorderPosition.LeftUpper);

            // Setting right upper window corner char
            charmap[0, Width - 1] = BorderDesign.GetBorderChar(BorderStyle.Rounded, BorderPosition.RightUpper);

            // Filling higher border between left and right upper corners
            for (int i = 1; i < Charmap.GetLength(1) - 1; i++)
                charmap[0, i] = BorderDesign.GetBorderChar(BorderStyle.Rounded, BorderPosition.Horizontal);
        }

        /// <summary>
        /// Setting status border with window state control buttons(fullscreen, minimize, close) and application name
        /// </summary>
        private void GenerateStatusRow()
        {
            // Setting left and right borders
            charmap[1, 0] = BorderDesign.GetBorderChar(BorderStyle.Rounded, BorderPosition.Vertical);
            charmap[1, Width - 1] = BorderDesign.GetBorderChar(BorderStyle.Rounded, BorderPosition.Vertical);

            // Setting window control buttons view (it is not regular buttons because buttons at least filling 3x3 grid)
            // but in this case we have only 1x1 box for drawindg
            charmap[1, 2] = '×';
            charmap[1, 5] = '□';
            charmap[1, 8] = '_';

            // Filling window status bar (Application name, or some other information for user)
            for (int i = 12; i < Width - 3; i++)
                if ((i - 12) < Title.Length)
                    charmap[1, i] = title.Text[i - 12];
        }

        private void GenerateLowerBorder()
        {
            // It is special symbols for window header ( '╞', '╡' ). For better UX i did`t placed it in 'BorderDesign' class
            // to prevent other GUI objects using it
            charmap[2, 0] = '╞';
            charmap[2, Width - 1] = '╡';

            // Generating lower statusbar row
            for (int i = 1; i < Width - 1; i++)
                charmap[2, i] = BorderDesign.GetBorderChar(BorderStyle.Double, BorderPosition.Horizontal);
        }
    }
}
