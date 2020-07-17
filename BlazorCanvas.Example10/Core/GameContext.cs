using System.Threading.Tasks;

namespace BlazorCanvas.Example10.Core
{
    public abstract class GameContext
    {
        private bool _isFirst = true;

        public async ValueTask Step()
        {
            if (_isFirst)
            {
                this.GameTime.Start();
                _isFirst = false;
            }

            this.GameTime.Step();

            await Update();
            await Render();
        }

        protected abstract ValueTask Update();
        protected abstract ValueTask Render();

        public GameTime GameTime { get; } = new GameTime();
        public Display Display { get; } = new Display();
    }
}