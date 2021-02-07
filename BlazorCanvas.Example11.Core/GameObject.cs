using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorCanvas.Example11.Core.Components;

namespace BlazorCanvas.Example11.Core
{
    public class GameObject 
    {
        private static int _lastId = 0;

        private readonly IList<GameObject> _children;

        public GameObject()
        {
            this.Id = ++_lastId;

            _children = new List<GameObject>();

            this.Components = new ComponentsCollection(this);
        }

        public int Id { get; }

        public ComponentsCollection Components { get; }

        public IEnumerable<GameObject> Children => _children;
        public GameObject Parent { get; private set; }

        public OnDisabledHandler OnDisabled;
        public delegate void OnDisabledHandler(GameObject gameObject);
        
        private bool _enabled = true;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                var oldValue = _enabled;
                _enabled = value;
                if(!_enabled && oldValue)
                    this.OnDisabled?.Invoke(this);
            }
        }
        
        public void AddChild(GameObject child)
        {
            if (this.Equals(child.Parent))
                return;
            
            child.Parent?._children.Remove(child);
            child.Parent = this;
            _children.Add(child);
        }

        public async ValueTask Update(GameContext game)
        {
            if (!Enabled)
                return;
            
            foreach (var component in this.Components)
                await component.Update(game);

            foreach (var child in _children)
                await child.Update(game);
        }

        public override int GetHashCode() => this.Id;

        public override bool Equals(object obj) => obj is GameObject node && this.Id.Equals(node.Id);

        public override string ToString() => $"GameObject {this.Id}";
    }
}