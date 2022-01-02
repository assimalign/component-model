using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class MapperPaths : IDictionary<string, Type>
    {
        private int         count;
        private string[]    keys;
        private Type[]      values;

        public MapperPaths()
        {
            
        }
        


        public Type this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<string> Keys => this.keys;

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Type> Values => this.Values;

        /// <summary>
        /// 
        /// </summary>
        public int Count => this.count;

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(string key, Type value)
        {
            
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<string, Type> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, Type> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, Type>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, Type>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, Type> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out Type value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }




        public static MapperPaths Create(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
