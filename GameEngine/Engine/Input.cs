using System.Collections.Generic;
using System.Windows.Forms;

namespace GameEngine.Engine
{
    public static class Input
    {
        private static List<Key> _keys = new List<Key>();

        public static void Initialize()
        {
            _keys.Add(new Key(Keys.A));
            _keys.Add(new Key(Keys.B));
            _keys.Add(new Key(Keys.C));
            _keys.Add(new Key(Keys.D));
            _keys.Add(new Key(Keys.E));
            _keys.Add(new Key(Keys.F));
            _keys.Add(new Key(Keys.G));
            _keys.Add(new Key(Keys.H));
            _keys.Add(new Key(Keys.I));
            _keys.Add(new Key(Keys.J));
            _keys.Add(new Key(Keys.K));
            _keys.Add(new Key(Keys.L));
            _keys.Add(new Key(Keys.M));
            _keys.Add(new Key(Keys.N));
            _keys.Add(new Key(Keys.O));
            _keys.Add(new Key(Keys.P));
            _keys.Add(new Key(Keys.Q));
            _keys.Add(new Key(Keys.R));
            _keys.Add(new Key(Keys.S));
            _keys.Add(new Key(Keys.T));
            _keys.Add(new Key(Keys.U));
            _keys.Add(new Key(Keys.V));
            _keys.Add(new Key(Keys.W));
            _keys.Add(new Key(Keys.X));
            _keys.Add(new Key(Keys.Y));
            _keys.Add(new Key(Keys.Z));
            _keys.Add(new Key(Keys.D0));
            _keys.Add(new Key(Keys.D1));
            _keys.Add(new Key(Keys.D2));
            _keys.Add(new Key(Keys.D3));
            _keys.Add(new Key(Keys.D4));
            _keys.Add(new Key(Keys.D5));
            _keys.Add(new Key(Keys.D6));
            _keys.Add(new Key(Keys.D7));
            _keys.Add(new Key(Keys.D8));
            _keys.Add(new Key(Keys.D9));
            _keys.Add(new Key(Keys.NumPad0));
            _keys.Add(new Key(Keys.NumPad1));
            _keys.Add(new Key(Keys.NumPad2));
            _keys.Add(new Key(Keys.NumPad3));
            _keys.Add(new Key(Keys.NumPad4));
            _keys.Add(new Key(Keys.NumPad5));
            _keys.Add(new Key(Keys.NumPad6));
            _keys.Add(new Key(Keys.NumPad7));
            _keys.Add(new Key(Keys.NumPad8));
            _keys.Add(new Key(Keys.NumPad9));
            _keys.Add(new Key(Keys.F1));
            _keys.Add(new Key(Keys.F2));
            _keys.Add(new Key(Keys.F3));
            _keys.Add(new Key(Keys.F4));
            _keys.Add(new Key(Keys.F5));
            _keys.Add(new Key(Keys.F6));
            _keys.Add(new Key(Keys.F7));
            _keys.Add(new Key(Keys.F8));
            _keys.Add(new Key(Keys.F9));
            _keys.Add(new Key(Keys.F10));
            _keys.Add(new Key(Keys.F11));
            _keys.Add(new Key(Keys.F12));
            _keys.Add(new Key(Keys.F13));
            _keys.Add(new Key(Keys.F14));
            _keys.Add(new Key(Keys.F15));
            _keys.Add(new Key(Keys.Space));
            _keys.Add(new Key(Keys.LShiftKey));
            _keys.Add(new Key(Keys.RShiftKey));
            _keys.Add(new Key(Keys.Shift));
            _keys.Add(new Key(Keys.LControlKey));
            _keys.Add(new Key(Keys.RControlKey));
            _keys.Add(new Key(Keys.Control));
            _keys.Add(new Key(Keys.ControlKey));
            _keys.Add(new Key(Keys.Tab));
            _keys.Add(new Key(Keys.Escape));
            _keys.Add(new Key(Keys.Up));
            _keys.Add(new Key(Keys.Down));
            _keys.Add(new Key(Keys.Enter));
            Debug.Info("Initialize Input Completed.");
        }

        public static void Update()
        {
            for (var i = 0; i < _keys.Count; i++)
            {
                _keys[i].Update();
            }
        }
        
        public static bool GetKeyDown(Keys keyCode)
        {
            for (var i = 0; i < _keys.Count; i++)
            {
                if (_keys[i].KeyCode == keyCode)
                {
                    return _keys[i].GetKeyDown();
                }
            }
            return false;
        }

        public static bool GetKeyPress(Keys keyCode)
        {
            for (var i = 0; i < _keys.Count; i++)
            {
                if (_keys[i].KeyCode == keyCode)
                {
                    return _keys[i].GetKeyPress();
                }
            }
            return false;
        }

        public static bool GetKeyUp(Keys keyCode)
        {
            for (var i = 0; i < _keys.Count; i++)
            {
                if (_keys[i].KeyCode == keyCode)
                {
                    return _keys[i].GetKeyUp();
                }
            }
            return false;
        }
    }
}