using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping.Internal;

internal sealed class MapperActionCollection : IMapperActionCollection
{
    private readonly IMapperAction[] actions;

    public MapperActionCollection(IList<IMapperAction> before, IList<IMapperAction> maps, IList<IMapperAction> after)
    {
        var stack = new Stack<IMapperAction>();

        foreach (var item in before)
        {
            stack.Push(item);
        }
        foreach (var item in maps)
        {
            stack.Push(item);
        }
        foreach (var item in after)
        {
            stack.Push(item);
        }

        this.actions = stack.ToArray();
    }

    public int Count => this.actions.Length;

    public IEnumerator<IMapperAction> GetEnumerator() => new MapperActionEnumerator(this.actions);
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


    private class MapperActionEnumerator : IEnumerator<IMapperAction>
    {
        private readonly IMapperAction[] actions;
        private int index;

        public MapperActionEnumerator(IMapperAction[] actions)
        {
            this.actions = actions;
            this.index = -1;
        }
        public IMapperAction Current => actions[index];
        object IEnumerator.Current => this.Current;


        public bool MoveNext()
        {
            index++;
            return index < actions.Length;
        }
        public void Reset()
        {
            index = -1;
        }
        public void Dispose() => Reset();
    }
}
