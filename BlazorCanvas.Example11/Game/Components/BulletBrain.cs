using System.Threading.Tasks;
using Blazor.Extensions;
using BlazorCanvas.Example11.Core;
using BlazorCanvas.Example11.Core.Components;

namespace BlazorCanvas.Example11.Game.Components
{
    public class BulletBrain : BaseComponent
    {
        private readonly MovingBody _movingBody;
        private readonly TransformComponent _transformComponent;
        
        public BulletBrain(GameObject owner) : base(owner)
        {
            _movingBody = owner.Components.Get<MovingBody>();
            _transformComponent = owner.Components.Get<TransformComponent>();
        }
        
        public override async ValueTask Update(GameContext game)
        {
            _movingBody.Thrust = this.Speed;

            var isOutScreen = _transformComponent.World.Position.X < 0 ||
                              _transformComponent.World.Position.Y < 0 ||
                              _transformComponent.World.Position.X > this.Canvas.Width ||
                              _transformComponent.World.Position.Y > this.Canvas.Height;
            if (isOutScreen)
                this.Owner.Enabled = false; 
        }

        public float Speed { get; set; }
        public BECanvasComponent Canvas { get; set; }
    }
}