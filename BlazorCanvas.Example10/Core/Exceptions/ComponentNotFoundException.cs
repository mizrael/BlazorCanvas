using System;
using BlazorCanvas.Example10.Core.Components;

namespace BlazorCanvas.Example10.Core.Exceptions
{
    public class ComponentNotFoundException<TC> : Exception where TC : IComponent
    {
        public ComponentNotFoundException() : base($"{typeof(TC).Name} not found on owner")
        {
        }
    }
}