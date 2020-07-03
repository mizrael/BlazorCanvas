using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;

namespace BlazorCanvas.Example7.Core.Components
{
    public class Transform : BaseComponent
    {
        public Transform(GameObject owner) : base(owner)
        {
        }

        public override async ValueTask Update(GameContext game)
        {
            _boundingBox.Size = this.Size;
            _boundingBox.X = (int)this.Position.X;
            _boundingBox.Y = (int)this.Position.Y;
        }

        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Direction { get; set; } = Vector2.UnitX;
        public Size Size { get; set; } = Size.Empty;

        private Rectangle _boundingBox;
        public Rectangle BoundingBox => _boundingBox;
    }
}