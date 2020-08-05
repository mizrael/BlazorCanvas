using System.Threading.Tasks;
using BlazorCanvas.Example10.Core;
using BlazorCanvas.Example10.Core.Components;

namespace BlazorCanvas.Example10
{
    public class AsteroidBrainComponent : BaseComponent
    {
        private readonly TransformComponent _transform;

        public float _speed = 0.0025f;

        private AsteroidBrainComponent(GameObject owner) : base(owner)
        {
            _transform = owner.Components.Get<TransformComponent>();
        }

        public override async ValueTask Update(GameContext game)
        {
            _transform.Local.Rotation += _speed * game.GameTime.ElapsedMilliseconds;
        }
    }
}