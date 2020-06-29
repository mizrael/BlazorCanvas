using System;
using System.Drawing;

namespace BlazorCanvas.Example5.Core
{
    public class InputSystem
    {
        private InputSystem()
        {
        }

        public Point Coords;

        private static readonly Lazy<InputSystem> _instance = new Lazy<InputSystem>(new InputSystem());
        public static InputSystem Instance => _instance.Value;
    }
}