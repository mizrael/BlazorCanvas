using System.Threading.Tasks;

namespace BlazorCanvas.Example10.Core.Components
{
    public class TransformComponent : BaseComponent
    {
        private Transform _local = Transform.Identity();
        private readonly Transform _world = Transform.Identity();

        private TransformComponent(GameObject owner) : base(owner)
        {
        }

        public override async ValueTask Update(GameContext game)
        {
            _world.Clone(ref _local);
            
            if (null != Owner.Parent && Owner.Parent.Components.TryGet<TransformComponent>(out var parentTransform))
                _world.Position = _local.Position + parentTransform.World.Position;
        }

        public Transform Local => _local;
        public Transform World => _world;
    }
}