using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorCanvas.Example11.Core.Utils;

namespace BlazorCanvas.Example11.Core
{
    public class InputService : IGameService
    {
        private readonly IDictionary<MouseButtons, ButtonState> _buttonStates;
        private readonly IDictionary<Keys, ButtonState> _keyboardStates;

        public InputService()
        {
            _buttonStates = EnumUtils.GetAllValues<MouseButtons>()
                .ToDictionary(v => v, v => ButtonState.None);

            _keyboardStates = EnumUtils.GetAllValues<Keys>()
                                       .ToDictionary(v => v, v => ButtonState.None);
        }
        
        public void SetButtonState(MouseButtons button, ButtonState.States state)
        {
            var oldState = _buttonStates[button];
            _buttonStates[button] = new ButtonState(state, oldState.State == ButtonState.States.Down);
        }

        public ButtonState GetButtonState(MouseButtons button) => _buttonStates[button];

        public void SetKeyState(Keys key, ButtonState.States state)
        {
            var oldState = _keyboardStates[key];
            _keyboardStates[key] = new ButtonState(state, oldState.State == ButtonState.States.Down);
        }

        public ButtonState GetKeyState(Keys key) => _keyboardStates[key];
        
        public ValueTask Step() => ValueTask.CompletedTask;
    }

    public enum MouseButtons
    {
        Left = 0,
        Middle = 1,
        Right = 2
    }

    public struct ButtonState
    {
        public ButtonState(States state, bool wasPressed)
        {
            State = state;
            WasPressed = wasPressed;
        }

        public bool WasPressed { get; }
        public States State { get; }

        public enum States
        {
            Up = 0,
            Down = 1
        }

        public static readonly ButtonState None = new ButtonState(States.Up, false);
    }

    public enum Keys
    {
        Up = 38,
        Left = 37,
        Down = 40,
        Right = 39,
        Space = 32,
        LeftCtrl = 17,
        LeftAlt = 18,
    }
}