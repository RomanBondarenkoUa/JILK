using JILK.Delegates;
using JILK.enums;
using JILK.Interfaces;
using JILK.Primitives;
using System;
using System.Threading;

namespace JILK.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class Button : InteractiveControl, IGUIelement, ISelfRedrawableControl
    {
        /// <summary>
        /// Button has not own char matrix, it takes innerBorder char matrix for drawing
        /// </summary>
        private readonly Border<TextBox> innerBorder;
        private BorderStyle defaultBorderStyle;
        private int height;
        private int width;


        /// <summary>
        /// Gets or sets button height, validates incoming value and sets minimal possible value if required
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                if (height < 3)
                    height = 3;
                Redraw();
            }
        }

        /// <summary>
        /// Gets or sets button width, validates incoming value and sets minimal possible value if required
        /// </summary>
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                if (width < 3)
                    width = 3;
                Redraw();
            }
        }

        /// <summary>
        /// Gets or sets default border style ( when control inavtive)
        /// </summary>
        public BorderStyle DefaultBorderStyle
        {
            get { return defaultBorderStyle; }
            set
            {
                defaultBorderStyle = value;
                Redraw();
            }
        }

        /// <summary>
        /// Gets or sets active border style ( when control selected )
        /// </summary>
        public BorderStyle SelectedBorderStyle
        {
            get { return innerBorder.Style; }
            set
            {
                innerBorder.Style = value;
                Redraw();
            }
        }

        /// <summary>
        /// Position on the parrent window
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Represents char matrix for on-screen drawing
        /// </summary>
        public char[,] Charmap { get; private set; }

        /// <summary>
        /// Invokes when after button redraws without outside triggers
        /// </summary>
        public event SelfRedrawEventHandler SelfRedrawEventHandler;

        /// <summary>
        /// Inits main fields, creates inner border and textbox
        /// </summary>
        /// <param name="position">Position on the parrent window</param>
        /// <param name="defaultStyle">Represent default border style</param>
        public Button(string innerText, int heigth, int width, Point position, BorderStyle defaultStyle = BorderStyle.Dashline)
        {
            this.height = heigth;
            this.width = width;
            defaultBorderStyle = defaultStyle;

            TextBox innerTextBox = new TextBox(Height - 2, Width - 2, innerText, new Point(1, 1, 0));
            innerBorder = new Border<TextBox>(innerTextBox, DefaultBorderStyle, new Point(0, 0, 0));

            Position = position;
            AddHandlers();
            ChangeText(innerText);
        }

        /// <summary>
        /// Resizes and updates charmap if required
        /// </summary>
        public void Redraw()
        {
            innerBorder.Height = Height;
            innerBorder.Width = Width;
            Charmap = innerBorder.Charmap;
            height = Charmap.GetLength(0);
            width = Charmap.GetLength(1);
        }

        /// <summary>
        /// Updates button text
        /// </summary>
        public void ChangeText(string newText)
        {
            innerBorder.InnerElement.Text = newText;
            Redraw();
        }

        /// <summary>
        /// Inits main events
        /// </summary>
        private void AddHandlers()
        {
            CursorOver_EventHandler += OnCursorOver;
            CursorOut_EventHandler += OnCursorOut;
            CursorClick_EventHandler += OnCursorClick;
        }
        
        public override void OnCursorOver(object sender, CursorEventArgs eventArgs)
        {
            SelectedBorderStyle = BorderStyle.Simple;
            Redraw();
            OnSelfRedraw(this, null);
        }

        public override void OnCursorOut(object sender, CursorEventArgs eventArgs)
        {
            SelectedBorderStyle = DefaultBorderStyle;
            Redraw();
            OnSelfRedraw(this, null);
        }

        public override void OnCursorClick(object sender, CursorEventArgs eventArgs)
        {
            SelectedBorderStyle = BorderStyle.Double;
            OnSelfRedraw(this, EventArgs.Empty);
            SelectedBorderStyle = DefaultBorderStyle;
        }

        private void OnSelfRedraw(IGUIelement sender, EventArgs e)
        {
            Redraw();
            SelfRedrawEventHandler?.Invoke(sender, e);
        }
    }
}
