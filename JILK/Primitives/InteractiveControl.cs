using JILK.Delegates;
namespace JILK.Primitives
{
    public abstract class InteractiveControl
    {
        public bool IsMouseOver { get; private set; } = false;

        // TODO: Implement it as event
        public CursorEventHandler CursorClick_EventHandler;

        public event CursorEventHandler CursorOver_EventHandler;
        public event CursorEventHandler CursorOut_EventHandler;
        public event CursorEventHandler CursorMove_EventHandler;

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

        #region Main cursor events
        public virtual void OnCursorOver(object sender, CursorEventArgs eventArgs)
        {
            IsMouseOver = true;
            CursorOver_EventHandler?.Invoke(sender, eventArgs);
        }

        public virtual void OnCursorOut(object sender, CursorEventArgs eventArgs)
        {
            IsMouseOver = false;
            CursorOut_EventHandler?.Invoke(sender, eventArgs);
        }

        public virtual void OnCursorClick(object sender, CursorEventArgs eventArgs)
        {
            CursorClick_EventHandler?.Invoke(sender, eventArgs);
        }

        public virtual void OnCursorMove(object sender, CursorEventArgs eventArgs)
        {
            CursorMove_EventHandler?.Invoke(sender, eventArgs);
        }
        #endregion

        #region Cursor short move steps
        public virtual void OnCursorMoveLeft(object sender, CursorEventArgs eventArgs)
        {
            CursorMoveLeft_EventHandler?.Invoke(sender, eventArgs);
        }
        public virtual void OnCursorMoveUp(object sender, CursorEventArgs eventArgs)
        {
            CursorMoveUp_EventHandler?.Invoke(sender, eventArgs);
        }
        public virtual void OnCursorMoveRight(object sender, CursorEventArgs eventArgs)
        {
            CursorMoveRight_EventHandler?.Invoke(sender, eventArgs);
        }
        public virtual void OnCursorMoveDown(object sender, CursorEventArgs eventArgs)
        {
            CursorMoveDown_EventHandler?.Invoke(sender, eventArgs);
        }
        #endregion

        #region Forced cursor staps
        public virtual void OnForcedCursorMoveLeft(object sender, CursorEventArgs eventArgs)
        {
            ForcedCursorMoveLeft_EventHandler?.Invoke(sender, eventArgs);

        }
        public virtual void OnForcedCursorMoveUp(object sender, CursorEventArgs eventArgs)
        {
            ForcedCursorMoveUp_EventHandler?.Invoke(sender, eventArgs);
        }
        public virtual void OnForcedCursorMoveRight(object sender, CursorEventArgs eventArgs)
        {
            ForcedCursorMoveRight_EventHandler?.Invoke(sender, eventArgs);
        }
        public virtual void OnForcedCursorMoveDown(object sender, CursorEventArgs eventArgs)
        {
            ForcedCursorMoveDown_EventHandler?.Invoke(sender, eventArgs);
        }
        #endregion

        #region Pressed cursor move steps
        public virtual void OnPressedCursorMoveLeft(object sender, CursorEventArgs eventArgs)
        {
            PressedCursorMoveLeft_EventHandler?.Invoke(sender, eventArgs);
        }
        public virtual void OnPressedCursorMoveUp(object sender, CursorEventArgs eventArgs)
        {
            PressedCursorMoveUp_EventHandler?.Invoke(sender, eventArgs);
        }
        public virtual void OnPressedCursorMoveRight(object sender, CursorEventArgs eventArgs)
        {
            PressedCursorMoveRight_EventHandler?.Invoke(sender, eventArgs);
        }
        public virtual void OnPressedCursorMoveDown(object sender, CursorEventArgs eventArgs)
        {
            PressedCursorMoveDown_EventHandler?.Invoke(sender, eventArgs);
        }
        #endregion
    }
}
