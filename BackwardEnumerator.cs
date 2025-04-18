using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTreeCollection
{
    public class BackwardEnumerator<T> : IEnumerator<T> where T : IComparable<T>
    {
        private readonly BinaryTreeNode<T> _root;
        private BinaryTreeNode<T> _current;
        private Stack<BinaryTreeNode<T>> _stack;
        private bool _started;

        public BackwardEnumerator(BinaryTreeNode<T> root)
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
                _current = _root;
                while (_current.Right != null)
                {
                    _stack.Push(_current);
                    _current = _current.Right;
                }
                return true;
            }

            if (_current.Left != null)
            {
                _current = _current.Left;
                while (_current.Right != null)
                {
                    _stack.Push(_current);
                    _current = _current.Right;
                }
                return true;
            }

            if (_stack.Count > 0)
            {
                _current = _stack.Pop();
                return true;
            }

            _current = null;
            return false;
        }

        public void Reset()
        {
            _current = null;
            _stack.Clear();
            _started = false;
        }

        public T Current => _current != null ? _current.Value : default;
        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}
