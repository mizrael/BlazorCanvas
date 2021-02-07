using System.Threading.Tasks;

namespace BlazorCanvas.Example11.Core
{
    public class SceneGraph : IGameService
    {
        private readonly GameContext _game;

        public SceneGraph(GameContext game)
        {
            _game = game;
            Root = new GameObject();
        }

        public async ValueTask Step()
        {
            if (null == Root)
                return;
            
            await Root.Update(_game);
        }
        
        public GameObject Root { get; }
    }
}