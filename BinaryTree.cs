using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTreeCollection
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public BinaryTreeNode<T> Root { get; private set; }
        public int Count { get; private set; }

        public void Add(T value)
        {
            if (Root == null)
            {
                Root = new BinaryTreeNode<T>(value);
                Count++;
                return;
            }

            var currentNode = Root;
            while (true)
            {
                int comparison = value.CompareTo(currentNode.Value);
                if (comparison < 0)
                {
                    if (currentNode.Left == null)
                    {
                        currentNode.Left = new BinaryTreeNode<T>(value) { Parent = currentNode };
                        Count++;
                        return;
                    }
                    currentNode = currentNode.Left;
                }
                else
                {
                    if (currentNode.Right == null)
                    {
                        currentNode.Right = new BinaryTreeNode<T>(value) { Parent = currentNode };
                        Count++;
                        return;
                    }
                    currentNode = currentNode.Right;
                }
            }
        }

        public IEnumerator<T> GetEnumerator() => new InOrderEnumerator<T>(Root);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> PreOrder => TraversePreOrder(Root);
        public IEnumerable<T> PostOrder => TraversePostOrder(Root);
        public IEnumerable<T> InOrderTraversal => TraverseInOrder(Root);

        private IEnumerable<T> TraversePreOrder(BinaryTreeNode<T> node)
        {
            if (node == null) yield break;
            yield return node.Value;
            foreach (var left in TraversePreOrder(node.Left)) yield return left;
            foreach (var right in TraversePreOrder(node.Right)) yield return right;
        }

        private IEnumerable<T> TraversePostOrder(BinaryTreeNode<T> node)
        {
            if (node == null) yield break;
            foreach (var left in TraversePostOrder(node.Left)) yield return left;
            foreach (var right in TraversePostOrder(node.Right)) yield return right;
            yield return node.Value;
        }

        private IEnumerable<T> TraverseInOrder(BinaryTreeNode<T> node)
        {
            if (node == null) yield break;
            foreach (var left in TraverseInOrder(node.Left)) yield return left;
            yield return node.Value;
            foreach (var right in TraverseInOrder(node.Right)) yield return right;
        }
    }
}
