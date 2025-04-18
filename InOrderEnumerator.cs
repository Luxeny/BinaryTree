using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTreeCollection
{
  public class InOrderEnumerator<T> : IEnumerator<T> where T : IComparable<T>
  {
    private readonly BinaryTreeNode<T> root;
    private BinaryTreeNode<T> current;
    private Stack<BinaryTreeNode<T>> nodeStack;
    private bool enumerationStarted;

    public InOrderEnumerator(BinaryTreeNode<T> root)
    {
      this.root = root;
      nodeStack = new Stack<BinaryTreeNode<T>>();
      Reset();
    }

    public bool MoveNext()
    {
      if (root == null) return false;

      if (!enumerationStarted)
      {
        enumerationStarted = true;
        current = root;
        while (current != null && current.Left != null)
        {
          nodeStack.Push(current);
          current = current.Left;
        }
        return current != null;
      }

      if (current == null) return false;

      if (current.Right != null)
      {
        current = current.Right;
        while (current.Left != null)
        {
          nodeStack.Push(current);
          current = current.Left;
        }
        return true;
      }

      if (nodeStack.Count > 0)
      {
        current = nodeStack.Pop();
        return true;
      }

      current = null;
      return false;
    }

    public void Reset()
    {
      current = null;
      nodeStack.Clear();
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
