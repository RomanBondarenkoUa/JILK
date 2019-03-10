using JILK.Delegates;
using System;

namespace JILK.Primitives
{
    public class Cursor
    {
        private readonly Surface relatedSurface;
        private Point cursorPosition;

        public Point CursorPosition
        {
            get { return cursorPosition; }
            set
            {
                if(value.X >= 0 &&
                    value.X < Console.WindowWidth &&
                    value.Y >= 0 &&
                    value.Y < Console.WindowHeight)
                {
                    cursorPosition = value;
                    UpdateCursorPosition();
                }
            }
        }


        public Cursor(Surface surface)
        {
            relatedSurface = surface;
        }

        public void UpdateCursorPosition()
        {
            Console.SetCursorPosition(CursorPosition.X, CursorPosition.Y);
        }

        #region Simple cursore move events
        public void MoveLeft(object sender, CursorEventArgs e)
        {

            CursorPosition = new Point(
                CursorPosition.X - 1,
                CursorPosition.Y,
                CursorPosition.Z);

        }

        public void MoveUp(object sender, CursorEventArgs e)
        {
            CursorPosition = new Point(
                CursorPosition.X,
                CursorPosition.Y - 1,
                0);
        }

        public void MoveRight(object sender, CursorEventArgs e)
        {
            CursorPosition = new Point(
                CursorPosition.X + 1,
                CursorPosition.Y,
                0);
        }

        public void MoveDown(object sender, CursorEventArgs e)
        {
            CursorPosition = new Point(
                CursorPosition.X,
                CursorPosition.Y + 1,
                0);

        }
        #endregion

        #region
        public void ForcedMoveLeft(object sender, CursorEventArgs e)
        { 
            CursorPosition = new Point(
                        CursorPosition.X - 18,
                        CursorPosition.Y,
                        0);
        }


        public void ForcedMoveUp(object sender, CursorEventArgs e)
        {
            CursorPosition = new Point(
                            CursorPosition.X,
                            CursorPosition.Y - 9,
                            0);

        }

        public void ForcedMoveRight(object sender, CursorEventArgs e)
        {
            CursorPosition = new Point(
                            CursorPosition.X + 18,
                            CursorPosition.Y,
                            0);
        }

        public void ForcedMoveDown(object sender, CursorEventArgs e)
        {
            CursorPosition = new Point(
                        CursorPosition.X,
                        CursorPosition.Y + 9,
                        0);
        }

        public void PressedMoveLeft(object sender, CursorEventArgs e)
        {
            MoveLeft(sender, e);
        }
        public void PressedMoveup(object sender, CursorEventArgs e)
        {
            MoveUp(sender, e);
        }
        public void PressedMoveRight(object sender, CursorEventArgs e)
        {
            MoveRight(sender, e);
        }
        public void PressedMoveDown(object sender, CursorEventArgs e)
        {
            MoveDown(sender, e);
        }

        public void Click(object sender, CursorEventArgs e)
        {
            relatedSurface.UpdateFocusedWindow();
            relatedSurface.FocusedControl?.CursorClick_EventHandler?.Invoke(sender, e);
        }
        #endregion
    }
}