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
        ++Count;
        return;
      }

      BinaryTreeNode<T> currentNode = Root;
      while (true)
      {
        int comparisonResult = value.CompareTo(currentNode.Value);
        if (comparisonResult < 0)
        {
          if (currentNode.Left == null)
          {
            currentNode.Left = new BinaryTreeNode<T>(value) { Parent = currentNode };
            ++Count;
            return;
          }
          currentNode = currentNode.Left;
        }
        else
        {
          if (currentNode.Right == null)
          {
            currentNode.Right = new BinaryTreeNode<T>(value) { Parent = currentNode };
            ++Count;
            return;
          }
          currentNode = currentNode.Right;
        }
      }
    }

    public bool Contains(T value)
    {
      BinaryTreeNode<T> currentNode = Root;
      while (currentNode != null)
      {
        int comparisonResult = value.CompareTo(currentNode.Value);
        if (comparisonResult == 0) return true;
        currentNode = comparisonResult < 0 ? currentNode.Left : currentNode.Right;
      }
      return false;
    }

    public IEnumerator<T> GetEnumerator() => new InOrderEnumerator<T>(Root);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerable<T> PreOrder => TraversePreOrder(Root);
    private IEnumerable<T> TraversePreOrder(BinaryTreeNode<T> node)
    {
      if (node == null) yield break;
      
      yield return node.Value;
      foreach (T leftValue in TraversePreOrder(node.Left)) yield return leftValue;
      foreach (T rightValue in TraversePreOrder(node.Right)) yield return rightValue;
    }

    public IEnumerable<T> PostOrder => TraversePostOrder(Root);
    private IEnumerable<T> TraversePostOrder(BinaryTreeNode<T> node)
    {
      if (node == null) yield break;
      
      foreach (T leftValue in TraversePostOrder(node.Left)) yield return leftValue;
      foreach (T rightValue in TraversePostOrder(node.Right)) yield return rightValue;
      yield return node.Value;
    }

    public IEnumerable<T> InOrderTraversal => 
      TraverseInOrder(Root, node => node.Left, node => node.Right, node => node.Value);

    public static IEnumerable<T> TraverseInOrder(
      BinaryTreeNode<T> root,
      Func<BinaryTreeNode<T>, BinaryTreeNode<T>> leftSelector,
      Func<BinaryTreeNode<T>, BinaryTreeNode<T>> rightSelector,
      Func<BinaryTreeNode<T>, T> valueSelector)
    {
      if (root == null) yield break;

      var nodeStack = new Stack<BinaryTreeNode<T>>();
      BinaryTreeNode<T> current = root;

      while (nodeStack.Count > 0 || current != null)
      {
        if (current != null)
        {
          nodeStack.Push(current);
          current = leftSelector(current);
        }
        else
        {
          current = nodeStack.Pop();
          yield return valueSelector(current);
          current = rightSelector(current);
        }
      }
    }
  }
}
