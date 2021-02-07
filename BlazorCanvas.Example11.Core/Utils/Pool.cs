using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorCanvas.Example11.Core.Utils
{
    public class Pool<T>
    {
        private readonly Func<T> _factory;
        private readonly Queue<T> _items = new();
        
        public Pool(Func<T> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public T Get()
        {
            return _items.Any() ? _items.Dequeue() : _factory();
        }

        public void Return(T item)
        {
            if (item == null) 
                throw new ArgumentNullException(nameof(item));
            _items.Enqueue(item);
        }
    }
}