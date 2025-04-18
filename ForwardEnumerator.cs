using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTreeCollection
{
    public class ForwardEnumerator<T> : IEnumerator<T> where T : IComparable<T>
    {
        private BinaryTreeNode<T> current;
        private readonly BinaryTreeNode<T> root;
        private bool enumerationStarted;

        public ForwardEnumerator(BinaryTreeNode<T> root)
        {
            this.root = root;
            Reset();
        }

        public bool MoveNext()
        {
            if (root == null) return false;

            if (!enumerationStarted)
            {
                current = FindLeftmostNode(root);
                enumerationStarted = true;
                return current != null;
            }

            if (current == null) return false;

            current = GetNextNode(current);
            return current != null;
        }

        private BinaryTreeNode<T> GetNextNode(BinaryTreeNode<T> node)
        {
            if (node.Right == null)
            {
                if (node.Parent == null || node == node.Parent.Right)
                    return null;
                else
                    return node.Parent;
            }
            else
            {
                BinaryTreeNode<T> nextNode = node.Right.Left;
                while (nextNode != null && nextNode.Left != null)
                    nextNode = nextNode.Left;
                return nextNode ?? node.Right;
            }
        }

        private BinaryTreeNode<T> FindLeftmostNode(BinaryTreeNode<T> node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        public void Reset()
        {
            current = null;
            enumerationStarted = false;
        }

        public T Current => current != null ? current.Value : default(T);
        object IEnumerator.Current => Current;

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}