using System.Drawing;

namespace BlazorCanvas.Example11.Core
{
    public class Display
    {
        private Size _size;
        public Size Size
        {
            get => _size;
            set
            {
                _size = value;
                OnSizeChanged?.Invoke();
            }
        }

        public event OnSizeChangedHandler OnSizeChanged;
        public delegate void OnSizeChangedHandler();
    }
}