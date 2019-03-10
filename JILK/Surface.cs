using JILK.Applications;
using JILK.Controls;
using JILK.Delegates;
using JILK.Extensions;
using JILK.Interfaces;
using JILK.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JILK
{
    public class Surface : InteractiveControl, ISelfRedrawableControl
    {
        private char[,] ScreenCharmap;
        private readonly KeyLogger keyLogger;
        private Window focusedWindow;
        private InteractiveControl focusedInteractiveControl;
        private bool isStateChanged = true;
        private int width = 0;
        private int height = 0;
        private string lastFrame;
        private List<Window> Windows = new List<Window>();

        public Window FocusedWindow
        {
            get { return focusedWindow; }
            set
            {
                if (value == null && value != focusedWindow)
                {// CursorOutOfWindow
                }
                if (value != null && value != focusedWindow && focusedWindow != null)
                {       // old  - CursorOutOfWindow
                        // new  - CursorOverthe Window
                }

                focusedWindow = value;
            }
        }

        public InteractiveControl FocusedControl
        {
            get { return focusedInteractiveControl; }
            set
            {
                // if user moved cursor inside one single control 
                if (value == focusedInteractiveControl)
                    return;

                // if user moved cursor in gap between the controls
                if (value == null && focusedInteractiveControl == null)
                    return;

                // if user moved cursor on control from gap between controls
                if (value != null &&
                        focusedInteractiveControl == null)
                {
                    keyLogger.AddFocusedControlHandlers(value);
                    focusedInteractiveControl = value;
                }

                // if user moved cursor from control to gap between control
                else if (value == null &&
                        focusedInteractiveControl != null)
                {
                    keyLogger.RemoveFocusedControlHandlers(focusedInteractiveControl);
                    focusedInteractiveControl = value;
                }

                // if user moved cursor form control to some another control
                else
                {
                    keyLogger.RemoveFocusedControlHandlers(focusedInteractiveControl);
                    keyLogger.AddFocusedControlHandlers(value);
                    focusedInteractiveControl = value;
                }
            }
        }

        public Cursor ScreenCursor { get; private set; }

        public event SelfRedrawEventHandler SelfRedrawEventHandler;

        public Surface()
        {
            ScreenCursor = new Cursor(this);
            keyLogger = new KeyLogger(this);
            AddWindow(new Apps(24, 100, "Apps menu", new Point(1, 1, 0), this));
            ShowTutorial();
            while (true)
            {
                GenerateFrame();
                UpdateFocusedWindow();
                UpdateFocusedControl();
                PrintLastFrame();
                keyLogger.ReadUserInput();
            }
        }

        /// <summary>
        /// Redisplay all inner windows, sorts it by zIndex and builds new char matrix if required, and sets new stringified frame
        /// </summary>
        public void GenerateFrame()
        {
            if (Windows == null || Windows.Count == 0)
                return;
            if (Console.WindowWidth != width || Console.WindowHeight != height)
            {
                width = Console.WindowWidth;
                height = Console.WindowHeight;
                isStateChanged = true;
            }
            if (isStateChanged)
                ScreenCharmap = new char[height, width];

            Windows.Sort((win1, win2) => win1.Position.Z.CompareTo(win2.Position.Z));

            foreach (var window in Windows)
            {
                ScreenCharmap.AddInnerBitmap(window.Charmap, window.Position.X, window.Position.Y);
            }

            StringifyFrame();
        }

        public void PrintLastFrame()
        {
            Console.Clear();
            Console.WriteLine(lastFrame);
            ScreenCursor.UpdateCursorPosition();
        }

        public void ShowTutorial()
        {
            this.AddWindow(
               new Notepad(27, 60,
               "READ ME",
               "'u' - click\n\n" +
               "'j' - step left\n" +
               "'i' - step up\n" +
               "'l' - step right\n" +
               "'k' - step down\n\n" +
               "'j + Shift' - long step left\n" +
               "'i + Shift' - long step up\n" +
               "'l + Shift' - long step right\n" +
               "'k + Shift' - long step down\n\n" +
               "'j + Ctrl' on window top - move window left\n" +
               "'i + Ctrl' on window top - move window up\n" +
               "'l + Ctrl' on window top - move window right\n" +
               "'k + Ctrl' on window top - move window down\n\n" +
               "'j + Shift' on window top - decrease window width\n" +
               "'i + Shift' on window top - decrease window height\n" +
               "'l + Shift' on window top - increase window width\n" +
               "'k + Shift' on window top - increase window height\n",
               new Point(0, 0, 0)
            ));
        }

        public void AddWindow(Window window)
        {
            window.Position = new Point(Windows.Count,
                            Windows.Count,
                            Windows.Count);
            Windows.Add(window);
            //TODO: Cleanup closure
            window.WindowCloseEventHandler += (object sender, EventArgs e) =>
            {
                if (Windows.Count > 1)
                    Windows.Remove(window);
            };
            window.SelfRedrawEventHandler += OnSelfRedraw;
        }

        private void OnSelfRedraw(object sender, EventArgs e)
        {
            GenerateFrame();
            SelfRedrawEventHandler?.Invoke(sender as IGUIelement, e);
        }

        /// <summary>
        /// Sets new stringified frame ready to dispaying on screen
        /// </summary>
        private void StringifyFrame()
        {
            StringBuilder frameBuilder = new StringBuilder();
            for (int i = 0; i < ScreenCharmap.GetLength(0) - 2; i++)
            {
                for (int j = 0; j < ScreenCharmap.GetLength(1) - 2; j++)
                {
                    frameBuilder.Append(ScreenCharmap[i, j]);
                }
                frameBuilder.Append('\n');
            }
            lastFrame = frameBuilder.ToString();
        }

        /// <summary>
        /// Updates focused window by current cursor click position and places this window on top relatively of another windows
        /// </summary>
        public void UpdateFocusedWindow()
        {
            Windows = Windows.OrderBy((win) => win.Position.Z).ToList();

            int zIndexCounter = 0;
            foreach (var window in Windows)
            {
                // Setting windows zIndexes campatible with wheir focusing aging???
                window.Position = new Point(window.Position.X, window.Position.Y, zIndexCounter++);

                if (ScreenCursor.CursorPosition.X > window.Position.X &&
                    ScreenCursor.CursorPosition.X < (window.Position.X + window.Width) &&
                    ScreenCursor.CursorPosition.Y > window.Position.Y &&
                    ScreenCursor.CursorPosition.Y < (window.Position.Y + window.Height))
                {
                    // Placing focused window on top
                    window.Position = new Point(window.Position.X, window.Position.Y, Windows.Count);
                    focusedWindow = window;
                }
            }
            GenerateFrame();
        }

        /// <summary>
        /// Updates focused interactive control and places it on top of the each other
        /// </summary>
        public void UpdateFocusedControl()
        {
            if (FocusedWindow == null)
                return;

            FocusedWindow.Controls.Sort((c1, c2) => c1.Position.Z.CompareTo(c2.Position.Z));

            IGUIelement selectedGUIelement = null;
            foreach (var control in focusedWindow.Controls)
            {
                if (ScreenCursor.CursorPosition.X - FocusedWindow.Position.X > control.Position.X &&
                    ScreenCursor.CursorPosition.X - FocusedWindow.Position.X <= (control.Position.X + control.Width) &&
                    ScreenCursor.CursorPosition.Y - FocusedWindow.Position.Y > control.Position.Y &&
                    ScreenCursor.CursorPosition.Y - FocusedWindow.Position.Y <= (control.Position.Y + control.Height))
                {
                    selectedGUIelement = control;
                }
            }
            FocusedControl = (selectedGUIelement as InteractiveControl);
            GenerateFrame();
        }
    }
}
