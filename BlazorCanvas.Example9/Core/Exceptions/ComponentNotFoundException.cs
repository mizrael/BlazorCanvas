using System;
using BlazorCanvas.Example9.Core.Components;

namespace BlazorCanvas.Example9.Core.Exceptions
{
    public class ComponentNotFoundException<TC> : Exception where TC : IComponent
    {
        public ComponentNotFoundException() : base($"{typeof(TC).Name} not found on owner")
        {
        }
    }
}