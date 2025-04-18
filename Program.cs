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

        Console.WriteLine("Прямой обход дерева (pre-order):");
        foreach (var item in tree.PreOrder) Console.Write(item + " ");

        Console.WriteLine("\nОбратный обход дерева (post-order):");
        foreach (var item in tree.PostOrder) Console.Write(item + " ");

        Console.WriteLine("\nСимметричный обход (in-order):");
        foreach (var item in tree) Console.Write(item + " ");

        Console.WriteLine("\nПрямой перебор (специальный итератор):");
        var forward = new ForwardEnumerator<int>(tree.Root);
        while (forward.MoveNext()) Console.Write(forward.Current + " ");

        Console.WriteLine("\nОбратный перебор (специальный итератор):");
        var backward = new BackwardEnumerator<int>(tree.Root);
        while (backward.MoveNext()) Console.Write(backward.Current + " ");
    }
}
