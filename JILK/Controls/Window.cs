using JILK.Delegates;
using JILK.enums;
using JILK.Extensions;
using JILK.Interfaces;
using JILK.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JILK.Controls
{
    public abstract class Window : IGUIelement, ISelfRedrawableControl
    {

        private int height;
        private int width;
        
        /// <summary>
        /// Gets or sets window height and redraws char matrix
        /// </summary>
        public virtual int Height
        {
            get { return height; }
            set
            {
                height = value;
                SizeChangedEventHandler?.Invoke(this, EventArgs.Empty);
                Redraw(true);
            }
        }

        /// <summary>
        /// Gets or sets window width and redraws char matrix
        /// </summary>
        public virtual int Width
        {
            get { return width; }
            set
            {
                width = value;
                SizeChangedEventHandler?.Invoke(this, EventArgs.Empty);
                Redraw(true);
            }
        }


        /// <summary>
        /// Defines window position relative to the upper left corner of the surface 
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Defines window char matrix for on-screen drawing
        /// </summary>
        public char[,] Charmap { get; private set; }

        /// <summary>
        /// Defines on-top window header charmap with borders, window control 
        /// buttons (maximize, minimize, close) and application title label
        /// </summary>
        public char[,] Header
        { get { return GetHeader().Charmap; } }

        /// <summary>
        /// Defines public property for changing window on-top label with automatic char matrix redraw
        /// </summary>
        public string Title
        {
            get { return GetHeader().Title; }
            set
            {
                GetHeader().Title = value;
                TitleChangedEventHandler?.Invoke(this, EventArgs.Empty);
                Redraw(false);
            }
        }

        /// <summary>
        /// Defines lower window border
        /// </summary>
        public char[] Footer
        {
            get
            {
                char[] footer = new char[Width];

                footer[0] = BorderDesign.GetBorderChar(BorderStyle.Rounded, BorderPosition.LeftLower);
                footer[Width - 1] = BorderDesign.GetBorderChar(BorderStyle.Rounded, BorderPosition.RightLower);
                char lowerBorder = BorderDesign.GetBorderChar(BorderStyle.Rounded, BorderPosition.Horizontal);

                for (int i = 1; i < footer.Length - 1; i++)
                    footer[i] = lowerBorder;

                return footer;
            }
        }

        /// <summary>
        /// Represent window children controls
        /// </summary>
        public List<IGUIelement> Controls { get; protected set; }

        public BorderStyle CurrentBorderStyle { get; set; }


        public event SelfRedrawEventHandler SelfRedrawEventHandler;
        public event EventHandler SizeChangedEventHandler;
        public event EventHandler TitleChangedEventHandler;
        public event EventHandler WindowCloseEventHandler;

        /// <summary>
        /// Defines full-parametrized constructor for Window base class
        /// </summary>
        /// <param name="height"> Sets window heigth ( without header )</param>
        /// <param name="width"> Sets window width (without borders )</param>
        /// <param name="text"> Sets text for filling </param>
        /// <param name="position"> Sets window position on sufrace ( starts from [0,0] index )</param>
        protected Window(int height, int width, string title, Point position)
        {
            Position = position;
            this.height = height + 4;
            this.width = width + 2;
            Controls = new List<IGUIelement>();
            ResizeBitmap();
            AddChild(new WindowStatusbar(this, title));
        }

        /// <summary>
        /// Redisplay all children controls and builds new char matrix if required
        /// </summary>
        public virtual void Redraw(bool sizeChanged)
        {
            if (sizeChanged)
            {
                ResizeBitmap();
            }

            // Sorts controls by zIndex
            Controls.Sort((control1, control2) => control1.Position.Z.CompareTo(control2.Position.Z));

            foreach (var control in Controls)
            {
                Charmap.AddInnerBitmap(control.Charmap, control.Position.X, control.Position.Y);
            }

            DrawBorders();
        }

        /// <summary>
        /// Adds child control and redraws char matrix including new control bitmap
        /// </summary>
        /// <param name="control">Child GUI control</param>
        public virtual void AddChild(IGUIelement control)
        {
            Controls.Add(control);
            var selfRedrawablecontrol = (control as ISelfRedrawableControl);
            if (selfRedrawablecontrol != null)
                (control as ISelfRedrawableControl).SelfRedrawEventHandler += this.OnSelfRedraw;
            Redraw(false);
        }

        public void OnClose()
        {
            WindowCloseEventHandler?.Invoke(this, EventArgs.Empty);
        }

        private void OnSelfRedraw(object sender, EventArgs e)
        {
            Redraw(false);
            SelfRedrawEventHandler?.Invoke(this, null);
        }

        private void ResizeBitmap()
        {
            Charmap = new char[Height, Width];
        }

        /// <summary>
        /// Draw border sides with corresponding border style symbols
        /// </summary>
        private void DrawBorders()
        {
            Charmap[Height - 1, 0] = BorderDesign.GetBorderChar(CurrentBorderStyle, BorderPosition.LeftLower);
            Charmap[Height - 1, Width - 1] = BorderDesign.GetBorderChar(CurrentBorderStyle, BorderPosition.RightLower);


            for (int i = 1; i < Width - 1; i++)
            {
                Charmap[Height - 1, i] = BorderDesign.GetBorderChar(CurrentBorderStyle, BorderPosition.Horizontal);
            }

            for (int i = 3; i < Height - 1; i++)
            {
                Charmap[i, 0] = BorderDesign.GetBorderChar(CurrentBorderStyle, BorderPosition.Vertical);
                Charmap[i, Width - 1] = BorderDesign.GetBorderChar(CurrentBorderStyle, BorderPosition.Vertical);
            }
        }
        private WindowStatusbar GetHeader()
        {
            return Controls.FirstOrDefault(i => (i is WindowStatusbar)) as WindowStatusbar;
        }
    }
}
