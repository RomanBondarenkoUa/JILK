using JILK.Applications;
using JILK.Delegates;
using JILK.enums;
using JILK.Primitives;
using System;
using System.Collections.Generic;

namespace JILK
{
    internal class KeyLogger
    {
        private static Dictionary<int, CursorEventHandler> actions = new Dictionary<int, CursorEventHandler>();
        private readonly Surface relatedSurface;

        /// <summary>       
        /// I`m using underline separation cuz Microsoft uses it too. In WinForm applications auto-generated
        /// events named using underline separation ( Button1_Click for instance )
        /// I know, MS code conventions hates underline separation but in this case 
        /// event names reads more smoothly than PascalCase analogue
        /// </summary>
        #region Cursor events

        // TODO: implement it as event
        public CursorEventHandler CursorClick_EventHandler;

        public event CursorEventHandler CursorMoveLeft_EventHandler;
        public event CursorEventHandler CursorMoveUp_EventHandler;
        public event CursorEventHandler CursorMoveRight_EventHandler;
        public event CursorEventHandler CursorMoveDown_EventHandler;

        public event CursorEventHandler ForcedCursorMoveLeft_EventHandler;
        public event CursorEventHandler ForcedCursorMoveUp_EventHandler;
        public event CursorEventHandler ForcedCursorMoveRight_EventHandler;
        public event CursorEventHandler ForcedCursorMoveDown_EventHandler;

        public event CursorEventHandler PressedCursorMoveLeft_EventHandler;
        public event CursorEventHandler PressedCursorMoveUp_EventHandler;
        public event CursorEventHandler PressedCursorMoveRight_EventHandler;
        public event CursorEventHandler PressedCursorMoveDown_EventHandler;

        #endregion

        public KeyLogger(Surface surface)
        {
            relatedSurface = surface;
            InitEvents();
            UpdateKeysHandlers();
        }


        #region Events initialization
        private void InitEvents()
        {
            CursorMoveLeft_EventHandler += relatedSurface.ScreenCursor.MoveLeft;
            CursorMoveUp_EventHandler += relatedSurface.ScreenCursor.MoveUp;
            CursorMoveRight_EventHandler += relatedSurface.ScreenCursor.MoveRight;
            CursorMoveDown_EventHandler += relatedSurface.ScreenCursor.MoveDown;

            PressedCursorMoveLeft_EventHandler += relatedSurface.ScreenCursor.PressedMoveLeft;
            PressedCursorMoveUp_EventHandler += relatedSurface.ScreenCursor.PressedMoveup;
            PressedCursorMoveRight_EventHandler += relatedSurface.ScreenCursor.PressedMoveRight;
            PressedCursorMoveDown_EventHandler += relatedSurface.ScreenCursor.PressedMoveDown;

            ForcedCursorMoveLeft_EventHandler += relatedSurface.ScreenCursor.ForcedMoveLeft;
            ForcedCursorMoveUp_EventHandler += relatedSurface.ScreenCursor.ForcedMoveUp;
            ForcedCursorMoveRight_EventHandler += relatedSurface.ScreenCursor.ForcedMoveRight;
            ForcedCursorMoveDown_EventHandler += relatedSurface.ScreenCursor.ForcedMoveDown;

            CursorClick_EventHandler += relatedSurface.ScreenCursor.Click;
        }
        #endregion

        private void UpdateKeysHandlers()
        {
            actions = new Dictionary<int, CursorEventHandler>();

            actions.Add(106, CursorMoveLeft_EventHandler);
            actions.Add(105, CursorMoveUp_EventHandler);
            actions.Add(108, CursorMoveRight_EventHandler);
            actions.Add(107, CursorMoveDown_EventHandler);

            actions.Add(74, ForcedCursorMoveLeft_EventHandler);
            actions.Add(73, ForcedCursorMoveUp_EventHandler);
            actions.Add(76, ForcedCursorMoveRight_EventHandler);
            actions.Add(75, ForcedCursorMoveDown_EventHandler);

            actions.Add(10, PressedCursorMoveLeft_EventHandler);
            actions.Add(9, PressedCursorMoveUp_EventHandler);
            actions.Add(12, PressedCursorMoveRight_EventHandler);
            actions.Add(11, PressedCursorMoveDown_EventHandler);

            actions?.Add(117, CursorClick_EventHandler);
        }

        public void AddFocusedControlHandlers(InteractiveControl control)
        {
            if (control == null)
                return;

            CursorMoveLeft_EventHandler += control.OnCursorMoveLeft;
            CursorMoveUp_EventHandler += control.OnCursorMoveUp;
            CursorMoveRight_EventHandler += control.OnCursorMoveRight;
            CursorMoveDown_EventHandler += control.OnCursorMoveDown;

            ForcedCursorMoveLeft_EventHandler += control.OnForcedCursorMoveLeft;
            ForcedCursorMoveUp_EventHandler += control.OnForcedCursorMoveUp;
            ForcedCursorMoveRight_EventHandler += control.OnForcedCursorMoveRight;
            ForcedCursorMoveDown_EventHandler += control.OnForcedCursorMoveDown;

            PressedCursorMoveLeft_EventHandler += control.OnPressedCursorMoveLeft;
            PressedCursorMoveUp_EventHandler += control.OnPressedCursorMoveUp;
            PressedCursorMoveRight_EventHandler += control.OnPressedCursorMoveRight;
            PressedCursorMoveDown_EventHandler += control.OnPressedCursorMoveDown;

            CursorClick_EventHandler += control.OnCursorClick;

            control?.OnCursorOver(relatedSurface, new CursorEventArgs(relatedSurface.ScreenCursor.CursorPosition, CursorEventType.CursorMove));

            UpdateKeysHandlers();
        }

        public void RemoveFocusedControlHandlers(InteractiveControl control)
        {
            CursorMoveLeft_EventHandler -= control.OnCursorMoveLeft;
            CursorMoveUp_EventHandler -= control.OnCursorMoveUp;
            CursorMoveRight_EventHandler -= control.OnCursorMoveRight;
            CursorMoveDown_EventHandler -= control.OnCursorMoveDown;

            ForcedCursorMoveLeft_EventHandler -= control.OnForcedCursorMoveLeft;
            ForcedCursorMoveUp_EventHandler -= control.OnForcedCursorMoveUp;
            ForcedCursorMoveRight_EventHandler -= control.OnForcedCursorMoveRight;
            ForcedCursorMoveDown_EventHandler -= control.OnForcedCursorMoveDown;

            PressedCursorMoveLeft_EventHandler -= control.OnPressedCursorMoveLeft;
            PressedCursorMoveUp_EventHandler -= control.OnPressedCursorMoveUp;
            PressedCursorMoveRight_EventHandler -= control.OnPressedCursorMoveRight;
            PressedCursorMoveDown_EventHandler -= control.OnPressedCursorMoveDown;

            CursorClick_EventHandler -= control.OnCursorClick;

            control?.OnCursorOut(relatedSurface, new CursorEventArgs(relatedSurface.ScreenCursor.CursorPosition, CursorEventType.CursorMove));
            UpdateKeysHandlers();
        }

        public void ReadUserInput()
        {
            int keyChar = (int)Console.ReadKey().KeyChar;
            if (actions.ContainsKey(keyChar))
            {
                Console.WriteLine();
                actions[keyChar]?.Invoke(
                    this,
                    new CursorEventArgs(
                        relatedSurface.ScreenCursor.CursorPosition,
                        CursorEventType.CursorMove));
            }
            else
            {
                relatedSurface.ShowTutorial();
            }
        }
    }
}
