using System.Windows.Forms;

namespace GameEngine.Engine
{
    public class Key
    {
        public Keys KeyCode { get; }
        private bool _keyDown;
        private bool _keyPress;
        private bool _keyUp;
        private bool _keyFirst;

        public Key(Keys keyCode)
        {
            KeyCode = keyCode;
            EngineController.Windows.KeyDown += WindowsOnKeyDown;
            EngineController.Windows.KeyUp += WindowsOnKeyUp;
        }

        private void WindowsOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == KeyCode && !_keyFirst)
            {
                _keyPress = true;
                _keyDown = true;
                _keyFirst = true;
            }
        }
    
        private void WindowsOnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == KeyCode)
            {
                _keyUp = true;
                _keyPress = false;
                if (_keyFirst)
                {
                    _keyFirst = false;
                }
            }
        }

        public void Update()
        {
            if (_keyDown)
            {
                _keyDown = false;
            }
        
            if (_keyUp)
            {
                _keyUp = false;
            }
        }
    
        public bool GetKeyDown()
        {
            return _keyDown;
        }
    
        public bool GetKeyPress()
        {
            return _keyPress;
        }

        public bool GetKeyUp()
        {
            return _keyUp;
        }
    }
}