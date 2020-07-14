using System.Threading.Tasks;

namespace BlazorCanvas.Example9.Core.Components
{
    public class TransformComponent : BaseComponent
    {
        private Transform _local = Transform.Identity();
        private Transform _world = Transform.Identity();

        public TransformComponent(GameObject owner) : base(owner)
        {
        }

        public override async ValueTask Update(GameContext game)
        {
            _world.Clone(ref _local);

            var parentTransform = Owner.Parent?.Components.Get<TransformComponent>();
            if (parentTransform != null)
                _world.Position = _local.Position + parentTransform.World.Position;
        }

        public Transform Local => _local;
        public Transform World => _world;
    }
}