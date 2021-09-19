using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    public class ValidationResults : IDictionary<string, ValidationResult>
    {
        public ValidationResult this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<string> Keys => throw new NotImplementedException();

        public ICollection<ValidationResult> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(string key, ValidationResult value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<string, ValidationResult> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, ValidationResult> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, ValidationResult>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, ValidationResult>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, ValidationResult> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out ValidationResult value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
