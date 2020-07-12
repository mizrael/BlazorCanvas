using System.Threading.Tasks;

namespace BlazorCanvas.Example9.Core.Components
{
    public class TransformComponent : BaseComponent
    {
        public TransformComponent(GameObject owner) : base(owner)
        {
        }

        public override async ValueTask Update(GameContext game)
        {
        }

        public Transform Transform { get; } = Transform.Identity;
    }
}