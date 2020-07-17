using System;
using System.Collections;
using System.Collections.Generic;
using BlazorCanvas.Example10.Core.Exceptions;

namespace BlazorCanvas.Example10.Core.Components
{
    public class ComponentsCollection : IEnumerable<IComponent>
    {
        private readonly IDictionary<Type, IComponent> _items;

        public ComponentsCollection()
        {
            _items = new Dictionary<Type, IComponent>();
        }

        public bool Add(IComponent component)
        {
            var type = component.GetType();
            if (_items.ContainsKey(type))
                return false;

            _items.Add(type, component);
            return true;
        }

        public T Get<T>() where T : class, IComponent
        {
            var type = typeof(T);
            return _items.ContainsKey(type) ? _items[type] as T : throw new ComponentNotFoundException<T>();
        }

        public bool TryGet<T>(out T result) where T : class, IComponent
        {
            var type = typeof(T);
            _items.TryGetValue(type, out var tmp);
            result = tmp as T;
            return result != null;
        }

        public IEnumerator<IComponent> GetEnumerator() => _items.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}