using JILK.Controls;
using JILK.Delegates;
using JILK.enums;
using JILK.Primitives;

namespace JILK.Applications
{
    public class Notepad : Window
    {
        private Border<TextBox> textContent;
        private Button scrollDownButton;
        private Button scrollUpButton;

        // Unformatted text without replacing control symbols
        private string _text;

        public const int MinWidth = 10;
        public const int MinHeigth = 11;


        /// <summary>
        /// Defines simplest constructor for JILK notepad
        /// </summary>
        /// <param name="text"></param>
        public Notepad(string text) : base(18, 70, "JILKpad V 0.3", new Point(5, 5, 0))
        {
            _text = text;
        }

        /// <summary>
        /// Defines full-parametrized constructor for JILK notepad
        /// </summary>
        /// <param name="height"> Sets window heigth ( please note, scroll buttons fills 6 bottom rows ) </param>
        /// <param name="width"> Sets window heigth ( please note, textbox border fills 2 additional columns )</param>
        /// <param name="title"> Sets window header </param>
        /// <param name="text"> Sets text for filling</param>
        /// <param name="position"> Sets window position on sufrace ( starts from [0,0] index )</param>
        public Notepad(int height, int width, string title, string text, Point position) : base(height, width, title, position)
        {
            this._text = text;
            this.InitControls();
            this.Redraw(true);
        }

        /// <summary>
        /// Checks that the window size is acceptable.
        /// Otherwise sets minimal values defined for this type of window
        /// </summary>
        private void ValidateSize()
        {
            if (Width < MinWidth)
                Width = MinWidth;
            if (Height < MinHeigth)
                Height = MinHeigth;
        }

        /// <summary>
        /// Initialize default controls with default params, adds and draws it
        /// </summary>
        private void InitControls()
        {
            textContent = new Border<TextBox>(
                    new TextBox(
                        Height - 14,
                        Width - 6,
                        _text,
                        new Point(0, 0, 0)),
                    BorderStyle.Double, new Point(2, 4, 0));

            scrollDownButton = new Button("scroll down", 3, 24, new Point(0, 0, 1), BorderStyle.Simple);
            scrollUpButton = new Button("scroll up", 3, 24, new Point(0, 0, 1), BorderStyle.Simple);

            scrollDownButton.CursorClick_EventHandler += (object sender, CursorEventArgs e) =>
            { textContent.InnerElement.ScrollDown(); };

            scrollUpButton.CursorClick_EventHandler += (object sender, CursorEventArgs e) =>
            { textContent.InnerElement.ScrollUp(); };

            AddChild(textContent);
            AddChild(scrollDownButton);
            AddChild(scrollUpButton);
            base.Redraw(true);
        }

        /// <summary>
        /// Defines method for updating controls position after state changing and redraws window char matrix
        /// </summary>
        public override void Redraw(bool isSizeChanged)
        {
            ValidateSize();

            if (textContent == null || scrollDownButton == null || scrollUpButton == null)
                return;
            
            textContent.Height = Height - 8;
            textContent.Width = Width - 4;

            scrollDownButton.Position = new Point(2, Height - 4, 1);
            scrollDownButton.Width = Width / 3;

            scrollUpButton.Position = new Point(Width - (scrollUpButton.Width + 2), Height - 4, 1);
            scrollUpButton.Width = Width / 3;

            base.Redraw(true);

        }
    }
}
