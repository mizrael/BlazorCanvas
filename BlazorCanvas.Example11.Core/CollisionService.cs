using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using BlazorCanvas.Example11.Core.Components;

namespace BlazorCanvas.Example11.Core
{
    internal class CollisionBucket
    {   
        private readonly HashSet<BoundingBoxComponent> _colliders = new();

        public CollisionBucket(Rectangle bounds)
        {
            Bounds = bounds;
        }
        
        public Rectangle Bounds { get; }

        public void Add(BoundingBoxComponent bbox) => _colliders.Add(bbox);
        
        public void Remove(BoundingBoxComponent bbox) => _colliders.Remove(bbox);

        public void CheckCollisions(BoundingBoxComponent bbox)
        {
            foreach (var collider in _colliders)
            {
                if (collider.Owner == bbox.Owner || !bbox.Bounds.IntersectsWith(collider.Bounds))
                    continue;
#if DEBUG
                Console.WriteLine($"collision detected {bbox.Owner.Id} => {collider.Owner.Id}");
#endif
                collider.CollideWith(bbox);
                bbox.CollideWith(collider);
            }
        }
    }
    
    public class CollisionService : IGameService
    {
        private readonly GameContext _game;
        
        private CollisionBucket[,] _buckets;
        private readonly Size _bucketSize;
        private Dictionary<int, IList<CollisionBucket>> _bucketsByCollider = new();
        
        public CollisionService(GameContext game, Size bucketSize)
        {
            _game = game;
            _bucketSize = bucketSize;
            _game.Display.OnSizeChanged += BuildBuckets;
        }

        private void BuildBuckets()
        {
            var rows = _game.Display.Size.Height / _bucketSize.Height;
            var cols = _game.Display.Size.Width / _bucketSize.Width;
            _buckets = new CollisionBucket[rows, cols];

            for (int row = 0; row < rows; row++)
            for (int col = 0; col < cols; col++)
            {
                var bounds = new Rectangle(
                    col * _bucketSize.Width,
                    row * _bucketSize.Height,
                    _bucketSize.Width, 
                    _bucketSize.Height);
                _buckets[row, col] = new CollisionBucket(bounds);
            }
            
            var colliders = FindAllColliders();
            foreach (var collider in colliders)
            {
                collider.OnPositionChanged -= CheckCollisions;
                collider.OnPositionChanged += CheckCollisions;
                RefreshColliderBuckets(collider);
            }
        }

        private void CheckCollisions(BoundingBoxComponent bbox)
        {
            RefreshColliderBuckets(bbox);
            
            var buckets = _bucketsByCollider[bbox.Owner.Id];
            foreach (var bucket in buckets)
            {
                bucket.CheckCollisions(bbox);
            }
        }
        
        private void RefreshColliderBuckets(BoundingBoxComponent collider)
        {
            var rows = _buckets.GetLength(0);
            var cols = _buckets.GetLength(1);
            var startX = (int) (cols * ((float) collider.Bounds.Left / _game.Display.Size.Width));
            var startY = (int) (rows * ((float) collider.Bounds.Top / _game.Display.Size.Height));

            var endX = (int) (cols * ((float) collider.Bounds.Right / _game.Display.Size.Width));
            var endY = (int) (rows * ((float) collider.Bounds.Bottom / _game.Display.Size.Height));

            if (!_bucketsByCollider.ContainsKey(collider.Owner.Id))
                _bucketsByCollider[collider.Owner.Id] = new List<CollisionBucket>();
            foreach (var bucket in _bucketsByCollider[collider.Owner.Id])
                bucket.Remove(collider);
            _bucketsByCollider[collider.Owner.Id].Clear();

            for (int row = startY; row <= endY; row++)
            for (int col = startX; col <= endX; col++)
            {
                if (row < 0 || row >= rows)
                    continue;
                if (col < 0 || col >= cols)
                    continue;

                if (_buckets[row, col].Bounds.IntersectsWith(collider.Bounds))
                {
                    _bucketsByCollider[collider.Owner.Id].Add(_buckets[row, col]);
                    _buckets[row, col].Add(collider);
                }
            }
        }

        private IEnumerable<BoundingBoxComponent> FindAllColliders()
        {
            var scenegraph = _game.GetService<SceneGraph>();
            var colliders = new List<BoundingBoxComponent>();

            FindAllColliders(scenegraph.Root, colliders);
            
            return colliders;
        }

        private void FindAllColliders(GameObject node, IList<BoundingBoxComponent> colliders)
        {
            if (node is null)
                return;
            
            if(node.Components.TryGet<BoundingBoxComponent>(out var bbox))
                colliders.Add(bbox);
            
            if(node.Children is not null)
                foreach(var child in node.Children)
                    FindAllColliders(child, colliders);
        }

        public ValueTask Step()
        {
            if(null == _buckets)
                BuildBuckets();
            return ValueTask.CompletedTask;
        }
    }
}