using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YouTube.ExControls
{
    public enum ExControlState
    {
        None,
        Normal,
        Disabled,
        Hover,
        Pressed,
        Focused
    }

    public abstract class ExControl : Control
    {
        public ExControl()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint, true);

            State = ExControlState.Normal;
        }

        protected bool _EnableTransparency = false;

        [DefaultValue(false)]
        public virtual bool EnableTransparency
        {
            get { return _EnableTransparency; }
            set
            {
                if (_EnableTransparency != value)
                {
                    _EnableTransparency = value;
                    SetStyle(ControlStyles.SupportsTransparentBackColor, value);
                }
            }
        }

        [DefaultValue(false)]
        public bool EnableKeyboardNavigation { get; set; }

        public ExControlState State { get; private set; }

        private bool _Hovering = false;
        public bool Hovering { get { return _Hovering; } }

        private bool _Pressed = false;
        public bool Pressed { get { return _Pressed; } }

        private void UpdateControlState()
        {
            if (!Enabled)
            {
                OnStateChange(ExControlState.Disabled);
            }
            else if (Pressed)
            {
                OnStateChange(ExControlState.Pressed);
            }
            else if (Hovering)
            {
                OnStateChange(ExControlState.Hover);
            }
            else
            {
                OnStateChange(EnableKeyboardNavigation && Focused ? ExControlState.Focused : ExControlState.Normal);
            }
        }

        protected virtual void OnStateChange(ExControlState newState)
        {
            State = newState;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            UpdateControlState();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _Pressed = true;

            UpdateControlState();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            _Pressed = false;
            
            UpdateControlState();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            _Hovering = true;

            UpdateControlState();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            _Hovering = false;

            UpdateControlState();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!EnableKeyboardNavigation)
                return;

            if (Enabled && (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space))
            {
                _Pressed = true;
                UpdateControlState();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (!EnableKeyboardNavigation)
                return;

            if (Enabled && (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space))
            {
                _Pressed = false;
                UpdateControlState();
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            if (EnableKeyboardNavigation && Enabled && State == ExControlState.Normal)
                UpdateControlState();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (EnableKeyboardNavigation && Enabled && State == ExControlState.Focused)
                UpdateControlState();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (EnableTransparency)
                base.OnPaintBackground(pevent);
        }
    }
}
