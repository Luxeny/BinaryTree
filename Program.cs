using System;
using BinaryTreeCollection;

class Program
{
    static void Main()
    {
        var tree = new BinaryTree<int>();
        tree.Add(5);
        tree.Add(3);
        tree.Add(7);
        tree.Add(2);
        tree.Add(4);
        tree.Add(6);
        tree.Add(8);

        Console.WriteLine("In-order traversal (default):");
        foreach (var item in tree)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        Console.WriteLine("Pre-order traversal:");
        foreach (var item in tree.PreOrder)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        Console.WriteLine("Post-order traversal:");
        foreach (var item in tree.PostOrder)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        Console.WriteLine("In-order traversal with lambda:");
        foreach (var item in tree.InOrderTraversal)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        Console.WriteLine("Forward iteration (custom enumerator):");
        var forward = new ForwardEnumerator<int>(tree.Root);
        while (forward.MoveNext())
        {
            Console.Write(forward.Current + " ");
        }
        Console.WriteLine();

        Console.WriteLine("Backward iteration (custom enumerator):");
        var backward = new BackwardEnumerator<int>(tree.Root);
        while (backward.MoveNext())
        {
            Console.Write(backward.Current + " ");
        }
        Console.WriteLine();
    }
}