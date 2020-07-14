using System;
using System.Numerics;
using System.Threading.Tasks;
using BlazorCanvas.Example9.Core;
using BlazorCanvas.Example9.Core.Components;

namespace BlazorCanvas.Example9
{
    public class OrbitComponent : BaseComponent
    {
        private readonly TransformComponent _transform;

        private readonly Vector2 _offset;
        private readonly float _speed = 0.0025f;

        public OrbitComponent(GameObject owner, Vector2 offset) : base(owner)
        {
            _offset = offset;
            _transform = owner.Components.Get<TransformComponent>();
        }

        public override async ValueTask Update(GameContext game)
        {
            _transform.Local.Position.X = MathF.Cos(game.GameTime.TotalMilliseconds * _speed) * _offset.X;
            _transform.Local.Position.Y = MathF.Sin(game.GameTime.TotalMilliseconds * _speed) * _offset.Y;
        }
    }
}