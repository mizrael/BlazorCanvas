using System;
using System.Collections.Generic;
using System.Drawing;

namespace BlazorCanvas.Example5.Core
{
    public class InputSystem
    {
        private readonly IDictionary<MouseButtons, ButtonStates> _buttonStates;

        private InputSystem()
        {
            _buttonStates = new Dictionary<MouseButtons, ButtonStates>()
            {
                {MouseButtons.Left, ButtonStates.Up},
                {MouseButtons.Middle, ButtonStates.Up},
                {MouseButtons.Right, ButtonStates.Up},
            };
        }

        public Point MouseCoords;

        private static readonly Lazy<InputSystem> _instance = new Lazy<InputSystem>(new InputSystem());
        public static InputSystem Instance => _instance.Value;

        public void SetButtonState(MouseButtons button, ButtonStates state) => _buttonStates[button] = state;

        public ButtonStates GetButtonState(MouseButtons button) => _buttonStates[button];
    }

    public enum MouseButtons
    {
        Left = 0,
        Middle = 1,
        Right = 2
    }

    public enum ButtonStates
    {
        Up = 0,
        Down = 1
    }
}