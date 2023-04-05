using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Services.Core.Validation.PayloadValidation
{
    public sealed class ValidationPath : IList<ValidationNode>
    {
        private List<ValidationNode> __nodes;
        object _mutex = new object();

        public ValidationPath()
        {
            __nodes = new List<ValidationNode>();
        }

        public ValidationNode this[int index] { get => __nodes[index]; set => __nodes[index] = value; }

        public int Count => __nodes.Count;

        public bool IsReadOnly => false;

        public void Add(ValidationNode item)
        {
            lock (_mutex)
            {
                __nodes.Add(item);
            }
        }

        public void Clear()
        {
            lock (_mutex)
            {
                __nodes.Clear();
            }
        }

        public bool Contains(ValidationNode item)
        {
            return __nodes.Contains(item);
        }

        public void CopyTo(ValidationNode[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public IEnumerator<ValidationNode> GetEnumerator()
        {
            return __nodes.GetEnumerator();
        }

        public int IndexOf(ValidationNode item)
        {
            return __nodes.IndexOf(item);
        }

        public void Insert(int index, ValidationNode item)
        {
            lock (_mutex)
            {
                __nodes.Insert(index, item);
            }
        }

        public bool Remove(ValidationNode item)
        {
            lock (_mutex)
            {
                return __nodes.Remove(item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (_mutex)
            {
                __nodes.RemoveAt(index);
            }
        }

        public override string ToString()
        {
            if (__nodes.Any())
            {
                var visits = __nodes.Select(x => $"{(string.IsNullOrWhiteSpace(x.ParentId) ? "SequenceStart" : x.ParentId)}.{x.Id}");
                return string.Join("->", visits);
            }

            return string.Empty;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return __nodes.GetEnumerator();
        }

        public void AddRange(IList<ValidationNode> nodes)
        {
            lock (_mutex)
            {
                __nodes.AddRange(nodes);
            }
        }
    }
}
