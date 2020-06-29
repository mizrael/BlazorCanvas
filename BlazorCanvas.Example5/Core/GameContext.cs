using System.Threading.Tasks;

namespace BlazorCanvas.Example5.Core
{
    public abstract class GameContext
    {
        public async ValueTask Step(float timeStamp)
        {
            this.GameTime.TotalTime = timeStamp;

            await Update();
            await Render();
        }

        protected abstract ValueTask Update();
        protected abstract ValueTask Render();

        public GameTime GameTime { get; } = new GameTime();
        public Display Display { get; } = new Display();
    }
}