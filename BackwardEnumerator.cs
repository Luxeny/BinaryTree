using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTreeCollection
{
  public class BackwardEnumerator<T> : IEnumerator<T> where T : IComparable<T>
  {
    private BinaryTreeNode<T> current;
    private readonly BinaryTreeNode<T> root;
    private Stack<BinaryTreeNode<T>> stack;
    private bool enumerationStarted;

    public BackwardEnumerator(BinaryTreeNode<T> root)
    {
      this.root = root;
      this.stack = new Stack<BinaryTreeNode<T>>();
      Reset();
    }

    public bool MoveNext()
    {
      if (root == null) return false;

      if (!enumerationStarted)
      {
        enumerationStarted = true;
        current = root;
        while (current.Right != null)
        {
          stack.Push(current);
          current = current.Right;
        }
        return true;
      }

      if (current.Left != null)
      {
        current = current.Left;
        while (current.Right != null)
        {
          stack.Push(current);
          current = current.Right;
        }
        return true;
      }

      if (stack.Count > 0)
      {
        current = stack.Pop();
        return true;
      }

      current = null;
      return false;
    }

    public void Reset()
    {
      current = null;
      stack.Clear();
      enumerationStarted = false;
    }

    public T Current => current.Value;
    object IEnumerator.Current => Current;

    public void Dispose() { }
  }
}
