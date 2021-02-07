using System.Drawing;
using System.Threading.Tasks;
using BlazorCanvas.Example11.Core;
using BlazorCanvas.Example11.Core.Components;

namespace BlazorCanvas.Example11.Game.Components
{
    public class PlayerBrain : BaseComponent
    {
        private readonly MovingBody _movingBody;
        private readonly TransformComponent _transform;
        private readonly SpriteRenderComponent _spriteRender;
        private readonly BoundingBoxComponent _boundingBox;

        private readonly Size _halfSize;
        
        private float _enginePower = 1000f;
        private float _rotationSpeed = 10f;

        public PlayerBrain(GameObject owner) : base(owner)
        {
            _movingBody = owner.Components.Get<MovingBody>();
            _transform = owner.Components.Get<TransformComponent>();
            _spriteRender = owner.Components.Get<SpriteRenderComponent>();
            _halfSize = _spriteRender.Sprite.Bounds.Size / 2;

            _boundingBox = owner.Components.Get<BoundingBoxComponent>();
            _boundingBox.OnCollision += (sender, collidedWith) =>
            {
                this.Owner.Enabled = false;
            };
        }

        public override async ValueTask Update(GameContext game)
        {
            var inputService = game.GetService<InputService>();
            HandleMovement(game, inputService);
        }

        private void HandleMovement(GameContext game, InputService inputService)
        {
            if (_transform.World.Position.X < -_spriteRender.Sprite.Bounds.Width)
                _transform.Local.Position.X = game.Display.Size.Width + _halfSize.Width;
            else if (_transform.World.Position.X > game.Display.Size.Width + _spriteRender.Sprite.Bounds.Width)
                _transform.Local.Position.X = -_halfSize.Width;

            if (_transform.World.Position.Y < -_spriteRender.Sprite.Bounds.Height)
                _transform.Local.Position.Y = game.Display.Size.Height + _halfSize.Height;
            else if (_transform.World.Position.Y > game.Display.Size.Height + _spriteRender.Sprite.Bounds.Height)
                _transform.Local.Position.Y = -_halfSize.Height;

            if (inputService.GetKeyState(Keys.Right).State == ButtonState.States.Down)
                _movingBody.RotationSpeed = _rotationSpeed;
            else if (inputService.GetKeyState(Keys.Left).State == ButtonState.States.Down)
                _movingBody.RotationSpeed = -_rotationSpeed;
            else
                _movingBody.RotationSpeed = 0f;

            if (inputService.GetKeyState(Keys.Up).State == ButtonState.States.Down)
                _movingBody.Thrust = _enginePower;
            else if (inputService.GetKeyState(Keys.Down).State == ButtonState.States.Down)
                _movingBody.Thrust = -_enginePower;
            else
                _movingBody.Thrust = 0f;
        }
    }
}