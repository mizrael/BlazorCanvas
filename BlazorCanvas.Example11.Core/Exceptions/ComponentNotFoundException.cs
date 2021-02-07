using System;
using BlazorCanvas.Example11.Core.Components;

namespace BlazorCanvas.Example11.Core.Exceptions
{
    public class ComponentNotFoundException<TC> : Exception where TC : IComponent
    {
        public ComponentNotFoundException() : base($"{typeof(TC).Name} not found on owner")
        {
        }
    }
}