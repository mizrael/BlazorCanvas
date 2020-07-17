using System;
using System.Numerics;
using System.Threading.Tasks;
using BlazorCanvas.Example10.Core;
using BlazorCanvas.Example10.Core.Components;

namespace BlazorCanvas.Example10
{
    public class OrbitComponent : BaseComponent
    {
        private readonly TransformComponent _transform;

        private readonly Vector2 _offset;
        private readonly float _speed;

        public OrbitComponent(GameObject owner, Vector2 offset, float speed = 0.0025f) : base(owner)
        {
            _offset = offset;
            _speed = speed;
            _transform = owner.Components.Get<TransformComponent>();
        }

        public override async ValueTask Update(GameContext game)
        {
            _transform.Local.Position.X = MathF.Cos(game.GameTime.TotalMilliseconds * _speed) * _offset.X;
            _transform.Local.Position.Y = MathF.Sin(game.GameTime.TotalMilliseconds * _speed) * _offset.Y;
        }
    }
}