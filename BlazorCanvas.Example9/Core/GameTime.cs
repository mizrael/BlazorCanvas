using System.Diagnostics;

namespace BlazorCanvas.Example9.Core
{
    public class GameTime
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        private long _lastTick = 0;
        private long _elapsedTicks = 0;
        private long _elapsedMilliseconds = 0;
        private long _lastMilliseconds = 0;

        public void Start()
        {
            _stopwatch.Reset();
            _stopwatch.Start();

            _lastTick = 0;
            _lastMilliseconds = 0;
        }

        public void Step()
        {
            _elapsedTicks = _stopwatch.ElapsedTicks - _lastTick;
            _elapsedMilliseconds = _stopwatch.ElapsedMilliseconds - _lastMilliseconds;

            _lastTick = _stopwatch.ElapsedTicks;
            _lastMilliseconds = _stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// total time elapsed since the beginning of the game, in ticks
        /// </summary>
        public long TotalTicks => _stopwatch.ElapsedTicks;

        /// <summary>
        /// total time elapsed since the beginning of the game, in milliseconds
        /// </summary>
        public long TotalMilliseconds => _stopwatch.ElapsedMilliseconds;

        /// <summary>
        /// time elapsed since last frame, in ticks
        /// </summary>
        public long ElapsedTicks => _elapsedTicks;

        /// <summary>
        /// time elapsed since last frame, in milliseconds
        /// </summary>
        public long ElapsedMilliseconds => _elapsedMilliseconds;
    }
}