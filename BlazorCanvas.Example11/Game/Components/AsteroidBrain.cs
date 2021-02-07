using System.Threading.Tasks;
using BlazorCanvas.Example11.Core;
using BlazorCanvas.Example11.Core.Components;

namespace BlazorCanvas.Example11.Game.Components
{
    public class AsteroidBrain : BaseComponent
    {
        private readonly TransformComponent _transform;
        private readonly BoundingBoxComponent _boundingBox;

        public float _speed = 0.0025f;

        private AsteroidBrain(GameObject owner) : base(owner)
        {   
            _transform = owner.Components.Get<TransformComponent>();
            _boundingBox = owner.Components.Get<BoundingBoxComponent>();
            _boundingBox.OnCollision += (sender, collidedWith) =>
            {
                // check if we're colliding with another asteroid
                if(!collidedWith.Owner.Components.TryGet<AsteroidBrain>(out var _))
                    this.Owner.Enabled = false;
            };
        }

        public override async ValueTask Update(GameContext game)
        {
            _transform.Local.Rotation += _speed * game.GameTime.ElapsedMilliseconds;
        }
    }
}