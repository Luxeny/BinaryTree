using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTreeCollection
{
    public class ForwardEnumerator<T> : IEnumerator<T> where T : IComparable<T>
    {
        private readonly BinaryTreeNode<T> _root;
        private Stack<BinaryTreeNode<T>> _stack;
        private bool _started;

        public ForwardEnumerator(BinaryTreeNode<T> root)
        {
            _root = root;
            _stack = new Stack<BinaryTreeNode<T>>();
            Reset();
        }

        public bool MoveNext()
        {
            if (_root == null) return false;

            if (!_started)
            {
                _started = true;
                _stack.Push(_root);
                return true;
            }

            if (_stack.Count == 0) return false;

            var current = _stack.Pop();

            if (current.Right != null) _stack.Push(current.Right);
            if (current.Left != null) _stack.Push(current.Left);

            return _stack.Count > 0;
        }

        public T Current => _stack.Count > 0 ? _stack.Peek().Value : default;
        object IEnumerator.Current => Current;

        public void Reset()
        {
            _stack.Clear();
            _started = false;
        }

        public void Dispose() { }
    }
}
