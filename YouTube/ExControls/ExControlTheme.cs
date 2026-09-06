using System;
using System.Drawing;

namespace YouTube.ExControls
{
    public class ExControlTheme
    {
        public event EventHandler ColorsUpdated;

        public ExControlTheme()
        { }

        public ExControlTheme(Color normal)
        {
            _Normal = normal;
        }

        private void DispatchEvent()
        {
            if (ColorsUpdated != null)
                ColorsUpdated.Invoke(this, new EventArgs());
        }

        private Color _Normal, _Disabled, _Pressed, _Focused;

        public Color Normal 
        {
            get { return _Normal; }
            set 
            {
                if (_Normal != value)
                {
                    _Normal = value;
                    DispatchEvent();
                }
            }
        }
        
        public Color Disabled
        {
            get 
            {
                if (_Disabled == null) 
                    return _Normal;

                return _Disabled; 
            }
            set
            {
                if (_Disabled != value)
                {
                    _Disabled = value;
                    DispatchEvent();
                }
            }
        }

        public Color Pressed
        {
            get
            {
                if (_Pressed == null)
                    return _Normal;

                return _Pressed;
            }
            set
            {
                if (_Pressed != value)
                {
                    _Pressed = value;
                    DispatchEvent();
                }
            }
        }

        public Color Focused
        {
            get
            {
                if (_Focused == null)
                    return _Normal;

                return _Focused;
            }
            set
            {
                if (_Focused != value)
                {
                    _Focused = value;
                    DispatchEvent();
                }
            }
        }

        public Color GetStateColor(ExControlState state)
        {
            switch(state)
            {
                case ExControlState.Disabled:
                    return Disabled;
                case ExControlState.Focused:
                    return Focused;
                case ExControlState.Pressed:
                    return Pressed;
                default:
                    return Normal;
            }
        }
    }
}
