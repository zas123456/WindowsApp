// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Node<T>
{
    public Node<T> Next { get; set; }
    public T Data { get; set; }
    
    public Node(T t)
    {
        Next = null;
        Data = t;
    }
}

public class GenericList<T>
{
    public Node<T> head;
    public Node<T> tail;

    public GenericList()
    {
        tail = head = null;
    }

    public Node<T> Head
    {
        get => head;
    }

    public void Add(T t)
    {
        Node<T> n = new Node<T>(t);
        if (tail == null)
        {
            head = tail = n;
        }
        else
        {
            tail.Next = n;
            tail = n;
        }
    }

    public void ForEach(Action<T> action)
    {
        for(Node<T> n =head;n!=null;n=n.Next)
        {
            action(n.Data);
        }
    }
}

public class creatList
{
    static void Main(string[] args)
    {
        GenericList<int> intlist=new GenericList<int>();
        Random random = new Random();
        for(int i=0;i<10;i++)
        {
            intlist.Add(random.Next(1000));
        }

        intlist.ForEach(n => Console.WriteLine(n));

        int min = int.MaxValue;
        int max= int.MinValue;
        double sum = 0;

        intlist.ForEach(n => { min = (n < min) ? n : min; });
        intlist.ForEach(n => { max=(n> max) ? n : max; });
        intlist.ForEach(n => sum += n);

        Console.WriteLine($"min={min},max={max},sum={sum}");
        
    }
}
