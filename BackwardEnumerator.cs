using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTreeCollection
{
    public class BackwardEnumerator<T> : IEnumerator<T> where T : IComparable<T>
    {
        private BinaryTreeNode<T> current;
        private readonly BinaryTreeNode<T> root;
        private bool enumerationStarted;

        public BackwardEnumerator(BinaryTreeNode<T> root)
        {
            this.root = root;
            Reset();
        }

        public bool MoveNext()
        {
            if (root == null) return false;

            if (!enumerationStarted)
            {
                current = FindRightmostNode(root);
                enumerationStarted = true;
                return current != null;
            }

            if (current == null) return false;

            current = GetPreviousNode(current);
            return current != null;
        }

        private BinaryTreeNode<T> GetPreviousNode(BinaryTreeNode<T> node)
        {
            if (node.Left == null)
            {
                if (node.Parent == null || node == node.Parent.Left)
                    return null;
                else
                    return node.Parent;
            }
            else
            {
                BinaryTreeNode<T> previousNode = node.Left.Right;
                while (previousNode != null && previousNode.Right != null)
                    previousNode = previousNode.Right;
                return previousNode ?? node.Left;
            }
        }

        private BinaryTreeNode<T> FindRightmostNode(BinaryTreeNode<T> node)
        {
            while (node.Right != null)
                node = node.Right;
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