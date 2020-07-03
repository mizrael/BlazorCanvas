namespace BlazorCanvas.Example8.Core
{
    public class GameTime
    {
        private float _totalTime = 0;

        /// <summary>
        /// total time elapsed since the beginning of the game
        /// </summary>
        public float TotalTime
        {
            get => _totalTime;
            set
            {
                this.ElapsedTime = value - _totalTime;
                _totalTime = value;

            }
        }

        /// <summary>
        /// time elapsed since last frame
        /// </summary>
        public float ElapsedTime { get; private set; }
    }
}